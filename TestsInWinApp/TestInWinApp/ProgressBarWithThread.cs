using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace TestInWinApp
{
    public partial class ProgressBarWithThread : Form
    {
        Stopwatch stw;
            
        public ProgressBarWithThread()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stw = new Stopwatch();
            stw.Start();
            
         
            backgroundWorker1.RunWorkerAsync();
            button1.Enabled = false;
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = progressBar1.Minimum; i <= progressBar1.Maximum; i++)
            {
                for (int j = 0; j < 8000000; j++) { }
                backgroundWorker1.ReportProgress(i);
                //注意一定要用百分比！！！进度条最大值需要为100，否则无法异步操作
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            stw.Stop();
            MessageBox.Show(stw.ElapsedMilliseconds.ToString());
            MessageBox.Show("Completed");
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("a");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stopwatch stw = new Stopwatch();
            stw.Start();
            
            for (int i = progressBar2.Minimum; i <= progressBar2.Maximum; i++)
            {
                for (int j = 0; j < 8000000; j++) { }
                //Thread.Sleep(50);
                //注意一定要用百分比！！！进度条最大值需要为100，否则无法异步操作
                progressBar2.Value=i;
                Application.DoEvents();
                
            }
            stw.Stop();
            MessageBox.Show(stw.ElapsedMilliseconds.ToString());
        }
    }
}
