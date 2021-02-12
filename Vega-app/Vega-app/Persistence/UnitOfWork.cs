using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega_app.Core;

namespace Vega_app.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext dbContext;

        public UnitOfWork(VegaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CompleteAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
