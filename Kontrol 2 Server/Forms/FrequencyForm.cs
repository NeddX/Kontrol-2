using System;
using System.Windows.Forms;

namespace Kontrol_2_Server
{
    public partial class FrequencyForm : Form
    {
        public int clientId = 0;
        public FrequencyForm()
        {
            InitializeComponent();
        }

        private void frequencyBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (frequencyBox.Text != "")
                {
                    int.Parse(frequencyBox.Text);
                }
            }
            catch (Exception)
            {
                frequencyBox.Text = "1000";
            }
        }

        private void durationBox_TextChanged(object sender, EventArgs ea)
        {
            try
            {
                if (durationBox.Text != "")
                {
                    int.Parse(durationBox.Text);
                }
            }
            catch (Exception)
            {
                durationBox.Text = "60000";
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.SendCommand("play_frequency\n" + frequencyBox.Text + "\n" + durationBox.Text, clientId);
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void FrequencyForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar));
        }

        private void FrequencyForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
