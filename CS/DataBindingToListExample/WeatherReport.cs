using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataBindingToListExample {
    #region #WeatherReport
    public class WeatherReport {
        [DisplayName("Date")]
        public DateTime Date { get; set; }
        [DisplayName("Weather Condition")]
        public Weather Weather { get; set; }
        [DisplayName("Max and Min Temperature")]
        public List<HourlyReport> HourlyReport { get; set; }
    }

    public class HourlyReport {
        public int Hour { get; set; }
        public int Temperature { get; set; }
    }

    public enum Weather {
        Sunny,
        Cloudy,
        Windy,
        Gloomy,
        Foggy,
        Misty,
        Rainy,
        Undefined
    }
    #endregion #WeatherReport
    public static class MyWeatherReportSource {
        private static Random rand = new System.Random();
        static List<WeatherReport> data;
        static BindingList<WeatherReport> dataBindingList;

        public static List<WeatherReport> Data
        {
            get {
                if (data == null) {
                    data = GetReport();
                }
                return data;
            }
        }
        public static BindingList<WeatherReport> DataAsBindingList
        {
            get {
                if (dataBindingList == null) {
                    dataBindingList = new BindingList<WeatherReport>(Data);
                }
                return dataBindingList;
            }
        }
        public static List<WeatherReport> GetReport() {
            var report = new List<WeatherReport>();

            report.Add(new WeatherReport() {
                Date = DateTime.Today,
                Weather = Weather.Rainy,
                HourlyReport = GenerateRandomHourlyReport()
            });

            report.Add(new WeatherReport() {
                Date = DateTime.Today.AddDays(-1),
                Weather = Weather.Cloudy,
                HourlyReport = GenerateRandomHourlyReport()
            });

            report.Add(new WeatherReport() {
                Date = DateTime.Today.AddDays(-2),
                Weather = Weather.Sunny,
                HourlyReport = GenerateRandomHourlyReport()
            });

            report.Add(new WeatherReport() {
                Date = DateTime.Today.AddDays(-3),
                Weather = Weather.Gloomy,
                HourlyReport = GenerateRandomHourlyReport()
            });
            return report;
        }
        public static List<HourlyReport> GenerateRandomHourlyReport() {
            var report = new List<HourlyReport>();

            for (int i = 0; i < 24; i++) {
                var hourlyReport = new HourlyReport();
                hourlyReport.Hour = i;
                hourlyReport.Temperature = rand.Next(30);
                report.Add(hourlyReport);
            }
            return report;
        }
        public static void Reload() {
            data = GetReport();
        }
    }
}
