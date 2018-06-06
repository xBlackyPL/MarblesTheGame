using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Marbles
{
    public partial class GameLobbyView
    {
        public GameLobbyView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var boardNumberOfFileds = (int) Size.Value;
            var numberOfColors = (int) Colors.Value;
            NavigationService?.Navigate(new GameView(boardNumberOfFileds,numberOfColors));
        }

        private void ScoreBoardButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new GameHighestScores());
        }
    }
}
