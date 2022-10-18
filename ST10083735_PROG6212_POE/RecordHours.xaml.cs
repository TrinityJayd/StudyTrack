using Modules;
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

                foreach (Module module in moduleList)
                {
                    //Update the details of the module
                    if (module.ModuleCode.Equals(moduleToUpdate))
                    {
                        //add the time studied to the current value of the time studied
                        module.HoursStudied += (TimeSpan)timespedt.Value;
                        //save the last date studied
                        module.DateLastStudied = datedp.SelectedDate.Value; ;

                        //if the user studies more than is required then set the hours left
                        //that they need to study to 0
                        if (module.HoursStudied > module.SelfStudyHours)
                        {
                            module.HoursLeft = TimeSpan.Zero;
                        }
                        else
                        {
                            //otherwise, subtract the times
                            module.HoursLeft = module.SelfStudyHours - module.HoursStudied;
                        }
                        
                    }
                }
                //Navigate to home page
                if (HideRecordHoursClicked != null)
                    HideRecordHoursClicked(this, EventArgs.Empty);
            }
            
        }

        private void RecordHours_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Save the list
            if((List<Module>)this.DataContext != null)
            {
                moduleList = (List<Module>)this.DataContext;
            }
           

            //Clear the combobox
            modulecmb.Items.Clear();

            if (this.Visibility == Visibility.Visible)
            {
                //If there are modules stored in the list then add it to the combobox
                if (moduleList.Count != 0)
                {
                    //Make the semester start date the beginning boundary
                    //The user cannot study for a module when the semester/classes has not started
                    datedp.DisplayDateStart = moduleList[0].SemesterStartDate;

                    //Make the end boundary, the current date, the user can only log past study sessions not future ones
                    datedp.DisplayDateEnd = DateTime.Now;
                   
                    //Add modules to the module combobox
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
    }
}
