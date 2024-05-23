using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model.Builders
{
    public class UserBuilder
    {
        private int _id;
        private string _name;
        private string _email;
        private string _password;
        private UserType _userType;
        private string _phone;

        public UserBuilder SetId(int id)
        {
            _id = id;
            return this;
        }

        public UserBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public UserBuilder SetEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder SetPassword(string password)
        {
            _password = password;
            return this;
        }

        public UserBuilder SetUserType(UserType userType)
        {
            _userType = userType;
            return this;
        }

        public UserBuilder SetPhone(string phone)
        {
            _phone = phone;
            return this;
        }

        public User Build()
        {
            // Hash the password before creating the User instance
            string hashedPassword;
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(_password));
                hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }

            return new User
            {
                Id = _id,
                Name = _name,
                Email = _email,
                Password = hashedPassword,
                UserType = _userType,
                Phone = _phone
            };
        }
    }
}
