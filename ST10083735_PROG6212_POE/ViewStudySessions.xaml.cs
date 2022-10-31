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

            modulecmb.Items.Clear();
            int userID = (int)this.DataContext;

            //Use LINQ to get only the fields we require from the data context
            //only if the list is not null,
            if (this.Visibility == Visibility.Visible)
            {
                if (moduleManagement.GetModules(userID).Count != 0)
                {
                    List<Module> modules = moduleManagement.GetModules(userID);
                    foreach (Module m in modules)
                    {
                        modulecmb.Items.Add(m.ModuleCode);
                    }
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
                int userID = (int)this.DataContext;
                //Set the datagrid ItemSource to null
                sessionDG.ItemsSource = null;
                
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

                    if (Sessions.Count != 0)
                    {
                        //if there are modules saved, display the datagrid
                        noSessionslb.Visibility = Visibility.Collapsed;
                        sessionDG.Visibility = Visibility.Visible;
                        sessionDG.ItemsSource = Sessions;
                    }
                    else
                    {
                        //If there are no modules in the list, show the user the label that states that there are no modules saved
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
