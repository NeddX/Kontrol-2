using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kontrol_2_Server
{
	public partial class RemoteShellForm : Form
	{
		public int clientId = 0;
		public RemoteShellForm()
		{
			InitializeComponent();
		}

		private void RemoteShellForm_Load(object sender, EventArgs e)
		{
			this.ActiveControl = textBox1;
			try
			{
				new MainForm().SendCommand("start_remoteshell", clientId);
			}
			catch (Exception)
			{

				MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				try
				{
					new MainForm().SendCommand("cmd_in\n" + textBox1.Text, clientId);
					textBox1.Text = "";
				}
				catch (Exception)
				{

					MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Close();
				}
			}
		}

		public void Append(string text)
		{
			if (this.InvokeRequired)
			{
				richTextBox1.Invoke(new MethodInvoker(delegate
				{
					richTextBox1.Text += text + "\n";
					richTextBox1.SelectionStart = richTextBox1.TextLength;
					richTextBox1.ScrollToCaret();
				}));
			}
			else
			{
				richTextBox1.Text += text + "\n";
				richTextBox1.SelectionStart = richTextBox1.TextLength;
				richTextBox1.ScrollToCaret();
			}
		}

		private void RemoteShellForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				new MainForm().SendCommand("cmd_kill", clientId);
				MainForm.rsf = new RemoteShellForm();
			}
			catch { }
		}
	}
}
