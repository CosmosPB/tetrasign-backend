using TetraSign.Core.Domain.Documents;

namespace TetraSign.Core.Application.Documents.ThirdPartyDocuments;

public abstract record ThirdPartyDocumentDTO(
    DateOnly issue_date,
    TimeOnly issue_hours,
    DocumentType document_type,
    string ubl_version_id,
    string customization_id
);