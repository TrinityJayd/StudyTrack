using DbManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace Modules
{
    public class Account
    {
        //Create an object of validation class
        ValidationMethods security = new ValidationMethods();

        public bool Login(string username, string password)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            
            //Check if the user exists
            bool userFound = appDataContext.Users.Any(u => u.Username.Equals(username));

            //set default value to false
            bool isLoggedIn = false;

            //only if the the user exists, check the password
            if (userFound == true)
            {
                //retrieve the user from the database
                var loginUser = appDataContext.Users.FirstOrDefault(u => u.Username.Equals(username));
                //check if the password is correct
                if (security.HashPassword($"{username}{password}").Equals(loginUser.Password) == true)
                {
                    //return log in status
                    isLoggedIn = true;
                    return isLoggedIn;
                }
            }
            //return log in status
            return isLoggedIn;
        }

        public async Task Register(User user)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //hash the password
            var hashedPassword = security.HashPassword($"{user.Username}{user.Password}");
            //set the password to the hashed password
            user.Password = hashedPassword;

            var lastEntry = await appDataContext.Users.OrderByDescending(x => x.UserId).FirstOrDefaultAsync();
            
            
            if (lastEntry == null)
            {
                user.UserId = 1;
            }
            else
            {
                user.UserId = lastEntry.UserId + 1;
            }
            //add the user to the database
            appDataContext.Users.Add(user);
            await appDataContext.SaveChangesAsync();

        }

        public int GetUserID(string username)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //check if the user exists
            bool userFound = appDataContext.Users.Any(u => u.Username.Equals(username));
            int userID = 0;
            //if the user exists retrieve the user object
            if (userFound == true)
            {
                var loginUser = appDataContext.Users.FirstOrDefault(u => u.Username.Equals(username));
                userID = loginUser.UserId;
            }

            //return userID
            return userID;
        }

        public bool UsernameExists(string username)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //check if a user with the same username exists
            bool userFound = appDataContext.Users.Any(u => u.Username.Equals(username));
            return userFound;
        }

        public bool EmailExists(string email)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //check if a user with the same email exists
            bool emailFound = appDataContext.Users.Any(u => u.Email.Equals(email));
            return emailFound;
        }

        public bool PhoneExists(string phone)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //check if a user with the same phone number exists
            bool phoneFound = appDataContext.Users.Any(u => u.CellNumber.Equals(phone));
            return phoneFound;
        }

    }  
}
