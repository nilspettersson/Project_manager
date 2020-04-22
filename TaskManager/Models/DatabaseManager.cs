using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TaskManager.Models
{
    public static class DatabaseManager
    {

        public static DataRowCollection Execute(string sql)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nils-\source\repos\TaskManager\TaskManager\App_Data\task_manager_db.mdf;Integrated Security=True";

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

    }
}
