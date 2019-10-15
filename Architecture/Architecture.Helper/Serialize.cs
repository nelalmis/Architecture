using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Architecture.Helper
{
    public class Serialize
    {
        public static string SoapSerialize(object graph)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    var formatter = new SoapFormatter();

                    formatter.Serialize(stream, graph);

                    return Encoding.UTF8.GetString(
                              stream.GetBuffer(), 0, (int)stream.Position);
                    
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public static object SoapDeserialize(string buffer)
        {
            try
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(buffer)))
                {
                    var formatter = new SoapFormatter();

                    return formatter.Deserialize(stream);
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public static byte[] BinarySerialize(object obj)
        {
            try
            {
                if (obj == null)
                    return null;
                using (var stream = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, obj);
                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static object BinaryDeserialize(byte[] arrBytes)
        {
            try
            {
                if (arrBytes == null)
                    return null;
                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter binForm = new BinaryFormatter();
                    stream.Write(arrBytes, 0, arrBytes.Length);
                    stream.Seek(0, SeekOrigin.Begin);
                    return binForm.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string JsonSerialize<T>(T obj)
            where T:class
        {
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(ms, obj);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }
        public static object JsonDeserialize<T>(string json)
            where T:class
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            var deserializedUser = ser.ReadObject(ms);
            ms.Close();
            return deserializedUser;
        }
    }
}
