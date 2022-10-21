using Modules;
using Modules.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Timer = System.Threading.Timer;

namespace ST10083735_PROG6212_POE
{
    /// <summary>
    /// Interaction logic for AddModule.xaml
    /// </summary>
    public partial class AddModule : UserControl
    {

        
        public event EventHandler HideModulePageButtonClicked;
        //Create an object of the validation methods class
        private ValidationMethods newValid = new ValidationMethods();
        //Create a List of module objects
        

        public AddModule()
        {
            InitializeComponent();
        }


        private void Completebtn_Click(object sender, RoutedEventArgs e)
        {
            //If the module code text box isnt empty, it means that the user has not clicked th add module button
            //this means that the module has not been added to the list
            if (!String.IsNullOrEmpty(moduleCodetbx.Text))
            {
                confirmAddlb.Visibility = Visibility.Visible;
                yeschbkx.Visibility = Visibility.Visible;   
                nochbx.Visibility = Visibility.Visible; 
                //alert the user and tell them that the module has not been saved
                confirmAddlb.Content = $"Continue without adding {moduleCodetbx.Text}?";
                if (yeschbkx.IsChecked == true)
                {
                    //if they decide to continue without saving the module go to the home page
                    NavigateToHome();
                }
            } 
            //if the module code textbox is empty, the module has been saved, so navigate to the home page
            else if(String.IsNullOrEmpty(moduleCodetbx.Text))
            {
                confirmAddlb.Visibility = Visibility.Visible;
                yeschbkx.Visibility = Visibility.Visible;
                nochbx.Visibility = Visibility.Visible;
                NavigateToHome();
            }
            
            
        }

        

        private void AddModulebtn_Click(object sender, RoutedEventArgs e)
        {
            //do not allow wthe user to ad more than 6 modules
            //if(moduleList.Count == 6)
            //{
            //    ClearText();
            //    NavigateToHome(); 

            //}
            confirmAddlb.Visibility = Visibility.Collapsed;
            yeschbkx.Visibility = Visibility.Collapsed;
            nochbx.Visibility = Visibility.Collapsed;
            //make the confirmation label and error label invisible
            confirmlb.Visibility = Visibility.Collapsed;
            errorlb.Visibility = Visibility.Collapsed;

            //if the user has not entered the module code/name or chosen a start date for the semester, display the error label
            if (String.IsNullOrWhiteSpace(moduleCodetbx.Text) || String.IsNullOrWhiteSpace(moduleNametbx.Text) || String.IsNullOrWhiteSpace(datedp.SelectedDate.ToString()))
            {
                errorlb.Visibility = Visibility.Visible;
            }
            else
            {
                errorlb.Visibility = Visibility.Collapsed;
                //Check if the module code and name are valid
                //Module codes/names can contain spaces/underscores/numbers/letters
                bool isModuleNameValid = newValid.LettersNumbersWhiteSpace(moduleNametbx.Text);
                bool isModuleCodeValid = newValid.LettersNumbersWhiteSpace(moduleCodetbx.Text);
                //If the module code or name is invalid alert the user
                if (!isModuleNameValid || !isModuleCodeValid)
                {
                    MessageBox.Show("Module names and codes only allow letters, digits, underscores and spaces.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ClearText();
                }
                else
                {
                    ////If the list already contains modules
                    //if (moduleList != null)
                    //{
                    //    //Check if the Module the user wants to create already exists
                    //    foreach(Module module in moduleList)
                    //    {
                    //        if (module.ModuleCode.Equals(moduleCodetbx.Text))
                    //        {
                    //            //Alert the user that the module exists
                    //            MessageBox.Show($"{moduleCodetbx.Text} already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //            //Clear all inputs
                    //            ClearText();
                    //            return;
                    //        }
                    //    }
                    //}
                   
                    //Save the information input from the user
                    string moduleCode = moduleCodetbx.Text;
                    string moduleName = moduleNametbx.Text;
                    decimal classHours = Convert.ToDecimal(hoursspn.Value);
                    decimal credits = Convert.ToDecimal(creditspn.Value);
                    decimal weeks = Convert.ToDecimal(weeksspn.Value);
                    DateTime startdate = datedp.SelectedDate.Value;

                    int userID = (int)this.DataContext;
                    ModuleManagement newMod = new ModuleManagement();

                    Module module = new Module
                    {                        
                        ModuleCode = moduleCode,
                        ModuleName = moduleName,
                        ClassHours = classHours,
                        Credits = credits,
                        WeeksInSemester = weeks,
                        SemesterStartDate = startdate,
                        UserId = userID
                    };
                
                    newMod.AddModule(module);
                   

                    //on the confirmation label add the code of the module so the user knows which module has been added 
                    confirmlb.Content = $"{moduleCode} Added.";
                    confirmlb.Visibility = Visibility.Visible;

                    //Clear all textboxes so the user can add another module
                    ClearText();

                    //The user must at least add one module before they can click the complete button.
                    completebtn.Visibility = Visibility.Visible;

                    //Once the user has added all the information about the semester make the semester inputs invisible
                    numberOfWeekslb.Visibility = Visibility.Collapsed;
                    weeksspn.Visibility = Visibility.Collapsed;
                    startDatelb.Visibility = Visibility.Collapsed;
                    datedp.Visibility = Visibility.Collapsed;
                }
               
            }
                     
        }

        //Code to navigate to the home page
        private void NavigateToHome()
        {
            confirmlb.Content = "";         

            if (HideModulePageButtonClicked != null)
            {
                HideModulePageButtonClicked(this, EventArgs.Empty);
            }
        }

        private void AddModule_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Clear all the input text once the user leaves the page           
            ClearText();
        }

        private void ClearText()
        {
            //clear all textboxes and set numeric up downs to their minimum value
            moduleCodetbx.Clear();
            moduleNametbx.Clear();
            creditspn.Value = creditspn.MinValue;
            hoursspn.Value = hoursspn.MinValue;
            errorlb.Visibility = Visibility.Collapsed;
            confirmAddlb.Visibility = Visibility.Collapsed;
            yeschbkx.Visibility = Visibility.Collapsed;
            nochbx.Visibility = Visibility.Collapsed;
        }

        private void Yeschbkx_Checked(object sender, RoutedEventArgs e)
        {
            //Uncheck the no box if the ye box is checked
            if (yeschbkx.IsChecked == true)
            {
                nochbx.IsChecked = false;
            }

        }

        private void Nochbx_Checked(object sender, RoutedEventArgs e)
        {
            //Uncheck the yes box if the no box is checked
            if (nochbx.IsChecked == true)
            {
                yeschbkx.IsChecked = false;
            }
        }




    }
}
