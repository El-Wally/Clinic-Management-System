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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            controllers.add_inventory_controller(textBox1.Text, textBox2.Text);
            /*SqlConnection con = new SqlConnection(@"Data Source=FAWZY;Initial Catalog=OOSE;Integrated Security=True;Pooling=False");
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                con.Open();
                int quantity = Int32.Parse(textBox2.Text);
                inventory.add_inventory(con,textBox1.Text,quantity);
                con.Close();
                textBox1.Text = textBox2.Text = "";
            }*/
        }
    }
}
