using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using Newtonsoft.Json;

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(LINQPadVisualizer.LINQPadVisualizer4JS),
typeof(LINQPadVisualizer.JSObjectSource),
Target = typeof(System.WeakReference),
Description = "LINQPad Visualizer4 JS")]
namespace LINQPadVisualizer
{

    /// <summary>
    /// A Visualizer for any types by using WeakReference.
    /// </summary>
    ///
    public class JSObjectSource : VisualizerObjectSource
    {
        public override void GetData(object inObject, Stream outStream)
        {
            string s1 = JsonConvert.SerializeObject(inObject);
            var writer = new StreamWriter(outStream);
            writer.Write(s1);
            writer.Flush();

        }
    }



    public class LINQPadVisualizer4JS : Microsoft.VisualStudio.DebuggerVisualizers.DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            // TODO: Get the object to display a visualizer for.
            //       Cast the result of objectProvider.GetObject()
            //       to the type of the object being visualized.
            //object data = (object)objectProvider.GetObject();
            var s2 = new StreamReader(objectProvider.GetData()).ReadToEnd();
            JObject jsonObject = JObject.Parse(s2);
            // eval into an expando
            dynamic dynObject = ConvertJTokenToObject(jsonObject);
           // WeakReference wr = (WeakReference)data;


            // TODO: Display your view of the object.
            //       Replace displayForm with your own custom Form or Control.
            using (Form1 displayForm = new Form1())
            {
                //displayForm.Text = data.ToString();
                var lxw = LINQPad.Util.CreateXhtmlWriter(true);
                lxw.Write(dynObject.TrackedObject);
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
            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(LINQPadVisualizer4JS), typeof(JSObjectSource));
            visualizerHost.ShowVisualizer();
        }


        // the credit for this code goes to Peter Goodman - http://blog.petegoo.com/archive/2009/10/27/using-json.net-to-eval-json-into-a-dynamic-variable-in.aspx
        //
        public object ConvertJTokenToObject(JToken token)
        {
            if (token is JValue)
            {
                return ((JValue)token).Value;
            }
            if (token is JObject)
            {
                ExpandoObject expando = new ExpandoObject();
                (from childToken in ((JToken)token) where childToken is JProperty select childToken as JProperty).ToList().ForEach(property =>
                {
                    ((IDictionary<string, object>)expando).Add(property.Name, ConvertJTokenToObject(property.Value));
                });
                return expando;
            }
            if (token is JArray)
            {
                object[] array = new object[((JArray)token).Count];
                int index = 0;
                foreach (JToken arrayItem in ((JArray)token))
                {
                    array[index] = ConvertJTokenToObject(arrayItem);
                    index++;
                }
                return array;
            }
            throw new ArgumentException(string.Format("Unknown token type '{0}'", token.GetType()), "token");
        }

    }
}
