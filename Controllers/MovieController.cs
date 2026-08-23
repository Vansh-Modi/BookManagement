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
        public ActionResult Index(string searchCategory)
        {
            Movie movieDisplay = new Movie();
            List<Movie> movieList = movieDisplay.fnDisplayMovie(searchCategory);
            DataTable categoryTable = movieDisplay.fnGetCategoryDropdown();
            ViewBag.CategoryList = new SelectList(categoryTable.DefaultView, "Cat_ID", "Cat_Type", searchCategory);

            return View(movieList);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int Movie_ID, int Cat_ID)
        {
            if (Session["UserID"] != null)
            {
                int tempvar = Convert.ToInt32(Session["UserID"]);
                Session["UserID"] = tempvar;
            }
            Session["SelectedMovieID"] = Movie_ID;
            Session["SelectedCategoryID"] = Cat_ID;
            return RedirectToAction("Create", "Booking");
        }

        // GET: Movie/Create
        [HttpGet]
        public ActionResult AddCategory()
        {
            Movie movie = new Movie();
            return View(movie);
        }

        [HttpPost]
        public ActionResult AddCategory(Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool success = movie.fnAddCategory();
                    if (success)
                    {
                        return RedirectToAction("AddMovie", "Movie");
                    }
                }
                return View(movie);
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Create
        [HttpGet]
        public ActionResult AddMovie()
        {
            Movie movie = new Movie();
            DataTable categoryTable = movie.fnGetCategoryDropdown();
            ViewBag.CategoryList = new SelectList(categoryTable.DefaultView, "Cat_ID", "Cat_Type");
            return View(movie);
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult AddMovie(Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool success = movie.fnAddMovie();
                    if (success)
                    {
                        ModelState.Clear();
                        Movie newMovie = new Movie();
                        DataTable categoryTable = newMovie.fnGetCategoryDropdown();
                        ViewBag.CategoryList = new SelectList(categoryTable.DefaultView, "Cat_ID", "Cat_Type");
                        return View(newMovie);
                    }
                }
                // TODO: Add insert logic here

                ViewBag.CategoryList = new SelectList(movie.fnGetCategoryDropdown().DefaultView, "Cat_ID", "Cat_Type");
                return View(movie);
            }
            catch
            {
                DataTable categoryTable = movie.fnGetCategoryDropdown();
                ViewBag.CategoryList = new SelectList(categoryTable.DefaultView, "Cat_ID", "Cat_Type");
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
        public ActionResult Delete(int? id)
        {
            if(id == null)
                return RedirectToAction("Index");
            Movie movie = new Movie();
            bool isDeleted = movie.fnDeleteMovie(id.Value);
            if(isDeleted)
                TempData["Message"] = "Movie deleted successfully.";
            else
                TempData["Message"] = "Failed to delete the movie.";
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Movie movie = new Movie();
                    bool isDeleted = movie.fnDeleteMovie(id);
                    if (isDeleted)
                    {
                        ViewBag.Message = "Movie deleted successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Failed to delete the movie.";
                        return View();
                    }
                }
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
