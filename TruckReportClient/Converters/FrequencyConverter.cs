using System;
using System.Globalization;
using System.Windows.Data;
using TruckReportLibF.Models;

namespace TruckReportClient.Converters
{
    /// <summary>
    /// Конвертер вывода значений переодичности на экран
    /// </summary>
    class FrequencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Frequency frequencyType)
            {
                switch (frequencyType)
                {
                    case Frequency.day:
                        value = "Раз в сутки";
                        break;
                    case Frequency.week:
                        value = "Раз в неделю";
                        break;
                    case Frequency.month:
                        value = "Раз в месяц";
                        break;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
