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
    /// Interaction logic for ViewStudySessions.xaml
    /// </summary>
    public partial class ViewStudySessions : UserControl
    {
        public List<DGSession> Sessions { get; set; }
        StudySessionManagement management = new StudySessionManagement();
        ModuleManagement moduleManagement = new ModuleManagement();
        List<StudySession> sessionsList;
        public ViewStudySessions()
        {
            InitializeComponent();
        }

        private void viewSessions_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //clear the module combobox
            modulecmb.Items.Clear();
            //get the logged in users id
            int userID = (int)this.DataContext;

            //Use LINQ to get only the fields we require from the data context
            //only if the list is not null,
            if (this.Visibility == Visibility.Visible)
            {
                // if the user has any modules saved then add them to the combobox
                if (moduleManagement.GetModules(userID).Count != 0)
                {
                    List<Module> modules = moduleManagement.GetModules(userID);
                    foreach (Module m in modules)
                    {
                        modulecmb.Items.Add(m.ModuleCode);
                    }
                    //get the sessions they have saved
                    sessionsList = management.GetSessions(userID);
                }
            }
            else
            {
                sessionDG.Visibility = Visibility.Collapsed;
                noSessionslb.Visibility = Visibility.Collapsed;
            }

        }

        private void modulecmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                //get the logged in users id
                int userID = (int)this.DataContext;
                //Set the datagrid ItemSource to null
                sessionDG.ItemsSource = null;

                //if they have study sessions saved then show them for that specific module
                if (sessionsList.Count != 0)
                {
                    Sessions = (from s in sessionsList
                                where s.ModuleCode.Equals(modulecmb.SelectedValue.ToString())
                                select new DGSession
                                {
                                    ModuleCode = s.ModuleCode,
                                    TimeStudied = TimeSpan.FromTicks(s.HoursStudied),
                                    DateStudied = s.DateStudied
                                }).ToList();

                    // if there are sessions for that module then show them
                    if (Sessions.Count != 0)
                    {
                        //if there are modules saved, display the datagrid
                        noSessionslb.Visibility = Visibility.Collapsed;
                        sessionDG.Visibility = Visibility.Visible;
                        sessionDG.ItemsSource = Sessions;
                    }
                    else
                    {
                        //If there are no session in the list, show the user the label that states that there are no modules saved
                        sessionDG.Visibility = Visibility.Collapsed;
                        noSessionslb.Visibility = Visibility.Visible;
                    }

                }
                else
                {
                    noSessionslb.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
