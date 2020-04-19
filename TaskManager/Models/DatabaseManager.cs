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
        public static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nils-\source\repos\TaskManager\TaskManager\App_Data\task_manager_db.mdf;Integrated Security=True");

        public static DataRowCollection Execute(string sql)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            System.Diagnostics.Debug.WriteLine("sql: "+sql);
            SqlCommand command = con.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sql;
            //command.ExecuteNonQuery();
            
            SqlDataAdapter adp = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            con.Close();

            return dt.Rows;
        }

    }
}