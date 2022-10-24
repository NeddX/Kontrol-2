using System.ComponentModel;

namespace Kontrol_2_Server
{
    partial class RemoteDesktopForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.qualityBox = new System.Windows.Forms.TextBox();
            this.resCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fullButton = new System.Windows.Forms.Button();
            this.keyboardCheck = new System.Windows.Forms.CheckBox();
            this.mouseCheck = new System.Windows.Forms.CheckBox();
            this.videoBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.recButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.fpsCounter = new System.Windows.Forms.Timer(this.components);
            this.moueTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.videoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.qualityBox);
            this.panel1.Controls.Add(this.resCombo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.fullButton);
            this.panel1.Controls.Add(this.keyboardCheck);
            this.panel1.Controls.Add(this.mouseCheck);
            this.panel1.Controls.Add(this.videoBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.recButton);
            this.panel1.Controls.Add(this.startButton);
            this.panel1.Location = new System.Drawing.Point(14, 14);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(597, 333);
            this.panel1.TabIndex = 0;
            // 
            // qualityBox
            // 
            this.qualityBox.Location = new System.Drawing.Point(507, 5);
            this.qualityBox.MaxLength = 3;
            this.qualityBox.Name = "qualityBox";
            this.qualityBox.Size = new System.Drawing.Size(85, 23);
            this.qualityBox.TabIndex = 20;
            this.qualityBox.Text = "50";
            this.qualityBox.TextChanged += new System.EventHandler(this.qualityBox_TextChanged);
            // 
            // resCombo
            // 
            this.resCombo.DisplayMember = "dd";
            this.resCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resCombo.FormattingEnabled = true;
            this.resCombo.Items.AddRange(new object[] {
            "640 x 480",
            "1280 x 720",
            "1920 x 1080",
            "2560 x 1440",
            "3840 x 2160",
            "Native Resolution"});
            this.resCombo.Location = new System.Drawing.Point(240, 5);
            this.resCombo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.resCombo.Name = "resCombo";
            this.resCombo.Size = new System.Drawing.Size(131, 23);
            this.resCombo.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(167, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Resolution:";
            // 
            // fullButton
            // 
            this.fullButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fullButton.Location = new System.Drawing.Point(252, 307);
            this.fullButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fullButton.Name = "fullButton";
            this.fullButton.Size = new System.Drawing.Size(139, 23);
            this.fullButton.TabIndex = 17;
            this.fullButton.Text = "Fullscreen mode";
            this.fullButton.UseVisualStyleBackColor = true;
            // 
            // keyboardCheck
            // 
            this.keyboardCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.keyboardCheck.AutoSize = true;
            this.keyboardCheck.Location = new System.Drawing.Point(120, 311);
            this.keyboardCheck.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.keyboardCheck.Name = "keyboardCheck";
            this.keyboardCheck.Size = new System.Drawing.Size(119, 19);
            this.keyboardCheck.TabIndex = 16;
            this.keyboardCheck.Text = "Keyboard Control";
            this.keyboardCheck.UseVisualStyleBackColor = true;
            this.keyboardCheck.CheckedChanged += new System.EventHandler(this.keyboardCheck_CheckedChanged);
            // 
            // mouseCheck
            // 
            this.mouseCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mouseCheck.AutoSize = true;
            this.mouseCheck.Location = new System.Drawing.Point(4, 311);
            this.mouseCheck.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mouseCheck.Name = "mouseCheck";
            this.mouseCheck.Size = new System.Drawing.Size(105, 19);
            this.mouseCheck.TabIndex = 15;
            this.mouseCheck.Text = "Mouse Control";
            this.mouseCheck.UseVisualStyleBackColor = true;
            this.mouseCheck.CheckedChanged += new System.EventHandler(this.mouseCheck_CheckedChanged);
            // 
            // videoBox
            // 
            this.videoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.videoBox.Location = new System.Drawing.Point(0, 33);
            this.videoBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.videoBox.Name = "videoBox";
            this.videoBox.Size = new System.Drawing.Size(597, 270);
            this.videoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.videoBox.TabIndex = 14;
            this.videoBox.TabStop = false;
            this.videoBox.Click += new System.EventHandler(this.videoBox_Click);
            this.videoBox.MouseEnter += new System.EventHandler(this.videoBox_MouseEnter);
            this.videoBox.MouseLeave += new System.EventHandler(this.videoBox_MouseLeave);
            this.videoBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.videoBox_MouseMove);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(452, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "Quality:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(379, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "FPS:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(416, 3);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(28, 23);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "30";
            // 
            // recButton
            // 
            this.recButton.Enabled = false;
            this.recButton.Location = new System.Drawing.Point(85, 3);
            this.recButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.recButton.Name = "recButton";
            this.recButton.Size = new System.Drawing.Size(75, 23);
            this.recButton.TabIndex = 7;
            this.recButton.Text = "Record";
            this.recButton.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(4, 3);
            this.startButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // fpsCounter
            // 
            this.fpsCounter.Tick += new System.EventHandler(this.fpsCounter_Tick);
            // 
            // moueTimer
            // 
            this.moueTimer.Interval = 50;
            this.moueTimer.Tick += new System.EventHandler(this.moueTimer_Tick);
            // 
            // RemoteDesktopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 361);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "RemoteDesktopForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kontrol 2 - Remote Desktop";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteDesktopForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RemoteDesktopForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RemoteDesktopForm_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.videoBox)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.PictureBox videoBox;

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button fullButton;
        private System.Windows.Forms.CheckBox mouseCheck;
        private System.Windows.Forms.CheckBox keyboardCheck;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;

        private System.Windows.Forms.Button recButton;

        private System.Windows.Forms.Button startButton;

        private System.Windows.Forms.Panel panel1;

		#endregion

		private System.Windows.Forms.ComboBox resCombo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox qualityBox;
		private System.Windows.Forms.Timer fpsCounter;
        private System.Windows.Forms.Timer moueTimer;
    }
}