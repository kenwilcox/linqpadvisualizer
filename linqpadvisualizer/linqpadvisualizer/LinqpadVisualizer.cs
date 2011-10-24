using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(LINQPadVisualizer.LINQPadVisualizer),
typeof(VisualizerObjectSource),
Target = typeof(System.WeakReference),
Description = "LINQPad Visualizer")]
namespace LINQPadVisualizer
{
   
    /// <summary>
    /// A Visualizer for any types by using WeakReference.  
    /// </summary>
    public class LINQPadVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            // TODO: Get the object to display a visualizer for.
            //       Cast the result of objectProvider.GetObject() 
            //       to the type of the object being visualized.
            object data = (object)objectProvider.GetObject();
            WeakReference wr = (WeakReference)data;


            // TODO: Display your view of the object.
            //       Replace displayForm with your own custom Form or Control.
            using (Form1 displayForm = new Form1())
            {
                displayForm.Text = data.ToString();
                var lxw = LINQPad.Util.CreateXhtmlWriter(true, 50);
                lxw.Write(wr.Target);
                displayForm.setHTML(lxw.ToString());
                windowService.ShowDialog(displayForm);
            }
        }

        // TODO: Add the following to your testing code to test the visualizer:
        // 
        //    LINQPadVisualizer.TestShowVisualizer(new SomeType());
        // 
        /// <summary>
        /// Tests the visualizer by hosting it outside of the debugger.
        /// </summary>
        /// <param name="objectToVisualize">The object to display in the visualizer.</param>
        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(LINQPadVisualizer));
            visualizerHost.ShowVisualizer();
        }
    }
}
