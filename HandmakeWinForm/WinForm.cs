using System;
using System.Windows.Forms;
using System.Drawing;
namespace a{
public class WinForm:Form
	{
		private System.ComponentModel.IContainer components=null;
		private MenuStrip ms=null;
		public WinForm()
		{
			
			
			this.components=new System.ComponentModel.Container();
			this.ClientSize=new Size(800,600);
			this.SuspendLayout();
			
			for(int i =0;i<5;i++)
			{
				Button btn=new Button();
				btn.Size=new Size(60,30);
				btn.Location=new Point(5+60*i,50);
				btn.Text="MyBtn";
				btn.UseVisualStyleBackColor=true;
				btn.Click+=new System.EventHandler(btn_click_handler);
				this.Controls.Add(btn);
			}
			ms = new MenuStrip();
			ms.SuspendLayout();
			this.Controls.Add(ms);
			ToolStripMenuItem File=new ToolStripMenuItem("File");
			ToolStripItem Open=new ToolStripMenuItem("Open");
			ToolStripItem Save=new ToolStripMenuItem("Save");
			ToolStripItem Print=new ToolStripMenuItem("Print");
			File.DropDownItems.AddRange(new ToolStripItem[]{Open,Save,Print});
			ms.Items.Add(File);
			Open.Click+=new EventHandler(msFileOpenItem_click_handler);
			Save.Click+=new EventHandler(msFileSaveItem_click_handler);
			Print.Click+=new EventHandler(msFilePrintItem_click_handler);
			
			
			this.ResumeLayout();
			ms.ResumeLayout();
		}
		
		protected override void Dispose (bool disposing)
		{
			if(disposing&&components!=null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		
		private void btn_click_handler(object sender, EventArgs e)
		{
			MessageBox.Show("btn clicked!");
		}
		private void msFileOpenItem_click_handler(object sender,EventArgs e)
		{
			OpenFileDialog openDlg=new OpenFileDialog();
			openDlg.Filter="Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
			if (openDlg.ShowDialog()==DialogResult.OK)
				MessageBox.Show(openDlg.FileName);
		}
		
		private void msFileSaveItem_click_handler(object sender, EventArgs e)
		{
			SaveFileDialog saveDlg=new SaveFileDialog();
			saveDlg.Filter="Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
			if(saveDlg.ShowDialog()==DialogResult.OK)
			{
				MessageBox.Show(String.Format("Your File is saved to {0}",saveDlg.FileName));
			}
		}
		
		private void msFilePrintItem_click_handler(object sender, EventArgs e)
		{
			PrintDialog printDlg=new PrintDialog();
			if (printDlg.ShowDialog()==DialogResult.OK)
			{
				
			}
		}
	}
}