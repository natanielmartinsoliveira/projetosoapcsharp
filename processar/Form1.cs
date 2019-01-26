using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using iTextSharp.tool.xml;
using System.Data.OleDb;
using System.Text.RegularExpressions; // Required for this example
using processar.serviceref;

using System.ServiceProcess;

using System.Security.Principal;
using System.Security;

using System.Runtime.InteropServices;

namespace processar
{
    public partial class Form1 : Form
    {
        private bool isProcessRunning = false;

        public enum LoadStyle
        {
            OnLoad,
            OnShown,
            OnShownDoEvents
        }

        private LoadStyle _ls;
        private string _processo;
        public Form1(string processo)
        {
            InitializeComponent();
            _processo = processo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void processar_parte()
        {

            //if (isProcessRunning)
            //{

               this.Cursor = Cursors.WaitCursor;

               //MessageBox.Show("A process is already running.");
             //   return;
            //}

            // Initialize the thread that will handle the background process
            //  Thread backgroundThread = new Thread(
            //    new ThreadStart(() =>
            //    {
            // Set the flag that indicates if a process is currently running
            //          isProcessRunning = true;

            // Iterate from 0 - 99
            // On each iteration, pause the thread for .05 seconds, then update the progress bar

            //for (int n = 0; n < 100; n++ )
            //{
            //   Thread.Sleep(50);
            //   progressBar1.BeginInvoke(new Action(() => progressBar1.Value = n));
            // }
               //this.label1.Text = "Buscando Fontes de dados... 0%";
               
               
               TimeHasChanged("Buscando Fontes de dados... 0%");
               ProcessRegistry("Buscando Fontes de dados... ");

               Processadados(_processo);

            // Show a dialog box that confirms the process has completed
            //         MessageBox.Show("Thread completed!");

            // Reset the progress bar's value if it is still valid to do so
            //        if (progressBar1.InvokeRequired)
            //          progressBar1.BeginInvoke(new Action(() => progressBar1.Value = 0));

            // Reset the flag that indicates if a process is currently running
            //      isProcessRunning = false;
            //           }
            //         ));

            // Start the background process thread
            //     backgroundThread.Start();
        }

        public void Processadados(string proce)
        {
            int contar = 1;
            //ProgressDialog progressDialog = new ProgressDialog();
            HelloExamplePortTypeClient ex = new HelloExamplePortTypeClient();
            //string simpleResult = ex.HelloWorld("Jon");

            //var myComplexType = new WsPhp.MyComplexType { ID = 1, YourName = "Joniiiiiiii" };
            //WsPhp.MyComplexType complexResult = ex.HelloComplexWorld(myComplexType);

            // var list = new ListCourses();
            // MyComplexType2 complexResult2 = list.ListCourses("Jon");

            //Output
            //Console.WriteLine("Simple: {0}", simpleResult);
            //Console.WriteLine("Complex: {0}", complexResult.YourName);
            String processo = ex.GetProximo(Convert.ToInt32(proce));

            String caminhosalvar = ex.GetCaminhoProcess(Convert.ToInt32(proce));

            
            //progressDialog.ShowDialog();

            var input2 = processo;

            var matches2 = Regex.Matches(input2, @"([0-9 ]*)([\|>; $]*)",
                RegexOptions.Multiline);
            
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;      // Esse é o valor que a progress bar vai subir quando você char a methodo PerformStep então ela vai ser incrementada esse valor até atingir o valor Maximum
            progressBar1.Value = 10;
            contar = 1;

            // while (contar <= 1000)
            // {
            //     progressBar1.PerformStep();
            //     contar++;
            // }
            //label1.ResetText();
           int totalvals = Convert.ToInt32(matches2[1].Groups[1].ToString().Trim()) - Convert.ToInt32(matches2[0].Groups[1].ToString().Trim());
           if (totalvals >= 1)
           {


               TimeHasChanged("Iniciando...");


               String strSelectUserListBuilder = "<style type='text/css'>td img {display: block;}</style><table style='display: inline-table; ' border='0' cellpadding='0' cellspacing='0' width='795'><tr><td><img src='http://localhost:82/dados/images/spacer.gif' width='311' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='141' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='6' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='92' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='245' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r1_c1' src='http://localhost:82/dados/images/ficha1_r1_c1.jpg' width='795' height='64' border='0' id='ficha1_r1_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='64' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r2_c1' src='http://localhost:82/dados/images/ficha1_r2_c1.jpg' width='795' height='8' border='0' id='ficha1_r2_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='8' border='0' alt='' /></td></tr><tr><td rowspan='2' colspan='3'><img name='ficha1_r3_c1' src='http://localhost:82/dados/images/ficha1_r3_c1.jpg' width='458' height='31' border='0' id='ficha1_r3_c1' alt='' /></td><td bgcolor='#8DB3E2' style='background:#8DB3E2'><b><font color='#FFF'>%numero%</font></b></td><td rowspan='2'><img name='ficha1_r3_c5' src='http://localhost:82/dados/images/ficha1_r3_c5.jpg' width='245' height='31' border='0' id='ficha1_r3_c5' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='28' border='0' alt='' /></td></tr><tr><td><img name='ficha1_r4_c4' src='http://localhost:82/dados/images/ficha1_r4_c4.jpg' width='92' height='3' border='0' id='ficha1_r4_c4' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r5_c1' src='http://localhost:82/dados/images/ficha1_r5_c1.jpg' width='795' height='4' border='0' id='ficha1_r5_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r6_c1' src='http://localhost:82/dados/images/ficha1_r6_c1.jpg' width='795' height='36' border='0' id='ficha1_r6_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='36' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r7_c1' src='http://localhost:82/dados/images/ficha1_r7_c1.jpg' width='795' height='11' border='0' id='ficha1_r7_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='11' border='0' alt='' /></td></tr><tr><td rowspan='2'><img name='ficha1_r8_c1' src='http://localhost:82/dados/images/ficha1_r8_c1.jpg' width='311' height='37' border='0' id='ficha1_r8_c1' alt='' /></td><td><b><font color='#1D0BFF'>%codigo%</font></b></td><td rowspan='2' colspan='3'><img name='ficha1_r8_c3' src='http://localhost:82/dados/images/ficha1_r8_c3.jpg' width='343' height='37' border='0' id='ficha1_r8_c3' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='30' border='0' alt='' /></td></tr><tr><td><img name='ficha1_r9_c2' src='http://localhost:82/dados/images/ficha1_r9_c2.jpg' width='141' height='7' border='0' id='ficha1_r9_c2' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='7' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1;' ><table width='100%' border='0'><tr><td width='88' height='76'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%descricao%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='84' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r11_c1' src='http://localhost:82/dados/images/ficha1_r11_c1.jpg' width='795' height='3' border='0' id='ficha1_r11_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r12_c1' src='http://localhost:82/dados/images/ficha1_r12_c1.jpg' width='795' height='41' border='0' id='ficha1_r12_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r13_c1' src='http://localhost:82/dados/images/ficha1_r13_c1.jpg' width='795' height='29' border='0' id='ficha1_r13_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='29' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1' ><table width='100%' border='0'><tr><td width='88' height='29'>&nbsp;</td><td width='697'><font color='#1D0BFF'>%ncm%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='34' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r15_c1' src='http://localhost:82/dados/images/ficha1_r15_c1.jpg' width='795' height='3' border='0' id='ficha1_r15_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r16_c1' src='http://localhost:82/dados/images/ficha1_r16_c1.jpg' width='795' height='41' border='0' id='ficha1_r16_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r17_c1' src='http://localhost:82/dados/images/ficha1_r17_c1.jpg' width='795' height='24' border='0' id='ficha1_r17_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='24' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1' ><table width='100%' border='0'><tr><td width='88' height='29'>&nbsp;</td><td width='697'><font color='#1D0BFF' size='3' >%fornecedor%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='35' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r19_c1' src='http://localhost:82/dados/images/ficha1_r19_c1.jpg' width='795' height='4' border='0' id='ficha1_r19_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r20_c1' src='http://localhost:82/dados/images/ficha1_r20_c1.jpg' width='795' height='40' border='0' id='ficha1_r20_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r21_c1' src='http://localhost:82/dados/images/ficha1_r21_c1.jpg' width='795' height='25' border='0' id='ficha1_r21_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='25' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1'><table width='100%' border='0'><tr><td width='88' height='29'>&nbsp;</td><td width='697'><font color='#1D0BFF'>%composicao%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='36' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r23_c1' src='http://localhost:82/dados/images/ficha1_r23_c1.jpg' width='795' height='4' border='0' id='ficha1_r23_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r24_c1' src='http://localhost:82/dados/images/ficha1_r24_c1.jpg' width='795' height='40' border='0' id='ficha1_r24_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r25_c1' src='http://localhost:82/dados/images/ficha1_r25_c1.jpg' width='795' height='51' border='0' id='ficha1_r25_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='51' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1'><table width='100%' border='0'><tr><td width='88' height='35'>&nbsp;</td><td width='697'><font color='#1D0BFF'>%areadeaplicacao%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r27_c1' src='http://localhost:82/dados/images/ficha1_r27_c1.jpg' width='795' height='3' border='0' id='ficha1_r27_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r28_c1' src='http://localhost:82/dados/images/ficha1_r28_c1.jpg' width='795' height='37' border='0' id='ficha1_r28_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='37' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r29_c1' src='http://localhost:82/dados/images/ficha1_r29_c1.jpg' width='795' height='54' border='0' id='ficha1_r29_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='54' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1'><table width='100%' border='0'><tr><td width='88' height='38'>&nbsp;</td><td width='697'><font color='#1D0BFF'>%equipamento%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='44' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r31_c1' src='http://localhost:82/dados/images/ficha1_r31_c1.jpg' width='795' height='3' border='0' id='ficha1_r31_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r32_c1' src='http://localhost:82/dados/images/ficha1_r32_c1.jpg' width='795' height='64' border='0' id='ficha1_r32_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='64' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r33_c1' src='http://localhost:82/dados/images/ficha1_r33_c1.jpg' width='795' height='3' border='0' id='ficha1_r33_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1'><table width='100%' border='0'><tr><td width='88' height='167'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF' size='3'>%funcao%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='174' border='0' alt='' /></td></tr></table>";
               // strSelectUserListBuilder = strSelectUserListBuilder + 
               strSelectUserListBuilder = strSelectUserListBuilder + "<table style='display: inline-table;' border='0' cellpadding='0' cellspacing='0' width='795'><tr><td><img src='http://localhost:82/dados/images/spacer.gif' width='83' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='3' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='3' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='2' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='18' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='2' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='2' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='344' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='18' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='5' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='19' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='6' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='47' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='238' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' /></td></tr><tr><td colspan='19'><img name='ficha02_r1_c1' src='http://localhost:82/dados/images/ficha02_r1_c1.jpg' width='795' height='69' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='69' border='0' /></td></tr><tr><td colspan='19'><img name='ficha02_r2_c1' src='http://localhost:82/dados/images/ficha02_r2_c1.jpg' width='795' height='6' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='6' border='0' /></td></tr><tr><td rowspan='2' colspan='11'><img name='ficha02_r3_c1' src='http://localhost:82/dados/images/ficha02_r3_c1.jpg' width='460' height='35' border='0'/></td><td colspan='7' style='background:#8DB3E2'><b><font color='#FFF'>%numero%</font></b></td><td rowspan='2'><img src='http://localhost:82/dados/images/ficha02_r3_c19.jpg' width='238' height='35' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='31' border='0' /></td></tr><tr><td colspan='7'><img src='http://localhost:82/dados/images/ficha02_r4_c12.jpg' width='97' height='4' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img name='ficha02_r6_c1' src='http://localhost:82/dados/images/ficha02_r6_c1.jpg' width='795' height='32' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='32' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img name='ficha02_r8_c1' src='http://localhost:82/dados/images/ficha02_r8_c1.jpg' width='795' height='5' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='5' border='0' /></td></tr><tr><td rowspan='3' colspan='4'><img name='ficha02_r9_c1' src='http://localhost:82/dados/images/ficha02_r9_c1.jpg' width='91' height='150' border='0'/></td><td colspan='5' rowspan='2' valign='top' style='background:#C7D9F1'><table width='100%' height='146' border='0'><tr><td width='14' height='21' align='center' style='padding-top:10px;'><font color='#1D0BFF'>%contato_1%</font></td></tr><tr><td height='17' align='center'></td></tr><tr><td height='17' align='center' style='padding-top:10px;'><font color='#1D0BFF'>%contato_2%</font></td></tr><tr><td height='21' align='center'></td></tr><tr><td height='17' align='center' style='padding-top:10px;'><font color='#1D0BFF'>%contato_3%</font></td></tr><tr><td height='11' align='center'></td></tr><tr><td height='22' align='center' style='padding-top:10px;'><font color='#1D0BFF'>%contato_4%</font></td></tr></table></td><td rowspan='3' colspan='4'><img src='http://localhost:82/dados/images/ficha02_r9_c10.jpg' width='368' height='150' border='0'/></td><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr><td height='24' style='padding-top:10px;'><font color='#1D0BFF' >%contato_5%</font></td></tr><tr><td height='19'></td></tr><tr><td height='21' style='padding-top:10px;'><font color='#1D0BFF' >%contato_6%</font></td></tr><tr><td height='15'></td></tr><tr><td height='19' style='padding-top:10px;' ><font color='#1D0BFF'>%contato_7%</font></td></tr></table></td><td rowspan='3' colspan='3'><img src='http://localhost:82/dados/images/ficha02_r9_c17.jpg' width='286' height='150' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='110' border='0' /></td></tr><tr><td rowspan='2' colspan='3'><img src='http://localhost:82/dados/images/ficha02_r10_c14.jpg' width='26' height='40' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='38' border='0' /></td></tr><tr><td colspan='5'><img src='http://localhost:82/dados/images/ficha02_r11_c5.jpg' width='24' height='2' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='2' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r13_c1.jpg' width='795' height='41' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r15_c1.jpg' width='795' height='6' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='6' border='0' /></td></tr><tr><td rowspan='3' colspan='5'><img src='http://localhost:82/dados/images/ficha02_r16_c1.jpg' width='92' height='116' border='0'/></td><td colspan='5' rowspan='2' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr><td height='24' style='padding-top:5px;'><font color='#1D0BFF'>%vidautil_1%</font></td></tr><tr><td height='16'></td></tr><tr><td height='21' style='padding-top:10px;'><font color='#1D0BFF'>%vidautil_2%</font></td></tr><tr><td height='15'></td></tr><tr><td height='19' style='padding-top:10px;'><font color='#1D0BFF'>%vidautil_3%</font></td></tr></table></td><td rowspan='3' colspan='4'><img src='http://localhost:82/dados/images/ficha02_r16_c11.jpg' width='368' height='116' border='0'/></td><td colspan='3' style='background:#C7D9F1'><table width='100%' border='0'><tr><td style='padding-top:5px;'><font color='#1D0BFF'>%vidautil_4%</font></td></tr><tr><td height='13'></td></tr><tr><td style='padding-top:5px;'><font color='#1D0BFF'>%vidautil_5%</font></td></tr></table></td><td rowspan='3' colspan='2'><img src='http://localhost:82/dados/images/ficha02_r16_c18.jpg' width='285' height='116' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='72' border='0' /></td></tr><tr><td rowspan='2' colspan='3'><img src='http://localhost:82/dados/images/ficha02_r17_c15.jpg' width='26' height='44' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='36' border='0' /></td></tr><tr><td colspan='5'><img src='http://localhost:82/dados/images/ficha02_r18_c6.jpg' width='24' height='8' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='8' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r20_c1.jpg' width='795' height='41' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r22_c1.jpg' width='795' height='7' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='7' border='0' /></td></tr><tr><td rowspan='2' colspan='3'><img src='http://localhost:82/dados/images/ficha02_r23_c1.jpg' width='89' height='109' border='0'/></td><td colspan='5' style='background:#C7D9F1'><table width='100%' border='0'><tr><td height='20'><font color='#1D0BFF'>%estado_1%</font></td></tr><tr><td height='19'></td></tr><tr><td height='18'><font color='#1D0BFF'>%estado_2%</font></td></tr><tr><td height='15'></td></tr><tr><td height='19'><font color='#1D0BFF'>%estado_3%</font></td></tr></table></td><td rowspan='2' colspan='11'><img src='http://localhost:82/dados/images/ficha02_r23_c9.jpg' width='682' height='109' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='104' border='0' /></td></tr><tr><td colspan='5'><img src='http://localhost:82/dados/images/ficha02_r24_c4.jpg' width='24' height='5' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='5' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r26_c1.jpg' width='795' height='36' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='36' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r28_c1.jpg' width='795' height='9' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='9' border='0' /></td></tr><tr><td rowspan='3' colspan='2'><img src='http://localhost:82/dados/images/ficha02_r29_c1.jpg' width='86' height='105' border='0'/></td><td colspan='5' rowspan='2' style='background:#C7D9F1'><table width='100%' border='0'><tr><td height='20'><font color='#1D0BFF'>%atividade_1%</font></td></tr><tr><td height='19'></td></tr><tr><td height='18'><font color='#1D0BFF'>%atividade_2%</font></td></tr><tr><td height='15'></td></tr><tr><td height='19'><font color='#1D0BFF'>%atividade_3%</font></td></tr></table></td><td rowspan='3' colspan='5'><img src='http://localhost:82/dados/images/ficha02_r29_c8.jpg' width='367' height='105' border='0'/></td><td colspan='3' style='background:#C7D9F1'><table width='100%' border='0'><tr><td><font color='#1D0BFF' style='margin-top:-2px'>%atividade_4%</font></td></tr><tr><td height='13'></td></tr><tr><td><font color='#1D0BFF'>%atividade_5%</font></td></tr></table></td><td rowspan='3' colspan='4'><img src='http://localhost:82/dados/images/ficha02_r29_c16.jpg' width='292' height='105' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='64' border='0' /></td></tr><tr><td rowspan='2' colspan='3'><img src='http://localhost:82/dados/images/ficha02_r30_c13.jpg' width='25' height='41' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='39' border='0' /></td></tr><tr><td colspan='5'><img src='http://localhost:82/dados/images/ficha02_r31_c3.jpg' width='25' height='2' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='2' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r33_c1.jpg' width='795' height='40' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r35_c1.jpg' width='795' height='4' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' /></td></tr><tr><td rowspan='2'><img src='http://localhost:82/dados/images/ficha02_r36_c1.jpg' width='83' height='93' border='0'/></td><td colspan='5' style='background:#C7D9F1' ><table width='100%' border='0'><tr><td height='20'><font color='#1D0BFF'>%ambiente_1%</font></td></tr><tr><td height='11'></td></tr><tr><td height='18'><font color='#1D0BFF'>%ambiente_2%</font></td></tr><tr><td height='9'></td></tr><tr><td height='19'><font color='#1D0BFF'>%ambiente_3%</font></td></tr></table></td><td rowspan='2' colspan='13'><img src='http://localhost:82/dados/images/ficha02_r36_c7.jpg' width='685' height='93' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='91' border='0' /></td></tr><tr><td colspan='5'><img src='http://localhost:82/dados/images/ficha02_r37_c2.jpg' width='27' height='2' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='2' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r39_c1.jpg' width='795' height='40' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r40_c1.jpg' width='795' height='28' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='28' border='0' /></td></tr><tr><td colspan='19' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr><td width='88' height='98'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%complementar%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='104' border='0' /></td></tr></table>";
               //String htmlText = strSelectUserListBuilder.ToString();
               strSelectUserListBuilder = strSelectUserListBuilder + "<table style='display: inline-table;' border='0' cellpadding='0' cellspacing='0' width='795'><tr><td><img src='http://localhost:82/dados/images/spacer.gif' width='460' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='80' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='255' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r1_c1' src='http://localhost:82/dados/images/ficha3_r1_c1.jpg' width='795' height='66' border='0' id='ficha3_r1_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='66' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r2_c1' src='http://localhost:82/dados/images/ficha3_r2_c1.jpg' width='795' height='7' border='0' id='ficha3_r2_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='7' border='0' alt='' /></td></tr><tr><td rowspan='2'><img name='ficha3_r3_c1' src='http://localhost:82/dados/images/ficha3_r3_c1.jpg' width='460' height='32' border='0' id='ficha3_r3_c1' alt='' /></td><td style=' background:#8DB3E2'><b><font color='#FFF'>%numero%</font></b></td><td rowspan='2'><img name='ficha3_r3_c3' src='http://localhost:82/dados/images/ficha3_r3_c3.jpg' width='255' height='32' border='0' id='ficha3_r3_c3' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='27' border='0' alt='' /></td></tr><tr><td><img name='ficha3_r4_c2' src='http://localhost:82/dados/images/ficha3_r4_c2.jpg' width='80' height='5' border='0' id='ficha3_r4_c2' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='5' border='0' alt='' /></td></tr><tr><td colspan='3'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r6_c1' src='http://localhost:82/dados/images/ficha3_r6_c1.jpg' width='795' height='41' border='0' id='ficha3_r6_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r7_c1' src='http://localhost:82/dados/images/ficha3_r7_c1.jpg' width='795' height='40' border='0' id='ficha3_r7_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <font color='#1D0BFF'>%classificao%</font></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='19' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r9_c1' src='http://localhost:82/dados/images/ficha3_r9_c1.jpg' width='795' height='23' border='0' id='ficha3_r9_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='23' border='0' alt='' /></td></tr><tr> <td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr><td width='88' height='47'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%contatofisicomateria%</font></td></tr> </table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='54' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r11_c1' src='http://localhost:82/dados/images/ficha3_r11_c1.jpg' width='795' height='24' border='0' id='ficha3_r11_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='24' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr> <td width='88' height='47'>&nbsp;</td> <td width='697' valign='top'><font color='#1D0BFF'>%consumoimediato%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='54' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r13_c1' src='http://localhost:82/dados/images/ficha3_r13_c1.jpg' width='795' height='27' border='0' id='ficha3_r13_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='27' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='47'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%materialintegra%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='55' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r15_c1' src='http://localhost:82/dados/images/ficha3_r15_c1.jpg' width='795' height='54' border='0' id='ficha3_r15_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='54' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='47'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%essencial%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='52' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r17_c1' src='http://localhost:82/dados/images/ficha3_r17_c1.jpg' width='795' height='26' border='0' id='ficha3_r17_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='26' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='50'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%individual%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='56' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r19_c1' src='http://localhost:82/dados/images/ficha3_r19_c1.jpg' width='795' height='24' border='0' id='ficha3_r19_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='24' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='39'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%ferramenta%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='46' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r21_c1' src='http://localhost:82/dados/images/ficha3_r21_c1.jpg' width='795' height='26' border='0' id='ficha3_r21_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='26' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='39'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%partemaquina%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='46' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r23_c1' src='http://localhost:82/dados/images/ficha3_r23_c1.jpg' width='795' height='32' border='0' id='ficha3_r23_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='32' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='39'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%recuperacao%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='49' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r25_c1' src='http://localhost:82/dados/images/ficha3_r25_c1.jpg' width='795' height='25' border='0' id='ficha3_r25_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='25' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='45'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%desgaste%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='51' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r27_c1' src='http://localhost:82/dados/images/ficha3_r27_c1.jpg' width='795' height='52' border='0' id='ficha3_r27_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='52' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='28'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%exaustacao%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='36' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r29_c1' src='http://localhost:82/dados/images/ficha3_r29_c1.jpg' width='795' height='23' border='0' id='ficha3_r29_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='23' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='60'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%consumonoprocesso%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='66' border='0' alt='' /></td></tr></table>";

               strSelectUserListBuilder = strSelectUserListBuilder + "<table style='display: inline-table;' border='0' cellpadding='0' cellspacing='0' width='795'><tr><td><img src='http://localhost:82/dados/images/spacer.gif' width='92' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='3' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='19' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='3' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='344' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='89' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='245' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r1_c1' src='http://localhost:82/dados/images/ficha4_r1_c1.jpg' width='795' height='71' border='0' id='ficha4_r1_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='71' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r2_c1' src='http://localhost:82/dados/images/ficha4_r2_c1.jpg' width='795' height='7' border='0' id='ficha4_r2_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='7' border='0' alt='' /></td></tr><tr><td rowspan='2' colspan='5'><img name='ficha4_r3_c1' src='http://localhost:82/dados/images/ficha4_r3_c1.jpg' width='461' height='34' border='0' id='ficha4_r3_c1' alt='' /></td><td style='background:#8DB3E2'><b><font color='#FFF'>%numero%</font></b></td><td rowspan='2'><img name='ficha4_r3_c7' src='http://localhost:82/dados/images/ficha4_r3_c7.jpg' width='245' height='34' border='0' id='ficha4_r3_c7' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='30' border='0' alt='' /></td></tr><tr><td><img name='ficha4_r4_c6' src='http://localhost:82/dados/images/ficha4_r4_c6.jpg' width='89' height='4' border='0' id='ficha4_r4_c6' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' alt='' /></td></tr><tr><td colspan='7'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r6_c1' src='http://localhost:82/dados/images/ficha4_r6_c1.jpg' width='795' height='40' border='0' id='ficha4_r6_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r7_c1' src='http://localhost:82/dados/images/ficha4_r7_c1.jpg' width='795' height='28' border='0' id='ficha4_r7_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='28' border='0' alt='' /></td></tr><tr><td colspan='7' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr> <td width='88' height='98'>&nbsp;</td> <td width='697' valign='top'><font color='#1D0BFF'>%classificacaomaterial%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='104' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r9_c1' src='http://localhost:82/dados/images/ficha4_r9_c1.jpg' width='795' height='41' border='0' id='ficha4_r9_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r10_c1' src='http://localhost:82/dados/images/ficha4_r10_c1.jpg' width='795' height='18' border='0' id='ficha4_r10_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='18' border='0' alt='' /></td></tr><tr><td rowspan='12'><img name='ficha4_r11_c1' src='http://localhost:82/dados/images/ficha4_r11_c1.jpg' width='92' height='452' border='0' id='ficha4_r11_c1' alt='' /></td><td colspan='2' style='background:#C7D9F1'><font color='#1D0BFF'>%insumodoproduto%</font></td><td rowspan='6' colspan='4'><img name='ficha4_r11_c4' src='http://localhost:82/dados/images/ficha4_r11_c4.jpg' width='681' height='237' border='0' id='ficha4_r11_c4' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='23' border='0' alt='' /></td></tr><tr><td colspan='2'><img name='ficha4_r12_c2' src='http://localhost:82/dados/images/ficha4_r12_c2.jpg' width='22' height='31' border='0' id='ficha4_r12_c2' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='31' border='0' alt='' /></td></tr><tr><td colspan='2' style='background:#C7D9F1'><font color='#1D0BFF'>%insumodoprocesso%</font></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='23' border='0' alt='' /></td></tr><tr><td colspan='2'><img name='ficha4_r14_c2' src='http://localhost:82/dados/images/ficha4_r14_c2.jpg' width='22' height='61' border='0' id='ficha4_r14_c2' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='61' border='0' alt='' /></td></tr><tr><td colspan='2' style='background:#C7D9F1'><font color='#1D0BFF'>%intermediario%</font></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='22' border='0' alt='' /></td></tr><tr><td colspan='2'><img name='ficha4_r16_c2' src='http://localhost:82/dados/images/ficha4_r16_c2.jpg' width='22' height='77' border='0' id='ficha4_r16_c2' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='77' border='0' alt='' /></td></tr><tr><td rowspan='6'><img name='ficha4_r17_c2' src='http://localhost:82/dados/images/ficha4_r17_c2.jpg' width='3' height='215' border='0' id='ficha4_r17_c2' alt='' /></td><td colspan='2' style='background:#C7D9F1'><font color='#1D0BFF'>%ativo%</font></td><td rowspan='6' colspan='3'><img name='ficha4_r17_c5' src='http://localhost:82/dados/images/ficha4_r17_c5.jpg' width='678' height='215' border='0' id='ficha4_r17_c5' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='21' border='0' alt='' /></td></tr><tr><td colspan='2'><img name='ficha4_r18_c3' src='http://localhost:82/dados/images/ficha4_r18_c3.jpg' width='22' height='47' border='0' id='ficha4_r18_c3' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='47' border='0' alt='' /></td></tr><tr><td colspan='2' style='background:#C7D9F1'><font color='#1D0BFF'>%embalagem%</font></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='23' border='0' alt='' /></td></tr><tr><td colspan='2'><img name='ficha4_r20_c3' src='http://localhost:82/dados/images/ficha4_r20_c3.jpg' width='22' height='51' border='0' id='ficha4_r20_c3' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='51' border='0' alt='' /></td></tr><tr><td colspan='2' style='background:#C7D9F1'><font color='#1D0BFF'>%usoconsumo%</font></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='23' border='0' alt='' /></td></tr><tr><td colspan='2'><img name='ficha4_r22_c3' src='http://localhost:82/dados/images/ficha4_r22_c3.jpg' width='22' height='50' border='0' id='ficha4_r22_c3' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='50' border='0' alt='' /></td></tr><tr><td colspan='7'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='4' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r24_c1' src='http://localhost:82/dados/images/ficha4_r24_c1.jpg' width='795' height='40' border='0' id='ficha4_r24_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' alt='' /></td></tr><tr><td colspan='7'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='4' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' alt='' /></td></tr><tr><td colspan='7' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr> <td width='88' height='255'>&nbsp;</td> <td width='697' valign='top'><font color='#1D0BFF'>%adcionais%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='263' border='0' alt='' /></td></tr></table>";
               //CreatePDFFromHTMLFile(htmlText , pdfFileName);

               String[] folhas = new String[totalvals+1];
               int[] folhas_num = new int[totalvals+1];
               String[] codigos = new String[totalvals + 1];

               var input = "30 = %classificao% ; 0 = %numero% ; 1 = %codigo% ; 4 = %descricao% ; 2 = %ncm% ; 3 = %fornecedor% ; 5 = %composicao% ; 6 = %areadeaplicacao% ; 7 = %equipamento% ; 8 = %funcao% ; 17 = %complementar% ; 18 = %contatofisicomateria% ; 19 = %consumoimediato% ; 20 = %materialintegra% ; 21 = %essencial% ; 22 = %individual% ; 23 = %ferramenta% ; 24 = %partemaquina% ; 25 = %recuperacao% ; 26 = %desgaste% ; 27 = %exaustacao% ; 28 = %consumonoprocesso% ; 29 = %classificacaomaterial% ; 31 = %adcionais% ;	35 = %contato_1% ; 36 = %contato_2% ; 37 = %contato_3% ; 38 = %contato_4% ;	39 = %contato_5% ; 40 = %contato_6% ; 41 = %contato_7% ; 42 = %vidautil_1% ; 43 = %vidautil_2% ; 44 = %vidautil_3% ; 45 = %vidautil_4% ; 46 = %vidautil_5% ; 47 = %estado_1% ; 48 = %estado_2% ; 49 = %estado_3% ; 50 = %atividade_1% ; 51 = %atividade_2% ; 52 = %atividade_3% ; 53 = %atividade_4% ; 54 = %atividade_5% ; 55 = %ambiente_1% ; 56 = %ambiente_2% ; 57 = %ambiente_3% ; 59 = %embalagem% ; 60 = %insumodoproduto% ;	61 = %insumodoprocesso%; 62 = %ativo% ; 63 = %intermediario% ; 64 = %usoconsumo% ";

               var matches = Regex.Matches(input, @"([0-9 ]*)=( [%]\w*[%])([\|>; $]*)",
                   RegexOptions.Multiline);
               var template = "Number[{0}] = {1}\r\n" +
                              "Name[{0}] = {2}\r\n" +
                              "Separator[{0}] = {3}\r\n";
               var sb = new StringBuilder();
               //sb.AppendLine("MyClass");
               //int counter = 0;
               //Console.WriteLine(sb.ToString());
               //Console.ReadKey();
               int m = 20;
               //  progressBar1.BeginInvoke(new Action(() => progressBar1.Value = m));
               //progressBar1.Step = 1;
               progressBar1.Value = m;
               //progressDialog.UpdateProgress(m);
               //progressDialog.BeginInvoke(new Action(() => progressBar1.Value = m));

               TimeHasChanged("Matriz Carregada!");
               ProcessRegistry("Matriz de relação de dados carregada. ");

               List<string> values = new List<string>();

               string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\PLANILHA_PROCESSAMENTO.xlsb;Extended Properties=\"Excel 12.0 Xml;HDR=NO;\"";
               using (OleDbConnection conn = new OleDbConnection(constr))
               {


                   OleDbCommand command = new OleDbCommand("Select * from [FINAL$a" + matches2[0].Groups[1].ToString().Trim() + ":ca" + matches2[1].Groups[1].ToString().Trim() + "]", conn);
                   //SELECT NAME, TELEFONE, DATA FROM   [sheet1$a1:q633] WHERE  NAME IN (SELECT * FROM  [sheet2$a1:a2])
                   conn.Open();

                   OleDbDataReader reader = command.ExecuteReader();
                   ProcessRegistry("Iniciando Mesclar fontes de dados com a relação.");
                   var cont = 0;
                   if (reader.HasRows)
                   {
                       while (reader.Read())
                       {
                           // this assumes just one column, and the value is text

                           //values.Add(value);
                           //Console.WriteLine(value + " - " + reader[6].ToString());
                           //strSelectUserListBuilder.Replace("oq","sub");
                           int counter = 0;
                           codigos[cont] = reader[1].ToString();

                          // if (codigos[cont] != "" || codigos[cont] != null )
                          // {
                                                    
                               foreach (Match item in matches)
                               {
                                   //sb.AppendLine(String.Format(template,
                                   //  counter++,

                                   if (counter == 0)
                                   {
                                       //Console.WriteLine(item.Groups[1].ToString().Trim());
                                       //Console.WriteLine(item.Groups[2].ToString().Trim());
                                       //Console.WriteLine(item.Groups[3].ToString().Trim());

                                       int teste = int.Parse(item.Groups[1].ToString().Trim());
                                       string value = reader[teste].ToString();
                                       //ProcessRegistry("vrteste: " + teste);
                                       //Console.WriteLine(item.Groups[2].ToString().Trim() + " -- -- - - " + value + "cooooontt "+ cont);

                                       //----------------------------------------------------------------------------------------
                                       string tmp = Regex.Replace(value, "<", "");
                                       tmp = Regex.Replace(tmp, ">", "");
                                       //tmp = Regex.Replace(tmp, "/", "-");
                                       //tmp = Regex.Replace(tmp, "///", "//");
                                       //tmp = Regex.Replace(tmp, "////", "-");
                                       tmp = Regex.Replace(tmp, ";", ";");
                                       tmp = Regex.Replace(tmp, ";;", ";");
                                       tmp = Regex.Replace(tmp, ";;;", ";");
                                       tmp = Regex.Replace(tmp, ";;;;", ";");
                                       tmp = Regex.Replace(tmp, "\"", " ");
                                       tmp = Regex.Replace(tmp, "\'", " ");
                                       tmp = Regex.Replace(tmp, "  ", " ");
                                       tmp = Regex.Replace(tmp, "&", " e ");
                                       tmp = Regex.Replace(tmp, "%", " Porcento ");
                                       //tmp = Regex.Replace(tmp, "$", "  ");
                                       //tmp = Regex.Replace(tmp, "(", " ");
                                       //---------------------------------------------------------------------------------------


                                       folhas[cont] = strSelectUserListBuilder.Replace(item.Groups[2].ToString().Trim(), tmp);
                                       //ProcessRegistry("vrreplace: " + item.Groups[2].ToString().Trim());
                                       //ProcessRegistry("vrcont: " + cont);
                                       folhas_num[cont] = cont;
                                   }
                                   else
                                   {
                                       //Console.WriteLine(item.Groups[1].ToString().Trim());
                                       //Console.WriteLine(item.Groups[2].ToString().Trim());
                                       //Console.WriteLine(item.Groups[3].ToString().Trim());



                                      // try{
                                           int teste = int.Parse(item.Groups[1].ToString().Trim());
                                           string value = reader[teste].ToString();
                                           //TimeHasChanged("vrteste: " + teste);
                                           string tmp = Regex.Replace(value, "<", "");
                                           tmp = Regex.Replace(tmp, ">", "");
                                           //tmp = Regex.Replace(tmp, "/", "-");
                                           //tmp = Regex.Replace(tmp, "///", "//");
                                           //tmp = Regex.Replace(tmp, "////", "-");
                                           tmp = Regex.Replace(tmp, ";", ";");
                                           tmp = Regex.Replace(tmp, ";;", ";");
                                           tmp = Regex.Replace(tmp, ";;;", ";");
                                           tmp = Regex.Replace(tmp, ";;;;", ";");
                                           tmp = Regex.Replace(tmp, "\"", " ");
                                           tmp = Regex.Replace(tmp, "\'", " ");
                                           tmp = Regex.Replace(tmp, "  ", " ");
                                           tmp = Regex.Replace(tmp, "&", " e ");
                                           tmp = Regex.Replace(tmp, "%", " Porcento ");
                                           //Console.WriteLine(item.Groups[2].ToString().Trim() + " -- -- - - " + value + "cooooontt "+ cont);
                                           folhas[cont] = folhas[cont].Replace(item.Groups[2].ToString().Trim(), tmp);
                                           //ProcessRegistry("vrreplace: " + item.Groups[2].ToString().Trim());
                                           //ProcessRegistry("vrcont: " + cont);
                                           folhas_num[cont] = cont;
                                       //}catch (Exception er)
                                       //{
                                          // ProcessRegistry("Exception Message: " + er.Message);

                                       //}
                                   
                                   }
                                   counter++;
                                   Double taxa = ((60 - progressBar1.Value) * counter) / item.Length;
                                   //Double soma = m + taxa;
                                   Double soma = m + taxa;
                                   //MessageBox.Show(m.ToString());
                                   m = Convert.ToInt32(soma);
                                   progressBar1.Value = m;
                                   //m = m + (counter*1);
                                   //progressBar1.BeginInvoke(new Action(() => progressBar1.Value  = m));
                                   //progressDialog.UpdateProgress(m);
                                   //progressDialog.BeginInvoke(new Action(() => progressBar1.Value = m));
                                   TimeHasChanged("Mesclando dados " + m.ToString() + "%");
                               }
                           //}
                           cont++;
                       }
                   }

               }
               TimeHasChanged("Fontes Mescladas as Mascaras, Concluido!");
               ProcessRegistry("Fontes Mescladas as Mascaras, Concluido! ");
               // String strSelectUserListBuilder = "<font  " +
               // " color=\"#0000FF\"><b><i>Title One</i></b></font><font   " +
               // " color=\"black\"><br><br>Some text here<br><br><br><font   " +
               // " color=\"#0000FF\"><b><i>Another title here   " +
               // " </i></b></font><font   " +
               // " color=\"black\"><br><br>Text1<br>Text2<br><OL><LI><DIV Style='color:green'>Pham Duy Hoa</DIV></LI><LI>how are u</LI></OL><br/>" +
               // "<table border='1'><tr><td style='color:red;text-align:right;width:20%'>123456</td><td style='color:green;width:60%'>78910</td><td style='color:red;width:20%'>ASFAFA</td></tr><tr><td style='color:red;text-align:right'>123456</td><td style='color:green;width:60%'>78910</td><td style='color:red;width:20%'>DAFSDGAFW</td></tr></table><br/>" +
               // "<div><ol><li>123456</li><li>123456</li><li>123456</li><li>123456</li></ol></div>";
               int cont3 = 0, n = 1;
               ProcessRegistry("A iniciar o Processar de arquivos  Total:"+folhas.Length.ToString());
               
               string caminho = @"c:\TEMPP\";
               if (!System.IO.Directory.Exists(caminho))
               {
                   System.IO.Directory.CreateDirectory(caminho);
               }
               else {
                   foreach (string file in Directory.GetFiles(caminho))
                       File.Delete(file);
               }

               while (cont3 < folhas.Length)
               {

                   if (codigos[cont3] != "" || codigos[cont3] != null)
                   {
                       String htmlText = folhas[cont3];

                       String filepath = caminho + codigos[cont3] + ".pdf";

                       Document document = new Document(PageSize.A4, 0, 0, 8, 0);
                       //document.
                      // document.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
                       PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@filepath, FileMode.Create));
                       document.Open();

                       TimeHasChanged("Criando Arquivo! Parte " + cont3 + "/" + folhas.Length);

                       HTMLWorker hw = new HTMLWorker(document);
                       StringReader sr = new StringReader(htmlText);
                       XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                       //hw.Parse(new StringReader(htmlText));
                       document.Close();


                       // bool arq = ex.InserirArq(Convert.ToInt32(proce), Convert.ToInt32(codigos[cont3]), caminhosalvar + codigos[cont3] + ".pdf", 1);
                       //cont3++;

                       // Double taxa = ((100 - progressBar1.Value) * cont3) / folhas.Length;

                       Double taxa = ((100 - progressBar1.Value) * cont3) / folhas.Length;
                       //Double soma = m + taxa;
                       Double soma = m + taxa;
                       // MessageBox.Show(m.ToString());
                       m = Convert.ToInt32(soma);
                       progressBar1.Value = m;

                       // Double soma = m + taxa;
                       //soma = m + soma;
                       //  m = Convert.ToInt32(soma);
                       //MessageBox.Show(m.ToString());
                       //progressBar1.BeginInvoke(new Action(() => progressBar1.Value = m));
                       // progressDialog.UpdateProgress(m);
                       //progressDialog.BeginInvoke(new Action(() => progressBar1.Value = m));
                   }
                   cont3++;
               }
               
               //Console.ReadKey();

               progressBar1.Value = 0;
               //MessageBox.Show("Processo Concluido!");
               TimeHasChanged("PROCESSO CONCLUIDO!");
               ProcessRegistry("Processo Concluido, aguardando nova chamada da fila.");
               label1.ResetText();

               if (!CopyFolderContents(@"c:\TEMPP\", caminhosalvar))
               {
                   MessageBox.Show("Falha ao reiniciar serviço!");
               }
               else {
                   foreach (string file in Directory.GetFiles(caminho))
                       File.Delete(file);
               }

               RestartService("Service13", 1000);
               StartService("Service13", 1000);
               this.Close();
               button1.Visible = true;
           }
           else {

               TimeHasChanged("Processo fechado...");
               ProcessRegistry("Processo fechado, não há lista de processo a realizar!");
               RestartService("Service13", 1000);
               this.Close();
           }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            var desktopWorkingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            this.Left = desktopWorkingArea.Right - this.Width - 5;
            this.Top = 5;// desktopWorkingArea.Bottom - this.Height;

            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            
           // if (_ls == LoadStyle.OnShown)
           //     processar_parte();
           // else if (_ls == LoadStyle.OnShownDoEvents)
          //  {
                Application.DoEvents();
                processar_parte();
          //  }
        }

        protected void TimeHasChanged(string newTime)
        {
            this.label1.Text = newTime;
            
            this.label1.Update();
        }

        public void ProcessRegistry(string name)
        {
            StreamWriter vWriter = new StreamWriter(@"c:\Audity.txt", true);
            vWriter.WriteLine("\n - " + name + " ás " + DateTime.Now.ToString());
            vWriter.Flush(); vWriter.Close();
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
               // MessageBox.Show("Falha ao Iniciar serviço!");
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
               // MessageBox.Show("Falha ao reiniciar serviço!");
            }
        }
    }
}
