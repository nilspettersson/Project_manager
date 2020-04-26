using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TaskManager.Models
{
    public static class Dao
    {

        private static DataRowCollection Execute(string sql)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\vs\TaskManager\TaskManager\App_Data\task_manager_db.mdf;Integrated Security=True";
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
            return dt.Rows;


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
                return row[0][0].ToString();
            }

            public static void createProject(string userId, string projectName, string projectDescription)
            {
                string project_id = Dao.Execute("insert into project (name, description) values('" + projectName + "', '" + projectDescription + "');" +
                                        "SELECT SCOPE_IDENTITY();")[0][0].ToString();

                Dao.Execute("insert into user_project_role(user_id, project_id, role_id) values('" + userId + "', '" + project_id + "', 1)");

            }
            
            public static DataRowCollection getAllProjects(string userId)
            {
                return Dao.Execute("select * from project inner join user_project_role on project.id = user_project_role.project_id");
            }

            public static bool usernameExists(string username)
            {
                DataRowCollection rows = Dao.Execute("select username from account where username = '" + username+"'");
                if(rows.Count == 0)
                {
                    return false;
                }
                return true;
            }

        }


        public static class Projects
        {

            public static DataRowCollection getAllProjects()
            {
                return Dao.Execute("select project.id, project.name, project.description, account.username from project" +
                    " inner join user_project_role on project.id = user_project_role.project_id" +
                    " inner join account on account.auth_id = user_project_role.user_id");
            }   

        }



    }



}
