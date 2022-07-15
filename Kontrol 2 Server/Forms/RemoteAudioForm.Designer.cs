using System.Drawing;

namespace Kontrol_2_Server
{
	partial class RemoteAudioForm
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
			this.waveViewer = new Kontrol_2_Server.CustomWaveViewer();
			this.devicesCombo = new System.Windows.Forms.ComboBox();
			this.recButton = new System.Windows.Forms.Button();
			this.startButton = new System.Windows.Forms.Button();
			this.WaveRenderer = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.waveViewer);
			this.panel1.Controls.Add(this.devicesCombo);
			this.panel1.Controls.Add(this.recButton);
			this.panel1.Controls.Add(this.startButton);
			this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.panel1.Location = new System.Drawing.Point(14, 12);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(597, 336);
			this.panel1.TabIndex = 0;
			// 
			// waveViewer
			// 
			this.waveViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.waveViewer.Location = new System.Drawing.Point(-2, 32);
			this.waveViewer.Name = "waveViewer";
			this.waveViewer.PenColor = System.Drawing.Color.Crimson;
			this.waveViewer.PenWidth = 1F;
			this.waveViewer.SamplesPerPixel = 128;
			this.waveViewer.Size = new System.Drawing.Size(599, 304);
			this.waveViewer.StartPosition = ((long)(0));
			this.waveViewer.TabIndex = 8;
			this.waveViewer.WaveStream = null;
			// 
			// devicesCombo
			// 
			this.devicesCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.devicesCombo.Enabled = false;
			this.devicesCombo.FormattingEnabled = true;
			this.devicesCombo.Location = new System.Drawing.Point(167, 3);
			this.devicesCombo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.devicesCombo.Name = "devicesCombo";
			this.devicesCombo.Size = new System.Drawing.Size(426, 23);
			this.devicesCombo.TabIndex = 7;
			// 
			// recButton
			// 
			this.recButton.Location = new System.Drawing.Point(84, 3);
			this.recButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.recButton.Name = "recButton";
			this.recButton.Size = new System.Drawing.Size(75, 23);
			this.recButton.TabIndex = 1;
			this.recButton.Text = "Record";
			this.recButton.UseVisualStyleBackColor = true;
			this.recButton.Click += new System.EventHandler(this.recButton_Click);
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(4, 3);
			this.startButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(75, 23);
			this.startButton.TabIndex = 0;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// WaveRenderer
			// 
			this.WaveRenderer.Tick += new System.EventHandler(this.WaveRenderer_Tick);
			// 
			// RemoteAudioForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 361);
			this.Controls.Add(this.panel1);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "RemoteAudioForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RemoteAudioForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteAudioForm_FormClosing);
			this.Load += new System.EventHandler(this.RemoteAudioForm_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button recButton;
		private System.Windows.Forms.ComboBox devicesCombo;
		private System.Windows.Forms.Timer WaveRenderer;
		private CustomWaveViewer waveViewer;
	}
}