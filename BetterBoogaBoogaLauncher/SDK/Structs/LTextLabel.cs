namespace BetterBoogaBoogaLauncher.SDK.Structs
{
    public class LTextLabel : LActor
    {
        public override LTypes GetType() => LTypes.TextLabel;

        public float fontSize;
        public string text;
    }
}