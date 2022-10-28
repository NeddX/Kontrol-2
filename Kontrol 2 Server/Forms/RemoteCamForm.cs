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
		private List<byte[]> imagesList = new List<byte[]>(); //for rec
		public RemoteCamForm()
		{
			InitializeComponent();
		}

		private void RemoteCamForm_Load(object sender, EventArgs e)
		{
			MainForm.SendCommand("remote_webcam\ncapture_devices", clientId);
			MainForm.SendCommand("remote_webcam\ndevice_resolutions\n0", clientId);
			//MainForm.SendCommand("remote_webcam\nbegin_stream", clientId);
		}
		public void listDevices(string devices)
		{
			string[] xsplit = devices.Split('&');
			if (this.InvokeRequired)
			{
				devicesCombo.Invoke(new MethodInvoker(delegate
				{
					foreach (string device in xsplit) devicesCombo.Items.Add(device);
					devicesCombo.SelectedIndex = 0;
					devicesCombo.Enabled = true;
				}));
			}
			else
			{
				foreach (string device in xsplit)
				{
					devicesCombo.Items.Add(device);
				}
				devicesCombo.SelectedIndex = 0;
				devicesCombo.Enabled = true;
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
					foreach (string res in xsplit) resCombo.Items.Add(res);
					resCombo.SelectedIndex = 0;
					resCombo.Enabled = true;
				}));
			}
			else
			{
				resCombo.Items.Clear();
				foreach (string res in xsplit) resCombo.Items.Add(res);
				resCombo.SelectedIndex = 0;
				resCombo.Enabled = true;
			}
		}
		public void displayImage(byte[] imageBytes)
		{
			if (wcStream)
			{
				videoBox.Image = (Bitmap) Image.FromStream(new MemoryStream(imageBytes));
				/*if (this.InvokeRequired)
				{
					videoBox.Invoke(new MethodInvoker(delegate
					{
						videoBox.Image = (Bitmap)Image.FromStream(new MemoryStream(imageBytes));
						if (recButton.Text == "Stop Recording")
						{
							imagesList.Add(imageBytes);
						}
					}));
				}
				else
				{
					videoBox.Image = (Bitmap)Image.FromStream(new MemoryStream(imageBytes));
					if (recButton.Text == "Stop Recording")
					{
						imagesList.Add(imageBytes);
					}
				}*/
			}
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			if (startButton.Text.ToLower() == "start")
			{
				MainForm.SendCommand("remote_webcam\nbegin_stream\n" + devicesCombo.SelectedIndex + "&" + resCombo.SelectedIndex + "&" + qualityBox.Text, clientId);
				resCombo.Enabled = false;
				devicesCombo.Enabled = false;
				recButton.Enabled = true;
				button1.Enabled = true;
				startButton.Text = "Stop";
			}
			else
			{
				MainForm.SendCommand("remote_webcam\nend_stream", clientId);
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
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = ".jpg";
            sfd.FileName = "Snapshot " + DateTime.Now.ToFileTime();
            sfd.Filter = "JPEG Image File (*.jpg, *.jpeg)|*.jpg;*.jpeg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                spath = sfd.FileName;
                videoBox.Image.Save(spath, ImageFormat.Jpeg);
            }
        }

		private void RemoteCamForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			MainForm.rcf = new RemoteCamForm();
			if(wcStream) MainForm.SendCommand("remote_webcam\nend_stream", clientId);
		}

		private void devicesCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			MainForm.SendCommand("remote_webcam\ndevice_resolutions\n" + devicesCombo.SelectedIndex, clientId);
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
		}
		private void videoEncoder_Tick(object sender, EventArgs e)
		{

		}

		private void qualityBox_TextChanged(object sender, EventArgs e)
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
	}
}
