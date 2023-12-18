using System.ComponentModel.DataAnnotations.Schema;

namespace Note.DataAccess.Entities
{
    [Table("documents")]
    public class DocumentEntity:BaseEntity
    {
        public string AutorMapping {  get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string TextDocument { get; set; }
        public DateTime? DateCreate { get; set; }
        public int Public {  get; set; } // публичность (то, сколько людей могут смотреть)

        public int AutorId { get; set; }
        public UserEntity Autor { get; set; }

        public virtual ICollection<ListEditorEntity>? ListEditors { get; set; }

    }
}
