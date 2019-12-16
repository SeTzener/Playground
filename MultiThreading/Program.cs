using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace MultiThreading
{
    class Program
    {
        static void Main(string[] args)
        {
            PathEntities.CreateDirectories(PathEntity.MultiThreading);
            Program p = new Program();
            p.DoWork();
        }

        public void DoWork()
        {
            Standard s = new Standard();
            Multi m = new Multi();

            //s.CreateFiles();
            //m.CreateFilesMultiThread();
            //s.MoveFiles();
            //m.MoveFilesToFolderMulti();
            m.FileWrite();
        }
    }
}
