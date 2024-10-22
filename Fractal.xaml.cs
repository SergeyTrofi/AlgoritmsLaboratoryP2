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
using System.Drawing;
using static MaterialDesignThemes.Wpf.Theme;

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
                canvas.Children.Clear();              
                DragonFractal dragonFractal = new DragonFractal(canvas, _deep);
                dragonFractal.DrawFractal();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректную глубину фрактала");
            }
        }
    }


    class DragonFractal
    {
        private readonly Canvas _canvas;
        private readonly Point _pointsStart;
        private readonly int _deep;
        private string axiom = "FX";
        private readonly int angle = 90;
        private string tempAx = string.Empty;
        private readonly Dictionary<char, string> logic = new Dictionary<char, string>()
        {
            {'X', "X+YF+"},
            {'Y', "-FX-Y"}
        };

        public DragonFractal(Canvas canvas, int deep)
        {
            _canvas = canvas;
            _deep = deep;
            _pointsStart = new Point((int)_canvas.ActualWidth / 2, (int)_canvas.ActualHeight / 2);
        }


        public void DrawFractal()
        {
            Point currentPos = new Point(_pointsStart.X, _pointsStart.Y);
            int angle = 90;

            List<Point> pointsList = new List<Point>(); 
            pointsList.Add(currentPos);

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
                   pointsList.Add(new Point(
                       currentPos.X + 2 * Math.Cos(angle * Math.PI / 180),
                       currentPos.Y - 2 * Math.Sin(angle * Math.PI / 180))
                       );

                    DrawLine(currentPos, pointsList.Last());
                    currentPos = new Point(pointsList.Last().X, pointsList.Last().Y);
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
        }

        private void DrawLine(Point start, Point end)
        {
            Line line = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = Brushes.Gray,
                StrokeThickness = 2
            };

            _canvas.Children.Add(line);
        }




































        /*
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
                canvas.Children.Clear();
                DrawDragonCurve(_deep);
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректную глубину фрактала");
            }
        }
        */



        /*
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
                canvas.Children.Clear();
                DragonCurve dragonCurve = new DragonCurve();
                dragonCurve.DrawDragonCurve(canvas, _deep);
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректную глубину фрактала");
            }
        }
    }

    
    class DragonFractal
    {
        private readonly Canvas _canvas;
        private readonly Point _xAxisStart, _xAxisEnd, _yAxisStart, _yAxisEnd;
        private readonly int _deep;
        private string axiom = "FX";
        private string tempAx = string.Empty;
        private readonly Dictionary<char, string> logic = new Dictionary<char, string>()
        {
            {'X', "X+YF+"},
            {'Y', "-FX-Y"}
        };

        public DragonFractal(Canvas canvas, int deep)
        {
            _canvas = canvas;
            _deep = deep;
            _xAxisStart = new Point((int)_canvas.ActualWidth/2, 0);
            _yAxisStart = new Point(0, (int)_canvas.ActualHeight / 2);
        }

        
        private void DrawFractal()
        {
            char lastChange = 'Y';

            for(int i = 0; i < _deep; i++)
            {
                foreach(char ch in axiom)
                {
                    tempAx += logic.TryGetValue(ch, out string value)? value: Char.ToString(ch);
                };
                axiom = tempAx;
                tempAx = string.Empty;
            }

            foreach(char k in axiom)
            {
                if (k == 'F')
                {

                }
                else if (k == '+')
                {

                }
                else if (k == '-')
                {

                }
            };
        }

        private void DrawLine(Point start, Point end)
        {
            Line line = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = Brushes.Gray,
                StrokeThickness = 2
            };

            _canvas.Children.Add(line);
        }
    */
        /*
    class DragonCurve
    {
        private int iterations = 12; // Количество итераций

        public void DrawDragonCurve(Canvas canvas,int iterations)
        {
            List<Point> points = new List<Point>
            {
                new Point(canvas.Width / 2, canvas.Height / 2)
            };

            for (int i = 0; i < iterations; i++)
            {
                int len = points.Count;
                for (int j = len - 1; j >= 0; j--)
                {
                    points.Add(RotatePoint(points[j], points[len - 1], 90));
                }
            }

            DrawLines(canvas, points);
        }

        private Point RotatePoint(Point point, Point pivot, double angle)
        {
            double radians = angle * Math.PI / 180;
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            double dx = point.X - pivot.X;
            double dy = point.Y - pivot.Y;

            return new Point(
                pivot.X + (dx * cos - dy * sin),
                pivot.Y + (dx * sin + dy * cos)
            );
        }

        private void DrawLines(Canvas canvas, List<Point> points)
        {
            Polyline polyline = new Polyline
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            foreach (var point in points)
            {
                polyline.Points.Add(point);
            }

            canvas.Children.Add(polyline);
        }
            */
    }
}
