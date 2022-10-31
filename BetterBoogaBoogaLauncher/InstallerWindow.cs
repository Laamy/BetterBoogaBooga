using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BetterBoogaBoogaLauncher
{
    partial class InstallerWindow : Form
    {
        public InstallerWindow() => InitializeComponent();

        private void InstallApp(object sender, EventArgs e) // this one is instant so it doesnt really matter
        {
            progressBar1.Value = 0;

            Program.ReplaceRoblox();
            Program.config.Write("RequiresReinstall", "0", "System"); // reset reinstall

            progressBar1.Value = 100;

            MessageBox.Show("Installed", "BetterBoogaBooga Installer");
        }

        private void RepairApp(object sender, EventArgs e)
        {
            progressBar1.Value = 0;

            Program.config.Write("RequiresReinstall", "1", "System"); // force reinstall

            var robloxVersions = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions");

            float increaseBy = 100 / robloxVersions.Length;

            foreach (string version in robloxVersions) // this is so lazy
            {
                Directory.Delete(version, true);
                progressBar1.Value += (int)increaseBy;
            }

            progressBar1.Value = 100;

            MessageBox.Show("Reinstall roblox & better booga booga to finish repair", "BetterBoogaBooga Installer");
        }

        private void UninstallApp(object sender, EventArgs e) // this one is instant so it doesnt really matter
        {
            progressBar1.Value = 0;

            var robloxVersions = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions");

            Program.config.Write("RequiresReinstall", "1", "System");
            Program.ReplaceRoblox(robloxVersions[robloxVersions.Length - 1] + "\\RobloxPlayerLauncher.exe");

            progressBar1.Value = 100;

            MessageBox.Show("Uninstalled", "BetterBoogaBooga Installer");
        }

        private void InstallerWindow_FormClosing(object sender, FormClosingEventArgs e)
            => Process.GetCurrentProcess().Kill();

        private void InstallerWindow_Load(object sender, EventArgs e)
        {
            if (!Program.CheckAdminPerms())
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;

                Text += " (Requires Administrator)";
            }
        }
    }
}
