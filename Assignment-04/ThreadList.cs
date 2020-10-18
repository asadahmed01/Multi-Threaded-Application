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

        public void Add(int number, ThreadStart thread)
        {
            Thread newThread = new Thread(thread);

            newThread.Name = number.ToString();
            threads.Add(newThread);
        }


        public void StartAllThreads()
        {
            foreach (Thread t in threads)
            {
                t.Start();
            }
        }

        public void JoinAll()
        {
            foreach (Thread t in threads)
            {
                t.Join();
            }
        }

    }
}
