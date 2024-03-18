namespace TetraSign.Core.Domain.Documents.ThirdPartyDocuments;

public class DespatchAdviceLine {

    public string? measurement_unit { get; set; }
    public string? quantity { get; set; }
    public string? description { get; set; }
    public string? code { get; set; }

    public static DespatchAdviceLine Create(
        string measurement_unit,
        string quantity,
        string description,
        string code
    ) {
        DespatchAdviceLine despatch_advice_line = new() {
            measurement_unit = measurement_unit,
            quantity = quantity,
            description = description,
            code = code
        };
        
        return despatch_advice_line;
    }
}