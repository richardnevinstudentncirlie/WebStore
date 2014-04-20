using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Concrete;

namespace WebStore.WebUI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class scaProductController : Controller
    {
        private EFDbContext db = new EFDbContext();

        public FileContentResult GetImage(int productId)
        {
            Product prod = db.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }


        // GET: /scaProduct/
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductCategoryID).Include(p => p.ProductCustomerID);
            return View(products.ToList());
        }

        // GET: /scaProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /scaProduct/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryCode");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            return View();
        }

        // POST: /scaProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,Description,Price,Category,Quantity,Special,ImageData,ImageMimeType,Seller,Buyer,CreatedAt,UpdatedAt,CategoryID,CustomerID")] Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {

                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                product.Seller = 0;
                product.Buyer = 0;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryCode", product.CategoryID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", product.CustomerID);
            return View(product);
        }

        // GET: /scaProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryCode", product.CategoryID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", product.CustomerID);
            return View(product);
        }

        // POST: /scaProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,Description,Price,Category,Quantity,Special,ImageData,ImageMimeType,Seller,Buyer,CreatedAt,UpdatedAt,CategoryID,CustomerID")] Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {

                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                product.Seller = 0;
                product.Buyer = 0;

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryCode", product.CategoryID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", product.CustomerID);
            return View(product);
        }

        // GET: /scaProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /scaProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
