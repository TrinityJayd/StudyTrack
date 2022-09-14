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
            GridContainer.Children.Clear();
            GridContainer.Children.Add(new LandingPage());


            //Author:andreask
            //https://stackoverflow.com/questions/25585491/showing-user-control-from-another-user-controls-button-click-in-main-window
            landingPage.HideButtonClicked += OnHideButtonClicked;

            addModule.HideModulePageButtonClicked += OnHideModulePageButtonClicked;
            
            home.ShowAddModulesBtnClicked += OnShowAddModulesBtnClicked;
            home.ShowDeleteModulesBtnClicked += OnShowDeleteModulesBtnClicked;
            home.ShowHoursBtnClicked += OnShowHoursBtnClicked;
            home.ShowModulesBtnClicked += OnShowModulesBtnClicked;
            
            home.ShowStudyHoursBtnClicked += OnShowStudyHoursBtnClicked;
            deleteModule.HideDeleteButtonClicked += OnHideDeleteButtonClicked;

        }

        private void OnHideDeleteButtonClicked(object? sender, EventArgs e)
        {
            //SetActiveUserControl(home);
            GridContainer.Children.Clear();
            GridContainer.Children.Add(new Home());

        }

        private void OnShowStudyHoursBtnClicked(object? sender, EventArgs e)
        {
            //SetActiveUserControl(recordHours);
            GridContainer.Children.Clear();
            GridContainer.Children.Add(new RecordHours());
        }

        //private List<Module> selectList()
        //{
        //    if (deleteModule.modules == null)
        //    {
        //        return addModule.modules;
        //    }
        //    else if(addM)
        //    {
        //        return deleteModule.modules;
        //    }
        //}

        

        private void OnShowModulesBtnClicked(object? sender, EventArgs e)
        {
            //viewModule.modules = selectList();
            //SetActiveUserControl(viewModule);
            GridContainer.Children.Clear();
            GridContainer.Children.Add(new ViewModules());

        }

        private void OnShowHoursBtnClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnShowDeleteModulesBtnClicked(object? sender, EventArgs e)
        {          
            deleteModule.modules = addModule.modules;
            GridContainer.Children.Clear();
            GridContainer.Children.Add(new DeleteModule());

        }

        private void OnShowAddModulesBtnClicked(object? sender, EventArgs e)
        {

            GridContainer.Children.Clear();
            GridContainer.Children.Add(new AddModule());

        }

        

        private void OnHideModulePageButtonClicked(object? sender, EventArgs e)
        {
            GridContainer.Children.Clear();
            GridContainer.Children.Add(new AddModule());
        }

        ////Author:andreask
        ////https://stackoverflow.com/questions/25585491/showing-user-control-from-another-user-controls-button-click-in-main-window
        private void OnHideButtonClicked(object? sender, EventArgs e)
        {
            GridContainer.Children.Clear();
            GridContainer.Children.Add(new AddModule());
        }

        ////youtube reference
        //public void SetActiveUserControl(UserControl control)
        //{
        //    landingPage.Visibility = Visibility.Collapsed;
        //    home.Visibility = Visibility.Collapsed;
        //    addModule.Visibility = Visibility.Collapsed;           
        //    recordHours.Visibility = Visibility.Collapsed;
        //    deleteModule.Visibility = Visibility.Collapsed;
        //    viewModule.Visibility = Visibility.Collapsed;

        //    control.Visibility = Visibility.Visible;

        //}

        private void homebtn_Click(object sender, RoutedEventArgs e)
        {
          
            //SetActiveUserControl(home);
            GridContainer.Children.Clear();
            GridContainer.Children.Add(new Home());
         
        }

        private void mainLb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GridContainer.Children.Clear();
            GridContainer.Children.Add(new LandingPage());
            //SetActiveUserControl(landingPage);
        }


    }
}
