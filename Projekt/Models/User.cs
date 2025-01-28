using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Projekt.Enums;
namespace Projekt.Models
{
    public abstract class User
    {
        public string Username { get; set; }
        public Role Role { get; set; }

        public User(string username) 
        {
            Username = username;
        }
    }
}
