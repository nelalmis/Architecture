using Architecture.Common.Types;
using Architecture.Proxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Caching;

namespace Architecture.Test.Base
{
    [TestClass]
    public class ProxyTest
    {
        string Key;
        CityContract contract = new CityContract()
        {
            CityId = 1,
            CityName = "denemeCache"
        };
        public ObjectCache cache = MemoryCache.Default;
       
        
        [TestMethod]
        public void AddCache()
        {
            CityRequest request = new CityRequest();
            request.MethodName = "Select";
            request.Contract = contract;
            Key = request.MethodName + request.GenerateCacheKey();
            Architecture.Proxy.CacheRepository<CityRequest> cac = new CacheRepository<CityRequest>();
            cac.SetCache(Key,contract, 50);
            var log = System.Diagnostics.EventLog.GetEventLogs();
        }

        [TestMethod]
        public void GetCache()
        {
            CityRequest request = new CityRequest();
            request.MethodName = "Select";
            Key = request.MethodName + request.GenerateCacheKey();
            Architecture.Proxy.CacheRepository<CityRequest> cac = new CacheRepository<CityRequest>();
            var response=cac.GetCache(Key);

        }
    }
    [Serializable]
    public partial class CityRequest : RequestBase, ICacheable
    {
        public CityContract Contract { get; set; }
        public bool DoNotUseCache { get; set; } = true;
        public CityRequest()
        {
            Contract = new CityContract();
        }

        public string GenerateCacheKey()
        {
            return DateTime.Now.ToShortDateString();
        }
    }
}
