using Modules;
using Modules.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for RecordHours.xaml
    /// </summary>
    public partial class RecordHours : UserControl
    {
        public event EventHandler HideRecordHoursClicked;
        private List<Module> moduleList = new List<Module>();
        ModuleManagement moduleManagement = new ModuleManagement();
        public RecordHours()
        {
            InitializeComponent();
            
        }

        private void Completebtn_Click(object sender, RoutedEventArgs e)
        {
            //Hide the error label
            errorlb.Visibility = Visibility.Collapsed;

            //Check if the user has filled in all required fields
            if (modulecmb.SelectedIndex == -1 || String.IsNullOrWhiteSpace(datedp.SelectedDate.ToString()) || timespedt.Text.Equals("0 hours 0 minutes"))
            {
                errorlb.Visibility = Visibility.Visible;
            }
            else
            {
                //Save the code of the module to update
                string moduleToUpdate = modulecmb.SelectedItem.ToString();

                TimeSpan hoursStudied = (TimeSpan)timespedt.Value;                

                int userID = (int)this.DataContext;
                moduleManagement.UpdateModule(hoursStudied.Ticks, datedp.SelectedDate.Value, moduleToUpdate, userID);

                
                //Navigate to home page
                if (HideRecordHoursClicked != null)
                    HideRecordHoursClicked(this, EventArgs.Empty);
            }
            
        }

        private void RecordHours_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            int userID = (int)this.DataContext;
            List<Module> moduleList = moduleManagement.GetModules(userID);

            //Clear the combobox of any other modules it contained
            modulecmb.Items.Clear();

            if (this.Visibility == Visibility.Visible)
            {
                //Only if the list is has modules stored, it must try to add modules to the combobox
                if (moduleList.Count != 0)
                {
                    //Make the semester start date the beginning boundary
                    //The user cannot study for a module when the semester/classes has not started
                    datedp.DisplayDateStart = moduleList[0].SemesterStartDate;

                    //Make the end boundary, the current date, the user can only log past study sessions not future ones
                    datedp.DisplayDateEnd = DateTime.Now;
                    foreach (Module module in moduleList)
                    {
                        modulecmb.Items.Add(module.ModuleCode);
                    }
                }
            }
            else
            {
                //Clear all inputs when the user control is not visible
                modulecmb.SelectedIndex = -1;
                datedp.SelectedDate = null;
                timespedt.Value = timespedt.MinValue;
            }
            
        }

        private void modulecmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (modulecmb.SelectedIndex != -1)
            {
                hourslb.Visibility = Visibility.Visible;
                timespedt.Visibility = Visibility.Visible;
                datedp.Visibility = Visibility.Visible;
                datelb.Visibility = Visibility.Visible;
            }
            else{
                hourslb.Visibility = Visibility.Collapsed;
                timespedt.Visibility = Visibility.Collapsed;
                datedp.Visibility = Visibility.Collapsed;
                datelb.Visibility = Visibility.Collapsed;
            }
        }
    }
}
