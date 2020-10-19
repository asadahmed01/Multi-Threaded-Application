using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment_04
{
    class ThreadList
    {
        List<Thread> threads = new List<Thread>();
        public ThreadList()
        { }

        /*
         * Name: Add()
         * Description: this method add the created threads into the thread List
         * Parameter: int number for thread ID and ThreadStart thread for the thread being created
         * Return Value: void. Nothing
         */
        public void Add(int number, ThreadStart thread)
        {
            Thread newThread = new Thread(thread);

            newThread.Name = number.ToString();
            threads.Add(newThread);
        }

        /*
         * Name: startAllThreads()
         * Description: this method starts all the threads in the thread List
         * Parameter: nothing
         * Return Value: void. Nothing
         */
        public void StartAllThreads()
        {
            foreach (Thread t in threads) // loop through the thread List and start all of them
            {
                t.Start();
            }
        }

        /*
         * Name: JoinAll()
         * Description: this method joins all the threads in the thread List
         * Parameter: nothing
         * Return Value: void. Nothing
         */
        public void JoinAll()
        {
            foreach (Thread t in threads) // loop through the started threads and joing them all
            {
                t.Join();
            }
        }

        /*
         * Name: KillAll()
         * Description: this method shuts down all the threads in the thread List
         * Parameter: nothing
         * Return Value: void. Nothing
         */
        public void KillAll()
        {
            foreach (Thread t in threads) // loop throught the running threads and shut them down
            {
                t.Abort();
            }
        }

    }
}
