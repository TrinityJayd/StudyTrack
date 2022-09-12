using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Modules;

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
            home.ShowAddModulesBtnClicked += OnShowAddModulesBtnClicked;
            home.ShowDeleteModulesBtnClicked += OnShowDeleteModulesBtnClicked;
            home.ShowHoursBtnClicked += OnShowHoursBtnClicked;
            home.ShowModulesBtnClicked += OnShowModulesBtnClicked;
            
            home.ShowStudyHoursBtnClicked += OnShowStudyHoursBtnClicked;
            deleteModule.HideDeleteButtonClicked += OnHideDeleteButtonClicked;

        }

        private void OnHideDeleteButtonClicked(object? sender, EventArgs e)
        {
            SetActiveUserControl(home);
        }

        private void OnShowStudyHoursBtnClicked(object? sender, EventArgs e)
        {
            SetActiveUserControl(recordHours);
        }

        

        private void OnShowModulesBtnClicked(object? sender, EventArgs e)
        {
            SetActiveUserControl(viewModule);

        }

        private void OnShowHoursBtnClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnShowDeleteModulesBtnClicked(object? sender, EventArgs e)
        {
           
            deleteModule.modules = addModule.modules;
            SetActiveUserControl(deleteModule);

        }

        private void OnShowAddModulesBtnClicked(object? sender, EventArgs e)
        {
            
            SetActiveUserControl(addModule);

        }

        private void OnHideSemesterDetailsButtonClicked(object? sender, EventArgs e)
        {
            
            SetActiveUserControl(home);
        }

        private void OnHideModulePageButtonClicked(object? sender, EventArgs e)
        {           
            SetActiveUserControl(semesterDetails);
        }

        ////Author:andreask
        ////https://stackoverflow.com/questions/25585491/showing-user-control-from-another-user-controls-button-click-in-main-window
        private void OnHideButtonClicked(object? sender, EventArgs e)
        {
            SetActiveUserControl(addModule);
        }

        ////youtube reference
        public void SetActiveUserControl(UserControl control)
        {
            landingPage.Visibility = Visibility.Collapsed;
            home.Visibility = Visibility.Collapsed;
            addModule.Visibility = Visibility.Collapsed;
            semesterDetails.Visibility = Visibility.Collapsed;
            recordHours.Visibility = Visibility.Collapsed;
            deleteModule.Visibility = Visibility.Collapsed;
            viewModule.Visibility = Visibility.Collapsed;

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
