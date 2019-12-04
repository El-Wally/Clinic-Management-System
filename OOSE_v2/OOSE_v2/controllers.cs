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

    static public class controllers
    {
        static public SqlConnection con = new SqlConnection(@"Data Source=FAWZY;Initial Catalog=OOSE;Integrated Security=True;Pooling=False");
        static public void view_patient_controller(string patient_id, Form3 f,ref bool f2)
        {
            if (patient_id != "")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = con.CreateCommand();
                SqlCommand cmd2 = con.CreateCommand();
                SqlCommand cmd3 = con.CreateCommand();
                users u = new users();
                cmd2.CommandText = "select count(*) from patient where id like'" + patient_id + "'";
                if ((int)cmd2.ExecuteScalar() == 1)
                {
                    patient pt = u.search_patient(cmd3, patient_id);
                    views.view_patient(pt,f);
                    f2 = true;
                }
                else
                {
                    MessageBox.Show("no patient found");
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("id field is empty");
            }

        }
        static public void Doc_insert_controller(string id, string diagnose, string prescription, string ed, bool f)
        {
            int i;
            if(f != false && id != "" && prescription != "")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd4 = con.CreateCommand();
                SqlCommand cmd2 = con.CreateCommand();
                if (permission.p == 2 && (ed == "" || int.TryParse(ed, out i)) && ed[0] != '0' && int.Parse(ed) >= 0)
                {
                    if (diagnose != "")
                    {
                        Doctor doc = new Doctor();
                        doc.insert(cmd4, id, prescription, diagnose, ed);
                    }
                    else
                    {
                        MessageBox.Show("diagnose field can't be empty");
                    }
                }
                else
                {
                    MessageBox.Show("Error: please check the excuse days");
                }
                if (permission.p == 3)
                {
                    nurse nr = new nurse();
                    nr.insert(cmd4, id, prescription);
                }
            }
            if(prescription == "")
            {
                MessageBox.Show("prescription field can't be empty");
            }
            if (f == false)
            {
                MessageBox.Show("please search for a patient first");
            }
        }
        static public void view_history_controller(bool f, string id)
        {
            if(f != false)
            {
                users u = new users();
                DataTable dt = u.full_history(id);
                views.view_full_history(dt);
            }
        }
        static public int add_user_controller(string id, string name, string password, int index)
        {
            int i = 0;
            if(index != 0 || index != 1 || index != 2)
            {
                MessageBox.Show("invalid user type");
                return 0;
            }
            if (!int.TryParse(id, out i) || int.Parse(id) < 0 || id.Length > 6)
            {
                MessageBox.Show("id only takes numbers");
                return 0;
            }
            else if (id != "" && name != "" && password != "" && index != -1)
            {
                SqlCommand cmd = db_helper.select_where("users", id);
                if (cmd.ExecuteScalar() != null)
                {
                    MessageBox.Show("a user with the same ID already exists");
                    return 0;
                }
                else
                {
                    con.Open();
                    admin.add_user(con, id, name, password, index);
                    con.Close();
                    MessageBox.Show("user has been added to the database");
                    con.Close();
                    return 1;
                }
            }
            else
            {
                MessageBox.Show("Error: can't add user, some inputs are empty");
                return 0;
            }
        }
        static public void get_drugs_controller(string tname, int index, Form5 f5)
        {
            if (index == 0)
            {
                SqlCommand cmd = db_helper.select_all(tname);
                DataTable dt = inventory.get_drugs(cmd);
                views.view_drugs(dt,f5);
            }
            if (index == 1)
            {
                if (tname != "")
                {
                    SqlCommand cmd = db_helper.select_where("drugs", tname);
                    if (cmd.ExecuteScalar() == null)
                    {
                        MessageBox.Show("no drug was found");
                    }
                    else
                    {
                        DataTable dt = inventory.get_drugs(cmd);
                        views.view_drugs(dt,f5);
                    }
                }
                else
                {
                    MessageBox.Show("please enter a name first");
                }
            }
        }
        static public void get_users_controller(string tname, int index, Form8 f8)
        {
            users u = new users();
            if (index == 0)
            {
                SqlCommand cmd = db_helper.select_all("users");
                DataTable dt = u.get_users(cmd);
                views.view_users(dt,f8);
            }
            if(index == 1)
            {
                if(tname != "")
                {
                    SqlCommand cmd = db_helper.select_where("users", tname);
                    if(cmd.ExecuteScalar() == null)
                    {
                        MessageBox.Show("Error: no user was found");
                    }
                    else
                    {
                        DataTable dt = u.get_users(cmd);
                        views.view_users(dt, f8);
                    }
                }
                else
                {
                    MessageBox.Show("please enter an ID first");
                }
            }
        }
        static public void add_inventory_controller(string name, string qu)
        {
            int i = 0;
            if (int.TryParse(qu, out i))
            {
                int quantity = Int32.Parse(qu);
                if (name != "" && quantity >= 0)
                {
                    con.Open();
                    inventory.add_inventory(con, name, quantity);
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Error: quantity only accepts numbers >= zero, or the name field is empty");
                }
            }
            else
            {
                MessageBox.Show("Error: quantity only accepts numbers");
            }
        }
        static public void set_appointment_controller(string name, string id, string email, bool f, DateTime date)
        {
            if(date <= System.DateTime.Today)
            {
                MessageBox.Show("Wrong Date: please check the date");
            }
            else if(f)
            {
                users u = new users();
                string date2 = date.ToString();
                u.set_appointment(name, id, date2, email);
            }
            else
            {
                MessageBox.Show("please search for a patient first");
            }
        }
    }
}
