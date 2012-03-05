using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TestInWinApp
{
    public partial class TestPanelAnimation : Form
    {
        public TestPanelAnimation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            Rectangle r = new Rectangle(0, 0, 20, 20);
            g.DrawRectangle(new Pen(Color.Red), r);
            while (r.Y < 300)
            {
                g.FillRectangle(new SolidBrush(this.BackColor), r);
                r.Y += 1;
                g.FillRectangle(new SolidBrush(Color.Red), r);
                Thread.Sleep(50);
            }
        

        }
    }
}
