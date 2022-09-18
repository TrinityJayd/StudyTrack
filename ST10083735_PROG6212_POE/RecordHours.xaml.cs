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
        public event EventHandler ShowRecordHoursClicked;
        private List<Module> moduleList = new List<Module>();
        public RecordHours()
        {
            InitializeComponent();
        }

        private void completebtn_Click(object sender, RoutedEventArgs e)
        {
            errorlb.Visibility = Visibility.Collapsed;
            if (modulecmb.SelectedIndex == -1 || String.IsNullOrWhiteSpace(datedp.SelectedDate.ToString()) || timespedt.Text.Equals("0 hours 0 minutes"))
            {
                errorlb.Visibility = Visibility.Visible;
            }
            else
            {
                string moduleToUpdate = modulecmb.SelectedItem.ToString();
                foreach (Module module in moduleList)
                {
                    if (module.ModuleCode.Equals(moduleToUpdate))
                    {
                        module.HoursStudied = (TimeSpan)timespedt.Value;
                        if(module.HoursStudied > module.SelfStudyHours)
                        {
                            module.HoursLeft = TimeSpan.Zero;
                        }
                        else
                        {
                            module.HoursLeft = module.SelfStudyHours - module.HoursStudied;
                        }
                        
                    }
                }
                if (ShowRecordHoursClicked != null)
                    ShowRecordHoursClicked(this, EventArgs.Empty);
            }
            
        }

        private void RecordHours_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
            moduleList = (List<Module>)this.DataContext;
            modulecmb.Items.Clear();

            if (this.Visibility == Visibility.Visible)
            {
                if (moduleList != null)
                {
                    datedp.DisplayDateStart = moduleList[0].SemesterStartDate;
                   
                    foreach (Module module in moduleList)
                    {
                        modulecmb.Items.Add(module.ModuleCode);
                    }
                }
            }
            else
            {
                modulecmb.SelectedIndex = -1;
                datedp.SelectedDate = null;
                timespedt.Text = "0 hours 0 minutes";
            }
        }
    }
}
