using Modules;
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
    /// Interaction logic for HoursLeft.xaml
    /// </summary>
    public partial class HoursLeft : UserControl
    {
        public List<Module> Modules { get; set; }
        public HoursLeft()
        {
            InitializeComponent();
        }

        private void hoursLeft_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            moduleDG.ItemsSource = null;

            Modules = (List<Module>)this.DataContext;

            if (hoursLeft.Visibility == Visibility.Visible)
            {
                if (Modules == null)
                {
                    moduleDG.Visibility = Visibility.Collapsed;
                    infolb.Visibility = Visibility.Collapsed;
                    noModuleslb.Visibility = Visibility.Visible;
                }
                else
                {
                    noModuleslb.Visibility = Visibility.Collapsed;
                    moduleDG.Visibility = Visibility.Visible;
                    infolb.Visibility = Visibility.Visible;
                    moduleDG.ItemsSource = Modules;

                }

            }
        }
    }
}
