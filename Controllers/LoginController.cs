using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using movieBooking.Models;

namespace movieBooking.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int UserID = loginModel.fnAddUsers(loginModel);
                    System.Diagnostics.Debug.WriteLine("Error : " + UserID);
                    if (UserID != 0)
                    {
                        Session["UserID"] = UserID;
                    }
                    if (Session["SelectedMoiveID"] != null || Session["SelectedCategoryID"] != null)
                    {
                        int Movie_ID = Convert.ToInt32(Session["SelectedMovieID"]);
                        int Cat_ID = Convert.ToInt32(Session["SelectedMovieID"]);

                        Session["SelectedMovieID"] = Movie_ID;
                        Session["SelectedCategoryID"] = Cat_ID;

                        return RedirectToAction("Create", "Booking");
                    }
                    return RedirectToAction("Index", "Booking");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password.");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login/Login
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoginModel userLogin = new LoginModel();
                    int UserID = userLogin.fnLogin(loginModel);

                    if (UserID != 0)
                    {
                        Session["UserID"] = UserID;

                        if (Session["SelectedMovieID"] != null || Session["SelectedCategoryID"] != null)
                        {
                            int Movie_ID = Convert.ToInt32(Session["SelectedMovieID"]);
                            int Cat_ID = Convert.ToInt32(Session["SelectedMovieID"]);

                            Session["SelectedMovieID"] = Movie_ID;
                            Session["SelectedCategoryID"] = Cat_ID;

                            return RedirectToAction("Create", "Booking");
                        }
                        return RedirectToAction("Index", "Booking");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username or Password.");
                    }
                }
                //return RedirectToAction("Index", "Movie");
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            int user_ID = Convert.ToInt32(Session["UserID"]);
            LoginModel loginModel = new LoginModel();

            loginModel = loginModel.fnGetUserByID(user_ID);

            if (loginModel == null)
                return HttpNotFound();
            return View(loginModel);
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(LoginModel model)
        {
            try
            {
                if (Session["UserID"] == null)
                    return RedirectToAction("Login");

                if (ModelState.IsValid)
                {
                    // Securely force the model's User_ID to match the active session user
                    model.User_ID = Convert.ToInt32(Session["UserID"]);

                    LoginModel updateUser = new LoginModel();
                    bool result = updateUser.fnUpdateUser(model);
                    if (result)
                    {
                        ViewBag.Message = "User updated successfully.";
                        return RedirectToAction("Index", "Booking"); // Redirect back to booking dashboard instead of forcing re-login
                    }
                }
                ModelState.AddModelError("", "Invalid data. Please check the input fields.");
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while updating the user. Please try again.");
                return View(model);
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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
