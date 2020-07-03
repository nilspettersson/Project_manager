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

                        if (Request["filter"] == null)
                        {
                            ViewBag.projects = Dao.Account.getAllProjectsByUsername(user);
                        }
                        else if (Request["filter"] == "all projects")
                        {
                            ViewBag.projects = Dao.Account.getAllProjectsByUsername(user);
                        }
                        else if (Request["filter"] == "your projects")
                        {
                            ViewBag.projects = Dao.Account.getAllOwnedProjectsByUsername(user);
                        }

                        if (Request["search"] == null)
                        {
                            Debug.Print("asdasdasdasdasdsa");
                            //ViewBag.projects = Dao.Account.getAllProjectsByUsername(user);
                        }
                        else
                        {
                            //ViewBag.projects = Dao.Account.getAllProjectsByUsername(user);
                        }
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
                string role = Dao.Account.getRole(User.Identity.GetUserId(), project);
                ViewBag.role = role;

                //overview
                if (type == null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        //checks if the user owns this project.

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
        [HttpGet]
        public ActionResult ChangeFilter(string filter, string user)
        {
            
            if (filter == "all projects")
            {
                Debug.Print("asdasdasdasd yes");
                ViewBag.projects = new DataRow[0]; /*Dao.Account.getAllProjectsByUsername(user);*/
            }
            else
            {
                Debug.Print("ddddddddddddd no");
            }

            return Content("");
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

            if (role == "1" || role == "2" || role == "3")
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
            
            if (role == "1")
            {
                //user_id is the owner.
                string user_id = Dao.Account.getUserIdByName(user);
                string projectId = Dao.Account.getProjectId(user_id, project);

                //id of the user that will be added
                string userId = Dao.Account.getUserIdByName(name);
                if(userId == null)
                {
                    return Content("{ \"message\": \"Could not find User\", \"success\": false }");
                }
                else if(Dao.Account.UserIsInProject(userId, projectId))
                {
                    return Content("{ \"message\": \"User already exist\" , \"success\": false }");
                }
                else
                {
                    Dao.Account.addUserToProject(userId, user_role, projectId);
                }

                
            }
            return Content("{ \"message\": \"User added to project\" , \"success\": true }");
        }

        [Authorize]
        [HttpPost]
        public ActionResult RemoveUser(string user_id, string project)
        {
            string role = Dao.Account.getRole(User.Identity.GetUserId(), project);
            if (role == "1")
            {
                string projectId = Dao.Account.getProjectId(user_id, project);
                Dao.Account.removeUserFromProject(user_id, projectId);

                return Content("");
            }
            else
            {
                return Content("");
            }

        }


    }
}