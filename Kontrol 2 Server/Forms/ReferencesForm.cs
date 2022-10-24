using System;
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
            try
            {
                ListViewItem lv = new ListViewItem(assem);
                if (owner.assemblies.Contains(assem))
                    lv.Checked = true;
                AssemblyList.Items.Add(lv);
            }
            catch { }
        }

        private void button1_Click()
        {
            owner.assemblies = "";
            foreach (ListViewItem asm in AssemblyList.Items)
            {
                if (asm.Checked)
                    owner.assemblies += asm.Text + "@";
            }
            if (owner.assemblies != "") owner.assemblies = owner.assemblies.Substring(0, owner.assemblies.Length - 1);
        }

        private void ReferencesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            button1_Click();
            this.Close();
        }
    }
}
