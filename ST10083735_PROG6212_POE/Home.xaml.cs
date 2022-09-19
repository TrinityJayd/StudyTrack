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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        //Create button event handlers for all user controls
        public event EventHandler ShowRecordHoursBtnClicked;
        public event EventHandler ShowAddModulesBtnClicked;
        public event EventHandler ShowHoursBtnClicked;       
        public event EventHandler ShowModulesBtnClicked;
        public event EventHandler ShowDeleteModulesBtnClicked;
        public Home()
        {
            InitializeComponent();
        }

        
        //Event handler for when the user wants to record hours
        private void RecordHoursbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowRecordHoursBtnClicked != null)
                ShowRecordHoursBtnClicked(this, EventArgs.Empty);
        }

        //Event handler for when the user wants to add modules
        private void AddModulesbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowAddModulesBtnClicked != null)
                ShowAddModulesBtnClicked(this, EventArgs.Empty);

        }

        //Event handler for when the user wants to view th hours they need to study for a specific module
        private void ViewHoursbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowHoursBtnClicked != null)
                ShowHoursBtnClicked(this, EventArgs.Empty);
        }


        //Event handler for when the user wants to view a list of all their modules
        private void ViewModulesListbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowModulesBtnClicked != null)
                ShowModulesBtnClicked(this, EventArgs.Empty);
        }

        //Event handler for when the user wants to delete a module
        private void DeleteModulebtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowDeleteModulesBtnClicked != null)
                ShowDeleteModulesBtnClicked(this, EventArgs.Empty);
        }
         
        
    }
}
