using System.ComponentModel.DataAnnotations.Schema;

namespace Note.DataAccess.Entities
{
    [Table("questions")]
    public class QuestionEntity:BaseEntity
    {
        public string Question {  get; set; }
        public string? Answer { get; set; }

        public int? AdminId { get; set; }
        public AdminEntity? Admin { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
