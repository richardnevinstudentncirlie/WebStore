using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using WebMatrix.WebData;


namespace WebStore.WebUI.Controllers
{

    [Authorize(Roles = "Customer")]
    public class AdminAddressController : Controller
    {
        private ICustomerRepository repositoryCustomer;
        private IAddressRepository repository;

        public AdminAddressController(IAddressRepository repo, ICustomerRepository repoCustomer)
        {
            repository = repo;
            repositoryCustomer = repoCustomer;
        }

        public ViewResult Index()
        {
            int userID = WebSecurity.CurrentUserId;
            //IEnumerable<Customer> customers = repositoryCustomer.Customers.Where(x => x.UserID == userID);
            //IEnumerable<Address> addresses = repository.Addresses;
            IEnumerable<Address> addresses = from x in repository.Addresses
                                                 join y in repositoryCustomer.Customers on x.CustomerID equals y.CustomerID
                                                 where y.UserID == userID
                                                 select x;
            return View(addresses);
        }


        private SelectList GetCustomerIDs(Address address)
        {
            int userID = WebSecurity.CurrentUserId;
            return new SelectList(repositoryCustomer.Customers.Where(x => x.UserID == userID), "CustomerID", "CustomerID", address.CustomerID);
        }

        public ViewResult Edit(int addressId)
        {
            Address address = repository.Addresses
                .FirstOrDefault(p => p.AddressID == addressId);
            ViewBag.CustomerID = GetCustomerIDs(address);
            return View(address);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "AddressID,StreetLine1,StreetLine2,StreetLine3,City,PostalCode,County,Country,CreatedAt,UpdatedAt,CustomerID")] Address address)
        {
            if (ModelState.IsValid)
            {
                repository.SaveAddress(address);
                TempData["message"] = string.Format("{0} has been saved", address.AddressID);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(address);
            }
        }

        public ViewResult Create()
        {
            Address address = new Address();
            ViewBag.CustomerID = GetCustomerIDs(address);
            return View("Edit", address);
        }

        [HttpPost]
        public ActionResult Delete(int addressId)
        {
            Address deletedAddress = repository.DeleteAddress(addressId);
            if (deletedAddress != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                    deletedAddress.AddressID);
            }
            return RedirectToAction("Index");
        }

    }
}