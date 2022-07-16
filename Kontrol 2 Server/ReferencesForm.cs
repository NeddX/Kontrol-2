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
	public partial class ReferencesForm : Form
	{
		private int clientId = -1;
		private ReKodeEditor owner;
		public ReferencesForm(int clientId, ReKodeEditor owner)
		{
			InitializeComponent();
			this.clientId = clientId;
			this.owner = owner;
			AssemblyList.DoubleBuffering(true);
		}

		private void ReferencesForm_Load(object sender, EventArgs e)
		{
			MainForm.SendCommand("conditions_getSelfContainedAssemblies", clientId);
		}

		public void Apppend(string assem)
		{
			ListViewItem lv = new ListViewItem(assem);
			/*if (owner.assemblies.Contains(assem))
				lv.Checked = true;*/
			AssemblyList.Items.Add(lv);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			/*owner.assemblies = "";
			for (int i = 0; i < AssemblyList.Items.Count - 1; i++)
			{
				if (AssemblyList.Items[i].Checked)
					owner.assemblies += AssemblyList.Items[i].Text + "@";
			}
			owner.assemblies = owner.assemblies.Substring(0, owner.assemblies.Length - 1);*/
			//this.Text = owner.assemblies;
		}
	}
}
