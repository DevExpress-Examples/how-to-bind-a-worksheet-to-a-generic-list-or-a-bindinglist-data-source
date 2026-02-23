<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128613489/16.2.6%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T480285)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# How to bind a worksheet to a generic list or a BindingList data source

This example uses `List<T>` and `BindingList<T>` objects as data sources to bind data to a worksheet range.

Use the [WorksheetDataBindingCollection.BindToDataSource](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Spreadsheet.WorksheetDataBindingCollection.BindToDataSource.overloads) method to bind data to a range, and the [WorksheetDataBindingCollection.BindTableToDataSource](http://help.devexpress.com/#CoreLibraries/DevExpressSpreadsheetWorksheetDataBindingCollection_BindTableToDataSourcetopic) method to bind data to a worksheet table.

The [ExternalDataSourceOptions](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Spreadsheet.ExternalDataSourceOptions) object specifies various data binding options. A custom converter that implements the [IBindingRangeValueConverter](http://help.devexpress.com/#CoreLibraries/clsDevExpressSpreadsheetIBindingRangeValueConvertertopic) interface converts weather data between the data source and the worksheet.

If the data source does not allow modification, the bound worksheet range is also read-only.

If a data binding error occurs, the `WorksheetDataBinding.Error` event is raised and the data update is canceled. In this example, the event handler displays a message that indicates the error type.

![Data Binding Example](https://raw.githubusercontent.com/DevExpress-Examples/how-to-bind-a-worksheet-to-a-generic-list-or-a-bindinglist-data-source-t480285/16.2.6+/media/1cc2cbf3-1f7e-11e7-80bf-00155d62480c.png)

<!-- default file list -->
## Files to Look At

* [Form1.cs](./CS/DataBindingToListExample/Form1.cs) (VB: [Form1.vb](./VB/DataBindingToListExample/Form1.vb))
* [MyConverter.cs](./CS/DataBindingToListExample/MyConverter.cs) (VB: [MyConverter.vb](./VB/DataBindingToListExample/MyConverter.vb))
* [WeatherReport.cs](./CS/DataBindingToListExample/WeatherReport.cs) (VB: [WeatherReport.vb](./VB/DataBindingToListExample/WeatherReport.vb))
<!-- default file list end -->

<!-- feedback -->

## Does This Example Address Your Development Requirements/Objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=how-to-bind-a-worksheet-to-a-generic-list-or-a-bindinglist-data-source&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=how-to-bind-a-worksheet-to-a-generic-list-or-a-bindinglist-data-source&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
