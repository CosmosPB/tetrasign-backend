namespace TetraSign.Core.Domain.Documents.ThirdPartyDocuments;

public class DespatchAdvice: ThirdPartyDocument {

    public string? document_number { get; set; }
    public string? identification_document_number { get; set; }
    public string? identification_document_type { get; set; }
    public string? business_name { get; set; }
    public string? transfer_reason_code { get; set; }
    public string? transfer_reason_description { get; set; }
    public string? scheduled_transshipment { get; set; }
    public string? total_gross_weight { get; set; }
    public string? measurement_unit { get; set; }
    public string? quantity_packages { get; set; }
    public string? transportation_mode { get; set; }
    public DateOnly? transportation_start_date { get; set; }
    public string? carriers_plate_number { get; set; }
    public string? carriers_document_number { get; set; }
    public string? carriers_document_type { get; set; }
    public string? carrier_name { get; set; }
    public string? destination_ubigeo { get; set; }
    public string? destination_address { get; set; }
    public string? departure_ubigeo { get; set; }
    public string? departure_address { get; set; }
    public IEnumerable<DespatchAdviceLine> despatch_advice_lines { get; set; }

    public static DespatchAdvice Create(
        DateOnly issue_date,
        TimeOnly issue_hours,
        DocumentType document_type,
        string document_number,
        string identification_document_number,
        string identification_document_type,
        string business_name,
        string transfer_reason_code,
        string transfer_reason_description,
        string scheduled_transshipment,
        string total_gross_weight,
        string measurement_unit,
        string quantity_packages,
        string transportation_mode,
        DateOnly transportation_start_date,
        string carriers_plate_number,
        string carriers_document_number,
        string carriers_document_type,
        string carrier_name,
        string destination_ubigeo,
        string destination_address,
        string departure_ubigeo,
        string departure_address,
        IEnumerable<DespatchAdviceLine> despatch_advice_lines,
        string ubl_version_id,
        string customization_id
    ) {
        DespatchAdvice despatch_advice = new() {
            issue_date = issue_date,
            issue_hours = issue_hours,
            document_type = document_type,
            document_number = document_number,
            identification_document_number = identification_document_number,
            identification_document_type = identification_document_type,
            business_name = business_name,
            transfer_reason_code = transfer_reason_code,
            transfer_reason_description = transfer_reason_description,
            scheduled_transshipment = scheduled_transshipment,
            total_gross_weight = total_gross_weight,
            measurement_unit = measurement_unit,
            quantity_packages = quantity_packages,
            transportation_mode = transportation_mode,
            transportation_start_date = transportation_start_date,
            carriers_plate_number = carriers_plate_number,
            carriers_document_number = carriers_document_number,
            carriers_document_type = carriers_document_type,
            carrier_name = carrier_name,
            destination_ubigeo = destination_ubigeo,
            destination_address = destination_address,
            departure_ubigeo = departure_ubigeo,
            departure_address = departure_address,
            despatch_advice_lines= despatch_advice_lines,
            ubl_version_id = ubl_version_id,
            customization_id = customization_id
        };
        
        return despatch_advice;
    }
}