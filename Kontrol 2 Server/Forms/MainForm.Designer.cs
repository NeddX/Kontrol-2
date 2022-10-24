
namespace Kontrol_2_Server
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ClientsView = new System.Windows.Forms.ListView();
            this.c_id = new System.Windows.Forms.ColumnHeader();
            this.c_ipAddress = new System.Windows.Forms.ColumnHeader();
            this.c_geoLoc = new System.Windows.Forms.ColumnHeader();
            this.c_machineName = new System.Windows.Forms.ColumnHeader();
            this.c_Time = new System.Windows.Forms.ColumnHeader();
            this.c_user = new System.Windows.Forms.ColumnHeader();
            this.c_os = new System.Windows.Forms.ColumnHeader();
            this.c_hwid = new System.Windows.Forms.ColumnHeader();
            this.c_clientVersion = new System.Windows.Forms.ColumnHeader();
            this.c_privileges = new System.Windows.Forms.ColumnHeader();
            this.c_activeWindow = new System.Windows.Forms.ColumnHeader();
            this.listViewStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.funMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendMessageBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playSystemSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.microsoftTextToSpeechToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playSinewaveFrequencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideWindowsElementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visitWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitoringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proccessManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteDesktopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteWebcamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyloggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteShellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.shutdownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sleepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uninstallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileCScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.builderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iconsList = new System.Windows.Forms.ImageList(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSentLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusRecievedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusCPULabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusRAMLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.listUpdate = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusUpdate = new System.Windows.Forms.Timer(this.components);
            this.systemTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.listViewStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.notifyStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTab
            // 
            this.mainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTab.Controls.Add(this.tabPage1);
            this.mainTab.Controls.Add(this.tabPage2);
            this.mainTab.Controls.Add(this.tabPage3);
            this.mainTab.Location = new System.Drawing.Point(12, 12);
            this.mainTab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(1188, 282);
            this.mainTab.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ClientsView);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Size = new System.Drawing.Size(1180, 254);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Clients";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ClientsView
            // 
            this.ClientsView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.ClientsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClientsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.c_id,
            this.c_ipAddress,
            this.c_geoLoc,
            this.c_machineName,
            this.c_Time,
            this.c_user,
            this.c_os,
            this.c_hwid,
            this.c_clientVersion,
            this.c_privileges,
            this.c_activeWindow});
            this.ClientsView.ContextMenuStrip = this.listViewStrip;
            this.ClientsView.FullRowSelect = true;
            this.ClientsView.LabelEdit = true;
            this.ClientsView.LargeImageList = this.iconsList;
            this.ClientsView.Location = new System.Drawing.Point(4, 3);
            this.ClientsView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ClientsView.Name = "ClientsView";
            this.ClientsView.Size = new System.Drawing.Size(1172, 248);
            this.ClientsView.SmallImageList = this.iconsList;
            this.ClientsView.TabIndex = 0;
            this.ClientsView.UseCompatibleStateImageBehavior = false;
            this.ClientsView.View = System.Windows.Forms.View.Details;
            // 
            // c_id
            // 
            this.c_id.Text = "ID";
            this.c_id.Width = 50;
            // 
            // c_ipAddress
            // 
            this.c_ipAddress.Text = "IP Address";
            this.c_ipAddress.Width = 125;
            // 
            // c_geoLoc
            // 
            this.c_geoLoc.Text = "Country";
            this.c_geoLoc.Width = 125;
            // 
            // c_machineName
            // 
            this.c_machineName.Text = "Machine Name";
            this.c_machineName.Width = 125;
            // 
            // c_Time
            // 
            this.c_Time.Text = "Time";
            this.c_Time.Width = 125;
            // 
            // c_user
            // 
            this.c_user.DisplayIndex = 6;
            this.c_user.Text = "Username";
            this.c_user.Width = 125;
            // 
            // c_os
            // 
            this.c_os.DisplayIndex = 5;
            this.c_os.Text = "Operating System";
            this.c_os.Width = 125;
            // 
            // c_hwid
            // 
            this.c_hwid.Text = "HWID";
            this.c_hwid.Width = 125;
            // 
            // c_clientVersion
            // 
            this.c_clientVersion.Text = "Client Version";
            this.c_clientVersion.Width = 125;
            // 
            // c_privileges
            // 
            this.c_privileges.Text = "Privileges";
            this.c_privileges.Width = 125;
            // 
            // c_activeWindow
            // 
            this.c_activeWindow.Text = "Active Window";
            this.c_activeWindow.Width = 125;
            // 
            // listViewStrip
            // 
            this.listViewStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.funMenuToolStripMenuItem,
            this.monitoringToolStripMenuItem,
            this.miscToolStripMenuItem,
            this.manageClientToolStripMenuItem,
            this.advancedToolStripMenuItem,
            this.builderToolStripMenuItem});
            this.listViewStrip.Name = "listViewStrip";
            this.listViewStrip.Size = new System.Drawing.Size(152, 136);
            // 
            // funMenuToolStripMenuItem
            // 
            this.funMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendMessageBoxToolStripMenuItem,
            this.playSystemSoundToolStripMenuItem,
            this.microsoftTextToSpeechToolStripMenuItem,
            this.playSinewaveFrequencyToolStripMenuItem,
            this.hideWindowsElementsToolStripMenuItem,
            this.visitWebsiteToolStripMenuItem,
            this.chatToolStripMenuItem});
            this.funMenuToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_14;
            this.funMenuToolStripMenuItem.Name = "funMenuToolStripMenuItem";
            this.funMenuToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.funMenuToolStripMenuItem.Text = "Fun Menu";
            // 
            // sendMessageBoxToolStripMenuItem
            // 
            this.sendMessageBoxToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_81;
            this.sendMessageBoxToolStripMenuItem.Name = "sendMessageBoxToolStripMenuItem";
            this.sendMessageBoxToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.sendMessageBoxToolStripMenuItem.Text = "Send Message Box";
            this.sendMessageBoxToolStripMenuItem.Click += new System.EventHandler(this.sendMessageBoxToolStripMenuItem_Click);
            // 
            // playSystemSoundToolStripMenuItem
            // 
            this.playSystemSoundToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.shell32_16824;
            this.playSystemSoundToolStripMenuItem.Name = "playSystemSoundToolStripMenuItem";
            this.playSystemSoundToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.playSystemSoundToolStripMenuItem.Text = "Play Sound";
            this.playSystemSoundToolStripMenuItem.Click += new System.EventHandler(this.playSystemSoundToolStripMenuItem_Click);
            // 
            // microsoftTextToSpeechToolStripMenuItem
            // 
            this.microsoftTextToSpeechToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_1010;
            this.microsoftTextToSpeechToolStripMenuItem.Name = "microsoftTextToSpeechToolStripMenuItem";
            this.microsoftTextToSpeechToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.microsoftTextToSpeechToolStripMenuItem.Text = "Microsoft Text to Speech";
            this.microsoftTextToSpeechToolStripMenuItem.Click += new System.EventHandler(this.microsoftTextToSpeechToolStripMenuItem_Click);
            // 
            // playSinewaveFrequencyToolStripMenuItem
            // 
            this.playSinewaveFrequencyToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_5343;
            this.playSinewaveFrequencyToolStripMenuItem.Name = "playSinewaveFrequencyToolStripMenuItem";
            this.playSinewaveFrequencyToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.playSinewaveFrequencyToolStripMenuItem.Text = "Play Sinewave Frequency";
            this.playSinewaveFrequencyToolStripMenuItem.Click += new System.EventHandler(this.playSinewaveFrequencyToolStripMenuItem_Click);
            // 
            // hideWindowsElementsToolStripMenuItem
            // 
            this.hideWindowsElementsToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_110;
            this.hideWindowsElementsToolStripMenuItem.Name = "hideWindowsElementsToolStripMenuItem";
            this.hideWindowsElementsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.hideWindowsElementsToolStripMenuItem.Text = "Hide Windows Elements";
            this.hideWindowsElementsToolStripMenuItem.Click += new System.EventHandler(this.hideWindowsElementsToolStripMenuItem_Click);
            // 
            // visitWebsiteToolStripMenuItem
            // 
            this.visitWebsiteToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_25;
            this.visitWebsiteToolStripMenuItem.Name = "visitWebsiteToolStripMenuItem";
            this.visitWebsiteToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.visitWebsiteToolStripMenuItem.Text = "Visit Website";
            this.visitWebsiteToolStripMenuItem.Click += new System.EventHandler(this.visitWebsiteToolStripMenuItem_Click);
            // 
            // chatToolStripMenuItem
            // 
            this.chatToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_10101;
            this.chatToolStripMenuItem.Name = "chatToolStripMenuItem";
            this.chatToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.chatToolStripMenuItem.Text = "Chat";
            this.chatToolStripMenuItem.Click += new System.EventHandler(this.chatToolStripMenuItem_Click);
            // 
            // monitoringToolStripMenuItem
            // 
            this.monitoringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.proccessManagerToolStripMenuItem,
            this.fileManagerToolStripMenuItem,
            this.remoteDesktopToolStripMenuItem,
            this.remoteWebcamToolStripMenuItem,
            this.remoteAudioToolStripMenuItem,
            this.keyloggerToolStripMenuItem});
            this.monitoringToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.shell32_22;
            this.monitoringToolStripMenuItem.Name = "monitoringToolStripMenuItem";
            this.monitoringToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.monitoringToolStripMenuItem.Text = "Surveillance";
            // 
            // proccessManagerToolStripMenuItem
            // 
            this.proccessManagerToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.shell32_321;
            this.proccessManagerToolStripMenuItem.Name = "proccessManagerToolStripMenuItem";
            this.proccessManagerToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.proccessManagerToolStripMenuItem.Text = "Process Manager";
            this.proccessManagerToolStripMenuItem.Click += new System.EventHandler(this.proccessManagerToolStripMenuItem_Click);
            // 
            // fileManagerToolStripMenuItem
            // 
            this.fileManagerToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_1023;
            this.fileManagerToolStripMenuItem.Name = "fileManagerToolStripMenuItem";
            this.fileManagerToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.fileManagerToolStripMenuItem.Text = "File Manager";
            this.fileManagerToolStripMenuItem.Click += new System.EventHandler(this.fileManagerToolStripMenuItem_Click);
            // 
            // remoteDesktopToolStripMenuItem
            // 
            this.remoteDesktopToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_110;
            this.remoteDesktopToolStripMenuItem.Name = "remoteDesktopToolStripMenuItem";
            this.remoteDesktopToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.remoteDesktopToolStripMenuItem.Text = "Remote Desktop";
            this.remoteDesktopToolStripMenuItem.Click += new System.EventHandler(this.remoteDesktopToolStripMenuItem_Click);
            // 
            // remoteWebcamToolStripMenuItem
            // 
            this.remoteWebcamToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_57;
            this.remoteWebcamToolStripMenuItem.Name = "remoteWebcamToolStripMenuItem";
            this.remoteWebcamToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.remoteWebcamToolStripMenuItem.Text = "Remote Webcam";
            this.remoteWebcamToolStripMenuItem.Click += new System.EventHandler(this.remoteWebcamToolStripMenuItem_Click);
            // 
            // remoteAudioToolStripMenuItem
            // 
            this.remoteAudioToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("remoteAudioToolStripMenuItem.Image")));
            this.remoteAudioToolStripMenuItem.Name = "remoteAudioToolStripMenuItem";
            this.remoteAudioToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.remoteAudioToolStripMenuItem.Text = "Remote Audio";
            this.remoteAudioToolStripMenuItem.Click += new System.EventHandler(this.remoteAudioToolStripMenuItem_Click);
            // 
            // keyloggerToolStripMenuItem
            // 
            this.keyloggerToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_5360;
            this.keyloggerToolStripMenuItem.Name = "keyloggerToolStripMenuItem";
            this.keyloggerToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.keyloggerToolStripMenuItem.Text = "Keylogger";
            this.keyloggerToolStripMenuItem.Click += new System.EventHandler(this.keyloggerToolStripMenuItem_Click);
            // 
            // miscToolStripMenuItem
            // 
            this.miscToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.remoteShellToolStripMenuItem});
            this.miscToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_8;
            this.miscToolStripMenuItem.Name = "miscToolStripMenuItem";
            this.miscToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.miscToolStripMenuItem.Text = "Misc";
            // 
            // remoteShellToolStripMenuItem
            // 
            this.remoteShellToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_5323;
            this.remoteShellToolStripMenuItem.Name = "remoteShellToolStripMenuItem";
            this.remoteShellToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.remoteShellToolStripMenuItem.Text = "Remote Shell";
            this.remoteShellToolStripMenuItem.Click += new System.EventHandler(this.remoteShellToolStripMenuItem_Click);
            // 
            // manageClientToolStripMenuItem
            // 
            this.manageClientToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1,
            this.shutdownToolStripMenuItem,
            this.sleepToolStripMenuItem,
            this.restartToolStripMenuItem,
            this.uninstallToolStripMenuItem,
            this.showLocationToolStripMenuItem,
            this.reloadToolStripMenuItem});
            this.manageClientToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.shell32_16826;
            this.manageClientToolStripMenuItem.Name = "manageClientToolStripMenuItem";
            this.manageClientToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.manageClientToolStripMenuItem.Text = "Manage Client";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Image = global::Kontrol_2_Server.Properties.Resources.imageres_1027;
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // shutdownToolStripMenuItem
            // 
            this.shutdownToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_1008;
            this.shutdownToolStripMenuItem.Name = "shutdownToolStripMenuItem";
            this.shutdownToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.shutdownToolStripMenuItem.Text = "Shutdown";
            this.shutdownToolStripMenuItem.Click += new System.EventHandler(this.shutdownToolStripMenuItem_Click);
            // 
            // sleepToolStripMenuItem
            // 
            this.sleepToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_10081;
            this.sleepToolStripMenuItem.Name = "sleepToolStripMenuItem";
            this.sleepToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sleepToolStripMenuItem.Text = "Sleep";
            this.sleepToolStripMenuItem.Click += new System.EventHandler(this.sleepToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_5311;
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // uninstallToolStripMenuItem
            // 
            this.uninstallToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_54;
            this.uninstallToolStripMenuItem.Name = "uninstallToolStripMenuItem";
            this.uninstallToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.uninstallToolStripMenuItem.Text = "Uninstall";
            this.uninstallToolStripMenuItem.Click += new System.EventHandler(this.uninstallToolStripMenuItem_Click);
            // 
            // showLocationToolStripMenuItem
            // 
            this.showLocationToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_18;
            this.showLocationToolStripMenuItem.Name = "showLocationToolStripMenuItem";
            this.showLocationToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showLocationToolStripMenuItem.Text = "Show Location";
            this.showLocationToolStripMenuItem.Click += new System.EventHandler(this.showLocationToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_5311;
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendCommandToolStripMenuItem,
            this.compileCScriptToolStripMenuItem});
            this.advancedToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_34;
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.advancedToolStripMenuItem.Text = "Advanced";
            // 
            // sendCommandToolStripMenuItem
            // 
            this.sendCommandToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.imageres_53241;
            this.sendCommandToolStripMenuItem.Name = "sendCommandToolStripMenuItem";
            this.sendCommandToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.sendCommandToolStripMenuItem.Text = "Send Command";
            this.sendCommandToolStripMenuItem.Click += new System.EventHandler(this.sendCommandToolStripMenuItem_Click);
            // 
            // compileCScriptToolStripMenuItem
            // 
            this.compileCScriptToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.shell32_153;
            this.compileCScriptToolStripMenuItem.Name = "compileCScriptToolStripMenuItem";
            this.compileCScriptToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.compileCScriptToolStripMenuItem.Text = "Compile C# Script";
            this.compileCScriptToolStripMenuItem.Click += new System.EventHandler(this.compileCScriptToolStripMenuItem_Click);
            // 
            // builderToolStripMenuItem
            // 
            this.builderToolStripMenuItem.Image = global::Kontrol_2_Server.Properties.Resources.shell32_153;
            this.builderToolStripMenuItem.Name = "builderToolStripMenuItem";
            this.builderToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.builderToolStripMenuItem.Text = "Builder";
            this.builderToolStripMenuItem.Click += new System.EventHandler(this.builderToolStripMenuItem_Click);
            // 
            // iconsList
            // 
            this.iconsList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.iconsList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconsList.ImageStream")));
            this.iconsList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconsList.Images.SetKeyName(0, "desktop_icon");
            this.iconsList.Images.SetKeyName(1, "exe_icon");
            this.iconsList.Images.SetKeyName(2, "admin_icon");
            this.iconsList.Images.SetKeyName(3, "explorer_icon");
            this.iconsList.Images.SetKeyName(4, "imageres_5323.ico");
            this.iconsList.Images.SetKeyName(5, "dosia_icon");
            this.iconsList.Images.SetKeyName(6, "shell_icon");
            this.iconsList.Images.SetKeyName(7, "3dshell_icon");
            this.iconsList.Images.SetKeyName(8, "user_icon");
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Size = new System.Drawing.Size(1180, 254);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1180, 254);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statusSentLabel,
            this.statusRecievedLabel,
            this.statusCPULabel,
            this.statusRAMLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 296);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(327, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(81, 17);
            this.toolStripStatusLabel1.Text = "Status: Offline";
            // 
            // statusSentLabel
            // 
            this.statusSentLabel.Name = "statusSentLabel";
            this.statusSentLabel.Size = new System.Drawing.Size(49, 17);
            this.statusSentLabel.Text = "Sent: 0B";
            // 
            // statusRecievedLabel
            // 
            this.statusRecievedLabel.Name = "statusRecievedLabel";
            this.statusRecievedLabel.Size = new System.Drawing.Size(73, 17);
            this.statusRecievedLabel.Text = "Recieved: 0B";
            // 
            // statusCPULabel
            // 
            this.statusCPULabel.Name = "statusCPULabel";
            this.statusCPULabel.Size = new System.Drawing.Size(52, 17);
            this.statusCPULabel.Text = "CPU: 0%";
            // 
            // statusRAMLabel
            // 
            this.statusRAMLabel.Name = "statusRAMLabel";
            this.statusRAMLabel.Size = new System.Drawing.Size(55, 17);
            this.statusRAMLabel.Text = "RAM: 0%";
            // 
            // listUpdate
            // 
            this.listUpdate.Interval = 3000;
            this.listUpdate.Tick += new System.EventHandler(this.listUpdate_Tick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1298, 282);
            this.panel1.TabIndex = 4;
            // 
            // statusUpdate
            // 
            this.statusUpdate.Interval = 1000;
            this.statusUpdate.Tick += new System.EventHandler(this.statusUpdate_Tick);
            // 
            // systemTray
            // 
            this.systemTray.BalloonTipText = "Right Click for options";
            this.systemTray.BalloonTipTitle = "Kontrol 2 Server - Running";
            this.systemTray.ContextMenuStrip = this.notifyStrip;
            this.systemTray.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTray.Icon")));
            this.systemTray.Text = "Kontrol 2 Server";
            this.systemTray.Visible = true;
            // 
            // notifyStrip
            // 
            this.notifyStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.notifyStrip.Name = "notifyStrip";
            this.notifyStrip.Size = new System.Drawing.Size(104, 48);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 318);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReKode Kontrol 2 - Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.mainTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.listViewStrip.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.notifyStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl mainTab;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView ClientsView;
		private System.Windows.Forms.ColumnHeader c_id;
		private System.Windows.Forms.ColumnHeader c_ipAddress;
		private System.Windows.Forms.ColumnHeader c_geoLoc;
		private System.Windows.Forms.ColumnHeader c_machineName;
		private System.Windows.Forms.ColumnHeader c_Time;
		private System.Windows.Forms.ColumnHeader c_user;
		private System.Windows.Forms.ColumnHeader c_os;
		private System.Windows.Forms.ColumnHeader c_hwid;
		private System.Windows.Forms.ColumnHeader c_clientVersion;
		private System.Windows.Forms.ColumnHeader c_privileges;
		private System.Windows.Forms.ColumnHeader c_activeWindow;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel statusSentLabel;
		private System.Windows.Forms.ToolStripStatusLabel statusRecievedLabel;
		private System.Windows.Forms.ToolStripStatusLabel statusCPULabel;
		private System.Windows.Forms.ToolStripStatusLabel statusRAMLabel;
		private System.Windows.Forms.Timer listUpdate;
		private System.Windows.Forms.ContextMenuStrip listViewStrip;
		private System.Windows.Forms.ToolStripMenuItem funMenuToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sendMessageBoxToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem playSystemSoundToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem microsoftTextToSpeechToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem playSinewaveFrequencyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem monitoringToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem proccessManagerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hideWindowsElementsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miscToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem remoteShellToolStripMenuItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripMenuItem fileManagerToolStripMenuItem;
		private System.Windows.Forms.ImageList iconsList;
		private System.Windows.Forms.Timer statusUpdate;
		private System.Windows.Forms.ToolStripMenuItem remoteDesktopToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem remoteWebcamToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem remoteAudioToolStripMenuItem;
		private System.Windows.Forms.NotifyIcon systemTray;
		private System.Windows.Forms.ContextMenuStrip notifyStrip;
		private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.ToolStripMenuItem manageClientToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem shutdownToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sleepToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uninstallToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showLocationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sendCommandToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem visitWebsiteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem chatToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem keyloggerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem compileCScriptToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem builderToolStripMenuItem;
    }
}

