using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ConsoleApp5
{
    class BinaryTree
    {
        public List<string> Countries = new List<string>(16);
        public BinaryTree(List<string> countries)
        {
            Countries = countries;
            Add();
            AddCountries(Root, Countries);
            FindWinner(Root);
            FindWinner(Root);
            FindWinner(Root);
            FindWinner(Root);
        }
        public TreeNode Root = new TreeNode(null, 0);
        int count = 4;
        public void Add()
        {
            add(Root, 4);
            RemoveUnnecessary(Root, 4);
        }
        public void RemoveUnnecessary(TreeNode unnecessary, int count)
        {
            if(count > 0)
            {
                count--;
                RemoveUnnecessary(unnecessary.Left, count);
                RemoveUnnecessary(unnecessary.Right, count);
            }
            else
            {
                if(unnecessary.Left != null || unnecessary.Right != null)
                {
                    unnecessary.Left = unnecessary.Right = null;
                }
            }
        }
        public void add(TreeNode current, int index)
        {
            if (index > 0)
            {
                current.Right = new TreeNode(null, 0);
                current.Right.Parent = current;
                current.Left = new TreeNode(null, 0);
                current.Left.Parent = current;
                index--;
                add(current.Right, index);
                add(current.Left, index);
            }
        }
        public int Count() { return count; }
        public void AddCountries(TreeNode current, List<string> countries)
        {
            if(current.Left.Left != null || current.Right.Right != null)
            {
                AddCountries(current.Left, countries);
                AddCountries(current.Right, countries);
            }
            else
            {
                Random random = new Random();
                current.Left.Country = countries[0];
                current.Left.Wins = random.Next(0, 10);
                countries.RemoveAt(0);
                current.Right.Country = countries[0];
                current.Right.Wins = random.Next(0, 10);
                countries.RemoveAt(0);
            }
        }
        public void FindWinner(TreeNode current)
        {
            if(current.Right.Country == null || current.Left.Country == null)
            {
                FindWinner(current.Left);
                FindWinner(current.Right);
            }
            else
            {
                Random random = new Random();
                if(current.Left.Wins > current.Right.Wins)
                {
                    Console.WriteLine($"{current.Left.Country} - {current.Right.Country} : {current.Left.Wins} - {current.Right.Wins}");
                    current.Country = current.Left.Country;
                    current.Wins = random.Next(0, 10);
                }
                else
                {
                    Console.WriteLine($"{current.Left.Country} - {current.Right.Country} : {current.Left.Wins} - {current.Right.Wins}");
                    current.Country = current.Right.Country;
                    current.Wins = random.Next(0, 10);
                }
            }
        }

    }
}
