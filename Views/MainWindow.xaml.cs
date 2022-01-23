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

namespace Zadanie_Calc_Wpf
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

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход из программы",
    MessageBoxButton.OKCancel,
    MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.WindowStyle = WindowStyle.ToolWindow;
            aboutWindow.ShowDialog();
        }
    }
}
