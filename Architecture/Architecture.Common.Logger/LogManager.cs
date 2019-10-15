using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Architecture.Common.Logger
{
    public static class LogManager
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private static StackTrace stack;
        private static LogEventInfo _logEvent;
        public static async void Log(string message)
        {
            Initialize();
            SetLogEventInfo(null,null,null, message);
            _logger.Log(_logEvent);

        }
        public static async void Log(string message, Exception ex)
        {
            Initialize();
            SetLogEventInfo(null, null, ex, message);
            _logger.Log(_logEvent);
        }
        public static async void Log(List<Exception> exList)
        {
            Initialize();
            foreach (var ex in exList)
            {
                SetLogEventInfo(null, null, ex, ex.Message);
                _logger.Log(_logEvent);
            }
        }
        public static async void Log(string userName, string message, Exception ex)
        {
            Initialize();
            SetLogEventInfo(userName,null, ex, ex.Message);
            _logger.Log(_logEvent);
        }
        public static async void Log(string userName, string hostIp, string message, Exception ex)
        {
            Initialize();
            SetLogEventInfo(userName, hostIp, ex, ex.Message);
            _logger.Log(_logEvent);
        }

        #region Private Methods
        private static void Initialize()
        {
            stack = new StackTrace();
            System.Diagnostics.Trace.CorrelationManager.ActivityId = stack.GetFrame(1).GetMethod().ReflectedType.GUID;
            _logEvent = new LogEventInfo();
        }
        private static void SetLogEventInfo(string userName,string hostIp,Exception ex, string message)
        {
            var machineName = Dns.GetHostName();             
            _logEvent.Properties.Add("hostIp", hostIp!=null?hostIp: Dns.GetHostByName(machineName).AddressList[0].ToString());
            _logEvent.Properties.Add("userName", userName!=null?userName: System.Environment.UserName);
            _logEvent.Properties.Add("errorType", ex == null ? null : GetExceptionTypeStack(ex));
            _logEvent.Properties.Add("errorSource", ex == null ? null : GetExceptionCallStack(ex));
            _logEvent.Properties.Add("errorClass", stack.GetFrame(1).GetMethod().ReflectedType.FullName);
            _logEvent.Properties.Add("errorMethod", stack.GetFrame(1).GetMethod().Name);
            _logEvent.Properties.Add("errorMessage", ex == null ? null : GetExceptionMessageStack(ex));
            _logEvent.Properties.Add("innerErrorMessage", ex == null ? null : ex.InnerException);
            _logEvent.Level = LogLevel.Error;
            _logEvent.Message = message;

        }

        private static string GetExceptionTypeStack(Exception e)
        {
            if (e.InnerException != null)
            {
                StringBuilder message = new StringBuilder();
                message.AppendLine(GetExceptionTypeStack(e.InnerException));
                message.AppendLine("   " + e.GetType().ToString());
                return (message.ToString());
            }
            else
            {
                return "   " + e.GetType().ToString();
            }
        }

        private static string GetExceptionMessageStack(Exception e)
        {
            if (e.InnerException != null)
            {
                StringBuilder message = new StringBuilder();
                message.AppendLine(GetExceptionMessageStack(e.InnerException));
                message.AppendLine("   " + e.Message);
                return (message.ToString());
            }
            else
            {
                return "   " + e.Message;
            }
        }

        private static string GetExceptionCallStack(Exception e)
        {
            if (e.InnerException != null)
            {
                StringBuilder message = new StringBuilder();
                message.AppendLine(GetExceptionCallStack(e.InnerException));
                message.AppendLine("--- Next Call Stack:");
                message.AppendLine(e.StackTrace);
                return (message.ToString());
            }
            else
            {
                return e.StackTrace;
            }
        }

        #endregion
    }
}
