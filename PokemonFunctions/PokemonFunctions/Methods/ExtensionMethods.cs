using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFunctions.Methods
{
    public static class ExtensionMethods
    {
        public static void LogTime(this TraceWriter log, TimeSpan ts)
        {
            if (ts.TotalMilliseconds > 1000)
            {
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts.Hours, ts.Minutes, ts.Seconds,
    ts.Milliseconds / 10);
                log.Info($"{elapsedTime}");
            } else
            {
                log.Info($"{ts.TotalMilliseconds} ms");
            }
        }
    }
}
