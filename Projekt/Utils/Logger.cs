using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Projekt.Utils
{
    public class Logger
    {
        private const string _logsPath = "../../../Data/logs.txt";
        public delegate void LoggerHandler();
        public event LoggerHandler? ActionMade;
        
        public void WriteToLog()
        {
            Console.WriteLine("blablabla");
            File.AppendAllText(_logsPath, $" adhfdhrhr\n");
            ActionMade?.Invoke();
        }
    }
}
