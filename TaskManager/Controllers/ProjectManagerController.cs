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
                 ViewBag.projects = AccountManager.getAllProjects(User.Identity.GetUserId());
            }
            

            return View();
        }

        [HttpPost]
        public ActionResult CreateProject()
        {
            System.Diagnostics.Debug.WriteLine("asddasasdasdasdasdasdsadsadssdasdasdasdsadasd");
            AccountManager.createProject(User.Identity.GetUserId(), Request["name"], Request["description"]);

            return Content("");
        }

    }
}