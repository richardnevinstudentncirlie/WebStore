using System;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using System.Collections.Generic;

namespace WebStore.Domain.Concrete 
{

    public class EFOrderItemRepository : IOrderItemRepository 
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<OrderItem> OrderItems 
        {
            get { return context.OrderItems; }
        }

 
        public void SaveOrderItem(OrderItem orderItem) 
        {

            if (orderItem.OrderItemID == 0) 
            {

                orderItem.CreatedAt = DateTime.Now;
                orderItem.UpdatedAt = DateTime.Now;
                context.OrderItems.Add(orderItem);
            } 
            else 
            {
                OrderItem dbEntry = context.OrderItems.Find(orderItem.OrderItemID);
                if (dbEntry != null) 
                {
                    dbEntry.Name = orderItem.Name;
                    dbEntry.Description = orderItem.Description;
                    dbEntry.Price = orderItem.Price;
                    dbEntry.Category = orderItem.Category;
                    dbEntry.ImageData = orderItem.ImageData;
                    dbEntry.ImageMimeType = orderItem.ImageMimeType;
                    dbEntry.Special = orderItem.Special;
                    dbEntry.Seller = orderItem.Seller;
                    dbEntry.Buyer = orderItem.Buyer;
                    dbEntry.Quantity = orderItem.Quantity;
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

        public OrderItem DeleteOrderItem(int orderItemID) 
        {
            OrderItem dbEntry = context.OrderItems.Find(orderItemID);
            if (dbEntry != null) 
            {
                context.OrderItems.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}