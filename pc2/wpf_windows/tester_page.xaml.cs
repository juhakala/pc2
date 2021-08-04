using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for tester_page.xaml
    /// </summary>
    public partial class tester_page : Page
    {
        public tester_page()
        {
            InitializeComponent();
            Button test_button = new Button();
            test_button.Content = "made in code";
            test_grid.Children.Add(test_button);
            Grid.SetColumn(test_button, 2);
            Grid.SetRow(test_button, 2);


        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("i said: YEET!");
        }
    }
}
