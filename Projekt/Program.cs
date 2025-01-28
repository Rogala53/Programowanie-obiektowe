using Projekt.Models;
using Projekt.Services;
using Projekt.Utils;

namespace Projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            ClientService clientService = new ClientService();
            OrderService orderService = new OrderService(clientService);           
            Mechanic mechanic = new Mechanic("mechanic1");
            Client client = new Client("luk", "rog", "123456789");
            Admin admin = new Admin("admin1");
            Order order = new Order(1, Enums.OrderStatus.Waiting, client);
            orderService.Mechanics.Add(mechanic);
            orderService.Orders.Add(order);
            clientService.Clients.Add(client);
            logger.ActionMade += orderService.AddOrder;

            orderService.AddOrder();
            Console.ReadKey();
        }
    }
}
