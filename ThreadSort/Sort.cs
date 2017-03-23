using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSort
{
    public static class Sort
    {

        private static List<Thread> threadList = new List<Thread>();
        private static bool doLogs;
        private static Semaphore sem;

        public static void ShellSort(int[] array, int maxThreads, bool logs)
        {
            doLogs = logs;
            for (int k = array.Length / 2; k > 0; k /= 2)
            {
                if (doLogs) Console.WriteLine("["+ DateTime.Now.ToString("hh:mm:ss:fff")+"] "+"Step {0} begin\n", k);
                sem = new Semaphore(maxThreads, maxThreads);
                threadList.Clear();
                for (int i = 0; i < k; i++)
                {
                    Thread thread = new Thread((id) =>
                    {
                        sem.WaitOne();
                        
                        if (doLogs) Console.WriteLine("[" + DateTime.Now.ToString("hh:mm:ss:fff") + "] "+"[Thread {0}] Start", (int)id);

                        for (int j = (int)id + k; j < array.Length; j += k)
                        {
                            for (int z = j; z >= k && array[z - k] > array[z]; z -= k)
                            {
                                int temp = array[z];
                                array[z] = array[z - k];
                                array[z - k] = temp;
                            }
                        }
                        if (doLogs) Console.WriteLine("[" + DateTime.Now.ToString("hh:mm:ss:fff") + "] "+"[Thread {0}] End", (int)id);
                        sem.Release();
                    });
                    threadList.Add(thread);
                    //thread.Start(i);
                    
                }
                for (int i = 0; i < threadList.Count; i++)
                {
                    threadList[i].Start(i);
                }
                foreach (Thread thread in threadList)
                {
                    thread.Join();
                }
                
                if (doLogs) Console.WriteLine("[" + DateTime.Now.ToString("hh:mm:ss:fff") + "] "+"Step {0} end\n", k);

            }
        }

        public static void ShellSortUsual(int[] array)
        {
            int step = array.Length / 2;
            while (step > 0)
            {
                int i, j;
                for (i = step; i < array.Length; i++)
                {
                    int value = array[i];
                    for (j = i - step; (j >= 0) && (array[j] > value); j -= step)
                    {
                        array[j + step] = array[j];
                    }
                    array[j + step] = value;
                }
                step /= 2;
            }
        }
    }
}
