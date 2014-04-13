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
    public class scaOrderItemController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: /scaOrderItem/
        public ActionResult Index()
        {
            var orderitems = db.OrderItems.Include(o => o.OrderItemCategoryID).Include(o => o.OrderItemOrderID).Include(o => o.OrderItemProductID);
            return View(orderitems.ToList());
        }

        // GET: /scaOrderItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderitem = db.OrderItems.Find(id);
            if (orderitem == null)
            {
                return HttpNotFound();
            }
            return View(orderitem);
        }

        // GET: /scaOrderItem/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryCode");
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "FirstName");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        // POST: /scaOrderItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="OrderItemID,Name,Description,Price,Category,Special,ImageData,ImageMimeType,Seller,Buyer,Quantity,CreatedAt,UpdatedAt,OrderID,ProductID,CategoryID")] OrderItem orderitem)
        {
            if (ModelState.IsValid)
            {
                db.OrderItems.Add(orderitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryCode", orderitem.CategoryID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "FirstName", orderitem.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderitem.ProductID);
            return View(orderitem);
        }

        // GET: /scaOrderItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderitem = db.OrderItems.Find(id);
            if (orderitem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryCode", orderitem.CategoryID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "FirstName", orderitem.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderitem.ProductID);
            return View(orderitem);
        }

        // POST: /scaOrderItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="OrderItemID,Name,Description,Price,Category,Special,ImageData,ImageMimeType,Seller,Buyer,Quantity,CreatedAt,UpdatedAt,OrderID,ProductID,CategoryID")] OrderItem orderitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryCode", orderitem.CategoryID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "FirstName", orderitem.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderitem.ProductID);
            return View(orderitem);
        }

        // GET: /scaOrderItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderitem = db.OrderItems.Find(id);
            if (orderitem == null)
            {
                return HttpNotFound();
            }
            return View(orderitem);
        }

        // POST: /scaOrderItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderItem orderitem = db.OrderItems.Find(id);
            db.OrderItems.Remove(orderitem);
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
