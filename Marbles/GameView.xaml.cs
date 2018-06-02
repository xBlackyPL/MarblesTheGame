using System;
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
    public partial class GameView : Page
    {
        private int _mapSize;

        public GameView(int mapSize)
        {
            _mapSize = mapSize;
            InitializeComponent();
        }

        private void GenerateGrid()
        {
            GameGrid.Children.Clear();
            var newGrid = new Grid
            {
                Background = Brushes.DarkGray,
                Margin = new Thickness(2, 2, 2, 2),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            GameGrid.Children.Add(newGrid);


            for (var i = 0; i < _mapSize; i++)
            {
                var column = new ColumnDefinition { Width = new GridLength((GameGrid.ActualWidth - 30) / _mapSize) };
                var row = new RowDefinition { Height = new GridLength(GameGrid.ActualHeight / _mapSize) };
                newGrid.ColumnDefinitions.Add(column);
                newGrid.RowDefinitions.Add(row);
            }

            var universalCanvas = new Grid[_mapSize][];
            for (var i = 0; i < _mapSize; i++)
            {
                universalCanvas[i] = new Grid[_mapSize];
            }

            for (var i = 0; i < _mapSize; i++)
            {
                for (var j = 0; j < _mapSize; j++)
                {
                    universalCanvas[i][j] = new Grid
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Background = Brushes.GhostWhite,
                        Height = GameGrid.ActualHeight / _mapSize - 4,
                        Width = (GameGrid.ActualWidth - 30) / _mapSize - 4,
                        Margin = new Thickness(1, 0, 1, 0)
                    };
                    universalCanvas[i][j].MouseLeftButtonDown += OnMouseDown;
                    Grid.SetColumn(universalCanvas[i][j], i);
                    Grid.SetRow(universalCanvas[i][j], j);
                    newGrid.Children.Add(universalCanvas[i][j]);
                }
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var canvas = (Grid)sender;
            var elipse = new Ellipse
            {
                Margin = new Thickness(1, 1, 1, 1),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Fill = Brushes.Red,
                Height = (GameGrid.ActualHeight) / _mapSize - 10,
                Width = (GameGrid.ActualWidth - 30) / _mapSize - 10
            };

            var elipseShadow = new Ellipse
            {
                Margin = new Thickness(1, 1, 1, 1),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Fill = Brushes.DarkGray,
                Height = (GameGrid.ActualHeight) / _mapSize - 9,
                Width = (GameGrid.ActualWidth - 30) / _mapSize - 9
            };

            canvas.Children.Add(elipseShadow);
            canvas.Children.Add(elipse);
        }

        private void GameViewGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GameGrid.Children.Clear();
            GenerateGrid();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new GameLobbyView());
        }
    }
}