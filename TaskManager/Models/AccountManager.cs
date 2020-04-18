using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public static class AccountManager
    {


        public static void register(string id, string username)
        {
            DatabaseManager.Execute("insert into account (auth_id, username) values('" + id + "', '" + username + "')");

        }
        public static string getNameById(string id)
        {
            var row = DatabaseManager.Execute("select username from account where auth_id = '" + id+"'");

            return row[0][0].ToString();

        }


    }
}