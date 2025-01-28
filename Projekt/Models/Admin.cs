using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models
{
    public class Admin : User
    {
        public Admin(string username) : base(username)
        {
            Username = username;
            Role = Enums.Role.Admin;
        }

    }
}
