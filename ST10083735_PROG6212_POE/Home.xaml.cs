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
        public Home()
        {
            InitializeComponent();
        }

        private void getStartedbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowStudyHoursBtnClicked != null)
                ShowStudyHoursBtnClicked(this, EventArgs.Empty);
        }
    }
}
