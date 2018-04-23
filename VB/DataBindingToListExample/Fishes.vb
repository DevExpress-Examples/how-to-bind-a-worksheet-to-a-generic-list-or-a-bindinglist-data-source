Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Xml.Serialization

Namespace DataBindingToListExample

    Public Class MyFish
        <DisplayName("ID")> _
        Public Property ID() As Integer
        <DisplayName("Fish Category")> _
        Public Property Category() As String
        <DisplayName("Fish Common Name")> _
        Public Property CommonName() As String
        <DisplayName("Fish Species Name")> _
        Public Property SpeciesName() As String
        <DisplayName("Hyperlink")> _
        Public Property Hyperlink() As String
        <DisplayName("TimeStamp")> _
        Public Property TimeStamp() As Date
        Public Sub New(ByVal id As Integer, ByVal category As String, ByVal commonName As String, ByVal speciesName As String, ByVal hyperlink As String, ByVal timeStamp As Date)
            Me.ID = id
            Me.Category = category
            Me.CommonName = commonName
            Me.SpeciesName = speciesName
            Me.Hyperlink = hyperlink
            Me.TimeStamp = timeStamp
        End Sub
    End Class
    Public Class Fish
        <DisplayName("ID")> _
        Public Property ID() As Integer
        <DisplayName("Category")> _
        Public Property Category() As String
        <DisplayName("Common Name")> _
        Public Property CommonName() As String
        <DisplayName("Notes")> _
        Public Property Notes() As String
        <DisplayName("SpeciesName")> _
        Public Property SpeciesName() As String
            Get
                Return Me.ScientificClassification.Species
            End Get
            Set(ByVal value As String)
                Me.ScientificClassification.Species = value
            End Set
        End Property
        <DisplayName("Hyperlink")> _
        Public Property Hyperlink() As String
            Get
                Return Me.ScientificClassification.Hyperlink
            End Get
            Set(ByVal value As String)
                Me.ScientificClassification.Hyperlink = value
            End Set
        End Property
        Public Property ScientificClassification() As ScientificClassification
    End Class
    Public Class ScientificClassification
        <XmlElement("Reference")> _
        Public Property Hyperlink() As String
        Public Property Kingdom() As String
        Public Property Phylum() As String
        <XmlElement("Class"), DisplayName("Class")> _
        Public Property _Class() As String
        Public Property Order() As String
        Public Property Family() As String
        Public Property Genus() As String
        Public Property Species() As String
    End Class
    Public NotInheritable Class MyFishesSource

        Private Sub New()
        End Sub


        Private Shared data_Renamed As List(Of MyFish)
        Public Shared ReadOnly Property Data() As System.Collections.ObjectModel.ReadOnlyCollection(Of MyFish)
            Get
                Dim rnd1 As New Random()
                If data_Renamed Is Nothing Then
                    Dim fLIst As List(Of Fish) = GetDataSource()
                    data_Renamed = New List(Of MyFish)()
                    For Each f As Fish In fLIst
                        data_Renamed.Add(New MyFish(f.ID, f.Category, f.CommonName, f.SpeciesName, f.Hyperlink, Date.Now.AddHours(-24 * rnd1.NextDouble())))
                    Next f
                End If
                Return data_Renamed.AsReadOnly()
            End Get
        End Property
        Private Shared Function GetDataSource() As List(Of Fish)
            Return DataSourceHelper.GetDataSouresFromXml(Of Fish)("fishes.xml", "Fishes")
        End Function
    End Class
End Namespace
