using System.Net.Http.Headers;
using System.Text.Json;
using TetraSign.Core.Domain.Configuration;
using TetraSign.Core.Helpers.Sunat;
using System.Security.Cryptography;
using System.Text;
using TetraSign.Core.Domain.Documents;

namespace  TetraSign.Core.Infraestructure;

public static class SunatWebService {

    public static async Task<(DocumentState, string)> CallRestApi(Configuration configuration, string filename, string filename_path, string response_path) {

        string access_token = GetAccessToken(configuration);
        string ticket = await SendDocument(configuration, access_token, filename, filename_path);
        DocumentState state = GetCDRFromRestApi(configuration, access_token, ticket, filename, response_path);

        return (state, ticket);
    }

    public static DocumentState GetCDRFromRestApi(Configuration configuration, string accessToken, string ticket, string filename, string response_path) {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{accessToken}");

        HttpResponseMessage response = client.GetAsync($"{configuration.configuration_sunat_endpoints.despatch_advice_url}/envios/{ticket}").GetAwaiter().GetResult();
        response.EnsureSuccessStatusCode();
        var jsonString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        HttpResponse responseJson = JsonSerializer.Deserialize<HttpResponse>(jsonString);
        if (responseJson.codRespuesta == "0")
        {
            string cdr = responseJson.arcCdr;
            Byte[] bytes = Convert.FromBase64String(cdr);
            File.WriteAllBytes($"{response_path}/R-{filename}", bytes);
            return DocumentState.Accepted;
        }
        else if (responseJson.codRespuesta == "98")
        {
            return DocumentState.Rejected;
        }
        string message = responseJson.error.desError;
        throw new Exception(message);
    }

    public static string GetAccessToken(Configuration configuration) {

        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        KeyValuePair<string, string>[] data = new[]
        {
            new KeyValuePair<string, string>("grant_type", configuration.configuration_sunat_authentication.grant_type),
            new KeyValuePair<string, string>("scope", configuration.configuration_sunat_authentication.scope),
            new KeyValuePair<string, string>("client_id", configuration.configuration_sunat_authentication.client_id),
            new KeyValuePair<string, string>("client_secret", configuration.configuration_sunat_authentication.client_secret),
            new KeyValuePair<string, string>("username", configuration.configuration_sunat_authentication.username),
            new KeyValuePair<string, string>("password", configuration.configuration_sunat_authentication.password)
        };
        string clientId = configuration.configuration_sunat_authentication.client_id;
        HttpResponseMessage response = client.PostAsync($"https://api-seguridad.sunat.gob.pe/v1/clientessol/{clientId}/oauth2/token/", new FormUrlEncodedContent(data)).GetAwaiter().GetResult();
        response.EnsureSuccessStatusCode();
        string jsonString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        Console.WriteLine(jsonString);
        if (jsonString != null) {
            HttpResponse responseJson = JsonSerializer.Deserialize<HttpResponse>(jsonString);
            return responseJson.access_token;
        }
        else throw new Exception("Can't get access token from https://api-seguridad.sunat.gob.pe");
    }

    public static async Task<string> SendDocument(Configuration configuration, string accessToken, string filename, string filename_path)
    {
        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", $"{accessToken}");
        HttpBody requestBody = new HttpBody();
        requestBody.archivo.nomArchivo = filename;
        requestBody.archivo.arcGreZip = await Base64File(filename_path);
        requestBody.archivo.hashZip = GetChecksum(filename_path).ToLower();

        string stringPayload = JsonSerializer.Serialize(requestBody);
        StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        HttpResponseMessage response = client.PostAsync($"{configuration.configuration_sunat_endpoints.despatch_advice_url}/" + filename.Replace(".zip", ""), httpContent).GetAwaiter().GetResult();
        response.EnsureSuccessStatusCode();
        var jsonString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        HttpResponse responseJson = JsonSerializer.Deserialize<HttpResponse>(jsonString);
        return responseJson.numTicket;
    }

    private static async Task<string> Base64File(string filename)
    {
        Byte[] bytes = await File.ReadAllBytesAsync(filename);
        String file = Convert.ToBase64String(bytes);
        return file;
    }

    private static string GetChecksum(string file)
    {
        using FileStream stream = File.OpenRead(file);
        using SHA256 sha = SHA256.Create();
        byte[] bytes = sha.ComputeHash(stream);
        return BitConverter.ToString(bytes).Replace("-", "");
    }
}