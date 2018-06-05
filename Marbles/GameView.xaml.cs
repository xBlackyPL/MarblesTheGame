using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Marbles
{
    public partial class GameView
    {
        private readonly GameElementCircle[][] _gameElements;
        private readonly int _mapSize;
        private readonly int _numberOfColors;
        private readonly Random _randomGenerator;
        private bool _busy;
        private Color _holdColor;
        private Grid[][] _universalCanvas;

        public GameView(int mapSize, int numberOfColors)
        {
            _busy = false;
            _mapSize = mapSize;
            _numberOfColors = numberOfColors;
            _gameElements = new GameElementCircle[_mapSize][];
            _randomGenerator = new Random();

            for (var i = 0; i < mapSize; i++)
            {
                _gameElements[i] = new GameElementCircle[_mapSize];
                for (var j = 0; j < mapSize; j++)
                    _gameElements[i][j] = new GameElementCircle();
            }

            InitializeComponent();
        }

        private void GameStartingSet()
        {
            var index = 0;
            while (index < 3)
            {
                var x = _randomGenerator.Next(0, _mapSize);
                var y = _randomGenerator.Next(0, _mapSize);

                if (!_gameElements[y][x].isEnable) continue;

                _gameElements[y][x].isEnable = false;
                _gameElements[y][x].isNowSet = true;

                var color = _randomGenerator.Next(0, _numberOfColors);
                _gameElements[y][x].CircleColor =
                    GameElementCircle.AvailableColors[color];

                index++;
            }
        }

        private void DrawMap()
        {
            for (var i = 0; i < _mapSize; i++)
            for (var j = 0; j < _mapSize; j++)
            {
                if (!_gameElements[i][j].isNowSet) continue;
                if (_gameElements[i][j].isEnable) continue;
                _gameElements[i][j].isNowSet = false;

                var color = new SolidColorBrush(_gameElements[i][j].CircleColor);
                var elipse = new Ellipse
                {
                    Margin = new Thickness(1, 1, 1, 1),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Fill = color,
                    Height = GameGrid.ActualHeight / _mapSize - 10,
                    Width = (GameGrid.ActualWidth - 30) / _mapSize - 10
                };

                var elipseShadow = new Ellipse
                {
                    Margin = new Thickness(1, 1, 1, 1),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Fill = Brushes.DarkGray,
                    Height = GameGrid.ActualHeight / _mapSize - 9,
                    Width = (GameGrid.ActualWidth - 30) / _mapSize - 9
                };

                _universalCanvas[i][j].Children.Add(elipseShadow);
                _universalCanvas[i][j].Children.Add(elipse);
            }
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
                var column = new ColumnDefinition {Width = new GridLength((GameGrid.ActualWidth - 30) / _mapSize)};
                var row = new RowDefinition {Height = new GridLength(GameGrid.ActualHeight / _mapSize)};
                newGrid.ColumnDefinitions.Add(column);
                newGrid.RowDefinitions.Add(row);
            }

            _universalCanvas = new Grid[_mapSize][];
            for (var i = 0; i < _mapSize; i++) _universalCanvas[i] = new Grid[_mapSize];

            for (var i = 0; i < _mapSize; i++)
            for (var j = 0; j < _mapSize; j++)
            {
                _universalCanvas[i][j] = new Grid
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Background = Brushes.GhostWhite,
                    Height = GameGrid.ActualHeight / _mapSize - 4,
                    Width = (GameGrid.ActualWidth - 30) / _mapSize - 4,
                    Margin = new Thickness(1, 0, 1, 0)
                };
                _universalCanvas[i][j].MouseLeftButtonDown += OnMouseDown;
                Grid.SetColumn(_universalCanvas[i][j], j);
                Grid.SetRow(_universalCanvas[i][j], i);
                newGrid.Children.Add(_universalCanvas[i][j]);
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var canvas = (Grid) sender;
            var x = 0;
            var y = 0;

            while (x < _mapSize && y < _mapSize)
            {
                while (x < _mapSize)
                {
                    if (_universalCanvas[y][x].Equals(canvas))
                        break;
                    x++;
                }

                if (x < _mapSize)
                    if (_universalCanvas[y][x].Equals(canvas))
                        break;
                x = 0;
                y++;
            }

            if (_busy)
            {
                if (_gameElements[y][x].isEnable != true) return;
                _gameElements[y][x].isNowSet = true;
                _gameElements[y][x].isEnable = false;
                _gameElements[y][x].CircleColor = _holdColor;
                _busy = false;
                CheckRowForPoints(new Point(x, y));
                CheckColumnForPoints(new Point(x, y));
                CheckSquareForPoints(new Point(x, y));
                GameStartingSet();
                DrawMap();
            }
            else
            {
                if (_gameElements[y][x].isEnable) return;
                _universalCanvas[y][x].Children.Clear();
                _gameElements[y][x].isEnable = true;
                _gameElements[y][x].isNowSet = false;
                _busy = true;
                _holdColor = _gameElements[y][x].CircleColor;
                _gameElements[y][x].CircleColor = GameElementCircle.AvailableColors.Last();
            }
        }

        private void CheckColumnForPoints(Point startingPoint)
        {
            var score = 1;
            var column = (int) startingPoint.X;
            var row = (int) startingPoint.Y;
            var top = column;
            var bottom = column;
            var checkTop = true;
            var checkBottom = true;

            for (var i = 1; i < _mapSize; i++)
            {
                if (checkTop)
                    if (row + i < _mapSize)
                        if (_gameElements[row + i][column].CircleColor.Equals(_holdColor))
                        {
                            top = row + i;
                            score++;
                        }
                        else
                        {
                            checkTop = false;
                        }
                    else
                        checkTop = false;

                if (checkBottom)
                    if (row - i >= 0)
                        if (_gameElements[row - i][column].CircleColor.Equals(_holdColor))
                        {
                            bottom = row - i;
                            score++;
                        }
                        else
                        {
                            checkBottom = false;
                        }
                    else
                        checkBottom = false;
            }

            if (score < 5) return;

            for (var i = top; i <= bottom; i++)
            {
                _gameElements[i][column].isEnable = true;
                _gameElements[i][column].CircleColor = GameElementCircle.AvailableColors.Last();
                _universalCanvas[i][column].Children.Clear();
            }

            var scoreString = (string) ScoreLabel.Content;
            int.TryParse(scoreString, out var actualScore);

            actualScore += score;
            ScoreLabel.Content = actualScore.ToString();
        }

        private void CheckRowForPoints(Point startingPoint)
        {
            var score = 1;
            var column = (int) startingPoint.X;
            var row = (int) startingPoint.Y;
            var leftMargin = column;
            var rightMaring = column;
            var checkRight = true;
            var checkLeft = true;

            for (var i = 1; i < _mapSize; i++)
            {
                if (checkRight)
                    if (column + i < _mapSize)
                        if (_gameElements[row][column + i].CircleColor.Equals(_holdColor))
                        {
                            rightMaring = column + i;
                            score++;
                        }
                        else
                        {
                            checkRight = false;
                        }

                if (checkLeft)
                    if (column - i >= 0)
                        if (_gameElements[row][column - i].CircleColor.Equals(_holdColor))
                        {
                            leftMargin = column - i;
                            score++;
                        }
                        else
                        {
                            checkLeft = false;
                        }
                    else
                        checkLeft = false;
            }

            if (score < 5) return;

            for (var i = leftMargin; i <= rightMaring; i++)
            {
                _gameElements[row][i].isEnable = true;
                _gameElements[row][i].CircleColor = GameElementCircle.AvailableColors.Last();
                _universalCanvas[row][i].Children.Clear();
            }

            var scoreString = (string) ScoreLabel.Content;
            int.TryParse(scoreString, out var actualScore);

            actualScore += score;
            ScoreLabel.Content = actualScore.ToString();
        }

        private void CheckSquareForPoints(Point startinPoint)
        {
            var column = (int) startinPoint.X;
            var row = (int) startinPoint.Y;

            if (column + 1 < _mapSize)
                if (_gameElements[row][column + 1].CircleColor.Equals(_holdColor))
                {
                    if (row + 1 < _mapSize)
                        if (_gameElements[row + 1][column + 1].CircleColor.Equals(_holdColor))
                            if (_gameElements[row + 1][column].CircleColor.Equals(_holdColor))
                            {
                                _gameElements[row][column].isEnable = true;
                                _gameElements[row][column].CircleColor = GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row][column].Children.Clear();

                                _gameElements[row + 1][column].isEnable = true;
                                _gameElements[row + 1][column].CircleColor = GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row + 1][column].Children.Clear();

                                _gameElements[row][column + 1].isEnable = true;
                                _gameElements[row][column + 1].CircleColor = GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row][column + 1].Children.Clear();

                                _gameElements[row + 1][column + 1].isEnable = true;
                                _gameElements[row + 1][column + 1].CircleColor =
                                    GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row + 1][column + 1].Children.Clear();

                                var scoreString = (string) ScoreLabel.Content;
                                int.TryParse(scoreString, out var actualScore);

                                actualScore += 4;
                                ScoreLabel.Content = actualScore.ToString();
                                return;
                            }

                    if (row - 1 >= 0)
                        if (_gameElements[row - 1][column + 1].CircleColor.Equals(_holdColor))
                            if (_gameElements[row - 1][column].CircleColor.Equals(_holdColor))
                            {
                                _gameElements[row][column].isEnable = true;
                                _gameElements[row][column].CircleColor = GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row][column].Children.Clear();

                                _gameElements[row][column + 1].isEnable = true;
                                _gameElements[row][column + 1].CircleColor = GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row][column + 1].Children.Clear();

                                _gameElements[row - 1][column].isEnable = true;
                                _gameElements[row - 1][column].CircleColor = GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row - 1][column].Children.Clear();

                                _gameElements[row - 1][column + 1].isEnable = true;
                                _gameElements[row - 1][column + 1].CircleColor =
                                    GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row - 1][column + 1].Children.Clear();

                                var scoreString = (string) ScoreLabel.Content;
                                int.TryParse(scoreString, out var actualScore);

                                actualScore += 4;
                                ScoreLabel.Content = actualScore.ToString();
                                return;
                            }
                }

            if (column - 1 >= 0)
                if (_gameElements[row][column - 1].CircleColor.Equals(_holdColor))
                {
                    if (row + 1 < _mapSize)
                        if (_gameElements[row + 1][column - 1].CircleColor.Equals(_holdColor))
                            if (_gameElements[row + 1][column].CircleColor.Equals(_holdColor))
                            {
                                _gameElements[row][column].isEnable = true;
                                _gameElements[row][column].CircleColor = GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row][column].Children.Clear();

                                _gameElements[row][column - 1].isEnable = true;
                                _gameElements[row][column - 1].CircleColor = GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row][column - 1].Children.Clear();

                                _gameElements[row + 1][column].isEnable = true;
                                _gameElements[row + 1][column].CircleColor = GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row + 1][column].Children.Clear();

                                _gameElements[row + 1][column - 1].isEnable = true;
                                _gameElements[row + 1][column - 1].CircleColor =
                                    GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row + 1][column - 1].Children.Clear();

                                var scoreString = (string) ScoreLabel.Content;
                                int.TryParse(scoreString, out var actualScore);

                                actualScore += 4;
                                ScoreLabel.Content = actualScore.ToString();
                                return;
                            }

                    if (row - 1 >= 0)
                        if (_gameElements[row - 1][column - 1].CircleColor.Equals(_holdColor))
                            if (_gameElements[row - 1][column].CircleColor.Equals(_holdColor))
                            {
                                _gameElements[row][column].isEnable = true;
                                _gameElements[row][column].CircleColor = GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row][column].Children.Clear();

                                _gameElements[row][column - 1].isEnable = true;
                                _gameElements[row][column - 1].CircleColor = GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row][column - 1].Children.Clear();

                                _gameElements[row - 1][column].isEnable = true;
                                _gameElements[row - 1][column].CircleColor = GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row - 1][column].Children.Clear();

                                _gameElements[row - 1][column - 1].isEnable = true;
                                _gameElements[row - 1][column - 1].CircleColor =
                                    GameElementCircle.AvailableColors.Last();
                                _universalCanvas[row - 1][column - 1].Children.Clear();

                                var scoreString = (string) ScoreLabel.Content;
                                int.TryParse(scoreString, out var actualScore);

                                actualScore += 4;
                                ScoreLabel.Content = actualScore.ToString();
                            }
                }
        }

        private void GameViewGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GameGrid.Children.Clear();
            GameStartingSet();
            GenerateGrid();
            DrawMap();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new GameLobbyView());
        }
    }
}