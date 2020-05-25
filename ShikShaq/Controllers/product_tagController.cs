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
    public class product_tagController : Controller
    {
        private Model1 db = new Model1();

        // GET: product_tag
        public ActionResult Index()
        {
            return View(db.product_tag.ToList());
        }

        // GET: product_tag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_tag product_tag = db.product_tag.Find(id);
            if (product_tag == null)
            {
                return HttpNotFound();
            }
            return View(product_tag);
        }

        // GET: product_tag/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: product_tag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tag_id,product_id")] product_tag product_tag)
        {
            if (ModelState.IsValid)
            {
                db.product_tag.Add(product_tag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_tag);
        }

        // GET: product_tag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_tag product_tag = db.product_tag.Find(id);
            if (product_tag == null)
            {
                return HttpNotFound();
            }
            return View(product_tag);
        }

        // POST: product_tag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tag_id,product_id")] product_tag product_tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_tag);
        }

        // GET: product_tag/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_tag product_tag = db.product_tag.Find(id);
            if (product_tag == null)
            {
                return HttpNotFound();
            }
            return View(product_tag);
        }

        // POST: product_tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_tag product_tag = db.product_tag.Find(id);
            db.product_tag.Remove(product_tag);
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
