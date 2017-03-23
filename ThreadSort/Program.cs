using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSort
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int maxThreads, amount;
                int[] array;
                bool logs;
                string time;

                Console.Write("Insert amount of elements: ");
                amount = Convert.ToInt32(Console.ReadLine());
                Console.Write("Insert maximal amount threads: ");
                maxThreads = Convert.ToInt32(Console.ReadLine());
                Console.Write("Insert 'y' to do logs and other key not to do logs: ");
                logs = Console.ReadLine() == "y";

                Console.WriteLine();
                Random rand = new Random();
                array = new int[amount];
                for (int i = 0; i < amount; i++)
                {
                    array[i] = rand.Next(-50, 50);
                    Console.Write(array[i] + " ");
                }
                int[] secondArray = new int[amount];
                array.CopyTo(secondArray, 0);
                Console.WriteLine();

                Console.Write('\n' + "Press any key to start sort...;");
                Console.ReadKey();

                Console.Write("\n\n");
                time = DateTime.Now.ToString("hh:mm:ss:fff");
                Sort.ShellSort(array, maxThreads, logs);
                Console.WriteLine(time);
                Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:fff"));
                Console.Write('\n');

                Console.Write("Sorting with usual Shell sort...\n\n");
                Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:fff"));
                Sort.ShellSortUsual(secondArray);
                Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:fff"));

                Console.WriteLine();
                for (int i = 0; i < amount; i++)
                {
                    Console.Write(array[i] + " ");
                }
                Console.WriteLine();

                Console.Write('\n' + "Press any key to exit...;");
                Console.ReadKey();
            }
        }
    }
}
