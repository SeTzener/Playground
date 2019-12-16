using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace MultiThreading
{
    class Multi
    {
        private readonly Object obj = new Object();
        public void CreateFilesMultiThread()
        {
            Stopwatch watch = new Stopwatch();
            List<Thread> _lstThread = new List<Thread>();

            watch.Start();
            ThreadPool.SetMaxThreads(4, 4);
            int toProcess = 10;
            using (ManualResetEvent resetEvent = new ManualResetEvent(false))
            {
                var list = new List<int>();
                for (int i = 0; i < 10; i++) list.Add(i);
                for (int i = 0; i < 10; i++)
                {
                    ThreadPool.QueueUserWorkItem(x => { CreateRandomText(i); if (Interlocked.Decrement(ref toProcess) == 0)
                            resetEvent.Set();
                    }, list[i]);
                    Thread.Sleep(1000);
                }
                resetEvent.WaitOne();
            }
            //foreach (Thread item in _lstThread)
            //{
            //    item.Join();
            //}
            watch.Stop();
            Utilities.LogElapsedTime(MultiThread.log, "Crea files Multithread", watch.Elapsed);
        }
        public void CreateRandomText(int idx)
        {
           

                Random random = new Random();
                for (int i = 0; i < 5000; i++)
                {
                    string text = "Lorem ipsum dolor. Lorem ipsum dolor. Lorem ipsum dolor. Lorem ipsum dolor. \r\n";
                    int randomNumber = random.Next(0, 100);
                    text = String.Concat(Enumerable.Repeat(text, randomNumber));
                    File.AppendAllText(Path.Combine(MultiThread.InputMulti, $"File da copiare n.{idx.ToString("D2")}{i.ToString("D4")}.txt"), text);
                }
            
            
        }
        public void MoveFilesToFolderMulti()
        {
            Stopwatch watch = new Stopwatch();
            List<Thread> _lstThread = new List<Thread>();

            string[] _aAllFiles = Directory.GetFiles(MultiThread.InputMulti);

            String[][] _aChunks = _aAllFiles
                    .Select((s, i) => new { Value = s, Index = i })
                    .GroupBy(x => x.Index / 500)
                    .Select(grp => grp.Select(x => x.Value).ToArray())
                    .ToArray();

            watch.Start();
            for (int i = 0; i < _aChunks.Count(); i++)
            {
                _lstThread.Add(InitThread(_aChunks[i], i));
            }
            foreach (Thread t in _lstThread)
            {
                t.Join();
            }
            watch.Stop();

            Utilities.LogElapsedTime(MultiThread.log, "Copia Files in Multithread", watch.Elapsed);
        }
        public Thread InitThread(string[] aFile, int i)
        {
            Thread t = new Thread(() => FileCopy(aFile));
            t.Name = $"Thread numero {i}";
            t.Start();
            return t;
        }
        private void FileCopy(string[] arr) 
        {
            foreach (string item in arr)
            {
                File.Copy(item, Path.Combine(MultiThread.OutputMulti, Path.GetFileName(item)), true);
            }
        }

        public void FileWrite()
        {
            string _allText = Properties.Resources.The_Hunger_Games;
            File.WriteAllText(Path.Combine(MultiThread.WriteMulti, "Hunger Games.txt"), _allText);
            string[] book = File.ReadAllLines(Path.Combine(MultiThread.WriteMulti, "Hunger Games.txt"));

            ThreadPool.SetMaxThreads(2, 2);
            for (int i = 0; i < 50; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(x => { readAndWrite(book); }));
            }

            Console.ReadLine();
        }
        public void readAndWrite(string[] book)
        {
            lock (obj)
            {
                for (int i = 0; i < 200; i++)
                {
                    string _sFinalTxt = GenerateRandomLine(book);
                    if (!File.Exists(Path.Combine(MultiThread.WriteMulti, "Hngrgm Ueae.txt")))
                    {
                        File.AppendAllText(Path.Combine(MultiThread.WriteMulti, "Hngrgm Ueae.txt"), _sFinalTxt);
                    }
                    else
                    {
                        while (String.IsNullOrEmpty(_sFinalTxt) )
                        {
                            _sFinalTxt = GenerateRandomLine(book);
                        }
                        File.AppendAllText(Path.Combine(MultiThread.WriteMulti, "Hngrgm Ueae.txt"), _sFinalTxt);
                    }
                }
            }
        }
        private string GenerateRandomLine(string[] paragraph)
        {
            Random rdm = new Random();
            int _iLine = rdm.Next(0, paragraph.Length);
            int _iSubText = rdm.Next(0, paragraph[_iLine].Length);

            return paragraph[_iLine].Substring(0, _iSubText) + Environment.NewLine;
        }
    }
}
