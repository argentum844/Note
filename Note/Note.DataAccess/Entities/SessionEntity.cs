using System.ComponentModel.DataAnnotations.Schema;

namespace Note.DataAccess.Entities
{
    [Table("sessions")]
    public class SessionEntity:BaseEntity
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
