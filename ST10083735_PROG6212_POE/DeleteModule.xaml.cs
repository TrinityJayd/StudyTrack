﻿using Modules;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ST10083735_PROG6212_POE
{
    /// <summary>
    /// Interaction logic for DeleteModule.xaml
    /// </summary>
    public partial class DeleteModule : Page
    {
       

        private List<Module> moduleList = new List<Module>();
        public List<Module> modules { get; set; }
        public DeleteModule()
        {
            InitializeComponent();

        }

      

        private void completebtn_Click(object sender, RoutedEventArgs e)
        {
           
            if (modulecmb.SelectedItem.ToString() == null || (yeschbkx.IsChecked == false && nochbx.IsChecked == false))
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
                        return;
                    }
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

        private void deleteModule_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            modulecmb.Items.Clear();

            moduleList = modules;
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
