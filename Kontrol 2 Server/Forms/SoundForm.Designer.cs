
namespace Kontrol_2_Server
{
	partial class SoundForm
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
			this.fileCheck = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.loadButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.soundBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.soundCombo = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.fileCheck);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.loadButton);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.soundBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.soundCombo);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(392, 158);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Sound settings";
			// 
			// fileCheck
			// 
			this.fileCheck.AutoSize = true;
			this.fileCheck.Location = new System.Drawing.Point(120, 94);
			this.fileCheck.Name = "fileCheck";
			this.fileCheck.Size = new System.Drawing.Size(15, 14);
			this.fileCheck.TabIndex = 8;
			this.fileCheck.UseVisualStyleBackColor = true;
			this.fileCheck.CheckedChanged += new System.EventHandler(this.fileCheck_CheckedChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(6, 114);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(379, 37);
			this.button1.TabIndex = 7;
			this.button1.Text = "Send";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// loadButton
			// 
			this.loadButton.Enabled = false;
			this.loadButton.Location = new System.Drawing.Point(342, 58);
			this.loadButton.Name = "loadButton";
			this.loadButton.Size = new System.Drawing.Size(44, 23);
			this.loadButton.TabIndex = 6;
			this.loadButton.Text = "...";
			this.loadButton.UseVisualStyleBackColor = true;
			this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 95);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(87, 15);
			this.label3.TabIndex = 5;
			this.label3.Text = "Play sound file:";
			// 
			// soundBox
			// 
			this.soundBox.Enabled = false;
			this.soundBox.Location = new System.Drawing.Point(120, 58);
			this.soundBox.Name = "soundBox";
			this.soundBox.ReadOnly = true;
			this.soundBox.Size = new System.Drawing.Size(214, 23);
			this.soundBox.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "Sound file:";
			// 
			// soundCombo
			// 
			this.soundCombo.FormattingEnabled = true;
			this.soundCombo.Items.AddRange(new object[] {
            "Error",
            "Information"});
			this.soundCombo.Location = new System.Drawing.Point(120, 25);
			this.soundCombo.Name = "soundCombo";
			this.soundCombo.Size = new System.Drawing.Size(266, 23);
			this.soundCombo.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Play system sound:";
			// 
			// SoundForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(412, 179);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "SoundForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Kontrol 2 - Play sound";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SoundForm_FormClosing);
			this.Load += new System.EventHandler(this.SoundForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox soundBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox soundCombo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button loadButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox fileCheck;
	}
}