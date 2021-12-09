Imports System.Data.SqlClient
Public Class frmAdmin
    'db table handlers
    Private objSecurities As CSecurities
    Private objMembers As CMembers

    'constants for validating PID
    Private Const GUEST_PID As String = "0000001"
    Private Const LOGIN_FAIL_PID As String = "9999999"

    Private Sub frmAdmin_Load(sender As Object, e As EventArgs) Handles Me.Load

        'instantiate db table handlers
        objSecurities = New CSecurities
        objMembers = New CMembers

        LoadCboPrivileges()
        LoadCboMemberPID()
        LoadCboSecurityPID()

    End Sub

    Private Sub frmAdmin_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        cboMemberPID.Focus()
    End Sub

    Private Sub LoadCboPrivileges()
        cboSecRole.Items.Clear()
        cboSecRole.Items.Add("ADMIN")
        cboSecRole.Items.Add("OFFICER")
        cboSecRole.Items.Add("MEMBER")
    End Sub

    Private Sub LoadCboMemberPID()
        Dim objDR As SqlDataReader
        Dim strPID As String = ""
        cboMemberPID.Items.Clear()
        Try
            objDR = objMembers.GetAllMembers()
            Do While objDR.Read
                strPID = objDR.Item("PID")
                If strPID <> GUEST_PID And strPID <> LOGIN_FAIL_PID Then
                    cboMemberPID.Items.Add(strPID)
                End If
            Loop
            objDR.Close()
        Catch ex As Exception
            MessageBox.Show("Unable to fetch Memebers PID" & ex.ToString, "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCboSecurityPID()
        Dim objDR As SqlDataReader
        cboSecurityPID.Items.Clear()
        Try
            objDR = objSecurities.GetAllSecurities()
            Do While objDR.Read
                cboSecurityPID.Items.Add(objDR.Item("PID"))
            Loop
            objDR.Close()
        Catch ex As Exception
            MessageBox.Show("Unable to fetch Security PID" & ex.ToString, "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ClearScreenControls(Me)
        errP.Clear()
        Me.Close()
    End Sub

    Private Sub btnAssignCred_Click(sender As Object, e As EventArgs) Handles btnAssignCred.Click 'done

        errP.Clear()

        'first guard, validate on empty
        If ValidateAssignCred() Then
            Exit Sub
        End If

        'second guard, validate PID against MEMBER table and dummy PID's "0000001" and "9999999"
        'If (Not objMembers.CheckMemberPIDExists(txtPID.Text)) Or (txtPID.Text = GUEST_PID) Or (txtPID.Text = LOGIN_FAIL_PID) Then
        '    errP.SetError(txtPID, "PID not found")
        '    Exit Sub
        'End If

        'third guard, validate PID against SECURITY table (if record exists then initial creds were already assigned)
        If objSecurities.CheckSecurityPIDExists(cboMemberPID.SelectedItem.ToString) Then
            errP.SetError(cboMemberPID, "This member has credentials already")
            Exit Sub
        End If

        'fourth guard, validate UserID is unique
        If objSecurities.CheckUserIDExists(txtUserID.Text) Then
            errP.SetError(txtUserID, "This UserID is not available")
            Exit Sub
        End If

        'save security credentials
        With objSecurities.CurrentObject
            .PID = cboMemberPID.SelectedItem.ToString
            .UserID = txtUserID.Text
            .Password = txtPassword.Text
            .SecRole = cboSecRole.SelectedItem
        End With
        Try
            objSecurities.Save()
        Catch ex As Exception
            MessageBox.Show("Unable to save Security record" & ex.ToString, "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        MessageBox.Show("Credentials Successfully Added")
        ClearScreenControls(grpAssignCred)
    End Sub

    Private Function ValidateAssignCred() As Boolean
        Dim blnErrors As Boolean

        If Not ValidateCombo(cboMemberPID, errP) Then
            blnErrors = True
        End If

        If Not ValidateTextboxLength(txtUserID, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxWhiteSpace(txtUserID, errP) Then
            blnErrors = True
        End If

        If Not ValidateTextboxLength(txtPassword, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxWhiteSpace(txtPassword, errP) Then
            blnErrors = True
        End If

        If Not ValidateCombo(cboSecRole, errP) Then
            blnErrors = True
        End If

        Return blnErrors
    End Function

    Private Sub btnResetPass_Click(sender As Object, e As EventArgs) Handles btnResetPass.Click 'done

        errP.Clear()

        'first guard, validate on empty
        If ValidateResetPass() Then
            Exit Sub
        End If

        'second guard, validate PID against MEMBER table
        'If (Not objMembers.CheckMemberPIDExists(txtPID2.Text)) Or (txtPID2.Text = GUEST_PID) Or (txtPID2.Text = LOGIN_FAIL_PID) Then
        '    errP.SetError(txtPID2, "PID not found")
        '    Exit Sub
        'End If

        'third guard, validate PID against SECURITY table
        'If Not objSecurities.CheckSecurityPIDExists(txtPID2.Text) Then
        '    errP.SetError(txtPID2, "Member has no assinged credentials yet")
        '    Exit Sub
        'End If

        'reset password
        LoadSecurity() 'loads security record to be updated
        objSecurities.CurrentObject.Password = txtNewPassword.Text  'update password field with new value
        Try
            objSecurities.ResetPassword()    'update security record with new password
        Catch ex As Exception
            MessageBox.Show("Unable to update Security record" & ex.ToString, "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        MessageBox.Show("Successful Password Reset")
        ClearScreenControls(grpResetPassword)
    End Sub

    Private Function ValidateResetPass() As Boolean
        Dim blnErrors As Boolean

        If Not ValidateCombo(cboSecurityPID, errP) Then
            blnErrors = True
        End If

        If Not ValidateTextboxLength(txtNewPassword, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxWhiteSpace(txtNewPassword, errP) Then
            blnErrors = True
        End If

        Return blnErrors
    End Function

    Private Sub LoadSecurity()
        Try
            objSecurities.GetSecurityByPID(cboSecurityPID.SelectedItem.ToString)
        Catch ex As Exception
            MessageBox.Show("Error loading Security values: " & ex.ToString, "Program error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class