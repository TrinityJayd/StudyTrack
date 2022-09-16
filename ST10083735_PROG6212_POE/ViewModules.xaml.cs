using Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ViewModules.xaml
    /// </summary>
    public partial class ViewModules : UserControl
    {
        public List<Module> modules { get; set; }
        public ViewModules()
        {
            InitializeComponent();
            
        }

        private void viewModules_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
            if (viewModules.Visibility == Visibility.Visible)
            {
                moduleDG.ItemsSource = modules;
            }
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }
    }
}
