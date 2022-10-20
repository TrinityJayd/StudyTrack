using Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class Account
    {
        ValidationMethods security = new ValidationMethods();   

        public bool Login(string username, string password)
        {         
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            var userFound = appDataContext.Users.Any(u => u.Name.Equals(username));
            bool isLoggedIn = false;
            if (userFound == true)
            {
                var loginUser = appDataContext.Users.FirstOrDefault(u => u.Name.Equals(username));
                if (security.HashPassword($"{username}{password}").Equals(loginUser.Password) == true)
                {
                    isLoggedIn = true;
                    return isLoggedIn;
                }               
            }

            return isLoggedIn;
        }
        
        public async Task Register(User user)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            var hashedPassword = security.HashPassword($"{user.Username}{user.Password}");
            user.Password = hashedPassword;
            appDataContext.Users.Add(user);
            await appDataContext.SaveChangesAsync();
            
            
        }
    }
}
