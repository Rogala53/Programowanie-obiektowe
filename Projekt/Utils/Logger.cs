using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace Projekt.Utils
{
    public class Logger
    {
        private const string _logsPath = "../../../Data/logs.txt";
        public void WriteToLog(string message)
        {
            File.AppendAllText(_logsPath, DateTime.Now.ToString("[yyyy-MM-dd HH-mm-ss]") + " " + message + "\n");
        }
    }
}
