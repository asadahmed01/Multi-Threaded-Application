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
        //program constants
        const int minFileSize = 1000;
        const int maxFileSize = 2000000;
        const int minThreadCount = 1;
        const int maxThreadCount = 1000;

        private static readonly object locker = new object();
        static volatile bool stop = true;
        
        static  string fileName ;
        static long parsedSize;
        static string fileSize;
        static string threadNumber;
        static ThreadList threadList = new ThreadList();


        static void Main(string[] args)
        {
            //instantiate an instance of Utilities class 
            Utilities Utilities = new Utilities();
           
            // take the name of the file from user
            Console.WriteLine("What is the name of the file You want to write to?: ");
            fileName = Console.ReadLine();
            //validate for empty file name field input
            while (fileName == "")
            {
                Console.WriteLine("Please provide valid file name!");
                fileName = Console.ReadLine();
            }
            // take the file size from the user
            Console.WriteLine("What is the maximum size of the file [1000 - 2000000?: ");
            fileSize = Console.ReadLine();
            int size;
            bool isValid = false;
            //validate for the file size 
            while(isValid == false)
            {
                //if input is not number, show error message 
                if (int.TryParse(fileSize, out size))
                {
                    //if the file provided is less than or greater the allowable range, show errror
                    //message
                    if (size < minFileSize || size > maxFileSize)
                    {
                        Console.WriteLine("file size provided is either too small or too big");
                        Console.WriteLine("What is the maximum size of the file [1000 - 2000000?: ");
                        fileSize = Console.ReadLine();
                    }
                    //the input for file size is all good
                    else
                    { isValid = true; parsedSize = size; }
                }
                //if input is not integer, show the proper error message
                else

                { 
                    Console.WriteLine("Oops! file size must an integer");
                    Console.WriteLine("What is the maximum size of the file [1000 - 2000000?: ");
                    fileSize = Console.ReadLine();
                }
                
            }

            //take the number of the threads the user wants to create
            Console.WriteLine("How many Threads are created for the file operation? [1 - 1000]: ");
            threadNumber = Console.ReadLine();

            int count;
            bool isGood = false;
            //validate for the user input for the number of threads
            while (isGood == false)
            {
                //if the input is an integer
                if (int.TryParse(threadNumber, out count))
                {
                    //if the input is outside the allowable range
                    if (count < minThreadCount || count > maxThreadCount)
                    {
                        //show proper error message and retake the user input
                        Console.WriteLine("Number of threads provided is beyond allowable range!");
                        Console.WriteLine("How many Threads are created for the file operation? [1 - 1000]: ");
                        threadNumber = Console.ReadLine();
                    }
                    else { isGood = true; }
                    
                }
                //if the user input is not an integer
                else

                {
                    //show error message and retake the user input
                    Console.WriteLine("Number of threads must an integer [1 - 1000]!");
                    Console.WriteLine("How many Threads are created for the file operation? [1 - 1000]: ");
                    threadNumber = Console.ReadLine();
                }

            }

            //convert the user input into int
            int threadCount = Int32.Parse(threadNumber);

            //create the thread that will monitor the file size as other threads write to it
            Thread watchDogThread = new Thread(MonitorClose);
            

            //create the number of threads requested by the user
            for (int i = 0; i < threadCount; i++)
            {
                
                threadList.Add(1, new ThreadStart(write));
            }
            //start all the threads created 
            threadList.StartAllThreads();
            //start the monitoring thread
            watchDogThread.Start();
            //join all the threads
            threadList.JoinAll();
            //negate the bool variable to stop the threads
            stop = false;
            //shut down the monitoring thread after all other threads are  shutdown
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
                        //create and write to the file
                        using (StreamWriter file =
                        new StreamWriter(fileName + ".txt", true))
                        {
                            // pass the stream write the method that generates random string
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

        /*
         * Name: MonitorClose()
         * Description: this method is delegated by the a thread to monitor the file size
         * if the file size is greater than maximum file size chosen by the user
         * the method shuts down all the threads that are writing  to the text file
         * Parameter: nothing
         * Return Value: nothing
         */
        static void MonitorClose()
        {
            

            try
            {
                while (stop)
                {
                    // access the file being written to by the threads
                    FileInfo fi = new FileInfo(fileName + ".txt");
                    Console.WriteLine("File size: {0}", fi.Length);
                    //shut down all the threads when the requested file size is reached
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
