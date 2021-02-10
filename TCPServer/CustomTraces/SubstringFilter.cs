using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TCPServer.CustomTraces
{
    class SubstringFilter : TraceFilter
    {
        
        public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
        {
            if (eventType == TraceEventType.Error)
            {
                return true;
            }

            else
                return false;
        }
    }
}
