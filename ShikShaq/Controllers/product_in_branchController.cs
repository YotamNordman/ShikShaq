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
    public class product_in_branchController : Controller
    {
        private Model1 db = new Model1();

        // GET: product_in_branch
        public ActionResult Index()
        {
            var product_in_branch = db.product_in_branch.Include(p => p.branch).Include(p => p.product);
            return View(product_in_branch.ToList());
        }

        // GET: product_in_branch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_in_branch product_in_branch = db.product_in_branch.Find(id);
            if (product_in_branch == null)
            {
                return HttpNotFound();
            }
            return View(product_in_branch);
        }

        // GET: product_in_branch/Create
        public ActionResult Create()
        {
            ViewBag.branch_id = new SelectList(db.branches, "id", "name");
            ViewBag.product_id = new SelectList(db.products, "id", "name");
            return View();
        }

        // POST: product_in_branch/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,branch_id,quantity")] product_in_branch product_in_branch)
        {
            if (ModelState.IsValid)
            {
                db.product_in_branch.Add(product_in_branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.branch_id = new SelectList(db.branches, "id", "name", product_in_branch.branch_id);
            ViewBag.product_id = new SelectList(db.products, "id", "name", product_in_branch.product_id);
            return View(product_in_branch);
        }

        // GET: product_in_branch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_in_branch product_in_branch = db.product_in_branch.Find(id);
            if (product_in_branch == null)
            {
                return HttpNotFound();
            }
            ViewBag.branch_id = new SelectList(db.branches, "id", "name", product_in_branch.branch_id);
            ViewBag.product_id = new SelectList(db.products, "id", "name", product_in_branch.product_id);
            return View(product_in_branch);
        }

        // POST: product_in_branch/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,branch_id,quantity")] product_in_branch product_in_branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_in_branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.branch_id = new SelectList(db.branches, "id", "name", product_in_branch.branch_id);
            ViewBag.product_id = new SelectList(db.products, "id", "name", product_in_branch.product_id);
            return View(product_in_branch);
        }

        // GET: product_in_branch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_in_branch product_in_branch = db.product_in_branch.Find(id);
            if (product_in_branch == null)
            {
                return HttpNotFound();
            }
            return View(product_in_branch);
        }

        // POST: product_in_branch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_in_branch product_in_branch = db.product_in_branch.Find(id);
            db.product_in_branch.Remove(product_in_branch);
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
