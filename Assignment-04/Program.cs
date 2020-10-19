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
        static volatile bool stop = true;
        const int minFileSize = 1000;
        const int maxFileSize = 2000000;
        const int minThreadCount = 1;
        const int maxThreadCount = 1000;
        static  string fileName ;
        static long parsedSize;
        static string fileSize;
        static string threadNumber;
        static ThreadList threadList = new ThreadList();


        static void Main(string[] args)
        {
            
            Utilities Utilities = new Utilities();
           

            Console.WriteLine("What is the name of the file You want to write to?: ");
            fileName = Console.ReadLine();
            while (fileName == "")
            {
                Console.WriteLine("Please provide valid file name!");
                fileName = Console.ReadLine();
            }

            Console.WriteLine("What is the maximum size of the file [1000 - 2000000?: ");
            fileSize = Console.ReadLine();
            int size;
            bool isValid = false;

            while(isValid == false)
            {
                if (int.TryParse(fileSize, out size))
                {
                    if (size < minFileSize || size > maxFileSize)
                    {
                        Console.WriteLine("file size provided is either too small or too big");
                        Console.WriteLine("What is the maximum size of the file [1000 - 2000000?: ");
                        fileSize = Console.ReadLine();
                    }
                    else
                    { isValid = true; parsedSize = size; }
                }
                else

                { 
                    Console.WriteLine("Oops! file size must an integer");
                    Console.WriteLine("What is the maximum size of the file [1000 - 2000000?: ");
                    fileSize = Console.ReadLine();
                }
                
            }
            Console.WriteLine("How many Threads are created for the file operation? [1 - 1000]: ");
            threadNumber = Console.ReadLine();

            int count;
            bool isGood = false;
            while (isGood == false)
            {
                if (int.TryParse(threadNumber, out count))
                {
                    if (count < minThreadCount || count > maxThreadCount)
                    {
                        Console.WriteLine("Number of threads provided is beyond allowable range!");
                        Console.WriteLine("How many Threads are created for the file operation? [1 - 1000]: ");
                        threadNumber = Console.ReadLine();
                    }
                    else { isGood = true; }
                    
                }
                else

                {
                    Console.WriteLine("Number of threads must an integer [1 - 1000]!");
                    Console.WriteLine("How many Threads are created for the file operation? [1 - 1000]: ");
                    threadNumber = Console.ReadLine();
                }

            }


            int threadCount = Int32.Parse(threadNumber);

            //watchdog thread
            Thread watchDogThread = new Thread(MonitorClose);
            


            for (int i = 0; i < threadCount; i++)
            {
                
                threadList.Add(1, new ThreadStart(write));
            }

            
       
            threadList.StartAllThreads();
            watchDogThread.Start();

            threadList.JoinAll();
            //watchDogThread.Join();

            stop = false;
            watchDogThread.Abort();

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }



        /*
         * Name: write()
         * Description: this method creates and writes into the text file
         * Parameter: nothing
         * Return Value: nothing
         */

        static void write()
        {
            
            try
            {
                while (stop) 
                {
                    lock (locker)
                    {
                        using (StreamWriter file =
                        new StreamWriter(fileName + ".txt", true))
                        {
                            file.WriteLine(Utilities.GenerateString(200));
                            Thread.Sleep(1000);
                        }
                    }
                  
                } 
                
            }
            catch (ThreadAbortException ex)
            {
                
                Thread.ResetAbort();
            }

        }


        static void MonitorClose()
        {
            

                try
                {
                    while (stop)
                    {
                    
                        FileInfo fi = new FileInfo(fileName + ".txt");
                        Console.WriteLine("File size: {0}", fi.Length);
                        if (fi.Length > parsedSize)
                        {
                        Console.WriteLine("Done!!!");
                            threadList.KillAll();
                            
                        }
                        Thread.Sleep(1000);
                    
                    } 
                }
                catch (ThreadAbortException ex)
                {
                    
                    Thread.ResetAbort();
                }

            
        }
    }
}
