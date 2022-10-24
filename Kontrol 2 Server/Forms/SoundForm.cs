using System;
using System.IO;
using System.Windows.Forms;

namespace Kontrol_2_Server
{
    public partial class SoundForm : Form
    {
        public int clientId = 0;
        public SoundForm()
        {
            InitializeComponent();
        }
        private void loadButton_Click(object sender, EventArgs e)
        {
            string fileContent;
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.InitialDirectory = "c:\\";
                fd.Filter = "Waveform and mp3 files (*.wav)|*.wav;*.mp3;";
                fd.FilterIndex = 1;
                fd.RestoreDirectory = true;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    soundBox.Text = fd.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = fd.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm mf = new MainForm();
                if (!fileCheck.Checked)
                {
                    MainForm.SendCommand("play_systemsound\n" + soundCombo.SelectedIndex, clientId);
                }
                else //play a sound file
                {

                }
                /*MainForm mf = new MainForm();
				MainForm.SendCommand("play_soundfile\n" + playFile.ToString().ToLower(), clientId);
				MainForm.SendFile(clientId, soundBox.Text);*/
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void fileCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (fileCheck.Checked)
            {
                soundBox.Enabled = true;
                loadButton.Enabled = true;
            }
            else
            {
                soundBox.Enabled = false;
                loadButton.Enabled = false;
            }
        }

        private void SoundForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void SoundForm_Load(object sender, EventArgs e)
        {

        }
    }
}
