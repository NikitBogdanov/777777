using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp3
{
    class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node(T data)
        {
            Data = data;
        }
    }
}
