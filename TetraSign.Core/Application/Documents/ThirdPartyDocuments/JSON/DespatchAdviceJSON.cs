using TetraSign.Core.Domain.Documents;

namespace TetraSign.Core.Application.Documents.ThirdPartyDocuments.JSON;

public record DespatchAdviceJSON(
    DateOnly fecEmision,
    TimeOnly horEmision,
    DocumentType tipDocGuia,
    string serNumDocGuia,
    string numDocDestinatario,
    string tipDocDestinatario,
    string rznSocialDestinatario,
    string motTrasladoDatosEnvio,
    string desMotivoTrasladoDatosEnvio,
    string indTransbordoProgDatosEnvio,
    string psoBrutoTotalBienesDatosEnvio,
    string uniMedidaPesoBrutoDatosEnvio,
    string numBultosDatosEnvio,
    string modTrasladoDatosEnvio,
    DateOnly fecInicioTrasladoDatosEnvio,
    string numPlacaTransPrivado,
    string numDocIdeConductorTransPrivado,
    string tipDocIdeConductorTransPrivado,
    string nomConductorTransPrivado,
    string ubiLlegada,
    string dirLlegada,
    string ubiPartida,
    string dirPartida,
    IEnumerable<DespatchAdviceLineJSON> detalle,
    string ublVersionId,
    string customizationId
): ThirdPartyDocumentJSON(fecEmision, horEmision, tipDocGuia, ublVersionId, customizationId);