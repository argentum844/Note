namespace Note.BL.Documents.Entities;

public class CreateDocumentModel
{
    public string AutorMapping { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string TextDocument { get; set; }
    public int Public { get; set; }
}