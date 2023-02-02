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

namespace UnBlindMe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void CtrShortcut1(Object sender, ExecutedRoutedEventArgs e)
        {
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19000000"));
        }

        public void CtrShortcut2(Object sender, ExecutedRoutedEventArgs e)
        {
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33000000"));
        }

        public void CtrShortcut3(Object sender, ExecutedRoutedEventArgs e)
        {
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C000000"));
        }

        public void CtrShortcut4(Object sender, ExecutedRoutedEventArgs e)
        {
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66000000"));
        }

        public void CtrShortcut5(Object sender, ExecutedRoutedEventArgs e)
        {
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F000000"));
        }

        public void CtrShortcut6(Object sender, ExecutedRoutedEventArgs e)
        {
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#99000000"));
        }

        public void CtrShortcut7(Object sender, ExecutedRoutedEventArgs e)
        {
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B2000000"));
        }

        public void CtrShortcut8(Object sender, ExecutedRoutedEventArgs e)
        {
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC000000"));
        }

        public void CtrShortcut9(Object sender, ExecutedRoutedEventArgs e)
        {
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E5000000"));
        }

        public void CtrShortcut0(Object sender, ExecutedRoutedEventArgs e)
        {
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
        }
        public void CtrShortcutA(Object sender, ExecutedRoutedEventArgs e)
        {
            if(this.Topmost == false)
            {
                this.Topmost = true;
            }
            else
            {
                this.Topmost = false;
            }
            
        }

    }
}
