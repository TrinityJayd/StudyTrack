using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

            //Author:andreask
            //https://stackoverflow.com/questions/25585491/showing-user-control-from-another-user-controls-button-click-in-main-window
            landingPage.HideButtonClicked += OnHideButtonClicked;
        }

        //Author:andreask
        //https://stackoverflow.com/questions/25585491/showing-user-control-from-another-user-controls-button-click-in-main-window
        private void OnHideButtonClicked(object? sender, EventArgs e)
        {
            landingPage.Visibility = Visibility.Collapsed;
            home.Visibility = Visibility.Visible;
        }

        //youtube reference
        public void SetActiveUserControl(UserControl control)
        {
            landingPage.Visibility = Visibility.Collapsed;
            home.Visibility = Visibility.Collapsed;

            control.Visibility = Visibility.Visible;

        }

        private void homebtn_Click(object sender, RoutedEventArgs e)
        {
            SetActiveUserControl(home);
           
            
        }

        private void mainLb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetActiveUserControl(landingPage);
        }


    }
}
