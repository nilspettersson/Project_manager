﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public static class AccountManager
    {


        public static void register(string user_id, string username)
        {
            DatabaseManager.Execute("insert into account (auth_id, username) values('" + user_id + "', '" + username + "')");

        }
        public static string getNameById(string user_id)
        {
            var row = DatabaseManager.Execute("select username from account where auth_id = '" + user_id + "'");

            return row[0][0].ToString();

        }

        public static void createProject(string user_id, string projectName, string projectDescription)
        {

            DatabaseManager.Execute("insert into project (name, description) values('"+projectName+"', '"+projectDescription+"')");

        }


    }
}