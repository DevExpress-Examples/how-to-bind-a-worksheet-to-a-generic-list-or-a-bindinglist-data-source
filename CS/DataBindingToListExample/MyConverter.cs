using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBindingToListExample {
    #region #MyWeatherConverter
    public class MyWeatherConverter : IBindingRangeValueConverter {
        public object ConvertToObject(CellValue value, Type requiredType, int columnIndex) {
            if (requiredType == typeof(DateTime))
                return value.DateTimeValue;
            if (requiredType == typeof(Weather)) {
                if (requiredType == typeof(Weather)) {
                    Weather w;
                    if (Enum.TryParse(value.TextValue, out w)) return w;
                    return Weather.Undefined;
                }
                else
                    return value.TextValue;
            }
            if (requiredType == typeof(List<HourlyReport>))
                return new List<HourlyReport>();
            return value.TextValue;
        }
        public CellValue TryConvertFromObject(object value) {
            if (value is DateTime) {
                return ((DateTime)value).ToString("MMM-dd");
            }
            if (value is Weather) {
                return value.ToString();
            }
            if (value is List<HourlyReport>) {
                var hourly = (List<HourlyReport>)value;
                if (hourly.Count == 0) return "Undefined";
                var high = hourly
                    .OrderByDescending(p => p.Temperature)
                    .FirstOrDefault()
                    .Temperature;
                var low = hourly
                    .OrderBy(p => p.Temperature)
                    .FirstOrDefault()
                    .Temperature;
                return String.Format("High - {0}, Low - {1}", high, low);
            }

            return CellValue.TryCreateFromObject(value);
        }
    }
    #endregion #MyWeatherConverter
}