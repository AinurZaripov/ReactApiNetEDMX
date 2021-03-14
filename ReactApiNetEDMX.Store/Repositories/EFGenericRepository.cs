using ReactApiNetEDMX.Store.Database;
using ReactApiNetEDMX.Store.Interface;
using ReactApiNetEDMX.Store.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReactApiNetEDMX.Store.Repositories
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> 
        where TEntity : class, IEntity
    {
        ShopEntities _context;
        DbSet<TEntity> _dbSet;
        public EFGenericRepository(ShopEntities context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }
        //public IEnumerable<TEntity> Get(Dictionary<string, string> filters)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual IEnumerable<TEntity> Get(Dictionary<string, string> filters)
        {
            var shopGet = _dbSet.AsNoTracking().AsQueryable();
            if (filters != null)
            {
                int valueFilter;
                foreach (var key in filters)
                {
                    valueFilter = Convert.ToInt32(key.Value);
                    switch (key.Key)
                    {
                        case "id":
                            shopGet = shopGet.Where(w => w.Id.ToString().Contains(key.Value));
                            break;
                        //case "name":
                        //    shopGet = shopGet.Where(w => w.Id.ToString().Contains(key.Value));
                        //    break;
                        default:
                            break;
                    }
                }
            }
            return shopGet.ToList();
        }
        public virtual IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        public virtual TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }
        public virtual void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public virtual void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        public virtual IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public virtual IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        
    }
}