namespace Kontrol_2_Server
{
	partial class ProgressBarForm
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
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.panel2 = new System.Windows.Forms.Panel();
			this.taskLabel = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.percentageLabel = new System.Windows.Forms.Label();
			this.updater = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.progressBar);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(293, 95);
			this.panel1.TabIndex = 0;
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(0, 36);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(293, 23);
			this.progressBar.TabIndex = 1;
			this.progressBar.Value = 70;
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.taskLabel);
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(293, 30);
			this.panel2.TabIndex = 0;
			// 
			// taskLabel
			// 
			this.taskLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.taskLabel.Location = new System.Drawing.Point(0, 0);
			this.taskLabel.Name = "taskLabel";
			this.taskLabel.Size = new System.Drawing.Size(293, 30);
			this.taskLabel.TabIndex = 0;
			this.taskLabel.Text = "taskLabel";
			this.taskLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.Controls.Add(this.percentageLabel);
			this.panel3.Location = new System.Drawing.Point(12, 77);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(293, 30);
			this.panel3.TabIndex = 1;
			// 
			// percentageLabel
			// 
			this.percentageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.percentageLabel.Location = new System.Drawing.Point(0, 0);
			this.percentageLabel.Name = "percentageLabel";
			this.percentageLabel.Size = new System.Drawing.Size(293, 30);
			this.percentageLabel.TabIndex = 0;
			this.percentageLabel.Text = "0%";
			this.percentageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// updater
			// 
			this.updater.Interval = 500;
			this.updater.Tick += new System.EventHandler(this.updater_Tick);
			// 
			// ProgressBarForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(317, 119);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "ProgressBarForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TITLE NAME";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressBarForm_FormClosing);
			this.Load += new System.EventHandler(this.ProgressBarForm_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Timer updater;
		private System.Windows.Forms.Label taskLabel;
		private System.Windows.Forms.Label percentageLabel;
	}
}