using WebStore.Domain.Entities;

namespace WebStore.Domain.Abstract 
{

    public interface IOrderProcessor 
    {

        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
