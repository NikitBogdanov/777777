using System;
using System.Collections.Generic;

namespace ConsoleApp_4
{
    class Program
    {
        public static int FindRoots(int number)
        {
            int count = 0;
            for (int i = 1; i < number; i++)
            {
                for(int j = 1; j < number; j++)
                {
                    for(int k = 1; k < number; k++)
                    {
                        double ansver = Math.Pow(i, 3) + Math.Pow(j, 3) + Math.Pow(k, 3);
                        if(ansver == number)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
        static void Main(string[] args)
        {
            Dictionary<int, int> numbers = new Dictionary<int, int>();
            for(int i = 0; i < 100; i++)
            {
                int count = FindRoots(i);
                numbers.Add(i, count);
            }
            foreach(var item in numbers)
            {
                if(item.Value >= 3)
                {
                    Console.WriteLine(item.Key);
                }
            }
        }
    }
}
