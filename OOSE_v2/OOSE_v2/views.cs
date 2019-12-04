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
    static public class views
    {
        static public void view_patient(patient pt, Form3 f)
        {
            //Form3 f = new Form3();
            f.label6.Text = pt.id;
            f.label5.Text = pt.name;
            f.label7.Text = pt.visit;
            f.label8.Text = pt.diagnose;
            f.label16.Text = pt.email;
        }
        static public void view_full_history(DataTable dt)
        {
            Form4 f4 = new Form4();
            f4.dataGridView1.DataSource = dt;
            f4.Show();
        }
        static public void view_drugs(DataTable dt, Form5 f5)
        {
            f5.dataGridView1.DataSource = dt;
        }
        static public void view_users(DataTable dt, Form8 f)
        {
            f.dataGridView1.DataSource = dt;
        }
    }
}
