using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Projekt.Enums;

namespace Projekt.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public Client Client { get; set; }

        public Order (int orderId, OrderStatus status, Client client)
        {
            OrderId = orderId;
            Status = status;
            Client = client;
        }
    }
}
