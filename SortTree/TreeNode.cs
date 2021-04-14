using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortTree
{
    public class TreeNode
    {
        public TreeNode(string data, int iter = 0)
        {
            Data = data;
            Iter = iter;
        }

        public static int Iter;
        public string Data { get; set; }

        public TreeNode Left { get; set; }

        public TreeNode Right { get; set; }

        public void Insert(TreeNode node)
        {
            if (String.Compare(node.Data, Data) < 0)
            {
                if (Left == null)
                {
                    Iter++;
                    Left = node;
                }
                else
                {
                    Iter++;
                    Left.Insert(node);
                }
            }
            else
            {
                if (Right == null)
                {
                    Iter++;
                    Right = node;
                }
                else
                {
                    Iter++;
                    Right.Insert(node);
                }
            }
        }

        public Tuple<List<string>, int> Transform(List<string> elements = null)
        {
            
            if (elements == null)
            {
                elements = new List<string>();
            }

            if (Left != null)
            {
                Left.Transform(elements);
            }

            Iter++;
            elements.Add(Data);

            if (Right != null)
            {
                Right.Transform(elements);
            }

            return Tuple.Create(elements, Iter);
        }
    }
}
