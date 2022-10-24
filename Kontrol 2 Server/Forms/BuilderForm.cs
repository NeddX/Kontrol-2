using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using FileAttributes = System.IO.FileAttributes;

namespace Kontrol_2_Server
{
    public partial class BuilderForm : Form
    {
        string[] dummies = { "CLRDependent.k2/", "SelfContained.k2/", "SelfContainedSingleFile.k2" };

        public BuilderForm()
        {
            InitializeComponent();
            outputCombo.SelectedIndex = 1;
        }

        private void buildBtn_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Executable file (*.exe)|*.exe";
            sfd.AddExtension = true;
            sfd.DefaultExt = ".exe";
            sfd.FileName = "Dummy.exe";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var prog = new ProgressBarForm("Building...", "Decompressing...");
                new Thread (() => { prog.ShowDialog(); }).Start();
                //this.Enabled = false;

                // Cleaning the directory
                DeleteDirectoryWithContents(Path.GetDirectoryName(sfd.FileName));

                // Unzip the dummy file first
                string location = Path.GetDirectoryName(sfd.FileName);
                //Directory.CreateDirectory(location);
                ZipFile.ExtractToDirectory($"./Data/{dummies[outputCombo.SelectedIndex]}/dummy.rte", location);

                // Determine the extension
                string ext = (outputCombo.SelectedIndex == 1) ? "dll" : "exe";

                prog.SetText("Loading the assembly...");
                prog.SetPercent(50);

                // Create a module instance
                ModuleContext modCtx = ModuleDef.CreateModuleContext();
                // Load the module
                ModuleDefMD module = ModuleDefMD.Load($"{location}/k2c.{ext}", modCtx);

                prog.SetText("Modifying the IL...");
                prog.SetPercent(75);

                // Get all the classes in the module
                foreach (var type in module.Types)
                {
                    if (type.Name == "Settings")
                    {
                        // Get the fields of the class
                        foreach (var method in type.Methods)
                        {
                            if (method == null) continue;

                            // For convenience sake
                            var insts = method.Body.Instructions;

                            // Get all the IL instruction in current method
                            for (int i = 0; i < insts.Count; i++)
                            {
                                if (insts[i].OpCode == OpCodes.Stsfld)
                                {
                                    string currentFieldName = insts[i].Operand.ToString().Split("::")[1];
                                    if (currentFieldName == "s_Ip") insts[i - 1].Operand = hostBox.Text;
                                    else if (currentFieldName == "s_Port") insts[i - 1].Operand = Convert.ToInt32(portBox.Text);
                                    else if (currentFieldName == "s_SW_MODE") insts[i - 1].Operand = swCheck.Checked;
                                }
                            }
                        }
                    }
                }

                prog.SetText("Outputting...");
                prog.SetPercent(100);

                // Switch on the output type
                switch (outputCombo.SelectedIndex)
                {
                    // Self contained
                    case 1:
                        module.Write(Path.Combine(location, "k2c.dll.bak"));
                        module.Dispose();
                        File.Move(Path.Combine(location, "k2c.dll.bak"), Path.Combine(location, "k2c.dll"), true);
                        File.Move(Path.Combine(location, "k2c.exe"), sfd.FileName);
                        break;
                    // Self Contained Single file and CLR Dependent
                    default:
                        module.Write(Path.Combine(location, "k2c.exe.bak"));
                        module.Dispose();
                        File.Move(Path.Combine(location, "k2c.exe.bak"), sfd.FileName, true);
                        break;
                }

                prog.Close();
                //this.Enabled = true;

                this.TopMost = true;
                this.Activate();
                MessageBox.Show("Build: Operation finished with success.", "Build Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void CopyDirectoryWtihContents(string source, string destination)
        {
            foreach (var dir in Directory.GetDirectories(source))
            {
                string path = Path.Combine(destination, Path.GetFileName(dir));
                Directory.CreateDirectory(path);
                CopyDirectoryWtihContents(dir, path);
            }
            foreach (var file in Directory.GetFiles(source)) File.Copy(file, Path.Combine(destination, Path.GetFileName(file)), true);
        }

        void DeleteDirectoryWithContents(string directory)
        {
            foreach (var dir in Directory.GetDirectories(directory)) DeleteDirectoryWithContents(dir);
            foreach (var file in Directory.GetFiles(directory)) File.Delete(file);
        }
    }
}
