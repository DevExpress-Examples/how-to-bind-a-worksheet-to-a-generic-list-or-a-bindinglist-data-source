Imports DevExpress.Spreadsheet
Imports System
Imports System.Data
Imports System.Linq
Imports System.Windows.Forms
Imports System.Collections.Generic

Namespace DataBindingToListExample
    Partial Public Class Form1
        Inherits DevExpress.XtraBars.Ribbon.RibbonForm

        Public Sub New()
            InitializeComponent()
#Region "#DataBindingErrorHandling"
            AddHandler spreadsheetControl1.Document.Worksheets(0).DataBindings.Error, AddressOf DataBindings_Error
#End Region ' #DataBindingErrorHandling
        End Sub
        #Region "#DataBindingErrorHandler"
        Private Sub DataBindings_Error(ByVal sender As Object, ByVal e As DataBindingErrorEventArgs)
            MessageBox.Show(String.Format("Error at worksheet.Rows[{0}]." & ControlChars.Lf & " The error is : {1}", e.RowIndex, e.ErrorType.ToString()), "Binding Error")
        End Sub
        #End Region ' #DataBindingErrorHandler

        Private Sub barBtnBindWeather_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles barBtnBindWeatherListToTable.ItemClick
            BindWeatherReportToTable(MyWeatherReportSource.Data, spreadsheetControl1.ActiveCell)
        End Sub

        Private Sub barBtnWeatherBindingList_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles barBtnWeatherBindingListToRange.ItemClick
            BindWeatherReportToRange(MyWeatherReportSource.DataAsBindingList, spreadsheetControl1.ActiveCell)
        End Sub

        Private Sub barBtnBindWeatherListToTableUsingTableCollection_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles barBtnBindWeatherListToTableUsingTablecCollection.ItemClick
            BindWeatherReportToFixedTable(MyWeatherReportSource.Data, spreadsheetControl1.Selection)
        End Sub

#Region "#BindingListBoundToRange"
        Private Sub BindWeatherReportToRange(ByVal weatherDatasource As Object, ByVal bindingRange As CellRange)
            Dim sheet As Worksheet = spreadsheetControl1.Document.Worksheets(0)

            ' Check for range conflicts.
            Dim dataBindingConflicts = sheet.DataBindings.Where(
                Function(binding)
                    Return (binding.Range.RightColumnIndex >= bindingRange.LeftColumnIndex) OrElse (binding.Range.BottomRowIndex >= bindingRange.TopRowIndex)
                End Function)
            If dataBindingConflicts.Count() > 0 Then
                MessageBox.Show("Cannot bind the range to data." & ControlChars.CrLf & "The worksheet contains other binding ranges which may conflict.", "Range Conflict")
                Return
            End If

            ' Specify the binding options.
            Dim dsOptions As New ExternalDataSourceOptions()
            dsOptions.ImportHeaders = True
            dsOptions.CellValueConverter = New MyWeatherConverter()
            dsOptions.SkipHiddenRows = True

            ' Bind the data source to the worksheet range.
            Dim sheetDataBinding As WorksheetDataBinding = sheet.DataBindings.BindToDataSource(weatherDatasource, bindingRange, dsOptions)

            ' Adjust the column width.
            sheetDataBinding.Range.AutoFitColumns()
        End Sub
#End Region ' #BindingListBoundToRange

#Region "#ListBoundToTable"
        Private Sub BindWeatherReportToTable(ByVal weatherDatasource As Object, ByVal bindingRange As CellRange)
            Dim sheet As Worksheet = spreadsheetControl1.Document.Worksheets(0)

            ' Remove all data bindings bound to the specified data source.
            sheet.DataBindings.Remove(weatherDatasource)

            ' Specify the binding options.
            Dim dsOptions As New ExternalDataSourceOptions()
            dsOptions.ImportHeaders = True
            dsOptions.CellValueConverter = New MyWeatherConverter()
            dsOptions.SkipHiddenRows = True

            ' Create a table and bind the data source to the table.
            Try
                Dim sheetDataBinding As WorksheetTableDataBinding = sheet.DataBindings.BindTableToDataSource(weatherDatasource, bindingRange, dsOptions)
                sheetDataBinding.Table.Style = spreadsheetControl1.Document.TableStyles(BuiltInTableStyleId.TableStyleMedium14)

                ' Adjust the column width.
                sheetDataBinding.Range.AutoFitColumns()
            Catch e As Exception
                MessageBox.Show(e.Message, "Binding Exception")
            End Try
        End Sub
#End Region ' #ListBoundToTable

#Region "#ListBoundToFixedTableUsingTableCollectionAdd"
        Private Sub BindWeatherReportToFixedTable(ByVal weatherDatasource As Object, ByVal selection As CellRange)
            Dim sheet As Worksheet = spreadsheetControl1.Document.Worksheets(0)

            ' Remove all data bindings bound to the specified data source.
            sheet.DataBindings.Remove(weatherDatasource)

            ' Specify the binding options.
            Dim dsOptions As New ExternalDataSourceOptions()
            dsOptions.ImportHeaders = True
            dsOptions.CellValueConverter = New MyWeatherConverter()
            dsOptions.SkipHiddenRows = True

            ' Create a table and bind the data source to the table.
            Try
                Dim boundTable As Table = sheet.Tables.Add(weatherDatasource, selection, dsOptions)
                boundTable.Style = spreadsheetControl1.Document.TableStyles(BuiltInTableStyleId.TableStyleMedium15)

                ' Adjust the column width.
                boundTable.Range.AutoFitColumns()
            Catch e As Exception
                MessageBox.Show(e.Message, "Binding Exception")
            End Try
        End Sub
#End Region ' #ListBoundToFixedTableUsingTableCollectionAdd

        Private Sub barBtnAddWeatherReport_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles barBtnAddWeatherReport.ItemClick
            MyWeatherReportSource.DataAsBindingList.Insert(1, New WeatherReport() With {.Date = New Date(1776, 2, 29), .Weather = Weather.Sunny, .HourlyReport = MyWeatherReportSource.GenerateRandomHourlyReport()})
        End Sub

        Private Sub barBtnUnbind_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles barBtnUnbind.ItemClick
            spreadsheetControl1.Document.Worksheets(0).DataBindings.Clear()
        End Sub

        Private Sub barBtnBindingInfo_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles barBtnBindingInfo.ItemClick
#Region "#GetDataBindings"
            Dim sheet As Worksheet = spreadsheetControl1.Document.Worksheets.ActiveWorksheet
            Dim bindings = sheet.DataBindings.GetDataBindings(sheet.Selection)
            Dim message As String = "No data bindings found"
            If bindings.Count <> 0 Then
                message = "The selected range contains the following data bindings:" & ControlChars.CrLf
                For Each binding As WorksheetDataBinding In bindings
                    message &= String.Format("Binding {0}" & ControlChars.CrLf, binding.Range)
                Next binding
            End If
            MessageBox.Show(message)
#End Region ' #GetDataBindings
        End Sub

        Private Sub btnReload_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnReload.ItemClick
            MyWeatherReportSource.Reload()
        End Sub
    End Class
End Namespace
