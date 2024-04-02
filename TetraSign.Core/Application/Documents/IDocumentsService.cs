using TetraSign.SDK.SignXML.ThirdPartyDocuments.DTO;
using TetraSign.SDK.SignXML.ThirdPartyDocuments.JSON;

namespace TetraSign.Core.Application.Documents;

public interface IDocumentsService {

    Task<IEnumerable<DocumentDTO<DespatchAdviceDTO>>> FindDespatchAdvice();
    Task<DocumentDTO<DespatchAdviceDTO>> FindDespatchAdviceById(string id);
    Task<DocumentDTO<DespatchAdviceDTO>> AddDespatchAdvice(DocumentDTO<DespatchAdviceDTO> document);
    Task<IEnumerable<DocumentDTO<DespatchAdviceDTO>>> AddDespatchAdvice(Dictionary<string, DespatchAdviceJSON> documents);
    [Obsolete("AddDocuments with specific type is deprecated")]
    Task<IEnumerable<U>> AddDocuments<T, U>(Dictionary<string, T> documents);
    Task<IEnumerable<string>> AddDocuments(Dictionary<string, string> documents);
    Task DeleteDespatchAdvice(string id);
}