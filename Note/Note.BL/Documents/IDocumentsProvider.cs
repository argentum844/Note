using Note.BL.Documents.Entities;

namespace Note.BL.Documents;

public interface IDocumentsProvider
{
    IEnumerable<DocumentModel> GetDocuments(DocumentsModelFilter modelFilter = null);
    DocumentModel GetDocumentInfo(Guid documentId);
}