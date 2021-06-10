using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TruckReportLibF.Models;

namespace TruckReportClient.Converters
{
    /// <summary>
    /// Конвертер вывода значений типов отчетов на экран
    /// </summary>
    class ReportTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ReportType reportType)
            {
                switch (reportType)
                {
                    case ReportType.MoveStop:
                        value = @"Время в Движении\Стоянке";
                        break;
                    case ReportType.MessageFromObject:
                        value = "Параметры автомобиля";
                        break;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
