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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : UserControl
    {
        public event EventHandler HideSignUpButtonClicked;
        public SignUp()
        {
            InitializeComponent();
        }

        private void Completebtn_Click(object sender, RoutedEventArgs e)
        {
            messagetb.Text = "";
            if (String.IsNullOrEmpty(usernametxt.Text) || String.IsNullOrEmpty(passwordtxt.Text) || String.IsNullOrEmpty(confirmpasswordtxt.Text)
                || String.IsNullOrEmpty(nametxt.Text) || String.IsNullOrEmpty(lastnametxt.Text) || String.IsNullOrEmpty(emailtxt.Text) || String.IsNullOrEmpty(celltxt.Text))
            {
                messagetb.Text = "Missing Information. All fields are required.";
            }
            else if (passwordtxt.Text != confirmpasswordtxt.Text)
            {
                messagetb.Text = "Passwords do not match";
            }
            else
            {
                Account account = new Account();
                User user = new User
                {
                    Username = usernametxt.Text,
                    Password = passwordtxt.Text,
                    Name = nametxt.Text,
                    Lastname = lastnametxt.Text,
                    Email = emailtxt.Text,
                    CellNumber = celltxt.Text
                };

                account.Register(user);
                ClearText();
                NavigateToHome();

            }
                
                
            
        }

        //Code to navigate to the home page
        private void NavigateToHome()
        {
            if (HideSignUpButtonClicked != null)
            {
                HideSignUpButtonClicked(this, EventArgs.Empty);
            }
        }
        

        private void ClearText()
        {
            //clear all textboxes            
            usernametxt.Text = "";
            passwordtxt.Text = "";
            confirmpasswordtxt.Text = "";
            nametxt.Text = "";
            lastnametxt.Text = "";
            emailtxt.Text = "";
            celltxt.Text = "";
            messagetb.Text = "";
        }
    }
}
