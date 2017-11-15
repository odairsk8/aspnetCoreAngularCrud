using System;
using System.Threading.Tasks;

namespace vega.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext context;

        public UnitOfWork(VegaDbContext context)
        {
            this.context = context;
        }

        public Task CompleteAsync()
        {
            return this.context.SaveChangesAsync();
        }
    }
}
