Imports System.Data.SqlClient
Public Class CRole
    'represents a single record in the ROLE table
    Private _mstrRoleID As String   ' "m" to indicate that it's at a module level as opposed to a form
    Private _mstrRoleDescription As String
    Private _isNewRole As Boolean   'internal to class (not represented in db table). Identifies whether insert or update

    'constructor
    Public Sub New()
        _mstrRoleID = ""
        _mstrRoleDescription = ""

    End Sub

#Region "Exposed Properties"
    Public Property RoleID As String
        Get
            Return _mstrRoleID
        End Get
        Set(strVal As String)
            _mstrRoleID = strVal
        End Set
    End Property

    Public Property RoleDescription As String
        Get
            Return _mstrRoleDescription
        End Get
        Set(strVal As String)
            _mstrRoleDescription = strVal
        End Set
    End Property
    Public Property IsNewRole As Boolean
        Get
            Return _isNewRole
        End Get
        Set(blnVal As Boolean)
            _isNewRole = blnVal
        End Set
    End Property
    Public ReadOnly Property GetSaveParameters() As ArrayList
        'this property's code will create the parameters for the stored procedure to save a record
        Get
            Dim params As New ArrayList
            params.Add(New SqlParameter("roleID", _mstrRoleID))
            params.Add(New SqlParameter("roleDescription", _mstrRoleDescription))
            Return params
        End Get
    End Property
#End Region

    Public Function Save() As Integer
        'return -1 if the ID already exists (and we cannot create a new record with duplicate ID)
        'handles insert
        If IsNewRole Then
            Dim params As New ArrayList
            params.Add(New SqlParameter("roleID", _mstrRoleID))
            Dim strResult As String = myDB.GetSingleValueFromSP("sp_CheckRoleIDExists", params)
            If Not strResult = 0 Then
                Return -1 'not UNIQUE!!
            End If
        End If
        'if not a new role, or it is new and has a unique ID, then do the save (update or insert)
        Return myDB.ExecSP("sp_saveRole", GetSaveParameters())
    End Function

    Public Function GetReportData() As SqlDataAdapter
        Return myDB.GetDataAdapterBySP("dbo.sp_getAllRoles", Nothing)
    End Function

End Class
