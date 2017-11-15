using System.ComponentModel.DataAnnotations;

namespace Vega.Core.Entities
{
    public class Feature
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
    }
}