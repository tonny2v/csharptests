using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ReflectionAndMultiThread : Form
    {
        public ReflectionAndMultiThread()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var a = Assembly.LoadFrom("ConsoleApplication1.dll");

            foreach (var i in a.GetTypes())
            {
                Debug.WriteLine(i);
            }

            Type type = a.GetType("ConsoleApplication1.MyLib");
            dynamic o = a.CreateInstance("ConsoleApplication1.MyClass2", false, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, null, null, null);
            string s = o.OutAtt();

            //foreach (var i in Enumerable.Range(1, 1000000))
            //{ }
            //Type t = typeof(Test);
            //var method = t.GetMethod("Func1", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance);
            //var instance = Activator.CreateInstance(t);
            //string x = method.Invoke(instance, null).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(method);
            t1.Start();
        }

        private void method()
        {
            Stopwatch stw = new Stopwatch();
            stw.Start();
            int n = 100000;
            foreach (int i in Enumerable.Range(1, n))
            {
                foreach (int j in Enumerable.Range(1, 10000)) { }
            }
            stw.Stop();
            MessageBox.Show(stw.ElapsedMilliseconds / 1000 + "seconds" + progressBar1.Value.ToString());
        }
    }

    public class Test { public string A1 { get; set; } public string Func1() { return " a fa "; } }
}