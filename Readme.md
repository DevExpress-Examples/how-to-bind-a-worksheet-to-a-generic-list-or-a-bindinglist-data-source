<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/DataBindingToListExample/Form1.cs) (VB: [Form1.vb](./VB/DataBindingToListExample/Form1.vb))
* [MyConverter.cs](./CS/DataBindingToListExample/MyConverter.cs) (VB: [MyConverter.vb](./VB/DataBindingToListExample/MyConverter.vb))
* [WeatherReport.cs](./CS/DataBindingToListExample/WeatherReport.cs) (VB: [WeatherReport.vb](./VB/DataBindingToListExample/WeatherReport.vb))
<!-- default file list end -->
# How to bind a worksheet to a generic list or a BindingList data source


This example demonstrates the use of a <strong>List<T> </strong>and <strong>BindingLIst<T></strong> objects as data sources to bind data to the worksheet range. <br>Use the <a href="http://help.devexpress.com/#CoreLibraries/DevExpressSpreadsheetWorksheetDataBindingCollection_BindToDataSourcetopic">WorksheetDataBindingCollection.BindToDataSource</a> method  to bind data to the range and the <a href="http://help.devexpress.com/#CoreLibraries/DevExpressSpreadsheetWorksheetDataBindingCollection_BindTableToDataSourcetopic">WorksheetDataBindingCollection.BindTableToDataSource</a> method to bind data to the worksheet table. <br>The <a href="http://help.devexpress.com/#CoreLibraries/clsDevExpressSpreadsheetExternalDataSourceOptionstopic">ExternalDataSourceOptions</a> object specifies various data binding options. A custom converter with the <a href="http://help.devexpress.com/#CoreLibraries/clsDevExpressSpreadsheetIBindingRangeValueConvertertopic">IBindingRangeValueConverter</a> interface converts weather data between the data source and a worksheet. <br>If the data source does not allow modification, the binding worksheet range also prevents modification. <br>Data binding error results in the <strong>WorksheetDataBinding.Error</strong> event and cancels data update. The event handler in this example displays a message containing the error type.<br><br><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-bind-a-worksheet-to-a-generic-list-or-a-bindinglist-data-source-t480285/17.1.3+/media/1cc2cbf3-1f7e-11e7-80bf-00155d62480c.png">

<br/>


