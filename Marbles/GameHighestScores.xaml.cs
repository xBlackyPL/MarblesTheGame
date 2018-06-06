using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Marbles
{
    public class Score
    {
        public Score()
        {
        }

        public Score(int score, int numberOfFields, int numberOfColors)
        {
            this.score = score;
            this.numberOfFields = numberOfFields;
            this.numberOfColors = numberOfColors;
        }

        public int score { get; set; }
        public int numberOfFields { get; set; }
        public int numberOfColors { get; set; }
    }

    public partial class GameHighestScores
    {
        private readonly string _pathToScores = "C:\\Programming\\scores.txt";
        private readonly List<Score> _actualScores;
        private readonly StreamWriter _fileWriter;
        private readonly string[] _scoreBoardStrings;

        public GameHighestScores()
        {
            InitializeComponent();

            if (File.Exists(_pathToScores))
            {
                _scoreBoardStrings = File.ReadAllLines(_pathToScores);
                _actualScores = new List<Score>();

                foreach (var row in _scoreBoardStrings)
                    _actualScores.Add(new Score
                    {
                        score = int.Parse(row.Split(null)[0]),
                        numberOfFields = int.Parse(row.Split(null)[1]),
                        numberOfColors = int.Parse(row.Split(null)[2])
                    });

                for (var i = 0; i < _scoreBoardStrings.Length; i++)
                {
                    var score = new Label
                    {
                        Content = _actualScores[i].score.ToString(),
                        FontFamily = new FontFamily("Consolas"),
                        FontSize = 18,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };

                    var numberOfFileds = new Label
                    {
                        Content = _actualScores[i].numberOfFields.ToString(),
                        FontFamily = new FontFamily("Consolas"),
                        FontSize = 18,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };

                    var numberOfColors = new Label
                    {
                        Content = _actualScores[i].numberOfColors.ToString(),
                        FontFamily = new FontFamily("Consolas"),
                        FontSize = 18,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };
                    
                    Grid.SetColumn(score, 0);
                    Grid.SetRow(score, i + 1);
                    Grid.SetColumn(numberOfColors, 1);
                    Grid.SetRow(numberOfColors, i + 1);
                    Grid.SetColumn(numberOfFileds, 2);
                    Grid.SetRow(numberOfFileds, i + 1);

                    ScoreGrid.Children.Add(score);
                    ScoreGrid.Children.Add(numberOfFileds);
                    ScoreGrid.Children.Add(numberOfColors);
                }
            }
        }

        public GameHighestScores(int _score, int _numberOfFileds, int _numberOfColors)
        {
            InitializeComponent();
            if (File.Exists(_pathToScores))
            {
                _scoreBoardStrings = File.ReadAllLines(_pathToScores);
                _actualScores = new List<Score>();

                foreach (var row in _scoreBoardStrings)
                    _actualScores.Add(new Score
                    {
                        score = int.Parse(row.Split(null)[0]),
                        numberOfFields = int.Parse(row.Split(null)[1]),
                        numberOfColors = int.Parse(row.Split(null)[2])
                    });

                var isNewHeight = false;
                var isAlreadyAdded = false;

                for (var i = 0; i < _actualScores.Count; i++)
                {
                    if (_score < _actualScores[i].score) continue;

                    _actualScores.Insert(i, new Score(_score, _numberOfFileds, _numberOfColors));
                    isNewHeight = true;
                    isAlreadyAdded = true;
                    break;
                }

                if (_actualScores.Count < 10)
                {
                    if(!isAlreadyAdded)
                    _actualScores.Insert(_actualScores.Count, new Score(_score, _numberOfFileds, _numberOfColors));
                    isNewHeight = true;
                }

                if (isNewHeight)
                {
                    if (_actualScores.Count >= 10)
                    _actualScores.RemoveAt(_actualScores.Count - 1);

                    var fileStream = File.Open(_pathToScores, FileMode.Open);
                    fileStream.SetLength(0);
                    fileStream.Close();

                    foreach (var newScore in _actualScores)
                    {
                        _fileWriter = File.AppendText(_pathToScores);
                        var scoreString = newScore.score + " " + newScore.numberOfFields + " "
                                          + newScore.numberOfColors + "\r\n";
                        _fileWriter.Write(scoreString);
                        _fileWriter.Close();
                    }
                }

                for (var i = 0; i < _actualScores.Count; i++)
                {
                    var score = new Label
                    {
                        Content = _actualScores[i].score.ToString(),
                        FontFamily = new FontFamily("Consolas"),
                        FontSize = 18,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };

                    var numberOfFileds = new Label
                    {
                        Content = _actualScores[i].numberOfFields.ToString(),
                        FontFamily = new FontFamily("Consolas"),
                        FontSize = 18,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };

                    var numberOfColors = new Label
                    {
                        Content = _actualScores[i].numberOfColors.ToString(),
                        FontFamily = new FontFamily("Consolas"),
                        FontSize = 18,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };

                    Grid.SetColumn(score, 0);
                    Grid.SetRow(score, i + 1);
                    Grid.SetColumn(numberOfColors, 1);
                    Grid.SetRow(numberOfColors, i + 1);
                    Grid.SetColumn(numberOfFileds, 2);
                    Grid.SetRow(numberOfFileds, i + 1);

                    ScoreGrid.Children.Add(score);
                    ScoreGrid.Children.Add(numberOfFileds);
                    ScoreGrid.Children.Add(numberOfColors);
                }
            }
            else
            {
                _fileWriter = File.AppendText(_pathToScores);
                var scoreString = _score + " " + _numberOfFileds + " " +
                                  _numberOfColors + "\r\n";
                _fileWriter.Write(scoreString);
                _fileWriter.Close();

                var score = new Label
                {
                    Content = _score.ToString(),
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = 18,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    HorizontalContentAlignment = HorizontalAlignment.Center
                };

                var numberOfFileds = new Label
                {
                    Content = _numberOfFileds.ToString(),
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = 18,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    HorizontalContentAlignment = HorizontalAlignment.Center
                };

                var numberOfColors = new Label
                {
                    Content = _numberOfColors.ToString(),
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = 18,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    HorizontalContentAlignment = HorizontalAlignment.Center
                };

                Grid.SetColumn(score, 0);
                Grid.SetRow(score, 1);
                Grid.SetColumn(numberOfColors, 1);
                Grid.SetRow(numberOfColors, 1);
                Grid.SetColumn(numberOfFileds, 2);
                Grid.SetRow(numberOfFileds, 1);
                ScoreGrid.Children.Add(score);
                ScoreGrid.Children.Add(numberOfFileds);
                ScoreGrid.Children.Add(numberOfColors);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new GameLobbyView());
        }
    }
}