using System;
using System.Windows.Forms;

namespace Kontrol_2_Server
{
    public partial class VisitWebsiteForm : Form
    {
        public int clientId = -1;

        public VisitWebsiteForm()
        {
            InitializeComponent();
        }

        private void visitBtn_Click(object sender, EventArgs e)
        {

            MainForm.SendCommand("create_process\n0\n" + siteBox.Text + "\n\ntrue\ntrue", clientId);
        }

        private void VisitWebsiteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.vwf = new VisitWebsiteForm();
            this.Dispose();
        }
    }
}
