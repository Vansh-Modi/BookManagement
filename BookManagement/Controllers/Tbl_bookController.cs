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
    public class Tbl_bookController : Controller
    {
        private BookManagementEntities1 db = new BookManagementEntities1();

        // GET: Tbl_book
        public ActionResult Index()
        {
            var tbl_book = db.Tbl_book.Include(t => t.Tbl_author).Include(t => t.Tbl_bookType).Include(t => t.Tbl_publisher);
            return View(tbl_book.ToList());
        }

        // GET: Tbl_book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_book tbl_book = db.Tbl_book.Find(id);
            if (tbl_book == null)
            {
                return HttpNotFound();
            }
            return View(tbl_book);
        }

        // GET: Tbl_book/Create
        public ActionResult Create()
        {
            ViewBag.author_id = new SelectList(db.Tbl_author, "author_id", "author_name");
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name");
            ViewBag.pub_id = new SelectList(db.Tbl_publisher, "pub_id", "pub_name");
            return View();
        }

        // POST: Tbl_book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "book_id,book_name,booktype_id,author_id,pub_id,book_pub_date,ISBN_No,Price")] Tbl_book tbl_book)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_book.Add(tbl_book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.author_id = new SelectList(db.Tbl_author, "author_id", "author_name", tbl_book.author_id);
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", tbl_book.booktype_id);
            ViewBag.pub_id = new SelectList(db.Tbl_publisher, "pub_id", "pub_name", tbl_book.pub_id);
            return View(tbl_book);
        }

        // GET: Tbl_book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_book tbl_book = db.Tbl_book.Find(id);
            if (tbl_book == null)
            {
                return HttpNotFound();
            }
            ViewBag.author_id = new SelectList(db.Tbl_author, "author_id", "author_name", tbl_book.author_id);
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", tbl_book.booktype_id);
            ViewBag.pub_id = new SelectList(db.Tbl_publisher, "pub_id", "pub_name", tbl_book.pub_id);
            return View(tbl_book);
        }

        // POST: Tbl_book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "book_id,book_name,booktype_id,author_id,pub_id,book_pub_date,ISBN_No,Price")] Tbl_book tbl_book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.author_id = new SelectList(db.Tbl_author, "author_id", "author_name", tbl_book.author_id);
            ViewBag.booktype_id = new SelectList(db.Tbl_bookType, "booktype_id", "booktype_name", tbl_book.booktype_id);
            ViewBag.pub_id = new SelectList(db.Tbl_publisher, "pub_id", "pub_name", tbl_book.pub_id);
            return View(tbl_book);
        }

        // GET: Tbl_book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_book tbl_book = db.Tbl_book.Find(id);
            if (tbl_book == null)
            {
                return HttpNotFound();
            }
            return View(tbl_book);
        }

        // POST: Tbl_book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_book tbl_book = db.Tbl_book.Find(id);
            db.Tbl_book.Remove(tbl_book);
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
