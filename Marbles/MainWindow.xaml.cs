using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Marbles
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateGrid(int mapSize)
        {
            var newGrid = new Grid
            {
                Background = Brushes.DarkGray,
                Margin = new Thickness(2, 2, 2, 2)
            };

            GameGrid.Children.Add(newGrid);

            for (var i = 0; i < mapSize; i++)
            {
                var column = new ColumnDefinition { Width = new GridLength(GameGrid.ActualWidth / mapSize) };
                var row = new RowDefinition { Height = new GridLength(GameGrid.ActualWidth / mapSize) };
                newGrid.ColumnDefinitions.Add(column);
                newGrid.RowDefinitions.Add(row);
            }

            var universalCanvas = new Canvas[mapSize][];
            for (var i = 0; i < mapSize; i++)
            {
                universalCanvas[i] = new Canvas[mapSize];
            }

            for (var i = 0; i < mapSize; i++)
            {
                for (var j = 0; j < mapSize; j++)
                {
                    universalCanvas[i][j] = new Canvas
                    {
                        Background = Brushes.GhostWhite,
                        Height = Width = (GameGrid.ActualWidth / mapSize) - 4,
                        Margin = new Thickness(2, 0, 2, 0)
                    };
                    Grid.SetColumn(universalCanvas[i][j], i);
                    Grid.SetRow(universalCanvas[i][j], j);
                    newGrid.Children.Add(universalCanvas[i][j]);
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            GameGrid.Children.Clear();
            GenerateGrid(9);
        }
    }
}