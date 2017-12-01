using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDojo4.Core
{
    public static class Logger
    {
        public static void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public static void LogError(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
        }
    }
}
