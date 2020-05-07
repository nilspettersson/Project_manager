using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

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
        public ActionResult Users(string user, string project, string type)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.username = Dao.Account.getUsername(User.Identity.GetUserId());
            }


            //shows all project for  user.
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

            //if a project is selected.
            else
            {
                if(type == null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        //checks if the user owns this project.
                        string username = Dao.Account.getUsername(User.Identity.GetUserId());
                        if (username == user)
                        {
                            
                        }
                    }
                    ViewBag.type = "overview";
                }
                else if(type == "sprint")
                {

                    DataRow[] rows = Dao.Account.getCurrentSprint(project);

                    if (rows.Length == 0)
                    {
                        ViewBag.sprintExists = false;
                    }
                    else
                    {
                        ViewBag.sprintExists = true;
                    }

                    ViewBag.type = "sprint";
                }
                

                ViewBag.isProject = true;
                ViewBag.projectName = project;
                ViewBag.userName = user;
                return View();
            }
            
        }




        [Authorize]
        [HttpPost]
        public ActionResult CreateProject()
        {
            Dao.Account.createProject(User.Identity.GetUserId(), Request["name"], Request["description"]);

            return Content("");
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateSprint(string name, string start_time, string weeks, string user, string project)
        {
            DateTime time = DateTime.Parse(start_time);
            time = time.AddDays(Double.Parse(weeks) * 7);
            string end_time = time.ToString();

            System.Diagnostics.Debug.WriteLine("******************   "+user+"  "+project+"  "+name );

            //Dao.Account.createSprint(User.Identity.GetUserId());
            return Content("");
        }

    }
}