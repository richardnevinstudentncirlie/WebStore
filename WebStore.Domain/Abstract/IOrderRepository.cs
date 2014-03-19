using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Domain.Abstract
{
    public interface IOrderRepository
    {

        IEnumerable<Order> Orders { get; }

        void SaveOrder(Order order);

        Order DeleteOrder(int orderID);
    }
}