using System.ServiceModel;
using System.ServiceModel.Web;
using System.Reflection;
using System.Collections.Generic;
using System;
using System.Linq;
using Architecture.Base;
using System.Runtime.Serialization;
using Architecture.Common.Types;

namespace Architecture.ExecuteService
{
    [ServiceContract(Namespace = "http://localhost:8000"//, SessionMode=SessionMode.Required
        )]
    public interface IExecuteService
    {
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/TestService/{text}")]
        [OperationContract]
        string TestService(string text);
        
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,UriTemplate ="Execute")]
        [ServiceKnownType(typeof(byte[]))]
        [OperationContract]
        byte[] Execute(byte[] serizalizedRequest, string operationName, bool isLocal, string callingAssemblyPath);
        
    }

    static class Helper
    {
        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ResponseBase));
            List<Type> types = assembly.GetExportedTypes().Where(e => e.BaseType != null && e.BaseType.BaseType == typeof(ResponseBase)).ToList();
            return types;
        }
    }

}
