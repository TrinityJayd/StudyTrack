using Modules;
using Modules.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ST10083735_PROG6212_POE
{
    /// <summary>
    /// Interaction logic for DeleteModule.xaml
    /// </summary>
    public partial class DeleteModule : UserControl
    {
        ModuleManagement modules = new ModuleManagement();
        public event EventHandler HideDeleteButtonClicked;

        public DeleteModule()
        {
            InitializeComponent();

        }

        private void Completebtn_Click(object sender, RoutedEventArgs e)
        {
            //Check if the user has chosen a module and confirmed if they wanted to delete the module
            if (modulecmb.SelectedIndex == -1 || (yeschbkx.IsChecked == false && nochbx.IsChecked == false))
            {
                errorlb.Visibility = Visibility.Visible;
            }
            else
            {
                //If the user has confirmed deleteion, then delete the module
                if (yeschbkx.IsChecked == true)
                {
                    string moduleToDelete = modulecmb.SelectedItem.ToString();
                    int userID = (int)this.DataContext;
                    modules.DeleteModule(moduleToDelete, userID);
                }
                
                //Navigate to home page
                if (HideDeleteButtonClicked != null)
                {
                    HideDeleteButtonClicked(this, EventArgs.Empty);
                }



            }


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
        

        //If the user control is visible then populate the combobox
        //with the module codes in the list
        private void DeleteModule_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            int userID = (int)this.DataContext;
            List<Module> moduleList = modules.GetModules(userID);

            //Clear the combobox of any other modules it contained
            modulecmb.Items.Clear();

            if (this.Visibility == Visibility.Visible)
            {
                //Only if the list is has modules stored, it must try to add modules to the combobox
                if (moduleList != null)
                {
                    foreach (Module module in moduleList)
                    {
                        modulecmb.Items.Add(module.ModuleCode);
                    }
                }
            }
            else
            {
                //If the page is not visible clear all the inputs
                yeschbkx.IsChecked = false;
                nochbx.IsChecked = false;
                modulecmb.SelectedIndex = -1;

                //Make the confirmation components invisible
                errorlb.Visibility = Visibility.Collapsed;
                confirmrtb.Visibility = Visibility.Collapsed;
                yeschbkx.Visibility = Visibility.Collapsed;
                nochbx.Visibility = Visibility.Collapsed;
            }
        }

        //If the module selection changes
        //Change the confirmation label to ask the user if they are sure they want to delete that module
        private void Modulecmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Use text ranges to customize the look of the text
            confirmrtb.Document.Blocks.Clear();
            TextRange rangeOfText1 = new TextRange(confirmrtb.Document.ContentEnd, confirmrtb.Document.ContentEnd);
            rangeOfText1.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);

            
            rangeOfText1.Text = "Are you sure you want to delete ";

            //Different colour for the module code
            SolidColorBrush mySolidColorBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF8C52FF");

            TextRange rangeOfWord = new TextRange(confirmrtb.Document.ContentEnd, confirmrtb.Document.ContentEnd);


            rangeOfWord.Text = $"{modulecmb.SelectedItem}";

            //Customize the font/color of the text
            rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, mySolidColorBrush);
            rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);


            //Chage the font colour
            TextRange rangeOfText2 = new TextRange(confirmrtb.Document.ContentEnd, confirmrtb.Document.ContentEnd);

            rangeOfText2.Text = " ?";

            //Customize the font/color of the text
            rangeOfText2.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            rangeOfText2.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);

            //When the user selects something from the combobox, make the confirmation components visible
            confirmrtb.Visibility = Visibility.Visible;
            yeschbkx.Visibility = Visibility.Visible;
            nochbx.Visibility = Visibility.Visible;

        }
    }
}
