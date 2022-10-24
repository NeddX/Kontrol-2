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
    public partial class ProgressBarForm : Form
    {
        public ProgressBarForm(string Title, string Text, int Percent = 0)
        {
            InitializeComponent();
            this.Text = Title;
            groupBox.Text = Text;
            progressBar.Value = Percent;
        }

        public void SetTitle(string Title)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.Text = Title;
            }));
        }

        public void SetText(string Text)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                groupBox.Text = Text;
            }));
        }

        public void SetPercent(int Percent)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                progressBar.Value = Percent;
            }));
        }
    }
}
