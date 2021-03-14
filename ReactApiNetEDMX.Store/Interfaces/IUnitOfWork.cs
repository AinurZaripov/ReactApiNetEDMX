using ReactApiNetEDMX.Store.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReactApiNetEDMX.Store.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Products> Products { get; }
    }
}
