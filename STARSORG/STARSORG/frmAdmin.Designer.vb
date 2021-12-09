<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdmin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grpAssignCred = New System.Windows.Forms.GroupBox()
        Me.cboMemberPID = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.cboSecRole = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnAssignCred = New System.Windows.Forms.Button()
        Me.grpResetPassword = New System.Windows.Forms.GroupBox()
        Me.btnResetPass = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtNewPassword = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.errP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.cboSecurityPID = New System.Windows.Forms.ComboBox()
        Me.grpAssignCred.SuspendLayout()
        Me.grpResetPassword.SuspendLayout()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(380, 27)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Admin"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpAssignCred
        '
        Me.grpAssignCred.Controls.Add(Me.cboMemberPID)
        Me.grpAssignCred.Controls.Add(Me.Label5)
        Me.grpAssignCred.Controls.Add(Me.txtPassword)
        Me.grpAssignCred.Controls.Add(Me.txtUserID)
        Me.grpAssignCred.Controls.Add(Me.cboSecRole)
        Me.grpAssignCred.Controls.Add(Me.Label4)
        Me.grpAssignCred.Controls.Add(Me.Label3)
        Me.grpAssignCred.Controls.Add(Me.Label2)
        Me.grpAssignCred.Controls.Add(Me.btnAssignCred)
        Me.grpAssignCred.Location = New System.Drawing.Point(12, 58)
        Me.grpAssignCred.Name = "grpAssignCred"
        Me.grpAssignCred.Size = New System.Drawing.Size(380, 265)
        Me.grpAssignCred.TabIndex = 1
        Me.grpAssignCred.TabStop = False
        Me.grpAssignCred.Text = "Assign Login Credentials"
        '
        'cboMemberPID
        '
        Me.cboMemberPID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMemberPID.FormattingEnabled = True
        Me.cboMemberPID.Location = New System.Drawing.Point(196, 38)
        Me.cboMemberPID.Name = "cboMemberPID"
        Me.cboMemberPID.Size = New System.Drawing.Size(144, 24)
        Me.cboMemberPID.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 17)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Select Member PID"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(196, 122)
        Me.txtPassword.MaxLength = 8
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(145, 23)
        Me.txtPassword.TabIndex = 3
        '
        'txtUserID
        '
        Me.txtUserID.Location = New System.Drawing.Point(196, 81)
        Me.txtUserID.MaxLength = 15
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(145, 23)
        Me.txtUserID.TabIndex = 2
        '
        'cboSecRole
        '
        Me.cboSecRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSecRole.FormattingEnabled = True
        Me.cboSecRole.Location = New System.Drawing.Point(196, 163)
        Me.cboSecRole.Name = "cboSecRole"
        Me.cboSecRole.Size = New System.Drawing.Size(145, 24)
        Me.cboSecRole.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 166)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(143, 17)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Select Privilege Level"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 125)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Create Password"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Create User ID"
        '
        'btnAssignCred
        '
        Me.btnAssignCred.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnAssignCred.Location = New System.Drawing.Point(243, 215)
        Me.btnAssignCred.Name = "btnAssignCred"
        Me.btnAssignCred.Size = New System.Drawing.Size(98, 29)
        Me.btnAssignCred.TabIndex = 5
        Me.btnAssignCred.Text = "Assign"
        Me.btnAssignCred.UseVisualStyleBackColor = False
        '
        'grpResetPassword
        '
        Me.grpResetPassword.Controls.Add(Me.cboSecurityPID)
        Me.grpResetPassword.Controls.Add(Me.btnResetPass)
        Me.grpResetPassword.Controls.Add(Me.Label7)
        Me.grpResetPassword.Controls.Add(Me.txtNewPassword)
        Me.grpResetPassword.Controls.Add(Me.Label6)
        Me.grpResetPassword.Location = New System.Drawing.Point(12, 353)
        Me.grpResetPassword.Name = "grpResetPassword"
        Me.grpResetPassword.Size = New System.Drawing.Size(380, 183)
        Me.grpResetPassword.TabIndex = 2
        Me.grpResetPassword.TabStop = False
        Me.grpResetPassword.Text = "Reset Member Password"
        '
        'btnResetPass
        '
        Me.btnResetPass.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnResetPass.Location = New System.Drawing.Point(244, 133)
        Me.btnResetPass.Name = "btnResetPass"
        Me.btnResetPass.Size = New System.Drawing.Size(98, 29)
        Me.btnResetPass.TabIndex = 8
        Me.btnResetPass.Text = "Reset"
        Me.btnResetPass.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(25, 91)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(125, 17)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Set New Password"
        '
        'txtNewPassword
        '
        Me.txtNewPassword.Location = New System.Drawing.Point(197, 88)
        Me.txtNewPassword.MaxLength = 8
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNewPassword.Size = New System.Drawing.Size(145, 23)
        Me.txtNewPassword.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(25, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 17)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Select User PID"
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(124, 553)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(137, 30)
        Me.btnLogin.TabIndex = 9
        Me.btnLogin.Text = "Back to Login"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'errP
        '
        Me.errP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.errP.ContainerControl = Me
        '
        'cboSecurityPID
        '
        Me.cboSecurityPID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSecurityPID.FormattingEnabled = True
        Me.cboSecurityPID.Location = New System.Drawing.Point(197, 43)
        Me.cboSecurityPID.Name = "cboSecurityPID"
        Me.cboSecurityPID.Size = New System.Drawing.Size(144, 24)
        Me.cboSecurityPID.TabIndex = 6
        '
        'frmAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(412, 598)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.grpResetPassword)
        Me.Controls.Add(Me.grpAssignCred)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmAdmin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmAdmin"
        Me.grpAssignCred.ResumeLayout(False)
        Me.grpAssignCred.PerformLayout()
        Me.grpResetPassword.ResumeLayout(False)
        Me.grpResetPassword.PerformLayout()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents grpAssignCred As GroupBox
    Friend WithEvents grpResetPassword As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnAssignCred As Button
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtUserID As TextBox
    Friend WithEvents cboSecRole As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents btnResetPass As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents txtNewPassword As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents btnLogin As Button
    Friend WithEvents errP As ErrorProvider
    Friend WithEvents cboMemberPID As ComboBox
    Friend WithEvents cboSecurityPID As ComboBox
End Class
