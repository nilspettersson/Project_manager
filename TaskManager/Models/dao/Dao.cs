﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

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
                DataRow[] rows = Execute("select * from sprint inner join project_sprint on project_sprint.id = sprint.id " +
                    "inner join project on project_sprint.project_id = project.id " +
                    "where project.name = " + "'"+projectName+"'");
                
                return rows;
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
