using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp3.Data.Abstract;
using StoreApp3.Entity;

namespace StoreApp3.Data.Concrete.EfCore
{
    public class EfOrderRepository : IOrderRepository
    {
         private IdentityContext _context;

        public EfOrderRepository(IdentityContext context)
        {
            _context = context;
        }
        public IQueryable<Order> Orders => _context.Orders;

        public void AddOrder(Order order)
        {
           _context.Orders.Add(order);
            _context.SaveChanges();
        
        }
    }
}