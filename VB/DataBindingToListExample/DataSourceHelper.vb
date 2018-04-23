Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Xml.Serialization

Namespace DataBindingToListExample
    Public NotInheritable Class DataSourceHelper

        Private Sub New()
        End Sub

        Public Shared Function GetDataSouresFromXml(Of T As Class)(ByVal fileName As String, ByVal attribute As String) As List(Of T)
            If Not File.Exists(fileName) Then
                Return Nothing
            End If
            Using stream As Stream = File.OpenRead(fileName)
                Dim s As New XmlSerializer(GetType(List(Of T)), New XmlRootAttribute(attribute))
                Return DirectCast(s.Deserialize(stream), List(Of T))
            End Using
        End Function
        Public Shared Function GetSourceProperties(ByVal list As Object) As PropertyDescriptorCollection
            Dim typedList As ITypedList = TryCast(list, ITypedList)
            If typedList IsNot Nothing Then
                Return typedList.GetItemProperties(Nothing)
            End If
            Dim l As IList = TryCast(list, IList)
            If l IsNot Nothing AndAlso l.Count > 0 Then
                Return GetItemProperties(l(0))
            End If
            Dim enumerable As IEnumerable = TryCast(list, IEnumerable)
            If enumerable IsNot Nothing Then
                Dim enumerator As IEnumerator = enumerable.GetEnumerator()
                If enumerator.MoveNext() Then
                    Return GetItemProperties(enumerator.Current)
                End If
            End If
            Return Nothing
        End Function
        Private Shared Function GetItemProperties(ByVal item As Object) As PropertyDescriptorCollection
            Dim col As PropertyDescriptorCollection = TypeDescriptor.GetProperties(item)
            If col Is Nothing OrElse col.Count = 0 Then
                Return Nothing
            End If
            Return col
        End Function

    End Class
End Namespace
