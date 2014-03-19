using System;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using System.Collections.Generic;

namespace WebStore.Domain.Concrete 
{

    public class EFCustomerRepository : ICustomerRepository 
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Customer> Customers 
        {
            get { return context.Customers; }
        }

 
        public void SaveCustomer(Customer customer) 
        {

            if (customer.CustomerID == 0) 
            {

                customer.CreatedAt = DateTime.Now;
                customer.UpdatedAt = DateTime.Now;
                context.Customers.Add(customer);
            } 
            else 
            {
                Customer dbEntry = context.Customers.Find(customer.CustomerID);
                if (dbEntry != null) 
                {
                    dbEntry.FirstName = customer.FirstName;
                    dbEntry.LastName = customer.LastName;
                    dbEntry.LastName = customer.LastName;
                    dbEntry.Company = customer.Company;
                    dbEntry.Email = customer.Email;
                    dbEntry.Phone = customer.Phone;
                    dbEntry.UpdatedAt = DateTime.Now;
                }
            }
            context.SaveChanges();
        }

        public Customer DeleteCustomer(int customerID) 
        {
            Customer dbEntry = context.Customers.Find(customerID);
            if (dbEntry != null) 
            {
                context.Customers.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}