using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCPServer.CustomTraces;

namespace TCPServer
{
    public abstract class AbstractTCPServer
    {
        private bool STOP = false;
        /// <summary>
        /// TcpServerWork er en abstract metode som man skal override og tilføje den funktionalitet som man ønsker i programet. 
        /// Man har på forhånd defineret metoderne StreamReader og StreamWriter, hvilket gør at man bare skal indskrive det man
        /// ønsker systemet skal udfører
        /// </summary>
        /// <param name="reader"> reader er en StreamReader som dermed aflæser tegn fra byte stream</param>
        /// <param name="writer"> writer er en StreamWriter som dermed omskriver tegn til byte stream</param>
        protected abstract void TcpServerWork(StreamReader reader, StreamWriter writer);
        private TraceSource ts;
        private CompositeFilter cf = new CompositeFilter();
        public AbstractTCPServer()
        {
            cf.Add(new SubstringFilter());


            ts = new TraceSource("AbstractTCP");


            ts.Switch = new SourceSwitch("AbstractTCP", "All");

            TraceListener rest = new RestTraceListener();
            ts.Listeners.Add(rest);
            
            //TraceListener consoleLog = new ConsoleTraceListener();
            //ts.Listeners.Add(consoleLog);

            TraceListener fileLog = new TextWriterTraceListener(new StreamWriter("text.txt"));
            ts.Listeners.Add(fileLog);
            fileLog.Filter = cf;

            TraceListener logListener = new EventLogTraceListener("Application");
            ts.Listeners.Add(logListener);

            //TraceListener jsonlistener = new JsonTraceListener();
            //ts.Listeners.Add(jsonlistener);
        }
        /// <summary>
        /// Denne metode udføre det mest af TCP Servers arbejde. Alt er nærmest defineret fra at den kan tage flere clients af gange
        /// til tjekkelsen af om nogen lytter på connection før det lukker ned.
        /// </summary>
        /// <param name="port"> Dette parameter giver muligheder for at bestemme hvilket portnummer der skal benyttes</param>
        public void Start(int port)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            ts.TraceEvent(TraceEventType.Information,444, "Server Start");
            ts.TraceEvent(TraceEventType.Information, 444, $"started  at port { port}");

            Task.Run(()=>StopServer(port));
            ts.TraceEvent(TraceEventType.Verbose, 444, "Stop server started");

            while (!STOP)
            {
                if (listener.Pending())
                {
                    TcpClient socket = listener.AcceptTcpClient();
                    ts.TraceEvent(TraceEventType.Information, 444, "Client incoming");
                    ts.TraceEvent(TraceEventType.Information, 444, $"remote (ip,port) = ({socket.Client.RemoteEndPoint})");

                    Task.Run(() =>
                    {
                        TcpClient tmpsocket = socket;
                        TcpServerWork(new StreamReader(tmpsocket.GetStream()), new StreamWriter(tmpsocket.GetStream()));
                }
                );
                }
                else
                {
                    Thread.Sleep(2*1000);
                }
            }
            ts.TraceEvent(TraceEventType.Error, 444, "Server Stopped");
            ts.Close();
        }
        /// <summary>
        /// StopServer gør at programmet har en blød nedlukning når der bliver kaldet på portnummeret + 1.
        /// </summary>
        /// <param name="port"> port parameter er bare en parameter der bliver plusset med 1, da den lytter på hvilket port Start() er på</param>
        private void StopServer(int port)
        {
            
            TcpListener listener = new TcpListener(IPAddress.Any, port+1);
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();

            STOP = true;
        }
    }
}
