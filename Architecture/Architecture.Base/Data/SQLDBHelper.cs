using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Architecture.Base
{
    public sealed class SQLDBHelper
    {
        public string GetStringValue(object reader)
        {
            return Convert.ToString(reader);
        }
        public Int32 GetInt32Value(object reader)
        {
            return Convert.ToInt32(reader);
        }
        public Int32? GetInt32NullableValue(object reader)
        {
            return reader == System.DBNull.Value ? default(Int32) : Convert.ToInt32(reader);
        }
        public Int16 GetInt16Value(object reader)
        {
            return Convert.ToInt16(reader);
        }
        public Int16? GetInt16NullableValue(object reader)
        {
            return reader == System.DBNull.Value ? default(Int16?) : Convert.ToInt16(reader);
        }
        public Int64 GetInt64Value(object reader)
        {
            return Convert.ToInt64(reader);
        }
        public Int64? GetInt64NullableValue(object reader)
        {
            return reader == System.DBNull.Value ? default(Int64?) : Convert.ToInt64(reader);
        }        
        public Boolean GetBooleanValue(object reader)
        {
            return Convert.ToBoolean(reader);
        }
        public Boolean? GetBooleanNullableValue(object reader)
        {
            return reader == System.DBNull.Value ? default(Boolean?) : Convert.ToBoolean(reader);
        }        
        public Double GetDoubleValue(object reader)
        {
            return Convert.ToDouble(reader);
        }
        public Double? GetDoubleNullableValue(object reader)
        {
            return reader == System.DBNull.Value ? default(Double?) : Convert.ToDouble(reader);
        }
        public Decimal GetDecimalValue(object reader)
        {
            return Convert.ToDecimal(reader);
        }
        public Decimal? GetDecimalNullableValue(object reader)
        {
            return reader == System.DBNull.Value ? default(Decimal?) :  Convert.ToDecimal(reader);
        }
        public DateTime GetDateTimeValue(object reader)
        {
            return Convert.ToDateTime(reader);
        }
        public DateTime? GetDateTimeNullableValue(object reader)
        {
            return reader == System.DBNull.Value ? default(DateTime?) : Convert.ToDateTime(reader);
        }
        public Byte GetByteValue(object reader)
        {
            return Convert.ToByte(reader);
        }
        public Byte? GetByteNullableValue(object reader)
        {
            return reader == System.DBNull.Value ? default(Byte?) : Convert.ToByte(reader);
        }
        public byte[] GetBinaryValue(object reader)
        {
            return reader == System.DBNull.Value ? default(byte[]) : (byte[])reader;
        }
        public byte?[] GetBinaryNullableValue(object reader)
        {
            return reader == System.DBNull.Value ? default(byte?[]):(byte?[])reader;
        }
        public static byte[] BinarySerialize(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
        public static object BinaryDeserialize(byte[] arrBytes)
        {
            if (arrBytes == null)
                return null;
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            return binForm.Deserialize(memStream);
        }

        /// <summary>
        /// Parola işlemleri için kullanılır.
        /// Girilin parolayı şifreler.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string MD5DoUTF8(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //md5 nesnesi türettik.
            byte[] bsifre = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text));
            //texti(girilen parolayı) Encoding.UTF8 in GetBytes() methodu ile bir byte dizisine çevirdik.
            StringBuilder sb = new StringBuilder();
            // string builder sınıfından bir nesne türetip , byte dizimizdeki değerleri 
            // Append methodu yardımıyla bir string ifadeye çevirdik.
            foreach (var by in bsifre)
            {
                //x2 burda string'e çevirirken vermesini istediğimiz format.
                //çıktısında göreceğimiz gibi sayılar ve harflerden oluşucaktır.
                sb.Append(by.ToString("x2").ToLower());
            }
            //oluşturduğumuz string ifadeyi geri döndürdük.
            return sb.ToString();
        }

        /// <summary>
        /// girilen bir veri ile MD5DoUTF8 methodu ile sifreli hale getirilen verinin karşılaştırmasını yapar.
        /// </summary>
        /// <param name="girilen"></param>
        /// <param name="sifreli"></param>
        /// <returns></returns>
        public bool ComparePasswordAndMD5Password(string girilen, string sifreli)
        {
            //Sifreli daha önce sifrelemiş olduğumuz parola. Burda veritabanı kullanacak olursanız
            //Sifreli değeri veritabanından çekeceğiniz kullanıcı parolası olacak.
            string girileniSifrele = MD5DoUTF8(girilen);
            // Kullanıcının giriş yapmak için girdiği parolayı biraz önce yazdığımız method ile
            // Hash haline getirdik.S
            StringComparer sc = StringComparer.OrdinalIgnoreCase;
            // StringComparer adından da anlaşıldığı gibi string karşılaştırması yapan bir sınıftır.
            // OrdinalIgnoreCase ile eşitse 0 değilse 1 döndürsün dedik .
            //sc.Compare() methodu ile iki ifadeyi karşılaştırdık. 
            if (0 == sc.Compare(girileniSifrele, sifreli))
            { //ifadeler uyuşuyorsa burası
                return true;
            }
            else
            {//ifadeler uyuşmuyorsa burası
                return false;
            }
        }
        public enum Databases
        {
            Company,
            DB_RNA,
            NORTHWND,
            ARCHITECTURE,
            ARCHITECTURELOG

        }
    }
}
