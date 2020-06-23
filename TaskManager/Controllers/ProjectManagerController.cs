using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Web.Script.Serialization;
using System.Diagnostics;

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

            //ViewBag.projects = Dao.Projects.getAllProjects();
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
                //overview
                if(type == null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        //checks if the user owns this project.
                        string username = Dao.Account.getUsername(User.Identity.GetUserId());
                        if (username == user)
                        {
                            
                        }

                        DataRow[] sprints = Dao.Account.getAllSprints(project);
                        ViewBag.sprints = sprints;

                    }
                    ViewBag.type = "overview";
                }
                //sprint. shows the current sprint.
                else if(type == "sprint")
                {
                    //finding the last created sprint.
                    DataRow[] rows = Dao.Account.getCurrentSprint(project);
                    //checks if the project has any sprints.
                    if (rows.Length == 0)
                    {
                        //html will show button to create a sprint.
                        ViewBag.sprintExists = false;
                    }
                    else
                    {
                        ViewBag.sprintExists = true;
                        ViewBag.sprint = rows[0];

                        string sprintId = rows[0][0].ToString();

                        DataRow[] tasks = Dao.Account.getTasks(sprintId);
                        ViewBag.tasks = tasks;

                    }

                    ViewBag.type = "sprint";
                }
                //users. shows users for the project.
                else if (type == "users")
                {
                    String user_id = Dao.Account.getUserIdByName(user);
                    String project_id = Dao.Account.getProjectId(user_id, project);
                    DataRow[] users = Dao.Account.getUsers(project_id);
                    ViewBag.users = users;

                    ViewBag.type = "users";
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


            string role = Dao.Account.getRole(User.Identity.GetUserId(), project);
            if (role == "1")
            {
                string userId = Dao.Account.getUserIdByName(user);
                string projectId = Dao.Account.getProjectId(userId, project);
                Dao.Account.createSprint(projectId, name, start_time, end_time);

                return Content("");
            }
            else
            {
                return Content("");
            }
            
        }

        [Authorize]
        [HttpPost]
        public ActionResult RemoveSprint(string sprint, string project)
        {
            string role = Dao.Account.getRole(User.Identity.GetUserId(), project);
            if (role == "1")
            {
                Dao.Account.removeSprint(sprint);

                return Content("");
            }
            else
            {
                return Content("");
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateTask(string name, string description, string type, string difficulty, string user, string project, string sprint)
        {
            string role = Dao.Account.getRole(User.Identity.GetUserId(), project);

            if(role == "1" || role == "2")
            {
                Dao.Account.createTask(sprint, name, description, difficulty, type);
            }
            return Content("");
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateTaskState(string task, string state, string project)
        {
            string role = Dao.Account.getRole(User.Identity.GetUserId(), project);

            if (role == "1" || role == "2")
            {
                Dao.Account.setTaskState(task, state);
            }
            return Content("");
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddUser(string name, string user_role, string user, string project)
        {
            string role = Dao.Account.getRole(User.Identity.GetUserId(), project);
            Debug.WriteLine(name+"  "+user_role);
            if (role == "1" || role == "2")
            {
                //Dao.Account.createTask(sprint, name, description, difficulty, type);
            }
            return Content("");
        }


    }
}