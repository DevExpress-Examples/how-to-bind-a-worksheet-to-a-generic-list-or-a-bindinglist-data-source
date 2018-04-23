using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBindingToListExample {
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm {
        WorksheetDataBinding weatherDataBinding;
        WorksheetDataBinding fishesDataBinding;
        
        public Form1() {
            InitializeComponent();
            #region #ErrorSubscribe
            spreadsheetControl1.Document.Worksheets[0].DataBindings.Error += DataBindings_Error;
            #endregion #ErrorSubscribe
        }
        #region #ErrorHandler
        private void DataBindings_Error(object sender, DataBindingErrorEventArgs e) {
            MessageBox.Show(String.Format("Error at worksheet.Rows[{0}].\n The error is : {1}", e.RowIndex, e.ErrorType.ToString()), "Binding Error");
        }
        #endregion #ErrorHandler

        #region Weather Report
        private void barBtnBindWeather_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            BindWeatherReport(MyWeatherReportSource.Data);
        }

        private void barBtnWeatherBindingList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            BindWeatherReport(MyWeatherReportSource.DataAsBindingList);
        }

        private void BindWeatherReport(object weatherDatasource) {
            if (this.weatherDataBinding != null)
                spreadsheetControl1.Document.Worksheets[0].DataBindings.Remove(this.weatherDataBinding);
            #region #BindTheList
            // Specify the binding options.
            ExternalDataSourceOptions dsOptions = new ExternalDataSourceOptions();
            dsOptions.ImportHeaders = true;
            dsOptions.CellValueConverter = new MyWeatherConverter();
            dsOptions.SkipHiddenRows = true;
            // Bind the data source to the worksheet range.
            Worksheet sheet = spreadsheetControl1.Document.Worksheets[0];
            WorksheetDataBinding sheetDataBinding = sheet.DataBindings.BindToDataSource(weatherDatasource, 2, 1, dsOptions);
            #endregion #BindTheList
            this.weatherDataBinding = sheetDataBinding;
            // Highlight the binding range.
            this.weatherDataBinding.Range.FillColor = Color.Lavender;
            // Adjust column width.
            spreadsheetControl1.Document.Worksheets[0].Range.FromLTRB(1, 1, this.weatherDataBinding.Range.RightColumnIndex, this.weatherDataBinding.Range.BottomRowIndex).AutoFitColumns();
        }

        private void barBtnAddWeatherReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            MyWeatherReportSource.DataAsBindingList.Insert(1, new WeatherReport() {
                Date = new DateTime(1776, 2, 29),
                Weather = Weather.Sunny,
                HourlyReport = MyWeatherReportSource.GenerateRandomHourlyReport()
            });
        }
        #endregion Weather Report

        #region My Fishes
        private void barBtnBindMyFishes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            // Specify the binding options.
            ExternalDataSourceOptions dsOptions = new ExternalDataSourceOptions();
            dsOptions.ImportHeaders = true;
            // Bind the data source to the worksheet range.
            this.fishesDataBinding = spreadsheetControl1.Document.Worksheets[0].DataBindings.BindToDataSource(MyFishesSource.Data, 2, 5, dsOptions);
            // Highlight the binding range.
            this.fishesDataBinding.Range.FillColor = Color.LightCyan;
            // Adjust column width.
            spreadsheetControl1.Document.Worksheets[0].Range.FromLTRB(5, 2, this.fishesDataBinding.Range.RightColumnIndex, this.fishesDataBinding.Range.BottomRowIndex).AutoFitColumns();
        }
        #endregion My Fishes

        private void barBtnUnbind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            foreach (WorksheetDataBinding wdb in spreadsheetControl1.Document.Worksheets[0].DataBindings)
                wdb.Range.FillColor = Color.Empty;
            weatherDataBinding = null;
            fishesDataBinding = null;
            spreadsheetControl1.Document.Worksheets[0].DataBindings.Clear();
        }

        private void barBtnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (this.fishesDataBinding != null) {
                this.fishesDataBinding.Range.FillColor = Color.Empty;
                spreadsheetControl1.Document.Worksheets[0].DataBindings.Remove(this.fishesDataBinding);
            }
            int columnCount = DisplayColumnHeaders(MyFishesSource.Data, 2,5);
            spreadsheetControl1.Document.Worksheets[0].Import(MyFishesSource.Data, 3, 5);
            spreadsheetControl1.Document.Worksheets[0].Range.FromLTRB(5, 2, 5 + columnCount, 2 + MyFishesSource.Data.Count).AutoFitColumns();
        }
        private int DisplayColumnHeaders(object dataSource, int topRow, int leftColumn) {
            // Get column headers from the data source  
            PropertyDescriptorCollection pdc = DataSourceHelper.GetSourceProperties(dataSource);
            for (int i = 0; i < pdc.Count; i++) {
                PropertyDescriptor pd = pdc[i];
                spreadsheetControl1.ActiveWorksheet[topRow, i + leftColumn].Value = pd.DisplayName;
            }
            return pdc.Count;
        }

        private void barBtnBindingInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            #region #GetDataBindings
            Worksheet sheet = spreadsheetControl1.Document.Worksheets.ActiveWorksheet;
            var bindings = sheet.DataBindings.GetDataBindings(sheet.Selection);
            string s = "No data bindings found";
            if (bindings.Count != 0) { 
                  s = "The selected range overlaps the following data bindings:\r\n";
            foreach (WorksheetDataBinding binding in bindings)
                s += String.Format("{0}\r\n", binding.Range.ToString());
            }
            MessageBox.Show(s);
            #endregion #GetDataBindings
        }
    }
}
