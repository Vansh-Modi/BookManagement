using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            id = 19;
            if (id == null)
                return RedirectToAction("Login");
            LoginModel model = new LoginModel();
            if (model == null || model.User_ID == 0)
                return RedirectToAction("Login");
            return View(model);
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoginModel updateUser = new LoginModel();
                    bool result = updateUser.fnUpdateUser(model);
                    if (result)
                        ViewBag.Message = "User updated successfully.";
                }
                return RedirectToAction("Login");
            }
            catch
            {
                return View();
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
