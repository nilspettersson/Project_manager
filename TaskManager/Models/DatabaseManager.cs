using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TaskManager.Models
{
    public class DatabaseManager
    {
        private static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=[DataDirectory]\task_manager_db.mdf;Integrated Security=True");

        public static DataTable Execute(string sql)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            SqlCommand command = con.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sql;
            command.ExecuteNonQuery();

            SqlDataAdapter adp = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            System.Diagnostics.Debug.WriteLine("no errors so far!!!!!");

            return dt;
        }

    }
}