using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TCPServer.CustomTraces
{
    class CompositeFilter : TraceFilter
    {
        private List<TraceFilter> lists = new List<TraceFilter>();
        public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
        {
            foreach (var item in lists)
            {
                if (!item.ShouldTrace(cache, source, eventType, id, formatOrMessage, args, data1, data))
                {
                    return false;
                }
            }
            return true;
        }

        public void Add(TraceFilter tf)
        {
            lists.Add(tf);
        }
        public void Delete(TraceFilter tf)
        {
            lists.Remove(tf);
        }
    }
}
