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
    public class Tbl_authorController : Controller
    {
        private BookManagementEntities1 db = new BookManagementEntities1();

        // GET: Tbl_author
        public ActionResult Index()
        {
            var tbl_author = db.Tbl_author.Include(t => t.Tbl_bookType);
            return View(tbl_author.ToList());
        }

        // GET: Tbl_author/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_author tbl_author = db.Tbl_author.Find(id);
            if (tbl_author == null)
            {
                return HttpNotFound();
            }
            return View(tbl_author);
        }

        // GET: Tbl_author/Create
        public ActionResult Create()
        {
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name");
            return View();
        }

        // POST: Tbl_author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "author_id,author_name,author_email,author_pass,booktype_id,phone")] Tbl_author tbl_author)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_author.Add(tbl_author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", tbl_author.booktype_id);
            return View(tbl_author);
        }

        // GET: Tbl_author/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_author tbl_author = db.Tbl_author.Find(id);
            if (tbl_author == null)
            {
                return HttpNotFound();
            }
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", tbl_author.booktype_id);
            return View(tbl_author);
        }

        // POST: Tbl_author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "author_id,author_name,author_email,author_pass,booktype_id,phone")] Tbl_author tbl_author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", tbl_author.booktype_id);
            return View(tbl_author);
        }

        // GET: Tbl_author/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_author tbl_author = db.Tbl_author.Find(id);
            if (tbl_author == null)
            {
                return HttpNotFound();
            }
            return View(tbl_author);
        }

        // POST: Tbl_author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_author tbl_author = db.Tbl_author.Find(id);
            db.Tbl_author.Remove(tbl_author);
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
