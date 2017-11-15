using System.ComponentModel.DataAnnotations;

namespace vega.Resources
{
    public class ContactResource
    {
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}