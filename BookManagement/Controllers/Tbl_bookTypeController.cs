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
    public class Tbl_bookTypeController : Controller
    {
        private BookManagementEntities1 db = new BookManagementEntities1();

        // GET: Tbl_bookType
        public ActionResult Index()
        {
            return View(db.Tbl_bookType.ToList());
        }

        // GET: Tbl_bookType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_bookType tbl_bookType = db.Tbl_bookType.Find(id);
            if (tbl_bookType == null)
            {
                return HttpNotFound();
            }
            return View(tbl_bookType);
        }

        // GET: Tbl_bookType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tbl_bookType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "booktype_id,booktype_name")] Tbl_bookType tbl_bookType)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_bookType.Add(tbl_bookType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_bookType);
        }

        // GET: Tbl_bookType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_bookType tbl_bookType = db.Tbl_bookType.Find(id);
            if (tbl_bookType == null)
            {
                return HttpNotFound();
            }
            return View(tbl_bookType);
        }

        // POST: Tbl_bookType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "booktype_id,booktype_name")] Tbl_bookType tbl_bookType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_bookType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_bookType);
        }

        // GET: Tbl_bookType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_bookType tbl_bookType = db.Tbl_bookType.Find(id);
            if (tbl_bookType == null)
            {
                return HttpNotFound();
            }
            return View(tbl_bookType);
        }

        // POST: Tbl_bookType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_bookType tbl_bookType = db.Tbl_bookType.Find(id);
            db.Tbl_bookType.Remove(tbl_bookType);
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
