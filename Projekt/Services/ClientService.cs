using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt.Interfaces;
using Projekt.Models;
namespace Projekt.Services
{
    public class ClientService : IClientManagement
    {
        public List<Client> Clients { get; set; }      

        public ClientService() 
        {
            Clients = new List<Client>();
        }

        public List<Client> GetClients()
        {
            return Clients;
        }
        public void AddClient()
        {
            Console.WriteLine("Podaj imię klienta");
            string FirstName = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko klienta");
            string LastName = Console.ReadLine();
            Console.WriteLine("Podaj numer telefonu klienta");
            string PhoneNumber = Console.ReadLine();
            foreach (var client in Clients)
            {
                if(client.PhoneNumber == PhoneNumber)
                {
                    Console.WriteLine("Klient z takim numerem telefonu już istnieje");
                    return;
                }
            }
            Client Client = new Client(FirstName, LastName, PhoneNumber);
            Clients.Add(Client);
            Console.WriteLine($"Klient {Client.FirstName} {Client.LastName} został dodany");
        }

        public void RemoveClient()
        {
            ShowClients();
            Console.Write("Podaj numer ID klienta, którego chcesz usunąć: ");           
            try
            {                
                int ClientId = int.Parse(Console.ReadLine()) - 1;
                Clients.Remove(Clients[ClientId]);
                Console.WriteLine($"{Clients[ClientId].FirstName} {Clients[ClientId].LastName} został usunięty");
            }
            catch(Exception ex)  
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void ShowClients()
        {
          
            Console.WriteLine("\t----KLIENCI----");
            Console.WriteLine("ID\tImie\tNazwisko\tNumer telefonu");
            Console.WriteLine();
            foreach (var client in Clients)
            {
                Console.WriteLine($"{Clients.IndexOf(client) + 1}\t{client.FirstName}\t{client.LastName}\t\t{client.PhoneNumber}");
            }
        }
    }
}
