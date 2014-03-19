using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Domain.Abstract
{
    public interface IAddressRepository
    {

        IEnumerable<Address> Addresses { get; }

        void SaveAddress(Address address);

        Address DeleteAddress(int addressID);
    }
}