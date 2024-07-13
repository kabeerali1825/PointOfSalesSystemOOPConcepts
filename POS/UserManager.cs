using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace POS
{
    public class UserManager
    {
        private List<Users> users = new List<Users>();
        private PassWordValidatorHandler passWordValidatorHandler = new PassWordValidatorHandler();
        private PasswordSecurityHandler psh = new PasswordSecurityHandler();
        private readonly string Key = "b14ca5898a4e4133bbce2ea2315a1916";

        // Register a new user
        public void RegisterUser(string name, string email, string password, UserRoles role)
        {
            if (!IsValidEmail(email))
            {
                throw new ArgumentException("Invalid email format.");
            }
            if (users.Any(u => u.Email == email))
            {
                throw new ArgumentException("User already exists with this email.");
            }

            bool isPasswordFine = passWordValidatorHandler.ValidatePassword(password);
            if (isPasswordFine)
            {
                var encryptedPassword = psh.EncryptPassword(Key, password);
                var newUser = new Users(name, email, encryptedPassword, role);
                users.Add(newUser);
            }
            else
            {
                throw new ArgumentException("Password Requirements are Not Fulfilled.");
            }
        }

        // Log in a user
        public Users LogInUserAuthentication(string email, string password)
        {
            bool isPasswordFine = passWordValidatorHandler.ValidatePassword(password);
            if (isPasswordFine)
            {
                var encryptedPassword = psh.EncryptPassword(Key, password);
                var user = users.FirstOrDefault(u => u.Email == email && u.Password == encryptedPassword);
                if (user == null)
                {
                    throw new ArgumentException("Invalid email or password.");
                }
                return user;
            }
            else
            {
                throw new ArgumentException("User is not Authenticated Due to Password Requirements Not Being Fulfilled.");
            }
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

        // Validate email format
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
