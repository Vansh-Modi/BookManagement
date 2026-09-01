using movieBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace movieBooking.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            Movie booking = new Movie();
            List<Movie> bookings = booking.fnDisplayBooking();
            return View(bookings);
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        // POST: Booking/Create
        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                if (!ModelState.IsValid)
                {
                    return View(); // Returns the form with validation errors
                }

                Movie addMovie = new Movie();
                addMovie.cd.User_ID = Convert.ToInt32(Session["UserID"]);

                if (Session["SelectedMovieID"] != null || Session["SelectedCategoryID"] != null)
                {
                    addMovie.cd.Movie_ID = Convert.ToInt32(Session["SelectedMovieID"]);
                    addMovie.cd.Cat_ID = Convert.ToInt32(Session["SelectedCategoryID"]);
                }

                bool res = addMovie.fnAddBooking();
                if (res)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Error while adding booking.");
                return View();
            }
            catch
            {
                ModelState.AddModelError("", "An unexpected error occurred.");
                return View();
            }
        }
        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        // POST: Booking/Edit/5
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

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        // POST: Booking/Delete/5
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
