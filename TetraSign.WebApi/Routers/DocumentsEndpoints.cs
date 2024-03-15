// using TetraSign.Core.Application.Configuration;
// using TetraSign.Core.Helpers;

using System.Text;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;

namespace TetraSign.WebApi.Routers;

public static class DocumentsEndpoint
{
    public static RouteGroupBuilder MapDocumentsApi(this RouteGroupBuilder group)
    {
        group.MapPost("/", UploadDocuments)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError)
            .Accepts<IFormFileCollection>("multipart/form-data")
            .WithName("UploadDocuments")
            .WithSummary("An endpoint to upload documents")
            .WithDescription("An endpoint to upload documents")
            .WithOpenApi();

        return group;
    }

    static async Task<IResult> UploadDocuments([FromForm]IFormFileCollection files)
    {
        foreach (IFormFile file in files)
        {
            string filedata;

            using var reader = new StreamReader(file.OpenReadStream());
            filedata = await reader.ReadToEndAsync();

            // string utfString = Encoding.UTF8.GetString(filedata, 0, filedata.Length);
            // var a = JsonSerializer.Deserialize<>(utfString);
            Console.WriteLine("====utfString====");
            Console.WriteLine(filedata);
            Console.WriteLine("====utfString====");
        }
        return TypedResults.NoContent();
    }
}