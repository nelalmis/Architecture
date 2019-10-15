using Microsoft.Web.Administration;
using System;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Web;

namespace Architecture.ExecuterService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ExecuterService" in both code and config file together.
    [GlobalErrorHandlerBehavior(typeof(GlobalErrorHandler))]
    public class ExecuterService : BaseService, IExecuterService
    {
        public string GetMessage(string value)
        {
            return "Merhaba "+value;
        }
        
    }
    public class BaseService
    {

    }
}
