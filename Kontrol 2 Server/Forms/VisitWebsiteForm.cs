using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
