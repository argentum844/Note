using System.ComponentModel.DataAnnotations.Schema;

namespace Note.DataAccess.Entities
{
    [Table("rules")]
    public class RulesEntity:BaseEntity
    {
        public string TextRule { get; set; }
    }
}
