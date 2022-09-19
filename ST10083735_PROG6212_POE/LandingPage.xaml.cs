using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
    /// Interaction logic for LandingPage.xaml
    /// </summary>
    public partial class LandingPage : UserControl
    {
        //Author:andreask
        //https://stackoverflow.com/questions/25585491/showing-user-control-from-another-user-controls-button-click-in-main-window 
        //Create an event within the control that is raised on button click
        public event EventHandler HideButtonClicked;

        public LandingPage()
        {
            InitializeComponent();
        }

         //Event handler if the user clicks th getting started button
        private void GetStartedbtn_Click(object sender, RoutedEventArgs e)
        {
            if (HideButtonClicked != null)
                HideButtonClicked(this, EventArgs.Empty);
        }
    }
}
