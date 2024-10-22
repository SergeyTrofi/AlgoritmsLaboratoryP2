using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Threading.Tasks;

namespace HanoiTowers
{
    public partial class StackHanoiTowers : Window
    {
        private int diskCount;
        private int stepCount;
        private List<List<Rectangle>> towers;

        public StackHanoiTowers()
        {
            InitializeComponent(); // Этот метод связывает XAML элементы с C# кодом
            towers = new List<List<Rectangle>> { new List<Rectangle>(), new List<Rectangle>(), new List<Rectangle>() };
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(DiscCountTextBox.Text, out diskCount) && diskCount > 0)
            {
                ResetGame();
                await SolveHanoi(diskCount, 0, 2, 1); // Перемещение дисков с башни 1 на башню 3
                StepsTextBlock.Text = stepCount.ToString();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное количество дисков.");
            }
        }

        private void ResetGame()
        {
            StepsTextBlock.Text = string.Empty;
            stepCount = 0;
            InitializeTowers();
        }

        private void InitializeTowers()
        {
            Tower1.Children.Clear();
            Tower2.Children.Clear();
            Tower3.Children.Clear();
            towers.ForEach(t => t.Clear());

            for (int i = 1; i <= diskCount; i++)
            {
                var disk = CreateDisk(i);
                Tower1.Children.Add(disk); // Добавляем все диски на башню 1 в начале
                towers[0].Add(disk);
            }
        }

        private Rectangle CreateDisk(int index)
        {
            return new Rectangle
            {
                Width = index * 20,
                Height = 20,
                Fill = new SolidColorBrush(GetDiskColor(index - 1)),
                Margin = new Thickness(0, 0, 0, 5)
            };
        }

        private Color GetDiskColor(int index)
        {
            switch (index)
            {
                case 0: return Colors.Red;
                case 1: return Colors.Green;
                case 2: return Colors.Blue;
                case 3: return Colors.Yellow;
                case 4: return Colors.Purple;
                default: return Colors.Black;
            }
        }

        private async Task SolveHanoi(int n, int from, int to, int aux)
        {
            if (n == 1)
            {
                await MoveDisk(from, to);
                return;
            }

            await SolveHanoi(n - 1, from, aux, to);
            await MoveDisk(from, to);
            await SolveHanoi(n - 1, aux, to, from);
        }

        private async Task MoveDisk(int from, int to)
        {
            if (towers[from].Count > 0)
            {
                var disk = towers[from][0];

                if (CanMoveDisk(disk, to))
                {
                    towers[from].RemoveAt(0);
                    await Task.Delay(500); // Задержка для визуализации перемещения
                    TowerPanelRemoveDisk(disk);
                    towers[to].Insert(0, disk);
                    GetTowerPanel(to).Children.Insert(0, disk);

                    stepCount++;
                    StepsTextBlock.Text = stepCount.ToString();
                }
            }
        }

        private bool CanMoveDisk(Rectangle disk, int to)
        {
            return towers[to].Count == 0 || towers[to][0].Width > disk.Width;
        }

        private void TowerPanelRemoveDisk(Rectangle disk)
        {
            if (disk.Parent is Panel parent)
            {
                parent.Children.Remove(disk);
            }
        }
        private StackPanel GetTowerPanel(int index)
        {
            switch (index)
            {
                case 0: return Tower1;
                case 1: return Tower2;
                case 2: return Tower3;
                default: throw new ArgumentOutOfRangeException(nameof(index), "Неверный индекс башни.");
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            DiscCountTextBox.Text = string.Empty;
            StepsTextBlock.Text = string.Empty;
            ResetGame();
        }
    }
}