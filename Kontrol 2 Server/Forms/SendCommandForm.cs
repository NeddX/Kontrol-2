﻿using System;
using System.Windows.Forms;

namespace Kontrol_2_Server
{
    public partial class SendCommandForm : Form
    {
        public int clientId = -1;
        public SendCommandForm()
        {
            InitializeComponent();
            cmdBox_TextChanged(null, null);
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            MainForm.SendCommand(cmdBox.Text, clientId);
        }

        private void cmdBox_TextChanged(object sender, EventArgs e)
        {
            cmdBox.Text = cmdBox.Text.Replace("\\n", "\r\n");
        }

        private void SendCommandForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.scf = new SendCommandForm();
        }

        private void SendCommandForm_Load(object sender, EventArgs e)
        {

        }
    }
}
