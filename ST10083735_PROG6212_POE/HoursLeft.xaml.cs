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

            itemscmb.Items.Clear();
            itemscmb.Items.Add("Study time per week");
            itemscmb.Items.Add("Time studied");
            itemscmb.Items.Add("Time left");
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
                           }).ToList();
                //if there are modules saved, display the datagrid
                noModuleslb.Visibility = Visibility.Collapsed;
                moduleDG.Visibility = Visibility.Visible;
                infolb.Visibility = Visibility.Visible;
                orderlb.Content = "🡣";
                sortbylb.Visibility = Visibility.Visible;
                itemscmb.Visibility = Visibility.Visible;              
                moduleDG.ItemsSource = Modules;

            }
            else
            {
                //If there are no modules in the list, show the user the label that states that there are no modules saved
                moduleDG.Visibility = Visibility.Collapsed;
                infolb.Visibility = Visibility.Collapsed;
                sortbylb.Visibility = Visibility.Collapsed;
                itemscmb.Visibility = Visibility.Collapsed;
                orderlb.Visibility = Visibility.Collapsed;
                noModuleslb.Visibility = Visibility.Visible;
            }




        }

        private void orderlb_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            moduleDG.ItemsSource = null;
            string text = orderlb.Content.ToString();
            Modules = (from m in UserModules
                       select new DGModule
                       {
                           ModuleCode = m.ModuleCode,
                           SelfStudyHours = TimeSpan.FromTicks(m.SelfStudyHours),
                           HoursStudied = TimeSpan.FromTicks(m.HoursStudied),
                           HoursLeft = TimeSpan.FromTicks(m.HoursLeft),
                       }).ToList();
            string itemToSortBy = itemscmb.SelectedItem.ToString();
            //If the buttons caption contans a down arrow then sort the list in descending order
            if (text.Equals("🡣"))
            {
                

                switch (itemToSortBy)
                {
                    case "Study time per week":
                        Modules = Modules.OrderBy(m => m.SelfStudyHours).ToList();
                        break;
                    case "Time studied":
                        Modules = Modules.OrderBy(m => m.HoursStudied).ToList();
                        break;
                    case "Time left":
                        Modules = Modules.OrderBy(m => m.HoursLeft).ToList();
                        break;
                }               
                orderlb.Content = "🡡";
            }
            else
            {
                switch (itemToSortBy)
                {
                    case "Study time per week":
                        Modules = Modules.OrderByDescending(m => m.SelfStudyHours).ToList();
                        break;
                    case "Time studied":
                        Modules = Modules.OrderByDescending(m => m.HoursStudied).ToList();
                        break;
                    case "Time left":
                        Modules = Modules.OrderByDescending(m => m.HoursLeft).ToList();
                        break;
                }
                orderlb.Content = "🡣";
            }

            moduleDG.ItemsSource = Modules;

        }

        private void itemscmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            orderlb.Content = "🡣";
            if (itemscmb.SelectedIndex > -1)
            {
                orderlb.Visibility = Visibility.Visible;
            }
            else
            {
                orderlb.Visibility = Visibility.Collapsed;
            }
           
        }
    }
}
