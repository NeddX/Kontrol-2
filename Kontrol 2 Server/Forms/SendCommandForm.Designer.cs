namespace Kontrol_2_Server
{
	partial class SendCommandForm
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.sendBtn = new System.Windows.Forms.Button();
			this.cmdBox = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(503, 223);
			this.panel1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.sendBtn);
			this.groupBox1.Controls.Add(this.cmdBox);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(497, 217);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Send commands manually";
			// 
			// sendBtn
			// 
			this.sendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sendBtn.Location = new System.Drawing.Point(6, 162);
			this.sendBtn.Name = "sendBtn";
			this.sendBtn.Size = new System.Drawing.Size(485, 49);
			this.sendBtn.TabIndex = 1;
			this.sendBtn.Text = "Send Command";
			this.sendBtn.UseVisualStyleBackColor = true;
			this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
			// 
			// cmdBox
			// 
			this.cmdBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBox.Location = new System.Drawing.Point(6, 22);
			this.cmdBox.Multiline = true;
			this.cmdBox.Name = "cmdBox";
			this.cmdBox.Size = new System.Drawing.Size(485, 134);
			this.cmdBox.TabIndex = 0;
			this.cmdBox.Text = "display_msgbox\\nThe title\\n1\\n1\\n@{msg}Error! Blah blah\\nSecond line of blah blah" +
    "s";
			this.cmdBox.TextChanged += new System.EventHandler(this.cmdBox_TextChanged);
			// 
			// SendCommandForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(527, 247);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SendCommandForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Kontrol 2 Server - Send Command";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SendCommandForm_FormClosing);
			this.Load += new System.EventHandler(this.SendCommandForm_Load);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button sendBtn;
		private System.Windows.Forms.TextBox cmdBox;
	}
}