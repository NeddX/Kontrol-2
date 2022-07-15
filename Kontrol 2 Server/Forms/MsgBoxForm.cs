using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Kontrol_2_Server
{
	public partial class MsgBoxForm : Form
	{
		public int clientId = 0;
		public MsgBoxForm()
		{
			InitializeComponent();
		}

		private void sendButton_Click(object sender, EventArgs e)
		{
			//display_msgbox\nThe title\n1\n1\n@{msg}Error! Blah blah\nSecond line of blah blahs
			string cmd = "display_msgbox\n" + titleBox.Text + "\n" + iconCombo.SelectedIndex + "\n" + buttonsCombo.SelectedIndex + "\n@{msg}" + msgBox.Text;
			try
			{
				MainForm.SendCommand(cmd, clientId);
			}
			/*catch (SocketException)
			{
				MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}*/
			catch (Exception)
			{
				MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}

		private void MsgBoxForm_Load(object sender, EventArgs e)
		{

		}

		private void soundCheck_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void MsgBoxForm_FormClosing(object sender, FormClosingEventArgs e)
		{

		}
	}
}
