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

                    dbEntry.FirstName = order.FirstName;
                    dbEntry.LastName = order.LastName;
                    dbEntry.Company = order.Company;
                    dbEntry.Email = order.Email;
                    dbEntry.StreetLine1 = order.StreetLine1;
                    dbEntry.StreetLine2 = order.StreetLine2;
                    dbEntry.StreetLine3 = order.StreetLine3;
                    dbEntry.City = order.City;
                    dbEntry.PostalCode = order.PostalCode;
                    dbEntry.County = order.County;
                    dbEntry.Country = order.Country;
                    dbEntry.PaymentConfirmation = order.PaymentConfirmation;

                    dbEntry.UpdatedAt = DateTime.Now;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }

              throw raise;
            }
            catch (Exception e)
            {
                int i = 0;
                Console.WriteLine("In Main catch block. Caught: {0}", e.Message);
                Console.WriteLine("Inner Exception is {0}", e.InnerException);

            }
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