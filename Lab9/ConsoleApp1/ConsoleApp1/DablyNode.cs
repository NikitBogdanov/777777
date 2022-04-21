using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class DoublyNode<T>
    {
        public T Data { get; set; }
        public DoublyNode<T> Previous { get; set; }
        public DoublyNode<T> Next { get; set; }
        public DoublyNode(T data)
        {
            Data = data;
        }
    }
}
