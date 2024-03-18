using TetraSign.Core.Domain.Documents;

namespace TetraSign.Core.Application.Documents.ThirdPartyDocuments.JSON;

public abstract record ThirdPartyDocumentJSON(
    DateOnly fecEmision,
    TimeOnly horEmision,
    DocumentType tipDocGuia,
    string ublVersionId,
    string customizationId
);