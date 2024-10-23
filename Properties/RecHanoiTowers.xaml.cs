using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LaboratoryP2.Properties
{
    public partial class RecHanoiTowers : Window
    {
        private int diskCount;
        private int stepCount;
        
        private List<List<Rectangle>> towers;

        public RecHanoiTowers()
        {
            InitializeComponent();
            towers = new List<List<Rectangle>> { new List<Rectangle>(), new List<Rectangle>(), new List<Rectangle>() };
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(DiscCountTextBox.Text, out diskCount) && diskCount > 0)
            {
                ResetButton.Visibility = Visibility.Hidden;
                StartButton.Visibility = Visibility.Hidden;
                ResetGame();
                await SolveHanoi(diskCount, 0, 2, 1); // Move from Tower 1 to Tower 3
                StepsTextBlock.Text = stepCount.ToString();
                ResetButton.Visibility = Visibility.Visible;
                StartButton.Visibility = Visibility.Visible;
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
                Tower1.Children.Add(disk); // Start with disks on Tower 1
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
            if (index == 0) return Colors.Red;
            if (index == 1) return Colors.Green;
            if (index == 2) return Colors.Blue;
            if (index == 3) return Colors.Yellow;
            if (index == 4) return Colors.Purple;
            return Colors.Black;
        }

        private async Task SolveHanoi(int n, int from, int to, int aux)
        {
            if (n <= 0) return; // Base case to prevent recursion on zero disks.

            await SolveHanoi(n - 1, from, aux, to); // Move n-1 disks to auxiliary
            await MoveDisk(from, to); // Move the nth disk to the target
            await SolveHanoi(n - 1, aux, to, from); // Move n-1 disks from auxiliary to target
        }

        private async Task MoveDisk(int from, int to)
        {
            if (towers[from].Count > 0)
            {
                var disk = towers[from][0];

                if (CanMoveDisk(disk, to))
                {
                    towers[from].RemoveAt(0);

                    await Task.Delay(500);
                    RemoveDiskFromParent(disk);
                    towers[to].Insert(0, disk); // Insert at the beginning to maintain order
                    GetTowerPanel(to).Children.Insert(0, disk); // Add to the tower panel

                    stepCount++; // Увеличиваем количество шагов
                    StepsTextBlock.Text = stepCount.ToString(); // Обновляем текстовое поле
                }
            }
        }


        private bool CanMoveDisk(Rectangle disk, int to)
        {
            return towers[to].Count == 0 || towers[to][0].Width > disk.Width;
        }

        private void RemoveDiskFromParent(Rectangle disk)
        {
            if (disk.Parent is Panel currentParent)
            {
                currentParent.Children.Remove(disk);
            }
        }

        private StackPanel GetTowerPanel(int index)
        {
            if (index == 0) return Tower1;
            if (index == 1) return Tower2;
            if (index == 2) return Tower3;

            throw new ArgumentOutOfRangeException(nameof(index), "Invalid tower index.");
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            StepsTextBlock.Text = string.Empty;
            DiscCountTextBox.Text = string.Empty;
            InitializeTowers();
        }
    }
}
