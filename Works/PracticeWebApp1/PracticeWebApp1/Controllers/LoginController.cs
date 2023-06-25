using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PracticeWebApp1.Models;
using System.Globalization;

namespace PracticeWebApp1.Controllers
{
    public class LoginController : Controller
    {
        private EventsTestEntities1 db = new EventsTestEntities1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UsersData user)
        {
            Object obj = null;
                obj = db.UsersDatas.SingleOrDefault(m => m.Login == user.Login);
            if(obj != null)
            {
                UsersData temp = db.UsersDatas.SingleOrDefault(m => m.Login == user.Login);
                if (temp.Password != user.Password)
                    obj = null;
            }

            if (obj != null)
            {
                System.Web.HttpContext.Current.Session["CurrentUser"] = obj;

                FormsAuthentication.SetAuthCookie(user.Password, true);

                return Redirect("/Events");
            }

            ViewBag.Error = "Введены неверные данные";
            return View();
        }

        [Authorize]
        public ActionResult Exit()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect("/Login");
        }

    }
}