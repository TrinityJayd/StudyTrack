using Modules.Models;

namespace Modules
{
    public class Account
    {
        ValidationMethods security = new ValidationMethods();

        public bool Login(string username, string password)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            bool userFound = appDataContext.Users.Any(u => u.Name.Equals(username));
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
            int totalUsers = appDataContext.Users.Count();
            user.UserId = totalUsers + 1;
            appDataContext.Users.Add(user);
            await appDataContext.SaveChangesAsync();

        }

        public int GetUserID(string username)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            bool userFound = appDataContext.Users.Any(u => u.Name.Equals(username));
            int userID = 0;
            if (userFound == true)
            {
                var loginUser = appDataContext.Users.FirstOrDefault(u => u.Name.Equals(username));
                userID = loginUser.UserId;
            }

            return userID;
        }

        public bool UsernameExists(string username)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            bool userFound = appDataContext.Users.Any(u => u.Username.Equals(username));
            return userFound;
        }

        public bool EmailExists(string email)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            bool emailFound = appDataContext.Users.Any(u => u.Email.Equals(email));
            return emailFound;
        }

        public bool PhoneExists(string phone)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            bool phoneFound = appDataContext.Users.Any(u => u.CellNumber.Equals(phone));
            return phoneFound;
        }

    }  
}
