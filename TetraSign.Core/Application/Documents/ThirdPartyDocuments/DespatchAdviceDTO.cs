using TetraSign.Core.Domain.Documents;

namespace TetraSign.Core.Application.Documents.ThirdPartyDocuments;

public record DespatchAdviceDTO(
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
    IEnumerable<DespatchAdviceLineDTO> despatch_advice_lines,
    string ubl_version_id,
    string customization_id
): ThirdPartyDocumentDTO(issue_date, issue_hours, document_type, ubl_version_id, customization_id);