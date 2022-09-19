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

        private void HoursLeft_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Set the datagrid ItemSource to null
            moduleDG.ItemsSource = null;
            //Use LINQ to get only the fields we require from the data context
            //only if the list is not null
            Modules = (List<Module>)this.DataContext;
            if (Modules != null)
            {
                var Modules = from Module in (List<Module>)this.DataContext
                              select new
                              {
                                  Module.ModuleCode,
                                  Module.SelfStudyHours,
                                  Module.HoursStudied,
                                  Module.HoursLeft,
                                  Module.DateLastStudied
                              };
            }
            


            if (hoursLeft.Visibility == Visibility.Visible)
            {
                //If there are no modules in the list, show the user the label that states that there are no modules saved
                if (Modules == null)
                {
                    moduleDG.Visibility = Visibility.Collapsed;
                    infolb.Visibility = Visibility.Collapsed;
                    noModuleslb.Visibility = Visibility.Visible;
                }
                else
                {
                    //if there are modules saved, display the datagrid
                    noModuleslb.Visibility = Visibility.Collapsed;
                    moduleDG.Visibility = Visibility.Visible;
                    infolb.Visibility = Visibility.Visible;
                    moduleDG.ItemsSource = Modules;

                }

            }
        }
    }
}
