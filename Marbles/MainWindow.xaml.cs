using System.Windows;

namespace Marbles
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MarblesTheGame_Loaded(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.Navigate(new GameLobbyView());
        }
    }
}