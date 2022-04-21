using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите математическое выражение");
            string MathStr = Console.ReadLine();
            Stack<char> stack = new Stack<char>();
            for(int i = MathStr.Length - 1; i > -1; i--)
            {
                stack.Push(MathStr[i]);
            }
            bool correct = true;
            int open = 0;
            int close = 0;
            foreach(char f in stack)
            {              
                if(f == '(')
                {
                    open++;
                }
                else if(f == ')')
                {
                    close++;
                }
                if (close > open)
                {
                    correct = false;
                    break;
                }
            }
            if(close < open || correct == false)
            {
                Console.WriteLine("Выражение не корректно!");
            }
            else 
            {
                Console.WriteLine("Выражение корректно");
            }
        }
    }
}
