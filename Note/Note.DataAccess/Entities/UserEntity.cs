using System.ComponentModel.DataAnnotations.Schema;

namespace Note.DataAccess.Entities
{
    [Table("users'")]
    public class UserEntity:BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<QuestionEntity>? Questions { get; set; }
        public virtual ICollection<SessionEntity>? Sessions { get; set; }
        public virtual ICollection<DocumentEntity>? Documents { get; set; }
        public virtual ICollection<ListEditorEntity>? ListEditors { get; set; }
    }
}
