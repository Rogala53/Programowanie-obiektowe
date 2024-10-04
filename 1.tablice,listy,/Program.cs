
using System.ComponentModel.DataAnnotations;

namespace _1.tablice_listy_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = ReadInt("Podaj liczbę n:", 0, int.MaxValue);
            string[] names = new string[n];
            int[] ages = new int[n];

            for (int i = 0; i < n; i++)
            {
                names[i] = ReadString($"Podaj imię osoby {i + 1}:");
                ages[i] = ReadInt($"Podaj wiek osoby {i + 1}:", 0, 150);
            }

            List<string> namesStartswithA = names.Where(name => name.StartsWith("A", StringComparison.OrdinalIgnoreCase)).ToList();

        }

        private static string ReadString(string prompt)
        {
            string result;
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();
                if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
                {
                    Console.WriteLine("Podaj niepusty ciąg znakow");
                }
            } while (string.IsNullOrEmpty(result));
            return result;
        }

        private static int ReadInt(string prompt, int low, int high)
        {
            int result;
            bool valid;
            do
            {
                Console.Write(prompt);
                valid = int.TryParse(Console.ReadLine(), out result) && result >= low && result <= high;
                if (!valid)
                {
                    Console.WriteLine($"Podaj liczbę z zakresu {low} - {high}");
                }
            } while (!valid);
            return result;
        }
    }
}




