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
    static class permission
    {
        static public int p;
        static public int id;
        static public int log_in(SqlCommand cmd, SqlCommand cmd2, string id, string password)
        {
            
            if (id != "" && password != "")
            {
                cmd2.CommandText = "select count(*) from users where id like'" + id + "' and password='" + password + "'";
                if ((int)cmd2.ExecuteScalar() == 0)
                {
                    MessageBox.Show("id or password is not correct");
                    return 0;
                }
                else
                {
                    MessageBox.Show("login successful");
                    cmd.CommandText = "select type from users where id ='" + id + "'";
                    SqlDataReader DR1 = cmd.ExecuteReader();
                    if (DR1.Read())
                    {
                        permission.p = DR1.GetInt32(0);
                    }
                    if (permission.p == 2)
                    {
                        int id2 = Convert.ToInt32(id);
                        permission.id = id2;
                    }                 
                    return 1;
                }
            }
            else { return 0; }
        }
        static public void log_out(Form2 f2)
        {
            permission.p = -1;
            f2.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=FAWZY;Initial Catalog=OOSE;Integrated Security=True;Pooling=False");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            SqlCommand cmd2 = con.CreateCommand();
            int f = permission.log_in(cmd, cmd2,textBox1.Text,textBox2.Text);
            if(f == 1)
            {
                this.Hide();
                Form2 F2 = new Form2();
                F2.Show();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
