using Modules;
using Modules.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace ST10083735_PROG6212_POE
{
    /// <summary>
    /// Interaction logic for ViewModules.xaml
    /// </summary>


    public partial class ViewModules : UserControl
    {
        public List<Module> Modules { get; set; }
        ModuleManagement modules = new ModuleManagement();
        private List<Module> moduleList = new List<Module>();
        public ViewModules()
        {
            InitializeComponent();
            
        }

        private void ViewModules_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Set the itemsource of the datagrid to null
            moduleDG.ItemsSource = null;
          

            if (viewModules.Visibility == Visibility.Visible)
            {
                int userID = (int)this.DataContext;
                //Save the list
                if (modules.GetModules(userID).Count != 0)
                {
                    moduleList = modules.GetModules(userID);
                }

                if (moduleList.Count != 0)
                {
                    //make the datagrid visible and add the list to the datagrid itemsource
                    noModuleslb.Visibility = Visibility.Collapsed;
                    moduleDG.Visibility = Visibility.Visible;
                    Modules = moduleList;
                    moduleDG.ItemsSource = Modules;
                }
                else
                {
                    //if the list is null, display the label that tells users that there are no modules added
                    moduleDG.Visibility = Visibility.Collapsed;
                    noModuleslb.Visibility = Visibility.Visible;
                }
               
                
            }
        }

        
    }
}
