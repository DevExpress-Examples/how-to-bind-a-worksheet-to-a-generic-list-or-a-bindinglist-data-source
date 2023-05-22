using DevExpress.Spreadsheet;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DataBindingToListExample {
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm {      
        public Form1() {
            InitializeComponent();
            #region #DataBindingErrorHandling
            spreadsheetControl1.Document.Worksheets[0].DataBindings.Error += DataBindings_Error;
            #endregion #DataBindingErrorHandling
        }
        #region #DataBindingErrorHandler
        private void DataBindings_Error(object sender, DataBindingErrorEventArgs e) {
            MessageBox.Show(String.Format("Error at worksheet.Rows[{0}].\n The error is : {1}", e.RowIndex, e.ErrorType.ToString()), "Binding Error");
        }
        #endregion #DataBindingErrorHandler

        private void barBtnBindWeather_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            BindWeatherReportToTable(MyWeatherReportSource.Data, spreadsheetControl1.ActiveCell);
        }

        private void barBtnWeatherBindingList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            BindWeatherReportToRange(MyWeatherReportSource.DataAsBindingList, spreadsheetControl1.ActiveCell);
        }

        private void barBtnBindWeatherListToTableUsingTableCollection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            BindWeatherReportToFixedTable(MyWeatherReportSource.Data, spreadsheetControl1.Selection);
        }

        #region #BindingListBoundToRange
        private void BindWeatherReportToRange(object weatherDatasource, CellRange bindingRange) {
            Worksheet sheet = spreadsheetControl1.Document.Worksheets[0];

            // Check for range conflicts.
            var dataBindingConflicts = sheet.DataBindings.
                Where(binding => (binding.Range.RightColumnIndex >= bindingRange.LeftColumnIndex) 
                || (binding.Range.BottomRowIndex >= bindingRange.TopRowIndex)) ;
            if (dataBindingConflicts.Count() > 0) {
                MessageBox.Show("Cannot bind the range to data.\r\nThe worksheet contains other binding ranges which may conflict.", "Range Conflict");
                return;
            }
            
            // Specify the binding options.
            ExternalDataSourceOptions dsOptions = new ExternalDataSourceOptions();
            dsOptions.ImportHeaders = true;
            dsOptions.CellValueConverter = new MyWeatherConverter();
            dsOptions.SkipHiddenRows = true;

            // Bind the data source to the worksheet range.
            WorksheetDataBinding sheetDataBinding = sheet.DataBindings.BindToDataSource(weatherDatasource, bindingRange, dsOptions);

            // Adjust the column width.
            sheetDataBinding.Range.AutoFitColumns();
        }
        #endregion #BindingListBoundToRange

        #region #ListBoundToTable
        private void BindWeatherReportToTable(object weatherDatasource, CellRange bindingRange) {
            Worksheet sheet = spreadsheetControl1.Document.Worksheets[0];

            // Remove all data bindings bound to the specified data source.
            sheet.DataBindings.Remove(weatherDatasource);

            // Specify the binding options.
            ExternalDataSourceOptions dsOptions = new ExternalDataSourceOptions();
            dsOptions.ImportHeaders = true;
            dsOptions.CellValueConverter = new MyWeatherConverter();
            dsOptions.SkipHiddenRows = true;

            // Create a table and bind the data source to the table.
            try {
                WorksheetTableDataBinding sheetDataBinding = sheet.DataBindings.BindTableToDataSource(weatherDatasource, bindingRange, dsOptions);
                sheetDataBinding.Table.Style = spreadsheetControl1.Document.TableStyles[BuiltInTableStyleId.TableStyleMedium14];

                // Adjust the column width.
                sheetDataBinding.Range.AutoFitColumns();
            }
            catch(Exception e) {
                MessageBox.Show(e.Message, "Binding Exception");
            }
        }
        #endregion #ListBoundToTable

        #region #ListBoundToFixedTableUsingTableCollectionAdd
        private void BindWeatherReportToFixedTable(object weatherDatasource, CellRange selection) {
            Worksheet sheet = spreadsheetControl1.Document.Worksheets[0];

            // Remove all data bindings bound to the specified data source.
            sheet.DataBindings.Remove(weatherDatasource);

            // Specify the binding options.
            ExternalDataSourceOptions dsOptions = new ExternalDataSourceOptions();
            dsOptions.ImportHeaders = true;
            dsOptions.CellValueConverter = new MyWeatherConverter();
            dsOptions.SkipHiddenRows = true;

            // Create a table and bind the data source to the table.
            try {
                Table boundTable = sheet.Tables.Add(weatherDatasource, selection, dsOptions);
                boundTable.Style = spreadsheetControl1.Document.TableStyles[BuiltInTableStyleId.TableStyleMedium15];

                // Adjust the column width.
                boundTable.Range.AutoFitColumns();
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, "Binding Exception");
            }
        }
        #endregion #ListBoundToFixedTableUsingTableCollectionAdd

        private void barBtnAddWeatherReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            MyWeatherReportSource.DataAsBindingList.Insert(1, new WeatherReport() {
                Date = new DateTime(1776, 2, 29),
                Weather = Weather.Sunny,
                HourlyReport = MyWeatherReportSource.GenerateRandomHourlyReport()
            });
        }

        private void barBtnUnbind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            spreadsheetControl1.Document.Worksheets[0].DataBindings.Clear();
        }

        private void barBtnBindingInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            #region #GetDataBindings
            Worksheet sheet = spreadsheetControl1.Document.Worksheets.ActiveWorksheet;
            var bindings = sheet.DataBindings.GetDataBindings(sheet.Selection);
            string message = "No data bindings found";
            if (bindings.Count != 0) {
                message = "The selected range contains the following data bindings:\r\n";
            foreach (WorksheetDataBinding binding in bindings)
                    message += String.Format("Binding {0}\r\n", binding.Range);
            }
            MessageBox.Show(message);
            #endregion #GetDataBindings
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            MyWeatherReportSource.Reload();
        }


    }
}
