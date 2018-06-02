using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Marbles
{
    public partial class MainWindow : Window
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