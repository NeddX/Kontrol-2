using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kontrol_2_Server
{
	public partial class FileManagerForm : Form
	{
		public int clientId = 0;
		private int files = 0;
		private int folders = 0;
		public string username = "N/A";
		public string prev_dir = "";
		private List<string> currentDirectoryFiles = new List<string>();
		public string fo_mode = "none";
		public string fo_src = "";
		public string fo_dest = "";
		public string fo_path = "";
		public long fo_writeSize = 0;
		public long fo_size = 0;
		public byte[] recvFile = new byte[1];


		public ProgressBar pb = new ProgressBar();

		public FileManagerForm()
		{
			InitializeComponent();
			filesList.DoubleBuffering(true); //enable double buffering to get rid of flickering n shit
			rootTree.DoubleBuffering(true); 
		}
		private void FileManagerForm_Load(object sender, EventArgs e)
		{
			try
			{
				filesList.Items.Clear();
				MainForm.SendCommand("list_drives", clientId);
				//updateFiles.Start();
			}
			catch (Exception)
			{
				MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}

		public void refreshCurrentDirectory()
		{
			try
			{
				MainForm.SendCommand("cd\n" + pathBox.Text, clientId);
				files = 0;
				folders = 0;
				filesList.Items.Clear();
				currentDirectoryFiles.Clear();
			}
			catch (Exception)
			{
				MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
				if (this.InvokeRequired)
				{
					this.Invoke(new MethodInvoker(delegate 
					{
						this.Close();
					}));
				}
				else
				{
					this.Close();
				}

			}
		}
		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(pathBox.Text) && !string.IsNullOrWhiteSpace(pathBox.Text))
				{

					refreshCurrentDirectory();
				}
				else
				{
					files = 0;
					folders = 0;
					filesList.Items.Clear();
					currentDirectoryFiles.Clear();
					MainForm.SendCommand("list_drives", clientId);
				}
			}
			catch (Exception)
			{
				//MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}
		public void addFileToList(string name, string size, string cdate, string path, string type)
		{
			if (this.InvokeRequired)
			{
				rootTree.Invoke(new MethodInvoker(delegate 
				{
					if (type == "drive")
					{
						if (rootTree.Nodes.Count <= 0)
						{
							TreeNode rtn = new TreeNode();
							rtn.Text = username;
							rtn.ImageIndex = 3;
							rtn.SelectedImageIndex = 3;
							rootTree.Nodes.Add(rtn);
						}
						TreeNode ctn = new TreeNode();
						ctn.Text = name.Remove(2);
						ctn.ImageIndex = 2;
						ctn.SelectedImageIndex = 2;
						rootTree.Nodes[0].Nodes.Add(ctn);
						rootTree.ExpandAll();
					}
				}));
				filesList.Invoke(new MethodInvoker(delegate
				{
					string[] lv_row = { name, normalizeFileSize(size), cdate };
					ListViewItem lv = new ListViewItem(lv_row);
					TreeNode tn = new TreeNode();
					switch (type)
					{
						case "dir":
							lv.ImageIndex = 0;
							folders++;
							break;
						case "file":
							lv.ImageIndex = 1;
							files++;
							break;
						case "drive":
							lv.ImageIndex = 2;
							break;
					}
					filesList.Items.Add(lv);
					filesList_CollectionChanged();
				}));
				if (!string.IsNullOrEmpty(path))
				{
					currentDirectoryFiles.Add(path);
					pathBox.Invoke(new MethodInvoker(delegate
					{
						pathBox.Text = Path.GetDirectoryName(path);
					}));
				}
			}
			else
			{
				if (type == "drive")
				{
					if (rootTree.Nodes.Count <= 0)
					{
						TreeNode rtn = new TreeNode();
						rtn.Text = username;
						rtn.ImageIndex = 3;
						rtn.SelectedImageIndex = 3;
						rootTree.Nodes.Add(rtn);
					}
					TreeNode ctn = new TreeNode();
					ctn.Text = name.Remove(2);
					ctn.ImageIndex = 2;
					ctn.SelectedImageIndex = 2;
					rootTree.Nodes[0].Nodes.Add(ctn);
					rootTree.ExpandAll();
				}
				string[] row = { name, normalizeFileSize(size), cdate };
				ListViewItem lv = new ListViewItem(row);
				TreeNode tn = new TreeNode();
				switch (type)
				{
					case "dir":
						lv.ImageIndex = 0;
						break;
					case "file":
						lv.ImageIndex = 1;
						break;
					case "drive":
						lv.ImageIndex = 2;
						break;
				}
				if (!string.IsNullOrEmpty(path))
				{
					pathBox.Text = Path.GetDirectoryName(path);
					currentDirectoryFiles.Add(path);
				}
				filesList.Items.Add(lv);
			}
		}

		private void FileManagerForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			MainForm.fmf = new FileManagerForm();
		}

		private string normalizeFileSize(string snumber) //epic math
		{
			try
			{
				long number = int.Parse(snumber);
				string type = " GB";
				string rer = "";
				long size = (number / (1024 * 1024 * 1024)); //why doesn't 1024 ^ 3 work????
				if (size < 1) //check if the size (in gbs) is lesser than one
				{
					size = (number / (1024 * 1024)); //if yes then convert to mbs
					type = " MB";
					rer = size.ToString("0.00") + type;
				}
				if (size < 1) //check if the size (in mbs) is lesser than one
				{
					size = (number / (1024)); //if yes than convert to kbs
					type = " KB";
					rer = size.ToString("0.00") + type;
				}
				if (false) //check if the size (in kbs ) is lesser than one
				{
					type = " B";
					rer = number.ToString() + type; //no need to convert
				}
				return rer;
			}
			catch (Exception)
			{
				return "N/A";
			}
		}

		private void rootTree_DoubleClick(object sender, EventArgs e)
		{
			if (rootTree.SelectedNode != null)
			{
				try
				{
					if (rootTree.SelectedNode.Text == username)
					{
						pathBox.Text = String.Empty;
						MainForm.SendCommand("list_drives", clientId);
						filesList.Items.Clear();
						rootTree.Nodes.Clear();
						currentDirectoryFiles.Clear();
					}
					else
					{

						pathBox.Text = rootTree.SelectedNode.ToString() + @"\";
						MainForm.SendCommand("cd\n" + rootTree.SelectedNode.Text + @"\", clientId);
						filesList.Items.Clear();
						currentDirectoryFiles.Clear();
					}
				}
				catch (Exception)
				{ 
				
				}
			}
		}

		private void filesList_CollectionChanged()
		{
			if (statusStrip.Items.Count > 1)
			{
				statusStrip.Items[0].Text = "Files: " + files + " |";
				statusStrip.Items[1].Text = "Folders: " + folders + " |";
			}
			else
			{
				statusStrip.Items.Add("Files: " + files + " |");
				statusStrip.Items.Add("Folders: " + folders + " |");
			}
		}

		private void filesList_DoubleClick(object sender, EventArgs e)
		{
			if (filesList.SelectedIndices.Count > 0)
			{
				if (currentDirectoryFiles.Count > 0)
				{
					//FileAttributes attr = File.GetAttributes(currentDirectoryFiles[filesList.SelectedIndices[0]]); //<-- WHAT THE FUCK IS THISSSSSSSSSSSs
					if (filesList.Items[filesList.SelectedIndices[0]].ImageIndex == 0 || filesList.Items[filesList.SelectedIndices[0]].ImageIndex == 2)
					{
						try
						{
							pathBox.Text = Path.Combine(pathBox.Text, currentDirectoryFiles[filesList.SelectedIndices[0]]);
							MainForm.SendCommand("cd\n" + currentDirectoryFiles[filesList.SelectedIndices[0]], clientId);
							filesList.Items.Clear();
							currentDirectoryFiles.Clear();
						}
						catch (Exception)
						{
							MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
							this.Close();
						}
					}
				}
				else
				{
					//FileAttributes attr = File.GetAttributes(filesList.FocusedItem.Text);
					if (filesList.Items[filesList.SelectedIndices[0]].ImageIndex == 0 || filesList.Items[filesList.SelectedIndices[0]].ImageIndex == 2)
					{
						try
						{
							pathBox.Text = Path.Combine(pathBox.Text, filesList.FocusedItem.Text);
							MainForm.SendCommand("cd\n" + filesList.FocusedItem.Text, clientId);
							filesList.Items.Clear();
						}
						catch (Exception)
						{
							MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
							this.Close();
						}
					}
				}
			}
		}

		private void pathBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				try
				{
					filesList.Items.Clear();
					currentDirectoryFiles.Clear();
					if (!string.IsNullOrEmpty(pathBox.Text))
						MainForm.SendCommand("cd\n" + pathBox.Text, clientId);
					else
						MainForm.SendCommand("list_drives", clientId);
				}
				catch (Exception)
				{
					MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Close();
				}
			}
		}
		private void prevButton_Click(object sender, EventArgs e)
		{
			try
			{
				files = 0;
				folders = 0;
				if (!string.IsNullOrEmpty(pathBox.Text) && !string.IsNullOrWhiteSpace(pathBox.Text)) //check if current path is not empty or null or some other bs
				{
					filesList.Items.Clear();
					currentDirectoryFiles.Clear(); 
					MainForm.SendCommand("cd\n" + Path.Combine(prev_dir), clientId);
					pathBox.Text = prev_dir;
				}
				else
				{
					filesList.Items.Clear();
					currentDirectoryFiles.Clear();
					MainForm.SendCommand("list_drives", clientId);
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}

		private void backButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(pathBox.Text) && !string.IsNullOrWhiteSpace(pathBox.Text)) //check if current path is not empty or null or some other bs
				{
					files = 0;
					folders = 0;
					filesList.Items.Clear();
					currentDirectoryFiles.Clear();
					DirectoryInfo d = new DirectoryInfo(pathBox.Text);
					if (d.Parent == null) //is root
					{
						rootTree.Nodes[0].Nodes.Clear();
						MainForm.SendCommand("list_drives", clientId);
						pathBox.Text = string.Empty;
					}
					else
					{
						prev_dir = pathBox.Text;
						pathBox.Text = Path.GetFullPath(Path.Combine(pathBox.Text, @"..\"));
						MainForm.SendCommand("cd\n" + Path.Combine(pathBox.Text), clientId);
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (filesList.FocusedItem != null)
				{
					MainForm.SendCommand("file_operation\nfile_delete\n" + currentDirectoryFiles[filesList.SelectedIndices[0]], clientId);
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Error: Connection lost.", "Socket Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}

		private void updateFiles_Tick(object sender, EventArgs e)
		{
			refreshCurrentDirectory();
		}

		private void fileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			fo_mode = "file_create";
			foreach (ListViewItem lvi in filesList.Items)
			{
				lvi.Selected = false;
			}
			string[] row = { "", "0 KB", DateTime.UtcNow.ToString() };
			ListViewItem lv = new ListViewItem(row);
			lv.ImageIndex = 1;
			filesList.Items.Add(lv);
			filesList.Items[filesList.Items.Count - 1].Selected = true;
			filesList.Items[filesList.Items.Count - 1].EnsureVisible();
			filesList.Items[filesList.Items.Count - 1].BeginEdit();
		}

		private void moveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (filesList.SelectedIndices.Count > 0)
			{
				fo_mode = "file_move";
				fo_src = currentDirectoryFiles[filesList.SelectedIndices[0]];
			}
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (filesList.SelectedIndices.Count > 0)
			{
				switch (fo_mode)
				{
					case "file_move": //move
						fo_dest = currentDirectoryFiles[filesList.SelectedIndices[0]];
						MainForm.SendCommand("file_operation\n" + fo_mode + "\n" + fo_src + "\n" + fo_dest, clientId);
						break;
					case "file_copy": //copy
						fo_dest = currentDirectoryFiles[filesList.SelectedIndices[0]];
						MainForm.SendCommand("file_operation\n" + fo_mode + "\n" + fo_src + "\n" + fo_dest, clientId);
						break;
				}
				fo_mode = "none";
			}
			else
			{
				switch (fo_mode)
				{
					case "file_move":
						fo_dest = Path.Combine(pathBox.Text);
						MainForm.SendCommand("file_operation\n" + fo_mode + "\n" + fo_src + "\n" + fo_dest, clientId);
						break;
					case "file_copy": //copy
						fo_dest = Path.Combine(pathBox.Text);
						MainForm.SendCommand("file_operation\n" + fo_mode + "\n" + fo_src + "\n" + fo_dest, clientId);
						break;
				}
				fo_mode = "none";
			}
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (filesList.SelectedIndices.Count > 0)
			{
				fo_mode = "file_copy";
				fo_src = currentDirectoryFiles[filesList.SelectedIndices[0]];
			}
		}

		private void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (filesList.SelectedIndices.Count > 0)
			{
				fo_mode = "file_rename";
				filesList.Items[filesList.SelectedIndices[0]].BeginEdit();
			}
		}

		private void filesList_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			switch (fo_mode)
			{
				case "file_rename":
					MainForm.SendCommand("file_operation\nfile_rename\n" + currentDirectoryFiles[e.Item] + "\n" + Path.Combine(Path.GetDirectoryName(currentDirectoryFiles[e.Item]), e.Label), clientId);
					break;
				case "file_create":
					MainForm.SendCommand("file_operation\nfile_create\n" + Path.Combine(pathBox.Text, e.Label), clientId);
					break;
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (filesList.SelectedIndices.Count > 0)
			{
				fo_mode = "file_open";
				fo_src = currentDirectoryFiles[filesList.SelectedIndices[0]];
				try
				{
					MainForm.SendCommand("file_operation\nfile_open\n" + fo_src, clientId);
				}
				catch (Exception)
				{

				}
			}
		}

		private void folderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			fo_mode = "file_create";
			foreach (ListViewItem lvi in filesList.Items)
			{
				lvi.Selected = false;
			}
			string[] row = { "", "0 KB", DateTime.UtcNow.ToString() };
			ListViewItem lv = new ListViewItem(row);
			lv.ImageIndex = 0;
			filesList.Items.Add(lv);
			filesList.Items[filesList.Items.Count - 1].Selected = true;
			filesList.Items[filesList.Items.Count - 1].EnsureVisible();
			filesList.Items[filesList.Items.Count - 1].BeginEdit();
		}

		private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (filesList.SelectedIndices.Count > 0)
			{
				try
				{
					OpenFileDialog fd = new OpenFileDialog();
					if (fd.ShowDialog() == DialogResult.OK)
					{
						if (string.IsNullOrEmpty(fd.FileName) || string.IsNullOrWhiteSpace(fd.FileName)) return;
						MainForm.SendCommand("file_operation\nfile_download\n" + Path.GetDirectoryName(currentDirectoryFiles[filesList.SelectedIndices[0]]) + "\n" + Path.GetFileName(fd.FileName) + "\n" + new FileInfo(fd.FileName).Length, clientId);
						MainForm.fo_path = fd.FileName;
					}
				}
				catch (Exception)
				{
					this.Close();
				}
			}
			else
			{
				try
				{
					OpenFileDialog fd = new OpenFileDialog();
					if (fd.ShowDialog() == DialogResult.OK)
					{
						if (string.IsNullOrEmpty(fd.FileName) || string.IsNullOrWhiteSpace(fd.FileName)) return;
						MainForm.fo_path = fd.FileName;
						MainForm.SendCommand("file_operation\nfile_download\n" + pathBox.Text + "\n" + Path.GetFileName(fd.FileName) + "\n" + new FileInfo(fd.FileName).Length, clientId);
					}
				}
				catch (Exception)
				{
					this.Close();
				}
			}
			if (statusStrip.Items.Count == 2)
			{
				ToolStripControlHost host;
				FlowLayoutPanel panel;
				panel = new FlowLayoutPanel();
				panel.FlowDirection = FlowDirection.TopDown;
				panel.WrapContents = false;
				panel.AutoSize = true;
				panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
				host = new ToolStripControlHost(panel);
				statusStrip.Items.Add(host);
				panel.Controls.Add(pb);
			}
			pb.Value = 0;
		}

		private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (filesList.SelectedIndices.Count > 0)
			{
				if (filesList.Items[filesList.SelectedIndices[0]].ImageIndex == 0 || filesList.Items[filesList.SelectedIndices[0]].ImageIndex == 2) return;
				SaveFileDialog sd = new SaveFileDialog();
				if (sd.ShowDialog() == DialogResult.OK)
				{
					if (string.IsNullOrEmpty(sd.FileName) || string.IsNullOrWhiteSpace(sd.FileName)) return;
					MainForm.fo_path = sd.FileName;
					MainForm.SendCommand("file_operation\nfile_upload\n" + currentDirectoryFiles[filesList.SelectedIndices[0]], clientId);
				}

			}
			else
			{
				if (filesList.Items[filesList.SelectedIndices[0]].ImageIndex == 0 || filesList.Items[filesList.SelectedIndices[0]].ImageIndex == 2) return;
				SaveFileDialog sd = new SaveFileDialog();
				if (sd.ShowDialog() == DialogResult.OK)
				{
					if (string.IsNullOrEmpty(sd.FileName) || string.IsNullOrWhiteSpace(sd.FileName)) return;
					MainForm.fo_path = sd.FileName;
					MainForm.SendCommand("file_operation\nfile_upload\n" + pathBox.Text, clientId);
				}
			}
			if (statusStrip.Items.Count == 2)
			{
				ToolStripControlHost host;
				FlowLayoutPanel panel;
				panel = new FlowLayoutPanel();
				panel.FlowDirection = FlowDirection.TopDown;
				panel.WrapContents = false;
				panel.AutoSize = true;
				panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
				host = new ToolStripControlHost(panel);
				statusStrip.Items.Add(host);
				panel.Controls.Add(pb);
			}
			pb.Value = 0;
		}

		private void filesList_SelectedIndexChanged(object sender, EventArgs e)
		{
			/*if (statusStrip.Items.Count > 1)
			{
				if (filesList.SelectedItems[0].ImageIndex == 0)
					statusStrip.Items[2].Text = "Selected: " + Path.GetDirectoryName(currentDirectoryFiles[filesList.SelectedIndices[0]]) + " | ";
				else if (filesList.SelectedItems[0].ImageIndex == 2)
					statusStrip.Items[2].Text = "Selected: " + filesList.SelectedItems[0].Text + " | ";
				else if (filesList.SelectedItems[0].ImageIndex == 1)
					statusStrip.Items[2].Text = "Selected: " + Path.GetFileName(currentDirectoryFiles[filesList.SelectedIndices[0]]) + " | ";
			}*/
			/*if (filesList.SelectedIndices.Count > -1)
			{
				if (filesList.SelectedItems[0].ImageIndex == 0)
					statusStrip.Items.Add("Selected: " + Path.GetDirectoryName(currentDirectoryFiles[filesList.SelectedIndices[0]]) + " | ");
				else if (filesList.SelectedItems[0].ImageIndex == 2)
					statusStrip.Items.Add("Selected: " + filesList.SelectedItems[0].Text + " | ");
				else if (filesList.SelectedItems[0].ImageIndex == 1)
					statusStrip.Items.Add("Selected: " + Path.GetFileName(currentDirectoryFiles[filesList.SelectedIndices[0]]) + " | ");
			}*/
		}

		public void progressBarUpdate(float value)
		{
			try
			{
				if (this.InvokeRequired)
				{
					statusStrip.Invoke(new MethodInvoker(delegate
					{
						pb.Value = (int)Math.Round(value);
					}));
				}
				else
					pb.Value = (int)Math.Round(value);
			} catch { }
		}
	}

	public static class ControlExtensions //reduce listview flickering
	{
		public static void DoubleBuffering(this Control control, bool enable)
		{
			var method = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
			method.Invoke(control, new object[] { ControlStyles.OptimizedDoubleBuffer, enable });
		}
	}
}
