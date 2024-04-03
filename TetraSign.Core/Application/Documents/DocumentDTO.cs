namespace TetraSign.Core.Application.Documents;

public record DocumentDTO<TEntity>(
    string id,
    string document_id,
    string document_type,
    DateTime issue_date,
    DateTime upload_date,
    DateTime? sign_date,
    DateTime? send_date,
    TEntity data,
    string filename,
    string extension,
    string state,
    string observation,
    string ticket_id
);