namespace BetterBoogaBoogaLauncher.SDK.Structs
{
    public enum LTypes
    {
        Unknown, Window, TextLabel
    }

    public class LActor
    {
        public virtual LTypes GetType() => LTypes.Unknown;
    }
}
