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
        ValidationMethods validation = new ValidationMethods();
        public SignUp()
        {
            InitializeComponent();
        }

        private void Completebtn_Click(object sender, RoutedEventArgs e)
        {
            messagetb.Text = "";
            if (String.IsNullOrEmpty(usernametxt.Text) || String.IsNullOrEmpty(passwordtxt.Password) || String.IsNullOrEmpty(confirmpasswordtxt.Password)
                || String.IsNullOrEmpty(nametxt.Text) || String.IsNullOrEmpty(lastnametxt.Text) || String.IsNullOrEmpty(emailtxt.Text) || String.IsNullOrEmpty(celltxt.Text))
            {
                messagetb.Text = "Missing Information. All fields are required.";
            }
            else if (passwordtxt.Password != confirmpasswordtxt.Password)
            {
                messagetb.Text = "Passwords do not match";
            }
            else
            {                
                if (validation.passwordRequirements(passwordtxt.Password) == false)
                {
                    messagetb.Text = "Password must contain at least 8 characters, 1 uppercase letter, 1 lowercase letter, 1 number and 1 special character.";
                }
                else if (validation.IsPhoneNumberValid(celltxt.Text) == false)
                {
                    messagetb.Text = "Cell number must be numeric and contain 10 digits.";
                }
                else if (validation.OnlyLetters(nametxt.Text) == false)
                {
                    messagetb.Text = "Name must only contain letters.";
                }
                else if (validation.OnlyLetters(lastnametxt.Text) == false)
                {
                    messagetb.Text = "Last name must only contain letters.";
                }
                else if (validation.IsEmailValid(emailtxt.Text) == false)
                {
                    messagetb.Text = "Email is not valid.";
                }
                else
                {
                    Account account = new Account();
                    User user = new User
                    {
                        Username = usernametxt.Text,
                        Password = passwordtxt.Password,
                        Name = nametxt.Text,
                        Lastname = lastnametxt.Text,
                        Email = emailtxt.Text,
                        CellNumber = celltxt.Text
                    };
                    if (account.UsernameExists(user.Username) == true)
                    {
                        messagetb.Text = "Username already exists.";
                    }
                    else if (account.EmailExists(user.Email) == true)
                    {
                        messagetb.Text = "User with this email already exists.";
                    }
                    else if (account.PhoneExists(user.CellNumber) == true)
                    {
                        messagetb.Text = "User with this cell number already exists.";
                    }
                    else
                    {
                        
                        account.Register(user);
                        ClearText();
                        NavigateToHome();
                    }
                    
                }
                

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
            passwordtxt.Password = "";
            confirmpasswordtxt.Password = "";
            nametxt.Text = "";
            lastnametxt.Text = "";
            emailtxt.Text = "";
            celltxt.Text = "";
            messagetb.Text = "";
        }
    }
}
