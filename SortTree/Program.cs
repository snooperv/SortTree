using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace SortTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var exps = ReadXML();
            for (var k = 0; k < exps.Count; k++)
            {
                var count = exps[k].MaxLength;
                var n = exps[k].Diff;
                if (exps[k].Name == "arith")
                {
                    using (StreamWriter sw = new(@"../../../Results.csv", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine("Arithmetic;");
                    }
                    for (var i = 0; i < count; i += n)
                        TreeSortArth(i);
                }
                else if (exps[k].Name == "denom")
                {
                    using (StreamWriter sw = new(@"../../../Results.csv", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine("Geometric;");
                    }
                    for (var i = 1; i <= count; i *= n)
                        TreeSortDenom(i);
                }
            }
        }

        private static List<Experiments> ReadXML()
        {
            var exps = new List<Experiments>();
            var xDoc = new XmlDocument();
            xDoc.Load(@"../../../experiments.xml");
            var xRoot = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                var exp = new Experiments();
                var attr = xnode.Attributes.GetNamedItem("name");
                if (attr != null)
                    exp.Name = attr.Value;
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "maxLength")
                        exp.MaxLength = int.Parse(childnode.InnerText);
                    if (childnode.Name == "diff")
                        exp.Diff = int.Parse(childnode.InnerText);
                    if (childnode.Name == "repeat")
                        exp.Repeat = int.Parse(childnode.InnerText);
                }
                exps.Add(exp);
            }
            return exps;
        }

        private static void TreeSortArth(int i)
        {
            var words = Randomize(i, 10);
            var nodes = TreeInsert(words);
            var wordsSort = nodes.Item1.Transform();
            var iter = wordsSort.Item2 + nodes.Item2;
            Output(i, iter);
        }

        private static void TreeSortDenom(int i)
        {
            var words = Randomize(i, 10);
            var nodes = TreeInsert(words);
            var wordsSort = nodes.Item1.Transform();
            var iter = wordsSort.Item2 + nodes.Item2;
            Output(i, iter);
        }

        private static List<string> Randomize(int count, int n)
        {
            var words = new List<string>();
            var letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            var rand = new Random();
            for (var i = 1; i <= count; i++)
            {
                var word = "";
                for (var j = 1; j <= n; j++)
                {
                    var letter_num = rand.Next(0, letters.Length - 1);
                    word += letters[letter_num];
                }
                words.Add(word);
            }
            return words;
        }

        private static Tuple<TreeNode, int> TreeInsert(List<string> array)
        {
            if (array.Count > 0)
            {
                var treeNode = new TreeNode(array[0]);
                var counter = 0;
                for (var i = 1; i < array.Count; i++)
                {
                    treeNode.Insert(new TreeNode(array[i]));
                    counter += TreeNode.Iter;
                }
                return Tuple.Create(treeNode, counter);
            }
            else
                return Tuple.Create(new TreeNode(null), 0);
        }

        private static void Output(int i, int sort)
        {
            using StreamWriter sw = new(@"../../../Results.csv", true, System.Text.Encoding.Default);
            sw.Write(i + ";");
            sw.WriteLine(sort);
        }
    }
}
