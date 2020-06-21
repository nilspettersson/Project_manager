using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc.Ajax;
using Microsoft.Ajax.Utilities;

namespace TaskManager.Models
{
    public static class Dao
    {

        private static DataRow[] Execute(string sql)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\task_manager_db.mdf;Integrated Security=True";
            DataTable dt = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                System.Diagnostics.Debug.WriteLine("sql: " + sql);
                SqlCommand command = con.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                con.Open();

                SqlDataAdapter adp = new SqlDataAdapter(command);
                dt = new DataTable();
                adp.Fill(dt);
            }

            DataRow[] rows = new DataRow[dt.Rows.Count];
            for(int i = 0; i<dt.Rows.Count; i++)
            {
                rows[i] = dt.Rows[i];
            }

            return rows;


        }



        public static class Account
        {
            public static void register(string userId, string username)
            {
                Dao.Execute("insert into account (auth_id, username) values('" + userId + "', '" + username + "')");
            }

            public static string getUsername(string userId)
            {
                var row = Dao.Execute("select username from account where auth_id = '" + userId + "'");
                if(row.Length == 0)
                {
                    return null;
                }
                return row[0][0].ToString();
            }

            public static string getUserIdByName(string userName)
            {
                var row = Dao.Execute("select auth_id from account where username = '" + userName + "'");
                if (row.Length == 0)
                {
                    return null;
                }
                return row[0][0].ToString();
            }

            public static void createProject(string userId, string projectName, string projectDescription)
            {
                string project_id = Dao.Execute("insert into project (name, description) values('" + projectName + "', '" + projectDescription + "');" +
                                        "SELECT SCOPE_IDENTITY();")[0][0].ToString();

                Dao.Execute("insert into user_project_role(user_id, project_id, role_id) values('" + userId + "', '" + project_id + "', 1)");

            }
            
            public static DataRow[] getAllProjects(string userId)
            {
                return Dao.Execute("select project.id, project.name, project.description, account.username from project" +
                    " inner join user_project_role on project.id = user_project_role.project_id" +
                    " inner join account on account.auth_id = user_project_role.user_id" +
                    " where account.auth_id = '" + userId + "'");
            }
            public static DataRow[] getAllProjectsByUsername(string username)
            {
                return Dao.Execute("select project.id, project.name, project.description, account.username from project" +
                    " inner join user_project_role on project.id = user_project_role.project_id" +
                    " inner join account on account.auth_id = user_project_role.user_id" +
                    " where account.username = '" + username + "'");
            }

            public static string getRole(string userId, string projectName)
            {
                var row = Dao.Execute("select role_id from user_project_role inner join project on user_project_role.project_id = project.id where user_id = '" + userId + "' and project.name = '"+projectName+"'");
                if (row.Length == 0)
                {
                    return null;
                }
                return row[0][0].ToString();
            }

            public static bool usernameExists(string username)
            {
                DataRow[] rows = Dao.Execute("select username from account where username = '" + username+"'");
                if(rows.Length == 0)
                {
                    return false;
                }
                return true;
            }

            public static DataRow[] getCurrentSprint(string projectName)
            {
                string currentTime = DateTime.Now.ToString();
                
                DataRow[] rows = Execute("select * from sprint inner join project_sprint on project_sprint.id = sprint.id " +
                    "inner join project on project_sprint.project_id = project.id " +
                    "where project.name = " + "'"+projectName+"' and '"+currentTime+ "' between sprint.start_time and sprint.end_time");

                
                return rows;
            }

            public static DataRow[] getAllSprints(string projectName)
            {

                DataRow[] rows = Execute("select * from sprint inner join project_sprint on project_sprint.id = sprint.id " +
                    "inner join project on project_sprint.project_id = project.id " +
                    "where project.name = " + "'" + projectName + "' order by sprint.start_time desc");
                return rows;
            }

            public static void createSprint(string projectId, string name, string start_time, string end_time)
            {
                string sprint_id = Execute("insert into sprint(name, start_time, end_time) values('"+name+"', '"+start_time+"', '"+end_time+"'); " +
                    "select SCOPE_IDENTITY();")[0][0].ToString();

                Execute("insert into project_sprint(project_id, sprint_id) values('"+projectId+ "', '"+sprint_id+"')");
            }

            public static DataRow[] removeSprint(string sprint)
            {

                DataRow[] rows = Execute("delete from sprint where id = '"+sprint+"' ");
                return rows;
            }

            //created_date
            public static void createTask(string sprint_id ,string name, string description, string difficulty, string type_id)
            {
                string task_id = Execute("insert into task(name, description, difficulty, type_id, created_date) " +
                    "values('" + name + "', '" + description + "', '" + difficulty + "', '" + type_id + "',  GETDATE()); " +
                    "select SCOPE_IDENTITY();")[0][0].ToString();

                Execute("insert into task_sprint(task_id, sprint_id, state_id) values('" + task_id + "', '" + sprint_id + "', '1')");
            }

            public static DataRow[] getTasks(string sprint_id)
            {
                DataRow[] rows = Execute("select task.id, task.name, task.description, task.difficulty, task.type_id, task_sprint.state_id, task.created_date from task " +
                    "inner join task_sprint on task.id = task_sprint.task_id " +
                    "where task_sprint.sprint_id = '" + sprint_id + "'");

                return rows;
            }

            public static void setTaskState(string task, string state)
            {
                Execute("update task_sprint set state_id = '" + state + "' where task_id = " + task);
            }

            public static string getProjectId(string user, string projectName)
            {
                DataRow[] row = Execute("select project_id from user_project_role inner join project on user_project_role.project_id = project.id " +
                    "where user_project_role.user_id = '" + user+"' and project.name = '"+projectName+"' ");

                if (row.Length == 0)
                {
                    return null;
                }
                return row[0][0].ToString();
            }

        }



        public static class Projects
        {
            public static DataRow[] getAllProjects()
            {
                return Dao.Execute("select project.id, project.name, project.description, account.username from project" +
                    " inner join user_project_role on project.id = user_project_role.project_id" +
                    " inner join account on account.auth_id = user_project_role.user_id");
            }   

        }

    }



}
