using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            string username = "not logged in";
            if (User.Identity.IsAuthenticated)
            {
                username = Dao.Account.getNameById(User.Identity.GetUserId());
                
            }
            ViewData["username"] = username;

            //System.Diagnostics.Debug.WriteLine("hello there!!!! " + User.Identity.GetUserName());
            /*var result = DatabaseManager.Execute("select * from account");

            for (int row = 0; row < result.Count; row++) 
            { 

                System.Diagnostics.Debug.WriteLine(result[row]["id"]);
                System.Diagnostics.Debug.WriteLine(result[row]["auth_id"]);
                System.Diagnostics.Debug.WriteLine(result[row]["username"]);
                System.Diagnostics.Debug.WriteLine("");
            }*/



            return View();
        }

        public ActionResult About()
        {

            

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}