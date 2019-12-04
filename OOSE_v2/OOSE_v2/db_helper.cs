using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OOSE_v2
{
    static public class db_helper
    {
        static public SqlConnection con = new SqlConnection(@"Data Source=FAWZY;Initial Catalog=OOSE;Integrated Security=True;Pooling=False");
        static public SqlCommand select_where(string tname, string id)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            if (tname == "users")
            {
                cmd.CommandText = "select * from " + tname + " where id = @id";
            }
            if(tname == "drugs")
            {
                cmd.CommandText = "select * from " + tname + " where name = @id";
            }
            cmd.ExecuteScalar();
            return cmd;
        }
        static public SqlCommand select_all(string tname)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from " + tname + "";
            con.Close();
            return cmd;
        }
        static public void update_pt(string id, string dg)
        {
            DateTime today = DateTime.Today;
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@date", today);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@dg", dg);
            cmd.CommandText = "update patient set visit = @date where id = @id";
            if(permission.p == 2)
            {
                cmd.CommandText = "update patient set visit = @date, diagnose = @dg where id = @id";
            }
            cmd.ExecuteScalar();
            con.Close();
        }
    }
}
