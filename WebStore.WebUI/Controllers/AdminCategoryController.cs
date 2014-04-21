using System.Linq;
using System.Web.Mvc;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;

namespace WebStore.WebUI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminCategoryController : Controller
    {
        private ICategoryRepository repository;

        public AdminCategoryController(ICategoryRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Categories);
        }

        public ViewResult Edit(int categoryId)
        {
            Category category = repository.Categories
                .FirstOrDefault(p => p.CategoryID == categoryId);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCategory(category);
                TempData["message"] = string.Format("{0} has been saved", category.CategoryCode);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(category);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Category());
        }

        [HttpPost]
        public ActionResult Delete(int categoryId)
        {
            Category deletedCategory = repository.DeleteCategory(categoryId);
            if (deletedCategory != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                    deletedCategory.CategoryCode);
            }
            return RedirectToAction("Index");
        }

    }
}
