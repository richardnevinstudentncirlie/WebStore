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
    public class AdminOrderController : Controller 
    {
        private ICustomerRepository repositoryCustomer;
        private IOrderRepository repository;

        public AdminOrderController(IOrderRepository repo, ICustomerRepository repoCustomer) 
        {
            repository = repo;
            repositoryCustomer = repoCustomer;
        }

        public ViewResult Index() 
        {
            int userID = WebSecurity.CurrentUserId;
            IEnumerable<Order> orders = from x in repository.Orders
                                             join y in repositoryCustomer.Customers on x.CustomerID equals y.CustomerID
                                             where y.UserID == userID
                                             select x;

            return View(repository.Orders);
        }

        public RedirectToRouteResult RouteToOrderItem(int orderId)
        {
            return RedirectToAction("DispOrderItem", "AdminOrderItem", new { orderId });
        }

    }
}
