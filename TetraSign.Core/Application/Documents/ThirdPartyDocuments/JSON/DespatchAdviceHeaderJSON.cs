using TetraSign.Core.Domain.Documents;

namespace TetraSign.Core.Application.Documents.ThirdPartyDocuments.JSON;

public record DespatchAdviceHeaderJSON(
    DateOnly fecEmision,
    TimeOnly horEmision,
    string tipDocGuia,
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
    string ublVersionId,
    string customizationId
): ThirdPartyDocumentJSON(fecEmision, horEmision, tipDocGuia, ublVersionId, customizationId);