using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.IO;
//using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using System.Net;
using Servico_cliente.wsphp;


namespace Servico_cliente
{
    public partial class Service1 : ServiceBase
    {
        System.Threading.Timer timer1;
        private bool isProcessRunning = false;

        public Service1()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {

            timer1 = new System.Threading.Timer(new TimerCallback(Executar_tudo), null, 15000, 60000);

            



            // launch the application
              //ApplicationLoader.PROCESS_INFORMATION procInfo;

            
            
            //System.Console.WriteLine(soma);
            //System.Console.WriteLine();
              

        }

        protected override void OnStop()
        {
            StreamWriter vWriter = new StreamWriter(@"c:\Audity.txt", true);
            vWriter.WriteLine("Servico Parado: " + DateTime.Now.ToString());
            vWriter.Flush(); vWriter.Close();

        }
        private void timer1_Tick(object sender)
        {

            StreamWriter vWriter = new StreamWriter(@"c:\Audity.txt", true);
            vWriter.WriteLine("Servico Rodando: " + DateTime.Now.ToString());
            vWriter.Flush(); vWriter.Close();
        }

        private void Executar_tudo(object sender)
        {
            //ProgressDialog progressDialog = new ProgressDialog();

            //progressDialog.SetIndeterminate(true);

            // Open the dialog
            //progressDialog.ShowDialog();

           // StreamWriter vWriter = new StreamWriter(@"c:\testeServico.txt", true);
           // vWriter.WriteLine("PRINCIPALL INCIADO: " + DateTime.Now.ToString());
           // vWriter.Flush(); vWriter.Close();

            

            // Initialize the thread that will handle the background process

            // Set the flag that indicates if a process is currently running
            isProcessRunning = true;

            //progressDialog.SetIndeterminate(true);
            //Processadados();

            
            String applicationName;
            ApplicationLoader.PROCESS_INFORMATION procInfo;


            //  ProgressDialog progressDialog = new ProgressDialog();

            if (Teste_servidor("localhost", "82"))
            {

                HelloExamplePortTypeClient ex = new HelloExamplePortTypeClient();
                //string simpleResult = ex.HelloWorld("Jon");

                //var myComplexType = new WsPhp.MyComplexType { ID = 1, YourName = "Joniiiiiiii" };
                //WsPhp.MyComplexType complexResult = ex.HelloComplexWorld(myComplexType);

                // var list = new ListCourses();
                // MyComplexType2 complexResult2 = list.ListCourses("Jon");

                //Output
                //Console.WriteLine("Simple: {0}", simpleResult);
                //Console.WriteLine("Complex: {0}", complexResult.YourName);
                String processo = ex.GetProcessoOpen(0);
                //String retornoresultado = ex.GetProximo(Convert.ToInt32(processo));


                //if (lista[2].ToString()!="")
                //{
                //    applicationName = "Alerta.exe";
                //    ApplicationLoader.StartProcessAndBypassUAC(applicationName, out procInfo);
                // }

                if (Convert.ToInt32(processo) != 0)
                {
                    if (!IsProcessOpen("processar"))
                    {
                        applicationName = "processar.exe " + processo;
                        ApplicationLoader.StartProcessAndBypassUAC(applicationName, out procInfo);
                       
                        ProcessRegistry("Chamada do Processamento inciado ...");
                    }
                    else
                    {

                        if (!IsProcessOpen("Controle"))
                        {
                            applicationName = "Controle.exe";
                           // ApplicationLoader.StartProcessAndBypassUAC(applicationName, out procInfo);

                            ProcessRegistry("Processo ja  esta em andamento, e outro processo está aguardando na fila.");

                        } else if (!IsProcessOpen("Alerta"))
                        {
                            applicationName = "Alerta.exe 4";
                            //ApplicationLoader.StartProcessAndBypassUAC(applicationName, out procInfo);

                            ProcessRegistry("Processo ja  esta em andamento, e outro processo está aguardando na fila.");
                        }
                    }
                }
                else {

                    if (!IsProcessOpen("Alerta"))
                    {
                        applicationName = "Alerta.exe 2";
                        //ApplicationLoader.StartProcessAndBypassUAC(applicationName, out procInfo);

                        ProcessRegistry(" Processos fechados, Patrulheiro aguardando abertura de um novo processo. ");
                    }
                }
           
            }
            // Show a dialog box that confirms the process has completed
           // MessageBox.Show("Thread completed!");

            // Close the dialog if it hasn't been already
           // if (progressDialog.InvokeRequired)
           //     progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));

            // Reset the flag that indicates if a process is currently running
            isProcessRunning = false;


        }

        private void Processadados()
        {

            // ProgressDialog progressDialog = new ProgressDialog();
            // progressDialog.SetIndeterminate(true);
            // progressDialog.ShowDialog();


            String strSelectUserListBuilder = "<style type='text/css'>td img {display: block;}</style><table style='display: inline-table; ' border='0' cellpadding='0' cellspacing='0' width='795'><tr><td><img src='http://localhost:82/dados/images/spacer.gif' width='311' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='141' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='6' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='92' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='245' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r1_c1' src='http://localhost:82/dados/images/ficha1_r1_c1.jpg' width='795' height='64' border='0' id='ficha1_r1_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='64' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r2_c1' src='http://localhost:82/dados/images/ficha1_r2_c1.jpg' width='795' height='8' border='0' id='ficha1_r2_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='8' border='0' alt='' /></td></tr><tr><td rowspan='2' colspan='3'><img name='ficha1_r3_c1' src='http://localhost:82/dados/images/ficha1_r3_c1.jpg' width='458' height='31' border='0' id='ficha1_r3_c1' alt='' /></td><td bgcolor='#8DB3E2' style='background:#8DB3E2'><b><font color='#FFF'>%numero%</font></b></td><td rowspan='2'><img name='ficha1_r3_c5' src='http://localhost:82/dados/images/ficha1_r3_c5.jpg' width='245' height='31' border='0' id='ficha1_r3_c5' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='28' border='0' alt='' /></td></tr><tr><td><img name='ficha1_r4_c4' src='http://localhost:82/dados/images/ficha1_r4_c4.jpg' width='92' height='3' border='0' id='ficha1_r4_c4' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r5_c1' src='http://localhost:82/dados/images/ficha1_r5_c1.jpg' width='795' height='4' border='0' id='ficha1_r5_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r6_c1' src='http://localhost:82/dados/images/ficha1_r6_c1.jpg' width='795' height='36' border='0' id='ficha1_r6_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='36' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r7_c1' src='http://localhost:82/dados/images/ficha1_r7_c1.jpg' width='795' height='11' border='0' id='ficha1_r7_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='11' border='0' alt='' /></td></tr><tr><td rowspan='2'><img name='ficha1_r8_c1' src='http://localhost:82/dados/images/ficha1_r8_c1.jpg' width='311' height='37' border='0' id='ficha1_r8_c1' alt='' /></td><td><b><font color='#1D0BFF'>%codigo%</font></b></td><td rowspan='2' colspan='3'><img name='ficha1_r8_c3' src='http://localhost:82/dados/images/ficha1_r8_c3.jpg' width='343' height='37' border='0' id='ficha1_r8_c3' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='30' border='0' alt='' /></td></tr><tr><td><img name='ficha1_r9_c2' src='http://localhost:82/dados/images/ficha1_r9_c2.jpg' width='141' height='7' border='0' id='ficha1_r9_c2' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='7' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1;' ><table width='100%' border='0'><tr><td width='88' height='76'>&nbsp;</td><td width='697'><font color='#1D0BFF'>%descricao%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='84' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r11_c1' src='http://localhost:82/dados/images/ficha1_r11_c1.jpg' width='795' height='3' border='0' id='ficha1_r11_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r12_c1' src='http://localhost:82/dados/images/ficha1_r12_c1.jpg' width='795' height='41' border='0' id='ficha1_r12_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r13_c1' src='http://localhost:82/dados/images/ficha1_r13_c1.jpg' width='795' height='29' border='0' id='ficha1_r13_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='29' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1' ><table width='100%' border='0'><tr><td width='88' height='29'>&nbsp;</td><td width='697'><font color='#1D0BFF'>%ncm%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='34' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r15_c1' src='http://localhost:82/dados/images/ficha1_r15_c1.jpg' width='795' height='3' border='0' id='ficha1_r15_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r16_c1' src='http://localhost:82/dados/images/ficha1_r16_c1.jpg' width='795' height='41' border='0' id='ficha1_r16_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r17_c1' src='http://localhost:82/dados/images/ficha1_r17_c1.jpg' width='795' height='24' border='0' id='ficha1_r17_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='24' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1' ><table width='100%' border='0'><tr><td width='88' height='29'>&nbsp;</td><td width='697'><font color='#1D0BFF'>%fornecedor%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='35' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r19_c1' src='http://localhost:82/dados/images/ficha1_r19_c1.jpg' width='795' height='4' border='0' id='ficha1_r19_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r20_c1' src='http://localhost:82/dados/images/ficha1_r20_c1.jpg' width='795' height='40' border='0' id='ficha1_r20_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r21_c1' src='http://localhost:82/dados/images/ficha1_r21_c1.jpg' width='795' height='25' border='0' id='ficha1_r21_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='25' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1'><table width='100%' border='0'><tr><td width='88' height='29'>&nbsp;</td><td width='697'><font color='#1D0BFF'>%composicao%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='36' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r23_c1' src='http://localhost:82/dados/images/ficha1_r23_c1.jpg' width='795' height='4' border='0' id='ficha1_r23_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r24_c1' src='http://localhost:82/dados/images/ficha1_r24_c1.jpg' width='795' height='40' border='0' id='ficha1_r24_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r25_c1' src='http://localhost:82/dados/images/ficha1_r25_c1.jpg' width='795' height='51' border='0' id='ficha1_r25_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='51' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1'><table width='100%' border='0'><tr><td width='88' height='35'>&nbsp;</td><td width='697'><font color='#1D0BFF'>%areadeaplicacao%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r27_c1' src='http://localhost:82/dados/images/ficha1_r27_c1.jpg' width='795' height='3' border='0' id='ficha1_r27_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r28_c1' src='http://localhost:82/dados/images/ficha1_r28_c1.jpg' width='795' height='37' border='0' id='ficha1_r28_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='37' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r29_c1' src='http://localhost:82/dados/images/ficha1_r29_c1.jpg' width='795' height='54' border='0' id='ficha1_r29_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='54' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1'><table width='100%' border='0'><tr><td width='88' height='38'>&nbsp;</td><td width='697'><font color='#1D0BFF'>%equipamento%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='44' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r31_c1' src='http://localhost:82/dados/images/ficha1_r31_c1.jpg' width='795' height='3' border='0' id='ficha1_r31_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r32_c1' src='http://localhost:82/dados/images/ficha1_r32_c1.jpg' width='795' height='64' border='0' id='ficha1_r32_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='64' border='0' alt='' /></td></tr><tr><td colspan='5'><img name='ficha1_r33_c1' src='http://localhost:82/dados/images/ficha1_r33_c1.jpg' width='795' height='3' border='0' id='ficha1_r33_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='5' valign='top' bgcolor='#C7D9F1' style='background:#C7D9F1'><table width='100%' border='0'><tr><td width='88' height='167'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%funcao%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='174' border='0' alt='' /></td></tr></table>";
            // strSelectUserListBuilder = strSelectUserListBuilder + 
            strSelectUserListBuilder = strSelectUserListBuilder + "<table style='display: inline-table;' border='0' cellpadding='0' cellspacing='0' width='795'><tr><td><img src='http://localhost:82/dados/images/spacer.gif' width='83' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='3' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='3' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='2' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='18' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='2' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='2' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='344' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='18' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='5' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='19' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='6' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='47' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='238' height='1' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' /></td></tr><tr><td colspan='19'><img name='ficha02_r1_c1' src='http://localhost:82/dados/images/ficha02_r1_c1.jpg' width='795' height='69' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='69' border='0' /></td></tr><tr><td colspan='19'><img name='ficha02_r2_c1' src='http://localhost:82/dados/images/ficha02_r2_c1.jpg' width='795' height='6' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='6' border='0' /></td></tr><tr><td rowspan='2' colspan='11'><img name='ficha02_r3_c1' src='http://localhost:82/dados/images/ficha02_r3_c1.jpg' width='460' height='35' border='0'/></td><td colspan='7' style='background:#8DB3E2'><b><font color='#FFF'>%numero%</font></b></td><td rowspan='2'><img src='http://localhost:82/dados/images/ficha02_r3_c19.jpg' width='238' height='35' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='31' border='0' /></td></tr><tr><td colspan='7'><img src='http://localhost:82/dados/images/ficha02_r4_c12.jpg' width='97' height='4' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img name='ficha02_r6_c1' src='http://localhost:82/dados/images/ficha02_r6_c1.jpg' width='795' height='32' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='32' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img name='ficha02_r8_c1' src='http://localhost:82/dados/images/ficha02_r8_c1.jpg' width='795' height='5' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='5' border='0' /></td></tr><tr><td rowspan='3' colspan='4'><img name='ficha02_r9_c1' src='http://localhost:82/dados/images/ficha02_r9_c1.jpg' width='91' height='150' border='0'/></td><td colspan='5' rowspan='2' valign='top' style='background:#C7D9F1'><table width='100%' height='146' border='0'><tr><td width='14' height='21' align='center'><font color='#1D0BFF'>%contato_1%</font></td></tr><tr><td height='17' align='center'></td></tr><tr><td height='17' align='center'><font color='#1D0BFF'>%contato_2%</font></td></tr><tr><td height='21' align='center'></td></tr><tr><td height='17' align='center'><font color='#1D0BFF'>%contato_3%</font></td></tr><tr><td height='11' align='center'></td></tr><tr><td height='22' align='center'><font color='#1D0BFF'>%contato_4%</font></td></tr></table></td><td rowspan='3' colspan='4'><img src='http://localhost:82/dados/images/ficha02_r9_c10.jpg' width='368' height='150' border='0'/></td><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr><td height='24'><font color='#1D0BFF'>%contato_5%</font></td></tr><tr><td height='19'></td></tr><tr><td height='21'><font color='#1D0BFF'>%contato_6%</font></td></tr><tr><td height='15'></td></tr><tr><td height='19'><font color='#1D0BFF'>%contato_7%</font></td></tr></table></td><td rowspan='3' colspan='3'><img src='http://localhost:82/dados/images/ficha02_r9_c17.jpg' width='286' height='150' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='110' border='0' /></td></tr><tr><td rowspan='2' colspan='3'><img src='http://localhost:82/dados/images/ficha02_r10_c14.jpg' width='26' height='40' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='38' border='0' /></td></tr><tr><td colspan='5'><img src='http://localhost:82/dados/images/ficha02_r11_c5.jpg' width='24' height='2' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='2' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r13_c1.jpg' width='795' height='41' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r15_c1.jpg' width='795' height='6' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='6' border='0' /></td></tr><tr><td rowspan='3' colspan='5'><img src='http://localhost:82/dados/images/ficha02_r16_c1.jpg' width='92' height='116' border='0'/></td><td colspan='5' rowspan='2' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr><td height='24'><font color='#1D0BFF'>%vidautil_1%</font></td></tr><tr><td height='16'></td></tr><tr><td height='21'><font color='#1D0BFF'>%vidautil_2%</font></td></tr><tr><td height='15'></td></tr><tr><td height='19'><font color='#1D0BFF'>%vidautil_3%</font></td></tr></table></td><td rowspan='3' colspan='4'><img src='http://localhost:82/dados/images/ficha02_r16_c11.jpg' width='368' height='116' border='0'/></td><td colspan='3' style='background:#C7D9F1'><table width='100%' border='0'><tr><td><font color='#1D0BFF'>%vidautil_4%</font></td></tr><tr><td height='13'></td></tr><tr><td><font color='#1D0BFF'>%vidautil_5%</font></td></tr></table></td><td rowspan='3' colspan='2'><img src='http://localhost:82/dados/images/ficha02_r16_c18.jpg' width='285' height='116' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='72' border='0' /></td></tr><tr><td rowspan='2' colspan='3'><img src='http://localhost:82/dados/images/ficha02_r17_c15.jpg' width='26' height='44' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='36' border='0' /></td></tr><tr><td colspan='5'><img src='http://localhost:82/dados/images/ficha02_r18_c6.jpg' width='24' height='8' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='8' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r20_c1.jpg' width='795' height='41' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r22_c1.jpg' width='795' height='7' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='7' border='0' /></td></tr><tr><td rowspan='2' colspan='3'><img src='http://localhost:82/dados/images/ficha02_r23_c1.jpg' width='89' height='109' border='0'/></td><td colspan='5' style='background:#C7D9F1'><table width='100%' border='0'><tr><td height='20'><font color='#1D0BFF'>%estado_1%</font></td></tr><tr><td height='19'></td></tr><tr><td height='18'><font color='#1D0BFF'>%estado_2%</font></td></tr><tr><td height='15'></td></tr><tr><td height='19'><font color='#1D0BFF'>%estado_3%</font></td></tr></table></td><td rowspan='2' colspan='11'><img src='http://localhost:82/dados/images/ficha02_r23_c9.jpg' width='682' height='109' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='104' border='0' /></td></tr><tr><td colspan='5'><img src='http://localhost:82/dados/images/ficha02_r24_c4.jpg' width='24' height='5' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='5' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r26_c1.jpg' width='795' height='36' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='36' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r28_c1.jpg' width='795' height='9' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='9' border='0' /></td></tr><tr><td rowspan='3' colspan='2'><img src='http://localhost:82/dados/images/ficha02_r29_c1.jpg' width='86' height='105' border='0'/></td><td colspan='5' rowspan='2' style='background:#C7D9F1'><table width='100%' border='0'><tr><td height='20'><font color='#1D0BFF'>%atividade_1%</font></td></tr><tr><td height='19'></td></tr><tr><td height='18'><font color='#1D0BFF'>%atividade_2%</font></td></tr><tr><td height='15'></td></tr><tr><td height='19'><font color='#1D0BFF'>%atividade_3%</font></td></tr></table></td><td rowspan='3' colspan='5'><img src='http://localhost:82/dados/images/ficha02_r29_c8.jpg' width='367' height='105' border='0'/></td><td colspan='3' style='background:#C7D9F1'><table width='100%' border='0'><tr><td><font color='#1D0BFF'>%atividade_4%</font></td></tr><tr><td height='13'></td></tr><tr><td><font color='#1D0BFF'>%atividade_5%</font></td></tr></table></td><td rowspan='3' colspan='4'><img src='http://localhost:82/dados/images/ficha02_r29_c16.jpg' width='292' height='105' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='64' border='0' /></td></tr><tr><td rowspan='2' colspan='3'><img src='http://localhost:82/dados/images/ficha02_r30_c13.jpg' width='25' height='41' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='39' border='0' /></td></tr><tr><td colspan='5'><img src='http://localhost:82/dados/images/ficha02_r31_c3.jpg' width='25' height='2' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='2' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r33_c1.jpg' width='795' height='40' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r35_c1.jpg' width='795' height='4' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' /></td></tr><tr><td rowspan='2'><img src='http://localhost:82/dados/images/ficha02_r36_c1.jpg' width='83' height='93' border='0'/></td><td colspan='5' style='background:#C7D9F1' ><table width='100%' border='0'><tr><td height='20'><font color='#1D0BFF'>%ambiente_1%</font></td></tr><tr><td height='11'></td></tr><tr><td height='18'><font color='#1D0BFF'>%ambiente_2%</font></td></tr><tr><td height='9'></td></tr><tr><td height='19'><font color='#1D0BFF'>%ambiente_3%</font></td></tr></table></td><td rowspan='2' colspan='13'><img src='http://localhost:82/dados/images/ficha02_r36_c7.jpg' width='685' height='93' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='91' border='0' /></td></tr><tr><td colspan='5'><img src='http://localhost:82/dados/images/ficha02_r37_c2.jpg' width='27' height='2' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='2' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r39_c1.jpg' width='795' height='40' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' /></td></tr><tr><td colspan='19'><img src='http://localhost:82/dados/images/ficha02_r40_c1.jpg' width='795' height='28' border='0'/></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='28' border='0' /></td></tr><tr><td colspan='19' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr><td width='88' height='98'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%complementar%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='104' border='0' /></td></tr></table>";
            //String htmlText = strSelectUserListBuilder.ToString();
            strSelectUserListBuilder = strSelectUserListBuilder + "<table style='display: inline-table;' border='0' cellpadding='0' cellspacing='0' width='795'><tr><td><img src='http://localhost:82/dados/images/spacer.gif' width='460' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='80' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='255' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r1_c1' src='http://localhost:82/dados/images/ficha3_r1_c1.jpg' width='795' height='66' border='0' id='ficha3_r1_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='66' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r2_c1' src='http://localhost:82/dados/images/ficha3_r2_c1.jpg' width='795' height='7' border='0' id='ficha3_r2_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='7' border='0' alt='' /></td></tr><tr><td rowspan='2'><img name='ficha3_r3_c1' src='http://localhost:82/dados/images/ficha3_r3_c1.jpg' width='460' height='32' border='0' id='ficha3_r3_c1' alt='' /></td><td style=' background:#8DB3E2'><b><font color='#FFF'>%numero%</font></b></td><td rowspan='2'><img name='ficha3_r3_c3' src='http://localhost:82/dados/images/ficha3_r3_c3.jpg' width='255' height='32' border='0' id='ficha3_r3_c3' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='27' border='0' alt='' /></td></tr><tr><td><img name='ficha3_r4_c2' src='http://localhost:82/dados/images/ficha3_r4_c2.jpg' width='80' height='5' border='0' id='ficha3_r4_c2' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='5' border='0' alt='' /></td></tr><tr><td colspan='3'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r6_c1' src='http://localhost:82/dados/images/ficha3_r6_c1.jpg' width='795' height='41' border='0' id='ficha3_r6_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r7_c1' src='http://localhost:82/dados/images/ficha3_r7_c1.jpg' width='795' height='40' border='0' id='ficha3_r7_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <font color='#1D0BFF'>%classificao%</font></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='19' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r9_c1' src='http://localhost:82/dados/images/ficha3_r9_c1.jpg' width='795' height='23' border='0' id='ficha3_r9_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='23' border='0' alt='' /></td></tr><tr> <td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr><td width='88' height='47'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%contatofisicomateria%</font></td></tr> </table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='54' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r11_c1' src='http://localhost:82/dados/images/ficha3_r11_c1.jpg' width='795' height='24' border='0' id='ficha3_r11_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='24' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr> <td width='88' height='47'>&nbsp;</td> <td width='697' valign='top'><font color='#1D0BFF'>%consumoimediato%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='54' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r13_c1' src='http://localhost:82/dados/images/ficha3_r13_c1.jpg' width='795' height='27' border='0' id='ficha3_r13_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='27' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='47'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%materialintegra%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='55' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r15_c1' src='http://localhost:82/dados/images/ficha3_r15_c1.jpg' width='795' height='54' border='0' id='ficha3_r15_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='54' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='47'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%essencial%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='52' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r17_c1' src='http://localhost:82/dados/images/ficha3_r17_c1.jpg' width='795' height='26' border='0' id='ficha3_r17_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='26' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='50'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%individual%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='56' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r19_c1' src='http://localhost:82/dados/images/ficha3_r19_c1.jpg' width='795' height='24' border='0' id='ficha3_r19_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='24' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='39'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%ferramenta%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='46' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r21_c1' src='http://localhost:82/dados/images/ficha3_r21_c1.jpg' width='795' height='26' border='0' id='ficha3_r21_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='26' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='39'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%partemaquina%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='46' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r23_c1' src='http://localhost:82/dados/images/ficha3_r23_c1.jpg' width='795' height='32' border='0' id='ficha3_r23_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='32' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='39'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%recuperacao%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='49' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r25_c1' src='http://localhost:82/dados/images/ficha3_r25_c1.jpg' width='795' height='25' border='0' id='ficha3_r25_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='25' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='45'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%desgaste%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='51' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r27_c1' src='http://localhost:82/dados/images/ficha3_r27_c1.jpg' width='795' height='52' border='0' id='ficha3_r27_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='52' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='28'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%exaustacao%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='36' border='0' alt='' /></td></tr><tr><td colspan='3'><img name='ficha3_r29_c1' src='http://localhost:82/dados/images/ficha3_r29_c1.jpg' width='795' height='23' border='0' id='ficha3_r29_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='23' border='0' alt='' /></td></tr><tr><td colspan='3' valign='top' style='background:#C7D9F1'><table width='100%' border='0'> <tr><td width='88' height='60'>&nbsp;</td><td width='697' valign='top'><font color='#1D0BFF'>%consumonoprocesso%</font></td> </tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='66' border='0' alt='' /></td></tr></table>";

            strSelectUserListBuilder = strSelectUserListBuilder + "<table style='display: inline-table;' border='0' cellpadding='0' cellspacing='0' width='795'><tr><td><img src='http://localhost:82/dados/images/spacer.gif' width='92' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='3' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='19' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='3' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='344' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='89' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='245' height='1' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='1' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r1_c1' src='http://localhost:82/dados/images/ficha4_r1_c1.jpg' width='795' height='71' border='0' id='ficha4_r1_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='71' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r2_c1' src='http://localhost:82/dados/images/ficha4_r2_c1.jpg' width='795' height='7' border='0' id='ficha4_r2_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='7' border='0' alt='' /></td></tr><tr><td rowspan='2' colspan='5'><img name='ficha4_r3_c1' src='http://localhost:82/dados/images/ficha4_r3_c1.jpg' width='461' height='34' border='0' id='ficha4_r3_c1' alt='' /></td><td style='background:#8DB3E2'><b><font color='#FFF'>%numero%</font></b></td><td rowspan='2'><img name='ficha4_r3_c7' src='http://localhost:82/dados/images/ficha4_r3_c7.jpg' width='245' height='34' border='0' id='ficha4_r3_c7' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='30' border='0' alt='' /></td></tr><tr><td><img name='ficha4_r4_c6' src='http://localhost:82/dados/images/ficha4_r4_c6.jpg' width='89' height='4' border='0' id='ficha4_r4_c6' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' alt='' /></td></tr><tr><td colspan='7'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='3' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='3' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r6_c1' src='http://localhost:82/dados/images/ficha4_r6_c1.jpg' width='795' height='40' border='0' id='ficha4_r6_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r7_c1' src='http://localhost:82/dados/images/ficha4_r7_c1.jpg' width='795' height='28' border='0' id='ficha4_r7_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='28' border='0' alt='' /></td></tr><tr><td colspan='7' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr> <td width='88' height='98'>&nbsp;</td> <td width='697' valign='top'><font color='#1D0BFF'>%classificacaomaterial%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='104' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r9_c1' src='http://localhost:82/dados/images/ficha4_r9_c1.jpg' width='795' height='41' border='0' id='ficha4_r9_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='41' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r10_c1' src='http://localhost:82/dados/images/ficha4_r10_c1.jpg' width='795' height='18' border='0' id='ficha4_r10_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='18' border='0' alt='' /></td></tr><tr><td rowspan='12'><img name='ficha4_r11_c1' src='http://localhost:82/dados/images/ficha4_r11_c1.jpg' width='92' height='452' border='0' id='ficha4_r11_c1' alt='' /></td><td colspan='2' style='background:#C7D9F1'><font color='#1D0BFF'>%insumodoproduto%</font></td><td rowspan='6' colspan='4'><img name='ficha4_r11_c4' src='http://localhost:82/dados/images/ficha4_r11_c4.jpg' width='681' height='237' border='0' id='ficha4_r11_c4' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='23' border='0' alt='' /></td></tr><tr><td colspan='2'><img name='ficha4_r12_c2' src='http://localhost:82/dados/images/ficha4_r12_c2.jpg' width='22' height='31' border='0' id='ficha4_r12_c2' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='31' border='0' alt='' /></td></tr><tr><td colspan='2' style='background:#C7D9F1'><font color='#1D0BFF'>%insumodoprocesso%</font></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='23' border='0' alt='' /></td></tr><tr><td colspan='2'><img name='ficha4_r14_c2' src='http://localhost:82/dados/images/ficha4_r14_c2.jpg' width='22' height='61' border='0' id='ficha4_r14_c2' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='61' border='0' alt='' /></td></tr><tr><td colspan='2' style='background:#C7D9F1'><font color='#1D0BFF'>%intermediario%</font></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='22' border='0' alt='' /></td></tr><tr><td colspan='2'><img name='ficha4_r16_c2' src='http://localhost:82/dados/images/ficha4_r16_c2.jpg' width='22' height='77' border='0' id='ficha4_r16_c2' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='77' border='0' alt='' /></td></tr><tr><td rowspan='6'><img name='ficha4_r17_c2' src='http://localhost:82/dados/images/ficha4_r17_c2.jpg' width='3' height='215' border='0' id='ficha4_r17_c2' alt='' /></td><td colspan='2' style='background:#C7D9F1'><font color='#1D0BFF'>%ativo%</font></td><td rowspan='6' colspan='3'><img name='ficha4_r17_c5' src='http://localhost:82/dados/images/ficha4_r17_c5.jpg' width='678' height='215' border='0' id='ficha4_r17_c5' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='21' border='0' alt='' /></td></tr><tr><td colspan='2'><img name='ficha4_r18_c3' src='http://localhost:82/dados/images/ficha4_r18_c3.jpg' width='22' height='47' border='0' id='ficha4_r18_c3' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='47' border='0' alt='' /></td></tr><tr><td colspan='2' style='background:#C7D9F1'><font color='#1D0BFF'>%embalagem%</font></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='23' border='0' alt='' /></td></tr><tr><td colspan='2'><img name='ficha4_r20_c3' src='http://localhost:82/dados/images/ficha4_r20_c3.jpg' width='22' height='51' border='0' id='ficha4_r20_c3' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='51' border='0' alt='' /></td></tr><tr><td colspan='2' style='background:#C7D9F1'><font color='#1D0BFF'>%usoconsumo%</font></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='23' border='0' alt='' /></td></tr><tr><td colspan='2'><img name='ficha4_r22_c3' src='http://localhost:82/dados/images/ficha4_r22_c3.jpg' width='22' height='50' border='0' id='ficha4_r22_c3' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='50' border='0' alt='' /></td></tr><tr><td colspan='7'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='4' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' alt='' /></td></tr><tr><td colspan='7'><img name='ficha4_r24_c1' src='http://localhost:82/dados/images/ficha4_r24_c1.jpg' width='795' height='40' border='0' id='ficha4_r24_c1' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='40' border='0' alt='' /></td></tr><tr><td colspan='7'><img src='http://localhost:82/dados/images/spacer.gif' width='795' height='4' border='0' alt='' /></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='4' border='0' alt='' /></td></tr><tr><td colspan='7' valign='top' style='background:#C7D9F1'><table width='100%' border='0'><tr> <td width='88' height='255'>&nbsp;</td> <td width='697' valign='top'><font color='#1D0BFF'>%adcionais%</font></td></tr></table></td><td><img src='http://localhost:82/dados/images/spacer.gif' width='1' height='263' border='0' alt='' /></td></tr></table>";
            //CreatePDFFromHTMLFile(htmlText , pdfFileName);

            String[] folhas = new String[5];
            int[] folhas_num = new int[5];

            var input = "1 = %numero% ; 2 = %codigo% | 5 = %ncm%\r\n" +
                        "1 = %numero% >> x56 = five >> string six\r\n" +
                        "xx1 = seven >> xx11 = eight || xx111 = nine\r\n ";

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
            int m = 1;
            //progressBar1.BeginInvoke(new Action(() => progressBar1.Value = m));
            //progressBar1.Step = 1;

            //progressDialog.UpdateProgress(m);
            //progressDialog.BeginInvoke(new Action(() => progressBar1.Value = m));

            List<string> values = new List<string>();

            string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\UNIUBE\\2013-2\\Nova pasta\\_REVISÃO PLANILHA DE CLASSIFICAÇÃO DOS MATERIAIS.xlsb;Extended Properties=\"Excel 12.0 Xml;HDR=NO;\"";
            using (OleDbConnection conn = new OleDbConnection(constr))
            {


                OleDbCommand command = new OleDbCommand("Select * from [REVISAO$a2:al6]", conn);
                //SELECT NAME, TELEFONE, DATA FROM   [sheet1$a1:q633] WHERE  NAME IN (SELECT * FROM  [sheet2$a1:a2])
                conn.Open();

                OleDbDataReader reader = command.ExecuteReader();

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
                        foreach (Match item in matches)
                        {
                            //sb.AppendLine(String.Format(template,
                            //  counter++,

                            if (counter == 0)
                            {
                                Console.WriteLine(item.Groups[1].ToString().Trim());
                                //Console.WriteLine(item.Groups[2].ToString().Trim());
                                //Console.WriteLine(item.Groups[3].ToString().Trim());

                                int teste = int.Parse(item.Groups[1].ToString().Trim());
                                string value = reader[teste].ToString();
                                //Console.WriteLine(item.Groups[2].ToString().Trim() + " -- -- - - " + value + "cooooontt "+ cont);
                                folhas[cont] = strSelectUserListBuilder.Replace(item.Groups[2].ToString().Trim(), value);
                                folhas_num[cont] = cont;
                            }
                            else
                            {
                                Console.WriteLine(item.Groups[1].ToString().Trim());
                                //Console.WriteLine(item.Groups[2].ToString().Trim());
                                //Console.WriteLine(item.Groups[3].ToString().Trim());

                                int teste = int.Parse(item.Groups[1].ToString().Trim());
                                string value = reader[teste].ToString();
                                //Console.WriteLine(item.Groups[2].ToString().Trim() + " -- -- - - " + value + "cooooontt "+ cont);
                                folhas[cont] = folhas[cont].Replace(item.Groups[2].ToString().Trim(), value);
                                folhas_num[cont] = cont;
                            }
                            counter++;
                            //Double taxa = ((50 - progressBar1.Value) * counter) / item.Length;
                            //Double soma = m + taxa;
                            //soma = m + soma;
                            //m = Convert.ToInt32(soma);

                            //m = m + (counter*1);
                            //progressBar1.BeginInvoke(new Action(() => progressBar1.Value  = m));
                            //progressDialog.UpdateProgress(m);
                            //progressDialog.BeginInvoke(new Action(() => progressBar1.Value = m));

                        }
                        cont++;
                    }
                }

            }

            // String strSelectUserListBuilder = "<font  " +
            // " color=\"#0000FF\"><b><i>Title One</i></b></font><font   " +
            // " color=\"black\"><br><br>Some text here<br><br><br><font   " +
            // " color=\"#0000FF\"><b><i>Another title here   " +
            // " </i></b></font><font   " +
            // " color=\"black\"><br><br>Text1<br>Text2<br><OL><LI><DIV Style='color:green'>Pham Duy Hoa</DIV></LI><LI>how are u</LI></OL><br/>" +
            // "<table border='1'><tr><td style='color:red;text-align:right;width:20%'>123456</td><td style='color:green;width:60%'>78910</td><td style='color:red;width:20%'>ASFAFA</td></tr><tr><td style='color:red;text-align:right'>123456</td><td style='color:green;width:60%'>78910</td><td style='color:red;width:20%'>DAFSDGAFW</td></tr></table><br/>" +
            // "<div><ol><li>123456</li><li>123456</li><li>123456</li><li>123456</li></ol></div>";
            int cont3 = 0, n = 1;
            while (cont3 < folhas.Length)
            {
                String htmlText = folhas[cont3];
                String filepath = "C:\\test" + cont3 + ".pdf";

                Document document = new Document(PageSize.A4, 0, 0, 8, 0);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@filepath, FileMode.Create));
                document.Open();
                HTMLWorker hw = new HTMLWorker(document);
                StringReader sr = new StringReader(htmlText);
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                //hw.Parse(new StringReader(htmlText));
                document.Close();
                cont3++;
                //Double taxa = ((100 - progressBar1.Value) * cont3) / folhas.Length;

                //Double soma = m + taxa;
                //soma = m + soma;
                //m = Convert.ToInt32(soma);
                //MessageBox.Show(m.ToString());
                //progressBar1.BeginInvoke(new Action(() => progressBar1.Value = m));
                // progressDialog.UpdateProgress(m);
                //progressDialog.BeginInvoke(new Action(() => progressBar1.Value = m));
            }
            //Console.ReadKey();


        }

        private bool Teste_servidor(String ip, String port) {

            string command = "teste_on";
            string qString = string.Empty;
            System.Uri serverpage1 = new System.Uri("http://"+ip+":"+port+"/interop.php");

            HttpWebRequest qRequest = (HttpWebRequest)HttpWebRequest.Create(serverpage1.AbsoluteUri + "?q=" + command);
            qRequest.Method = "GET";
            qRequest.UserAgent = "MyAppName/1.1 (Instruction Request)";

            using (HttpWebResponse qResponse = (HttpWebResponse)qRequest.GetResponse())
                if (qResponse.StatusCode == HttpStatusCode.OK)
                    using (System.IO.StreamReader qReader = new System.IO.StreamReader(qResponse.GetResponseStream()))
                        qString = qReader.ReadToEnd().Trim(); ;
            if (qString == "Servidor_Online")
            {
                return true;
            }
            else {
                return false;

            }
            
            //MessageBox.Show(QueryServer("get_instructions", new Uri("http://localhost/interop.php")));
           // Console.WriteLine(qString);

            //string test = QueryServer("get_instructions", "http://localhost:84/interop.php");
            
        }

        
        public bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses()) {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }

        public void ProcessRegistry(string name)
        {
            StreamWriter vWriter = new StreamWriter(@"c:\Audity.txt", true);
            vWriter.WriteLine( name + " ás " + DateTime.Now.ToString());
            vWriter.Flush(); vWriter.Close();
        }
    }
}
