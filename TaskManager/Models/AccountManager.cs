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


    }
}