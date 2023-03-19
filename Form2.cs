using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicketVendorMachine
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text += "*********************************************\n";
            richTextBox1.Text += "**                  Receipt                **\n";
            richTextBox1.Text += "*********************************************\n";
            richTextBox1.Text += "Name: " + textBox1.Text + "\n\n";
            richTextBox1.Text += "Destination: " + textBox2.Text + "\n\n";
            richTextBox1.Text += "Fees: " + textBox3.Text + "\n\n";
            richTextBox1.Text += "*********************************************\n";
            richTextBox1.Text += "*********************************************\n";
            richTextBox1.Text += "*********************************************\n";

        }
    }
}
