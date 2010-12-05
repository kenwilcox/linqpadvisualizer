using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQPadVisualizer;

namespace LINQPadVisualizerTest
{
    
    public class TestData
    {
        public int testInt { get; set; }
        public string MyProperty { get; set; }

    }
    class Program
    {
        
        static void Main(string[] args)
        {
            var tst = new TestData { MyProperty="asasa", testInt=121231 };
            LINQPadVisualizer4JS.TestShowVisualizer(new System.WeakReference(tst));
        }
    }
}
