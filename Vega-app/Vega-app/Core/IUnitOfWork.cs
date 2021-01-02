using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega_app.Core
{
  public  interface IUnitOfWork
    {
        Task Complete();
    }
}
