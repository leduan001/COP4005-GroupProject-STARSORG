Imports System.Data.SqlClient
Public Class frmChangePassword

    'db table handlers
    Private objSecurities As CSecurities

    Private Sub frmChangePassword_Load(sender As Object, e As EventArgs) Handles Me.Load

        objSecurities = New CSecurities

    End Sub

    Private Sub frmChangePassword_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        txtCurrentPass.Focus()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ClearScreenControls(grpChangePass)
        errP.Clear()
        Me.Close()
    End Sub

    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click

        errP.Clear()

        'first guard, validate empty textboxes
        If ValidateChangePass() Then
            Exit Sub
        End If

        'second guard, validate current password
        LoadSecurity()
        If objSecurities.CurrentObject.Password <> txtCurrentPass.Text Then
            errP.SetError(txtCurrentPass, "Does not match current password")
            Exit Sub
        End If

        'third guard, validate re-entered password
        If txtNewPass.Text <> txtReNewPass.Text Then
            errP.SetError(txtReNewPass, "Does not match new password")
            Exit Sub
        End If

        'change password
        objSecurities.CurrentObject.Password = txtNewPass.Text  'update password field with new value
        Try
            objSecurities.ResetPassword()    'update security record with new password
        Catch ex As Exception
            MessageBox.Show("Unable to update Security record" & ex.ToString, "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        MessageBox.Show("Password Successfully Changed")
        ClearScreenControls(grpChangePass)
    End Sub

    Private Function ValidateChangePass() As Boolean
        Dim blnErrors As Boolean

        If Not ValidateTextboxLength(txtCurrentPass, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxWhiteSpace(txtCurrentPass, errP) Then
            blnErrors = True
        End If

        If Not ValidateTextboxLength(txtNewPass, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxWhiteSpace(txtNewPass, errP) Then
            blnErrors = True
        End If

        If Not ValidateTextboxLength(txtReNewPass, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxWhiteSpace(txtReNewPass, errP) Then
            blnErrors = True
        End If

        Return blnErrors
    End Function

    Private Sub LoadSecurity()
        Try
            objSecurities.GetSecurityByPID(strPID)
        Catch ex As Exception
            MessageBox.Show("Error loading Security values: " & ex.ToString, "Program error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class