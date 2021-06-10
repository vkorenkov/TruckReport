using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TruckReportLibF.Action
{
    /// <summary>
    /// Класс собержит методы формирования массивов байтов и объектов из массивов байтов
    /// </summary>
    public class ByteArray
    {
        private static BinaryFormatter bf = new BinaryFormatter();

        /// <summary>
        /// Формирование массива байтов из объекта
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] GetByteArray(object obj)
        {
            byte[] reportsByteArray;

            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);

                reportsByteArray = ms.ToArray();
            }

            return reportsByteArray;
        }

        /// <summary>
        /// Возвращает объект сформированный из массива байтов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static T GetObjectFromByteArray<T>(byte[] byteArray)
        {
            T obj;

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                ms.Write(byteArray, 0, byteArray.Length);
                ms.Seek(0, SeekOrigin.Begin);
                obj = (T)bf.Deserialize(ms);
            }

            return obj;
        }
    }
}
