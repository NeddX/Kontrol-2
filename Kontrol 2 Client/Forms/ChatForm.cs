using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kontrol_2_Client
{
	public partial class ChatForm : Form
	{
		public ChatForm()
		{
			InitializeComponent();
		}

		public void AppendText(string text, string adminNick)
		{
			textBox2.Text += adminNick + "> " + text;
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			if (!textBox2.Text.StartsWith("> "))
				textBox2.Text += nickBox.Text + "> ";
		}

		private void sendBtn_Click(object sender, EventArgs e)
		{
			logBox.Text += textBox2.Text + "\n";
			Client.Send("chat_in\n" + nickBox.Text + "\n@{msg}" + nickBox.Text + textBox2.Text);
			textBox2.SelectionStart = textBox2.SelectionLength;
		}

		private void textBox2_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				sendBtn_Click(null, null);
				textBox2.Text = "> ";
				textBox2.SelectionStart = textBox2.Text.Length + 1;
			}
		}

		private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
		}
	}
}
