using Architecture.Common.Types;
using Architecture.ExecuteService;
using Architecture.Helper;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Transactions;

namespace Architecture.WCFClient
{
    public class ExecuteServiceClient
    {
        private static GenericProxy<IExecuteService,ExecuteService.ExecuteService> Proxy { get; set; }
        public ExecuteServiceClient()
        {
           if(Proxy==null)   
           Proxy = new GenericProxy<IExecuteService, ExecuteService.ExecuteService>(ServiceType.SOAP);
        }
        public ResponseBase Execute<TRequest>(TRequest request, string operationName, bool isLocal, string callingAssemblyPath)
        {
            ResponseBase returnObject = new ResponseBase();
           
            try
            {
                var serializedRequest = Serialize.BinarySerialize(request);

                string authHeaer = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("nevzat@firm.com" + ":" + "123456"));

                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    try
                    {
                        using (new OperationContextScope((IContextChannel)Proxy.Channel))
                        {

                            WebOperationContext.Current.OutgoingRequest.Headers.Add("Authorization", "Basic " + authHeaer);

                            var response = Proxy.Execute<byte[]>(u => u.Execute(serializedRequest, operationName, isLocal, callingAssemblyPath));
                            var responseWcf = (ResponseBase)Serialize.BinaryDeserialize(response);
                            if (!responseWcf.Success)
                            {
                                returnObject.Results.AddRange(responseWcf.Results);
                                return returnObject;
                            }
                            ts.Complete();
                            return responseWcf;
                        }
                    }catch(Exception ex)
                    {
                        ts.Dispose();
                        returnObject.Results.Add(ex);
                        return returnObject;
                    }
                }
            }
            catch (Exception ex)
            {
                returnObject.Results.Add(ex);
                return returnObject;
            }
        }

        public string TestService(string message)
        {
            
            string response="";
            try
            {   
                string authHeaer = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("nevzat@firm.com" + ":" + "123456"));
                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    using (new OperationContextScope((IContextChannel)Proxy.Channel))
                    {
                        try
                        {
                            WebOperationContext.Current.OutgoingRequest.Headers.Add("Authorization", "Basic " + authHeaer);

                            response = Proxy.Execute<string>(u => u.TestService(message));
                            ts.Complete();
                        }catch(Exception ex)
                        {
                            ts.Dispose();
                        }
                    }
                }

                return response;
            }catch(Exception ex)
            {
                return ex.Message;
            }
        }
        
    }
}
