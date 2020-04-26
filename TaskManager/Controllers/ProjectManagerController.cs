using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using Microsoft.AspNet.Identity;

namespace TaskManager.Controllers
{
    public class ProjectManagerController : Controller
    {
        // GET: ProjectManager
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                 ViewBag.projects = Dao.Projects.getAllProjects();
            }
            

            return View();
        }
        public ActionResult Users(string user, string project)
        {
            string message = "this is not your project";
            if (User.Identity.IsAuthenticated)
            {
                //checks if the user owns this project.
                string username = Dao.Account.getUsername(User.Identity.GetUserId());
                if(username == user)
                {
                    message = "this is your project";
                }
            }


            return Content("user: "+user+" project: "+project+"  "+message);
        }




        [HttpPost]
        public ActionResult CreateProject()
        {
            Dao.Account.createProject(User.Identity.GetUserId(), Request["name"], Request["description"]);

            return Content("");
        }

    }
}