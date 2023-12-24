using AutoMapper;
using Note.BL.Documents.Entities;
using Note.DataAccess;
using Note.DataAccess.Entities;
using Note.BL.Documents;

namespace Note.BL.Documents;

public class DocumentsManager : IDocumentsManager
{
    private readonly IRepository<DocumentEntity> _DocumentsRepository;
    private readonly IMapper _mapper;

    public DocumentsManager(IRepository<DocumentEntity> DocumentsRepository, IMapper mapper)
    {
        _DocumentsRepository = DocumentsRepository;
        _mapper = mapper;
    }

    public DocumentModel CreateDocument(CreateDocumentModel model)
    {
        if (model.Public < 0)
        {
            throw new ArgumentException("Public must be greater than 0.");
        }

        var entity = _mapper.Map<DocumentEntity>(model);

        _DocumentsRepository.Save(entity); //id, modification, externalId

        return _mapper.Map<DocumentModel>(entity);
    }

    public void DeleteDocument(Guid DocumentId)
    {
        var entity = _DocumentsRepository.GetById(DocumentId) ?? throw new Exception("Document not found");
        _DocumentsRepository.Delete(entity);
    }

    public DocumentModel UpdateDocument(Guid DocumentId, CreateDocumentModel model)
    {
        //validate data
        var entity = _DocumentsRepository.GetById(DocumentId) ?? throw new Exception("Document not found");
        entity.AutorMapping = model.AutorMapping;
        entity.Title = model.Title;
        entity.Description = model.Description;
        entity.TextDocument = model.TextDocument;
        entity.Public = model.Public;
        _DocumentsRepository.Save(entity);
        return _mapper.Map<DocumentModel>(entity);
    }
}