using System;
using System.Collections.Generic;
using System.Text;
using TCPServer.Servers;

namespace TCPServer
{
    class ServerSetup
    {
        public void Start()
        {
            AbstractTCPServer adt = new EchoServer();
            //AbstractTCPServer adt1 = new BIGEchoServer();


            adt.Start(1000);
        }
    }
}
