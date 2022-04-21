using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace ConsoleApp7
{
    public class ArrayList<T>: IEnumerable<T>
    {
        public Node<T> Head;
        public Node<T> Tail;
        public int count;
        public int MaxCount { get { return IndexLine * IndexColumn; } }
        public int IndexLine { get; set; }
        public int IndexColumn { get; set; }
        public ArrayList(int indexLine, int indexColumn)
        {
            IndexLine = indexLine;
            IndexColumn = indexColumn;
        }
        public int ColumnTransition = 0;
        public int LineTransition = 0;
        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);
            if(Head == null)
            {
                LineTransition++;
                ColumnTransition++;
                node.I = LineTransition;
                node.J = ColumnTransition;
                Head = node;
                Tail = node;
            }
            else
            {
                if(ColumnTransition < IndexColumn)
                {
                    ColumnTransition++;
                    node.J = ColumnTransition;
                    node.I = LineTransition;
                    Tail.Column = node;
                    Tail = node;
                }
                else if(ColumnTransition >= IndexColumn && LineTransition < IndexLine)
                {
                    LineTransition++;
                    ColumnTransition = 1;
                    node.I = LineTransition;
                    node.J = ColumnTransition;
                    Tail.Line = node;
                    Tail = node;
                }
            }
            count++;
        }
        public T Output(int i, int j)
        {
            Node<T> current = Head;
            int indexLine = 1;
            int indexColumn = 1;
            while(current != Tail.Column || current != Tail.Line)
            {
                if(current.I == i && current.J == j)
                {
                    return current.Data;
                }
                else
                {
                    if(current.Column == null)
                    {
                        indexColumn = 1;
                        indexLine++; 
                        current = current.Line;
                    }
                    else
                    {
                        indexColumn++;
                        current = current.Column;
                    }
                }
            }
            return Tail.Column.Data;
        }
        public bool Update(int i, int j, T data)
        {
            Node<T> current = Head;
            int index = 1;
            while(index <= (IndexLine * IndexColumn))
            {
                if(current.I == i && current.J == j)
                {
                    current.Data = data;
                    return true;
                }
                else if(current.Column != null)
                {
                    current = current.Column;
                }
                else
                {
                    current = current.Line;
                }
                index++;
            }
            return true;
        }
        public int Count() { return count; }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = Head;
            int CurrentIndexOfElement = 0;
            while(CurrentIndexOfElement < count)
            {
                yield return current.Data;
                if(current.Line != null)
                {
                    Console.WriteLine();
                }
                current = current.Column == null ? current.Line : current.Column;
                CurrentIndexOfElement++;
            }
        }
    }
}
