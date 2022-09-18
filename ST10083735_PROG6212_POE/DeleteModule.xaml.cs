using Modules;
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

        public event EventHandler HideDeleteButtonClicked;
        private List<Module> moduleList = new List<Module>();


        public DeleteModule()
        {
            InitializeComponent();

        }



        private void completebtn_Click(object sender, RoutedEventArgs e)
        {



            if (modulecmb.SelectedIndex == -1 || (yeschbkx.IsChecked == false && nochbx.IsChecked == false))
            {
                errorlb.Visibility = Visibility.Visible;
            }
            else
            {
                string moduleToDelete = modulecmb.SelectedItem.ToString();

                moduleList.RemoveAll(x => x.ModuleCode == moduleToDelete);
                modulecmb.Items.Remove(moduleToDelete);
                

                this.DataContext = moduleList;
                if (HideDeleteButtonClicked != null)
                {
                    HideDeleteButtonClicked(this, EventArgs.Empty);

                }



            }


        }

        private void yeschbkx_Checked(object sender, RoutedEventArgs e)
        {
            if (yeschbkx.IsChecked == true)
            {
                nochbx.IsChecked = false;
            }

        }

        private void nochbx_Checked(object sender, RoutedEventArgs e)
        {
            if (nochbx.IsChecked == true)
            {
                yeschbkx.IsChecked = false;
            }
        }

        private void DeleteModule_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            moduleList = (List<Module>)this.DataContext;
            modulecmb.Items.Clear();

            if (this.Visibility == Visibility.Visible)
            {
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
                yeschbkx.IsChecked = false;
                nochbx.IsChecked = false;
                modulecmb.SelectedIndex = -1;
                errorlb.Visibility = Visibility.Collapsed;
                confirmrtb.Visibility = Visibility.Collapsed;
                yeschbkx.Visibility = Visibility.Collapsed;
                nochbx.Visibility = Visibility.Collapsed;
            }
        }

        private void modulecmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            confirmrtb.Document.Blocks.Clear();
            TextRange rangeOfText1 = new TextRange(confirmrtb.Document.ContentEnd, confirmrtb.Document.ContentEnd);


            //Display a different message based on what the user has left over at the end of the month.
            rangeOfText1.Text = "Are you sure you want to delete ";

            //Different colour and font for the amount
            SolidColorBrush mySolidColorBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF8C52FF");

            TextRange rangeOfWord = new TextRange(confirmrtb.Document.ContentEnd, confirmrtb.Document.ContentEnd);


            rangeOfWord.Text = $"{modulecmb.SelectedItem}";

            //Customize the font/color of the text
            rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, mySolidColorBrush);
            rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.ExtraBold);


            //Chage the font colour
            TextRange rangeOfText2 = new TextRange(confirmrtb.Document.ContentEnd, confirmrtb.Document.ContentEnd);

            rangeOfText2.Text = " ?";

            //Customize the font/color of the text
            rangeOfText2.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            rangeOfText2.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);

            confirmrtb.Visibility = Visibility.Visible;
            yeschbkx.Visibility = Visibility.Visible;
            nochbx.Visibility = Visibility.Visible;

        }
    }
}
