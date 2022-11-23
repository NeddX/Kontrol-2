using NAudio.Wave;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kontrol_2_Server
{
    public partial class RemoteDesktopForm : Form
	{
		long frames = 0;
		double seconds = 0;
		bool isHovering = false;
		public int clientId = 0;
		public int[] screenResolution = { 0, 0 };
		string recPath = string.Empty;
		MemoryStream recStream = new MemoryStream();

		static string KeyToString(int key)
		{
			string keyStr;

			if (key == 8) keyStr = "[Backspace]";
			else if (key == 9) keyStr = "[TAB]";
			else if (key == 13) keyStr = "[Enter]";
			else if (key == 19) keyStr = "[Pause]";
			else if (key == 20) keyStr = "[Caps Lock]";
			else if (key == 27) keyStr = "[Esc]";
			else if (key == 32) keyStr = " ";
			else if (key == 33) keyStr = "[Page Up]";
			else if (key == 34) keyStr = "[Page Down]";
			else if (key == 35) keyStr = "[End]";
			else if (key == 36) keyStr = "[Home]";
			else if (key == 37) keyStr = "[Left]";
			else if (key == 38) keyStr = "[Up]";
			else if (key == 39) keyStr = "[Right]";
			else if (key == 40) keyStr = "[Down]";
			else if (key == 44) keyStr = "[Print Screen]";
			else if (key == 45) keyStr = "[Insert]";
			else if (key == 46) keyStr = "[Delete]";
			else if (key == 48) keyStr = "0";
			else if (key == 49) keyStr = "1";
			else if (key == 50) keyStr = "2";
			else if (key == 51) keyStr = "3";
			else if (key == 52) keyStr = "4";
			else if (key == 53) keyStr = "5";
			else if (key == 54) keyStr = "6";
			else if (key == 55) keyStr = "7";
			else if (key == 56) keyStr = "8";
			else if (key == 57) keyStr = "9";
			else if (key == 65) keyStr = "a";
			else if (key == 66) keyStr = "b";
			else if (key == 67) keyStr = "c";
			else if (key == 68) keyStr = "d";
			else if (key == 69) keyStr = "e";
			else if (key == 70) keyStr = "f";
			else if (key == 71) keyStr = "g";
			else if (key == 72) keyStr = "h";
			else if (key == 73) keyStr = "i";
			else if (key == 74) keyStr = "j";
			else if (key == 75) keyStr = "k";
			else if (key == 76) keyStr = "l";
			else if (key == 77) keyStr = "m";
			else if (key == 78) keyStr = "n";
			else if (key == 79) keyStr = "o";
			else if (key == 80) keyStr = "p";
			else if (key == 81) keyStr = "q";
			else if (key == 82) keyStr = "r";
			else if (key == 83) keyStr = "s";
			else if (key == 84) keyStr = "t";
			else if (key == 85) keyStr = "u";
			else if (key == 86) keyStr = "v";
			else if (key == 87) keyStr = "w";
			else if (key == 88) keyStr = "x";
			else if (key == 89) keyStr = "y";
			else if (key == 90) keyStr = "z";
			else if (key == 91) keyStr = "[Windows]";
			else if (key == 92) keyStr = "[Windows]";
			else if (key == 93) keyStr = "[List]";
			else if (key == 96) keyStr = "0";
			else if (key == 97) keyStr = "1";
			else if (key == 98) keyStr = "2";
			else if (key == 99) keyStr = "3";
			else if (key == 100) keyStr = "4";
			else if (key == 101) keyStr = "5";
			else if (key == 102) keyStr = "6";
			else if (key == 103) keyStr = "7";
			else if (key == 104) keyStr = "8";
			else if (key == 105) keyStr = "9";
			else if (key == 106) keyStr = "*";
			else if (key == 107) keyStr = "+";
			else if (key == 109) keyStr = "-";
			else if (key == 110) keyStr = ",";
			else if (key == 111) keyStr = "/";
			else if (key == 112) keyStr = "[F1]";
			else if (key == 113) keyStr = "[F2]";
			else if (key == 114) keyStr = "[F3]";
			else if (key == 115) keyStr = "[F4]";
			else if (key == 116) keyStr = "[F5]";
			else if (key == 117) keyStr = "[F6]";
			else if (key == 118) keyStr = "[F7]";
			else if (key == 119) keyStr = "[F8]";
			else if (key == 120) keyStr = "[F9]";
			else if (key == 121) keyStr = "[F10]";
			else if (key == 122) keyStr = "[F11]";
			else if (key == 123) keyStr = "[F12]";
			else if (key == 144) keyStr = "[Num Lock]";
			else if (key == 145) keyStr = "[Scroll Lock]";
			else if (key == 160) keyStr = "[Shift]";
			else if (key == 161) keyStr = "[Shift]";
			else if (key == 162) keyStr = "[Ctrl]";
			else if (key == 163) keyStr = "[Ctrl]";
			else if (key == 164) keyStr = "[Alt]";
			else if (key == 165) keyStr = "[Alt]";
			else if (key == 187) keyStr = "=";
			else if (key == 186) keyStr = "ç";
			else if (key == 188) keyStr = ",";
			else if (key == 189) keyStr = "-";
			else if (key == 190) keyStr = ".";
			else if (key == 192) keyStr = "'";
			else if (key == 191) keyStr = ";";
			else if (key == 193) keyStr = "/";
			else if (key == 194) keyStr = ".";
			else if (key == 219) keyStr = "´";
			else if (key == 220) keyStr = "]";
			else if (key == 221) keyStr = "[";
			else if (key == 222) keyStr = "~";
			else if (key == 226) keyStr = "\\";
			else keyStr = "[" + key + "]";
			return keyStr;
		}

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
            videoBox.Image = (Bitmap) Image.FromStream(new MemoryStream(imageBytes));
            if (recPath != string.Empty) recStream.Write(imageBytes, 0, imageBytes.Length);
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
            if (keyboardCheck.Checked && startButton.Text == "Stop")
            {
                //Console.WriteLine($"key press: {e.KeyCode}");
                MainForm.Send(new byte[] { 0xFE, 0xF1, 0xA1, (byte) e.KeyCode }, clientId);
            }
        }

        /*protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space || keyData == Keys.Enter && keyboardCheck.Focused || mouseCheck.Focused) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }*/

        void fpsCounter_Tick(object sender, EventArgs e)
		{
            seconds += 0.1;
            try
            { 
                //this.Text = string.Format("FPS: {0} Frames: {1} Seconds: {2}", Math.Round(frames/seconds), frames, seconds.ToString("0.0"));
            }
            catch { }
		}

        void UpdateKeyboardStateToClient()
        {
            if (Form.ActiveForm != this && !keyboardCheck.Checked) return;


        }

        void UpdateMouseStateToClient()
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
                        break;
                    case MouseButtons.Right:
                        mouseState = 0xB2;
                        break;
                    case MouseButtons.Middle:
                        mouseState = 0xB3;
                        break;
                }

                Buffer.BlockCopy(header, 0, data, 0, 3);
                data[3] = mouseState;
                Buffer.BlockCopy(mousePos[0], 0, data, 4, mousePos[0].Length);
                Buffer.BlockCopy(mousePos[1], 0, data, mousePos[0].Length + 4, mousePos[1].Length);

                MainForm.Send(data, clientId);
            }
        }

        void moueTimer_Tick(object sender, EventArgs e)
        {
            UpdateMouseStateToClient();
            UpdateKeyboardStateToClient();
        }

        void videoBox_MouseEnter(object sender, EventArgs e)
        {
            isHovering = true;
        }

        private void videoBox_MouseLeave(object sender, EventArgs e)
        {
            isHovering = false;
        }

        private void recButton_Click(object sender, EventArgs e)
        {
            if (recButton.Text == "Record")
            {
                SaveFileDialog fd = new SaveFileDialog();
                fd.AddExtension = true;
                fd.DefaultExt = ".mkv";
                fd.Filter = "MKV Video File | *.mkv";
                fd.Title = "MKV Video File location.";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(fd.FileName) || string.IsNullOrWhiteSpace(fd.FileName)) return;
                    recPath = fd.FileName;
                }
                recButton.Text = "Stop recording";
            }
            else
            {
                recStream.Position = 0;
                File.WriteAllBytes(recPath, recStream.ToArray());
                //var waveStream = new RawSourceWaveStream(recStream, waveFormat);
                //WaveFileWriter.CreateWaveFile(recPath, waveStream);
                recStream.Dispose();
                recStream.Close();
                recStream = new MemoryStream();
                recPath = null;
                recButton.Text = "Record";
            }
        }
    }
}