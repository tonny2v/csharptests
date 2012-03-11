using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(1000, 1000);
            Graphics g = Graphics.FromImage(b);
            foreach (var i in Enumerable.Range(1, 400))
            {
                foreach (var j in Enumerable.Range(1, 400))
                    g.FillEllipse(new SolidBrush(Color.Black), new Rectangle(i * 10, j * 10, 5, 5));
            }
            pictureBox1.Size = b.Size;
            pictureBox1.CreateGraphics().DrawImage(b, 0, 0);
        }
    }
}