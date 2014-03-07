using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Domain.Abstract {
    public interface IProductRepository {

        IEnumerable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productID);
    }
}