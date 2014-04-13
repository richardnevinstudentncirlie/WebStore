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
    public class scaAddressController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: /scaAddress/
        public ActionResult Index()
        {
            var addresses = db.Addresses.Include(a => a.AddressCustomerID);
            return View(addresses.ToList());
        }

        // GET: /scaAddress/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: /scaAddress/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            return View();
        }

        // POST: /scaAddress/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AddressID,StreetLine1,StreetLine2,StreetLine3,City,PostalCode,County,Country,CreatedAt,UpdatedAt,CustomerID")] Address address)
        {
            if (ModelState.IsValid)
            {
                address.CreatedAt = DateTime.Now;
                address.UpdatedAt = DateTime.Now;

                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", address.CustomerID);
            return View(address);
        }

        // GET: /scaAddress/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", address.CustomerID);
            return View(address);
        }

        // POST: /scaAddress/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AddressID,StreetLine1,StreetLine2,StreetLine3,City,PostalCode,County,Country,CreatedAt,UpdatedAt,CustomerID")] Address address)
        {
            if (ModelState.IsValid)
            {
                address.CreatedAt = DateTime.Now;
                address.UpdatedAt = DateTime.Now;

                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", address.CustomerID);
            return View(address);
        }

        // GET: /scaAddress/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: /scaAddress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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
