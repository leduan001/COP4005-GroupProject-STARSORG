Imports System.Data.SqlClient
Public Class CMemberRole
    'Represents a single record in the ROLE table
    Private _mstrPID As String
    Private _mstrRoleID As String
    Private _mstrSemesterID As String
    Private _isNewMemberRole As Boolean
    'costructor
    Public Sub New()
        _mstrPID = ""
    End Sub
#Region "Exposed Properties"
    Public Property PID As String
        Get
            Return _mstrPID
        End Get
        Set(strVal As String)
            _mstrPID = strVal
        End Set
    End Property

    Public Property RoleID As String
        Get
            Return _mstrRoleID
        End Get
        Set(strVal As String)
            _mstrRoleID = strVal
        End Set
    End Property

    Public Property SemesterID As String
        Get
            Return _mstrSemesterID
        End Get
        Set(strVal As String)
            _mstrSemesterID = strVal
        End Set
    End Property

    Public Property IsNewMemberRole As Boolean
        Get
            Return _isNewMemberRole
        End Get
        Set(blnVal As Boolean)
            _isNewMemberRole = blnVal
        End Set
    End Property

    Public ReadOnly Property GetSaveParameters() As ArrayList
        'this properties code will create the parameters for the stored procedure to save a record
        Get
            Dim params As New ArrayList
            params.Add(New SqlParameter("PID", _mstrPID))
            params.Add(New SqlParameter("RoleID", _mstrRoleID))
            params.Add(New SqlParameter("SemesterID", _mstrSemesterID))
            Return params
        End Get
    End Property

    Public ReadOnly Property GetDeleteParameters() As ArrayList
        'this properties code will create the parameters for the stored procedure to save a record
        Get
            Dim params As New ArrayList
            params.Add(New SqlParameter("PID", _mstrPID))
            params.Add(New SqlParameter("RoleID", _mstrRoleID))
            params.Add(New SqlParameter("SemesterID", _mstrSemesterID))
            Return params
        End Get
    End Property

    Public Function Save() As Integer
        If IsNewMemberRole Then
            Dim params As New ArrayList
            params.Add(New SqlParameter("PID", _mstrPID))
            params.Add(New SqlParameter("RoleID", _mstrRoleID))
            params.Add(New SqlParameter("SemesterID", _mstrSemesterID))
            Dim strResult As String = myDB.GetSingleValueFromSP("sp_CheckIfMemberRoleIsUnique", params)
            If Not strResult = 0 Then
                Return -1 'not unique
            End If
            'if not a new role, or it is new and has a unique ID, then do the save(update or insert)
            Return myDB.ExecSP("sp_saveMemberRole", GetSaveParameters())
        End If
        Return myDB.ExecSP("sp_saveMemberRole", GetSaveParameters())
    End Function

    Public Function Delete() As Integer
        If Not IsNewMemberRole Then
            Dim params As New ArrayList
            params.Add(New SqlParameter("PID", _mstrPID))
            params.Add(New SqlParameter("RoleID", _mstrRoleID))
            params.Add(New SqlParameter("SemesterID", _mstrSemesterID))
            Dim strResult As String = myDB.GetSingleValueFromSP("sp_CheckIfMemberRoleIsUnique", params)
            If strResult = 0 Then
                Return -1 'not unique
            End If
            'if not a new role, or it is new and has a unique ID, then do the save(update or insert)
            Return myDB.ExecSP("sp_deleteMemberRole", GetDeleteParameters())
        End If
        Return myDB.ExecSP("sp_deleteMemberRole", GetDeleteParameters())
    End Function

#End Region
    Public Function GetReportData() As SqlDataAdapter
        Return myDB.GetDataAdapterBySP("dbo.sp_getAllMemberRoles", Nothing)
    End Function

End Class
