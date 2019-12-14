using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace MultiThreading
{
    class Standard
    {
        public void CreateFiles()
        {
            Random random = new Random();
            Stopwatch watch = new Stopwatch();

            watch.Start();
            for (int i = 0; i < 50000; i++)
            {
                string text = "Lorem ipsum dolor. Lorem ipsum dolor. Lorem ipsum dolor. Lorem ipsum dolor. \r\n";
                int randomNumber = random.Next(0, 100);
                text = String.Concat(Enumerable.Repeat(text, randomNumber));
                File.AppendAllText(Path.Combine(MultiThread.Input, $"File da copiare n.{i.ToString("D5")}.txt"), text);
            }
            watch.Stop();
            Utilities.LogElapsedTime(MultiThread.log, "Creazione files Standard", watch.Elapsed);
        }

        public void MoveFiles()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (string item in Directory.GetFiles(MultiThread.Input))
            {
                File.Copy(item, Path.Combine(MultiThread.Output, Path.GetFileName(item)));
            }
            watch.Stop();
            Utilities.LogElapsedTime(MultiThread.log, "Copia files Standard", watch.Elapsed);
        }
    }
}
