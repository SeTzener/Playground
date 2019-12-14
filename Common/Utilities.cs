using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Utilities
    {
        /// <summary>
        /// Crea un file che permette di monitorare il tempo trascorso dopo uno stopwatch
        /// </summary>
        /// <param name="logPath">Percorso di sistema nel quale il log deve essere creato</param>
        /// <param name="msg">Messaggio per capire </param>
        /// <param name="ts">Ore minuti e secondi dello stopwatch</param>
        public static void LogElapsedTime(string logPath, string msg, TimeSpan ts)
        {
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            string text = $"METODO: {msg}{Environment.NewLine}TEMPO TRASCORSO: {elapsedTime}{Environment.NewLine}";
            File.AppendAllText(Path.Combine(logPath, $"{DateTime.Now.ToString("yyyyMMdd")} Elapsed Time.log"), text);
        }
    }
}
