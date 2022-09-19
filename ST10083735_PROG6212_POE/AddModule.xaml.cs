using Modules;
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
        private List<Module> moduleList = new List<Module>();

        public AddModule()
        {
            InitializeComponent();
        }


        private void Completebtn_Click(object sender, RoutedEventArgs e)
        {
            //If the module code text box isnt empty, it means that the user has not clicked th add module button
            //this means that the module has not been added to the list
            if (moduleCodetbx.Text != "")
            {
                //alert the user and tell them that the module has not been saved
                MessageBoxResult dialog = MessageBox.Show($"Are you sure you want to continue without adding {moduleNametbx.Text}?","Alert",MessageBoxButton.YesNo,MessageBoxImage.Warning);
                if (dialog.Equals(MessageBoxResult.Yes))
                {
                    //if they decide to continue without saving the module go to the home page
                    NavigateToHome();
                }
            } 
            //if the module code textbox is empty, the module has been saved, so navigate to the home page
            else if(moduleCodetbx.Text.Equals(""))
            {
                NavigateToHome();
            }
            
            
        }

        

        private void AddModulebtn_Click(object sender, RoutedEventArgs e)
        {
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
                //Check if the module code and name are valid
                //Module codes/names can contain spaces/underscores/numbers/letters
                bool isModuleNameValid = newValid.LettersNumbersWhiteSpace(moduleNametbx.Text);
                bool isModuleCodeValid = newValid.LettersNumbersWhiteSpace(moduleCodetbx.Text);
                //If the module code or name is invalid alert the user
                if (!isModuleNameValid || !isModuleCodeValid)
                {
                    MessageBox.Show("Module names and codes only allow letters, digits, underscores and spaces.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //If the list already contains modules
                    if (moduleList != null)
                    {
                        //Check if the Module the user wants to create already exists
                        foreach(Module module in moduleList)
                        {
                            if (module.ModuleCode.Equals(moduleCodetbx.Text))
                            {
                                //Alert the user that the module exists
                                MessageBox.Show($"{moduleCodetbx.Text} already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                //Clear all inputs
                                ClearText();
                                return;
                            }
                        }
                    }
                   
                    //Save the information input from the user
                    string moduleCode = moduleCodetbx.Text;
                    string moduleName = moduleNametbx.Text;
                    double classHours = Convert.ToDouble(hoursspn.Value);
                    double credits = Convert.ToDouble(creditspn.Value);
                    double weeks = Convert.ToDouble(weeksspn.Value);
                    DateTime startdate = datedp.SelectedDate.Value;

                    //add the module to the list
                    moduleList.Add(new Module(moduleCode, moduleName, credits, classHours, weeks, startdate));

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
            //Update the data context with the new list
            this.DataContext = moduleList;

            if (HideModulePageButtonClicked != null)
            {
                HideModulePageButtonClicked(this, EventArgs.Empty);
            }
        }

        private void addModule_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Clear all the input text once the user leaves the page
            this.DataContext = moduleList;
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
        }

        

        

       
    }
}
