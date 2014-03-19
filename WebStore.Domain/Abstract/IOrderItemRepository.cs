using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Domain.Abstract
{
    public interface IOrderItemRepository
    {

        IEnumerable<OrderItem> OrderItems { get; }

        void SaveOrderItem(OrderItem orderItem);

        OrderItem DeleteOrderItem(int orderItemID);
    }
}