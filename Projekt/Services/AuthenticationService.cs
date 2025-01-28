using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt.Enums;
using Projekt.Models;


namespace Projekt.Services
{
    public class AuthenticationService
    {
        private const string _usersPath = "../../../Data/users.txt";

        public static event Action<string, bool> PasswordVerified;
        static AuthenticationService()
        {
            if (!File.Exists(_usersPath))
            {
                File.Create(_usersPath).Dispose();
            }
        }

        public static void SavePassword(User user, string password)
        {
            if (File.ReadLines(_usersPath).Any(line => line.Split(';')[0] == user.Username))
            {
                Console.WriteLine($"Użytkownik {user.Username} już istnieje w systemie.");
                return;
            }

            File.AppendAllText(_usersPath, $"{user.Username};{password};{user.Role}\n");
        }
        public static bool VerifyPassword(string username, string password)
        {
            foreach (var line in File.ReadAllLines(_usersPath))
            {
                var parts = line.Split(';');
                if (parts[0] == username && parts[1] == password)
                {
                    PasswordVerified?.Invoke(username, true);
                    return true;
                }
            }
            PasswordVerified?.Invoke(username, false);
            return false;
        }
    }
}
