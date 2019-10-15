using Architecture.Base;
using Architecture.Common.Types;
using Architecture.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Architecture.Proxy
{
    class YedekExecute<TRequest, TResponse>
        where TRequest : RequestBase
        where TResponse : ResponseBase
    {
        public YedekExecute()
        {
        }
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
        private static TResponse NewExecute(TRequest request, Assembly callingAssembly, Enums.ExecuteType environment)
        {
            var tip = typeof(TResponse);
            var returnObject = (TResponse)Activator.CreateInstance(tip);
            /*
            if (callingAssembly.Modules.FirstOrDefault().ToString().Contains(Constants.Root))
            {
                operationName = callingAssembly.Modules.FirstOrDefault().ToString().Replace(Constants.View, Constants.GreeterClassName);
            }
            */
            var operationName = callingAssembly.ManifestModule.Name.ToString().Replace(Constants.View, Constants.GreeterClassName);
            var className = request.GetType().Name.Replace("Request", "");
            var methodName = request.MethodName;
            //var projectName = callingAssembly.ManifestModule.Name.Split('.').FirstOrDefault();

            FileInfo operationDllFileInfo = null;
            if (environment == Enums.ExecuteType.Local)
            {
                string callingAssemblyPath = callingAssembly.CodeBase.Replace("file:///", "");
                var responseOperationFileInfoLocal = FindOperationDllFileInLocal(operationName, callingAssemblyPath);
                if (!responseOperationFileInfoLocal.Success)
                {
                    returnObject.Results.AddRange(responseOperationFileInfoLocal.Results);
                    return returnObject;
                }
                operationDllFileInfo = responseOperationFileInfoLocal.Value;
            }
            else
            {
                var responseOperationFileInfoServer = FindOperationDllFileInServer(operationName);
                if (!responseOperationFileInfoServer.Success)
                {
                    returnObject.Results.AddRange(responseOperationFileInfoServer.Results);
                    return returnObject;
                }
                operationDllFileInfo = responseOperationFileInfoServer.Value;
            }

            var responseAssembly = LoadAssembly(operationDllFileInfo.FullName);
            if (!responseAssembly.Success)
            {
                returnObject.Results.AddRange(responseAssembly.Results);
                return returnObject;
            }
            var responseClass = GetOperationClassType(responseAssembly.Value, className);
            if (!responseClass.Success)
            {
                returnObject.Results.AddRange(responseClass.Results);
                return returnObject;
            }
            var responseInstance = CreateClassInstance(responseClass.Value);
            if (!responseInstance.Success)
            {
                returnObject.Results.AddRange(responseInstance.Results);
                return returnObject;
            }
            var responseMetHodInfo = GetMethodInfo(responseClass.Value, request.MethodName);
            if (!responseMetHodInfo.Success)
            {
                returnObject.Results.AddRange(responseMetHodInfo.Results);
                return returnObject;
            }

            ExecutionDataContext edc = new ExecutionDataContext();
            ObjectHelper objectHelper = new ObjectHelper(edc);
            object[] parameters = { objectHelper, request };

            var responseInvoke = InvokeMethod(responseMetHodInfo.Value, responseInstance.Value, parameters);
            if (!responseInvoke.Success)
            {
                returnObject.Results.AddRange(responseInvoke.Results);
                return returnObject;
            }
            //returnObject.Value = responseInvoke.Value;

            return returnObject;
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

        #region Executer Helper
        private static GenericResponse<FileInfo> FindOperationDllFileInLocal(string operationDllName, string callingAssemblyPath)
        {
            string operationDllFolderName = operationDllName.Replace(".dll", "");
            GenericResponse<FileInfo> returnObject = FactoryHelper.InitializeGenericResponse<FileInfo>("GetMethodInfo");
            try
            {
                DirectoryInfo di = new DirectoryInfo(callingAssemblyPath);
                var parentFile = di.Parent;
                var isExists = parentFile.GetDirectories("*" + operationDllFolderName + "*").Any();

                var prevFile = di;
                while (!isExists && di.Root != parentFile)
                {
                    isExists = parentFile.GetDirectories("*" + operationDllFolderName + "*").Any();
                    prevFile = parentFile;
                    parentFile = parentFile.Parent;
                }

                FileInfo operationDll = null;
                if (isExists)
                {
                    var folderOperation = prevFile.GetDirectories("*" + operationDllFolderName + "*").FirstOrDefault();
                    if (folderOperation != null)
                    {
                        var binFile = folderOperation.GetDirectories("bin").FirstOrDefault();
                        if (binFile != null)
                        {
                            var debugFile = binFile.GetDirectories("Debug").FirstOrDefault();
                            if (debugFile != null)
                            {
                                operationDll = debugFile.GetFiles("*" + operationDllName + "*").FirstOrDefault();
                                if (operationDll != null)
                                {
                                    returnObject.Value = operationDll;
                                    return returnObject;
                                }
                                else { returnObject.Results.Add(new DllNotFoundException(operationDllName + " bulunamadı.")); }
                            }
                            else { returnObject.Results.Add(new FileNotFoundException("debug")); }
                        }
                        else { returnObject.Results.Add(new FileNotFoundException("bin")); }
                    }
                    else { returnObject.Results.Add(new FileNotFoundException("bin")); }
                }
                else { returnObject.Results.Add(new DllNotFoundException(operationDllName)); }
            }
            catch
            {
                returnObject.Results.Add(new FileNotFoundException("Yanlış klasörde arama yapılıyor."));
            }
            return returnObject;

        }
        private static GenericResponse<FileInfo> FindOperationDllFileInServer(string operationName)
        {
            GenericResponse<FileInfo> returnObject = FactoryHelper.InitializeGenericResponse<FileInfo>("FindOperationDllFileInServer");

            string locationServerDll = Constants.ServerDllPath + @"\"; ;
            string operationLocationDll = @locationServerDll + operationName.Replace(Constants.View, Constants.GreeterClassName);

            if (!File.Exists(operationLocationDll))
            {
                returnObject.Results.Add(new KeyNotFoundException("Server Operation dll bulunamadı."));
                return returnObject;
            }

            FileInfo fileInfo = new FileInfo(operationLocationDll);

            returnObject.Value = fileInfo;

            return returnObject;
        }
        private static GenericResponse<Assembly> LoadAssembly(string assemblyfile)
        {
            GenericResponse<Assembly> returnObject = FactoryHelper.InitializeGenericResponse<Assembly>("GetMethodInfo");
            try
            {
                var assembly = Assembly.LoadFrom(assemblyfile);
                if (assembly != null)
                {
                    returnObject.Value = assembly;
                }
                else
                {
                    returnObject.Results.Add(new FileLoadException(Constants.GreeterClassName + " dll yüklenemedi."));
                }
            }
            catch (Exception ex)
            {
                returnObject.Results.Add(ex);
                return returnObject;
            }
            return returnObject;
        }
        private static GenericResponse<Type> GetOperationClassType(Assembly operationAssembly, string className)
        {
            GenericResponse<Type> returnObject = FactoryHelper.InitializeGenericResponse<Type>("GetMethodInfo");
            if (operationAssembly != null)
            {
                var typeClass = operationAssembly.GetTypes().Where(u => u.IsClass == true && u.Name == className).FirstOrDefault();
                if (typeClass != null)
                {
                    returnObject.Value = typeClass;
                }
                else { returnObject.Results.Add(new EntryPointNotFoundException(className + " sınıfı bulunamadı")); }
            }
            else { returnObject.Results.Add(new ArgumentNullException(Constants.GreeterClassName + " dll null olamaz.")); }

            return returnObject;
        }
        private static GenericResponse<object> CreateClassInstance(Type classType)
        {
            GenericResponse<object> returnObject = FactoryHelper.InitializeGenericResponse<object>("GetMethodInfo");

            var instance = Activator.CreateInstance(classType);
            if (instance != null)
            {
                returnObject.Value = instance;
            }
            else
            {
                returnObject.Results.Add(new Exception(classType.Name + " sınıfına ait instance üretilemedi."));
            }

            return returnObject;
        }
        private static GenericResponse<MethodInfo> GetMethodInfo(Type classType, string methodName)
        {
            GenericResponse<MethodInfo> returnObject = FactoryHelper.InitializeGenericResponse<MethodInfo>("GetMethodInfo");
            var targetMethod = classType.GetMethod(methodName);
            if (targetMethod != null)
            {
                returnObject.Value = targetMethod;
            }
            else { returnObject.Results.Add(new KeyNotFoundException("Metod bulunamadı.")); }
            return returnObject;
        }
        private static TResponse InvokeMethod(MethodInfo targetMethod, object classInstance, object[] parameters)
        {
            var tip = typeof(TResponse);
            var returnObject = (TResponse)Activator.CreateInstance(tip);
            try
            {

                BindingFlags flags = BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance;
                returnObject = (TResponse)targetMethod.Invoke(classInstance, flags, null, parameters, null);
            }
            catch (Exception ex)
            {
                returnObject.Results.Add(ex);
                return returnObject;
            }
            return returnObject;
        }
        
        #endregion Executer Helper
    }
}
