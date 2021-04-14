using NUnit.Framework;
using SortTree;
using System;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Test_Empty()
        {
            var test = new TreeNode(null);
            Assert.AreEqual(null, test.Transform().Item1[0]);
        }

        [Test]
        public void Test_OneElem()
        {
            var test = new TreeNode("a");
            Assert.AreEqual("a", test.Transform().Item1[0]);
        }

        [Test]
        public void Test_MultyElem()
        {
            var test = new TreeNode("b");
            test.Insert(new TreeNode("c"));
            test.Insert(new TreeNode("a"));
            Assert.AreEqual(new string[] { "a", "b", "c" }, test.Transform().Item1);
        }

        [Test]
        public void Test_Numbers()
        {
            var test = new TreeNode("3");
            test.Insert(new TreeNode("2"));
            test.Insert(new TreeNode("1"));
            Assert.AreEqual(new string[] { "1", "2", "3" }, test.Transform().Item1);
        }

        [Test]
        public void Test_StrDiffCases()
        {
            var test = new TreeNode("B");
            test.Insert(new TreeNode("a"));
            test.Insert(new TreeNode("A"));
            test.Insert(new TreeNode("b"));
            Assert.AreEqual(new string[] { "a", "A", "b", "B" }, test.Transform().Item1);
        }

        [Test]
        public void Test_StrSpecChar()
        {
            var test = new TreeNode("B");
            test.Insert(new TreeNode("a"));
            test.Insert(new TreeNode("A"));
            test.Insert(new TreeNode("b"));
            test.Insert(new TreeNode("?"));
            Assert.AreEqual(new string[] { "?", "a", "A", "b", "B" }, test.Transform().Item1);
        }

        [Test]
        public void Test_StrSpecChar_Numbers_DiffCases()
        {
            var test = new TreeNode("B");
            test.Insert(new TreeNode("a"));
            test.Insert(new TreeNode("A"));
            test.Insert(new TreeNode("b"));
            test.Insert(new TreeNode("3"));
            test.Insert(new TreeNode("1"));
            test.Insert(new TreeNode("2"));
            test.Insert(new TreeNode("?"));
            Assert.AreEqual(new string[] { "?", "1", "2", "3", "a", "A", "b", "B" }, test.Transform().Item1);
        }

        [Test]
        public void Test_Strings()
        {
            var test = new TreeNode("abc");
            test.Insert(new TreeNode("acb"));
            test.Insert(new TreeNode("bca"));
            test.Insert(new TreeNode("cba"));
            test.Insert(new TreeNode("bac"));
            Assert.AreEqual(new string[] { "abc", "acb", "bac", "bca", "cba" }, test.Transform().Item1);
        }

        [Test]
        public void Test_EqualStr()
        {
            var test = new TreeNode("a");
            test.Insert(new TreeNode("a"));
            test.Insert(new TreeNode("a"));
            Assert.AreEqual(new string[] { "a", "a", "a" }, test.Transform().Item1);
        }
    }
}