using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        public static bool EqualityOfDataLists(DataList<string> dl1, DataList<string> dl2)
        {
            if(dl1.Count != dl2.Count)
            { 
                return false;
            }
            else
            {
                foreach(string i in dl1)
                {
                    if(dl2.Contains(i) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool EqualityOfLists(List<string> dl1, List<string> dl2)
        {
            if (dl1.Count != dl2.Count)
            {
                return false;
            }
            else
            {
                foreach (string i in dl1)
                {
                    if (dl2.Contains(i) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            DataList<string> dl1 = new DataList<string>();
            DataList<string> dl2 = new DataList<string>();
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                dl1.Add($"{i}");
                dl2.Add($"{i}");
                list1.Add($"{i}");
                list2.Add($"{i}");
            }
            dl2.Remove("6");
            dl2.Add("6");
            Console.WriteLine(EqualityOfLists(list1, list2));
            Console.ReadLine();
        }
    }
}
