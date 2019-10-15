using Architecture.Common.Types;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;

namespace Architecture.Proxy
{
    //TODO: Client cache, container.exe ‘nin proxy execute aşamasında gelen requesti  uygulama sunucusuna göndermeyip bellekteki cachede ilgili veri varsa cacheden response nesnesini okuması  yoksa application serverlara gidip dönen response’u cache’e eklemesi olayıdır.  Bu şekilde round trip olayının azaltılması hedeflenmektedir. 

    public class CacheRepository<TRequest>
        where TRequest : RequestBase
    {
        public ResponseBase GetCache(string key)
        {
            try
            {
                return (ResponseBase)MemoryCache.Default[key];

            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public void SetCache(string key, object data, int cacheTime)
        {
            try
            {
                if (data == null)
                    return;
                if (IsExist(key))
                {
                    Remove(key);
                }
                else
                {
                    var policy = new CacheItemPolicy();
                    policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
                    CacheItem ci = new CacheItem(key, data);
                    MemoryCache.Default.AddOrGetExisting(ci, policy);
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        private bool IsExist(string key)
        {
            return MemoryCache.Default.Any(q => q.Key == key);
        }
        private void Remove(string key)
        {
            MemoryCache.Default.Remove(key);
        }
    }

    //TODO:Cache yapısı Request üzerinde çalışır.  bir request sınıfının cacheden çalışmasını sağlamak için ICacheable dan miras almak yeterlidir. Başka bir hiç değişiklik yapmaya gerek yoktur. 
    /// <summary>
    /// 
    /// </summary>
    public interface ICacheable
    {
        /// <summary>
        /// generates the unique cache key for request.
        /// </summary>
        /// <returns></returns>
        string GenerateCacheKey();

        /// <summary>
        /// if true this request do not use cache and goes to appserver. but returned data is always cached.
        /// </summary>
        bool DoNotUseCache { get; set; }
    }

    public class CacheKey
    {
        private readonly string _format = "{0}-{1}";
        private readonly string _generatedKey;

        public CacheKey(EntitiesEnum entity, int entityId)
        {
            _generatedKey = string.Format(_format, entity.ToString(), entityId.ToString());
        }

        public static CacheKey New(EntitiesEnum entity, int entityId)
        {
            return new CacheKey(entity, entityId);
        }

        public override string ToString()
        {
            return _generatedKey;
        }
    }

    public enum EntitiesEnum
    {
        Categories,
        Products
    }
    public abstract class CacheProvider
    {
        public static int CacheDuration = 60;
        public static CacheProvider Instance { get; set; }

        public abstract object Get(CacheKey key);
        public abstract void Set(CacheKey key, object value);
        public abstract bool IsExist(CacheKey key);
        public abstract void Remove(CacheKey key);
    }
    public class DefaultCacheProvider : CacheProvider
    {
        private ObjectCache _cache = null;
        private CacheItemPolicy _policy = null;

        public DefaultCacheProvider()
        {
            Trace.WriteLine("Cache Initialize Oldu!");

            _cache = MemoryCache.Default;
            _policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(CacheDuration),
                RemovedCallback = new CacheEntryRemovedCallback(CacheRemovedCallback)
            };
        }

        private static void CacheRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            Trace.WriteLine("----------Cache Expire Oldu----------");
            Trace.WriteLine("Key : " + arguments.CacheItem.Key);
            Trace.WriteLine("Value : " + arguments.CacheItem.Value.ToString());
            Trace.WriteLine("RemovedReason : " + arguments.RemovedReason);
            Trace.WriteLine("-------------------------------------");
        }

        public override object Get(CacheKey key)
        {
            object retVal = null;

            try
            {
                retVal = _cache.Get(key.ToString());
            }
            catch (Exception e)
            {
                Trace.WriteLine("Hata : CacheProvider.Get()\n" + e.Message);
                throw new Exception("Cache Get sırasında bir hata oluştu!", e);
            }

            return retVal;
        }

        public override void Set(CacheKey key, object value)
        {
            try
            {
                Trace.WriteLine("Cache Setleniyor. Key : " + key.ToString());
                _cache.Set(key.ToString(), value, _policy);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Hata : CacheProvider.Set()\n" + e.Message);
                throw new Exception("Cache Set sırasında bir hata oluştu!", e);
            }
        }

        public override bool IsExist(CacheKey key)
        {
            return _cache.Any(q => q.Key == key.ToString());
        }

        public override void Remove(CacheKey key)
        {
            _cache.Remove(key.ToString());
        }
    }

















    /* İlgili requestimiz ICacheable’dan türetilir. Burada önemli olan GenerateCacheKey metodunun doğru bir şekilde kodlanmasıdır. Bu metodun geriye, requestin   üzerinde taşıdığı tüm parametrelerden oluşan bir key ,dönmesi gerekmektedir. Bu yapıldığı takdirde artık bu request cacheden çalışacaktır.

        Key Mantığı Nasıl Çalışır : Key mantığı 2 parametreden oluşur. Zaten her request tipi ayrı tutulduğu için request.MethodName değeri ve GenerateCacheKey ile üretilen key tekil bir anahtar üretir. Request.MethodName keye cache repository tarafından eklenmektedir.

        Cache Key = request.MethodName + request.GenerateCacheKey();

        Bazı durumlarda cache kullanılmadan doğrudan application server’a gitmek gerekebilir. Bu durumda DoNotUseCache = true atanarak cacheden okunmaz. Ama sonuçlar her zaman cache’e yazılır.

        NOT : Bu geliştirilen Cache mantığı ile ParameterComponent, WorkgroupComponent, ProductComponent komponentlerinin requestleri  ICacheable’dan türetilerek cacheden çalışacak şekilde düzenlenmiştir

 
     * */
}

