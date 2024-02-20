namespace TetraSign.Core.Domain.Configuration;

public class ConfigurationSunatAuthentication {

    public string? grant_type { get; protected set; }
    public string? scope { get; protected set; }
    public string? client_id { get; protected set; }
    public string? client_secret { get; protected set; }
    public string? username { get; protected set; }
    public string? password { get; protected set; }

    public static ConfigurationSunatAuthentication Create(
        string grant_type,
        string scope,
        string client_id,
        string client_secret,
        string username,
        string password
    ) {
        ConfigurationSunatAuthentication configuration_sunat_authentication = new() {
            grant_type = grant_type,
            scope = scope,
            client_id = client_id,
            client_secret = client_secret,
            username = username,
            password = password
        };
        
        return configuration_sunat_authentication;
    }
}