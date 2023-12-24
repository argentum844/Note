using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.BL.Documents.Entities
{
    public class DocumentsModelFilter
    {
        public int? MinimumPublic {  get; set; }
        public int? MaximumPublic { get; set;}
        public string? Title { get; set; }
        public string? AutorMapping { get; set; }
    }
}
