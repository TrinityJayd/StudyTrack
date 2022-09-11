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
        public event EventHandler ShowStudyHoursBtnClicked;
        public event EventHandler ShowAddModulesBtnClicked;
        public event EventHandler ShowHoursBtnClicked;
        public event EventHandler ShowSemesterDetailsBtnClicked;
        public event EventHandler ShowModulesBtnClicked;
        public event EventHandler ShowDeleteModulesBtnClicked;
        public Home()
        {
            InitializeComponent();
        }

        

        private void recordHoursbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowStudyHoursBtnClicked != null)
                ShowStudyHoursBtnClicked(this, EventArgs.Empty);
        }

        private void addModulesbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowAddModulesBtnClicked != null)
                ShowAddModulesBtnClicked(this, EventArgs.Empty);
        }

        private void viewHoursbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowHoursBtnClicked != null)
                ShowHoursBtnClicked(this, EventArgs.Empty);
        }

        private void editSemesterDetailsbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowSemesterDetailsBtnClicked != null)
                ShowSemesterDetailsBtnClicked(this, EventArgs.Empty);
        }

        private void viewModulesListbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowModulesBtnClicked != null)
                ShowModulesBtnClicked(this, EventArgs.Empty);
        }

        private void deleteModulebtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowDeleteModulesBtnClicked != null)
                ShowDeleteModulesBtnClicked(this, EventArgs.Empty);
        }

        
    }
}
