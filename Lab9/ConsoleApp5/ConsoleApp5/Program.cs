using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> counries = new List<string>(16);
            string path = @"C:\Users\intre\source\repos\ConsoleApp5\country.txt";
            using(StreamReader sr = new StreamReader(path))
            {
                while(sr.Peek() > -1)
                {
                    counries.Add(sr.ReadLine());
                }
            }
            BinaryTree binaryTree = new BinaryTree(counries);
            Console.ReadLine();
        }
    }
}
