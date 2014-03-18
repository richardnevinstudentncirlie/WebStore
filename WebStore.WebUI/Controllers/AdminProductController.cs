using System.Linq;
using System.Web.Mvc;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;

namespace WebStore.WebUI.Controllers 
{

    public class AdminProductController : Controller 
    {
        private IProductRepository repository;

        public AdminProductController(IProductRepository repo) 
        {
            repository = repo;
        }

        public ViewResult Index() 
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId) 
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product) 
        {
            if (ModelState.IsValid) 
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            } 
            else 
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Create() 
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId) 
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null) 
            {
                TempData["message"] = string.Format("{0} was deleted",
                    deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

    }
}
