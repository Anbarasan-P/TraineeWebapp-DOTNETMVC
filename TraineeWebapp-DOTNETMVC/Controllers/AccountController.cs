using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TraineeWebapp_DOTNETMVC.Models;

namespace TraineeWebapp_DOTNETMVC.Controllers
{
    public class AccountController : Controller
    {
        TraineeDAL dataAccessLayer = new TraineeDAL();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost] // This attribute indicates that this action method will handle POST requests
        public ActionResult Register(Trainee model, HttpPostedFileBase PhotoFile)
        {
            if (ModelState.IsValid)
            {
                if (PhotoFile != null && PhotoFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(PhotoFile.InputStream))
                    {
                        model.Photo = binaryReader.ReadBytes(PhotoFile.ContentLength);
                    }
                }

                if (string.IsNullOrWhiteSpace(model.Password))
                {
                    ModelState.AddModelError("Password", "Password is required.");
                    return View(model);
                }

                dataAccessLayer.InsertTrainee(model);

                return RedirectToAction("Login");
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string role = model.Role;
                string userName = string.Empty;
                bool isValid = false;

                if (role == "Admin")
                {
                    var admin = dataAccessLayer.ValidateAdmin(model.Email, model.Password);
                    if (admin != null)
                    {
                        isValid = true;
                        userName = admin.Name;
                    }
                }
                else if (role == "Trainee")
                {
                    var trainee = dataAccessLayer.ValidateTrainee(model.Email, model.Password);
                    if (trainee != null)
                    {
                        isValid = true;
                        userName = trainee.Name;
                        Session["TraineeID"] = trainee.TraineeID;
                    }
                }

                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(userName, false);

                    var ticket = new FormsAuthenticationTicket(
                        1,
                        userName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        role
                    );

                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    Response.Cookies.Add(authCookie);

                    return RedirectToAction("Index", "Trainee");
                }

                ModelState.AddModelError(string.Empty, "Invalid email or password.");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}