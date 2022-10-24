using System.Collections.Generic;
using System.Drawing;

using BetterBoogaBoogaLauncher.SDK.Structs;

namespace BetterBoogaBoogaLauncher.SDK
{
    public class LauncherUIRenderContext
    {
        private Graphics graphics;
        public List<LWindow> windows = new List<LWindow>();

        public LauncherUIRenderContext(Graphics g) => graphics = g;

        public void SetGraphics(Graphics g)
        {
            graphics = g;

            windows.Clear();
        }
        public Graphics GetGraphics() => graphics;

        public int Window(string name)
        {
            int windowId = windows.Count;
            LWindow window = new LWindow();

            window.name = name;

            windows.Add(window);
            return windowId;
        }
        public void Window_SetPos(int id, int x, int y)
        {
            windows[id].x = x;
            windows[id].y = y;
        }
        public void Window_SetSize(int id, int width, int height)
        {
            windows[id].width = width;
            windows[id].height = height;
        }
        public void Window_Add_TextLabel(int id, string text, float fontSize = 12f)
        {
            LTextLabel label = new LTextLabel();

            label.fontSize = fontSize;
            label.text = text;

            windows[id].DrawData.Add(label);
        }

        public void DrawWindows()
        {
            foreach (LWindow lwin in windows)
            {
                RectangleF rect = new RectangleF();
                rect.Width = lwin.width;
                rect.Height = lwin.height;

                graphics.FillRectangle(lwin.fillColor, rect);

                int offsetY = 5;
                foreach (LActor actor in lwin.DrawData)
                {
                    switch (actor.GetType())
                    {
                        case LTypes.Unknown:
                            throw new System.Exception("Cant draw actor of type 'Unknown'");
                            break;

                        case LTypes.Window:
                            throw new System.Exception("Cant draw actor of type 'Window'");
                            break;

                        case LTypes.TextLabel: // should probably move the rendering into the respected classes
                            LTextLabel label = actor as LTextLabel;

                            PointF labelRect = new PointF(rect.X + 5, rect.Y + offsetY);

                            Font font = new Font(FontFamily.GenericSerif, label.fontSize);

                            offsetY += (int)graphics.MeasureString(label.text, font).Height + 5;
                            graphics.DrawString(label.text, font, Brushes.White, labelRect);
                            break;
                    }
                }
            }
        }
    }
}
