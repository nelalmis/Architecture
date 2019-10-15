using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Architecture.ExecuterService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IExecuterService" in both code and config file together.
    [ServiceContract]
    public interface IExecuterService:IBaseService
    {
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,UriTemplate ="GetMessage")]
        [OperationContract]
        string GetMessage(string value);
    }
    public interface IBaseService { }

    [DataContract]
    public class TContract
    {
        [DataMember]
        public object Value { get; set; }
    }
}
