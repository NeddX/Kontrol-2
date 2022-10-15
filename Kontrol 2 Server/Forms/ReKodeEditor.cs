using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Text.RegularExpressions;

namespace Kontrol_2_Server
{
	public partial class ReKodeEditor : Form
	{
		public int clientId = -1;
		public string assemblies = "none";
		public ReferencesForm rf;
		private PrivateFontCollection pfc = new PrivateFontCollection();

		public ReKodeEditor()
		{
			InitializeComponent();
			foreach (var btn in this.Controls.OfType<Button>())
			{
				btn.BackColor = Color.FromArgb(40, 40, 40);
				btn.FlatStyle = FlatStyle.Flat;
				btn.FlatAppearance.BorderColor = Color.FromArgb(40, 40, 40);
				btn.FlatAppearance.BorderSize = 1;
			}
			//pfc.AddFontFile(@".\data\fonts\UbuntuMono-Regular.ttf");
			//fctb.Font = new Font(pfc.Families[0], 16, FontStyle.Regular, GraphicsUnit.Pixel);
		}

		private void ReKodeEditor_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			rf = new ReferencesForm(clientId, this);
			rf.ShowDialog();
		}

		private void compileBtn_Click(object sender, EventArgs e)
		{
			MainForm.SendCommand($"exec_csscript\n{assemblies}\n{fctb.Text}", clientId);
		}
	}
}
