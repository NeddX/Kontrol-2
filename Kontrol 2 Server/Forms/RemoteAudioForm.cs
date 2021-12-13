using System;
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

namespace Kontrol_2_Server
{
	public partial class RemoteAudioForm : Form
	{
		public int clientId = 0;
		public bool auStream = false;
		private AudioStream stream = new AudioStream();
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
			//waveViewer.WaveStream = new StreamReader(ms); 
			waveViewer.WaveStream = new RawSourceWaveStream(ms, new WaveFormat());
		}

		public void listDevices(string devices)
		{
			string[] xsplit = devices.Split('&');
			if (this.InvokeRequired)
			{
				devicesCombo.Invoke(new MethodInvoker(delegate
				{
					foreach (string device in xsplit)
					{
						devicesCombo.Items.Add(device);
					}
					devicesCombo.SelectedIndex = 0;
				}));
			}
			else
			{
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

		private void WaveRenderer_Tick(object sender, EventArgs e)
		{

		}
	}
}
