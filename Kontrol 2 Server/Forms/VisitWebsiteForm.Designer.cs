namespace Kontrol_2_Server
{
    partial class VisitWebsiteForm
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
            this.siteBox = new System.Windows.Forms.TextBox();
            this.visitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // siteBox
            // 
            this.siteBox.Location = new System.Drawing.Point(12, 12);
            this.siteBox.Name = "siteBox";
            this.siteBox.Size = new System.Drawing.Size(346, 23);
            this.siteBox.TabIndex = 0;
            this.siteBox.Text = "https://neddiendrohu.ml/";
            // 
            // visitBtn
            // 
            this.visitBtn.Location = new System.Drawing.Point(12, 41);
            this.visitBtn.Name = "visitBtn";
            this.visitBtn.Size = new System.Drawing.Size(346, 23);
            this.visitBtn.TabIndex = 1;
            this.visitBtn.Text = "Visit";
            this.visitBtn.UseVisualStyleBackColor = true;
            this.visitBtn.Click += new System.EventHandler(this.visitBtn_Click);
            // 
            // VisitWebsiteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 73);
            this.Controls.Add(this.visitBtn);
            this.Controls.Add(this.siteBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisitWebsiteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kontrol 2 - Visit Website";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VisitWebsiteForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox siteBox;
        private System.Windows.Forms.Button visitBtn;
    }
}