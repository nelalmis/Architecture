using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Architecture.WCFDeneme
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPaymentService" in both code and config file together.
    [ServiceContract]
    public interface IPaymentService
    {
        [OperationContract]
        [WebInvoke(BodyStyle =WebMessageBodyStyle.Wrapped,Method ="GET",RequestFormat =WebMessageFormat.Json,ResponseFormat =WebMessageFormat.Json,UriTemplate ="/PayBill/{payId}")]
        string PayBill(string payId);
    }
}
