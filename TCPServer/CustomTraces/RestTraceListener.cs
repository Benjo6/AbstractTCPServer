using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace TCPServer.CustomTraces
{
    class RestTraceListener : TraceListener
    {
        private string URI = "http://localhost:56476/api/Event/";


        public override void Write(string message)
        {
            Console.WriteLine(message);
        }
      

        public override void WriteLine(string message)
        {
            BaseClass b = new BaseClass(message);
            PostItemAsync(b);
            Console.WriteLine(b);

        }

        public async void PostItemAsync(BaseClass b)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(b);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(URI, content);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                throw new ArgumentException("Opret fejlede");
            }
        }
    }
}
