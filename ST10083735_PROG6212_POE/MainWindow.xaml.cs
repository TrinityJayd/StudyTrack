using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Modules;

namespace ST10083735_PROG6212_POE
{
    
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
           

        }

        private void getStartedbtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeView(new AddModule());
        }

        private void homebtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeView(new Home());
        }

        

        private void exitbtn_Click(object sender, RoutedEventArgs e)
        {

        }

        public void ChangeView(Page view)
        {
            MainFrame.NavigationService.Navigate(view);
        }
    }
}
