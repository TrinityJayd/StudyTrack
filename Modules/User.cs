using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public string HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedPassword = hash.ComputeHash(passwordBytes);
            return Convert.ToHexString(hashedPassword);
        }

        public void Login(string username, string password)
        {
            //using AppDataContext appDataContext = new AppDataContext();
            //var userFound = appDataContext.Users.Any(u => u.Name.Equals(username));

            //if (userFound == true)
            //{
            //    var loginUser = appDataContext.Users.FirstOrDefault(u => u.Name.Equals(username));
            //    if (HashPassword($"{password}{loginUser.Salt}").Equals(loginUser.Password) == true)
            //    {
            //        MessageBox.Show("Logged in");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Not logged in");
            //    }
            //}
        }

        public void Register(string username, string password)
        {
         
            //using AppDataContext appDataContext = new AppDataContext();

            //var salt = DateTime.Now.ToString();
            //var hashedPassword = HashPassword($"{password}{salt}");
            //appDataContext.Add(new User() { Name = username, Password = hashedPassword, Salt = salt });
            //appDataContext.SaveChangesAsync();
            //MessageBox.Show("Registered");
        }
    }
}
