using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookManagement.Models;

namespace BookManagement.Controllers
{
    public class Tbl_publisherController : Controller
    {
        private BookManagementEntities1 db = new BookManagementEntities1();

        // GET: Tbl_publisher
        public ActionResult Index()
        {
            return View(db.Tbl_publisher.ToList());
        }

        // GET: Tbl_publisher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_publisher tbl_publisher = db.Tbl_publisher.Find(id);
            if (tbl_publisher == null)
            {
                return HttpNotFound();
            }
            return View(tbl_publisher);
        }

        // GET: Tbl_publisher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tbl_publisher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pub_id,pub_name")] Tbl_publisher tbl_publisher)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_publisher.Add(tbl_publisher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_publisher);
        }

        // GET: Tbl_publisher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_publisher tbl_publisher = db.Tbl_publisher.Find(id);
            if (tbl_publisher == null)
            {
                return HttpNotFound();
            }
            return View(tbl_publisher);
        }

        // POST: Tbl_publisher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pub_id,pub_name")] Tbl_publisher tbl_publisher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_publisher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_publisher);
        }

        // GET: Tbl_publisher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_publisher tbl_publisher = db.Tbl_publisher.Find(id);
            if (tbl_publisher == null)
            {
                return HttpNotFound();
            }
            return View(tbl_publisher);
        }

        // POST: Tbl_publisher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_publisher tbl_publisher = db.Tbl_publisher.Find(id);
            db.Tbl_publisher.Remove(tbl_publisher);
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
