using movieBooking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            int User_ID = Convert.ToInt32(Session["UserID"]);
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            Movie booking = new Movie();
            List<Movie> bookings = booking.fnDisplayBooking(User_ID);
            return View(bookings);
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public void fnInitDropdown()
        {
            Movie movie = new Movie();
            DataTable movieTable = movie.fnGetMovieDropdown();
            ViewBag.MovieList = new SelectList(movieTable.DefaultView, "Movie_ID", "Movie_name");
            ViewBag.MovieDataTable = movieTable;
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            Movie model = new Movie();

            // Handle pre-selected movie/category from session if coming directly from a movie catalog page
            if (Session["SelectedMovieID"] != null)
            {
                model.cd.Movie_ID = Convert.ToInt32(Session["SelectedMovieID"]);
            }
            if (Session["SelectedCategoryID"] != null)
            {
                model.cd.Cat_ID = Convert.ToInt32(Session["SelectedCategoryID"]);
            }

            fnInitDropdown();
            return View(model);
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (Session["UserID"] == null)
                    return RedirectToAction("Login", "Login");

                if (ModelState.IsValid)
                {
                    Movie addMovie = new Movie();
                    addMovie.cd.User_ID = Convert.ToInt32(Session["UserID"]);

                    // Prioritize form submission, fallback to session values if empty
                    if (!string.IsNullOrEmpty(collection["cd.Movie_ID"]))
                    {
                        addMovie.cd.Movie_ID = Convert.ToInt32(collection["cd.Movie_ID"]);
                        addMovie.cd.Cat_ID = Convert.ToInt32(collection["cd.Cat_ID"]);
                    }
                    else if (Session["SelectedMovieID"] != null)
                    {
                        addMovie.cd.Movie_ID = Convert.ToInt32(Session["SelectedMovieID"]);
                        addMovie.cd.Cat_ID = Convert.ToInt32(Session["SelectedCategoryID"]);
                    }

                    if (!string.IsNullOrEmpty(collection["cd.no_of_Tickets"]))
                    {
                        addMovie.cd.no_of_Tickets = Convert.ToInt32(collection["cd.no_of_Tickets"]);
                    }

                    bool res = addMovie.fnAddBooking();
                    if (res)
                    {
                        // Clear session selection tokens after a successful booking
                        Session["SelectedMovieID"] = null;
                        Session["SelectedCategoryID"] = null;
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError("", "Error while adding booking.");
                fnInitDropdown();
                return View();
            }
            catch
            {
                ModelState.AddModelError("", "An unexpected error occurred.");
                fnInitDropdown();
                return View();
            }
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (id == null)
            {
                return RedirectToAction("Index");
            }
            Movie booking = new Movie();
            int userID = Convert.ToInt32(Session["UserID"]);
            int bookingId = Convert.ToInt32(id);
            booking.fnGetBookingID(bookingId);
            if (booking == null)
                return HttpNotFound();
            Session["booking_id"] = bookingId;
            fnInitDropdown();
            return View(booking);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (Session["UserID"] == null)
                    return RedirectToAction("Login", "Login");
                if (ModelState.IsValid)
                {
                    Movie booking = new Movie();
                    booking.cd.User_ID = Convert.ToInt32(Session["UserID"]);

                    if (!string.IsNullOrEmpty(collection["cd.Movie_ID"]))
                    {
                        booking.cd.Movie_ID = Convert.ToInt32(collection["cd.Movie_ID"]);
                        booking.cd.Cat_ID = Convert.ToInt32(collection["cd.Cat_ID"]);
                    }
                    if (!string.IsNullOrEmpty(collection["cd.no_of_Tickets"]))
                    {
                        booking.cd.no_of_Tickets = Convert.ToInt32(collection["cd.no_of_Tickets"]);
                    }
                    int bookingId = Convert.ToInt32(Session["booking_id"]);
                    bool res = booking.fnUpdateBooking(bookingId);
                    if (res)
                    {
                        Session["booking_id"] = null;
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("", "An Error Booking Update");
                fnInitDropdown();
                return View();
            }
            catch
            {
                ModelState.AddModelError("", "An Error Occured");
                fnInitDropdown();
                return View();
            }
        }

        // GET: Booking/Delete/5
        // GET: Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (id != null)
            {
                Movie movie = new Movie();
                movie.fnDeleteBooking(id.Value);
            }
            return RedirectToAction("Index");
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Movie movie = new Movie();
                    bool isDeleted = movie.fnDeleteBooking(id);
                    if (isDeleted)
                    {
                        ViewBag.Message = "Booking deleted successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Error deleting booking.";
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}