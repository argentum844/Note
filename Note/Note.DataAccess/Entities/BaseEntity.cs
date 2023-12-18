using System.ComponentModel.DataAnnotations;

namespace Note.DataAccess.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; } //ключ в бд

        public Guid ExternalId { get; set; } // уникальный индекс, уникальный опционал
        public DateTime ModificationTime { get; set; }
        public DateTime CreationTime { get; set; }

        public bool IsNew()
        {
            return ExternalId == Guid.Empty;
        }
        public void Init()
        {
            ModificationTime = DateTime.Now;
            ExternalId = Guid.NewGuid();
            CreationTime = DateTime.Now;
        }
    }
}
