using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Domain.Abstract
{
    public interface ICustomerRepository
    {

        IEnumerable<Customer> Customers { get; }

        void SaveCustomer(Customer customer);

        Customer DeleteCustomer(int customerID);
    }
}