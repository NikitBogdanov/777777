using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace ConsoleApp3
{
    class CircularLinkedList<T> : IEnumerable<T>
    {
        public Node<T> Head;
        public Node<T> Tail;
        int count = 0;
        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);
            if(Head != null)
            {
                node.Next = Head;
                Tail.Next = node;
                Tail = node;
            }
            else
            {
                Head = node;
                Tail = node;
                Tail.Next = Head;
            }
            count++;
        }
        public bool Remove(T data)
        {
            Node<T> current = Head;
            Node<T> previous = null;
            if (IsEmpty) return false;
            do
            {
                if (current.Data.Equals(data))
                {
                    if (previous == null)
                    {
                        Head = current.Next;
                        Tail.Next = Head;
                        if (count == 1)
                        {
                            Head = null;
                            Tail = null;
                        }
                    }
                    else
                    {
                        previous.Next = current.Next;
                        if (current == Tail)
                        {
                            Tail = previous;
                        }
                    }
                    count--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            while (current != Head);
            return false;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        public int Count
        {
            get
            {
                return count;
            }
        }
        public bool IsEmpty
        {
            get
            {
                return count == 0;
            }
        }
        public void Clear()
        {
            Head = null;
            Tail = null;
            count = 0;
        }
        public bool Contains(T data)
        {
            Node<T> current = Head;
            if(current == null)
            {
                return false;
            }
            do
            {
                if (current.Data.Equals(data))
                {
                    return true;
                }
                else
                {
                    current = current.Next;
                }
            } 
            while (current != Head);
            return false;
        }
        public T Info(int index)
        {
            Node<T> current = Head;
            int number = 0;
            while(number < index)
            {
                number++;
                current = current.Next;
            }
            return current.Data;
        }
    }
}
