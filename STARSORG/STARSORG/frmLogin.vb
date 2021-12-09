Imports System.Data.SqlClient
Public Class frmLogin
    'forms
    Private AdminForm As frmAdmin
    Private PasswordForm As frmChangePassword

    'db table handlers
    Private objAudits As CAudits
    Private objSecurities As CSecurities
    Private objMembers As CMembers

    'constants
    Private Const GUEST_PID As String = "0000001"
    Private Const GUEST_FNAME As String = "GUEST"
    Private Const GUEST_LNAME As String = "GUEST"
    Private Const GUEST_PRIVILEGE As String = "GUEST"
    Private Const LOGIN_FAIL_PID As String = "9999999"


    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles Me.Load

        'instantiate forms
        AdminForm = New frmAdmin
        PasswordForm = New frmChangePassword

        'instantiate db table handlers
        objAudits = New CAudits
        objSecurities = New CSecurities
        objMembers = New CMembers

        'hide error labels
        lblValidate.Visible = False
    End Sub

    Private Sub frmLogin_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        txtUsername.Focus()
    End Sub

    Private Sub btnGuest_Click(sender As Object, e As EventArgs) Handles btnGuest.Click ' done

        'set audit for guest
        AuditLogin(GUEST_PID, 1)

        'set globals for guest
        strPID = GUEST_PID
        strFirstName = GUEST_FNAME
        strLastName = GUEST_LNAME
        strPrivilegeLevel = GUEST_PRIVILEGE

        Me.Close()

    End Sub

    Private Sub btnSingIn_Click(sender As Object, e As EventArgs) Handles btnSingIn.Click ' done

        'first guard, validate on empty
        If ValidateTextBoxEmpty() Then
            Exit Sub
        End If

        'second guard, validate username and password combination
        'audit login failure
        If Not objSecurities.CheckUserPasswordCombo(txtUsername.Text, txtPassword.Text) Then
            lblValidate.Text = "* username or password incorrect"
            lblValidate.Visible = True
            AuditLogin(LOGIN_FAIL_PID, 0)
            Exit Sub
        End If

        'set globals for user
        LoadSecurity()
        With objSecurities.CurrentObject
            strPID = .PID
            strPrivilegeLevel = .SecRole
        End With

        LoadMember(strPID)
        With objMembers.CurrentObject
            strFirstName = .FName
            strLastName = .LName
        End With

        'audit login success
        AuditLogin(strPID, 1)
        'MessageBox.Show("Welcome: " & strFirstName & " " & strLastName & " " & strPrivilegeLevel)
        Me.Close()
    End Sub

    Private Sub btnPassword_Click(sender As Object, e As EventArgs) Handles btnPassword.Click ' done

        'first guard, validate on empty
        If ValidateTextBoxEmpty() Then
            Exit Sub
        End If

        'second guard, validate username and password combination
        'audit login failure
        If Not objSecurities.CheckUserPasswordCombo(txtUsername.Text, txtPassword.Text) Then
            lblValidate.Text = "* username or password incorrect"
            lblValidate.Visible = True
            AuditLogin(LOGIN_FAIL_PID, 0)
            Exit Sub
        End If

        'audit login success
        LoadSecurity()
        AuditLogin(objSecurities.CurrentObject.PID, 1)

        'set PID global for use in 'change password' screen
        strPID = objSecurities.CurrentObject.PID

        'MessageBox.Show("Welcome: " & objSecurities.CurrentObject.UserID)
        lblValidate.Visible = False
        PasswordForm.ShowDialog()
        txtPassword.Clear()
        txtUsername.Clear()
        txtUsername.Focus()
    End Sub

    Private Sub btnAdmin_Click(sender As Object, e As EventArgs) Handles btnAdmin.Click ' done

        'first guard, validate on empty
        If ValidateTextBoxEmpty() Then
            Exit Sub
        End If

        'second guard, validate username and password combination
        'audit login failure
        If Not objSecurities.CheckUserPasswordCombo(txtUsername.Text, txtPassword.Text) Then
            lblValidate.Text = "* username or password incorrect"
            lblValidate.Visible = True
            AuditLogin(LOGIN_FAIL_PID, 0)
            Exit Sub
        End If

        'third guard, validate admin privilege
        'audit login failure
        LoadSecurity()
        If objSecurities.CurrentObject.SecRole <> "ADMIN" Then
            lblValidate.Text = "* not admin"
            lblValidate.Visible = True
            AuditLogin(LOGIN_FAIL_PID, 0)
            Exit Sub
        End If

        'audit login success
        AuditLogin(objSecurities.CurrentObject.PID, 1)

        'MessageBox.Show("Welcome: " & objSecurities.CurrentObject.UserID)
        lblValidate.Visible = False
        AdminForm.ShowDialog()
        txtPassword.Clear()
        txtUsername.Clear()
        txtUsername.Focus()
    End Sub

    Private Sub LoadSecurity()
        Try
            objSecurities.GetSecurityByUserID(txtUsername.Text)
        Catch ex As Exception
            MessageBox.Show("Error loading Security values: " & ex.ToString, "Program error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadMember(pid As String)
        Try
            objMembers.GetMemberByPID(pid)
        Catch ex As Exception
            MessageBox.Show("Error loading Member values: " & ex.ToString, "Program error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AuditLogin(pid As String, success As Integer)
        objAudits.CurrentObject.PID = pid
        objAudits.CurrentObject.ACCESSTIMESTAMP = DateTime.Now
        objAudits.CurrentObject.SUCCESS = success
        Try
            objAudits.Save()
        Catch ex As Exception
            MessageBox.Show("Unable to save Audit record" & ex.ToString, "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateTextBoxEmpty() As Boolean
        Dim blnErrors As Boolean
        If Not ValidateTextboxLength(txtUsername, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxWhiteSpace(txtUsername, errP) Then
            blnErrors = True
        End If

        If Not ValidateTextboxLength(txtPassword, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextboxWhiteSpace(txtPassword, errP) Then
            blnErrors = True
        End If

        Return blnErrors
    End Function
End Class