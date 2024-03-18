using TetraSign.Core.Application.Documents.ThirdPartyDocuments;

namespace TetraSign.Core.Application.Documents;

public interface IDocumentsService {

    Task<IEnumerable<DocumentDTO<DespatchAdviceDTO>>> FindDespatchAdvice();
    Task<DocumentDTO<DespatchAdviceDTO>> FindDespatchAdviceById(string id);
    Task<DocumentDTO<DespatchAdviceDTO>> AddDespatchAdvice(DocumentDTO<DespatchAdviceDTO> document);
    // Task Update(DocumentDTO<TEntity> document);
}