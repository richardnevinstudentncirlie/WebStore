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
    public class scaOrderController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: /scaOrder/
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.OrderBillToAddressID).Include(o => o.OrderCustomerID).Include(o => o.OrderShipToAddressID);
            return View(orders.ToList());
        }

        // GET: /scaOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: /scaOrder/Create
        public ActionResult Create()
        {
            ViewBag.BillToAddressID = new SelectList(db.Addresses, "AddressID", "StreetLine1");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            ViewBag.ShipToAddressID = new SelectList(db.Addresses, "AddressID", "StreetLine1");
            return View();
        }

        // POST: /scaOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="OrderID,OrderDate,ShipDate,TotalOrder,ProductCount,FirstName,LastName,Company,Email,StreetLine1,StreetLine2,StreetLine3,City,PostalCode,County,Country,PaymentConfirmation,CreatedAt,UpdatedAt,CustomerID,ShipToAddressID,BillToAddressID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BillToAddressID = new SelectList(db.Addresses, "AddressID", "StreetLine1", order.BillToAddressID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            ViewBag.ShipToAddressID = new SelectList(db.Addresses, "AddressID", "StreetLine1", order.ShipToAddressID);
            return View(order);
        }

        // GET: /scaOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillToAddressID = new SelectList(db.Addresses, "AddressID", "StreetLine1", order.BillToAddressID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            ViewBag.ShipToAddressID = new SelectList(db.Addresses, "AddressID", "StreetLine1", order.ShipToAddressID);
            return View(order);
        }

        // POST: /scaOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="OrderID,OrderDate,ShipDate,TotalOrder,ProductCount,FirstName,LastName,Company,Email,StreetLine1,StreetLine2,StreetLine3,City,PostalCode,County,Country,PaymentConfirmation,CreatedAt,UpdatedAt,CustomerID,ShipToAddressID,BillToAddressID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillToAddressID = new SelectList(db.Addresses, "AddressID", "StreetLine1", order.BillToAddressID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            ViewBag.ShipToAddressID = new SelectList(db.Addresses, "AddressID", "StreetLine1", order.ShipToAddressID);
            return View(order);
        }

        // GET: /scaOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /scaOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
