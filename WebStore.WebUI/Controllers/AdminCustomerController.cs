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
    public class AdminCustomerController : Controller
    {
        private ICustomerRepository repository;

        public AdminCustomerController(ICustomerRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            int userID = WebSecurity.CurrentUserId;
            IEnumerable<Customer> customers = repository.Customers.Where(x => x.UserID == userID);

            ViewBag.CustomerExists = false;
            if (customers.Count() > 0)   
                ViewBag.CustomerExists = true;

            return View(customers);
        }

        public ViewResult Edit(int customerId)
        {
            Customer customer = repository.Customers
                .FirstOrDefault(p => p.CustomerID == customerId);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CustomerID,UserID,FirstName,LastName,Company,Email,Phone,CreatedAt,UpdatedAt")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.UserID = WebSecurity.CurrentUserId;
                repository.SaveCustomer(customer);
                TempData["message"] = string.Format("{0} has been saved", customer.FirstName);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(customer);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Customer());
        }

        [HttpPost]
        public ActionResult Delete(int customerId)
        {
            Customer deletedCustomer = repository.DeleteCustomer(customerId);
            if (deletedCustomer != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                    deletedCustomer.FirstName);
            }
            return RedirectToAction("Index");
        }

    }
}