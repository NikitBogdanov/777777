using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace ConsoleApp1
{
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        DoublyNode<T> Head;
        DoublyNode<T> Tail;
        int count = 0;

        public void Add(T data)
        {
            DoublyNode<T> node = new DoublyNode<T>(data);
            if (Head == null)
            {
                Head = node;
            }
            else
            {
                Tail.Next = node;
                node.Previous = Tail;
            }
            Tail = node;
            count++;
        }
        public void AddFirst(T data)
        {
            DoublyNode<T> node = new DoublyNode<T>(data);
            if (Head == null)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                Head.Previous = node;
                node.Next = Head;
                Head = node;
            }
            count++;
        }
        public bool Remove(int number)
        {
            DoublyNode<T> current = Head;
            int index = -1;
            while (index != number)
            {
                current = current.Next;
                index++;
            }
            if (current != null)
            {
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    Tail = current.Previous;
                }
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    Head = current.Next;
                }
                count--;
                return true;
            }
            return false;
        }
        public int Count { get { return count; } }
        public bool Empty{ get { return count == 0; } }
        public void Clear()
        {
            count = 0;
            Head = null;
            Tail = null;
        }
        public bool Contains(T data)
        {
            DoublyNode<T> current = Head;
            while(current != null)
            {
                if (current.Data.Equals(data))
                {
                    break;
                }
                else
                {
                    current = current.Next;
                }
            }
            if(current == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void Update(int number, T data)
        {
            DoublyNode<T> current = Head;
            int index = -1;
            if(current != null)
            {
                while(index != number)
                {
                    current = current.Next;
                    index++;
                }
                current.Data = data;
            }
        }
        public T Get(int index)
        {
            DoublyNode<T> current = Head;
            int number = -1;
            while(number != index)
            {
                if(number > -1)
                {
                    current = current.Next;
                }
                number++;
            }
            return current.Data;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            DoublyNode<T> CurrentNode = Head;
            while(CurrentNode != null)
            {
                yield return CurrentNode.Data;
                CurrentNode = CurrentNode.Next;
            }
        }
    }
}
