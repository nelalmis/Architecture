﻿using Architecture.WCFService;
using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;


namespace Architecture.WCFClient
{
    public sealed class GenericProxy<TContract, TServiceContract> : IDisposable
        where TContract : class
        where TServiceContract : class
    {
        public ServiceHost host;
        private WebChannelFactory<TContract> _webChannelFactory;
        private ChannelFactory<TContract> _channelFactory;
        private TContract _channel;
        private BasicHttpBinding basicHttpBinding;
        private WSHttpBinding wsHttpBinding;
        private WebHttpBinding webHttpBinding;
       
        private static readonly string baseSoapAddress = "http://localhost:8000";
        private static readonly string baseRestAddress = "http://vaio:8000";

        private static readonly string remoteWebAddress = baseRestAddress + "/Web";
        private static readonly string remoteSoapAddress = baseSoapAddress + "/Soap";
        public GenericProxy(ServiceType type)
        {
            if (host==null|| (host!=null && host.State != CommunicationState.Opened))
            {
                if (type == ServiceType.SOAP)
                    SOAPInitialize();
                else
                    RESTInitialize();
            }
        }
        private void SOAPInitialize()
        {   
            TimeSpan ts = new TimeSpan(0, 30, 0);
            TimeSpan sendTimeOut = new TimeSpan(0, 0, 120);
            
            /*
            basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            basicHttpBinding.ReceiveTimeout = ts;
            basicHttpBinding.MaxBufferPoolSize = 2147483647;
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            basicHttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            basicHttpBinding.ReaderQuotas.MaxDepth = 2147483647;
            basicHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            basicHttpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            basicHttpBinding.SendTimeout = sendTimeOut;
            */
            wsHttpBinding = new WSHttpBinding();
            wsHttpBinding.ReceiveTimeout = ts;
            wsHttpBinding.MaxBufferPoolSize = 2147483647;
            wsHttpBinding.MaxReceivedMessageSize = 2147483647;
            wsHttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            wsHttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            wsHttpBinding.ReaderQuotas.MaxDepth = 2147483647;
            wsHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            wsHttpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            wsHttpBinding.SendTimeout = sendTimeOut;
            wsHttpBinding.TransactionFlow = true;
            
            //HOST
            host = new ServiceHost(typeof(TServiceContract), new Uri(baseSoapAddress));
            host.AddServiceEndpoint(typeof(TContract), wsHttpBinding, "Soap");

            host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = System.ServiceModel.Security.UserNamePasswordValidationMode.Custom;
            host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new Validator();

            //ServiceMetadataBehavior
            ServiceMetadataBehavior metadataBehavior = new ServiceMetadataBehavior();
            metadataBehavior.HttpGetEnabled = true;
            host.Description.Behaviors.Add(metadataBehavior);

            //ServiceBehaviorAttribute
            ServiceBehaviorAttribute behavior = host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behavior.UseSynchronizationContext = false;

            //ServiceDebugBehavior
            host.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;

            //ServiceAuthorizationBehavior
            host.Description.Behaviors.Find<ServiceAuthorizationBehavior>().ServiceAuthorizationManager = new AuthorizationManager();

            //ServiceThrottlingBehavior
            var throttlingDavranisi = new ServiceThrottlingBehavior
            {
                MaxConcurrentCalls = 16,
                MaxConcurrentInstances = Int32.MaxValue,
                MaxConcurrentSessions = 10
            };
            host.Description.Behaviors.Add(throttlingDavranisi);
            
            try
            {
                host.Open();

                _channelFactory = new ChannelFactory<TContract>(wsHttpBinding, remoteSoapAddress);
                //_channelFactory.Credentials.UserName.UserName = UserName;
                //_channelFactory.Credentials.UserName.Password = Password;
                
            }
            catch(AddressAlreadyInUseException ex)
            {
                _channelFactory = new ChannelFactory<TContract>(wsHttpBinding, remoteSoapAddress);
            }
            catch (Exception cex)
            {
                Console.WriteLine("An exception occurred: {0}", cex.Message);
                host.Abort();
                throw cex;
            }
        }
        private void RESTInitialize()
        {
            webHttpBinding = new WebHttpBinding();

            TimeSpan ts = new TimeSpan(0, 30, 0);
            TimeSpan sendTimeOut = new TimeSpan(0, 0, 120);

            webHttpBinding.ReceiveTimeout = ts;
            webHttpBinding.MaxBufferPoolSize = 2147483647;
            webHttpBinding.MaxReceivedMessageSize = 2147483647;
            webHttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            webHttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            webHttpBinding.ReaderQuotas.MaxDepth = 2147483647;
            webHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            webHttpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            webHttpBinding.SendTimeout = sendTimeOut;
            
            
            host = new ServiceHost(typeof(TServiceContract), new Uri(baseRestAddress));
            host.AddServiceEndpoint(typeof(TContract), new BasicHttpBinding(), "Soap");
            ServiceEndpoint endpoint = host.AddServiceEndpoint(typeof(TContract), webHttpBinding, "Web");
            endpoint.Behaviors.Add(new WebHttpBehavior());
            
            //ServiceMetadataBehavior
            ServiceMetadataBehavior metadataBehavior = new ServiceMetadataBehavior();
            metadataBehavior.HttpGetEnabled = true;
            metadataBehavior.HttpsGetEnabled = true;
            host.Description.Behaviors.Add(metadataBehavior);
                       
            //ServiceDebugBehavior
            host.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;

            //ServiceAuthorizationBehavior
            host.Description.Behaviors.Find<ServiceAuthorizationBehavior>().ServiceAuthorizationManager = new AuthorizationManager();

            //ServiceBehaviorAttribute
            ServiceBehaviorAttribute behavior = host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behavior.UseSynchronizationContext = false;

            //ServiceDebugBehavior
            host.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;

            //ServiceAuthorizationBehavior
            host.Description.Behaviors.Find<ServiceAuthorizationBehavior>().ServiceAuthorizationManager = new AuthorizationManager();

            //ServiceThrottlingBehavior
            var throttlingDavranisi = new ServiceThrottlingBehavior
            {
                MaxConcurrentCalls = 16,
                MaxConcurrentInstances = Int32.MaxValue,
                MaxConcurrentSessions = 10
            };
            host.Description.Behaviors.Add(throttlingDavranisi);
            
            try
            {
                host.Open();

                _webChannelFactory = new WebChannelFactory<TContract>(new Uri(remoteWebAddress));
                //_webChannelFactory.Credentials.UserName.UserName = UserName;
                //_webChannelFactory.Credentials.UserName.Password = Password;
            }
            catch (FaultException cex)
            {
                host.Abort();
                throw cex;
            }
            catch (AddressAlreadyInUseException ex)
            {
                _channelFactory = new WebChannelFactory<TContract>(new Uri(remoteWebAddress));
            }
            catch (Exception cex)
            {
                Console.WriteLine("An exception occurred: {0}", cex.Message);
                host.Abort();
                throw cex;
            }
        }
        public void Execute(Action<TContract> action)
        {
            action.Invoke(Channel);
        }
        public TResult Execute<TResult>(Func<TContract, TResult> function)
        {
            return function.Invoke(Channel);
        }
        public TContract Channel
        {
            get
            {
                if (_channel == null)
                {
                    if (_channelFactory != null)
                        _channel = _channelFactory.CreateChannel();
                    else
                    {
                        _channel = _webChannelFactory.CreateChannel();
                    }
                }

                return _channel;
            }
        }
        public void Dispose()
        {
            try
            {
                if (_channel != null)
                {
                    var currentChannel = _channel as IClientChannel;
                    if (currentChannel.State == CommunicationState.Faulted)
                    {
                        currentChannel.Abort();
                    }
                    else
                    {
                        currentChannel.Close();
                    }
                }
            }
            finally
            {
                _channel = null;
                GC.SuppressFinalize(this);
            }
        }

    }
    public enum ServiceType
    {
        SOAP = 1,
        REST = 2
    }
}
