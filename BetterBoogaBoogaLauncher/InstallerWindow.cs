using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BetterBoogaBoogaLauncher
{
    partial class InstallerWindow : Form
    {
        public InstallerWindow() => InitializeComponent();

        private void InstallApp(object sender, EventArgs e)
        {
            Program.ReplaceRoblox();
            Program.config.Write("RequiresReinstall", "0", "System"); // reset reinstall

            MessageBox.Show("Installed", "BetterBoogaBooga Installer");
        }

        private void RepairApp(object sender, EventArgs e)
        {
            Program.config.Write("RequiresReinstall", "1", "System"); // force reinstall

            var robloxVersions = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions");

            foreach (string version in robloxVersions)
                Directory.Delete(version, true);

            MessageBox.Show("Reinstall roblox & better booga booga to finish repair", "BetterBoogaBooga Installer");
        }

        private void UninstallApp(object sender, EventArgs e)
        {
            var robloxVersions = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions");

            Program.config.Write("RequiresReinstall", "1", "System");
            Program.ReplaceRoblox(robloxVersions[robloxVersions.Length - 1] + "\\RobloxPlayerLauncher.exe");

            MessageBox.Show("Uninstalled", "BetterBoogaBooga Installer");
        }

        private void InstallerWindow_FormClosing(object sender, FormClosingEventArgs e)
            => Process.GetCurrentProcess().Kill();
    }
}
