using System;
using System.Windows.Forms;
namespace a
{
	static class MainClass
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new WinForm());
		}
	}
	
	
}
