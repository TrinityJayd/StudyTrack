﻿using System;
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
        public RecordHours()
        {
            InitializeComponent();
        }

        private void completebtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowRecordHoursClicked != null)
                ShowRecordHoursClicked(this, EventArgs.Empty);
        }
    }
}
