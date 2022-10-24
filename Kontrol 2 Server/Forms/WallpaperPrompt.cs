using System;
using System.Windows.Forms;

namespace Kontrol_2_Server
{
    public partial class WallpaperPrompt : Form
    {
        public byte mode = 0;

        public WallpaperPrompt()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mode = 0;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mode = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mode = 2;
            this.Close();
        }
    }
}
