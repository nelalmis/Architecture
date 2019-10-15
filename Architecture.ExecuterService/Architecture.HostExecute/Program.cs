using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.ExecuterService;
using Architecture.HostService;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace Architecture.HostExecute
{
    class Program
    {
        static void Main(string[] args)
        {
            var adfa= Architecture.View.Root.BusinessHelper.BusinessHelper.TestService("sdfsdfsd");
            using (var proxy = new GenericProxy<ExecuterService.IExecuterService,ExecuterService.ExecuterService>("admin","",ServiceType.REST))
            {
                string authHeaer = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("admin" + ":" + "admin"));

                using (new OperationContextScope((IContextChannel)proxy.Channel))
                {
                    WebOperationContext.Current.OutgoingRequest.Headers.Add("Authorization", "Basic " + authHeaer);

                    var result = proxy.Execute<string>(x => x.GetMessage("Nevzat "));
                    Console.WriteLine(result);
                    Console.ReadLine();
                }
            }

            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            Console.WriteLine(client.GetServerName());
            Console.ReadLine();
            return;
            
            return;
            ServiceHost host = new ServiceHost(typeof(ExecuterService.ExecuterService), new Uri("http://localhost:8000"));
            host.AddServiceEndpoint(typeof(ExecuterService.IExecuterService), new BasicHttpBinding(), "Soap");
            ServiceEndpoint endpoint = host.AddServiceEndpoint(typeof(ExecuterService.IExecuterService), new WebHttpBinding(), "Web");
            endpoint.Behaviors.Add(new WebHttpBehavior());

            try
            {
                host.Open();

                using (WebChannelFactory<ExecuterService.IExecuterService> wcf = new WebChannelFactory<ExecuterService.IExecuterService>(new Uri("http://localhost:8000/Web")))
                {
                    ExecuterService.IExecuterService channel = wcf.CreateChannel();

                    string s;

                    Console.WriteLine("Calling EchoWithGet by HTTP GET: ");
                    s = channel.GetMessage("Hello, world");
                    Console.WriteLine("   Output: {0}", s);

                    Console.WriteLine("");
                    Console.WriteLine("This can also be accomplished by navigating to");
                    Console.WriteLine("http://localhost:8000/Web/EchoWithGet?s=Hello, world!");
                    Console.WriteLine("in a web browser while this sample is running.");

                    Console.WriteLine("");

                    Console.WriteLine("Calling EchoWithPost by HTTP POST: ");
                    s = channel.GetMessage("Hello, world");
                    Console.WriteLine("   Output: {0}", s);
                    Console.WriteLine("");
                }
                using (ChannelFactory<ExecuterService.IExecuterService> scf = new ChannelFactory<ExecuterService.IExecuterService>(new BasicHttpBinding(), "http://localhost:8000/Soap"))
                {
                    ExecuterService.IExecuterService channel = scf.CreateChannel();

                    string s;

                    Console.WriteLine("Calling EchoWithGet on SOAP endpoint: ");
                    s = channel.GetMessage("Hello, world");
                    Console.WriteLine("   Output: {0}", s);

                    Console.WriteLine("");

                    Console.WriteLine("Calling EchoWithPost on SOAP endpoint: ");
                    s = channel.GetMessage("Hello, world");
                    Console.WriteLine("   Output: {0}", s);
                    Console.WriteLine("");
                }


                Console.WriteLine("Press [Enter] to terminate");
                Console.ReadLine();
                host.Close();
            }
            catch (CommunicationException cex)
            {
                Console.WriteLine("An exception occurred: {0}", cex.Message);
                host.Abort();
            }
        }
    }
        
    }

