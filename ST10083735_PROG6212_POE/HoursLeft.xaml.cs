using Modules;
using Modules.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ST10083735_PROG6212_POE
{
    /// <summary>
    /// Interaction logic for HoursLeft.xaml
    /// </summary>
    public partial class HoursLeft : UserControl
    {
        public List<Module> Modules { get; set; }
        ModuleManagement moduleManagement = new ModuleManagement();
        
        public HoursLeft()
        {
            InitializeComponent();
        }

        private void HoursLeft_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Set the datagrid ItemSource to null
            moduleDG.ItemsSource = null;

            if (hoursLeft.Visibility == Visibility.Visible)
            {
                Modules = moduleManagement.GetModules((int)this.DataContext);

                //Use LINQ to get only the fields we require from the data context
                //only if the list is not null

                if (Modules != null)
                {
                    var Modules = from Module in moduleManagement.GetModules(1)
                                  select new
                                  {
                                      Module.ModuleCode,
                                      Module.SelfStudyHours,
                                      Module.HoursStudied,
                                      Module.HoursLeft,
                                      Module.DateLastStudied
                                  };
                }
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
                    orderBySelfStudybtn.Visibility = Visibility.Visible;
                    moduleDG.ItemsSource = Modules;
                }

            }
        }

        private void OrderBySelfStudybtn_Click(object sender, RoutedEventArgs e)
        {
            moduleDG.ItemsSource = null;
            string text = orderBySelfStudybtn.Content.ToString();
            //If the buttons caption contans a down arrow then sort the list in descending order
            if (text.Contains("🡣"))
            {
                var Modules = (from Module in moduleManagement.GetModules((int)this.DataContext)
                               select new
                               {
                                   Module.ModuleCode,
                                   Module.SelfStudyHours,
                                   Module.HoursStudied,
                                   Module.HoursLeft,
                                   Module.DateLastStudied
                               }).OrderByDescending(x => x.SelfStudyHours);
                orderBySelfStudybtn.Content = "  Self Study Hours   🡡";

            }
            else
            {
                //If the buttons caption contans an up arrow then sort the list in descending order
                var Modules = (from Module in moduleManagement.GetModules((int)this.DataContext)
                               select new
                               {
                                   Module.ModuleCode,
                                   Module.SelfStudyHours,
                                   Module.HoursStudied,
                                   Module.HoursLeft,
                                   Module.DateLastStudied
                               }).OrderBy(x => x.SelfStudyHours);
                orderBySelfStudybtn.Content = "  Self Study Hours   🡣";
            }

            moduleDG.ItemsSource = Modules;

        }
    }
}
