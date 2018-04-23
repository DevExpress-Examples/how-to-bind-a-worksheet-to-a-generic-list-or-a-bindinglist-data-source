Imports DevExpress.Spreadsheet
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace DataBindingToListExample
    #Region "#MyWeatherConverter"
    Public Class MyWeatherConverter
        Implements IBindingRangeValueConverter

        Public Function ConvertToObject(ByVal value As CellValue, ByVal requiredType As Type, ByVal columnIndex As Integer) As Object Implements IBindingRangeValueConverter.ConvertToObject
            If requiredType Is GetType(Date) Then
                Return value.DateTimeValue
            End If
            If requiredType Is GetType(Weather) Then
                If requiredType Is GetType(Weather) Then
                    Dim w As Weather = Nothing
                    If System.Enum.TryParse(value.TextValue, w) Then
                        Return w
                    End If
                    Return Weather.Undefined
                Else
                    Return value.TextValue
                End If
            End If
            If requiredType Is GetType(List(Of HourlyReport)) Then
                Return New List(Of HourlyReport)()
            End If
            Return value.TextValue
        End Function
        Public Function TryConvertFromObject(ByVal value As Object) As CellValue Implements IBindingRangeValueConverter.TryConvertFromObject
            If TypeOf value Is Date Then
                Return DirectCast(value, Date).ToString("MMM-dd")
            End If
            If TypeOf value Is Weather Then
                Return value.ToString()
            End If
            If TypeOf value Is List(Of HourlyReport) Then
                Dim hourly = DirectCast(value, List(Of HourlyReport))
                If hourly.Count = 0 Then
                    Return "Undefined"
                End If
                Dim high = hourly.OrderByDescending(Function(p) p.Temperature).FirstOrDefault().Temperature
                Dim low = hourly.OrderBy(Function(p) p.Temperature).FirstOrDefault().Temperature
                Return String.Format("High - {0}, Low - {1}", high, low)
            End If

            Return CellValue.TryCreateFromObject(value)
        End Function
    End Class
    #End Region ' #MyWeatherConverter
End Namespace