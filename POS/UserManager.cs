using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class UserManager
    {
        private List<Users> users = new List<Users>();
        private readonly string Key = "b14ca5898a4e4133bbce2ea2315a1916";

        // Register a new user
        public void RegisterUser(string name, string email, string password, UserRoles role)
        {
            PasswordSecurityHandler psh = new PasswordSecurityHandler();
            var encryptedPassword = psh.EncryptPassword(Key, password);
            var newUser = new Users(name, email, encryptedPassword, role);
            users.Add(newUser);
        }

        // Log in a user
        public Users LogInUserAuthentication(string email, string password)
        {
            PasswordSecurityHandler psh = new PasswordSecurityHandler();
            var encryptedPassword = psh.EncryptPassword(Key, password);
            return users.FirstOrDefault(u => u.Email == email && u.Password == encryptedPassword);
        }

        // Check if a user is an admin
        public bool IsAdmin(Users user)
        {
            return user.UserRole.RoleName == Roles.Admin.RoleName;
        }

        // Check if a user is a cashier
        public bool IsCashier(Users user)
        {
            return user.UserRole.RoleName == Roles.Cashier.RoleName;
        }
    }
}
