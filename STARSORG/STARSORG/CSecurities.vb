Imports System.Data.SqlClient
Public Class CSecurities
    Private _Security As CSecurity
    Public Sub New()
        _Security = New CSecurity
    End Sub

    Public ReadOnly Property CurrentObject() As CSecurity
        Get
            Return _Security
        End Get
    End Property

    Public Sub Clear()
        _Security = New CSecurity
    End Sub

    Public Sub CreateNewSecurity()
        'call this when clearing the edit portion of the screen to add a new Security
        Clear()
        _Security.IsNewSecurity = True
    End Sub

    Public Function Save() As Integer
        Return _Security.Save()
    End Function

    Public Function ResetPassword() As Integer
        Return _Security.ResetPassword()
    End Function
    Public Function GetAllSecurities() As SqlDataReader
        Dim objDR As SqlDataReader
        objDR = myDB.GetDataReaderBySP("sp_getAllSecurities", Nothing)
        Return objDR
    End Function

    Public Function GetSecurityByPID(strID As String) As CSecurity
        Dim params As New ArrayList
        'Dim objDR As SqlDataReader
        params.Add(New SqlParameter("PID", strID))
        'objDR = myDB.GetDataReaderBySP("sp_getRoleByRoleID", params)
        'Return objDR
        FillObject(myDB.GetDataReaderBySP("sp_getSecurityByPID", params))
        Return _Security
    End Function

    Public Function GetSecurityByUserID(strID As String) As CSecurity
        Dim params As New ArrayList
        'Dim objDR As SqlDataReader
        params.Add(New SqlParameter("userID", strID))
        'objDR = myDB.GetDataReaderBySP("sp_getRoleByRoleID", params)
        'Return objDR
        FillObject(myDB.GetDataReaderBySP("sp_getSecurityByUserID", params))
        Return _Security
    End Function

    Private Function FillObject(objDR As SqlDataReader) As CSecurity
        'use if instead of while bc we are getting just one record
        If objDR.Read Then
            With _Security
                .PID = objDR.Item("PID")
                .UserID = objDR.Item("UserID")
                .Password = objDR.Item("Password")
                .SecRole = objDR.Item("SecRole")
            End With
        Else 'no record was returned
            'nothing to do here
        End If
        objDR.Close()
        Return _Security
    End Function

    Public Function CheckUserPasswordCombo(userID As String, password As String) As Boolean
        Dim params As New ArrayList
        params.Add(New SqlParameter("userID", userID))
        params.Add(New SqlParameter("password", password))
        Dim strResult As String = myDB.GetSingleValueFromSP("sp_CheckUserPasswordCombo", params)
        If strResult = 0 Then
            Return False
        End If
        Return True
    End Function

    Public Function CheckUserIDExists(strID As String) As Boolean
        Dim params As New ArrayList
        params.Add(New SqlParameter("userID", strID))
        Dim strResult As String = myDB.GetSingleValueFromSP("sp_CheckUserIDExists", params)
        If strResult = 0 Then
            Return False 'does not exist
        End If
        Return True
    End Function

    Public Function CheckSecurityPIDExists(strID As String) As Boolean
        Dim params As New ArrayList
        params.Add(New SqlParameter("PID", strID))
        Dim strResult As String = myDB.GetSingleValueFromSP("sp_CheckSecurityPIDExists", params)
        If strResult = 0 Then
            Return False 'does not exist
        End If
        Return True
    End Function
End Class
