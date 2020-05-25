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
    public class tagsController : Controller
    {
        private Model1 db = new Model1();

        // GET: tags
        public ActionResult Index()
        {
            return View(db.tag.ToList());
        }

        // GET: tags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tag tag = db.tag.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: tags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] tag tag)
        {
            if (ModelState.IsValid)
            {
                db.tag.Add(tag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        // GET: tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tag tag = db.tag.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        // GET: tags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tag tag = db.tag.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tag tag = db.tag.Find(id);
            db.tag.Remove(tag);
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
