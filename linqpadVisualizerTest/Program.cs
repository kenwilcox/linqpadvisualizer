using System;
using LINQPadVisualizer;

namespace LINQPadVisualizerTest
{
    [Serializable]
    public class TestData
    {
        public int testInt { get; set; }
        public string MyProperty { get; set; }

    }

    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var tst = new TestData { MyProperty="asasa", testInt=121231 };
            LINQPadVisualizer4JS.TestShowVisualizer(new System.WeakReference(tst));
            //LINQPadVisualizer.LINQPadVisualizer.TestShowVisualizer(new System.WeakReference(tst));
        }
    }
}
