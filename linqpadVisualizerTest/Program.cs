using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace linqpadVisualizerTest
{
    [Serializable]
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
            linqpadvisualizer.LinqpadVisualizer.TestShowVisualizer(new System.WeakReference(tst));
        }
    }
}
