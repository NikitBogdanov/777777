using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp7
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node(T data)
        {
            Data = data;
        }
        public Node<T> Column;
        public Node<T> Line;
        public int I { get; set; }
        public int J { get; set; }
    }
}
