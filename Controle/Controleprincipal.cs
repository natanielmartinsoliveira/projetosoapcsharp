using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;

using System.Security.Principal;
using System.Security;
using System.IO;
using System.Runtime.InteropServices;


namespace Controle
{
    public partial class Controleprincipal : Form
    {
        public Controleprincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StartService("Service13", 1000);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StopService("Service13", 1000);
        }

        public static void StartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {
                MessageBox.Show("Falha ao Iniciar serviço!");
            }
        }
        public static void StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch
            {
                MessageBox.Show("Falha ao Parar serviço!");
            }
        }
        public static void RestartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {
                MessageBox.Show("Falha ao reiniciar serviço!");
            }
        }

        private void Controleprincipal_Load(object sender, EventArgs e)
        {
            var desktopWorkingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            WindowsIdentity MyIdentity = WindowsIdentity.GetCurrent();

            WindowsPrincipal MyPrincipal = new WindowsPrincipal(MyIdentity);

            WindowsImpersonationContext context = MyIdentity.Impersonate();

            if (!System.IO.Directory.Exists(@"c:\mp_upload"))
            {
                System.IO.Directory.CreateDirectory(@"c:\mp_upload");
            }

            try
            {
                File.Copy(@"\\192.168.0.199\disco02\Myfile.txt", @"c:\MyFile.txt", true);
            }
            catch
            {
                context.Undo();

            }
            */

            

            //copy_stuff(caminho, caminho2);
            if (!CopyFolderContents(@"c:\mp_upload", @"c:\mp_upload2"))
            {
                MessageBox.Show("Falha ao reiniciar serviço!");
            };
            
        }

        private bool CopyFolderContents(string SourcePath, string DestinationPath)
        {

            if (!System.IO.Directory.Exists(SourcePath))
            {
                System.IO.Directory.CreateDirectory(SourcePath);
            }

            if (!System.IO.Directory.Exists(DestinationPath))
            {
                System.IO.Directory.CreateDirectory(DestinationPath);
            }

            SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
            DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

            try
            {
                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }

                    foreach (string files in Directory.GetFiles(SourcePath))
                    {
                        FileInfo fileInfo = new FileInfo(files);
                        fileInfo.CopyTo(string.Format(@"{0}\{1}", DestinationPath, fileInfo.Name), true);
                    }

                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(drs);
                        if (CopyFolderContents(drs, DestinationPath + directoryInfo.Name) == false)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
