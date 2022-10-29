using Modules;
using Modules.Models;
using System;
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
        public List<DGModule> Modules { get; set; }
        ModuleManagement moduleManagement = new ModuleManagement();
        List<Module> UserModules;
        public HoursLeft()
        {
            InitializeComponent();
        }

        private void HoursLeft_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Set the datagrid ItemSource to null
            moduleDG.ItemsSource = null;

            int userID = (int)this.DataContext;
            UserModules = moduleManagement.GetModules(userID);

            //Use LINQ to get only the fields we require from the data context
            //only if the list is not null,

            if (UserModules.Count != 0)
            {
                Modules = (from m in UserModules
                           select new DGModule
                           {
                               ModuleCode = m.ModuleCode,
                               SelfStudyHours = TimeSpan.FromTicks(m.SelfStudyHours),
                               HoursStudied = TimeSpan.FromTicks(m.HoursStudied),
                               HoursLeft = TimeSpan.FromTicks(m.HoursLeft),
                               DateLastStudied = m.DateLastStudied
                           }).ToList();
                //if there are modules saved, display the datagrid
                noModuleslb.Visibility = Visibility.Collapsed;
                moduleDG.Visibility = Visibility.Visible;
                infolb.Visibility = Visibility.Visible;

                moduleDG.ItemsSource = Modules;
                orderBySelfStudybtn.Visibility = Visibility.Visible;
            }
            else
            {
                //If there are no modules in the list, show the user the label that states that there are no modules saved
                moduleDG.Visibility = Visibility.Collapsed;
                infolb.Visibility = Visibility.Collapsed;
                noModuleslb.Visibility = Visibility.Visible;
            }




        }

        private void OrderBySelfStudybtn_Click(object sender, RoutedEventArgs e)
        {
            moduleDG.ItemsSource = null;
            string text = orderBySelfStudybtn.Content.ToString();
            //If the buttons caption contans a down arrow then sort the list in descending order
            if (text.Contains("🡣"))
            {
                Modules = (from m in UserModules
                           select new DGModule
                           {
                               ModuleCode = m.ModuleCode,
                               SelfStudyHours = TimeSpan.FromTicks(m.SelfStudyHours),
                               HoursStudied = TimeSpan.FromTicks(m.HoursStudied),
                               HoursLeft = TimeSpan.FromTicks(m.HoursLeft),
                               DateLastStudied = m.DateLastStudied
                           }).OrderByDescending(x => x.SelfStudyHours).ToList();
                orderBySelfStudybtn.Content = "  Self Study Hours   🡡";
            }
            else
            {
                //If the buttons caption contans an up arrow then sort the list in descending order
                Modules = (from m in UserModules
                           select new DGModule
                           {
                               ModuleCode = m.ModuleCode,
                               SelfStudyHours = TimeSpan.FromTicks(m.SelfStudyHours),
                               HoursStudied = TimeSpan.FromTicks(m.HoursStudied),
                               HoursLeft = TimeSpan.FromTicks(m.HoursLeft),
                               DateLastStudied = m.DateLastStudied
                           }).OrderBy(x => x.SelfStudyHours).ToList();
                orderBySelfStudybtn.Content = "  Self Study Hours   🡣";
            }

            moduleDG.ItemsSource = Modules;

        }
    }
}
