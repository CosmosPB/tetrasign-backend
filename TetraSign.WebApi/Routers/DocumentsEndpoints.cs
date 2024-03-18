// using TetraSign.Core.Application.Configuration;
// using TetraSign.Core.Helpers;

using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using TetraSign.Core.Application.Documents;
using TetraSign.Core.Application.Documents.ThirdPartyDocuments;
using TetraSign.Core.Application.Documents.ThirdPartyDocuments.JSON;

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

        return group;
    }

    static async Task<IResult> UploadDocuments([FromForm]IFormFileCollection files, IDocumentsService documents_service)
    {
        foreach (IFormFile file in files)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            string filedata = await reader.ReadToEndAsync();

            // string utfString = Encoding.UTF8.GetString(filedata, 0, filedata.Length);
            // var a = JsonSerializer.Deserialize<>(utfString);
            DespatchAdviceJSON despatch_advice_json = JsonSerializer.Deserialize<DespatchAdviceJSON>(filedata);
            if (despatch_advice_json == null) continue;
            string[] metadata = file.FileName.Split("-");
            Console.WriteLine(file.FileName);
            string identification_document_number = metadata[0];
            string document_type = metadata[1];
            string document_serial_number = metadata[2];
            string document_number = metadata[3].Split(".")[0];
            string document_id = $"{document_serial_number}-{document_number}";
            List<DespatchAdviceLineDTO> despatch_advice_lines_dto = new();
            foreach (var item in despatch_advice_json.detalle)
            {
                despatch_advice_lines_dto.Add(
                    new DespatchAdviceLineDTO(
                        item.uniMedidaItem,
                        item.canItem,
                        item.desItem,
                        item.codItem
                    )
                );
            }
            DespatchAdviceDTO despatch_advice_dto = new(
                despatch_advice_json.fecEmision,
                despatch_advice_json.horEmision,
                despatch_advice_json.tipDocGuia,
                despatch_advice_json.serNumDocGuia,
                despatch_advice_json.numDocDestinatario,
                despatch_advice_json.tipDocDestinatario,
                despatch_advice_json.rznSocialDestinatario,
                despatch_advice_json.motTrasladoDatosEnvio,
                despatch_advice_json.desMotivoTrasladoDatosEnvio,
                despatch_advice_json.indTransbordoProgDatosEnvio,
                despatch_advice_json.psoBrutoTotalBienesDatosEnvio,
                despatch_advice_json.uniMedidaPesoBrutoDatosEnvio,
                despatch_advice_json.numBultosDatosEnvio,
                despatch_advice_json.modTrasladoDatosEnvio,
                despatch_advice_json.fecInicioTrasladoDatosEnvio,
                despatch_advice_json.numPlacaTransPrivado,
                despatch_advice_json.numDocIdeConductorTransPrivado,
                despatch_advice_json.tipDocIdeConductorTransPrivado,
                despatch_advice_json.nomConductorTransPrivado,
                despatch_advice_json.ubiLlegada,
                despatch_advice_json.dirLlegada,
                despatch_advice_json.ubiPartida,
                despatch_advice_json.dirPartida,
                despatch_advice_lines_dto,
                despatch_advice_json.ublVersionId,
                despatch_advice_json.customizationId
            );

            DocumentDTO<DespatchAdviceDTO> document_dto = new DocumentDTO<DespatchAdviceDTO>(
                null,
                document_id,
                document_type,
                despatch_advice_dto.issue_date.ToDateTime(despatch_advice_dto.issue_hours).ToUniversalTime(),
                DateTime.UtcNow,
                null,
                null,
                despatch_advice_dto,
                file.FileName,
                file.FileName.Split(".")[1],
                "Pending",
                null,
                null
            );

            await documents_service.AddDespatchAdvice(document_dto);
        }
        return TypedResults.NoContent();
    }

    static async Task<IResult> GetDespatchAdvices(IDocumentsService documents_service)
    {
        // logger.LogInformation("{userId} - MSProducts.ProductsEndpoints.GetAllProducts", userId);
        IEnumerable<DocumentDTO<DespatchAdviceDTO>> documents = await documents_service.FindDespatchAdvice();
        return TypedResults.Ok(documents);
    }
}