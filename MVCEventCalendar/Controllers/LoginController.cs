using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEventCalendar.Views.Login
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(MVCEventCalendar.User loginmodel )
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var adminlg = dc.Users.Select(x => x.UserID == 1).FirstOrDefault();
                var workerlg = dc.Users.Where(x => x.UserID == 2).FirstOrDefault();

                var userdetails = dc.Users.Where(x => x.UserName == loginmodel.UserName && x.Password == loginmodel.Password).FirstOrDefault();
                if (userdetails == null)
                {
                    loginmodel.LoginErrorMessage = "Wrong UserName and Password";
                    return View("Index",loginmodel);
                }
                Session["userID"] = userdetails.UserID;

                if (userdetails.UserName=="admin" && userdetails.Password=="admin2018")
                {
                               return RedirectToAction("Index", "Home");

                }
                else
               

                {
                    
                   Session["userID"] = userdetails.UserID;
                    return RedirectToAction("Index", "Worker");
                }
                
               
            }
        }
    }
}