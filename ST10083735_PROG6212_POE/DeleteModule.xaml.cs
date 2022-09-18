using Modules;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ST10083735_PROG6212_POE
{
    /// <summary>
    /// Interaction logic for DeleteModule.xaml
    /// </summary>
    public partial class DeleteModule : UserControl
    {

        public event EventHandler HideDeleteButtonClicked;
        private List<Module> moduleList = new List<Module>();
      

        public DeleteModule()
        {
            InitializeComponent();
            
        }

      

        private void completebtn_Click(object sender, RoutedEventArgs e)
        {
            
            

            if (modulecmb.SelectedIndex == -1 || (yeschbkx.IsChecked == false && nochbx.IsChecked == false))
            {
                errorlb.Visibility = Visibility.Visible;
            }
            else
            {
                string moduleToDelete = modulecmb.SelectedItem.ToString();


                foreach(Module module in moduleList)
                {
                    if (module.ModuleCode.Equals(moduleToDelete) && (yeschbkx.IsChecked == true))
                    {
                        moduleList.Remove(module);
                        modulecmb.Items.Remove(module.ModuleCode);
                        break;
                    }
                }

                yeschbkx.IsChecked = false;
                nochbx.IsChecked = false;
                modulecmb.SelectedIndex = -1;
                errorlb.Visibility = Visibility.Collapsed;
                this.DataContext = moduleList;
                if (HideDeleteButtonClicked != null)
                {
                    HideDeleteButtonClicked(this, EventArgs.Empty);
                    
                }



            }
            

        }

        private void yeschbkx_Checked(object sender, RoutedEventArgs e)
        {
            if (yeschbkx.IsChecked == true)
            {
                nochbx.IsChecked = false;
            }

        }

        private void nochbx_Checked(object sender, RoutedEventArgs e)
        {
            if (nochbx.IsChecked == true)
            {
                yeschbkx.IsChecked = false;
            }
        }

        private void DeleteModule_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            moduleList = (List<Module>)this.DataContext;
            modulecmb.Items.Clear();
           
            if (this.Visibility == Visibility.Visible)
            {
                if (moduleList != null)
                {
                    foreach (Module module in moduleList)
                    {
                        modulecmb.Items.Add(module.ModuleCode);
                    }
                }
            }
        }
    }
}
