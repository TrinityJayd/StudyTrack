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

        private async void Completebtn_Click(object sender, RoutedEventArgs e)
        {
            //set the textbox used to communicate errors to empty
            messagetb.Text = "";
            //check if any fields are empty
            if (String.IsNullOrEmpty(usernametxt.Text) || String.IsNullOrEmpty(passwordtxt.Password) || String.IsNullOrEmpty(confirmpasswordtxt.Password)
                || String.IsNullOrEmpty(nametxt.Text) || String.IsNullOrEmpty(lastnametxt.Text) || String.IsNullOrEmpty(emailtxt.Text) || String.IsNullOrEmpty(celltxt.Text))
            {
                messagetb.Text = "Missing Information. All fields are required.";
            }
            //check if the users password and confirm password fields dont match
            else if (passwordtxt.Password != confirmpasswordtxt.Password)
            {
                messagetb.Text = "Passwords do not match";
            }
            else
            {
                //check is the passwors is valid
                if (validation.passwordRequirements(passwordtxt.Password) == false)
                {
                    messagetb.Text = "Password must contain at least 8 characters, 1 uppercase letter, 1 lowercase letter, 1 number and 1 special character.";
                }
                //check if the phone number is valid
                else if (validation.IsPhoneNumberValid(celltxt.Text) == false)
                {
                    messagetb.Text = "Cell number must be numeric and contain 10 digits.";
                }
                //check if the name is valid
                else if (validation.OnlyLetters(nametxt.Text) == false)
                {
                    messagetb.Text = "Name must only contain letters.";
                }
                //check if the last name is valid
                else if (validation.OnlyLetters(lastnametxt.Text) == false)
                {
                    messagetb.Text = "Last name must only contain letters.";
                }
                //check if the email is valid
                else if (validation.IsEmailValid(emailtxt.Text) == false)
                {
                    messagetb.Text = "Email is not valid.";
                }
                else
                {
                    Account account = new Account();
                    //create a user object
                    User user = new User
                    {
                        Username = usernametxt.Text,
                        Password = passwordtxt.Password,
                        Name = nametxt.Text,
                        Lastname = lastnametxt.Text,
                        Email = emailtxt.Text,
                        CellNumber = celltxt.Text
                    };

                    //check if the username is already taken
                    if (account.UsernameExists(user.Username) == true)
                    {
                        messagetb.Text = "Username already exists.";
                    }
                    //check if the email is already in use
                    else if (account.EmailExists(user.Email) == true)
                    {
                        messagetb.Text = "User with this email already exists.";
                    }
                    //check if the phone number is already in use
                    else if (account.PhoneExists(user.CellNumber) == true)
                    {
                        messagetb.Text = "User with this cell number already exists.";
                    }
                    else
                    {
                        //register the user
                        await account.Register(user);
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
