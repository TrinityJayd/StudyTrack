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
            //Add the items to the combobox
            //these items are what the user can sort by
            itemscmb.Items.Clear();
            itemscmb.Items.Add("Study time per week");
            itemscmb.Items.Add("Time studied");
            itemscmb.Items.Add("Time left");
            //Set the datagrid ItemSource to null
            moduleDG.ItemsSource = null;

            //get the id of the logged in user
            int userID = (int)this.DataContext;
            
            //get a list of all their modules
            UserModules = moduleManagement.GetModules(userID);

            //Use LINQ to get only the fields we require from the data context
            //only if the list is not empty,
            if (UserModules.Count != 0)
            {
                //select specific fields from the list
                Modules = (from m in UserModules
                           select new DGModule
                           {
                               ModuleCode = m.ModuleCode,
                               //Use the from ticks method so that the time may be shown to the user in a simple way
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
            //clear the datagrid
            moduleDG.ItemsSource = null;

            //check the current order string of the label
            string text = orderlb.Content.ToString();
            //Modules = (from m in UserModules
            //           select new DGModule
            //           {
            //               ModuleCode = m.ModuleCode,
            //               SelfStudyHours = TimeSpan.FromTicks(m.SelfStudyHours),
            //               HoursStudied = TimeSpan.FromTicks(m.HoursStudied),
            //               HoursLeft = TimeSpan.FromTicks(m.HoursLeft),
            //           }).ToList();

            //get the item the user wants to sort by
            string itemToSortBy = itemscmb.SelectedItem.ToString();
            //If the buttons caption contans a down arrow then sort the list in ascending order
            if (text.Equals("🡣"))
            {
               
                switch (itemToSortBy)
                {
                    //sort by study time ascending
                    case "Study time per week":
                        Modules = Modules.OrderBy(m => m.SelfStudyHours).ToList();
                        break;
                    //sort by time studied ascending
                    case "Time studied":
                        Modules = Modules.OrderBy(m => m.HoursStudied).ToList();
                        break;
                    //sort by time left ascending
                    case "Time left":
                        Modules = Modules.OrderBy(m => m.HoursLeft).ToList();
                        break;
                }
                //change the button caption to show that the list is now sorted in ascending order
                orderlb.Content = "🡡";
            }
            else
            {
                switch (itemToSortBy)
                {
                    //sort by study time descending
                    case "Study time per week":
                        Modules = Modules.OrderByDescending(m => m.SelfStudyHours).ToList();
                        break;
                    //sort by time studied descending
                    case "Time studied":
                        Modules = Modules.OrderByDescending(m => m.HoursStudied).ToList();
                        break;
                    //sort by time left descending
                    case "Time left":
                        Modules = Modules.OrderByDescending(m => m.HoursLeft).ToList();
                        break;
                }
                //change the button caption to show that the list is now sorted in descending order
                orderlb.Content = "🡣";
            }

            moduleDG.ItemsSource = Modules;

        }

        private void itemscmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //always set the default order text to ascending
            orderlb.Content = "🡣";
            
            //only if the user has selected what to sort by then make the order button visible
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
