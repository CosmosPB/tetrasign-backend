namespace TetraSign.Core.Domain.Documents.ThirdPartyDocuments;

public class ThirdPartyDocument {

    public DateOnly? issue_date { get; set; }
    public TimeOnly? issue_hours { get; set; }
    public DocumentType? document_type { get; set; }

    //**/

    public string? ubl_version_id { get; set; }
    public string? customization_id { get; set; }
}