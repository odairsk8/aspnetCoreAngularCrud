using System.Collections.Generic;
using Vega.Domain.Entities;

namespace Vega.Domain.Services
{
    public class MakeService
    {
        public MakeService()
        {
        }

        public List<Make> GetAll()
        {
            List<Make> result = new List<Make>();
            for (int i = 0; i < 5; i++)
            {

                result.Add(new Make()
                {
                    Id = i,
                    Name = "Make" + i.ToString(),
                    Models = new List<Model>() { new Model() { Id = i, Name = "Model" + "i" } }
                });
            }
            return result;
        }
    }
}
