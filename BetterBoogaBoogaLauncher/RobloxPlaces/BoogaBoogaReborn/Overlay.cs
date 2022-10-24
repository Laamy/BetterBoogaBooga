using BetterBoogaBoogaLauncher.SDK;
using System;
using System.Diagnostics;
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
        }

        public bool ACEnabled = false;

        private void onKey(object sender, KeyEvent e)
        {
            if (e.vkey == VKeyCodes.KeyDown)
            {
                if (e.key == Keys.F6)
                {
                    ACEnabled = !ACEnabled;

                    if (ACEnabled)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            while (ACEnabled && (Focused || Keymap.IsRobloxFocused()))
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


            if (e.vkey == VKeyCodes.KeyDown || e.vkey == VKeyCodes.KeyUp) // probably gonna need 2 delegate this
            {
                Invalidate();
            }
        }

        private void OnAdjust(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
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

            StringBuilder sb = new StringBuilder("Roblox".Length + 1);
            User32.GetWindowText(User32.GetForegroundWindow(), sb, "Roblox".Length + 1);
            if (sb.ToString() == "Roblox")
                focusInsert = (IntPtr)(-1);
            focusInsert = (IntPtr)(-2);

            User32.SetWindowPos(Handle, focusInsert, x, y, width, height, 0x40);

            TopMost = true;
            //User32.SetWindowPos(Handle, new IntPtr(1), 0, 0, 0, 0, 2 | 1 | 10); // reload
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

                ctx.Window_Add_TextLabel(demo, "[F6] AutoClicker", 9.5f);
                ctx.Window_Add_TextLabel(demo, "[" + (checkBox1.Checked ? "✔" : "❌") + "] Inverted", 9.5f);
                ctx.Window_Add_TextLabel(demo, "[" + numericUpDown1.Value + "] ClickerSpeed", 9.5f);
            }

            ctx.DrawWindows();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) => Invalidate();
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) => Invalidate();

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            devMode = checkBox2.Checked;

            Invalidate();
        }
    }
}
