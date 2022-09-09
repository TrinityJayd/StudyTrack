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

namespace ST10083735_PROG6212_POE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();
            SetActiveUserControl(landingPage);
        }


        public void SetActiveUserControl (UserControl control) { 
        landingPage.Visibility = Visibility.Collapsed;
        control.Visibility = Visibility.Visible;

        }

        private void homebtn_Click(object sender, RoutedEventArgs e)
        {
            SetActiveUserControl(landingPage);
        }

        private void mainLb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetActiveUserControl(landingPage);
        }
    }
}
