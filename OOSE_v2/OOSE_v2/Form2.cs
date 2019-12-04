using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOSE_v2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 F3 = new Form3();
            F3.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                Form5 f5 = new Form5();
                f5.Show();
                controllers.get_drugs_controller("drugs",comboBox1.SelectedIndex,f5);
            }
            if(comboBox1.SelectedIndex == 1)
            {
                Form f6 = new Form6();
                f6.Show();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("View drugs");
            comboBox1.Items.Add("Add Drugs");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(permission.p == 1)
            {
                Form7 f7 = new Form7();
                f7.Show();
            }
            else
            {
                MessageBox.Show("only an admin can add users");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            permission.log_out(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (permission.p == 1)
            {
                Form8 f8 = new Form8();
                f8.Show();
            }
            else
            {
                MessageBox.Show("only an admin can view users");
            }
        }
    }
}
