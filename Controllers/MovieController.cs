using movieBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace movieBooking.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        [HttpGet]
        public ActionResult AddMovieCategory()
        {
            Movie movie = new Movie();
            DataTable categoryTable = movie.fnGetCategoryDropdown();
            ViewBag.CatgeogryList = new SelectList(categoryTable.DefaultView, "Cat_ID", "Cat_Type");
            return View(movie);
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult AddMovieCategory(Movie movie)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    bool success = movie.fnAddMovie();
                    if(success) 
                        return RedirectToAction("Index");
                }
                // TODO: Add insert logic here
                ViewBag.CatgeogryList = new SelectList(movie.fnGetCategoryDropdown().DefaultView, "Cat_ID", "Cat_Type");
                return View(movie);
            }
            catch
            {
                return View(movie);
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
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

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
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
