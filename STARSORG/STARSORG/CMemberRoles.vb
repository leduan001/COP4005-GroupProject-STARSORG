Imports System.Data.SqlClient
Public Class CMemberRoles
    Private _MemberRole As New CMemberRole

    Public Sub New()
        _MemberRole = New CMemberRole
    End Sub

    Public ReadOnly Property CurrentObject() As CMemberRole
        Get
            Return _MemberRole
        End Get
    End Property

    Public Sub Clear()
        _MemberRole = New CMemberRole
    End Sub

    Public Function Save() As Integer
        Return _MemberRole.Save()
    End Function

    Public Function Delete() As Integer
        Return _MemberRole.Delete()
    End Function

    Public Sub CreateNewRole()
        Clear()
        _MemberRole.IsNewMemberRole = True
    End Sub

    Public Function GetAllMembers() As SqlDataReader
        Dim objDR As SqlDataReader
        objDR = myDB.GetDataReaderBySP("sp_getAllRoles", Nothing)
        Return objDR
    End Function

    Public Function GetAllMemberRoles() As SqlDataReader
        Dim objDR As SqlDataReader
        objDR = myDB.GetDataReaderBySP("sp_getAllMemberRoles", Nothing)
        Return objDR
    End Function

    Public Function GetMemberByMemberID(strID As String) As CMemberRole
        Dim params As New ArrayList
        params.Add(New SqlParameter("MemberID", strID))
        FillObject(myDB.GetDataReaderBySP("sp_getMemberByPID", params))
        Return _MemberRole
    End Function

    Public Function GetMemberRoleByPID(strID As String) As CMemberRole
        Dim params As New ArrayList
        params.Add(New SqlParameter("PID", strID))
        FillObject(myDB.GetDataReaderBySP("sp_getMemberRoleByPID", params))
        Return _MemberRole
    End Function

    Public Function GetMemberRoleByPIDandSemesterID(strID As String, Semester As String) As SqlDataReader
        Dim objDR As SqlDataReader
        Dim params As New ArrayList
        params.Add(New SqlParameter("PID", strID))
        params.Add(New SqlParameter("SemesterID", Semester))
        objDR = myDB.GetDataReaderBySP("sp_getMemberRoleByPIDandSemester", params)
        Return objDR
    End Function

    Private Function FillObject(objDR As SqlDataReader) As CMemberRole
        If objDR.Read Then
            With _MemberRole
                .PID = objDR.Item("PID")
                .RoleID = objDR.Item("RoleID")
                .SemesterID = objDR.Item("SemesterID")
            End With
        Else 'no record was returned
            'nothing to do here
        End If
        objDR.Close()
        Return _MemberRole
    End Function

End Class
