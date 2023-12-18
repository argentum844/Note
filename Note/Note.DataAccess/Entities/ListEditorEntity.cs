using System.ComponentModel.DataAnnotations.Schema;

namespace Note.DataAccess.Entities
{
    [Table("list_editors")]
    public class ListEditorEntity:BaseEntity
    {
        public int DocumentId { get; set; }
        public DocumentEntity Document { get; set; }
        public int EditorId { get; set; }
        public UserEntity Editor { get; set; }
    }
}
