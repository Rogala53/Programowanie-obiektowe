namespace _2_klasy
{
    internal class Program
    {
        public enum Color
        {
            Czerwony,
            Niebieski,
            Zielony
        }
        public enum Type
        {
            Samochód,
            Motor,
            Cieżarówka,
            SUV

        }
        class Vehicle
        {
            public string Name { get; set; }
            public Color Color { get; set; }
            public double Speed { get; set; }
            public double Fuel { get; set; }
            public List<Type> Type { get; set; }
            public string Extra { get; set; }

            public void ShowData()
            {
                Console.WriteLine($"Nazwa: {Name} , kolor: {Color}, prędkość: {Speed}, rodzaj paliwa: {Fuel}, Typ: {string.Join(", ", Type)}, \n dodatkowe informacje: {Extra}");
            }
        }

        class Garage
        {
            public List<Vehicle> Vehicles { get; set; }
            public byte Capacity { get; set; }
            public void AddVehicles(Vehicle vehicle)
            {
                if (Vehicles.Count < Capacity)
                {
                    Vehicles.Add(vehicle);
                    Console.WriteLine($"Dodano {vehicle.Name} do garażu");
                }
                else
                {
                    Console.WriteLine("Brak miejsc w garażu");
                }
            }
            public void RemoveVehicles(Vehicle vehicle)
            {
                if (Vehicles.Contains(vehicle))
                {
                    Vehicles.Remove(vehicle);
                    Console.WriteLine($"Usunięto {vehicle.Name} z garażu");
                }
                else Console.WriteLine("Brak pojazdu w garażu");
            }

            public void ShowVehicles()
            {
                Dictionary<Type, int> vehicleTypes = new Dictionary<Type, int>();
                foreach (Vehicle vehicle in Vehicles)
                {
                    foreach (Type type in vehicle.Type)
                    {
                        if (vehicleTypes.ContainsKey(type))
                        {
                            vehicleTypes[type]++;
                        }
                        else
                        {
                            vehicleTypes[type] = 1;
                        }
                    }
                }
                foreach (KeyValuePair<Type, int> pair in vehicleTypes)
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }

            public void ShowAllVehicles()
            {
                foreach (var vehicle in Vehicles)
                {
                    vehicle.ShowData();
                }
            }
        }
        static void Main(string[] args)
        {
            Vehicle bike1 = new Vehicle();
            bike1.Name = "Yamaha";
            bike1.Type = new List<Type> { Type.Motor };

            Console.WriteLine(bike1.Name);

            Vehicle car2 = new Vehicle() { Name = "Fiat", Color = Color.Niebieski, Speed = 115, Fuel = 50, Type = new List<Type>() { Type.Samochód, Type.SUV }, Extra = "126p" };

            Console.WriteLine(car2.Name);
            Console.Clear();

            // car2.ShowData();


            Vehicle car3 = new Vehicle() { Name = "Fiat", Color = Color.Niebieski, Speed = 115, Fuel = 50, Type = new List<Type>() { Type.Samochód }, Extra = "126p" };
            Garage garage = new Garage();
            garage.Capacity = 10;

            garage.Vehicles = new List<Vehicle>();

            garage.AddVehicles(bike1);
            garage.AddVehicles(car2);
            garage.AddVehicles(car3);

            Console.WriteLine("Pojazdy");
            garage.ShowAllVehicles();


            Console.WriteLine("\n");

            garage.ShowVehicles();

            garage.RemoveVehicles(car2);

            garage.ShowVehicles();

            garage.ShowAllVehicles();
            Console.ReadKey();
        }
    }

}
