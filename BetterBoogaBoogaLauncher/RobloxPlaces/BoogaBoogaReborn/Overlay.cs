using BetterBoogaBoogaLauncher.RobloxSDK;
using BetterBoogaBoogaLauncher.SDK;

using System;
using System.Diagnostics;
using System.MDI; // custom library
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetterBoogaBoogaLauncher.RobloxPlaces.BoogaBoogaReborn
{
    public partial class Overlay : Form
    {
        Keymap keymap = new Keymap();

        public Overlay() => InitializeComponent();

        private void Overlay_Load(object sender, EventArgs e)
        {
            LoadConfig();
            textBox1.Text = MDIFile.CheckReplaceRead("Documents\\notes.txt", "I put notes here");

            TabHolder.Controls.Add(GameModsTab);
            GameModsTab.Dock = DockStyle.Fill;
            GameModsTab.Visible = true;

            TabHolder.Controls.Add(GameNotepadTab);
            GameNotepadTab.Dock = DockStyle.Fill;

            uint robloxProcId = (uint)Program.RobloxProcess.roblox.Id;

            //int initalStyle = User32.GetWindowLong(Handle, -20);
            //User32.SetWindowLong(Handle, -20, initalStyle | 0x80000 | 0x20); // setup style

            overDel = new User32.WinEventDelegate(OnAdjust);

            // Initialize some roblox window hooks
            User32.SetWinEventHook(
                (uint)SWEH_Events.EVENT_OBJECT_LOCATIONCHANGE,
                (uint)SWEH_Events.EVENT_OBJECT_LOCATIONCHANGE,
                IntPtr.Zero,
                overDel,
                robloxProcId,
                User32.GetWindowThreadProcessId((IntPtr)robloxProcId, IntPtr.Zero),
                (uint)SWEH_dwFlags.WINEVENT_OUTOFCONTEXT | (uint)SWEH_dwFlags.WINEVENT_SKIPOWNPROCESS | (uint)SWEH_dwFlags.WINEVENT_SKIPOWNTHREAD
            );

            User32.SetWinEventHook(
                (uint)SWEH_Events.EVENT_SYSTEM_FOREGROUND,
                (uint)SWEH_Events.EVENT_OBJECT_LOCATIONCHANGE,
                IntPtr.Zero,
                overDel,
                0,
                0,
                (uint)SWEH_dwFlags.WINEVENT_OUTOFCONTEXT | (uint)SWEH_dwFlags.WINEVENT_SKIPOWNPROCESS | (uint)SWEH_dwFlags.WINEVENT_SKIPOWNTHREAD
            );

            TopMost = true;

            Keymap.keyEvent += onKey;

            /*Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000 / 4);

                    BeginInvoke((MethodInvoker)delegate ()
                    {
                        if (Keymap.IsRobloxFocused() && Focused)
                        {
                            BeginInvoke((MethodInvoker)delegate ()
                            {
                                if (Opacity != 1)
                                    Opacity = 1;
                            });
                        }
                        else
                        {
                            BeginInvoke((MethodInvoker)delegate ()
                            {
                                if (Opacity != 0)
                                    Opacity = 0;
                            });
                        }
                    });
                }
            });*/
        }

        public bool ACEnabled = false;

        private void onKey(object sender, KeyEvent e)
        {
            if (e.vkey == VKeyCodes.KeyDown)
            {
                if (e.key == acKeybind)
                {
                    ACEnabled = !ACEnabled;

                    if (ACEnabled)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            while (ACEnabled && (Focused || Keymap.IsRobloxFocused())) // && (Focused || Keymap.IsRobloxFocused())
                            {
                                if (numericUpDown1.Value > 0)
                                    Thread.Sleep((int)numericUpDown1.Value);

                                if (checkBox1.Checked)
                                    Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_RIGHTDOWN);
                                else Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_LEFTDOWN);
                            }
                            ACEnabled = false;
                        }); // hopefully this optimizes it a bit
                    }
                }
            }

            if (e.vkey == VKeyCodes.KeyHeld)
            {
                if (e.key == Keys.F)
                {
                    if (checkBox3.Checked) //Console.WriteLine("Spam F");
                    {
                        //SendKeys.Send("F");
                    }
                }
            }


            if (e.vkey == VKeyCodes.KeyDown || e.vkey == VKeyCodes.KeyUp) // probably gonna need 2 delegate this
            {
                Invalidate();
            }
        }

        private void OnAdjust(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (Keymap.IsRobloxFocused())
            {
                if (!TopMost)
                    TopMost = true;

                var rect = new ProcessRectangle();

                Program.RobloxProcess.User32.GetWindowRect(
                    Program.RobloxProcess.roblox.MainWindowHandle,
                    out rect
                );

                int x = rect.Left + 12,
                    y = rect.Top + 35; // cuz title bar & resizing

                int width = rect.Right - rect.Left - 24,
                    height = rect.Bottom - rect.Top - 47;

                IntPtr focusInsert = IntPtr.Zero;

                if (Keymap.IsRobloxFocused()) // broken h
                    focusInsert = (IntPtr)(-1);
                focusInsert = (IntPtr)(-2);

                User32.SetWindowPos(Handle, focusInsert, x, y, width, height, 0x40);

                TopMost = true;
            }
        }

        User32.WinEventDelegate overDel;

        public class User32 // user32.dll imports
        {
            [DllImport("User32.dll")]
            public static extern int GetWindowLong(IntPtr hwnd, int nIndex);

            [DllImport("User32.dll")]
            public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

            [DllImport("User32.dll")]
            public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc,
                WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

            [DllImport("User32.dll")]
            public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr voidProcessId);

            [DllImport("User32.dll", EntryPoint = "SetWindowPos")]
            public static extern IntPtr SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

            [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

            [DllImport("User32.dll")]
            public static extern IntPtr GetForegroundWindow();

            public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        }

        private void SettingsIconClicked(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }

        bool devMode = false;   

        LauncherUIRenderContext ctx = new LauncherUIRenderContext(null);
        private void Update(object sender, PaintEventArgs e)
        {
            ctx.SetGraphics(e.Graphics);

            if (devMode)
            {
                int demo = ctx.Window("Demo window");
                //ctx.Window_SetAnchor(demo, SDK.Structs.LAnchor.Left);
                ctx.Window_SetPos(demo, 25, 25);
                ctx.Window_SetSize(demo, 120, 100);

                ctx.Window_Add_TextLabel(demo, "[" + acKeybind.ToString() + "] AutoClicker", 9.5f);
                ctx.Window_Add_TextLabel(demo, "[" + (checkBox1.Checked ? "✔" : "❌") + "] Inverted", 9.5f);
                ctx.Window_Add_TextLabel(demo, "[" + numericUpDown1.Value + "] ClickerSpeed", 9.5f);
            }

            ctx.DrawWindows();
        }

        Keys acKeybind = Keys.F6;

        public void LoadConfig()
        {
            if (Program.config.KeyExists("inverted", "Autoclicker"))
                checkBox1.Checked = Program.config.Read("inverted", "Autoclicker") == "1";

            if (Program.config.KeyExists("clickspeed", "Autoclicker"))
                numericUpDown1.Value = decimal.Parse(Program.config.Read("clickspeed", "Autoclicker"));

            if (Program.config.KeyExists("keybind", "Autoclicker"))
            {
                acKeybind = (Keys)decimal.Parse(Program.config.Read("keybind", "Autoclicker"));
                label1.Text = "[" + acKeybind.ToString() + "] AutoClicker";
            }

            if (Program.config.KeyExists("devmode", "Settings"))
            {
                checkBox2.Checked = Program.config.Read("devmode", "Settings") == "1";
                devMode = checkBox2.Checked;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Program.config.Write("inverted", checkBox1.Checked ? "1" : "0", "Autoclicker");

            Invalidate();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Program.config.Write("clickspeed", numericUpDown1.Value.ToString(), "Autoclicker");

            Invalidate();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            devMode = checkBox2.Checked;

            Program.config.Write("devmode", devMode ? "1" : "0", "Settings");

            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MDIDirectory.CheckCreate("Documents");
            MDIFile.Write("Documents\\notes.txt", textBox1.Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Program.RobloxProcess.roblox.Kill();
            RobloxClient.ExitApp();
        }

        private void HideAllTabs()
        {
            GameModsTab.Visible = false;
            GameNotepadTab.Visible = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            HideAllTabs();

            GameModsTab.Visible = true;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            HideAllTabs();

            GameNotepadTab.Visible = true;
        }

        bool searchingForKey = false;

        private void AutoclickerKeybindPress(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.Text = "[..] AutoClicker";

            searchingForKey = true;

            Keymap.globalKeyEvent += waitingBind;
        }

        private void waitingBind(object sender, KeyEvent e)
        {
            if ((int)e.key < 7) return;

            acKeybind = e.key;
            Program.config.Write("keybind", $"{(int)acKeybind}", "Autoclicker");
            label1.Text = "[" + e.key.ToString() + "] AutoClicker";

            Keymap.globalKeyEvent -= waitingBind;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Program.config.Write("enabled", checkBox3.Checked ? "1" : "0", "QuickPickup");

            Invalidate();
        }
    }
}
