using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class DataList<T> : IEnumerable<T>
    {
        Node<T> Head;
        Node<T> Tail;
        int count = 0;
        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);
            if(Head == null)
            {
                Head = node;
            }
            else
            {
                Tail.Next = node;
            }
            Tail = node;
            count++;
        }
        public bool Contains(T data)
        {
            Node<T> current = Head;
            while(current != null)
            {
                if(current.Data.Equals(data))
                {
                    return true;
                }
                else
                {
                    current = current.Next;
                }
            }
            return false;
        }
        public bool Remove(T data)
        {
            Node<T> current = Head;
            Node<T> last = null;
            if(Contains(data) == true)
            {
                count--;
                while (current != null)
                {
                    if (current.Data.Equals(data))
                    {
                        last.Next = current.Next;
                        current = null;
                        return true;
                    }
                    else
                    {
                        last = current;
                        current = current.Next;
                    }
                }
            }
            return false;
        }
        public int Count { get { return count; } }
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = Head;
            while(current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }
}
