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
    public partial class Form5 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=FAWZY;Initial Catalog=OOSE;Integrated Security=True;Pooling=False");
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            controllers.get_drugs_controller(textBox1.Text, 1, this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controllers.get_drugs_controller("drugs", 0, this);
        }
    }
    static public class inventory
    {
         static public DataTable get_drugs(SqlCommand cmd)
         {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
         }
          
         static public void check_inventory(SqlConnection con,string name)
         {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select count(*) from drugs where name like'" + name + "'";
            if((int)cmd.ExecuteScalar() == 1)
            {
                cmd.CommandText = "select quantity from drugs where name ='" + name + "'";
                SqlDataReader R = cmd.ExecuteReader();
                if(R.Read())
                {
                    int q = R.GetInt32(0);
                    R.Close();
                    if(q < 1)
                    {
                        MessageBox.Show("warning: no enough quantity");
                    }
                    else
                    {
                        cmd.CommandText = "update drugs set quantity = quantity - 1 where name ='" + name + "'";
                        cmd.ExecuteScalar();
                    }
                }
            }
            else
            {
                MessageBox.Show("the clinic does not provide that drug");
            }
         }
         static public void add_inventory(SqlConnection con,string name , int quantity)
         {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select count(*) from drugs where name like'" + name + "'";
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            if ((int)cmd.ExecuteScalar() == 1)
            {
                MessageBox.Show("drug already exists quantity will be updated");
                cmd.CommandText = "update drugs set quantity = @quantity where name = @name";
                cmd.ExecuteScalar();
            }
            else
            {
                cmd.CommandText = "insert into drugs (name,quantity) values (@name,@quantity)";
                cmd.ExecuteScalar();
            }
            MessageBox.Show("Done");
         }
    }
}
