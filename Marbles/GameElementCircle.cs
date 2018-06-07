using System.Windows.Media;

namespace Marbles
{
    internal class GameElementCircle
    {
        public static Color[] AvailableColors =
        {
            Colors.Red,
            Colors.Green,
            Colors.Blue,
            Colors.Yellow,
            Colors.Aqua,
            Colors.Orange,
            Colors.MediumOrchid,
            Colors.Pink,
            Colors.GreenYellow,
            Colors.Firebrick,
            Colors.Black
        };

        public bool IsClickable;
        public bool IsNowSet;

        public GameElementCircle()
        {
            IsClickable = true;
            IsNowSet = false;
            CircleColor = AvailableColors[9];
        }

        public Color CircleColor { get; set; }
    }
}