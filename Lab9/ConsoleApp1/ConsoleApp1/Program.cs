using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    enum Functions
    {
        See = 1,
        Add = 2,
        Del = 3,
        Update = 4,
        Find = 5,
        Log = 6,
        Sort = 7,
        Exit = 8
    }
    enum System
    {
        Ф = 1,
        УГ = 2
    }
    struct Geograf
    {
        public string state;
        public string capital;
        public string people;
        public System system;
        public void Print(string path, DoublyLinkedList<Geograf> list)
        {
            Console.WriteLine();
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("|География                                                   |");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine("|Государство       |Столица      |Население      |Строй      |");
            Console.WriteLine("|__________________|_____________|_______________|___________|");
            foreach (Geograf geo in list)
            {
                Console.WriteLine("|____________________________________________________________|");
                Console.WriteLine($"|{geo.state,-18}|{geo.capital,-13}|{geo.people,-15}|{geo.system,-11}|");
                Console.WriteLine("|__________________|_____________|_______________|___________|");
            }
            Console.WriteLine("|Перечисляемый тип: Ф - федерация, УГ - унитарное государство|");
            Console.WriteLine("|____________________________________________________________|");
            Console.WriteLine();
        }
    }
    struct Log
    {
        public string choice_Functions;
        public string time;
        public static void SeeLog(DoublyLinkedList<Log> time)
        {
            if (time.Count > 1)
            {
                foreach (Log g in time)
                {
                    Console.WriteLine($"{Convert.ToDateTime(g.time).TimeOfDay}{g.choice_Functions}");
                }
                TimeSpan ansver = Convert.ToDateTime(time.Get(1).time).TimeOfDay - Convert.ToDateTime(time.Get(0).time).TimeOfDay;
                for (int i = 0, j = 1; i < time.Count - 1; i++)
                {
                    if (j < time.Count)
                    {
                        TimeSpan dif = Convert.ToDateTime(time.Get(j).time).TimeOfDay - Convert.ToDateTime(time.Get(i).time).TimeOfDay;
                        if (dif > ansver)
                        {
                            ansver = dif;
                        }
                        j++;
                    }
                }
                Console.WriteLine();
                Console.WriteLine(ansver + " - Самый долгий период бездействия пользователя");
            }
            else if (time.Count == 1)
            {
                for (int i = 0; i < time.Count; i++)
                {
                    Console.WriteLine($"{Convert.ToDateTime(time.Get(i).time).TimeOfDay}{time.Get(i).choice_Functions}");
                }
            }
            else
            {
                Console.WriteLine("Лог пуст...");
            }
        }
    }
    class Program
    {
        static void WriteToFile(string path, DoublyLinkedList<Geograf> list)
        {
            File.WriteAllText(path, string.Empty);
            using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                foreach (Geograf g in list)
                {
                    bw.Write($"{g.state} {g.capital} {g.people} {g.system}");
                }
            }
        }
        static void ReadFromFile(string path, DoublyLinkedList<Geograf> list)
        {
            Geograf geo;
            if (File.Exists(path))
            {
                list.Clear();
                using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    while (br.PeekChar() > -1)
                    {
                        string[] geografi = br.ReadString().Split();
                        geo.state = geografi[0];
                        geo.capital = geografi[1];
                        geo.people = geografi[2];
                        if (geografi[3].Equals("Ф"))
                        {
                            geo.system = System.Ф;
                        }
                        else
                        {
                            geo.system = System.УГ;
                        }
                        list.Add(geo);
                    }
                }
            }
            else
            {
                File.Create(path).Close();
            }
        }
        static void Main(string[] args)
        {
            DoublyLinkedList<Geograf> list = new DoublyLinkedList<Geograf>();
            DoublyLinkedList<Log> time_List = new DoublyLinkedList<Log>();
            DoublyLinkedList<Geograf> newList = new DoublyLinkedList<Geograf>();
            string path = @"C:\Users\intre\source\repos\ConsoleApp1\lab.dat";
            bool exit = true;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Выберите пункт меню: ");
                Console.WriteLine($"1 – Просмотр таблицы" +
                                  $"\n2 – Добавить запись" +
                                  $"\n3 – Удалить запись" +
                                  $"\n4 – Обновить запись" +
                                  $"\n5 – Поиск записей" +
                                  $"\n6 – Просмотреть лог" +
                                  $"\n7 - Выход");
                Console.WriteLine();
                try
                {
                    Functions choice = (Functions)int.Parse(Console.ReadLine().Trim());
                    Geograf geo;
                    geo.state = null;
                    geo.capital = null;
                    geo.people = null;
                    geo.system = System.Ф;
                    Log log;
                    log.choice_Functions = null;
                    log.time = null;
                    if (choice is Functions.See)
                    {
                        ReadFromFile(path, list);
                        geo.Print(path, list);
                    }
                    else if (choice is Functions.Add)
                    {
                        Console.WriteLine("Государство:");
                        geo.state = Console.ReadLine().Trim();
                        Console.WriteLine("Столица:");
                        geo.capital = Console.ReadLine().Trim();
                        Console.WriteLine("Население:");
                        geo.people = Console.ReadLine().Trim();
                        Console.WriteLine("Госудврственный строй (Ф - федерация, УГ - унитарное государство):");
                        bool error = true;
                        do
                        {
                            string sys = Console.ReadLine().Trim();
                            if (sys == "Ф")
                            {
                                geo.system = System.Ф;
                                error = false;
                            }
                            else if (sys == "УГ")
                            {
                                geo.system = System.УГ;
                                error = false;
                            }
                            else
                            {
                                Console.WriteLine("Введите либо Ф, либо УГ!");
                            }
                        } while (error);
                        list.Add(geo);
                        WriteToFile(path, list);
                        log.time = Convert.ToString(DateTime.Now);
                        log.choice_Functions = $" - Добавлена запись: '{geo.state}'";
                        time_List.Add(log);
                    }
                    else if (choice is Functions.Del)
                    {
                        Console.WriteLine("Введите номер удаляемой записи:");
                        int number;
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("Введите число!");
                        }
                        else
                        {
                            if (list.Count >= number)
                            {
                                log.time = Convert.ToString(DateTime.Now);
                                log.choice_Functions = $" - Удалена запись: '{list.Get(number - 1)}'";
                                time_List.Add(log);
                                list.Remove(number - 1);
                                WriteToFile(path, list);
                            }
                            else if (number < 0)
                            {
                                Console.WriteLine("Записи с таким номером не существует!");
                            }
                            else
                            {
                                Console.WriteLine("Записи с таким номером не существует!");
                            }
                        }
                    }
                    else if (choice is Functions.Update)
                    {

                        Console.WriteLine("Введине номер обновляемой записи:");
                        int number;
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("Вы ввели не число!");
                        }
                        else
                        {
                            if (list.Count >= number)
                            {
                                Console.WriteLine("Государство:");
                                geo.state = Console.ReadLine().Trim();
                                Console.WriteLine("Столица:");
                                geo.capital = Console.ReadLine().Trim();
                                Console.WriteLine("Население:");
                                geo.people = Console.ReadLine().Trim();
                                Console.WriteLine("Строй:");
                                bool error = true;
                                do
                                {
                                    string sys = Console.ReadLine().Trim();
                                    if (sys == "Ф")
                                    {
                                        geo.system = System.Ф;
                                        error = false;
                                    }
                                    else if (sys == "УГ")
                                    {
                                        geo.system = System.УГ;
                                        error = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Введите либо Ф, либо УГ!");
                                    }
                                } while (error);
                                log.time = Convert.ToString(DateTime.Now);
                                log.choice_Functions = $" - Обновлена запись: '{list.Get(number - 1)}'";
                                time_List.Add(log);
                                list.Update(number, geo);
                                WriteToFile(path, list);
                            }
                            else if (list.Count < number)
                            {
                                Console.WriteLine("Записи с таким номером не существует!");
                            }
                        }
                    }
                    else if (choice is Functions.Find)
                    {
                        DoublyLinkedList<Geograf> list_Find = new DoublyLinkedList<Geograf>();
                        Console.WriteLine("Выберите фильтр:");
                        string filter = Console.ReadLine().Trim();
                        switch (filter)
                        {
                            case "Государство":
                                Console.WriteLine("Введите название государства:");
                                string state = Console.ReadLine().Trim();
                                string find_state = null;
                                foreach(Geograf g in list)
                                {
                                    find_state = $"{g.state}";
                                    if (find_state.Contains(state))
                                    {
                                        list_Find.Add(g);
                                        geo.Print(path, list_Find);
                                    }
                                }
                                break;
                            case "Столица":
                                Console.WriteLine("Введите название столицы:");
                                string capital = Console.ReadLine().Trim();
                                string find_capital = null;
                                foreach (Geograf g in list)
                                {
                                    find_capital = $"{g.capital}";
                                    if (find_capital.Contains(capital))
                                    {
                                        list_Find.Add(g);
                                        geo.Print(path, list_Find);
                                    }
                                }
                                break;
                            case "Население":
                                Console.WriteLine("Введите численность населения:");
                                string people = Console.ReadLine().Trim();
                                string find_people = null;
                                foreach (Geograf g in list)
                                {
                                    find_people = $"{g.people}";
                                    if (find_people.Contains(people))
                                    {
                                        list_Find.Add(g);
                                        geo.Print(path, list_Find);
                                    }
                                }
                                break;
                            case "Строй":
                                Console.WriteLine("Введите форму государственного устройства:");
                                string system = Console.ReadLine().Trim();
                                string find_system = null;
                                foreach (Geograf g in list)
                                {
                                    find_system = $"{g.system}";
                                    if (find_system.Contains(system))
                                    {
                                        list_Find.Add(g);
                                        geo.Print(path, list_Find);
                                    }
                                }
                                break;
                            default:
                                Console.WriteLine("Вы ввели несуществующий фильтр!");
                                break;
                        }
                    }
                    else if (choice is Functions.Log)
                    {
                        Log.SeeLog(time_List);
                    }
                    else if (choice is Functions.Sort)
                    {
                        int index;
                        for (int i = 0; i < list.Count; i++)
                        {
                            index = i;
                            for (int j = i; j < list.Count; j++)
                            {
                                if (Convert.ToInt32(list.Get(j).people) < Convert.ToInt32(list.Get(index).people))
                                {
                                    index = j;
                                }
                            }
                            if (Convert.ToInt32(list.Get(index).people) == Convert.ToInt32(list.Get(i).people))
                            {
                                continue;
                            }
                            else
                            {
                                Geograf temp = list.Get(i);
                                list.Update(i, list.Get(index));      
                                list.Update(index, temp);
                            }
                        }
                        geo.Print(path, list);
                    }
                    else if (choice is Functions.Exit)
                    {
                        exit = false;
                    }
                }
                catch
                {
                    Console.WriteLine("Такого пункта меню не существует!");
                }
            } while (exit == true);
        }
    }
}