using System;
using System.Xml;
using TCPServer.Servers;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerSetup worker = new ServerSetup();
            worker.Start();

            //ReadConfiguration();




        }
        private static void ReadConfiguration() 
        {
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(@"C:\Users\Benjamin Curovic\source\repos\AbstractTCPServer-master\AbstractTCPServer-master\ServerConfiguration.txt");
            
            XmlNode portNode = configDoc.DocumentElement.SelectSingleNode("ServerPort");
            if (portNode != null)
            {
                String portStr = portNode.InnerText.Trim();
                int serverPort = Convert.ToInt32(portStr);


                Console.WriteLine("Server port er " + serverPort);
            }

            XmlNode shutDownPortNode = configDoc.DocumentElement.SelectSingleNode("ShutDownPort");
            if (shutDownPortNode != null)
            {
                String portStr = shutDownPortNode.InnerText.Trim();
                int shutDownPort = Convert.ToInt32(portStr);


                Console.WriteLine("ShutDown port er " + shutDownPort);
            }

            XmlNode nameNode = configDoc.DocumentElement.SelectSingleNode("ServerName");
            if (nameNode != null)
            {
                String nameStr = nameNode.InnerText.Trim();

                Console.WriteLine("Server name er " + nameStr);
            }

        }

    }
}
