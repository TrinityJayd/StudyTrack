using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Configuration;

namespace ST10083735_PROG6212_POE
{

    public partial class MainWindow : Window
    {
        ////Code Attribution
        ////Author: Abhinav Sharma
        ////Link: https://stackoverflow.com/questions/22573295/sharing-data-between-2-usercontrols-in-wpf
        ////Create a dependency property for the list of module objects
        public int UserID
        {
            get { return (int)GetValue(UserIDProperty); }
            set { SetValue(UserIDProperty, value); }
        }

        //Code to enable binding to the dependency property
        public static readonly DependencyProperty UserIDProperty =
            DependencyProperty.Register("UserID", typeof(int), typeof(MainWindow), new PropertyMetadata(null));


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
            home.ShowRecordHoursBtnClicked += OnShowRecordStudyHoursBtnClicked;
            recordHours.HideRecordHoursClicked += OnHideRecordHoursClicked;
            deleteModule.HideDeleteButtonClicked += OnHideDeleteButtonClicked;
            signUp.HideSignUpButtonClicked += OnHideSignUpButtonClicked;
            login.LoginSuccess += OnLoginSuccess;
            login.RegisterButtonClicked += Login_RegisterButtonClicked;
            home.ShowSessionsBtnClicked += OnShowSessionsBtnClicked;
        }

      
        ////Author:andreask
        ////https://stackoverflow.com/questions/25585491/showing-user-control-from-another-user-controls-button-click-in-main-window
        private void OnHideButtonClicked(object? sender, EventArgs e)
        {
            //Show the add module page after the user clicks get started on the landing page
            SetActiveUserControl(signUp);
        }

        //Show the user control that alows the user to view their study sessions for a specific module
        private void OnShowSessionsBtnClicked(object? sender, EventArgs e)
        {
            SetActiveUserControl(viewSessions);
        }
        
        private void OnLoginSuccess(object? sender, EventArgs e)
        {
            //If the user successfully logs in show the home page
            SetActiveUserControl(home);
            loginbtn.Visibility = Visibility.Collapsed;
            logoutbtn.Visibility = Visibility.Visible;
        }

        private void Login_RegisterButtonClicked(object? sender, EventArgs e)
        {
            //Show sign up page if the user clicks the sign up button on the login pages
            SetActiveUserControl(signUp);
        }

        private void OnHideSignUpButtonClicked(object? sender, EventArgs e)
        {
            //Show the login page after the user clicks complete on the registration
            SetActiveUserControl(login);
        }


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
            signUp.Visibility = Visibility.Collapsed;
            login.Visibility = Visibility.Collapsed;
            viewSessions.Visibility = Visibility.Collapsed;
            
            //Make the user control recieved as a parameter visible
            control.Visibility = Visibility.Visible;
        }

        private void MainLb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Make the landing page visible
            SetActiveUserControl(landingPage);
        }

        private void Homebtn_Click(object sender, RoutedEventArgs e)
        {
            //if the user id is 0 then the user is not logged in
            if (UserID == 0)
            {
                //If the user is not logged in show the login page
                SetActiveUserControl(login);
            }
            else
            {
                //If the user is logged in show the home page
                SetActiveUserControl(home);
            }

            
        }

        private void OnHideRecordHoursClicked(object? sender, EventArgs e)
        {
            //Make the home page visible after the user clicks complete on the record hours page
            SetActiveUserControl(home);
        }

        private void OnHideDeleteButtonClicked(object? sender, EventArgs e)
        {
            //Make the home page visible after the user clickc complete on the delete page
            SetActiveUserControl(home);
        }

        private void OnShowRecordStudyHoursBtnClicked(object? sender, EventArgs e)
        {
            //Make the page that allows the user to record the hours they have studied when
            //the user clicks the button on the home page
            SetActiveUserControl(recordHours);
        }


        private void OnShowModulesBtnClicked(object? sender, EventArgs e)
        {
            //Makes the page that allows users to view the list of all their modules visible when
            //the user clicks the button on the home page
            SetActiveUserControl(viewModule);
        }

        private void OnShowHoursBtnClicked(object? sender, EventArgs e)
        {
            //Makes the page that allows users to check how many hours they have left to study visible when
            //the user clicks the button on the home page
            SetActiveUserControl(hoursLeft);
        }

        private void OnShowDeleteModulesBtnClicked(object? sender, EventArgs e)
        {
            //Makes the page that allows users to delete modules visible when
            //the user clicks the button on the home page
            SetActiveUserControl(deleteModule);
        }

        private void OnShowAddModulesBtnClicked(object? sender, EventArgs e)
        {
            //Makes the page that allows users to add modules visible when
            //the user clicks the button on the home page
            SetActiveUserControl(addModule);
        }


        private void OnHideModulePageButtonClicked(object? sender, EventArgs e)
        {
            //Makes the home page visible
            SetActiveUserControl(home);
        }


        private void Exitbtn_Click(object sender, RoutedEventArgs e)
        {
            //Code to exit the application
            Application.Current.Shutdown();
        }

        private void signUpbtn_Click(object sender, RoutedEventArgs e)
        {
            //if a user is currently logged in and they click the sign up button log them out
            if (loginbtn.Visibility == Visibility.Collapsed)
            {
                UserID = 0;
                logoutbtn.Visibility = Visibility.Collapsed;
                loginbtn.Visibility = Visibility.Visible;

            }
            SetActiveUserControl(signUp);
        }

        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            //Show the login page
            SetActiveUserControl(login);
        }

        private void logoutbtn_Click(object sender, RoutedEventArgs e)
        {
            //set the user id to 0 because it means that no user is logged in
            UserID = 0;
            SetActiveUserControl(landingPage);
            logoutbtn.Visibility = Visibility.Collapsed;
            loginbtn.Visibility = Visibility.Visible;          
        }

        
    }
}
