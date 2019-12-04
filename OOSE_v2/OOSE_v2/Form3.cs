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
    public partial class Form3 : Form
    {
        bool f = false;

        SqlConnection con = new SqlConnection(@"Data Source=FAWZY;Initial Catalog=OOSE;Integrated Security=True;Pooling=False");
        public Form3()
        {
            InitializeComponent();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            controllers.view_patient_controller(textBox1.Text,this,ref f);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (permission.p == 1)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                //textBox5.Enabled = false;
                button1.Enabled = false;
            }
            if (permission.p == 3)
            {
                textBox2.Enabled = false;
                textBox4.Enabled = false;
                //textBox5.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) => controllers.Doc_insert_controller(label6.Text, textBox2.Text, textBox3.Text, textBox4.Text, f);

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            controllers.view_history_controller(f, label6.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            controllers.set_appointment_controller(label5.Text, label6.Text, label16.Text, f, dateTimePicker1.Value);
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
    public class users
    {
        string user_name;
        int user_id;
        public patient search_patient(SqlCommand cmd3, string patient_id)
        {
            patient pt = new patient();
            cmd3.CommandText = "select * from patient where id ='" + patient_id + "'";
            SqlDataReader DR1 = cmd3.ExecuteReader();
            if (DR1.Read())
            {
                pt.id = DR1.GetValue(0).ToString();
                pt.name = DR1.GetString(1);
                pt.visit = DR1.GetValue(3).ToString();
                pt.diagnose = DR1.GetValue(5).ToString();
                pt.email = DR1.GetValue(6).ToString();
            }
            return pt;
        }
        public DataTable full_history(string id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=FAWZY;Initial Catalog=OOSE;Integrated Security=True;Pooling=False");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from records where id =" + id;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public void set_appointment(string name, string id, string date, string email)
        {
            SqlConnection con = new SqlConnection(@"Data Source=FAWZY;Initial Catalog=OOSE;Integrated Security=True;Pooling=False");
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.CommandText = "insert into appointment (id,name,date) values (@id,@name,@date)";
            cmd.ExecuteScalar();
            con.Close();
            string subject = "Next appointment";
            string body = "dear " + name + " your next appointment will be on " + date;
            Email.send_email(email, subject, body);
            MessageBox.Show("Done");
        }
        public DataTable get_users(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        public void insert()
        {

        }
    }
    public class nurse : users
    {
        public void insert(SqlCommand cmd4,string id,string prescreption)
        {
            DateTime today = DateTime.Today;
            cmd4.CommandText = "insert into records (id,prescreption,visit_date) values ('" + id + "','" + prescreption + "','" + today.ToString("dd/MM/yyyy") + "')";
            cmd4.ExecuteScalar();
            SqlConnection con2 = new SqlConnection(@"Data Source=FAWZY;Initial Catalog=OOSE;Integrated Security=True;Pooling=False");
            con2.Open();
            inventory.check_inventory(con2, prescreption);
            con2.Close();
            MessageBox.Show("Done");
            db_helper.update_pt(id, "");
        }
    }
    public class Doctor : users
    {
        public void insert(SqlCommand cmd4, string id,string prescreption,string diagnose, string ed)
        {
            DateTime today = DateTime.Today;
            cmd4.CommandText = "insert into records (id,prescreption,diagnose,e_date,visit_date) values ('" + id + "','" + prescreption + "','" + diagnose +  "','" + ed + "','" + today.ToString("dd/MM/yyyy") + "')";
            cmd4.ExecuteScalar();
            SqlConnection con2 = new SqlConnection(@"Data Source=FAWZY;Initial Catalog=OOSE;Integrated Security=True;Pooling=False");
            con2.Open();
            inventory.check_inventory(con2, prescreption);
            con2.Close();
            MessageBox.Show("Done");
            db_helper.update_pt(id, diagnose);
        }
    }
    static public class admin
    {
        public static void add_user(SqlConnection con, string id, string name,string password,int type)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@type", type+1);
            cmd.CommandText = "insert into users (id,password,type,name) values (@id,@password,@type,@name)";
            cmd.ExecuteScalar();

        }
    }
    public class patient
    {
        public string name, visit, prescreption, diagnose,id,email;

    }
}
