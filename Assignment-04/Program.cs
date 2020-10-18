using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment_04
{
    class Program
    {
        //private readonly object balanceLock = new object();
        private static readonly object locker = new object();
        static void Main(string[] args)
        {
            //static volatile bool stop = true;

            List<Thread> threads = new List<Thread>();
            Utilities Utilities = new Utilities();
            ThreadList threadList = new ThreadList();

            Console.WriteLine("What is the name of the file Youk want to write to?: ");
            string fileName = Console.ReadLine();

            Console.WriteLine("What is the maximum size of the file?: ");
            string fileSize = Console.ReadLine();

            Console.WriteLine("How many Threads are created for the file operation?: ");
            string threadNumber = Console.ReadLine();

            Console.WriteLine("File Name: {0}\n Size: {1}\n number; {2}",fileName, fileSize, threadNumber);

            // create the requested number of threads
            int threadCount = Int32.Parse(threadNumber);
            ddfdfdsfdsfdsfdsfdsfa
            
            for (int i = 0; i < threadCount; i++)
            {
                Thread thread = new Thread(write);

                threads.Add(thread);
                thread.Start(fileName);
                thread.Join();
                
            }
            
            //new ThreadStart(() => write(fileName)));
            FileInfo fi = new FileInfo(fileName + ".txt");
             Console.WriteLine("file size is : {0}", fi.Length);
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }



        ///// write to file method ////////
        
        static void write(object fileName)
        {
            
            FileInput.WriteToFile((string)fileName);
            //Thread.Sleep(200);

            Console.WriteLine("{0} Thread Done", Thread.CurrentThread.Name);
            Thread.Sleep(200);
        }
    }
}
