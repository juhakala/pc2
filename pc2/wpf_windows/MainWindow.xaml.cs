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

namespace pc2.wpf_windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Menu();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tester_page page1 = new tester_page();
            this.Content = page1;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new tester_page();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page1();
        }
    }
}
