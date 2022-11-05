using BetterBoogaBoogaLauncher.RobloxSDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.MDI;
using System.Net;
using System.Windows.Forms;

namespace BetterBoogaBoogaLauncher
{
    partial class InstallerWindow : Form
    {
        bool hasToRepair = false;
        public InstallerWindow(bool reinstall = false)
        {
            hasToRepair = reinstall;
            InitializeComponent();
        }

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

            string robloxFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                + "\\Roblox\\Versions";
            string robloxPFPath = "C:\\Program Files (x86)\\Roblox\\Versions"; // some people have other folder so this fixes it ig

            WebClient wc = new WebClient();

            Program.RobloxProcess.version = wc.DownloadString("https://setup.rbxcdn.com/version");

            string robloxPath = "";

            if (!Directory.Exists(robloxFolder + "\\" + Program.RobloxProcess.version) && !Directory.Exists(robloxPFPath + "\\" + Program.RobloxProcess.version))
            {
                Program.config.Write("RequiresReinstall", "1", "System");

                MessageBox.Show("Latest roblox version not detected (FATAL FAILURE)", "BBRB");
                return;
            }
            else
            {
                if (Directory.Exists(robloxFolder + "\\" + Program.RobloxProcess.version))
                    robloxPath = robloxFolder + "\\" + Program.RobloxProcess.version;

                if (Directory.Exists(robloxPFPath + "\\" + Program.RobloxProcess.version))
                    robloxPath = robloxPFPath + "\\" + Program.RobloxProcess.version;
            }

            List<string> folders = new List<string>();

            if (Directory.Exists(robloxFolder))
                folders.AddRange(Directory.GetDirectories(robloxFolder));

            if (Directory.Exists(robloxPFPath))
                folders.AddRange(Directory.GetDirectories(robloxPFPath));

            float increaseBy = 100 / robloxFolder.Length;

            foreach (string version in folders.ToArray()) // this is so lazy
            {
                Directory.Delete(version, true);
                progressBar1.Value += (int)increaseBy;
            }

            RobloxClient.UpdateRoblox(); // this is a massive flaw..

            progressBar1.Value = 100;

            MessageBox.Show("Reinstall roblox & better booga booga to finish repair", "BetterBoogaBooga Installer");
        }

        private void UninstallApp(object sender, EventArgs e) // this one is instant so it doesnt really matter
        {
            progressBar1.Value = 0;

            var robloxVersions = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions");

            Program.config.Write("RequiresReinstall", "1", "System");

            string robloxFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                + "\\Roblox\\Versions";
            string robloxPFPath = "C:\\Program Files (x86)\\Roblox\\Versions"; // some people have other folder so this fixes it ig

            WebClient wc = new WebClient();

            Program.RobloxProcess.version = wc.DownloadString("https://setup.rbxcdn.com/version");

            string robloxPath = "";

            if (!Directory.Exists(robloxFolder + "\\" + Program.RobloxProcess.version) && !Directory.Exists(robloxPFPath + "\\" + Program.RobloxProcess.version))
            {
                Program.config.Write("RequiresReinstall", "1", "System");

                MessageBox.Show("Latest roblox version not detected (FATAL FAILURE)", "BBRB");
                return;
            }
            else
            {
                if (Directory.Exists(robloxFolder + "\\" + Program.RobloxProcess.version))
                    robloxPath = robloxFolder + "\\" + Program.RobloxProcess.version;

                if (Directory.Exists(robloxPFPath + "\\" + Program.RobloxProcess.version))
                    robloxPath = robloxPFPath + "\\" + Program.RobloxProcess.version;
            }
            Program.ReplaceRoblox(robloxPath + "\\RobloxPlayerLauncher.exe");

            progressBar1.Value = 100;

            MessageBox.Show("Uninstalled", "BetterBoogaBooga Installer");
        }

        private void InstallerWindow_FormClosing(object sender, FormClosingEventArgs e)
            => RobloxClient.ExitApp();

        private void InstallerWindow_Load(object sender, EventArgs e)
        {
            if (hasToRepair)
            {
                RobloxClient.UpdateRoblox();
                MessageBox.Show("Reinstall bbrb to update roblox");
            }

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
