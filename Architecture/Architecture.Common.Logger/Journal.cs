using Architecture.Common.Types;
using NLog;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace Architecture.Common.Logger
{
    public static class Journal
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private static LogEventInfo _logEvent;
        public static void InsertJournal(string operationKey,DateTime tranDate,string transactionName, string userName, string hostIp, string hostName, short? languageId, RequestBase request)
        {            
            Initialize();
           NLog.Logger loggerJournal = NLog.LogManager.GetLogger("insertjournal");
            var req = ObjectToByteArray(request);
            
            var machineName = Dns.GetHostName();
            _logEvent.Properties.Add("Operation", operationKey);
            _logEvent.Properties.Add("TranDate", tranDate);
            _logEvent.Properties.Add("TransactionName", transactionName);
            _logEvent.Properties.Add("HasException", null);
            _logEvent.Properties.Add("Request", req);
            _logEvent.Properties.Add("Response", null);
            _logEvent.Properties.Add("ExecutionTree", null);
            _logEvent.Properties.Add("LanguageId", languageId);
            _logEvent.Properties.Add("UserName", userName != null ? userName : System.Environment.UserName);
            _logEvent.Properties.Add("HostName", hostName);
            _logEvent.Properties.Add("SystemDate", DateTime.Now);
            _logEvent.Properties.Add("HostIp", hostIp != null ? hostIp : Dns.GetHostByName(machineName).AddressList[0].ToString());
            
            _logEvent.Level = LogLevel.Info;
            loggerJournal.Log(_logEvent);
        }

        public static void UpdateJournal(string operationKey, string updateUserName,double duration, ResponseBase response)
        {
            Initialize();
            NLog.Logger loggerJournal = NLog.LogManager.GetLogger("updatejournal");
            var res = ObjectToByteArray(response);

            _logEvent.Properties.Add("OperationKey", operationKey);
            _logEvent.Properties.Add("HasException", Convert.ToInt16(!response.Success));
            _logEvent.Properties.Add("Duration", duration);
            _logEvent.Properties.Add("Response", res);
            _logEvent.Properties.Add("ExecutionTree", null);
            _logEvent.Properties.Add("UpdateUserName", updateUserName);
            _logEvent.Properties.Add("UpdateSystemDate", DateTime.Now);

            _logEvent.Level = LogLevel.Trace;

            loggerJournal.Log(_logEvent);
        }
        private static void Initialize()
        {
            _logEvent = new LogEventInfo();
        }

        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }
}
