
namespace Kontrol_2_Server
{
	partial class MsgBoxForm
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
			this.soundCheck = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.msgBox = new System.Windows.Forms.TextBox();
			this.titleBox = new System.Windows.Forms.TextBox();
			this.buttonsCombo = new System.Windows.Forms.ComboBox();
			this.iconCombo = new System.Windows.Forms.ComboBox();
			this.sendButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.soundCheck);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.msgBox);
			this.groupBox1.Controls.Add(this.titleBox);
			this.groupBox1.Controls.Add(this.buttonsCombo);
			this.groupBox1.Controls.Add(this.iconCombo);
			this.groupBox1.Controls.Add(this.sendButton);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(331, 314);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Message box settings";
			// 
			// soundCheck
			// 
			this.soundCheck.AutoSize = true;
			this.soundCheck.Location = new System.Drawing.Point(80, 134);
			this.soundCheck.Name = "soundCheck";
			this.soundCheck.Size = new System.Drawing.Size(15, 14);
			this.soundCheck.TabIndex = 11;
			this.soundCheck.UseVisualStyleBackColor = true;
			this.soundCheck.CheckedChanged += new System.EventHandler(this.soundCheck_CheckedChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 133);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(68, 15);
			this.label5.TabIndex = 10;
			this.label5.Text = "Play sound:";
			// 
			// msgBox
			// 
			this.msgBox.Location = new System.Drawing.Point(80, 168);
			this.msgBox.Multiline = true;
			this.msgBox.Name = "msgBox";
			this.msgBox.Size = new System.Drawing.Size(245, 92);
			this.msgBox.TabIndex = 9;
			// 
			// titleBox
			// 
			this.titleBox.Location = new System.Drawing.Point(80, 25);
			this.titleBox.Name = "titleBox";
			this.titleBox.Size = new System.Drawing.Size(245, 23);
			this.titleBox.TabIndex = 8;
			// 
			// buttonsCombo
			// 
			this.buttonsCombo.FormattingEnabled = true;
			this.buttonsCombo.Items.AddRange(new object[] {
            "Abort Retry Ignore",
            "Ok",
            "Ok Cancel",
            "Retry Cancel",
            "Yes No",
            "Yes No Cancel"});
			this.buttonsCombo.Location = new System.Drawing.Point(80, 93);
			this.buttonsCombo.Name = "buttonsCombo";
			this.buttonsCombo.Size = new System.Drawing.Size(245, 23);
			this.buttonsCombo.TabIndex = 7;
			this.buttonsCombo.Text = "Default (Ok)";
			// 
			// iconCombo
			// 
			this.iconCombo.FormattingEnabled = true;
			this.iconCombo.Items.AddRange(new object[] {
            "Asterik",
            "Error",
            "Exclamation mark",
            "Hand",
            "Information",
            "None",
            "Question",
            "Stop",
            "Warning"});
			this.iconCombo.Location = new System.Drawing.Point(80, 58);
			this.iconCombo.Name = "iconCombo";
			this.iconCombo.Size = new System.Drawing.Size(245, 23);
			this.iconCombo.TabIndex = 6;
			this.iconCombo.Text = "Default (None)";
			// 
			// sendButton
			// 
			this.sendButton.Location = new System.Drawing.Point(6, 274);
			this.sendButton.Name = "sendButton";
			this.sendButton.Size = new System.Drawing.Size(319, 33);
			this.sendButton.TabIndex = 4;
			this.sendButton.Text = "Send";
			this.sendButton.UseVisualStyleBackColor = true;
			this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 168);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 15);
			this.label4.TabIndex = 3;
			this.label4.Text = "Message:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "Buttons";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Icon:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Title:";
			// 
			// MsgBoxForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(355, 335);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MsgBoxForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Kontrol 2 - Send a message box";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MsgBoxForm_FormClosing);
			this.Load += new System.EventHandler(this.MsgBoxForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox buttonsCombo;
		private System.Windows.Forms.ComboBox iconCombo;
		private System.Windows.Forms.Button sendButton;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox msgBox;
		private System.Windows.Forms.TextBox titleBox;
		private System.Windows.Forms.CheckBox soundCheck;
		private System.Windows.Forms.Label label5;
	}
}