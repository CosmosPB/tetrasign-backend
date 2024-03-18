namespace TetraSign.Core.Application.Documents.ThirdPartyDocuments;

public record DespatchAdviceLineDTO(
    string measurement_unit,
    string quantity,
    string description,
    string code
);