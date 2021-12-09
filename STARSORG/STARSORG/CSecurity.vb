Imports System.Data.SqlClient
Public Class CSecurity
    Private _mstrPID As String
    Private _mstrUserID As String
    Private _mstrPassword As String
    Private _mstrSecRole As String
    Private _isNewSecurity As Boolean

#Region "Exposed Properties"
    Public Property PID As String
        Get
            Return _mstrPID
        End Get
        Set(strVal As String)
            _mstrPID = strVal
        End Set
    End Property
    Public Property UserID As String
        Get
            Return _mstrUserID
        End Get
        Set(strVal As String)
            _mstrUserID = strVal
        End Set
    End Property
    Public Property Password As String
        Get
            Return _mstrPassword
        End Get
        Set(strVal As String)
            _mstrPassword = strVal
        End Set
    End Property
    Public Property SecRole As String
        Get
            Return _mstrSecRole
        End Get
        Set(strVal As String)
            _mstrSecRole = strVal
        End Set
    End Property
    Public Property IsNewSecurity As Boolean
        Get
            Return _isNewSecurity
        End Get
        Set(blnVal As Boolean)
            _isNewSecurity = blnVal
        End Set
    End Property
    Public ReadOnly Property GetSaveParameters() As ArrayList
        Get
            Dim params As New ArrayList
            params.Add(New SqlParameter("PID", _mstrPID))
            params.Add(New SqlParameter("userID", _mstrUserID))
            params.Add(New SqlParameter("password", _mstrPassword))
            params.Add(New SqlParameter("secRole", _mstrSecRole))
            Return params
        End Get
    End Property
#End Region
    Public Function Save() As Integer
        'return -1 if the ID already exists (and we cannot create a new record with duplicate ID)
        'handles insert
        If IsNewSecurity Then
            Dim params As New ArrayList
            params.Add(New SqlParameter("PID", _mstrPID))
            Dim strResult As String = myDB.GetSingleValueFromSP("sp_CheckSecurityPIDExists", params)
            If Not strResult = 0 Then
                Return -1 'not UNIQUE!!
            End If
        End If
        'if not a new security, or it is new and has a unique ID, then do the save (update or insert)
        Return myDB.ExecSP("sp_saveSecurity", GetSaveParameters())
    End Function

    Public Function ResetPassword() As Integer
        Return myDB.ExecSP("sp_saveSecurity", GetSaveParameters())
    End Function
End Class
