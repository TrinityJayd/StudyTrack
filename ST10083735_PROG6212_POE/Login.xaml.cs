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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public event EventHandler LoginSuccess;
        public event EventHandler RegisterButtonClicked;
        public Login()
        {
            InitializeComponent();
        }

        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(usernametxt.Text) || String.IsNullOrEmpty(passwordtxt.Password))
            {
                messagetb.Text = "Please enter username and password";
            }
            else
            {
                Account account = new Account();
                bool isLoggedIn = account.Login(usernametxt.Text, passwordtxt.Password);
                if (isLoggedIn)
                {
                    this.DataContext = account.GetUserID(usernametxt.Text);
                    if (LoginSuccess != null)
                    {
                        LoginSuccess(this, EventArgs.Empty);
                        ClearText();
                    }
                }
                else
                {
                    messagetb.Text = "Invalid username or password.";
                }
            }
            
        }

        private void signUpbtn_Click(object sender, RoutedEventArgs e)
        {
            if (RegisterButtonClicked != null)
            {
                RegisterButtonClicked(this, EventArgs.Empty);
                ClearText();
            }

        }

        private void ClearText()
        {
            usernametxt.Text = "";
            passwordtxt.Password = "";
        }
    }
}
