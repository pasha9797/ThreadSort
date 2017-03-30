using System;
using System.Collections.Generic;
using System.IO;
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
                int maxThreads=0, amount;
                List<int> list = new List<int>();
                bool logs, print;
                int timeOld, timeNew;

                Console.Write("Insert amount of elements: ");
                amount = Convert.ToInt32(Console.ReadLine());
                Console.Write("Insert 'y' to print array and other key not to print array: ");
                print = Console.ReadLine() == "y";

                Console.WriteLine();
                Random rand = new Random();
                for (int i = 0; i < amount; i++)
                {
                    list.Add(rand.Next(-50, 50));
                    if (print) Console.Write(list[i] + " ");
                }
                List<int> second = new List<int>(list);
                Console.WriteLine();
                Console.WriteLine();

                while (maxThreads < 1 || maxThreads > amount / 2)
                {
                    Console.Write("Insert maximal amount threads: ");
                    maxThreads = Convert.ToInt32(Console.ReadLine());
                }
                Console.Write("Insert 'y' to do logs and other key not to do logs: ");
                logs = Console.ReadLine() == "y";
                Console.WriteLine();

                Console.Write("Press any key to start sort...;");
                Console.ReadKey();

                Console.WriteLine();
                Console.WriteLine();
                timeOld = DateTime.Now.Millisecond + DateTime.Now.Second * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Hour * 60 * 60 * 1000;
                Sort.ShellSort(ref list, maxThreads, logs);
                timeNew = DateTime.Now.Millisecond + DateTime.Now.Second * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Hour * 60 * 60 * 1000;
                Console.WriteLine(((timeNew - timeOld) > 0 ? (timeNew - timeOld).ToString() : "<1") + " mileseconds");
                Console.WriteLine();

                Console.WriteLine("Sorting with usual Shell sort...");
                timeOld = DateTime.Now.Millisecond + DateTime.Now.Second * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Hour * 60 * 60 * 1000;
                Sort.ShellSortUsual(ref second);
                timeNew = DateTime.Now.Millisecond + DateTime.Now.Second * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Hour * 60 * 60 * 1000;
                Console.WriteLine(((timeNew - timeOld) > 0 ? (timeNew - timeOld).ToString() : "<1") + " mileseconds");
                Console.WriteLine();

                for (int i = 0; i < amount; i++)
                {
                    if (print) Console.Write(list[i] + " ");
                }

                list.Clear();

                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Press any key to exit...;");

                if(Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    Console.WriteLine();
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    StreamReader reader = new StreamReader("solo.txt");
                    while(!reader.EndOfStream)
                    {
                        Console.WriteLine(reader.ReadLine());
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    reader.Close();
                }

            }
        }
    }
}
