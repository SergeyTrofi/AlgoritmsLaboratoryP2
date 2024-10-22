using HanoiTowers;
using LaboratoryP2.Properties;
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


namespace LaboratoryP2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RHTower_Click(object sender, RoutedEventArgs e)
        {
            RecHanoiTowers win2 = new RecHanoiTowers();
            win2.Show();
            this.Close();
        }

        private void SHTower_Click(object sender, RoutedEventArgs e)
        {
            StackHanoiTowers win3 = new StackHanoiTowers(); // StackHanoiTowers окно для решения через стек
            win3.Show();
            this.Close();
        }
    }
}