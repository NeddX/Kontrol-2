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
	public partial class T2SForm : Form
	{
		public int clientId = 0;
		public T2SForm()
		{
			InitializeComponent();
		}

		private void sendButton_Click(object sender, EventArgs e)
		{
			MainForm.SendCommand("t2s_read\n" + msgBox.Text, clientId);
		}
	}
}
