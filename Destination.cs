using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;
using ZXing.Mobile;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TicketVendorMachine
{

    public partial class Destination : Form
    {
        SqlConnection conn = new SqlConnection("initial catalog = TicketVending; data source = MSI\\SQLEXPRESS; integrated security = true");
        SqlDataAdapter data;
        SqlConnection cn;
        DataTable tb;
        SqlCommand cm;
        SqlDataReader dr;
        public Destination()
        {
            InitializeComponent();
        }

        private void btnCancelDes_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Destination_Load(object sender, EventArgs e)
        {
            string sql = "initial catalog = TicketVending; data source = MSI\\SQLEXPRESS; integrated security = true";
            cn = new SqlConnection(sql);
            cn.Open();
            showdes();
            showpayment();
        }

        void showdes()
        {
            string sql = "select distinct dename from destination";
            data = new SqlDataAdapter(sql, cn);
            tb = new DataTable();
            data.Fill(tb);

            comboBox1.DataSource = tb;
            comboBox1.ValueMember = "dename";
            comboBox1.DisplayMember = "dename";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = "select * from destination where dename = @dename ";
            cm = new SqlCommand(s, conn);
            cm.Parameters.AddWithValue("@dename", comboBox1.Text);

            conn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                string demoney = dr["demoney"].ToString() + " VND";
                textBox1.Text = demoney;
            }
            conn.Close();


        }

        void showpayment()
        {
            comboBox2.Items.Add("Credit card");
            comboBox2.Items.Add("QR code payment");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Credit card")
            {
                textBox2.Visible = true;
                label4.Visible = true;
                pictureBox1.Visible = false;
            }
            else
            {
                textBox2.Visible = false;
                label4.Visible = false;
                pictureBox1.Visible = true;
                QRCoder.QRCodeGenerator QR = new QRCoder.QRCodeGenerator();
                var myQR = QR.CreateQrCode("QRpayment", QRCoder.QRCodeGenerator.ECCLevel.H);
                var code = new QRCoder.QRCode(myQR);
                pictureBox1.Image = code.GetGraphic(6);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Credit card")
            {
                string s = "select * from credit_card where crenumber = @crenumber ";
                cm = new SqlCommand(s, conn);
                cm.Parameters.AddWithValue("@crenumber", textBox2.Text);

                conn.Open();
                dr = cm.ExecuteReader();
                string crename = "";
                while (dr.Read())
                {
                    crename = dr["crename"].ToString();
                }

                conn.Close();

                richTextBox1.Clear();
                richTextBox1.Text += "****************************************\n";
                richTextBox1.Text += "**                  Receipt           **\n";
                richTextBox1.Text += "****************************************\n";
                richTextBox1.Text += "Name: " + crename + "\n\n";
                richTextBox1.Text += "Destination: " + comboBox1.Text + "\n\n";
                richTextBox1.Text += "Mode of payment: " + comboBox2.Text + "\n\n";
                richTextBox1.Text += "Credit number: " + textBox2.Text + "\n\n";
                richTextBox1.Text += "Fees: " + textBox1.Text + "\n\n";
                richTextBox1.Text += "****************************************\n";
                richTextBox1.Text += "****************************************\n";
                richTextBox1.Text += "****************************************\n";


                
            }
            else
            {
                string s = "select * from QRcode";
                cm = new SqlCommand(s, conn);

                conn.Open();
                dr = cm.ExecuteReader();
                string qrname = "";
                string qracc = "";
                while (dr.Read())
                {
                    qrname = dr["qrname"].ToString();
                    qracc = dr["accnumber"].ToString();
                }



                richTextBox1.Clear();
                richTextBox1.Text += "****************************************\n";
                richTextBox1.Text += "**                  Receipt           **\n";
                richTextBox1.Text += "****************************************\n";
                richTextBox1.Text += "Name: " + qrname + "\n\n";
                richTextBox1.Text += "Number account: " + qracc + "\n\n";
                richTextBox1.Text += "Destination: " + comboBox1.Text + "\n\n";
                richTextBox1.Text += "Mode of payment: " + comboBox2.Text + "\n\n";
                richTextBox1.Text += "Fees: " + textBox1.Text + "\n\n";
                richTextBox1.Text += "****************************************\n";
                richTextBox1.Text += "****************************************\n";
                richTextBox1.Text += "****************************************\n";

                
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 10;
            int y = 0;
            int charpos = 0;
            while(charpos < richTextBox1.Text.Length)
            {
                if (richTextBox1.Text[charpos] == '\n')
                {
                    charpos++;
                    y += 20;
                    x = 10;
                }
                else if (richTextBox1.Text[charpos] == '\r')
                {
                    charpos++;
                }
                else
                {
                    richTextBox1.Select(charpos, 1);
                    e.Graphics.DrawString(richTextBox1.SelectedText, richTextBox1.SelectionFont, new SolidBrush(richTextBox1.SelectionColor), new PointF(x, y));
                    x = x + 8;
                    charpos++;
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDocument doc = new PrintDocument();
            PrintDialog pd = new PrintDialog();
            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = doc;
            pd.Document = doc;
            doc.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            if(ppd.ShowDialog() == DialogResult.OK)
            {
                if(pd.ShowDialog() == DialogResult.OK)
                {
                    doc.Print();
                }
            }
        }
    }
}

