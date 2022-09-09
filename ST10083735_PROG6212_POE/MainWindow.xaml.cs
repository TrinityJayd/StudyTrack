using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ST10083735_PROG6212_POE
{
    
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            SetActiveUserControl(landingPage);

            //Author:andreask
            //https://stackoverflow.com/questions/25585491/showing-user-control-from-another-user-controls-button-click-in-main-window
            landingPage.HideButtonClicked += OnHideButtonClicked;
            addModule.HideModulePageButtonClicked += OnHideModulePageButtonClicked;
            semesterDetails.HideSemesterDetailsButtonClicked += OnHideSemesterDetailsButtonClicked;
        }

        private void OnHideSemesterDetailsButtonClicked(object? sender, EventArgs e)
        {
            semesterDetails.Visibility = Visibility.Collapsed;
            home.Visibility = Visibility.Visible;
        }

        private void OnHideModulePageButtonClicked(object? sender, EventArgs e)
        {
            addModule.Visibility = Visibility.Collapsed;
            semesterDetails.Visibility = Visibility.Visible;
        }

        //Author:andreask
        //https://stackoverflow.com/questions/25585491/showing-user-control-from-another-user-controls-button-click-in-main-window
        private void OnHideButtonClicked(object? sender, EventArgs e)
        {
            landingPage.Visibility = Visibility.Collapsed;
            addModule.Visibility = Visibility.Visible;
        }

        //youtube reference
        public void SetActiveUserControl(UserControl control)
        {
            landingPage.Visibility = Visibility.Collapsed;
            home.Visibility = Visibility.Collapsed;
            addModule.Visibility = Visibility.Collapsed;
            semesterDetails.Visibility = Visibility.Collapsed;

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
