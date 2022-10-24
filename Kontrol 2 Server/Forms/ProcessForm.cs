using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Kontrol_2_Server
{
    public partial class ProcessForm : Form
    {
        public int clientId = 0;
        public bool upload = false;
        public ProcessForm()
        {
            InitializeComponent();
            processList.DoubleBuffering(true);
        }
        public void setProcessInfo(string processInfo)
        {
            if (this.InvokeRequired)
            {
                processList.Invoke(new MethodInvoker(delegate
                {
                    string[] xsplit = processInfo.Split('\n');
                    string[] row = { xsplit[1], xsplit[2], xsplit[3], xsplit[4], xsplit[5], xsplit[6] };
                    ListViewItem lv = new ListViewItem(row);
                    lv.ImageIndex = 0;
                    processList.Items.Add(lv);
                }));
            }
            else
            {
                string[] xsplit = processInfo.Split('\n');
                string[] row = { xsplit[1], xsplit[2], xsplit[3], xsplit[4], xsplit[5], xsplit[6] };
                ListViewItem lv = new ListViewItem(row);
                lv.ImageIndex = 0;
                processList.Items.Add(lv);
            }
        }
        private void UpdateList_Tick(object sender, EventArgs e)
        {
            processList.Invoke(new MethodInvoker(delegate
            {
                processList.Items.Clear();
            }));
            try
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("list_proceses", clientId);
                    }
                    catch { MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error); this.Close(); }
                }).Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void ProcessForm_Load(object sender, EventArgs e)
        {
            //UpdateList.Start(); 
            processList.Items.Clear();
            try
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("list_proceses", clientId);
                    }
                    catch { MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error); this.Close(); }
                }).Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                processList.Invoke(new MethodInvoker(delegate
                {
                    processList.Items.Clear();
                }));
            }
            else
            {
                processList.Items.Clear();
            }
            try
            {
                new Thread(() =>
                {
                    try
                    {
                        MainForm.SendCommand("list_proceses", clientId);
                    }
                    catch { MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error); this.Close(); }
                }).Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void ProcessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateList.Stop();
            MainForm.pmf = new ProcessForm();
            this.Dispose();
        }

        private void killToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (processList.SelectedIndices.Count > 0)
                {
                    MainForm.SendCommand("kill_process\n" + processList.Items[processList.SelectedIndices[0]].SubItems[1].Text, clientId);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void ProcessForm_Shown(object sender, EventArgs e)
        {

        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (!upload)
            {
                string fileContent;
                using (OpenFileDialog fd = new OpenFileDialog())
                {
                    fd.InitialDirectory = "c:\\";
                    fd.FilterIndex = 1;
                    fd.RestoreDirectory = true;

                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        uploadBox.Text = fd.FileName;

                        //Read the contents of the file into a stream
                        var fileStream = fd.OpenFile();

                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            fileContent = reader.ReadToEnd();
                        }
                    }
                }
                browseButton.Text = "Unload";
                upload = true;
            }
            else
            {
                browseButton.Text = "Browse";
                uploadBox.Text = "";
                upload = false;
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!upload)
                {
                    MainForm.SendCommand("create_process\n" + visibilityCombo.SelectedIndex + "\n" + Path.Combine(pathBox.Text) + "\n" + argBox.Text + "\n" + asciiCheck.Checked + "\n" + shellCheck.Checked, clientId);
                }
                else //Will come back to this later
                {
                    if (memoryCheck.Checked) //Run from memory
                    {

                    }
                    else //Run from disk
                    {

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
