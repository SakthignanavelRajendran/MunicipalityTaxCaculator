using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalityTaxCaculator.Service
{
    public static class Logger
    {
        public static void writeLog(string message)
        {
            string logPath = "C:\\DemoLogs\\nlog.txt";
            using (StreamWriter writer = new StreamWriter(logPath,true) )
            {
                writer.WriteLine($"{DateTime.Now} : {message}");
            }

        }
    }
}
