using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp3.Entity;

namespace StoreApp3.Data.Abstract
{
    public interface IOrderRepository
    {
          IQueryable<Order>  Orders { get; }

        void AddOrder(Order Order);
    }
}