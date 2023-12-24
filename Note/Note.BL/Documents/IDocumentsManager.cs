using Note.BL.Documents.Entities;

namespace Note.BL.Documents;

public interface IDocumentsManager
{
    DocumentModel CreateDocument(CreateDocumentModel model);
    void DeleteDocument(Guid documentId);
    DocumentModel UpdateDocument(Guid documentId, CreateDocumentModel model);
}