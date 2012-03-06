using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Npgsql;

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

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            using (NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=postgres;Password=wangyiqi;Database=postgis2;"))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("select * from road order by gid", conn);
                
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds);
            }
            listBox1.DataSource = ds.Tables[0];
            listBox1.DisplayMember = "gid";
            listBox1.ValueMember = "length";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(listBox1.SelectedValue.ToString());
        }
    }
}
