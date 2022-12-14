using System.Collections.Generic;
using System.Drawing;

namespace BetterBoogaBoogaLauncher.SDK.Structs
{
    public class LWindow : LActor
    {
        public override LTypes GetType() => LTypes.Window;

        public int x, y;
        public int width, height;

        public bool dragging = false;

        public int dx = 0, dy = 0;

        public string name;

        public Brush fillColor = Brushes.Red; // default
        public List<LActor> DrawData = new List<LActor>();
    }
}
