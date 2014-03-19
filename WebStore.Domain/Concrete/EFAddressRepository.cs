using System;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using System.Collections.Generic;

namespace WebStore.Domain.Concrete 
{

    public class EFAddressRepository : IAddressRepository 
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Address> Addresses 
        {
            get { return context.Addresses; }
        }

 
        public void SaveAddress(Address address) 
        {

            if (address.AddressID == 0) 
            {

                address.CreatedAt = DateTime.Now;
                address.UpdatedAt = DateTime.Now;
                context.Addresses.Add(address);
            } 
            else 
            {
                Address dbEntry = context.Addresses.Find(address.AddressID);
                if (dbEntry != null) 
                {
                    dbEntry.StreetLine1 = address.StreetLine1;
                    dbEntry.StreetLine2 = address.StreetLine2;
                    dbEntry.StreetLine3 = address.StreetLine3;
                    dbEntry.City = address.City;
                    dbEntry.County = address.County;
                    dbEntry.Country = address.Country;
                    dbEntry.PostalCode = address.PostalCode;
                    dbEntry.UpdatedAt = DateTime.Now;
                }
            }
            context.SaveChanges();
        }

        public Address DeleteAddress(int addressID) 
        {
            Address dbEntry = context.Addresses.Find(addressID);
            if (dbEntry != null) 
            {
                context.Addresses.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}