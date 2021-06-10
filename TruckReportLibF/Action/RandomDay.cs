using System;

namespace TruckReportLibF.Action
{
    /// <summary>
    /// Класс содержит метод, который возвращает случайную дату
    /// </summary>
    public class RandomDay
    {
        /// <summary>
        /// Возвращает случайную дату
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static DateTime GetRandomDay(Random r)
        {
            DateTime start = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(r.Next(range));
        }
    }
}
