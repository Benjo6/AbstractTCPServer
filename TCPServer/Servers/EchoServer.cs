using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Servers
{
    class EchoServer : AbstractTCPServer
    {
        protected override void TcpServerWork(StreamReader reader, StreamWriter writer)
        {
            while (true)
            {
                string str = reader.ReadLine();
                writer.WriteLine(str);
                writer.Flush();
            }
        }
    }
}
