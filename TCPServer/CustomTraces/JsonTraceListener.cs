using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace TCPServer.CustomTraces
{
    class JsonTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            Console.WriteLine(message);
        }

        public override void WriteLine(string message)
        {
            BaseClass b = new BaseClass(message);
            string json = JsonConvert.SerializeObject(b);
            Console.WriteLine(json);

        }
    }
}
