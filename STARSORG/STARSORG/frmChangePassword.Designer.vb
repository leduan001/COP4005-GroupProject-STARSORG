<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangePassword
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCurrentPass = New System.Windows.Forms.TextBox()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.txtReNewPass = New System.Windows.Forms.TextBox()
        Me.txtNewPass = New System.Windows.Forms.TextBox()
        Me.errP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.grpChangePass = New System.Windows.Forms.GroupBox()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpChangePass.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(53, 28)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Current Password"
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(384, 36)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Change Password"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(73, 89)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "New Password"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 131)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(160, 17)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Re-enter New Password"
        '
        'txtCurrentPass
        '
        Me.txtCurrentPass.Location = New System.Drawing.Point(186, 25)
        Me.txtCurrentPass.MaxLength = 8
        Me.txtCurrentPass.Name = "txtCurrentPass"
        Me.txtCurrentPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtCurrentPass.Size = New System.Drawing.Size(160, 23)
        Me.txtCurrentPass.TabIndex = 1
        '
        'btnChange
        '
        Me.btnChange.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnChange.Location = New System.Drawing.Point(186, 176)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(160, 30)
        Me.btnChange.TabIndex = 4
        Me.btnChange.Text = "Change"
        Me.btnChange.UseVisualStyleBackColor = False
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(118, 312)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(160, 30)
        Me.btnLogin.TabIndex = 5
        Me.btnLogin.Text = "Back to Login"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'txtReNewPass
        '
        Me.txtReNewPass.Location = New System.Drawing.Point(186, 128)
        Me.txtReNewPass.MaxLength = 8
        Me.txtReNewPass.Name = "txtReNewPass"
        Me.txtReNewPass.Size = New System.Drawing.Size(160, 23)
        Me.txtReNewPass.TabIndex = 3
        '
        'txtNewPass
        '
        Me.txtNewPass.Location = New System.Drawing.Point(186, 86)
        Me.txtNewPass.MaxLength = 8
        Me.txtNewPass.Name = "txtNewPass"
        Me.txtNewPass.Size = New System.Drawing.Size(160, 23)
        Me.txtNewPass.TabIndex = 2
        '
        'errP
        '
        Me.errP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.errP.ContainerControl = Me
        '
        'grpChangePass
        '
        Me.grpChangePass.Controls.Add(Me.Label1)
        Me.grpChangePass.Controls.Add(Me.txtNewPass)
        Me.grpChangePass.Controls.Add(Me.Label3)
        Me.grpChangePass.Controls.Add(Me.txtReNewPass)
        Me.grpChangePass.Controls.Add(Me.Label4)
        Me.grpChangePass.Controls.Add(Me.txtCurrentPass)
        Me.grpChangePass.Controls.Add(Me.btnChange)
        Me.grpChangePass.Location = New System.Drawing.Point(12, 67)
        Me.grpChangePass.Name = "grpChangePass"
        Me.grpChangePass.Size = New System.Drawing.Size(384, 225)
        Me.grpChangePass.TabIndex = 12
        Me.grpChangePass.TabStop = False
        '
        'frmChangePassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(408, 359)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpChangePass)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.Label2)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmChangePassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmChangePassword"
        CType(Me.errP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpChangePass.ResumeLayout(False)
        Me.grpChangePass.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtCurrentPass As TextBox
    Friend WithEvents btnChange As Button
    Friend WithEvents btnLogin As Button
    Friend WithEvents txtReNewPass As TextBox
    Friend WithEvents txtNewPass As TextBox
    Friend WithEvents errP As ErrorProvider
    Friend WithEvents grpChangePass As GroupBox
End Class
