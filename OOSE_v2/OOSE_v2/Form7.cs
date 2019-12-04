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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Admin");
            comboBox1.Items.Add("Doctor");
            comboBox1.Items.Add("nurse");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int r = controllers.add_user_controller(textBox1.Text,  textBox2.Text, textBox3.Text, comboBox1.SelectedIndex);
            if(r == 1)
            {
                textBox1.Text = textBox2.Text = textBox3.Text = "";
                comboBox1.SelectedIndex = -1;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
