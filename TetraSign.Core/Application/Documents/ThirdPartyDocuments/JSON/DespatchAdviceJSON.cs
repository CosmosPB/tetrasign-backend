namespace TetraSign.Core.Application.Documents.ThirdPartyDocuments.JSON;

public record DespatchAdviceJSON(
    DespatchAdviceHeaderJSON cabecera,
    IEnumerable<DespatchAdviceLineJSON> detalle
);