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

        private static List<Thread> threadList = new List<Thread>(); //список потоков
        private static List<List<int>> subListList = new List<List<int>>(); //список подлистов
        private static bool Logs;

        struct ThreadContext //информация передаваемая в поток
        {
            public int Index; //индекс потока
            public List<int> SubList; //подлист для сортировки

            public ThreadContext(int i, List<int> s)
            {
                Index = i;
                SubList = s;
            }
        }

        public static void ShellSort(ref List<int> MainList, int maxThreads, bool logs) //многопоточная сортировка шелла
        {
            Logs = logs;
            int N = MainList.Count;
            int subListCount = MainList.Count / maxThreads;

            for (int subI = 0; subI < maxThreads; subI++) //создание потоков
            {
                Thread thread = new Thread(o =>
                  {
                      ThreadContext tc = (ThreadContext)o;

                      //Вывод начала потока и информации о нем
                      if (Logs)
                          Console.WriteLine("[" + DateTime.Now.ToString("hh:mm:ss:fff") + "] " + "Thread {0} started sorting elements from {1} to {2}", tc.Index, tc.Index * subListCount, (tc.Index < (maxThreads - 1) ? (tc.Index + 1) * subListCount : N)-1);

                      //Сортировка подлиста
                      int step = tc.SubList.Count / 2;
                      while (step > 0)
                      {
                          int i, j;
                          for (i = step; i < tc.SubList.Count; i++)
                          {
                              int value = tc.SubList[i];
                              for (j = i - step; (j >= 0) && (tc.SubList[j] > value); j -= step)
                              {
                                  tc.SubList[j + step] = tc.SubList[j];
                              }
                              tc.SubList[j + step] = value;
                          }
                          step /= 2;
                      }

                      //Вывод окончания потока
                      if (Logs)
                          Console.WriteLine("[" + DateTime.Now.ToString("hh:mm:ss:fff") + "] " + "Thread {0} terminated", tc.Index);

                  });

                //создание подлиста
                List<int> subList = new List<int>();
                for (int i = subI * subListCount; subI < (maxThreads - 1) ? i < (subI + 1) * subListCount : i < MainList.Count; i++)
                {
                    subList.Add(MainList[i]);
                }
                subListList.Add(subList);
                threadList.Add(thread);
                thread.Start(new ThreadContext(subI, subList)); //запуск потока и передача ему подлиста
            }

            foreach(Thread thread in threadList) //ожидание завершения всех потоков
            {
                thread.Join();
            }

            if (Logs)
                Console.WriteLine("[" + DateTime.Now.ToString("hh:mm:ss:fff") + "] " + "Merging sublists");

            //Слияние всех подмассивов в один
            MainList = subListList[0];
            for(int i=1;i<subListList.Count;i++)
            {
                MainList = Merge(MainList, subListList[i]);
            }
            threadList.Clear();
            subListList.Clear();
        }

        private static List<int> Merge(List<int> left, List<int> right)
        {
            int[] buff = new int[left.Count + right.Count];
            int i = 0;  //соединенный массив
            int l = 0;  //левый массив
            int r = 0;  //правый массив
            for (; i < buff.Length; i++)
            {
                if (r >= right.Count)
                {
                    buff[i] = left[l];
                    l++;
                }
                else if (l < left.Count && left[l] < right[r])
                {
                    buff[i] = left[l];
                    l++;
                }
                else
                {
                    buff[i] = right[r];
                    r++;
                }
            }
            return new List<int>(buff);
        }

        public static void ShellSortUsual(ref List<int> list) //Стандартная сортировка шелла
        {
            int step = list.Count / 2;
            while (step > 0)
            {
                int i, j;
                for (i = step; i < list.Count; i++)
                {
                    int value = list[i];
                    for (j = i - step; (j >= 0) && (list[j] > value); j -= step)
                    {
                        list[j + step] = list[j];
                    }
                    list[j + step] = value;
                }
                step /= 2;
            }
        }
    }
}
