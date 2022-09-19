using Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ViewModules.xaml
    /// </summary>
    public partial class ViewModules : UserControl
    {
        public List<Module> Modules { get; set; }
        public ViewModules()
        {
            InitializeComponent();
            
        }

        private void ViewModules_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Set the itemsource of the datagrid to null
            moduleDG.ItemsSource = null;

            //Save the list
            Modules = (List<Module>)this.DataContext;
            
            if (viewModules.Visibility == Visibility.Visible)
            {
                //if the list is null, display the label that tells users that there are no modules added
                if(Modules == null)
                {
                    moduleDG.Visibility = Visibility.Collapsed;
                    noModuleslb.Visibility = Visibility.Visible;
                }
                else
                {
                    //otherwise, make the datagrid visible and add the list to the datagrid itemsource
                    noModuleslb.Visibility = Visibility.Collapsed;
                    moduleDG.Visibility = Visibility.Visible;
                    moduleDG.ItemsSource = Modules;
                   
                }
                
            }
        }

        
    }
}
