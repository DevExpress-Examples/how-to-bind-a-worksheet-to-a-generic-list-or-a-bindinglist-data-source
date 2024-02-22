<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128613489/19.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T480285)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# How to: Bind a Worksheet to a Generic List or a BindingList Data Source

This example demonstrates the use of a [List\<T\>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-7.0) and [BindingList\<T\>](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.bindinglist-1?view=net-7.0) objects as data sources to bind data to the worksheet range.

![image](./media/1cc2cbf3-1f7e-11e7-80bf-00155d62480c.png)

## Implementation Details

Use the [WorksheetDataBindingCollection.BindToDataSource](https://docs.devexpress.com/OfficeFileAPI/devexpress.spreadsheet.worksheetdatabindingcollection.bindtodatasource.overloads) method to bind data to the range, and the [WorksheetDataBindingCollection.BindTableToDataSource](https://docs.devexpress.com/OfficeFileAPI/devexpress.spreadsheet.worksheetdatabindingcollection.bindtabletodatasource.overloads) method to bind data to the worksheet table.

The [ExternalDataSourceOptions](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Spreadsheet.ExternalDataSourceOptions) object specifies various data binding options. A custom converter with the [IBindingRangeValueConverter](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Spreadsheet.IBindingRangeValueConverter) interface converts weather data between the data source and a worksheet.

If the data source does not allow modification, the binding worksheet range also prevents modification.

Data binding error results in the [WorksheetDataBinding.Error](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Spreadsheet.WorksheetDataBindingCollection.Error) event and cancels data update. The event handler in this example displays a message containing the error type.

## Files to Review

* [Form1.cs](./CS/DataBindingToListExample/Form1.cs) (VB: [Form1.vb](./VB/DataBindingToListExample/Form1.vb))
* [MyConverter.cs](./CS/DataBindingToListExample/MyConverter.cs) (VB: [MyConverter.vb](./VB/DataBindingToListExample/MyConverter.vb))
* [WeatherReport.cs](./CS/DataBindingToListExample/WeatherReport.cs) (VB: [WeatherReport.vb](./VB/DataBindingToListExample/WeatherReport.vb))

## Documentation

* [Data Binding in WinForms Spreadsheet Control](https://docs.devexpress.com/WindowsForms/117679/controls-and-libraries/spreadsheet/data-binding)
