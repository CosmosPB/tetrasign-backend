using AutoMapper;
using TetraSign.Core.Application.Documents.ThirdPartyDocuments;
using TetraSign.Core.Domain.Documents;
using TetraSign.Core.Domain.Documents.ThirdPartyDocuments;
using TetraSign.Core.Helpers;
using TetraSign.Core.Helpers.Database;
using TetraSign.Core.Infraestructure;

namespace TetraSign.Core.Application.Documents;

public class DocumentsService: IDocumentsService {


    private readonly IRepository<Document<DespatchAdvice>, DocumentsDBSettings> despatch_advice_repository;
    private readonly IMapper mapper;

    public DocumentsService(
        IRepository<Document<DespatchAdvice>, DocumentsDBSettings> despatch_advice_repository,
        IMapper mapper
    ) {
        this.despatch_advice_repository = despatch_advice_repository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<DocumentDTO<DespatchAdviceDTO>>> FindDespatchAdvice() {

        IEnumerable<Document<DespatchAdvice>> document = await despatch_advice_repository.Find();

        return mapper.Map<IEnumerable<DocumentDTO<DespatchAdviceDTO>>>(document);
    }

    public async Task<DocumentDTO<DespatchAdviceDTO>> FindDespatchAdviceById(string id) {

        Document<DespatchAdvice> document = await despatch_advice_repository.FindById(id) 
                                                            ?? throw new Exception("Document not found");

        return mapper.Map<DocumentDTO<DespatchAdviceDTO>>(document);
    }

    public async Task<DocumentDTO<DespatchAdviceDTO>> AddDespatchAdvice(DocumentDTO<DespatchAdviceDTO> document) {

        List<DespatchAdviceLine> despatch_advice_lines = new();

        foreach (DespatchAdviceLineDTO line in document.data.despatch_advice_lines)
        {
            despatch_advice_lines.Add(
                DespatchAdviceLine.Create(
                    line.measurement_unit,
                    line.quantity,
                    line.description,
                    line.code
                )
            );
        }

        DespatchAdvice despatch_advice = DespatchAdvice.Create(
            document.data.issue_date,
            document.data.issue_hours,
            document.data.document_type,
            document.data.document_number,
            document.data.identification_document_number,
            document.data.identification_document_type,
            document.data.business_name,
            document.data.transfer_reason_code,
            document.data.transfer_reason_description,
            document.data.scheduled_transshipment,
            document.data.total_gross_weight,
            document.data.measurement_unit,
            document.data.quantity_packages,
            document.data.transportation_mode,
            document.data.transportation_start_date,
            document.data.carriers_plate_number,
            document.data.carriers_document_number,
            document.data.carriers_document_type,
            document.data.carrier_name,
            document.data.destination_ubigeo,
            document.data.destination_address,
            document.data.departure_ubigeo,
            document.data.departure_address,
            despatch_advice_lines,
            document.data.ubl_version_id,
            document.data.customization_id
        );

        Document<DespatchAdvice> new_document = Document<DespatchAdvice>.Create(
            document.document_id,
            document.document_type,
            document.issue_date,
            document.upload_date,
            document.sign_date,
            document.send_date,
            despatch_advice,
            document.filename,
            document.extension,
            document.state,
            document.observation,
            document.ticket_id
        );

        await despatch_advice_repository.Add(new_document);

        return mapper.Map<Document<DespatchAdvice>, DocumentDTO<DespatchAdviceDTO>>(new_document);
    }
}