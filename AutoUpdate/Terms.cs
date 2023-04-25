using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoUpdate
{
    public partial class Terms : Form
    {
        public Terms()
        {
            InitializeComponent();
        }

        private void Terms_Load(object sender, EventArgs e)
        {
            //timer1.Enabled= true;
            String updaterV = "Error check the terms of services on our Website: https://creativosrp.com/tos/tos.html";
            updaterV = (new WebClient()).DownloadString("https://creativosrp.com/updater/terms.php");
            richTextBox1.Text = updaterV;
            //MessageBox.Show(clientread);
        }

        private void isRead()
        {
            string clientread = ConfigurationManager.AppSettings["read"];
            if (clientread == "true")
            {
                //MessageBox.Show(clientread);
                timer1.Enabled = false;
                Form1 form1 = new Form1(); // Instantiate a Form3 object.
                form1.Show(); // Show Form3 and
                this.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["read"].Value = "true";
            configuration.Save(ConfigurationSaveMode.Modified);
            timer1.Enabled = false;
            Form1 f1 = new Form1(); // Instantiate a Form3 object.
            f1.Show(); // Show Form3 and
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
            System.Windows.Forms.Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
            System.Windows.Forms.Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            isRead();
        }
    }
}
