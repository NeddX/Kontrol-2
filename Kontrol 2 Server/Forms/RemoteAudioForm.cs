using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;
using NAudio.Lame;

namespace Kontrol_2_Server
{
	public partial class RemoteAudioForm : Form
	{
		public int clientId = 0;
		public bool auStream = false;
		private string recPath = null;
		private AudioStream stream = new AudioStream();
		WaveFormat waveFormat = new WaveFormat(8000, 8, 2);
		private MemoryStream recStream = new MemoryStream();
		public RemoteAudioForm()
		{
			InitializeComponent();
		}

		private void RemoteAudioForm_Load(object sender, EventArgs e)
		{
			stream.Init();
			new MainForm().SendCommand("remote_audio\naudio_devices", clientId);
		}

		private void RemoteAudioForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			MainForm.raf = new RemoteAudioForm();
			if (auStream)
				new MainForm().SendCommand("remote_audio\nend_stream", clientId);
		}

		public void processAudio(byte[] audioBytes)
		{
			MemoryStream ms = new MemoryStream(audioBytes);
			stream.BufferPlay(audioBytes);
			waveViewer.WaveStream = new RawSourceWaveStream(ms, new WaveFormat());
			if (recPath != null)
			{
				recStream.Write(audioBytes, 0, audioBytes.Length);
			}
		}

		public void listDevices(string devices)
		{
			string[] xsplit = devices.Split('&');
			if (this.InvokeRequired)
			{
				devicesCombo.Invoke(new MethodInvoker(delegate
				{
					devicesCombo.Items.Clear();
					foreach (string device in xsplit)
					{
						devicesCombo.Items.Add(device);
					}
					devicesCombo.SelectedIndex = 0;
				}));
			}
			else
			{
				devicesCombo.Items.Clear();
				foreach (string device in xsplit)
				{
					devicesCombo.Items.Add(device);
				}
				devicesCombo.SelectedIndex = 0;
			}
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			if (startButton.Text.ToLower() == "start")
			{
				if (devicesCombo.GetItemText(devicesCombo.SelectedItem) == "Internal Audio")
					new MainForm().SendCommand("remote_audio\nbegin_stream\n-5", clientId);
				else
					new MainForm().SendCommand("remote_audio\nbegin_stream\n" + devicesCombo.SelectedIndex, clientId);
				qualityCombo.Enabled = false;
				devicesCombo.Enabled = false;
				startButton.Text = "Stop";
				auStream = true;
				//WaveRenderer.Start();
			}
			else
			{
				new MainForm().SendCommand("remote_audio\nend_stream", clientId);
				qualityCombo.Enabled = true;
				devicesCombo.Enabled = true;
				startButton.Text = "Start";
				auStream = false;
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
				var waveStream = new RawSourceWaveStream(recStream, new WaveFormat()); 
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
