using Microsoft.AspNetCore.Mvc;
using TetraSign.Core.Application.Documents;
using TetraSign.SDK.SignXML.ThirdPartyDocuments.DTO;

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

        
        group.MapGet("/despatch-advices", GetDespatchAdvices)
            .Produces<List<DocumentDTO<DespatchAdviceDTO>>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetDespatchAdvices")
            .WithSummary("An endpoint to get all despatch advices")
            .WithDescription("An endpoint to get all despatch advices")
            .WithOpenApi();

        group.MapDelete("/despatch-advices/{id}", DeleteDespatchAdvices)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("DeleteDespatchAdvices")
            .WithSummary("An endpoint to delete a despatch advices")
            .WithDescription("An endpoint to delete a despatch advices")
            .WithOpenApi();

        group.MapPost("/send-sunat", SendSunat)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError)
            .Accepts<DocumentFilenamesDTO>("application/json")
            .WithName("SendSunat")
            .WithSummary("An endpoint to send documents to sunat")
            .WithDescription("An endpoint to send documents to sunat")
            .WithOpenApi();

        return group;
    }

    static async Task<IResult> UploadDocuments([FromForm]IFormFileCollection files, IDocumentsService documents_service)
    {
        Dictionary<string, string> documents = new();
        foreach (IFormFile file in files)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            string filedata = await reader.ReadToEndAsync();
            documents.Add(file.FileName, filedata);
        }
        await documents_service.AddDocuments(documents); // AddDocuments<DespatchAdviceJSON, DocumentDTO<DespatchAdviceDTO>>
        return TypedResults.NoContent();
    }

    static async Task<IResult> GetDespatchAdvices(IDocumentsService documents_service)
    {
        // logger.LogInformation("{userId} - MSProducts.ProductsEndpoints.GetAllProducts", userId);
        IEnumerable<DocumentDTO<DespatchAdviceDTO>> documents = await documents_service.FindDespatchAdvice();
        return TypedResults.Ok(documents);
    }

    static async Task<IResult> DeleteDespatchAdvices([FromRoute]string id, IDocumentsService documents_service)
    {
        // logger.LogInformation("{userId} - MSProducts.ProductsEndpoints.GetAllProducts", userId);
        await documents_service.DeleteDespatchAdvice(id);
        return TypedResults.NoContent();
    }

    static async Task<IResult> SendSunat(DocumentFilenamesDTO documents, IDocumentsService documents_service)
    {
        // logger.LogInformation("{userId} - MSProducts.ProductsEndpoints.GetAllProducts", userId);
        // ConfigurationDTO new_configuration = await configuration_service.Add(configuration);
        await documents_service.SendSunat(documents.filenames);
        return TypedResults.NoContent();
    }
}