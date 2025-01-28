using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models
{
    public class Mechanic : User
    {
        public List<Order> Orders { get; set; }
        public Mechanic(string username) : base(username)
        {
            Username = username;
            Role = Enums.Role.Mechanic;
            Orders = new List<Order>();
        }
    }
}