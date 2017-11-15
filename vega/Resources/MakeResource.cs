using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace vega.Resources
{
    public class MakeResource : KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Models {get;set;}

        public MakeResource()
        {
            this.Models = new Collection<KeyValuePairResource>();
        }
    }
}