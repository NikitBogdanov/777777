using System;
using System.IO;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            CircularLinkedList<string> CircularLinkedList = new CircularLinkedList<string>();
            string path = @"C:\Users\intre\source\repos\ConsoleApp3\names.dat";
            File.WriteAllText(path, string.Empty);
            using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                bw.Write("Виктор"); //0
                bw.Write("Олег");   //1
                bw.Write("Роберт"); //2
                bw.Write("Евгений");//3
                bw.Write("Аркадий");//4
            }
            using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while(br.PeekChar() > -1)
                {
                    string[] names = br.ReadString().Split();
                    CircularLinkedList.Add(names[0]);
                }
            }
            Console.WriteLine("Введите считалочку:");
            string counting = Console.ReadLine();
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();
            string[] ArrayCounting = counting.Split(" ");
            bool find = false;
            int index = -1;
            foreach(string str in CircularLinkedList)
            {
                if (str.Equals(name))
                {
                    break;
                }
                index++;
            }
            Console.WriteLine(CircularLinkedList.Info(ArrayCounting.Length - index - 1));
        }
    }
}
