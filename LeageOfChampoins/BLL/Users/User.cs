using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Users
{
    public class User : IUser
    {
        private Owner? _owner;
        private string _user;
        //private readonly string PATH = $"{Environment.CurrentDirectory}\\JSON data\\SecretaryAccount.json";

        public User(Owner owner)
        {
            _user = String.Empty;
            this._owner = owner;
        }
        public User(string user)
        {
            this._user = user;
        }
        public object? GetUser()
        {
            if (!String.IsNullOrEmpty(_user))
            {
                return _user;
            }
            else if (_owner != null)
            {
                return _owner;
            }
            else
            {
                return null;
            }
        }

        public void SetUser(string user)
        {
            this._user = user;
            
        }
        public void SetUser(Owner owner)
        {
            this._owner = owner;
        }
        public static string CreatePasswordHash(string password)
        {
            byte[] salt;
#pragma warning disable SYSLIB0023 // Type or member is obsolete
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
#pragma warning restore SYSLIB0023 // Type or member is obsolete

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
      
        }
        public static string Secretary
        {
            get => "Secretary";
        }
        public static string Guest
        {
            get => "Guest";
        }
    }
}
