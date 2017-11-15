using System.ComponentModel.DataAnnotations;

namespace Vega.Domain.Entities
{
    public class Model
    { 
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public Make Make { get; set; }
        public int MakeId { get; set; }
    }
}