using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TCPServer.Servers
{
    class BIGEchoServer : AbstractTCPServer
    {
        protected override void TcpServerWork(StreamReader reader, StreamWriter writer)
        {
            while (true)
            {
                string str = reader.ReadLine();
                writer.WriteLine(str.ToUpper());
                writer.Flush();
            }
        }
    }
}
