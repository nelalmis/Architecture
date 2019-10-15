using Architecture.Common.Types;
using Architecture.Types.Root.BusinessHelper;
using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class Program
    {
       private static Architecture.WCFClient.ExecuteServiceClient client;
        static void Main(string[] args)
        {

            Solution2();

            Console.ReadLine();
        }

        public  static void Solution1()
        {/*
            ServiceHost host = new ServiceHost(typeof(ExecuteService), new Uri("http://localhost:8000"));
            host.AddServiceEndpoint(typeof(IExecuteService), new BasicHttpBinding(), "Soap");
            ServiceEndpoint endpoint = host.AddServiceEndpoint(typeof(IExecuteService), new WebHttpBinding(), "Web");
        endpoint.Behaviors.Add(new WebHttpBehavior());

            try
            {
                host.Open();
                using (WebChannelFactory<IExecuteService> wcf = new WebChannelFactory<IExecuteService>(new Uri("http://localhost:8000/Web")))
                {

                    string authHeaer = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("admin" + ":" + "admin"));
        IExecuteService channel = wcf.CreateChannel();

                    using (new OperationContextScope((IContextChannel)channel))
                    {
                        WebOperationContext.Current.OutgoingRequest.Headers.Add("Authorization", "Basic " + authHeaer);

                        var result = channel.TestService("Nevzat ");
    Console.WriteLine(result);
                        Console.ReadLine();
                    }
                    
                }
            }
            catch { }
            */
        }
        public static void Solution2()
        {
            client = new Architecture.WCFClient.ExecuteServiceClient();
            Console.WriteLine(client.TestService("Merhaba"));
        }
        public static void Solution3()
        {
           
        }
    }
}
