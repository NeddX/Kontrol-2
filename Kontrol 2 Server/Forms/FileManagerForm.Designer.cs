namespace Kontrol_2_Server
{
	partial class FileManagerForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileManagerForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.rootTree = new System.Windows.Forms.TreeView();
			this.iconList = new System.Windows.Forms.ImageList(this.components);
			this.filesList = new System.Windows.Forms.ListView();
			this.fileName = new System.Windows.Forms.ColumnHeader();
			this.fileSize = new System.Windows.Forms.ColumnHeader();
			this.creationDate = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.folderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.prevButton = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.backButton = new System.Windows.Forms.Button();
			this.pathBox = new System.Windows.Forms.TextBox();
			this.updateFiles = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.statusStrip);
			this.panel1.Controls.Add(this.rootTree);
			this.panel1.Controls.Add(this.filesList);
			this.panel1.Controls.Add(this.prevButton);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.backButton);
			this.panel1.Controls.Add(this.pathBox);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(776, 426);
			this.panel1.TabIndex = 0;
			// 
			// statusStrip
			// 
			this.statusStrip.Location = new System.Drawing.Point(0, 404);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(776, 22);
			this.statusStrip.TabIndex = 18;
			this.statusStrip.Text = "statusStrip1";
			// 
			// rootTree
			// 
			this.rootTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.rootTree.ImageIndex = 0;
			this.rootTree.ImageList = this.iconList;
			this.rootTree.Location = new System.Drawing.Point(0, 29);
			this.rootTree.Name = "rootTree";
			this.rootTree.SelectedImageIndex = 0;
			this.rootTree.Size = new System.Drawing.Size(150, 370);
			this.rootTree.TabIndex = 17;
			this.rootTree.DoubleClick += new System.EventHandler(this.rootTree_DoubleClick);
			// 
			// iconList
			// 
			this.iconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
			this.iconList.TransparentColor = System.Drawing.Color.Transparent;
			this.iconList.Images.SetKeyName(0, "folderIcon");
			this.iconList.Images.SetKeyName(1, "textIcon");
			this.iconList.Images.SetKeyName(2, "driveIcon");
			this.iconList.Images.SetKeyName(3, "pcIcon");
			// 
			// filesList
			// 
			this.filesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.filesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileName,
            this.fileSize,
            this.creationDate});
			this.filesList.ContextMenuStrip = this.contextMenuStrip1;
			this.filesList.FullRowSelect = true;
			this.filesList.LabelEdit = true;
			this.filesList.LargeImageList = this.iconList;
			this.filesList.Location = new System.Drawing.Point(156, 29);
			this.filesList.MultiSelect = false;
			this.filesList.Name = "filesList";
			this.filesList.Size = new System.Drawing.Size(620, 370);
			this.filesList.SmallImageList = this.iconList;
			this.filesList.TabIndex = 16;
			this.filesList.UseCompatibleStateImageBehavior = false;
			this.filesList.View = System.Windows.Forms.View.Details;
			this.filesList.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.filesList_AfterLabelEdit);
			this.filesList.SelectedIndexChanged += new System.EventHandler(this.filesList_SelectedIndexChanged);
			this.filesList.DoubleClick += new System.EventHandler(this.filesList_DoubleClick);
			// 
			// fileName
			// 
			this.fileName.Text = "File Name";
			this.fileName.Width = 140;
			// 
			// fileSize
			// 
			this.fileSize.Text = "File Size";
			this.fileSize.Width = 80;
			// 
			// creationDate
			// 
			this.creationDate.Text = "Creation Date";
			this.creationDate.Width = 395;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.editToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.moveToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.createToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.uploadToolStripMenuItem,
            this.propertiesToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(129, 268);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
			// 
			// moveToolStripMenuItem
			// 
			this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
			this.moveToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.moveToolStripMenuItem.Text = "Move";
			this.moveToolStripMenuItem.Click += new System.EventHandler(this.moveToolStripMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.renameToolStripMenuItem.Text = "Rename";
			this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
			// 
			// createToolStripMenuItem
			// 
			this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.folderToolStripMenuItem});
			this.createToolStripMenuItem.Name = "createToolStripMenuItem";
			this.createToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.createToolStripMenuItem.Text = "Create";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.fileToolStripMenuItem.Text = "File";
			this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
			// 
			// folderToolStripMenuItem
			// 
			this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
			this.folderToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.folderToolStripMenuItem.Text = "Folder";
			this.folderToolStripMenuItem.Click += new System.EventHandler(this.folderToolStripMenuItem_Click);
			// 
			// downloadToolStripMenuItem
			// 
			this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
			this.downloadToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.downloadToolStripMenuItem.Text = "Download";
			this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
			// 
			// uploadToolStripMenuItem
			// 
			this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
			this.uploadToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.uploadToolStripMenuItem.Text = "Upload";
			this.uploadToolStripMenuItem.Click += new System.EventHandler(this.uploadToolStripMenuItem_Click);
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.propertiesToolStripMenuItem.Text = "Propetries";
			// 
			// prevButton
			// 
			this.prevButton.Location = new System.Drawing.Point(52, -1);
			this.prevButton.Name = "prevButton";
			this.prevButton.Size = new System.Drawing.Size(46, 23);
			this.prevButton.TabIndex = 15;
			this.prevButton.Text = ">";
			this.prevButton.UseVisualStyleBackColor = true;
			this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(104, -1);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(46, 23);
			this.button1.TabIndex = 14;
			this.button1.Text = "^";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// backButton
			// 
			this.backButton.Location = new System.Drawing.Point(0, -1);
			this.backButton.Name = "backButton";
			this.backButton.Size = new System.Drawing.Size(46, 23);
			this.backButton.TabIndex = 13;
			this.backButton.Text = "<";
			this.backButton.UseVisualStyleBackColor = true;
			this.backButton.Click += new System.EventHandler(this.backButton_Click);
			// 
			// pathBox
			// 
			this.pathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pathBox.Location = new System.Drawing.Point(156, 0);
			this.pathBox.Name = "pathBox";
			this.pathBox.Size = new System.Drawing.Size(620, 23);
			this.pathBox.TabIndex = 0;
			this.pathBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pathBox_KeyDown);
			// 
			// updateFiles
			// 
			this.updateFiles.Interval = 5000;
			this.updateFiles.Tick += new System.EventHandler(this.updateFiles_Tick);
			// 
			// FileManagerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.panel1);
			this.Name = "FileManagerForm";
			this.Text = "Kontrol 2 - File Manager";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileManagerForm_FormClosing);
			this.Load += new System.EventHandler(this.FileManagerForm_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button prevButton;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button backButton;
		private System.Windows.Forms.TextBox pathBox;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
		private System.Windows.Forms.Timer updateFiles;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem folderToolStripMenuItem;
		private System.Windows.Forms.ListView filesList;
		private System.Windows.Forms.ColumnHeader fileName;
		private System.Windows.Forms.ColumnHeader fileSize;
		private System.Windows.Forms.ColumnHeader creationDate;
		private System.Windows.Forms.ImageList iconList;
		private System.Windows.Forms.ToolStripMenuItem uploadToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
		private System.Windows.Forms.TreeView rootTree;
		private System.Windows.Forms.StatusStrip statusStrip;
	}
}