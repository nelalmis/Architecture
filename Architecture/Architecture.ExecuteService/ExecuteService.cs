using Architecture.Base;
using Architecture.Common.Types;
using Architecture.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Transactions;

namespace Architecture.ExecuteService
{

    [ServiceBehavior(TransactionIsolationLevel = IsolationLevel.Serializable, TransactionTimeout = "00:02:00", TransactionAutoCompleteOnSessionClose = false, ReleaseServiceInstanceOnTransactionComplete = false)]
    public class ExecuteService : ObjectHelperBase, IExecuteService
    {
        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        public string TestService(string text)
        {
            Transaction.Current.TransactionCompleted += Current_TransactionCompleted;

            return "Test başarılı! " + text;
        }

        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        public byte[] Execute(byte[] serizalizedRequest, string operationName, bool isLocal, string callingAssemblyPath)
        {
            var returnObject = new ResponseBase();
            try
            {
                var response = ExecuteCore(serizalizedRequest, operationName, isLocal, callingAssemblyPath);
                return Serialize.BinarySerialize(response);

            }
            catch (Exception ex)
            {
                returnObject.Results.Add(ex);
                return Serialize.BinarySerialize(returnObject);
            }
        }

        private void Current_TransactionCompleted(object sender, TransactionEventArgs e)
        {

        }

        #region Helper
        private static ResponseBase ExecuteCore(byte[] serizalizedRequest, string operationName, bool isLocal, string callingAssemblyPath)
        {
            ResponseBase returnObject = new ResponseBase();
           
            try
            {
                var request = (RequestBase)Serialize.BinaryDeserialize(serizalizedRequest);

                var className = request.GetType().Name.Replace("Request", "");
                var methodName = request.MethodName;
                //var projectName = callingAssembly.ManifestModule.Name.Split('.').FirstOrDefault();

                FileInfo operationDllFileInfo = null;
                if (isLocal)
                {
                    //string callingAssemblyPath = callingAssembly.CodeBase.Replace("file:///", "");
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
                return responseInvoke;
            }
            catch (Exception ex)
            {
                returnObject.Results.Add(ex);
                return returnObject;
            }
        }
        private static GenericResponse<FileInfo> FindOperationDllFileInLocal(string operationDllName, string callingAssemblyPath)
        {
            string operationDllFolderName = operationDllName.Replace(".dll", "");
            GenericResponse<FileInfo> returnObject = FactoryHelper.InitializeGenericResponse<FileInfo>("FindOperationDllFileInLocal");
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
            try
            {
                string locationServerDll = Constants.ServerDllPath + @"\"; ;
                string operationLocationDll = @locationServerDll + operationName.Replace(Constants.View, Constants.GreeterClassName);
                
                if (!File.Exists(operationLocationDll))
                {
                    returnObject.Results.Add(new KeyNotFoundException(string.Format("Server {0} Operation dll bulunamadı.",operationLocationDll)));
                    return returnObject;
                }

                FileInfo fileInfo = new FileInfo(operationLocationDll);

                returnObject.Value = fileInfo;
            }
            catch (Exception ex)
            {
                returnObject.Results.Add(ex);
            }
            return returnObject;
        }
        private static GenericResponse<Assembly> LoadAssembly(string assemblyfile)
        {
            GenericResponse<Assembly> returnObject = FactoryHelper.InitializeGenericResponse<Assembly>("LoadAssembly");
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
            GenericResponse<Type> returnObject = FactoryHelper.InitializeGenericResponse<Type>("GetOperationClassType");
            try
            {
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
            }
            catch (Exception ex)
            {
                returnObject.Results.Add(ex);
            }
            return returnObject;
        }
        private static GenericResponse<object> CreateClassInstance(Type classType)
        {
            GenericResponse<object> returnObject = FactoryHelper.InitializeGenericResponse<object>("CreateClassInstance");
            try
            {
                var instance = Activator.CreateInstance(classType);
                if (instance != null)
                {
                    returnObject.Value = instance;
                }
                else
                {
                    returnObject.Results.Add(new Exception(classType.Name + " sınıfına ait instance üretilemedi."));
                }
            }
            catch (Exception ex)
            {
                returnObject.Results.Add(ex);
            }
            return returnObject;
        }
        private static GenericResponse<MethodInfo> GetMethodInfo(Type classType, string methodName)
        {
            GenericResponse<MethodInfo> returnObject = FactoryHelper.InitializeGenericResponse<MethodInfo>("MethodInfo");
            try
            {
                var targetMethod = classType.GetMethod(methodName);
                if (targetMethod != null)
                {
                    returnObject.Value = targetMethod;
                }
                else { returnObject.Results.Add(new KeyNotFoundException("Metod bulunamadı.")); }
            }
            catch (Exception ex)
            {
                returnObject.Results.Add(ex);
            }
            return returnObject;
        }
        private static ResponseBase InvokeMethod(MethodInfo targetMethod, object classInstance, object[] parameters)
        {
            ResponseBase returnObject = new ResponseBase();

            try
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance;
                returnObject = (ResponseBase)targetMethod.Invoke(classInstance, flags, null, parameters, null);
                
            }
            catch (Exception ex)
            {
                returnObject.Results.Add(ex);
                return returnObject;
            }
            return returnObject;
        }

        #endregion Helper
    }
}
