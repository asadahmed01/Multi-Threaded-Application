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
        static volatile bool stop = false;
        static void Main(string[] args)
        {
            

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
            //new ThreadStart(() => write(fileName)));
            FileInfo fi = new FileInfo(fileName + ".txt");
            //Console.WriteLine("file size is : {0}", fi.Length);

            
            //watchdog thread
            Thread watchDogThread = new Thread(closeFile);
            watchDogThread.Start(fi.Length);
            

            for (int i = 0; i < threadCount; i++)
            {
                //Thread thread = new Thread(write);

                threads.Add(new Thread(write));
                
            }
            foreach(Thread t in threads)
            {
                t.Start(fileName);
                t.Join();
                Thread.Sleep(1000);

            }
            watchDogThread.Join();
            Thread.Sleep(1000);

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }



        ///// write to file method ////////
        
        static void write(object fileName)
        {
            
            FileInput.WriteToFile((string)fileName);
            //Thread.Sleep(200);

            Console.WriteLine("{0} Thread Done", Thread.CurrentThread.Name);
            //Thread.Sleep(200);
        }


        static void closeFile(object file)
        {
            while(!stop)
            {
                Console.WriteLine("File size: {0}", file);
                Thread.Sleep(1000);
            }
            
        }
    }
}
