using BetterBoogaBoogaLauncher.RobloxSDK;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace BetterBoogaBoogaLauncher
{
    public partial class LauncherWindow : Form
    {
        public LauncherWindow() => InitializeComponent();

        public void CancelShutdown()
        {
            robloxTimer.Enabled = false;
            SuspendTimer.Enabled = false;

            Close();
        }

        public void InitRobloxDetectTask()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1);

                    if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
                        RobloxClient.ExitApp(); // means no roblox instances r open
                }
            });
        }

        private void RobloxTimer_Tick(object sender, EventArgs e)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.MainWindowTitle == "Roblox")
                {
                    switch (placeId)
                    {
                        case "10758111998": // reborn

                            CancelShutdown();
                            InitRobloxDetectTask();
                            RobloxPlaces.BoogaBoogaReborn.Index.Init();

                            return;

                        case "10767870749": // reborn VOID

                            CancelShutdown();
                            InitRobloxDetectTask();
                            RobloxPlaces.BoogaBoogaReborn.Index.Init();

                            return;

                        case "11471636191": // Ooga Booga

                            CancelShutdown();
                            InitRobloxDetectTask();
                            RobloxPlaces.BoogaBoogaReborn.Index.Init();

                            return;

                        case "11337066400": // pvp game

                            CancelShutdown();
                            InitRobloxDetectTask();
                            RobloxPlaces.BoogaBoogaReborn.Index.Init();

                            return;
                    }

                    RobloxClient.ExitApp();
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
            label2.Text = Program.RobloxProcess.curPlace.data.First().sourceName;
            //label2.Text = Program.RobloxProcess.curPlace.data[0].name;

            placeId = HttpUtility.UrlDecode(Program.la.PlaceLauncherUrl).Split('&')[2].Split('=')[1];

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

        private void formBackground_Click(object sender, EventArgs e)
        {

        }
    }
}
