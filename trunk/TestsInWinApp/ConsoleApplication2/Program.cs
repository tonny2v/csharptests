﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Diagnostics;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    [TestFixture]
    public class Tests
    {
        [Test]
        public void test1() 
        {
            Assert.AreEqual(1, 2-1);
            foreach (var i in Enumerable.Range(1,100)){
            Debug.WriteLine(i);}
        }
    }
}
