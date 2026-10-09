using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookManagement.Controllers
{
    public class AuthorLoginController : Controller
    {
        // GET: AuthorLogin
        public ActionResult Index()
        {
            return View();
        }

        // GET: AuthorLogin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthorLogin/Create
        public ActionResult Login()
        {
            return View();
        }

        // POST: AuthorLogin/Create
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                if(ModelState.IsValid)
                {

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorLogin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthorLogin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorLogin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthorLogin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
