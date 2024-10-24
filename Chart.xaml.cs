using OxyPlot.Series;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Wpf;
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
using System.Windows.Shapes;

namespace LaboratoryP2
{
    /// <summary>
    /// Логика взаимодействия для Chart.xaml
    /// </summary>
    public partial class Chart : Window
    {
        public Chart()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (count.Text == "Введите количество колец")
            {
                count.Text = string.Empty;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(count.Text))
            {
                count.Text = "Введите количество колец";
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(count.Text, out int _count))
            {
                var values = Coordinates.Tools.Export(_count);
                var plotModel = new PlotModel();

                var lineSeries = new LineSeries
                {
                    ItemsSource = values,
                    MarkerType = MarkerType.Circle,                    
                    Color = OxyColors.Red,
                    MarkerFill = OxyColors.DarkRed
                };

                plotModel.Series.Add(lineSeries);

                // Создание графика                    
                var linearAxis = new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    Title = "Count(n)",
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot
                };

                var linearAxis2 = new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Title = "Avg time(Tick)",
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot
                };

                plotModel.Axes.Add(linearAxis);
                plotModel.Axes.Add(linearAxis2);

                // Привязка данных к графику
                Plot.Model = plotModel;
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректную глубину фрактала");
            }
        }
    }
}
