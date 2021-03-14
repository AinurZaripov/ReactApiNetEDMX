using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactApiNetEDMX.Store.Repositories
{
    public partial class ShopEntities : DbContext
    {
        public ShopEntities(string connetctionString)
            : base(connetctionString)
        {
            //this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}
