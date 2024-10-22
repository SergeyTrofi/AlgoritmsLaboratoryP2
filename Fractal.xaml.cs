using System;
using OxyPlot;
using OxyPlot.Axes; 
using OxyPlot.Wpf;
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
using System.Drawing;
using static MaterialDesignThemes.Wpf.Theme;
using OxyPlot.Series;

namespace LaboratoryP2
{
    public partial class Fractal : Window
    {
        public Fractal()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (deep.Text == "Введите глубину фрактала")
            {
                deep.Text = string.Empty;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(deep.Text))
            {
                deep.Text = "Введите глубину фрактала";
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(deep.Text, out int _deep))
            {
                DragonFractal dragonFractal = new DragonFractal(_deep);
                dragonFractal.DrawFractal(Plot);
                
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректную глубину фрактала");
            }
        }
    }


    class DragonFractal
    {
        private readonly Point _pointsStart;
        PlotModel plotModel = new PlotModel();
        private readonly int _deep;
        private string axiom = "FX";
        private int angle = 90;
        private string tempAx = string.Empty;
        private readonly Dictionary<char, string> logic = new Dictionary<char, string>()
        {
            {'X', "X+YF+"},
            {'Y', "-FX-Y"}
        };

        public DragonFractal(int deep)
        {
            _deep = deep;
            _pointsStart = new Point(0, 0);
        }


        public void DrawFractal(PlotView plotView)
        {
            DataPoint currentPos = new DataPoint(_pointsStart.X, _pointsStart.Y);

            List<DataPoint> pointsList = new List<DataPoint>(); 

            for (int i = 0; i < _deep; i++)
            {
                foreach (char ch in axiom)
                {
                    tempAx += logic.TryGetValue(ch, out string value) ? value : Char.ToString(ch);
                };
                axiom = tempAx;
                tempAx = string.Empty;
            }

            foreach (char k in axiom)
            {
                if (k == 'F')
                {
                   var nextPoint = new DataPoint(
                       currentPos.X + 2 * Math.Cos(angle * Math.PI / 180),
                       currentPos.Y - 2 * Math.Sin(angle * Math.PI / 180)
                       );

                    pointsList.Add(currentPos);
                    pointsList.Add(nextPoint);
                    currentPos = new DataPoint(pointsList.Last().X, pointsList.Last().Y);
                }
                else if (k == '+')
                {
                    angle += 90;
                }
                else if (k == '-')
                {
                    angle -= 90;
                }
            };
            DrawLine(pointsList);         
            plotModel.Axes.Clear();
            plotView.Model = plotModel;
        }

        private void DrawLine(List<DataPoint> points)
        {

            var lineSeries = new LineSeries
            {
                ItemsSource = points,
                MarkerType = MarkerType.None,
                Title = "Dragon Curve",
                Color = OxyColors.Red,
                StrokeThickness = 1,
            };

            plotModel.Series.Add(lineSeries);
        }
    }
}
