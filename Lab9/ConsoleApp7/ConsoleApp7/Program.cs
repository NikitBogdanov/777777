using System;

namespace ConsoleApp7
{
    class Program
    {
        public static void NewArrayList(ArrayList<int> List)
        {
            Random rand = new Random();
            for(int i = 0; i < List.MaxCount; i++)
            {
                List.Add(rand.Next(0, 2));
            }
        }
        public static void Trans(ArrayList<int> List)
        {
            for(int i = 1; i <= List.IndexColumn; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    int temp = List.Output(i, j);
                    List.Update(i, j, List.Output(j, i));
                    List.Update(j, i, temp);
                }
            }
            foreach (int i in List)
            {
                Console.Write(i + " ");
            }
        }
        public static void Sum(ArrayList<int> List1, ArrayList<int> List2)
        {
            ArrayList<int> List3 = new ArrayList<int>(List1.IndexColumn, List1.IndexLine);
            for(int i = 1; i <= List1.IndexColumn; i++)
            {
                for(int j = 1; j <= List2.IndexLine; j++)
                {
                    List3.Add(List1.Output(i, j) + List2.Output(i, j));
                }
            }
            foreach(int i in List3)
            {
                Console.Write(i + " ");
            }
        }
        public static void Сomposition(ArrayList<int> List1, ArrayList<int> List2)
        {
            ArrayList<int> List3 = new ArrayList<int>(List1.IndexColumn, List1.IndexLine);
            int temp = 0;
            for (int k = 1; k <= List1.IndexColumn; k++)
            {
                for (int i = 1; i <= List1.IndexColumn; i++)
                {
                    for (int j = 1; j <= List2.IndexLine; j++)
                    {
                        temp += List1.Output(k, j) * List2.Output(j, i);
                    }
                    List3.Add(temp);
                    temp = 0;
                }
            }
            foreach (int i in List3)
            {
                Console.Write(i + " ");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер массива: ");
            int column = Convert.ToInt32(Console.ReadLine());
            int line = Convert.ToInt32(Console.ReadLine());
            ArrayList<int> List1 = new ArrayList<int>(column, line);
            ArrayList<int> List2 = new ArrayList<int>(column, line);
            NewArrayList(List1);
            NewArrayList(List2);
            foreach(int i in List1)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            foreach (int i in List2)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Сomposition(List1, List2);
        }
    }
}
