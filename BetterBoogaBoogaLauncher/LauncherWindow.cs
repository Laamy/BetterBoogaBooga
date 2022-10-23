using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace BetterBoogaBoogaLauncher
{
    public partial class LauncherWindow : Form
    {
        public LauncherWindow() => InitializeComponent();

        private void RobloxTimer_Tick(object sender, EventArgs e)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.MainWindowTitle == "Roblox")
                {
                    switch (placeId)
                    {
                        case "10758111998":

                            robloxTimer.Enabled = false;
                            SuspendTimer.Enabled = false;

                            Close();

                            RobloxPlaces.BoogaBoogaReborn.Index.Init();

                            return;

                        case "11337066400": // pvp game

                            robloxTimer.Enabled = false;
                            SuspendTimer.Enabled = false;

                            Close();

                            RobloxPlaces.BoogaBoogaReborn.Index.Init();

                            return;
                    }

                    Process.GetCurrentProcess().Kill(); // might combine everything into the one process
                }
            }
        }

        string loadingSufix = "Starting Roblox";

        int dots = 1;
        private void SuspendTimer_Tick(object sender, EventArgs e)
        {
            if (dots == 4) dots = 0;
            dots++;

            label1.Text = loadingSufix + " " + String.Concat(Enumerable.Repeat(".", dots));
        }

        string placeId = "0";

        private void LauncherWindow_Load(object sender, EventArgs e)
        {
            placeId = HttpUtility.UrlDecode(Program.la.PlaceLauncherUrl).Split('&')[2].Split('=')[1];
            //label2.Text = "PlaceID: " + placeId;

            switch (placeId)
            {
                case "10758111998":
                    //formBackground.Image = Properties.Resources.bbrb;
                    //loadingSufix = "Starting Booga";
                    break;
            }

            // update label
            label1.Text = loadingSufix + " " + String.Concat(Enumerable.Repeat(".", dots));
        }
    }
}
