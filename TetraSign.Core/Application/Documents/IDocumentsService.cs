using TetraSign.Core.Application.Documents.ThirdPartyDocuments;
using TetraSign.Core.Application.Documents.ThirdPartyDocuments.JSON;

namespace TetraSign.Core.Application.Documents;

public interface IDocumentsService {

    Task<IEnumerable<DocumentDTO<DespatchAdviceDTO>>> FindDespatchAdvice();
    Task<DocumentDTO<DespatchAdviceDTO>> FindDespatchAdviceById(string id);
    Task<DocumentDTO<DespatchAdviceDTO>> AddDespatchAdvice(DocumentDTO<DespatchAdviceDTO> document);
    Task<IEnumerable<DocumentDTO<DespatchAdviceDTO>>> AddDespatchAdvice(Dictionary<string, DespatchAdviceJSON> documents);
    Task<IEnumerable<U>> AddDocuments<T, U>(Dictionary<string, T> documents);
    Task DeleteDespatchAdvice(string id);
}