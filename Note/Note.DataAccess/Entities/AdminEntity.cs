using System.ComponentModel.DataAnnotations.Schema;

namespace Note.DataAccess.Entities
{
    [Table("admins")]
    public class AdminEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; } 
        public int Age { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<QuestionEntity>? Questions { get; set; }
    }
}
