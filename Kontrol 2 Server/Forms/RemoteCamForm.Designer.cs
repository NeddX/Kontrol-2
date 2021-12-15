﻿namespace Kontrol_2_Server
{
	partial class RemoteCamForm
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.recButton = new System.Windows.Forms.Button();
			this.resCombo = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.videoBox = new AForge.Controls.PictureBox();
			this.devicesCombo = new System.Windows.Forms.ComboBox();
			this.startButton = new System.Windows.Forms.Button();
			this.videoEncoder = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.videoBox)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.recButton);
			this.panel1.Controls.Add(this.resCombo);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.videoBox);
			this.panel1.Controls.Add(this.devicesCombo);
			this.panel1.Controls.Add(this.startButton);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(600, 297);
			this.panel1.TabIndex = 0;
			// 
			// recButton
			// 
			this.recButton.Enabled = false;
			this.recButton.Location = new System.Drawing.Point(84, 4);
			this.recButton.Name = "recButton";
			this.recButton.Size = new System.Drawing.Size(75, 23);
			this.recButton.TabIndex = 6;
			this.recButton.Text = "Record";
			this.recButton.UseVisualStyleBackColor = true;
			this.recButton.Click += new System.EventHandler(this.recButton_Click);
			// 
			// resCombo
			// 
			this.resCombo.FormattingEnabled = true;
			this.resCombo.Location = new System.Drawing.Point(246, 5);
			this.resCombo.Name = "resCombo";
			this.resCombo.Size = new System.Drawing.Size(170, 23);
			this.resCombo.TabIndex = 5;
			// 
			// button1
			// 
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(165, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Snapshot";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// videoBox
			// 
			this.videoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.videoBox.Image = null;
			this.videoBox.Location = new System.Drawing.Point(0, 33);
			this.videoBox.Name = "videoBox";
			this.videoBox.Size = new System.Drawing.Size(600, 264);
			this.videoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.videoBox.TabIndex = 3;
			this.videoBox.TabStop = false;
			// 
			// devicesCombo
			// 
			this.devicesCombo.FormattingEnabled = true;
			this.devicesCombo.Location = new System.Drawing.Point(422, 5);
			this.devicesCombo.Name = "devicesCombo";
			this.devicesCombo.Size = new System.Drawing.Size(170, 23);
			this.devicesCombo.TabIndex = 2;
			this.devicesCombo.SelectedIndexChanged += new System.EventHandler(this.devicesCombo_SelectedIndexChanged);
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(3, 3);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(75, 23);
			this.startButton.TabIndex = 0;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// videoEncoder
			// 
			this.videoEncoder.Interval = 10;
			this.videoEncoder.Tick += new System.EventHandler(this.videoEncoder_Tick);
			// 
			// RemoteCamForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 321);
			this.Controls.Add(this.panel1);
			this.Name = "RemoteCamForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Kontrol 2 - Remote Cam";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteCamForm_FormClosing);
			this.Load += new System.EventHandler(this.RemoteCamForm_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.videoBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private AForge.Controls.PictureBox videoBox;
		private System.Windows.Forms.ComboBox devicesCombo;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox resCombo;
		private System.Windows.Forms.Button recButton;
		private System.Windows.Forms.Timer videoEncoder;
	}
}