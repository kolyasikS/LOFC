using BLL.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Users
{
    public class UserPassword
    {
        private IEnumerable<Owner> _owners;
        private IEnumerable<Account> _accounts;

        public delegate void SetUser(dynamic user);
        public UserPassword(IEnumerable<Owner> owners, IEnumerable<Account> accounts)
        {
            _owners = owners;
            _accounts = accounts;
        }
        public Account? HasAccount(string login, string password, string PATH, SetUser? SetUser = null)
        {
            if (!File.Exists(PATH))
            {
                var secretaryAccount = JsonConvert.DeserializeObject<Account>(File.ReadAllText(PATH));
                if (IsDataCoincide(login, password, secretaryAccount?.LogIn, secretaryAccount?.Password ?? String.Empty))
                {
                    if (SetUser != null)
                    {
                        SetUser(User.Secretary);
                    }
                    return null;
                }
            }
            var result = _owners.Join(_accounts, own => own.AccountId, acc => acc.Id, (own, acc) => new { own, acc }).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                if (IsDataCoincide(login, password, result[i].acc.LogIn, result[i].acc.Password))
                {
                    if (SetUser != null)
                    {
                        SetUser(result[i]);
                    }
                    return result[i].acc;
                }
            }

            return null;
        }
        static private bool IsDataCoincide(string newLogin, string newPass, string? savedLogin, string savedPass)
        {
            byte[] hashBytes;
            try
            {
                hashBytes = Convert.FromBase64String(savedPass);
            }
            catch (Exception)
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(savedPass);
                savedPass = Convert.ToBase64String(plainTextBytes);
                hashBytes = Convert.FromBase64String(savedPass);
            }

            byte[] salt = new byte[16];

            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(newPass, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            if (newLogin != savedLogin)
            {
                return false;
            }
            return true;
        }
        public Account? HasAccount(string password)
        {
            var result = _owners.Join(_accounts, own => own.AccountId, acc => acc.Id, (own, acc) => new { own, acc }).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                if (IsDataCoincide(password, result[i].acc.Password))
                {
                    return result[i].acc;
                }
            }

            return null;
        }
        static public bool IsDataCoincide(string newPass, string savedPass)
        {
            byte[] hashBytes;
            try
            {
                hashBytes = Convert.FromBase64String(savedPass);
            }
            catch (Exception)
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(savedPass);
                savedPass = Convert.ToBase64String(plainTextBytes);
                hashBytes = Convert.FromBase64String(savedPass);
            }

            byte[] salt = new byte[16];

            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(newPass, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
        static public string ToHashedPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }
        static public string ToHashedPassword(SecureString securePassword)
        {
            var pointer = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
            var password = Marshal.PtrToStringUni(pointer) ?? String.Empty;

            return ToHashedPassword(password);
        }
    }
}
