
namespace Kontrol_2_Server
{
	partial class ProcessForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessForm));
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.processList = new System.Windows.Forms.ListView();
			this.p_ProcessName = new System.Windows.Forms.ColumnHeader();
			this.p_PID = new System.Windows.Forms.ColumnHeader();
			this.p_Responding = new System.Windows.Forms.ColumnHeader();
			this.p_Title = new System.Windows.Forms.ColumnHeader();
			this.p_Priority = new System.Windows.Forms.ColumnHeader();
			this.p_Path = new System.Windows.Forms.ColumnHeader();
			this.processMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.killToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.shellCheck = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.asciiCheck = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.argBox = new System.Windows.Forms.TextBox();
			this.uploadBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.memoryCheck = new System.Windows.Forms.CheckBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.createButton = new System.Windows.Forms.Button();
			this.visibilityCombo = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.pathBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.UpdateList = new System.Windows.Forms.Timer(this.components);
			this.iconList = new System.Windows.Forms.ImageList(this.components);
			this.groupBox2.SuspendLayout();
			this.processMenuStrip.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.processList);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(776, 417);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Processes";
			// 
			// processList
			// 
			this.processList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.p_ProcessName,
            this.p_PID,
            this.p_Responding,
            this.p_Title,
            this.p_Priority,
            this.p_Path});
			this.processList.ContextMenuStrip = this.processMenuStrip;
			this.processList.FullRowSelect = true;
			this.processList.GridLines = true;
			this.processList.Location = new System.Drawing.Point(6, 15);
			this.processList.MultiSelect = false;
			this.processList.Name = "processList";
			this.processList.Size = new System.Drawing.Size(764, 396);
			this.processList.SmallImageList = this.iconList;
			this.processList.TabIndex = 0;
			this.processList.UseCompatibleStateImageBehavior = false;
			this.processList.View = System.Windows.Forms.View.Details;
			// 
			// p_ProcessName
			// 
			this.p_ProcessName.Text = "Process Name";
			this.p_ProcessName.Width = 120;
			// 
			// p_PID
			// 
			this.p_PID.Text = "PID";
			// 
			// p_Responding
			// 
			this.p_Responding.Text = "Responding";
			this.p_Responding.Width = 120;
			// 
			// p_Title
			// 
			this.p_Title.Text = "Title";
			this.p_Title.Width = 120;
			// 
			// p_Priority
			// 
			this.p_Priority.Text = "Priority";
			this.p_Priority.Width = 120;
			// 
			// p_Path
			// 
			this.p_Path.Text = "Path";
			this.p_Path.Width = 200;
			// 
			// processMenuStrip
			// 
			this.processMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.killToolStripMenuItem});
			this.processMenuStrip.Name = "contextMenuStrip1";
			this.processMenuStrip.Size = new System.Drawing.Size(114, 48);
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
			// 
			// killToolStripMenuItem
			// 
			this.killToolStripMenuItem.Name = "killToolStripMenuItem";
			this.killToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.killToolStripMenuItem.Text = "Kill";
			this.killToolStripMenuItem.Click += new System.EventHandler(this.killToolStripMenuItem_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.shellCheck);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.asciiCheck);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.argBox);
			this.groupBox1.Controls.Add(this.uploadBox);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.memoryCheck);
			this.groupBox1.Controls.Add(this.browseButton);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.createButton);
			this.groupBox1.Controls.Add(this.visibilityCombo);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.pathBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 429);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(770, 268);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Create process";
			// 
			// shellCheck
			// 
			this.shellCheck.AutoSize = true;
			this.shellCheck.Location = new System.Drawing.Point(128, 200);
			this.shellCheck.Name = "shellCheck";
			this.shellCheck.Size = new System.Drawing.Size(15, 14);
			this.shellCheck.TabIndex = 23;
			this.shellCheck.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 199);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 15);
			this.label7.TabIndex = 21;
			this.label7.Text = "Use shell execute:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 170);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(116, 15);
			this.label6.TabIndex = 18;
			this.label6.Text = "Unicode Parameters:";
			// 
			// asciiCheck
			// 
			this.asciiCheck.AutoSize = true;
			this.asciiCheck.Location = new System.Drawing.Point(128, 171);
			this.asciiCheck.Name = "asciiCheck";
			this.asciiCheck.Size = new System.Drawing.Size(15, 14);
			this.asciiCheck.TabIndex = 17;
			this.asciiCheck.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 83);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(69, 15);
			this.label5.TabIndex = 16;
			this.label5.Text = "Arguments:";
			// 
			// argBox
			// 
			this.argBox.Location = new System.Drawing.Point(128, 80);
			this.argBox.Name = "argBox";
			this.argBox.Size = new System.Drawing.Size(636, 23);
			this.argBox.TabIndex = 15;
			// 
			// uploadBox
			// 
			this.uploadBox.Location = new System.Drawing.Point(128, 51);
			this.uploadBox.Name = "uploadBox";
			this.uploadBox.ReadOnly = true;
			this.uploadBox.Size = new System.Drawing.Size(509, 23);
			this.uploadBox.TabIndex = 14;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 142);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(108, 15);
			this.label3.TabIndex = 13;
			this.label3.Text = "Run from memory:";
			// 
			// memoryCheck
			// 
			this.memoryCheck.AutoSize = true;
			this.memoryCheck.Location = new System.Drawing.Point(128, 143);
			this.memoryCheck.Name = "memoryCheck";
			this.memoryCheck.Size = new System.Drawing.Size(15, 14);
			this.memoryCheck.TabIndex = 12;
			this.memoryCheck.UseVisualStyleBackColor = true;
			// 
			// browseButton
			// 
			this.browseButton.Location = new System.Drawing.Point(643, 51);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new System.Drawing.Size(121, 23);
			this.browseButton.TabIndex = 11;
			this.browseButton.Text = "Browse";
			this.browseButton.UseVisualStyleBackColor = true;
			this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 15);
			this.label2.TabIndex = 10;
			this.label2.Text = "Upload and run:";
			// 
			// createButton
			// 
			this.createButton.Location = new System.Drawing.Point(6, 220);
			this.createButton.Name = "createButton";
			this.createButton.Size = new System.Drawing.Size(758, 44);
			this.createButton.TabIndex = 9;
			this.createButton.Text = "Create Process";
			this.createButton.UseVisualStyleBackColor = true;
			this.createButton.Click += new System.EventHandler(this.createButton_Click);
			// 
			// visibilityCombo
			// 
			this.visibilityCombo.FormattingEnabled = true;
			this.visibilityCombo.Items.AddRange(new object[] {
            "Visible",
            "Hidden",
            "Maximized",
            "Minimized"});
			this.visibilityCombo.Location = new System.Drawing.Point(128, 108);
			this.visibilityCombo.Name = "visibilityCombo";
			this.visibilityCombo.Size = new System.Drawing.Size(121, 23);
			this.visibilityCombo.TabIndex = 8;
			this.visibilityCombo.Text = "Visible (default)";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(102, 15);
			this.label4.TabIndex = 7;
			this.label4.Text = "Proccess visibility:";
			// 
			// pathBox
			// 
			this.pathBox.Location = new System.Drawing.Point(128, 22);
			this.pathBox.Name = "pathBox";
			this.pathBox.Size = new System.Drawing.Size(636, 23);
			this.pathBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Path:";
			// 
			// UpdateList
			// 
			this.UpdateList.Interval = 10000;
			this.UpdateList.Tick += new System.EventHandler(this.UpdateList_Tick);
			// 
			// iconList
			// 
			this.iconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
			this.iconList.TransparentColor = System.Drawing.Color.Transparent;
			this.iconList.Images.SetKeyName(0, "exe_icon");
			// 
			// ProcessForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 710);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "ProcessForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Kontrol 2 - Process manager";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProcessForm_FormClosing);
			this.Load += new System.EventHandler(this.ProcessForm_Load);
			this.Shown += new System.EventHandler(this.ProcessForm_Shown);
			this.groupBox2.ResumeLayout(false);
			this.processMenuStrip.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListView processList;
		private System.Windows.Forms.ColumnHeader p_ProcessName;
		private System.Windows.Forms.ColumnHeader p_PID;
		private System.Windows.Forms.ColumnHeader p_Responding;
		private System.Windows.Forms.ColumnHeader p_Title;
		private System.Windows.Forms.ColumnHeader p_Priority;
		private System.Windows.Forms.ColumnHeader p_Path;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button createButton;
		private System.Windows.Forms.ComboBox visibilityCombo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox pathBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ContextMenuStrip processMenuStrip;
		private System.Windows.Forms.Timer UpdateList;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem killToolStripMenuItem;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox memoryCheck;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox uploadBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox argBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox asciiCheck;
		private System.Windows.Forms.CheckBox shellCheck;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ImageList iconList;
	}
}