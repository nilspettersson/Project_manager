using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public static class AccountManager
    {


        public static void register(string userId, string username)
        {
            DatabaseManager.Execute("insert into account (auth_id, username) values('" + userId + "', '" + username + "')");

        }
        
        public static string getNameById(string userId)
        {
            var row = DatabaseManager.Execute("select username from account where auth_id = '" + userId + "'");

            return row[0][0].ToString();

        }

        public static void createProject(string userId, string projectName, string projectDescription)
        {

            string project_id = DatabaseManager.Execute("insert into project (name, description) values('"+projectName+"', '"+projectDescription+"');" +
                                    "SELECT SCOPE_IDENTITY();")[0][0].ToString();

            DatabaseManager.Execute("insert into user_project_role(user_id, project_id, role_id) values('" + userId + "', '" + project_id + "', 1)");

        }

        public static DataRowCollection getAllProjects(string userId)
        {

            return DatabaseManager.Execute("select * from project inner join user_project_role on project.id = user_project_role.project_id");


        }


    }
}