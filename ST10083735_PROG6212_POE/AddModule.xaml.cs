using Modules;
using Modules.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Module = Modules.Models.Module;

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

        public AddModule()
        {
            InitializeComponent();
        }


        private void Completebtn_Click(object sender, RoutedEventArgs e)
        {
            //If the module code text box isnt empty, it means that the user has not clicked the add module button
            //this means that the module has not been added to the database
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
            else if (String.IsNullOrEmpty(moduleCodetbx.Text))
            {
                NavigateToHome();
            }


        }


        private void AddModulebtn_Click(object sender, RoutedEventArgs e)
        {
            moduleExistslb.Visibility = Visibility.Collapsed;
            confirmAddlb.Visibility = Visibility.Collapsed;
            yeschbkx.Visibility = Visibility.Collapsed;
            nochbx.Visibility = Visibility.Collapsed;

            //make the confirmation label and error label invisible
            addedModuleslstbx.Visibility = Visibility.Collapsed;
            errorlb.Visibility = Visibility.Collapsed;

            ModuleManagement newMod = new ModuleManagement();
            int userID = (int)this.DataContext;
            //retrieve a list of all modules for the current user in the database
            List<Module> modules = newMod.GetModules(userID);

            ModuleLimitReached(modules);


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
                    HideExtraComponents();
                    nameErrorlb.Visibility = Visibility.Visible;
                    moduleCodetbx.Text = "";
                    moduleNametbx.Text = "";
                }
                else
                {
                    nameErrorlb.Visibility = Visibility.Collapsed;
                    string moduleCode = moduleCodetbx.Text;
                    ////If the list already contains modules
                    if (modules.Count != 0)
                    {
                        //Check if the Module the user wants to create already exists
                        bool moduleExists = newMod.ModuleExists(moduleCode, userID);

                        if (moduleExists)
                        {
                            //Hide components like error messages, to show the label that
                            //tells the user that the module already exists
                            HideExtraComponents();
                            moduleExistslb.Visibility = Visibility.Visible;
                            //Clear textboxes
                            moduleCodetbx.Text = "";
                            moduleNametbx.Text = "";
                            return;
                        }
                    }

                    //Save the information input from the user                   
                    string moduleName = moduleNametbx.Text;
                    decimal classHours = Convert.ToDecimal(hoursspn.Value);
                    decimal credits = Convert.ToDecimal(creditspn.Value);
                    decimal weeks = Convert.ToDecimal(weeksspn.Value);
                    DateTime startdate = datedp.SelectedDate.Value;

                    SemesterManagement semesterManagement = new SemesterManagement();
                    //If the user has no saved modules but they have already enetered semester information
                    //then update the current saved semester
                    if (modules.Count == 0 && semesterManagement.SemesterExists(userID))
                    {
                        semesterManagement.UpdateSemester(userID,startdate,weeks);
                    }
                    //Otherwise, create a new semester
                    else if (modules.Count == 0 && !semesterManagement.SemesterExists(userID))
                    {
                        UserSemester semester = new UserSemester
                        {
                            UserId = userID,
                            WeeksInSemester = weeks,
                            SemesterStartDate = startdate
                        };
                        semesterManagement.AddSemester(semester);
                    }   
                              
                    //Create a module object
                    Module module = new Module
                    {
                        ModuleCode = moduleCode,
                        ModuleName = moduleName,
                        ClassHours = classHours,
                        Credits = credits,
                        UserId = userID
                    };
                    
                    //Add the module
                    newMod.AddModule(module);

                    
                    //on the confirmation listbox add the code of the module so the user knows which module has been added 
                    addedModuleslstbx.Items.Add($"{moduleCode} Added.");
                    addedModuleslstbx.Visibility = Visibility.Visible;

                    //Clear all textboxes so the user can add another module
                    ClearText();

                    //The user must at least add one module before they can click the complete button.
                    completebtn.Visibility = Visibility.Visible;

                    //Once the user has added all the information about the semester make the semester inputs invisible
                    HideSemesterComponents();
                    modules = newMod.GetModules(userID);
                    ModuleLimitReached(modules);
                }




            }

        }

        private void ModuleLimitReached(List<Module> modules)
        {
            //do not allow the user to add more than 6 modules
            if (modules.Count == 6)
            {
                //make the information label visible that tells the user they can only add 6 modules
                infolb.Visibility = Visibility.Visible;
                //hide all the components to prevent the user from entering data
                HideAllComponents();
                //clear all fields
                ClearText();
                //move to home page
                NavigateToHome();
            }
        }
        
        
        //Code to navigate to the home page
        private void NavigateToHome()
        {
            addedModuleslstbx.Items.Clear();

            if (HideModulePageButtonClicked != null)
            {
                HideModulePageButtonClicked(this, EventArgs.Empty);
            }
        }

        private void AddModule_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                moduleExistslb.Visibility = Visibility.Collapsed;
                addedModuleslstbx.Visibility = Visibility.Collapsed;
                //Clear the list box
                addedModuleslstbx.Items.Clear();
                ModuleManagement newMod = new ModuleManagement();
                
                //get the user id of the current logged in user
                int userID = (int)this.DataContext;
                
                //retrieve their modules
                List<Module> modules = newMod.GetModules(userID);
                SemesterManagement manageSemester = new SemesterManagement();

                //if the user has 6 modules then hide all components and display the label that tells them they may only save 6 modules
                if (modules.Count == 6)
                {
                   HideAllComponents();
                   infolb.Visibility = Visibility.Visible;
                }
                else if (modules.Count > 0)
                {
                    //otherwise, set the value of the date and week components to the values of the current semester
                    //this is done so that the data passes the checks in the add button which checks that all components are not null
                    weeksspn.Value = (double?)manageSemester.GetWeeksInSemester(userID);
                    datedp.SelectedDate = manageSemester.GetSemesterStartDate(userID);
                    
                    //Hide the semester components so that they cannot edit the semester details
                    HideSemesterComponents();
                    
                    //show all other module components
                    ShowAllComponents();

                    //hide error labels
                    infolb.Visibility = Visibility.Hidden;
                    nameErrorlb.Visibility = Visibility.Hidden;
                    
                    //Add all saved modules to the list box
                    foreach (Module module in modules)
                    {
                        addedModuleslstbx.Items.Add($"{module.ModuleCode} Added.");
                    }
                    addedModuleslstbx.Visibility = Visibility.Visible;

                }
                else if (modules.Count == 0)
                {
                    //if the user has no saved modules, show them the semester components
                    ShowSemesterComponents();
                    addedModuleslstbx.Visibility = Visibility.Hidden;
                }

            }

            //Clear all the input text once the user leaves the page
            ClearText();
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

        private void ShowSemesterComponents()
        {
            numberOfWeekslb.Visibility = Visibility.Visible;
            weeksspn.Visibility = Visibility.Visible;
            startDatelb.Visibility = Visibility.Visible;
            datedp.Visibility = Visibility.Visible;
        }

        private void HideSemesterComponents()
        {
            numberOfWeekslb.Visibility = Visibility.Hidden;
            weeksspn.Visibility = Visibility.Hidden;
            startDatelb.Visibility = Visibility.Hidden;
            datedp.Visibility = Visibility.Hidden;
        }


        private void ClearText()
        {
            //clear all textboxes and set numeric up downs to their minimum value
            moduleCodetbx.Clear();
            moduleNametbx.Clear();
            creditspn.Value = creditspn.MinValue;
            hoursspn.Value = hoursspn.MinValue;
            HideExtraComponents();
        }

        private void HideExtraComponents()
        {
            confirmAddlb.Visibility = Visibility.Hidden;
            yeschbkx.Visibility = Visibility.Hidden;
            nochbx.Visibility = Visibility.Hidden;           
            nameErrorlb.Visibility = Visibility.Hidden;
        }

        private void HideAllComponents()
        {
            moduleInfolb.Visibility = Visibility.Hidden;
            moduleCodelb.Visibility = Visibility.Hidden;
            moduleCodetbx.Visibility = Visibility.Hidden;
            moduleNamelb.Visibility = Visibility.Hidden;
            moduleNametbx.Visibility = Visibility.Hidden;
            numCreditslb.Visibility = Visibility.Hidden;
            creditspn.Visibility = Visibility.Hidden;
            classHourslb.Visibility = Visibility.Hidden;
            hoursspn.Visibility = Visibility.Hidden;
            addModulebtn.Visibility = Visibility.Hidden;
            HideSemesterComponents();
        }

        private void ShowAllComponents()
        {
            moduleInfolb.Visibility = Visibility.Visible;
            moduleCodelb.Visibility = Visibility.Visible;
            moduleCodetbx.Visibility = Visibility.Visible;
            moduleNamelb.Visibility = Visibility.Visible;
            moduleNametbx.Visibility = Visibility.Visible;
            numCreditslb.Visibility = Visibility.Visible;
            creditspn.Visibility = Visibility.Visible;
            classHourslb.Visibility = Visibility.Visible;
            hoursspn.Visibility = Visibility.Visible;
            addModulebtn.Visibility = Visibility.Visible;

        }

        
    }
}
