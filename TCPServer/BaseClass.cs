using System;
using System.Collections.Generic;
using System.Text;

namespace TCPServer
{
    public class BaseClass
    {
        public BaseClass()
        {
        }

        public BaseClass(string eventconsoles)
        {
            EventConsoles = eventconsoles;
        }
        public string EventConsoles { get; set; }
        public override string ToString()
        {
            return $"{EventConsoles}";
        }
    }
}
