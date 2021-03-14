//using Microsoft.EntityFrameworkCore;
//using ReactApiNetEDMX.Store.EF;
//using ReactApiNetEDMX.Store.Entities;
//using ReactApiNetEDMX.Store.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace ReactApiNetEDMX.Store.Repositories
//{
//    public class ProductsRepository : IGenericRepository<Products> 
//    {
//        ShopContext _context;
//        public ProductsRepository(ShopContext context)
//        {
//            _context = context;
//        }
//        public void Create(Products item)
//        {
//            Create(item);
//        }
//        public IEnumerable<Products> Get(Dictionary<string, string> filters)
//        {
//            var shopGet = _context.Products.AsNoTracking().AsQueryable();
//            if (filters != null)
//            {
//                int valueFilter;
//                foreach (var key in filters)
//                {
//                    valueFilter = Convert.ToInt32(key.Value);
//                    switch (key.Key)
//                    {
//                        case "id":
//                            shopGet = shopGet.Where(w => w.Id.ToString().Contains(key.Value));
//                            break;
//                        case "AddressId":
//                            shopGet = shopGet.Where(w => w.AddressId.ToString().Contains(key.Value));
//                            break;
//                        default:
//                            break;
//                    }
//                }
//            }
//            return shopGet.ToList();

//            //var shop = _context.Products.AsQueryable();
//            //if (filters.Count > 0)
//            //{
//            //    int valueFilter;
//            //    foreach (var key in filters)
//            //    {
//            //        valueFilter = Convert.ToInt32(key.Value);
//            //        switch (key.Key)
//            //        {
//            //            case "id":
//            //                shop = shop.Where(w => w.Id == valueFilter);
//            //                break;
//            //            default:
//            //                break;
//            //        }
//            //    }
//            //}
//            //return shop.ToList();
//        }

//        public IEnumerable<Products> Get()
//        {
//            throw new NotImplementedException();
//        }

//        void IGenericRepository<Products>.Create(Products item)
//        {
//            throw new NotImplementedException();
//        }

//        Products IGenericRepository<Products>.FindById(int id)
//        {
//            throw new NotImplementedException();
//        }

//        IEnumerable<Products> IGenericRepository<Products>.Get(Func<Products, bool> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        void IGenericRepository<Products>.Remove(Products item)
//        {
//            throw new NotImplementedException();
//        }

//        void IGenericRepository<Products>.Update(Products item)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}