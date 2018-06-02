﻿using System;
using System.Collections.Generic;
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
    public partial class GameLobbyView : Page
    {
        public GameLobbyView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var boardNumberOfFileds = int.Parse(SizeTextBox.Text);
            if (!(boardNumberOfFileds <= 5 || boardNumberOfFileds >= 26))
            {
                NavigationService?.Navigate(new GameView(boardNumberOfFileds));
            }

            Waring.Content = $@"Number of fields must be within 6 and 25!";
        }
    }
}