using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Vega.Core.Entities
{
    public class Make
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public ICollection<Model> Models {get;set;}

        public Make()
        {
            this.Models = new Collection<Model>();
        }
    }
}