Public Class frmMain
    Private RoleInfo As frmRole
    Private LoginForm As frmLogin
    Private blnLoginFlag As Boolean = True
    Private MemberForm As frmMember
    Private MemberRoleForm As frmMemberRole
    Private EventForm As frmEvent
    Private RSVPForm As frmRSVP

    Private Sub tsbProxy_MouseEnter(sender As Object, e As EventArgs) Handles tsbCourse.MouseEnter, tsbEvent.MouseEnter, tsbHelp.MouseEnter, tsbHome.MouseEnter, tsbLogOut.MouseEnter, tsbMember.MouseEnter, tsbRole.MouseEnter, tsbRSVP.MouseEnter, tsbSemester.MouseEnter, tsbTutor.MouseEnter, tsbMemberRoles.MouseEnter
        'We need to do this only because we are not putting our images in the Image property of the toolbar buttons
        Dim tsbProxy As ToolStripButton
        tsbProxy = DirectCast(sender, ToolStripButton)
        tsbProxy.DisplayStyle = ToolStripItemDisplayStyle.Text
    End Sub

    Private Sub tsbProxy_MouseLeave(sender As Object, e As EventArgs) Handles tsbCourse.MouseLeave, tsbEvent.MouseLeave, tsbHelp.MouseLeave, tsbHome.MouseLeave, tsbLogOut.MouseLeave, tsbMember.MouseLeave, tsbRole.MouseLeave, tsbRSVP.MouseLeave, tsbSemester.MouseLeave, tsbTutor.MouseLeave, tsbMemberRoles.MouseLeave
        'We need to do this only because we are not putting our images in the Image property of the toolbar buttons
        Dim tsbProxy As ToolStripButton
        tsbProxy = DirectCast(sender, ToolStripButton)
        tsbProxy.DisplayStyle = ToolStripItemDisplayStyle.Image
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            myDB.OpenDB()
        Catch ex As Exception
            MessageBox.Show("Unable to open database. Connection string=" & gstrConn, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            EndProgram()
        End Try

        RoleInfo = New frmRole
        LoginForm = New frmLogin
        MemberForm = New frmMember
        MemberRoleForm = New frmMemberRole
        EventForm = New frmEvent
        RSVPForm = New frmRSVP

    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If blnLoginFlag Then
            LoginForm.ShowDialog()
        End If
        blnLoginFlag = False

        If strPrivilegeLevel = GUEST Then
            tsbRole.Enabled = False
            tsbMember.Enabled = False
            tsbEvent.Enabled = False
            tsbMemberRoles.Enabled = False
        End If
        If strPrivilegeLevel = MEMBER Then
            tsbMember.Enabled = False
            tsbEvent.Enabled = False
            tsbMemberRoles.Enabled = False
        End If
    End Sub

    Private Sub EndProgram()
        'Close each form except main
        Dim f As Form
        Me.Cursor = Cursors.WaitCursor
        For Each f In Application.OpenForms
            If f.Name <> Me.Name Then
                If Not f Is Nothing Then
                    f.Close()
                End If
            End If
        Next
        'close database connection
        If Not objSQLConn Is Nothing Then
            objSQLConn.Close()
            objSQLConn.Dispose()
        End If
        Me.Cursor = Cursors.Default 'always put the user's cursor back to normal
        Application.Exit()
    End Sub

    Private Sub tsbRole_Click(sender As Object, e As EventArgs) Handles tsbRole.Click
        Me.Hide()
        RoleInfo.ShowDialog()
        Me.Show()
        PerformNextAction()
    End Sub

    Private Sub tsbMember_Click(sender As Object, e As EventArgs) Handles tsbMember.Click
        Me.Hide()
        MemberForm.ShowDialog()
        Me.Show()
        PerformNextAction()
    End Sub

    Private Sub tsbMemberRoles_Click(sender As Object, e As EventArgs) Handles tsbMemberRoles.Click
        Me.Hide()
        MemberRoleForm.ShowDialog()
        Me.Show()
        PerformNextAction()
    End Sub
    Private Sub tsbEvemt_Click(sender As Object, e As EventArgs) Handles tsbEvent.Click
        Me.Hide()
        EventForm.ShowDialog()
        Me.Show()
        PerformNextAction()
    End Sub
    Private Sub tsbRSVP_Click(sender As Object, e As EventArgs) Handles tsbRSVP.Click
        Me.Hide()
        RSVPForm.ShowDialog()
        Me.Show()
        PerformNextAction()
    End Sub

    Private Sub tsbLogOut_Click(sender As Object, e As EventArgs) Handles tsbLogOut.Click
        EndProgram()
    End Sub
    Private Sub PerformNextAction()
        'get the next action specified on the child form, and then simulate the click of that button here
        Select Case intNextAction
            Case ACTION_COURSE
                tsbCourse.PerformClick()
            Case ACTION_EVENT
                tsbEvent.PerformClick()
            Case ACTION_HELP
                tsbHelp.PerformClick()
            Case ACTION_HOME
                tsbHome.PerformClick()
            Case ACTION_LOGOUT
                tsbLogOut.PerformClick()
            Case ACTION_MEMBER
                tsbMember.PerformClick()
            Case ACTION_NONE
                'nothing to do here
            Case ACTION_ROLE
                tsbRole.PerformClick()
            Case ACTION_RSVP
                tsbRSVP.PerformClick()
            Case ACTION_SEMESTER
                tsbSemester.PerformClick()
            Case ACTION_TUTOR
                tsbTutor.PerformClick()
            Case ACTION_MBRROLE
                tsbMemberRoles.PerformClick()
            Case Else
                MessageBox.Show("Unexpected case value in frmMain:PerformNextAction", "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select
    End Sub


End Class
