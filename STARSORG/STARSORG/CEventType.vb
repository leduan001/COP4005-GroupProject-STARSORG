Imports System.Data.SqlClient
Public Class CEventType
    Private _mstrID As String
    Private _mstrDesc As String

    Public Sub New()
        _mstrID = ""
        _mstrDesc = ""
    End Sub

    Public Property EventTypeID As String
        Get
            Return _mstrID
        End Get
        Set(strVal As String)
            _mstrID = strVal
        End Set
    End Property
    Public Property EventTypeDesc As String
        Get
            Return _mstrDesc
        End Get
        Set(strVal As String)
            _mstrDesc = strVal
        End Set
    End Property

    Public Function GetReportData() As SqlDataAdapter
        Return myDB.GetDataAdapterBySP("dbo.sp_getAllEventTypes", Nothing)
    End Function
End Class
