using System;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using System.Collections.Generic;

namespace WebStore.Domain.Concrete 
{

    public class EFOrderRepository : IOrderRepository 
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Order> Orders 
        {
            get { return context.Orders; }
        }

 
        public void SaveOrder(Order order) 
        {

            if (order.OrderID == 0) 
            {

                order.CreatedAt = DateTime.Now;
                order.UpdatedAt = DateTime.Now;
                context.Orders.Add(order);
            } 
            else 
            {
                Order dbEntry = context.Orders.Find(order.OrderID);
                if (dbEntry != null) 
                {
                    dbEntry.OrderDate = order.OrderDate;
                    dbEntry.ShipDate = order.ShipDate;
                    dbEntry.TotalOrder = order.TotalOrder;
                    dbEntry.ProductCount = order.ProductCount;
                    dbEntry.UpdatedAt = DateTime.Now;
                }
            }
            context.SaveChanges();
        }

        public Order DeleteOrder(int orderID) 
        {
            Order dbEntry = context.Orders.Find(orderID);
            if (dbEntry != null) 
            {
                context.Orders.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}