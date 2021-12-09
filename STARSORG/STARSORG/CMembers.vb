Imports System.Data.SqlClient
Public Class CMembers
    'Represents the MEMBER table and its associated business rules
    Private _Member As CMember
    'constructor
    Public Sub New()
        'instantiate the CRole object
        _Member = New CMember
    End Sub

    Public ReadOnly Property CurrentObject() As CMember
        Get
            Return _Member
        End Get
    End Property

    Public Sub Clear()
        _Member = New CMember
    End Sub

    Public Sub CreateNewMember()
        'call this when clearing the edit portion of the screen to add a new member
        Clear()
        _Member.IsNewMember = True
    End Sub

    Public Function Save() As Integer
        Return _Member.Save()
    End Function

    Public Function GetAllMembers() As SqlDataReader
        Dim objDR As SqlDataReader
        objDR = myDB.GetDataReaderBySP("sp_getAllMembers", Nothing)
        Return objDR
    End Function

    Public Function GetMemberByPID(strID As String) As CMember
        Dim params As New ArrayList
        'Dim objDR As SqlDataReader
        params.Add(New SqlParameter("PID", strID))
        'objDR = myDB.GetDataReaderBySP("sp_getRoleByRoleID", params)
        'Return objDR
        FillObject(myDB.GetDataReaderBySP("sp_getMemberByPID", params))
        Return _Member
    End Function

    Public Function GetMembersByLName(strName As String) As SqlDataReader
        Dim objDR As SqlDataReader
        Dim params As New ArrayList
        params.Add(New SqlParameter("LName", strName))
        objDR = myDB.GetDataReaderBySP("sp_getMembersByLName", params)
        Return objDR
    End Function

    Private Function FillObject(objDR As SqlDataReader) As CMember
        'use if instead of while bc we are getting just one record
        If objDR.Read Then
            With _Member
                .PID = objDR.Item("PID")
                .FName = objDR.Item("FName")
                .LName = objDR.Item("LName")
                '.MI = objDR.Item("MI")
                If Not IsDBNull(objDR.Item("MI")) Then
                    .MI = objDR.Item("MI")
                Else
                    .MI = ""
                End If
                .Email = objDR.Item("Email")
                .Phone = objDR.Item("Phone")
                '.PhotoPath = objDR.Item("PhotoPath")
                If Not IsDBNull(objDR.Item("PhotoPath")) Then
                    .PhotoPath = objDR.Item("PhotoPath")
                Else
                    .PhotoPath = ""
                End If
            End With
        Else 'no record was returned
            'nothing to do here
        End If
        objDR.Close()
        Return _Member
    End Function

    Public Function CheckMemberPIDExists(strID As String) As Boolean
        Dim params As New ArrayList
        params.Add(New SqlParameter("PID", strID))
        Dim strResult As String = myDB.GetSingleValueFromSP("sp_CheckMemberPIDExists", params)
        If strResult = 0 Then
            Return False
        End If
        Return True
    End Function
End Class
