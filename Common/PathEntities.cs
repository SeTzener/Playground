using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum PathEntity
    {
        MultiThreading = 0,
    }
    public class PathEntities
    {
        public static void CreateDirectories( PathEntity entity)
        {
            switch (entity)
            {
                case PathEntity.MultiThreading:
                    MultiThread multy = new MultiThread();
                    foreach (var item in multy.AllPaths)
                    {
                        if (!Directory.Exists(item))
                            Directory.CreateDirectory(item);
                    }
                    break;
            }
        }
    }

    public class MultiThread
    {
        public static string Input { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Playground", "MultiThread", "Standard", "Input"); } }
        public static string InputMulti { get {return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Playground", "MultiThread", "MultiThread", "Input"); } }
        public static string WriteMulti { get {return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Playground", "MultiThread", "MultiThread", "Write test"); } }
        public static string Output { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Playground", "MultiThread", "Standard", "Output"); } }
        public static string OutputMulti { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Playground", "MultiThread", "MultiThread", "Output"); } }
        public static string log { get { Logs l = new Logs(); return l.BasePath; } }
        public string[] AllPaths
        { 
            get
            {
                return new string[] { Input, InputMulti, Output, OutputMulti, log, WriteMulti };
            }
        }
    }

    public class Logs
    {
        public string BasePath { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Playground", "Logs"); } }
    }
}

        //static string _sLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Playground", "MultiThread");