Imports System.Data.SqlClient
Public Class CSemester
    Private _mstrID As String
    Private _mstrDesc As String

    Public Sub New()
        _mstrID = ""
        _mstrDesc = ""
    End Sub

    Public Property SemesterID As String
        Get
            Return _mstrID
        End Get
        Set(strVal As String)
            _mstrID = strVal
        End Set
    End Property
    Public Property SemesterDesc As String
        Get
            Return _mstrDesc
        End Get
        Set(strVal As String)
            _mstrDesc = strVal
        End Set
    End Property

    Public Function GetReportData() As SqlDataAdapter
        Return myDB.GetDataAdapterBySP("dbo.sp_getAllSemesters", Nothing)
    End Function
End Class
