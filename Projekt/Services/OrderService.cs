using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Projekt.Enums;
using Projekt.Interfaces;
using Projekt.Models;
using Projekt.Utils;

namespace Projekt.Services
{
    public class OrderService : IOrderManagement
    {
        public delegate void OnOrderStatusChangeHandler(string message);
        public delegate void OnOrderAddHandler(string message);
        public event OnOrderAddHandler OnOrderAdd;
        public event OnOrderStatusChangeHandler OnOrderStatusChange;
        private ClientService _clientService;

        public Dictionary<OrderStatus, string> Status;
        public List<Order> Orders { get; set; }
        public List<Mechanic> Mechanics { get; set; }
        public List<OrderStatus> AvailableStatuses { get; set; }
        

        public OrderService(ClientService clientService)
        {
            Status = new Dictionary<OrderStatus, string>
            {
                {Enums.OrderStatus.Waiting, "Oczekujący" },
                {Enums.OrderStatus.InProgress, "W trakcie" },
                {Enums.OrderStatus.Finished, "Zakończony" },
            };
            AvailableStatuses = new List<OrderStatus>
            {
                Enums.OrderStatus.Waiting,
                Enums.OrderStatus.InProgress,
                Enums.OrderStatus.Finished,
            };
            Orders = new List<Order>();
            Mechanics = new List<Mechanic>();
            _clientService = clientService;

        }

        public void AddOrder()
        {
            Console.Write("Wprowadź ID zlecenia: ");
            int OrderId;
            while (!int.TryParse(Console.ReadLine(), out OrderId))
            {
                Console.WriteLine("Nieprawidłowe ID. Spróbuj ponownie");
            }

            Console.WriteLine("Wybierz początkowy status zlecenia: ");
            for (int i = 0; i < AvailableStatuses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Status[AvailableStatuses[i]]}");
            }
            int StatusId;
            while (!int.TryParse(Console.ReadLine(), out StatusId) || StatusId < 1 || StatusId > AvailableStatuses.Count)
            {
                Console.WriteLine("Nieprawidłowy wybór statusu. Spróbuj ponownie");
            }
            Console.WriteLine("Podaj numer telefonu klienta:");
            string PhoneNumber = Console.ReadLine();
            while (PhoneNumber.Length != 9)
            {
                Console.WriteLine("Nieprawidłowy format numeru telefonu, spróbuj ponownie");
            }
            Client Client = FindClientByPhoneNumber(PhoneNumber);
            Order Order = new Order(OrderId, AvailableStatuses[StatusId - 1], Client);
            Orders.Add(Order);
            Console.WriteLine($"Zlecenie o numerze {Order.OrderId} zostało dodane");
            OnOrderAdd?.Invoke($"Zlecenie {Order.OrderId} utworzone.");
        }
        private Client FindClientByPhoneNumber(string PhoneNumber)
        {
            foreach (var client in _clientService.GetClients())
            {
                if (client.PhoneNumber == PhoneNumber)
                {
                    return client;
                }
            }
            Console.WriteLine("Klient z takim numerem telefonu nie istnieje");
            return null;
        }
        public void ChangeOrderStatus()
        {
            Console.Write("Wprowadź ID zlecenia: ");
            int OrderId = int.Parse(Console.ReadLine());
            Order Order = FindOrderById(OrderId);
            if (Order == null)
            {
                Console.WriteLine($"Zlecenie o numerze ID {OrderId} nie istnieje, spróbuj ponownie");
                return;
            }
            else
            {
                Console.WriteLine($"Obecny status: {Status[Order.Status]}");
                Console.WriteLine("Wybierz nowy status:");
                for (int j = 0; j < AvailableStatuses.Count; j++)
                {
                    if (AvailableStatuses[j] == Order.Status)
                        continue;

                    Console.WriteLine($" {j + 1}. {Status[AvailableStatuses[j]]}");
                }

                Console.Write("Twój wybór: ");
                int StatusId;
                while (!int.TryParse(Console.ReadLine(), out StatusId) || StatusId < 1 || StatusId > AvailableStatuses.Count || AvailableStatuses[StatusId - 1] == Order.Status)
                {
                    Console.WriteLine("Wybrałeś zły numer statusu, spróbuj ponownie");
                }
                Order.Status = AvailableStatuses[StatusId - 1];
                OnOrderStatusChange?.Invoke($"Zmieniono status zlecenia {Order.OrderId} na \"{Order.Status}\"");
                Console.WriteLine($"Status zlecenia o numerze ID {Order.OrderId} zmieniono na: {Status[Order.Status]}");
            }
        }
        public void ShowOrders()
        {
            if (Orders.Count > 0)
            {
                Console.WriteLine("ID\tStatus\t\t Numer telefonu klienta");
                foreach (var order in Orders)
                {
                    Console.WriteLine($"{order.OrderId}\t{Status[order.Status]}\t{order.Client.PhoneNumber}");
                }
            }
        }
        public void SetOrder()
        {
            Console.Write("Wprowadź ID zlecenia: ");
            int OrderId;
            while (!int.TryParse(Console.ReadLine(), out OrderId) || OrderId < 1)
            {
                Console.WriteLine("Niepoprawny numer ID zlecenia");
            }
            Console.Write("Podaj nazwę mechanika: ");
            string MechanicUsername = Console.ReadLine();
            foreach (var mechanic in Mechanics)
            {
                if (mechanic.Username == MechanicUsername)
                {
                    mechanic.Orders.Add(FindOrderById(OrderId));
                    Console.WriteLine($"Zlecenie numer {OrderId} przypisany mechanikowi {mechanic.Username}");
                    return;
                }

            }
            Console.WriteLine("Nie ma mechanika o tej nazwie");
        }
        private Order FindOrderById(int OrderId)
        {
            foreach (var order in Orders)
            {
                if (order.OrderId == OrderId)
                {
                    return order;
                }
            }
            return null;
        }
        public void ClientHistory(Client Client)
        {
            Console.WriteLine("");
            foreach (var order in Orders)
            {
                if (order.Client == Client)
                {
                    Console.WriteLine($"{order.OrderId}\t{Status[order.Status]}");
                }
            }
        }
        public void AddMechanic()
        {
            Console.WriteLine("Podaj login dla nowego mechanika");
            string MechanicName = Console.ReadLine();
            foreach(var mechanic in Mechanics)
            {
                if(mechanic.Username == MechanicName)
                {
                    Console.WriteLine("Mechanik o takim loginie już istnieje.");
                    return;
                }
            }
            Mechanic Mechanic = new Mechanic(MechanicName);

            Console.WriteLine($"Mechanik o nazwie {Mechanic.Username} został dodany.");
        }
        public void RemoveMechanic()
        {
            Console.WriteLine("Podaj login mechanika, którego chcesz usunąć");
            string MechanicName = Console.ReadLine();
            foreach (var mechanic in Mechanics)
            {
                if (mechanic.Username == MechanicName)
                {
                    Mechanics.Remove(mechanic);
                    return;
                }
            }
            Console.WriteLine("Mechanik o takim loginie nie istnieje");            
        }
        public void ShowMechanics()
        {
            if (Mechanics.Count > 0)
            {
                Console.WriteLine("----MECHANICY----");               
                foreach (var mechanic in Mechanics)
                {
                    Console.WriteLine();
                    Console.WriteLine(mechanic.Username);
                    ShowMechanicOrders(mechanic);
                }
            }
        }
        public void ShowMechanicOrders(Mechanic mechanic)
        {
            if (mechanic.Orders.Count > 0)
            {
                Console.WriteLine("Numer zlecenia mechanika\t\tID zlecenia\t\tStatus zlecenia\tNumer telefonu klienta");
                foreach (var order in mechanic.Orders)
                {
                    Console.WriteLine($"{mechanic.Orders.IndexOf(order) + 1}\t{order.OrderId}\t{order.Status}\t{order.Client.PhoneNumber}");
                }
            }
            else
            {
                Console.WriteLine($"{mechanic.Username} nie ma żadnych zleceń");
            }
        }
    }
}