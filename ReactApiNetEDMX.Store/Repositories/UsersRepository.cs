using ReactApiNetEDMX.Store.Database;
using ReactApiNetEDMX.Store.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactApiNetEDMX.Store.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        ShopEntities _context;
        DbSet<Users> _dbSet;

        public UsersRepository(ShopEntities context)
        {
            _context = context;
            _dbSet = _context.Set<Users>();
        }

        public Users FindByLoginAndPassword(string login, string password)
        {
            return _dbSet.FirstOrDefault(u => u.Login == login && u.Password == password);
        }
    }
}
