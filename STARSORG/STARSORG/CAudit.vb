Imports System.Data.SqlClient
Public Class CAudit
    Private _mstrPID As String
    Private _mdatACCESSTIMESTAMP As Date
    Private _mintSUCCEESS As Integer

    'Public Sub New()
    '    _mdatACCESSTIMESTAMP = Date.Today
    'End Sub

#Region "Exposed Properties"
    Public Property PID As String
        Get
            Return _mstrPID
        End Get
        Set(strVal As String)
            _mstrPID = strVal
        End Set
    End Property
    Public Property ACCESSTIMESTAMP As Date
        Get
            Return _mdatACCESSTIMESTAMP
        End Get
        Set(datVal As Date)
            _mdatACCESSTIMESTAMP = datVal
        End Set
    End Property
    Public Property SUCCESS As Integer
        Get
            Return _mintSUCCEESS
        End Get
        Set(intVal As Integer)
            _mintSUCCEESS = intVal
        End Set
    End Property
    Public ReadOnly Property GetSaveParameters As ArrayList
        Get
            Dim params As New ArrayList
            params.Add(New SqlParameter("PID", _mstrPID))
            params.Add(New SqlParameter("accessTimeStamp", _mdatACCESSTIMESTAMP))
            params.Add(New SqlParameter("success", _mintSUCCEESS))
            Return params
        End Get
    End Property
#End Region
    Public Function Save() As Integer
        Return myDB.ExecSP("sp_saveAudit", GetSaveParameters())
    End Function
End Class
