using Architecture.Common.Types;
using System;
using System.Collections.Generic;
using Architecture.Proxy;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.Base;
using Architecture.Common.Logger;

//TODO: DÜZENLENECEK!!!
namespace Architecture.Messages
{
    
    public class MessageDictionary
    {
        public static string GetMessage(string messageCode)
        {
            var response = Get(messageCode);
            return response == null ? null : response.MessageText;
        }
        public static MessageContract GetMessageContract(string messageCode)
        {
            return Get(messageCode);
        }
        private static MessageContract Get(string messageCode)
        {
            MessageRequest request = new MessageRequest();
            request.MethodName = "SelectByColumns";
            request.Contract.MessageCode = messageCode;
            var response = Proxy.Executer<MessageRequest, GenericResponse<MessageContract>>.Execute(request,null,Enums.ExecuteType.Server);
            if (!response.Success)
            {
                LogManager.Log(response.Results);
            }
            return response.Value;
        }
        public static string GetResultMessage(Result result)
        {
            throw new NotImplementedException();
        }

        public static string GetResultMessage(List<Result> resultList)
        {
            throw new NotImplementedException();
        }

        public static List<string> GetResultMessageAndDetail(List<Result> resultList)
        {
            throw new NotImplementedException();
        }        
    }
    public class System
    {
        public static string Approved
        {
            get
            {
                return "";
            }
        }
        public static string ApprovedCode { get { return @"79236#VSLongProjectName"; } }

    }
    //UserMesages.CannotSaveChanges
    //UserMesages.CheckStateChanged

}
