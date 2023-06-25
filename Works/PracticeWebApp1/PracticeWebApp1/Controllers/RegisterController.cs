using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeWebApp1.Models;

namespace PracticeWebApp1.Controllers
{
    public class RegisterController : Controller
    {
        private EventsTestEntities1 db = new EventsTestEntities1();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RegisterModel user)
        {
            Object obj = null;
            obj = db.UsersDatas.SingleOrDefault(m => m.Login == user.Login);
            if (obj == null)
            {
                if (ModelState.IsValid)
                {
                    UsersData temp = new UsersData();
                    temp.Login = user.Login;
                    temp.Password = user.Password;
                    temp.Mod = false;
                    db.UsersDatas.Add(temp);
                    db.SaveChanges();
                    return Redirect("/Home/Index/");
                }
            }
            else
            {
                ViewBag.Error = "Пользователь уже существует";
            }

            return View();
            //return View();
        }
    }
}