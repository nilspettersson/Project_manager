using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using Microsoft.AspNet.Identity;
using System.Data;

namespace TaskManager.Controllers
{
    public class ProjectManagerController : Controller
    {
        // GET: ProjectManager
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.username = Dao.Account.getUsername(User.Identity.GetUserId());
            }

            ViewBag.projects = Dao.Projects.getAllProjects();
            return View();
        }
        public ActionResult Users(string user, string project)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.username = Dao.Account.getUsername(User.Identity.GetUserId());
            }


            if (project == null && user != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    string username = Dao.Account.getUsername(User.Identity.GetUserId());
                    //if you own the user page you can see the projects.
                    if (username == user)
                    {
                        ViewBag.projects = Dao.Account.getAllProjectsByUsername(user);
                    }
                    else
                    {
                        //if you are not allowed to see page.
                        ViewBag.projects = new DataRow[0];
                    }
                }

                ViewBag.isProject = false;
                return View();
            }
            else
            {
                string message = "this is not your project";
                if (User.Identity.IsAuthenticated)
                {
                    //checks if the user owns this project.
                    string username = Dao.Account.getUsername(User.Identity.GetUserId());
                    if (username == user)
                    {
                        message = "this is your project";
                    }
                }

                ViewBag.isProject = true;
                ViewBag.projectName = project;
                return View();
            }
            
        }





        [HttpPost]
        public ActionResult CreateProject()
        {
            Dao.Account.createProject(User.Identity.GetUserId(), Request["name"], Request["description"]);

            return Content("");
        }

    }
}