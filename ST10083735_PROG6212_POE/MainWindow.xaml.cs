using Modules;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ST10083735_PROG6212_POE
{

    public partial class MainWindow : Window
    {
        //Code Attribution
        //Author: Abhinav Sharma
        //Link: https://stackoverflow.com/questions/22573295/sharing-data-between-2-usercontrols-in-wpf
        //Create a dependency property for the list of module objects
        public List<Module> MainList
        {
            get { return MainList; }
            set { SetValue(MainListProperty, value); }
        }

        //Code to enable binding to the dependency property
        public static readonly DependencyProperty MainListProperty =
            DependencyProperty.Register("MainList", typeof(List<Module>), typeof(MainWindow), new PropertyMetadata(null));


        public MainWindow()
        {
            InitializeComponent();

            SetActiveUserControl(landingPage);

            //Author:andreask
            //https://stackoverflow.com/questions/25585491/showing-user-control-from-another-user-controls-button-click-in-main-window
            //Subscribe to the event of the user control so that other user controls can be shown
            landingPage.HideButtonClicked += OnHideButtonClicked;
            addModule.HideModulePageButtonClicked += OnHideModulePageButtonClicked;
            home.ShowAddModulesBtnClicked += OnShowAddModulesBtnClicked;
            home.ShowDeleteModulesBtnClicked += OnShowDeleteModulesBtnClicked;
            home.ShowHoursBtnClicked += OnShowHoursBtnClicked;
            home.ShowModulesBtnClicked += OnShowModulesBtnClicked;
            home.ShowStudyHoursBtnClicked += OnShowStudyHoursBtnClicked;
            recordHours.ShowRecordHoursClicked += OnShowRecordHoursClicked;           
            deleteModule.HideDeleteButtonClicked += OnHideDeleteButtonClicked;
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
            //First make all user controls invisible
            landingPage.Visibility = Visibility.Collapsed;
            home.Visibility = Visibility.Collapsed;
            addModule.Visibility = Visibility.Collapsed;
            recordHours.Visibility = Visibility.Collapsed;
            deleteModule.Visibility = Visibility.Collapsed;
            viewModule.Visibility = Visibility.Collapsed;
            hoursLeft.Visibility = Visibility.Collapsed;

            //Make the user control recieved as a parameter visible
            control.Visibility = Visibility.Visible;
        }

        private void mainLb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Make the landing page visible
            SetActiveUserControl(landingPage);
        }

        private void homebtn_Click(object sender, RoutedEventArgs e)
        {
            //Make the home page visible
            SetActiveUserControl(home);
        }

        private void OnShowRecordHoursClicked(object? sender, EventArgs e)
        {
            //Make the home page visible
            SetActiveUserControl(home);
        }

        private void OnHideDeleteButtonClicked(object? sender, EventArgs e)
        {
            //Make the home page visible
            SetActiveUserControl(home);
        }

        private void OnShowStudyHoursBtnClicked(object? sender, EventArgs e)
        {
            //Make the page that allows the user to record the hours they have studied
            SetActiveUserControl(recordHours);
        }


        private void OnShowModulesBtnClicked(object? sender, EventArgs e)
        {
            //Makes the page that allows users to view the list of all their modules visible
            SetActiveUserControl(viewModule);
        }

        private void OnShowHoursBtnClicked(object? sender, EventArgs e)
        {
            //Makes the page that allows users to check how many hours they have left to study visible
            SetActiveUserControl(hoursLeft);
        }

        private void OnShowDeleteModulesBtnClicked(object? sender, EventArgs e)
        {
            //Makes the page that allows users to delete modules visible
            SetActiveUserControl(deleteModule);
        }

        private void OnShowAddModulesBtnClicked(object? sender, EventArgs e)
        {
            //Makes the page that allows users to add modules visible
            SetActiveUserControl(addModule);
        }


        private void OnHideModulePageButtonClicked(object? sender, EventArgs e)
        {
            //Makes the home page visible
            SetActiveUserControl(home);
        }



        private void exitbtn_Click(object sender, RoutedEventArgs e)
        {
            //Code to exit the application
            System.Windows.Application.Current.Shutdown();
        }

        
    }
}
