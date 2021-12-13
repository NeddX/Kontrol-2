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

namespace Kontrol_2_Server
{
	public partial class ProgressBarForm : Form
	{
		public static string taskName = null;
		public static string title = null;
		public static int percentage;
		public ProgressBarForm()
		{
			InitializeComponent();
			this.Text = title;
			this.taskLabel.Text = taskName;
		}

		private void ProgressBarForm_Load(object sender, EventArgs e)
		{
			updater.Start();
		}

		private void updater_Tick(object sender, EventArgs e)
		{
			percentageLabel.Text = percentage.ToString() + "%";
			if (percentage == 100)
			{
				Thread.Sleep(2000);
				this.Close();
			}
		}

		private void ProgressBarForm_FormClosing(object sender, FormClosingEventArgs e)
		{

		}
	}
}
