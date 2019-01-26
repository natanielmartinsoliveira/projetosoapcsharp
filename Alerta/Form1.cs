using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alerta
{
    public partial class Form1 : Form
    {
        private string _processo;
        
        public Form1(string processo)
        {
            InitializeComponent();
            _processo = processo;
            if (_processo == "1")
            {
            TimeHasChanged("Há Processos na fila de processos...");
            }
            if (_processo == "2")
            {
                TimeHasChanged("Processo Fechado...");
            }
            if (_processo == "3")
            {
                TimeHasChanged("Erro ao inciar o processo...");
            }
            if (_processo == "4")
            {
                TimeHasChanged("Janela de processo.. Aberta");
            }
            if (_processo == "5")
            {
                TimeHasChanged("Processo local em Execução");
            }
        }

        protected void TimeHasChanged(string newTime)
        {
            this.label1.Text = newTime;
            this.label1.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
