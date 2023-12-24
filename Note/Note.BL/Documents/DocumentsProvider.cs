using AutoMapper;
using Note.BL.Documents.Entities;
using Note.DataAccess;
using Note.DataAccess.Entities;

namespace Note.BL.Documents;

public class DocumentsProvider : IDocumentsProvider
{
    private readonly IRepository<DocumentEntity> _DocumentRepository;
    private readonly IMapper _mapper;

    public DocumentsProvider(IRepository<DocumentEntity> DocumentsRepository, IMapper mapper, int minimumDocumentAge)
    {
        _DocumentRepository = DocumentsRepository;
        _mapper = mapper;
    }

    public IEnumerable<DocumentModel> GetDocuments(DocumentsModelFilter modelFilter = null)
    {
        var minimumPublic = modelFilter?.MinimumPublic;
        var maximumPublic = modelFilter?.MaximumPublic;

        var currentDate = DateTime.UtcNow;

        var Documents = _DocumentRepository.GetAll(x =>
            (minimumPublic == null || x.Public > minimumPublic) &&
            (maximumPublic == null || x.Public < maximumPublic));

        return _mapper.Map<IEnumerable<DocumentModel>>(Documents);
    }

    public DocumentModel GetDocumentInfo(Guid DocumentId)
    {
        var Document = _DocumentRepository.GetById(DocumentId); //return null if not exists
        if (Document is null)
        {
            throw new ArgumentException("Document not found.");
        }

        return _mapper.Map<DocumentModel>(Document);
    }
}