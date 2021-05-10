using ReactApiNetEDMX.Store.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactApiNetEDMX.Store.Interfaces
{
    public interface IUsersRepository
    {
        Users FindByLoginAndPassword(string username, string password);
    }
}
