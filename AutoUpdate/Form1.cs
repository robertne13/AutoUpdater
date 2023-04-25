using System;
using System.IO; //borrar carpetas y zip
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net; //zip
using System.Diagnostics; //iniciar programa

using System.Configuration;
using System.Windows.Input;

/// <summary>
/// AutoUpdater v1.0
/// Programado Por Roberto Nervo
/// Para Rsoftware Software de Gestion Comercial
/// </summary>
namespace AutoUpdate
{

    public partial class Form1 : Form

    {
        /// <summary>
        /// Variables que utilizo
        /// </summary>
        String ver;
        String sqlStat;
        string mypath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        string myver= "1.3.0";
        string database = ConfigurationManager.AppSettings["Database"];
        string username = ConfigurationManager.AppSettings["MysqlUser"];
        string databasepw = ConfigurationManager.AppSettings["MysqlPW"];
        string updateVer;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String timem;
            timem = DateTime.Now.ToString("dd-mm-yyyy HH-mm");
            lblAUpd.Text = myver;
            Inicio.Enabled = true;
            lblTime.Text = timem;
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            string clientver = ConfigurationManager.AppSettings["version"];
            lblVer.Text = clientver;
            string folderPath = AppDomain.CurrentDomain.BaseDirectory;  
            DirectoryInfo directory = new DirectoryInfo(folderPath);

            foreach (FileInfo file in directory.GetFiles("*.zip"))
            {
                file.Delete();
            }

        }

        private void TimDescarga_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value == 1000)
            {
                progressBar.Value=0;
                TimDescarga.Enabled = false;
                Backup();
                label2.Text = "Descarga Completada";
            }
        }
        private void Backup()
        {
            TimBackup.Enabled = true;


        }
        private void Borro()
        {
            label2.Text = "Instalando nuevos Archivos";
            string topPath = mypath + @"\Client";

            try
            {
                Directory.Delete(topPath, true);
                bool directoryExists = Directory.Exists(topPath);
            }
            catch (Exception e)
            {
                
            }

            TimUnzip.Enabled = true;

        }
        private void Descomprimir()
        {
            label2.Text = "Organizando Sistema de Ficheros";//Chamullo marca dios
            ///unzip
            //string startPath = @"c:\Rsoftware2\start";
            string zipPath = mypath + @"\update.xrt";
            string extractPath = mypath + @"\Client";
            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);

        }
        private void InicioTpv()
        {

            DialogResult dialogResult = MessageBox.Show("Actualizacion Exitosa desea iniciar CreativosRP?.", "Actualizacion Exitosa", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ///iniciar el programa
                System.Diagnostics.Process.Start(mypath + "\\Client\\crt.exe");
                //this.Close();
                System.Windows.Forms.Application.ExitThread();
                System.Windows.Forms.Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
               ///no hago nada
            }

        }

        private void MsqlQuery()
        {

        }

        /// /////////
        private void MsqlVer()
        {

        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Inicio_Tick(object sender, EventArgs e)
        {
            string updaterV;
            string updv;
            string textFromFile;
            string url;
            try
            {
                url = "http://creativosrp.com/updater/update.php";
                textFromFile = (new WebClient()).DownloadString(url);
                ///////////// version Updater
                updv = "http://creativosrp.com/updater/updv.txt";
                updaterV = (new WebClient()).DownloadString(updv);
                ///////////// si el updater sirve...
                if (myver == updaterV)
                {
                    label1.Text = "Version Actual: " + textFromFile.ToString() + " ";
                    ver = textFromFile.ToString();
                    if (lblVer.Text == textFromFile.ToString())
                    {
                        label2.Text = "SU SOFTWARE SE ENCUENTRA ACTUALIZADO...";
                        label2.Visible = true;
                        button1.Enabled = true;
                        timer1.Enabled = true;
                    }
                    else
                    {
                        updateVer = textFromFile.ToString();
                        WebClient client = new WebClient();
                        client.DownloadProgressChanged += (s, y) =>
                        {
                            progressBar.Value = y.ProgressPercentage * 10;

                        };
                        label2.Text = "Comenzando Descarga";
                        label2.Visible = true;
                        ///descargo              
                        client.DownloadFileAsync(new Uri("https://yourwebsite.com/updater/update.xrt"), mypath + "\\update.xrt");
                        Inicio.Enabled = false;
                    }
                }
                else
                {
                    Inicio.Enabled = false;
                    MessageBox.Show("Este Actualizador Automatico se encuentra obsoleto, decargue la nueva version desde el sitio web www.creativosRP.com");

                    this.Close();


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifique su conexion a internet no se ha podido descargar la actualizacion " );

                Inicio.Enabled = false;
            }


        }

        private void TimBackup_Tick(object sender, EventArgs e)
        {
            Borro();
            TimBackup.Enabled = false;
        }

        private void TimUnzip_Tick(object sender, EventArgs e)
        {
            Descomprimir();
            label2.Text = "Actualizacion Finalizada";
            MsqlVer();
            MsqlQuery();
            button1.Enabled = true;
            TimUnzip.Enabled = false;
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["version"].Value = updateVer;
            configuration.Save(ConfigurationSaveMode.Modified);
            InicioTpv();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
            System.Windows.Forms.Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        int timer = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer = timer + 1;
            if (timer == 3)
            {
                timer1.Enabled = false;
                System.Diagnostics.Process.Start(mypath + "\\Client\\client.exe");
                System.Windows.Forms.Application.ExitThread();
                System.Windows.Forms.Application.Exit();


            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void lblVer_Click(object sender, EventArgs e)
        {

        }

        private void lbl_exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
            System.Windows.Forms.Application.Exit();
        }
    }
}
