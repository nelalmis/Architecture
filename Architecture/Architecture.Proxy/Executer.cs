using Architecture.Base;
using Architecture.Common.Types;
using Architecture.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Architecture.Proxy
{
    public static class Executer<TRequest, TResponse> 
        where TRequest : RequestBase
        where TResponse : ResponseBase
    {
        public static TResponse Execute(TRequest request)
        {
            return ExecuteWithWCF(request, Assembly.GetCallingAssembly(), Enums.ExecuteType.Server);
        }
        public static TResponse Execute(TRequest request,Enums.ExecuteType executeType)
        {
            return ExecuteWithWCF(request, Assembly.GetCallingAssembly(),executeType);
        }
        public static TResponse Execute(TRequest request, Assembly callingAssembly,Enums.ExecuteType executeType)
        {
            callingAssembly = callingAssembly == null ? Assembly.GetCallingAssembly() : callingAssembly;
 
            return ExecuteWithWCF(request, callingAssembly, executeType);
        }
        public static MultipleResponse MultipleExecute(List<RequestBase> requestList, Assembly callingAssembly)
        {
            var tip = typeof(MultipleResponse);
            var returnObject = (MultipleResponse)Activator.CreateInstance(tip);
            returnObject.ResponseList = new List<ResponseBase>();
            foreach (var item in requestList)
            {
                var response = Execute((TRequest)item, callingAssembly, Enums.ExecuteType.Server);
                if (!response.Success)
                {
                    returnObject.Results.AddRange(response.Results);
                    break;
                }
                returnObject.ResponseList.Add((ResponseBase)response);
            }
            return returnObject;
        }
        public static async Task<TResponse> ExecuteAsync(TRequest request)
        {
            return (TResponse)Execute(request, Assembly.GetCallingAssembly(), Enums.ExecuteType.Server);
        }
        public static async Task<MultipleResponse> MultipleExecuteAsync(List<RequestBase> requestList, Assembly callingAssembly)
        {
            return MultipleExecute(requestList, callingAssembly);
        }
        private static TResponse ExecuteWithWCF(TRequest request, Assembly callingAssembly, Enums.ExecuteType? executeType)
        {
            callingAssembly = callingAssembly == null ? Assembly.GetCallingAssembly() : callingAssembly;
            executeType = (executeType == null || executeType == Enums.ExecuteType.Server) ? Enums.ExecuteType.Server : Enums.ExecuteType.Local;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            request.OperationKey = Guid.NewGuid().ToString("N");
            var machineName = Dns.GetHostName();
           
            //TODO: BURALAR AÇILACAK
            //Journal.InsertJournal(request.OperationKey, DateTime.Now, request.ToString(), System.Environment.UserName, Dns.GetHostByName(machineName).AddressList[0].ToString(), machineName, null, request);

            callingAssembly = callingAssembly == null ? Assembly.GetCallingAssembly() : callingAssembly;
            var returnObject = WCFConnect(request, callingAssembly, (Enums.ExecuteType)executeType);
            returnObject.OperationKey = request.OperationKey;
            
            watch.Stop();
            var durationsn = watch.Elapsed.TotalSeconds;

            //Journal.UpdateJournal(request.OperationKey, System.Environment.UserName, durationsn, returnObject);

            return returnObject;

        }
        private static TResponse WCFConnect(TRequest request, Assembly callingAssembly, Enums.ExecuteType environment)
        {
            var tip = typeof(TResponse);
            var returnObject = (TResponse)Activator.CreateInstance(tip);
            
            var className = request.GetType().Name.Replace("Request", "");
            var methodName = request.MethodName;
            // TResponse ResponseBase türünde GenericResponse<T> şeklinde 
            // Tresponse türünde göndermek isitoyorum
            var tempOperationName = callingAssembly.ManifestModule.Name.ToString().Replace(Constants.View, Constants.GreeterClassName);
            string[] splitList = tempOperationName.Split('.');
            string operationName = "";
            for (int i = 0; i < splitList.Count()-2; i++)
            {
                operationName += splitList[i];
                operationName += ".";
            }
            operationName += className;
            operationName += ".dll";
            //var projectName = callingAssembly.ManifestModule.Name.Split('.').FirstOrDefault();

            string callingAssemblyPath = callingAssembly.CodeBase.Replace("file:///", "");
            bool isLocal = environment == Enums.ExecuteType.Local ? true : false;
            WCFClient.ExecuteServiceClient client = new WCFClient.ExecuteServiceClient();

            var responseWcf = client.Execute<TRequest>(request, operationName, isLocal, callingAssemblyPath);
            if (!responseWcf.Success)
            {
                returnObject.Results.AddRange(responseWcf.Results);
                return returnObject;
            }
            return (TResponse)responseWcf;
            //returnObject =(TResponse)FactoryHelper.InitializeResponseBase(responseWcf);
            //return returnObject;
            
        }
        public static string TestService(string message)
        {
            WCFClient.ExecuteServiceClient client = new WCFClient.ExecuteServiceClient();
            return client.TestService(message);
        }

    }
    public static class WebExecuter<TRequest, TResponse>
        where TRequest : RequestBase
        where TResponse : ResponseBase
    {   
        public static TResponse Execute(TRequest request, Assembly callingAssembly, Enums.ExecuteType? executeType)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            //TODO: OperationKey üretilmesi lazımdır.
            request.OperationKey = Guid.NewGuid().ToString("N").ToUpper().Substring(0, 28);
            var machineName = Dns.GetHostName();

            //TODO: BURALAR AÇILACAK
            //Journal.InsertJournal(request.OperationKey, DateTime.Now, request.ToString(), System.Environment.UserName, Dns.GetHostByName(machineName).AddressList[0].ToString(), machineName, null, request);
            callingAssembly = callingAssembly == null ? Assembly.GetCallingAssembly() : callingAssembly;
            var returnObject = executeType == Enums.ExecuteType.Local ? ExecLocal(request, callingAssembly) : ExecServer(request, callingAssembly);

            watch.Stop();
            var durationsn = watch.Elapsed.TotalSeconds;

            //Journal.UpdateJournal(request.OperationKey, System.Environment.UserName, durationsn, returnObject);

            return returnObject;

        }
        public static TResponse Execute(TRequest request)
        {
            return Execute(request, Assembly.GetCallingAssembly(), Enums.ExecuteType.Server);

        }
        public static TResponse Execute(TRequest request, Assembly callingAssembly)
        {
            return Execute(request, callingAssembly, Enums.ExecuteType.Server);

        }
        private static TResponse ExecLocal(TRequest request, Assembly callingAssembly)
        {
            ExecutionDataContext edc = new ExecutionDataContext();
            ObjectHelper objectHelper = new ObjectHelper(edc);
            object[] parameters = { objectHelper, request };

            var tip = typeof(TResponse);
            var returnValueAtMethod = (TResponse)Activator.CreateInstance(tip);

            try
            {
                var className = request.GetType().Name.Replace("Request", "");

                var projectName = callingAssembly.ManifestModule.Name.Split('.').FirstOrDefault();

                DirectoryInfo di = new DirectoryInfo(callingAssembly.CodeBase.Replace("file:///", ""));
                var parentFile = di.Parent;
                var isExists = parentFile.GetDirectories("*" + Constants.GreeterClassName + "*").Any();

                var prevFile = di;
                while (!isExists && di.Root != parentFile)
                {
                    isExists = parentFile.GetDirectories("*" + Constants.GreeterClassName + "*").Any();
                    prevFile = parentFile;
                    parentFile = parentFile.Parent;
                }

                FileInfo[] operationDll = null;
                if (isExists)
                {
                    var folderOperation = prevFile.GetDirectories("*" + Constants.GreeterClassName + "*").FirstOrDefault();
                    if (folderOperation != null)
                    {
                        var binFile = folderOperation.GetDirectories("bin").FirstOrDefault();
                        if (binFile != null)
                        {
                            var debugFile = binFile.GetDirectories("Debug").FirstOrDefault();
                            if (debugFile != null)
                            {
                                operationDll = debugFile.GetFiles("*" + Constants.GreeterClassName + "*" + ".dll");
                                if (operationDll != null || operationDll.Count() > 0)
                                {
                                    var assembly = Assembly.LoadFrom(operationDll.FirstOrDefault().FullName);
                                    if (assembly != null)
                                    {
                                        var typeClass = assembly.GetTypes().Where(u => u.IsClass == true && u.Name == className).FirstOrDefault();
                                        if (typeClass != null)
                                        {
                                            var instance = Activator.CreateInstance(typeClass);
                                            var targetMethod = typeClass.GetMethod(request.MethodName);
                                            if (targetMethod != null)
                                            {
                                                BindingFlags flags = BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance;
                                                try
                                                {
                                                    returnValueAtMethod = (TResponse)targetMethod.Invoke(instance, flags, null, parameters, null);  
                                                    // returnValueAtMethod = targetMethod.Invoke(instance,parameters);
                                                }
                                                catch (Exception e)
                                                {
                                                    returnValueAtMethod.Results.Add(e);
                                                    return returnValueAtMethod;
                                                }
                                                finally
                                                {

                                                }
                                            }
                                            else { returnValueAtMethod.Results.Add(new KeyNotFoundException("Metod bulunamadı.")); }
                                        }
                                        else { returnValueAtMethod.Results.Add(new EntryPointNotFoundException(className + " sınıfı bulunamadı")); }
                                    }
                                    else { returnValueAtMethod.Results.Add(new FileLoadException(Constants.GreeterClassName + " dll yüklenemedi.")); }
                                }
                                else { returnValueAtMethod.Results.Add(new DllNotFoundException(Constants.GreeterClassName + " dll bulunamadı.")); }
                            }
                            else { returnValueAtMethod.Results.Add(new FileNotFoundException("debug")); }
                        }
                        else { returnValueAtMethod.Results.Add(new FileNotFoundException("bin")); }
                    }
                    else { returnValueAtMethod.Results.Add(new FileNotFoundException("bin")); }
                }
                else { returnValueAtMethod.Results.Add(new DllNotFoundException(Constants.GreeterClassName)); }
            }
            catch
            {
                returnValueAtMethod.Results.Add(new FileNotFoundException("Yanlış klasörde arama yapılıyor."));
            }
            return returnValueAtMethod;
        }
        private static TResponse ExecServer(TRequest request, Assembly callingAssembly)
        {
            var className = request.GetType().Name.Replace("Request", "");
            string operationName = "";
            if (callingAssembly.Modules.FirstOrDefault().ToString().Contains(Constants.Root))
            {
                operationName = callingAssembly.Modules.FirstOrDefault().ToString().Replace(Constants.View, Constants.GreeterClassName);
            }
            else
            {

                var splitlist = callingAssembly.Modules.FirstOrDefault().ToString().Split('.').ToList();
                var count = splitlist.Count - 2;
                var lit = splitlist.Take(count).ToList();

                for (int i = 0; i < lit.Count(); i++)
                {
                    operationName += lit[i] + ".";
                }
                operationName += "dll";
            }
            string locationServerDll = Constants.ServerDllPath + @"\"; ;
            string operationLocationDll = @locationServerDll + operationName.Replace(Constants.View, Constants.GreeterClassName);

            var tip = typeof(TResponse);
            var returnValueAtMethod = (TResponse)Activator.CreateInstance(tip);

            if (!File.Exists(operationLocationDll))
            {
                returnValueAtMethod.Results.Add(new KeyNotFoundException("Server Operation dll bulunamadı."));
                return returnValueAtMethod;
            }
            ExecutionDataContext edc = new ExecutionDataContext();
            ObjectHelper objectHelper = new ObjectHelper(edc);
            object[] parameters = { objectHelper, request };

            /* TODO: Kullanılabilir
           if (!returnValueAtMethod.Success)
           {
               var tip = typeof(TResponse);
               var val = (TResponse)Activator.CreateInstance(tip);
               val.Results.AddRange(returnValueAtMethod.Results);
               return val;
           }*/

            //ResponseBase returnValueAtMethod = new ResponseBase();           
            try
            {
                var assembly = Assembly.LoadFrom(operationLocationDll);
                if (assembly != null)
                {
                    var typeClass = assembly.GetTypes().Where(u => u.IsClass == true && u.Name == className).FirstOrDefault();
                    if (typeClass != null)
                    {
                        var instance = Activator.CreateInstance(typeClass);
                        var targetMethod = typeClass.GetMethod(request.MethodName);
                        if (targetMethod != null)
                        {
                            BindingFlags flags = BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance;
                            try
                            {
                                returnValueAtMethod = (TResponse)targetMethod.Invoke(instance, flags, null, parameters, null);
                                // returnValueAtMethod = targetMethod.Invoke(instance,parameters);
                            }
                            catch (Exception e)
                            {
                                returnValueAtMethod.Results.Add(e);
                            }
                        }
                        else { returnValueAtMethod.Results.Add(new KeyNotFoundException("Metod bulunamadı.")); }
                    }
                    else { returnValueAtMethod.Results.Add(new EntryPointNotFoundException(className + " sınıfı bulunamadı")); }
                }
                else { returnValueAtMethod.Results.Add(new FileLoadException(Constants.GreeterClassName + " dll yüklenemedi.")); }
            }
            catch (Exception e)
            {
                returnValueAtMethod.Results.Add(e);
            }

            return returnValueAtMethod;
        }
        public static MultipleResponse MultipleExecute(List<RequestBase> requestList, Assembly callingAssembly)
        {
            var tip = typeof(MultipleResponse);
            var returnObject = (MultipleResponse)Activator.CreateInstance(tip);
            returnObject.ResponseList = new List<ResponseBase>();
            foreach (var item in requestList)
            {
                var response = Execute((TRequest)item, callingAssembly, Enums.ExecuteType.Server);
                if (!response.Success)
                {
                    returnObject.Results.AddRange(response.Results);
                    break;
                }
                returnObject.ResponseList.Add((ResponseBase)response);
            }
            return returnObject;
        }
        public static async Task<TResponse> ExecuteAsync(TRequest request)
        {
            return (TResponse)Execute(request, Assembly.GetCallingAssembly(), Enums.ExecuteType.Server);
        }
        public static async Task<MultipleResponse> MultipleExecuteAsync(List<RequestBase> requestList, Assembly callingAssembly)
        {
            return MultipleExecute(requestList, callingAssembly);
        }
        
    }

}
