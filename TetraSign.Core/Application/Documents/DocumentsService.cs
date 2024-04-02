using System.Text.Json;
using AutoMapper;
using TetraSign.Core.Domain.Documents;
using TetraSign.SDK.SignXML;
using TetraSign.SDK.SignXML.ThirdPartyDocuments;
using TetraSign.SDK.SignXML.ThirdPartyDocuments.DTO;
using TetraSign.SDK.SignXML.ThirdPartyDocuments.JSON;
using TetraSign.Core.Helpers.Database;
using TetraSign.Core.Infraestructure;
using DomainConfiguration = TetraSign.Core.Domain.Configuration;

namespace TetraSign.Core.Application.Documents;

public class DocumentsService: IDocumentsService {

    private readonly IRepository<DomainConfiguration.Configuration, ConfigurationDBSettings> configuration_repository;
    private readonly IRepository<Document<DespatchAdvice>, DocumentsDBSettings> despatch_advice_repository;
    private readonly IMapper mapper;

    public DocumentsService(
        IRepository<DomainConfiguration.Configuration, ConfigurationDBSettings> configuration_repository,
        IRepository<Document<DespatchAdvice>, DocumentsDBSettings> despatch_advice_repository,
        IMapper mapper
    ) {
        this.configuration_repository = configuration_repository;
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

        IEnumerable<DomainConfiguration.Configuration> configurations = await configuration_repository.Find();
        if (!configurations.Any()) throw new Exception("Could not find any settings");
        DomainConfiguration.Configuration configuration = configurations.First();

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
            document.data.total_gross_weight,
            document.data.measurement_unit,
            document.data.quantity_packages,
            document.data.transportation_mode,
            document.data.transportation_start_date,
            document.data.carriers_plate_number,
            document.data.carriers_document_number,
            document.data.carriers_document_type,
            document.data.carrier_name,
            document.data.carriers_licence_number,
            document.data.destination_ubigeo,
            document.data.destination_address,
            document.data.departure_ubigeo,
            document.data.departure_address,
            despatch_advice_lines,
            document.data.observation,
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

        #nullable disable warnings
        Dictionary<string, string> metadata = new() {
            { "party_identification", configuration.party_identification },
            { "party_name", configuration.party_name },
            { "registration_name", configuration.registration_name },
            { "certificate", configuration.configuration_paths.certificate },
            { "certificate_password", configuration.configuration_paths.certificate_password },
            { "output_path", configuration.configuration_paths.output }
        };

        SignXML.SignDespatchAdvice(
            new_document.data,
            new_document.document_id,
            configuration.configuration_paths.despatch_advice_template,
            new_document.filename.Split(".")[0],
            metadata
        );
        #nullable enable warnings
        await despatch_advice_repository.Add(new_document);

        return mapper.Map<Document<DespatchAdvice>, DocumentDTO<DespatchAdviceDTO>>(new_document);
    }

    public async Task<IEnumerable<DocumentDTO<DespatchAdviceDTO>>> AddDespatchAdvice(Dictionary<string, DespatchAdviceJSON> documents) {

        List<DocumentDTO<DespatchAdviceDTO>> result = new();
        foreach (KeyValuePair<string, DespatchAdviceJSON> document in documents)
        {
            string[] metadata = document.Key.Split("-");
            string document_type = metadata[1];
            string document_serial_number = metadata[2];
            string document_number = metadata[3].Split(".")[0];
            string document_id = $"{document_serial_number}-{document_number}";
            List<DespatchAdviceLineDTO> despatch_advice_lines_dto = new();
            foreach (var item in document.Value.detalle)
            {
                despatch_advice_lines_dto.Add(
                    new DespatchAdviceLineDTO(
                        item.uniMedidaItem,
                        item.canItem,
                        item.desItem,
                        item.codItem
                    )
                );
            }
            DespatchAdviceDTO despatch_advice_dto = new(
                document.Value.cabecera.fecEmision,
                document.Value.cabecera.horEmision,
                (DocumentType)Convert.ToInt16(document.Value.cabecera.tipDocGuia),
                document.Value.cabecera.serNumDocGuia,
                document.Value.cabecera.numDocDestinatario,
                document.Value.cabecera.tipDocDestinatario,
                document.Value.cabecera.rznSocialDestinatario,
                document.Value.cabecera.motTrasladoDatosEnvio,
                document.Value.cabecera.desMotivoTrasladoDatosEnvio,
                document.Value.cabecera.psoBrutoTotalBienesDatosEnvio,
                document.Value.cabecera.uniMedidaPesoBrutoDatosEnvio,
                document.Value.cabecera.numBultosDatosEnvio,
                document.Value.cabecera.modTrasladoDatosEnvio,
                document.Value.cabecera.fecInicioTrasladoDatosEnvio,
                document.Value.cabecera.numPlacaTransPrivado,
                document.Value.cabecera.numDocIdeConductorTransPrivado,
                document.Value.cabecera.tipDocIdeConductorTransPrivado,
                document.Value.cabecera.nomConductorTransPrivado,
                document.Value.cabecera.numLicenciaTransPrivado,
                document.Value.cabecera.ubiLlegada,
                document.Value.cabecera.dirLlegada,
                document.Value.cabecera.ubiPartida,
                document.Value.cabecera.dirPartida,
                despatch_advice_lines_dto,
                document.Value.cabecera.obsGuia,
                document.Value.cabecera.ublVersionId,
                document.Value.cabecera.customizationId
            );

            DocumentDTO<DespatchAdviceDTO> document_dto = new(
                null,
                document_id,
                document_type,
                despatch_advice_dto.issue_date.ToDateTime(despatch_advice_dto.issue_hours).ToUniversalTime(),
                DateTime.UtcNow,
                null,
                null,
                despatch_advice_dto,
                document.Key,
                document.Key.Split(".")[1],
                DocumentState.Unprocessed.ToString(),
                null,
                null
            );

            document_dto = await AddDespatchAdvice(document_dto);
            result.Add(document_dto);
        }

        return result.AsEnumerable();
    }

    public async Task<IEnumerable<U>> AddDocuments<T, U>(Dictionary<string, T> documents) {

        Type typeParameterType = typeof(T);
        IEnumerable<U> result = Enumerable.Empty<U>();
        Console.WriteLine(typeParameterType.ToString());
        switch(typeParameterType.ToString()) {
            case "TetraSign.Core.Application.Documents.ThirdPartyDocuments.JSON.DespatchAdviceJSON": 
                IEnumerable<DocumentDTO<DespatchAdviceDTO>> savedDocuments = await AddDespatchAdvice((Dictionary<string, DespatchAdviceJSON>)(object) documents);
                result = (IEnumerable<U>) savedDocuments;
                break;
            default:
                break;
        }

        return result;
        // return (T)(object)(new DespatchAdviceJSON)
        // IEnumerable<T>
    }

    public async Task<IEnumerable<string>> AddDocuments(Dictionary<string, string> documents) {

        List<string> result = new(); 
        Dictionary<string, DespatchAdviceJSON> despatch_advices_json = new();

        foreach (KeyValuePair<string, string> document in documents)
        {
            string[] metadata = document.Key.Split("-");
            DocumentType document_type = DocumentType.Unknown;
            if(metadata.Length > 1) {
                bool parse_document_type = Enum.TryParse(metadata[1], out document_type);
                if(!parse_document_type || document_type == DocumentType.Unknown) continue;
            }
            
            switch (document_type) {
                case DocumentType.DespatchAdvice:
                    DespatchAdviceJSON? despatch_advice_json = JsonSerializer.Deserialize<DespatchAdviceJSON>(document.Value);
                    if (despatch_advice_json == null) continue;
                    else despatch_advices_json.Add(document.Key, despatch_advice_json);
                break;
                default:
                break;
            }
        }

        if(despatch_advices_json.Count > 0) {
            IEnumerable<DocumentDTO<DespatchAdviceDTO>> despatch_advices = await AddDespatchAdvice(despatch_advices_json);
            result.AddRange(despatch_advices.Select(x => x.document_id));
        }

        return result.AsEnumerable();        
    }

    public async Task DeleteDespatchAdvice(string id) {
        await despatch_advice_repository.Remove(id);
    }
}