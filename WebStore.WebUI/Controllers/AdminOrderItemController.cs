using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;

namespace WebStore.WebUI.Controllers 
{

    [Authorize(Roles = "Customer")]
    public class AdminOrderItemController : Controller 
    {
        private IOrderItemRepository repository;

        public AdminOrderItemController(IOrderItemRepository repo) 
        {
            repository = repo;
        }

        public ViewResult DispOrderItem(int orderId)
        {
            return View(repository.OrderItems.Where(x => x.OrderID == orderId));
        }

        public RedirectToRouteResult RouteToOrder()
        {
            return RedirectToAction("Index", "AdminOrder");
        }

    }
}
