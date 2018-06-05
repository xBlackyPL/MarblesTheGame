using System.Windows;
using System.Windows.Media;

namespace Marbles
{
    class GameElementCircle
    {
        public Color CircleColor { get; set; }
        public bool isEnable;
        public bool isNowSet;

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

        public GameElementCircle()
        {
            isEnable = true;
            isNowSet = false;
            CircleColor = AvailableColors[9];
        }
    }
}
