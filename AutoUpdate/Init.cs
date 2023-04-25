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
    public partial class Init : Form
    {
        int timertostart = 0;
        public Init()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timertostart = timertostart + 1;
            string versionRead = ConfigurationManager.AppSettings["version"];
            string clientread = ConfigurationManager.AppSettings["read"];
            string updv = "https://yourwebsite.com/updater/update.php";
            string updaterV = (new WebClient()).DownloadString(updv);
            if (timertostart  > 2)
            { 
                updaterV = (new WebClient()).DownloadString(updv);
                if (clientread == "false" || versionRead != updaterV)
                {
                    
                    Terms Terms = new Terms(); // Instantiate a Form3 object.
                    Terms.Show(); // Show Form3 and
                    timer1.Enabled = false;
                    this.Visible = false;

            }
            else
                {
                    //MessageBox.Show(clientread);
                    timer1.Enabled = false;
                    Form1 form1 = new Form1(); // Instantiate a Form3 object.
                    form1.Show(); // Show Form3 and
                    this.Visible = false;

            }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Init_Load(object sender, EventArgs e)
        {
            timer1.Enabled=true;
        }


    }
}
