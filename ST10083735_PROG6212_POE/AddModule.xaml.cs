using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Modules;

namespace ST10083735_PROG6212_POE
{
    /// <summary>
    /// Interaction logic for AddModule.xaml
    /// </summary>
    public partial class AddModule : UserControl
    {
        public event EventHandler HideModulePageButtonClicked;

        private ValidationMethods newValid = new ValidationMethods();
        private Module newMod;
        public AddModule()
        {
            InitializeComponent();
        }

        private void completebtn_Click(object sender, RoutedEventArgs e)
        {
            errorlb.Visibility = Visibility.Collapsed;

            if (String.IsNullOrWhiteSpace(moduleCodetbx.Text) || String.IsNullOrWhiteSpace(moduleNametbx.Text) || String.IsNullOrWhiteSpace(numCreditstbx.Text) || String.IsNullOrWhiteSpace(classHourstbx.Text))
            {
                errorlb.Visibility = Visibility.Visible;
            }
            else 
            {
                string moduleCode = moduleCodetbx.Text;
                string moduleName = moduleNametbx.Text;
                int classHours = Convert.ToInt32(classHourstbx.Text);
                int credits = Convert.ToInt32(numCreditstbx.Text);

                
                modules.Add(new Module(moduleCode, moduleName, credits, classHours ));       
               
                if(modules.Count > 1)
                {
                    addModulebtn.IsEnabled = true;
                }

                

                if (HideModulePageButtonClicked != null)
                {
                    HideModulePageButtonClicked(this, EventArgs.Empty);
                }
                    
            }
            
        }

        //https://iditect.com/guide/csharp/csharp_howto_make_a_textbox_only_accept_numbers_in_wpf.html#:~:text=C%23%20How%20to%20make%20a%20TextBox%20only%20accept,into%20the%20textbox%20but%20only%20typing%20numeric%20input.
        private void TypeNumericValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void PasteNumericValidation(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String input = (String)e.DataObject.GetData(typeof(String));
                if (new Regex("[^0-9]+").IsMatch(input))
                {
                    e.CancelCommand();
                }
            }
            else e.CancelCommand();
        }

        private void addModulebtn_Click(object sender, RoutedEventArgs e)
        {
            
            moduleCodetbx.Clear();
            moduleNametbx.Clear();
            numCreditstbx.Clear();
            classHourstbx.Clear();
            completebtn_Click( sender,  e);
        }

        
    }
}
