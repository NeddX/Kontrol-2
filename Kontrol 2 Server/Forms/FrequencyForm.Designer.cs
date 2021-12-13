
namespace Kontrol_2_Server
{
	partial class FrequencyForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.startButton = new System.Windows.Forms.Button();
			this.durationBox = new System.Windows.Forms.TextBox();
			this.frequencyBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.startButton);
			this.groupBox1.Controls.Add(this.durationBox);
			this.groupBox1.Controls.Add(this.frequencyBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(253, 135);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Frequency settings";
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(6, 90);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(241, 37);
			this.startButton.TabIndex = 4;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// durationBox
			// 
			this.durationBox.Location = new System.Drawing.Point(100, 61);
			this.durationBox.Name = "durationBox";
			this.durationBox.Size = new System.Drawing.Size(147, 23);
			this.durationBox.TabIndex = 3;
			this.durationBox.Text = "60000";
			this.durationBox.TextChanged += new System.EventHandler(this.durationBox_TextChanged);
			// 
			// frequencyBox
			// 
			this.frequencyBox.Location = new System.Drawing.Point(100, 32);
			this.frequencyBox.Name = "frequencyBox";
			this.frequencyBox.Size = new System.Drawing.Size(147, 23);
			this.frequencyBox.TabIndex = 2;
			this.frequencyBox.Text = "1000";
			this.frequencyBox.TextChanged += new System.EventHandler(this.frequencyBox_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Duration (ms):";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Frequency (hz):";
			// 
			// FrequencyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(277, 154);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "FrequencyForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Play Frequency";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrequencyForm_FormClosing);
			this.Load += new System.EventHandler(this.frequencyBox_TextChanged);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrequencyForm_KeyPress);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.TextBox durationBox;
		private System.Windows.Forms.TextBox frequencyBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}