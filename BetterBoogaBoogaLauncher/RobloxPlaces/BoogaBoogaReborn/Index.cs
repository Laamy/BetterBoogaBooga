using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetterBoogaBoogaLauncher.RobloxPlaces.BoogaBoogaReborn
{
    public class Index
    {
        public static void Init()
        {
            // initialize overlay display for rendering
            Task.Run(() => Application.Run(new Overlay()));
        }
    }
}
