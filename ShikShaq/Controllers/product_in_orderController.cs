using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShikShaq;

namespace ShikShaq.Controllers
{
    public class product_in_orderController : Controller
    {
        private Model1 db = new Model1();

        // GET: product_in_order
        public ActionResult Index()
        {
            var product_in_order = db.product_in_order.Include(p => p.order).Include(p => p.product);
            return View(product_in_order.ToList());
        }

        // GET: product_in_order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_in_order product_in_order = db.product_in_order.Find(id);
            if (product_in_order == null)
            {
                return HttpNotFound();
            }
            return View(product_in_order);
        }

        // GET: product_in_order/Create
        public ActionResult Create()
        {
            ViewBag.order_id = new SelectList(db.order, "id", "id");
            ViewBag.product_id = new SelectList(db.product, "id", "name");
            return View();
        }

        // POST: product_in_order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,order_id,quantity")] product_in_order product_in_order)
        {
            if (ModelState.IsValid)
            {
                db.product_in_order.Add(product_in_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.order_id = new SelectList(db.order, "id", "id", product_in_order.order_id);
            ViewBag.product_id = new SelectList(db.product, "id", "name", product_in_order.product_id);
            return View(product_in_order);
        }

        // GET: product_in_order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_in_order product_in_order = db.product_in_order.Find(id);
            if (product_in_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.order_id = new SelectList(db.order, "id", "id", product_in_order.order_id);
            ViewBag.product_id = new SelectList(db.product, "id", "name", product_in_order.product_id);
            return View(product_in_order);
        }

        // POST: product_in_order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,order_id,quantity")] product_in_order product_in_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_in_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.order_id = new SelectList(db.order, "id", "id", product_in_order.order_id);
            ViewBag.product_id = new SelectList(db.product, "id", "name", product_in_order.product_id);
            return View(product_in_order);
        }

        // GET: product_in_order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_in_order product_in_order = db.product_in_order.Find(id);
            if (product_in_order == null)
            {
                return HttpNotFound();
            }
            return View(product_in_order);
        }

        // POST: product_in_order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_in_order product_in_order = db.product_in_order.Find(id);
            db.product_in_order.Remove(product_in_order);
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
