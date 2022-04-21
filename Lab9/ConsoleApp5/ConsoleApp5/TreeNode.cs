using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5
{
    class TreeNode
    {
        public string Country { get; set; }
        public int Wins { get; set; }
        public TreeNode Right { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Parent { get; set; }
        public TreeNode(string country, int wins)
        {
            Country = country;
            Wins = wins;
        }
    }
}
