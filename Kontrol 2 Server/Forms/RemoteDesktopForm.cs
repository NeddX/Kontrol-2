using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Kontrol_2_Server
{
    public partial class RemoteDesktopForm : Form
    {
        long frames = 0;
        double seconds = 0;
        bool isHovering = false;
        public int clientId = 0;
        public int[] screenResolution = { 0, 0 };
        public RemoteDesktopForm()
        {
            InitializeComponent();
            resCombo.SelectedIndex = 5;
            moueTimer.Start();
        }

		void startButton_Click(object sender, System.EventArgs e)
		{
            if (startButton.Text == "Start")
            {
                MainForm.SendCommand("remote_desktop\nbegin_stream\nprimary\n" + resCombo.GetItemText(resCombo.SelectedItem) + "\n30\n" + qualityBox.Text    + "\ntrue", clientId);
                startButton.Text = "Stop";
                fpsCounter.Start();
            }
            else
			{
                MainForm.SendCommand("remote_desktop\nend_stream", clientId);
                startButton.Text = "Start";
                fpsCounter.Stop();
                frames = 0;
                seconds = 0;
            }
		}

        public void processImage(byte[] imageBytes)
		{
            videoBox.Image = (Bitmap)Image.FromStream(new MemoryStream(imageBytes));
            frames++;
        }

		void RemoteDesktopForm_FormClosing(object sender, FormClosingEventArgs e)
		{
            MainForm.rdf = new RemoteDesktopForm();
            if (startButton.Text == "Stop")
                MainForm.SendCommand("remote_desktop\nend_stream", clientId);
        }

		void qualityBox_TextChanged(object sender, System.EventArgs e)
		{
            int temp;
            if (qualityBox.Text == string.Empty)
			{
                qualityBox.Text = "10";
			}
            else if (!int.TryParse(qualityBox.Text, out temp))
			{
                {
                    qualityBox.Text = qualityBox.Text.Remove(qualityBox.Text.Length - 1);
                }
			}
            else if (temp < 0)
			{
                qualityBox.Text = "10";
			}
            else if (temp > 100)
			{
                qualityBox.Text = "100";
			}
		}

		void mouseCheck_CheckedChanged(object sender, System.EventArgs e)
		{

		}

		void videoBox_MouseMove(object sender, MouseEventArgs e)
        {

            /*if (screenResolution[0] > 0 && screenResolution[1] > 0 && mouseCheck.Checked && startButton.Text == "Stop")
            {
				int x = videoBox.PointToClient(Cursor.Position).X;
				int y = videoBox.PointToClient(Cursor.Position).Y;
				double relx = (x * screenResolution[0]) / videoBox.Width;
				double rely = (y * screenResolution[1]) / videoBox.Height;
				//this.Text = string.Format("screenResolution: {0}x{1} videoBox Client X: {2} videoBox Client Y: {3} videoBox Size: {4}x{5} Relative X: {6} Relative Y: {7}",
				//screenResolution[0], screenResolution[1], x, y, videoBox.Width, videoBox.Height, relx, rely);
				MainForm.SendCommand("remote_desktop\nsmcord\n" + relx.ToString() + "\n" + rely.ToString(), clientId);
				Thread.Sleep(70); //let's be friendly to the network bandwidth :)
			}*/
        }

        void videoBox_Click(object sender, System.EventArgs e)
        {
            /*if (screenResolution[0] > 0 && screenResolution[1] > 0 && mouseCheck.Checked && startButton.Text == "Stop")
            {
                videoBox.Focus();
                int x = videoBox.PointToClient(Cursor.Position).X;
                int y = videoBox.PointToClient(Cursor.Position).Y;
                double relx = (x * screenResolution[0]) / videoBox.Width;
                double rely = (y * screenResolution[1]) / videoBox.Height;

                MouseEventArgs me = (MouseEventArgs) e;
                switch (me.Button)
				{
                    case MouseButtons.Left:
                        MainForm.SendCommand("remote_desktop\nlmclk\n" + relx.ToString() + "\n" + rely.ToString(), clientId);
                        break;
                    case MouseButtons.Right:
                        MainForm.SendCommand("remote_desktop\nrmclk\n" + relx.ToString() + "\n" + rely.ToString(), clientId);
                        break;
                    case MouseButtons.Middle:
                        MainForm.SendCommand("remote_desktop\nmmclk\n" + relx.ToString() + "\n" + rely.ToString(), clientId);
                        break;
				}
                //this.Text = string.Format("screenResolution: {0}x{1} videoBox Client X: {2} videoBox Client Y: {3} videoBox Size: {4}x{5} Relative X: {6} Relative Y: {7}",
                //screenResolution[0], screenResolution[1], x, y, videoBox.Width, videoBox.Height, relx, rely);
            }*/
        }

        void RemoteDesktopForm_KeyDown(object sender, KeyEventArgs e)
		{
            
        }
        void RemoteDesktopForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (keyboardCheck.Checked && startButton.Text == "Stop")
            {
                int temp;
                string key = e.KeyChar.ToString();
                if (key.StartsWith("D") && key.Length > 1)
                {
                    if (int.TryParse(key[1].ToString(), out temp)) key = key.Substring(1);
                }
                switch (key.ToLower())
                {
                    case "back":
                        key = "BS";
                        break;
                    case "capital":
                        key = "CAPSLOCK";
                        break;
                    case "return":
                        key = "ENTER";
                        break;
                    case "escape":
                        key = "ESC";
                        break;
                    case "next":
                        key = "PGDN";
                        break;
                    case "shiftkey":
                        key = "+";
                        break;
                    case "controlkey":
                        key = "^";
                        break;
                    case "menu":
                        key = "%";
                        break;
                }
                if (Char.IsControl(e.KeyChar))
				{
                    this.Text = "IT IS E CONTROLLL!!!";
				}
                else
                {
                    this.Text = e.KeyChar.ToString();
                }
                MainForm.SendCommand("remote_desktop\nkpress\n" + "{" + key + "}", clientId);
            }
        }

        void fpsCounter_Tick(object sender, EventArgs e)
		{
            seconds += 0.1;
            try
            { 
                //this.Text = string.Format("FPS: {0} Frames: {1} Seconds: {2}", Math.Round(frames/seconds), frames, seconds.ToString("0.0"));
            }
            catch { }
		}

        void keyboardCheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        void moueTimer_Tick(object sender, EventArgs e)
        {
            if (screenResolution[0] > 0 
                && screenResolution[1] > 0 
                && mouseCheck.Checked 
                && startButton.Text == "Stop" 
                && isHovering)
            {
                MouseButtons mButtonState = Control.MouseButtons;

                int x = videoBox.PointToClient(Cursor.Position).X;
                int y = videoBox.PointToClient(Cursor.Position).Y;
                int relx = (x * screenResolution[0]) / videoBox.Width;
                int rely = (y * screenResolution[1]) / videoBox.Height;

                byte[] header = { 0xFE, 0xF1, 0xA0 };
                byte[][] mousePos = 
                { 
                    BitConverter.GetBytes(relx), 
                    BitConverter.GetBytes(rely)
                };
                byte mouseState = 0xB0;
                byte[] data = new byte[mousePos[0].Length + mousePos[1].Length + 4];

                switch (mButtonState)
                {
                    case MouseButtons.None:
                        mouseState = 0xB0;
                        break;
                    case MouseButtons.Left:
                        mouseState = 0xB1;
                        Buffer.BlockCopy(header, 0, data, 0, 3);
                        data[3] = mouseState;
                        Buffer.BlockCopy(mousePos[0], 0, data, 4, mousePos[0].Length);
                        Buffer.BlockCopy(mousePos[1], 0, data, mousePos[0].Length + 4, mousePos[1].Length);
                        //MainForm.Send(data, clientId);

                        Console.WriteLine($"MState: {mouseState} MX: {relx} MY: {rely}");
                        break;
                    case MouseButtons.Right:
                        mouseState = 0xB2;
                        break;
                    case MouseButtons.Middle:
                        mouseState = 0xB3;
                        break;
                }

            }
        }

        void videoBox_MouseEnter(object sender, EventArgs e)
        {
            isHovering = true;
        }

        private void videoBox_MouseLeave(object sender, EventArgs e)
        {
            isHovering = false;
        }
    }
}