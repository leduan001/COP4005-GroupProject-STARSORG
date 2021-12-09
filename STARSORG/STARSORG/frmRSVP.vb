Imports System.Data.SqlClient
Public Class frmRSVP
    Private objRSVPs As CEvent_RSVPs
    Private blnClearing As Boolean
    Private blnReloading As Boolean
    Private HomeInfo As frmMain
    'Private RSVPReport As frmRSVPReport
    Private objEvents As CEvents
    Private MemberForm As CMembers
#Region "Toolbar Stuff"
    Private Sub tsbProxy_MouseEnter(sender As Object, e As EventArgs) Handles tsbCourse.MouseEnter, tsbEvent.MouseEnter, tsbHelp.MouseEnter, tsbHome.MouseEnter, tsbLogOut.MouseEnter, tsbMember.MouseEnter, tsbRole.MouseEnter, tsbRSVP.MouseEnter, tsbSemester.MouseEnter, tsbTutor.MouseEnter, tsbMemberRoles.MouseEnter
        'We need to do this only because we are not putting our images in the Image property, but instead in the BackgroudImage Property
        Dim tsbProxy As ToolStripButton
        tsbProxy = DirectCast(sender, ToolStripButton)
        tsbProxy.DisplayStyle = ToolStripItemDisplayStyle.Text
    End Sub
    Private Sub tsbProxy_MouseLeave(sender As Object, e As EventArgs) Handles tsbCourse.MouseLeave, tsbEvent.MouseLeave, tsbHelp.MouseLeave, tsbHome.MouseLeave, tsbLogOut.MouseLeave, tsbMember.MouseLeave, tsbRole.MouseLeave, tsbRSVP.MouseLeave, tsbSemester.MouseLeave, tsbTutor.MouseLeave, tsbMemberRoles.MouseLeave
        'We need to do this only because we are not putting our images in the Image property, but instead in the BackgroudImage Property
        Dim tsbProxy As ToolStripButton
        tsbProxy = DirectCast(sender, ToolStripButton)
        tsbProxy.DisplayStyle = ToolStripItemDisplayStyle.Image
    End Sub

    'toolstrip actions. no action specified for current screen
    Private Sub tsbCourse_Click(sender As Object, e As EventArgs) Handles tsbCourse.Click
        intNextAction = ACTION_COURSE
        Me.Hide()
    End Sub

    Private Sub tsbEvent_Click(sender As Object, e As EventArgs) Handles tsbEvent.Click
        intNextAction = ACTION_EVENT
        Me.Hide()
    End Sub

    Private Sub tsbHelp_Click(sender As Object, e As EventArgs) Handles tsbHelp.Click
        intNextAction = ACTION_HELP
        Me.Hide()
    End Sub

    Private Sub tsbHome_Click(sender As Object, e As EventArgs) Handles tsbHome.Click
        intNextAction = ACTION_HOME
        Me.Hide()
    End Sub

    Private Sub tsbLogOut_Click(sender As Object, e As EventArgs) Handles tsbLogOut.Click
        intNextAction = ACTION_LOGOUT
        Me.Hide()
    End Sub

    Private Sub tsbMember_Click(sender As Object, e As EventArgs) Handles tsbMember.Click
        intNextAction = ACTION_MEMBER
        Me.Hide()
    End Sub

    Private Sub tsbRole_Click(sender As Object, e As EventArgs) Handles tsbRole.Click
        intNextAction = ACTION_ROLE
        Me.Hide()
    End Sub

    Private Sub tsbRSVP_Click(sender As Object, e As EventArgs) Handles tsbRSVP.Click
        'nothing to do here
    End Sub

    Private Sub tsbSemester_Click(sender As Object, e As EventArgs) Handles tsbSemester.Click
        intNextAction = ACTION_SEMESTER
        Me.Hide()
    End Sub

    Private Sub tsbTutor_Click(sender As Object, e As EventArgs) Handles tsbTutor.Click
        intNextAction = ACTION_TUTOR
        Me.Hide()
    End Sub
    Private Sub tsbMemeberRoles_Click(sender As Object, e As EventArgs) Handles tsbMemberRoles.Click
        intNextAction = ACTION_MBRROLE
        Me.Hide()
    End Sub

#End Region

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        'initialize everything here and instantiate a form object for each form in the application
        HomeInfo = New frmMain
        objRSVPs = New CEvent_RSVPs
        objEvents = New CEvents
        MemberForm = New CMembers

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
    Private Sub frmRSVP_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'refresh the list each time this form is shown
        ClearScreenControls(Me)
        LoadEvents()
        If strPID <> "0000001" Then
            txtFName.Enabled = False
            txtLName.Enabled = False
            txtEmail.Enabled = False

            txtFName.Text = strFirstName
            txtLName.Text = strLastName

            MemberForm.GetMemberByPID(strPID)
            txtEmail.Text = MemberForm.CurrentObject.Email
        End If
    End Sub
    Private Sub LoadEvents()
        Dim objReader As SqlDataReader
        lstEvents.Items.Clear()
        Try
            objReader = objEvents.GetFutureEvents()
            Do While objReader.Read
                lstEvents.Items.Add(objReader.Item("EventID"))
            Loop
            objReader.Close()
        Catch ex As Exception
            MessageBox.Show("Failed To Fetch Events" & ex.ToString, "Query Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        If objEvents.CurrentObject.EventID <> "" Then
            lstEvents.SelectedIndex = lstEvents.FindStringExact(objEvents.CurrentObject.EventID)
        End If
        blnReloading = False
    End Sub

    'Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
    '    RSVPReport = New frmRSVPReport
    '    If lstEvents.Items.Count = 0 Then
    '        MessageBox.Show("There are no RSVPs for this event")
    '        Exit Sub
    '    End If
    '    Me.Cursor = Cursors.WaitCursor
    '    RSVPReport.Display()
    'End Sub

    Private Sub lstEvents_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstEvents.SelectedIndexChanged
        errP.Clear()
        If blnClearing Then
            Exit Sub
        End If
        If Not blnReloading Then
            tslStatus.Text = " "
        End If
        If lstEvents.SelectedIndex = -1 Then 'nothing to do
            Exit Sub
        End If
        LoadSelectedRecord()
    End Sub
    Private Sub LoadSelectedRecord()
        Try
            objEvents.GetEventByEventID(lstEvents.SelectedItem.ToString)
            With objEvents.CurrentObject
                lblDesc.Text = .EventDescription
                txtEvent.Text = .EventID
            End With
        Catch ex As Exception
            MessageBox.Show("Error Loading Event Values", "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If strPID = "0000001" Then
            tslStatus.Text = ""
            txtFName.Clear()
            txtLName.Clear()
            txtEmail.Clear()
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        errP.Clear()
        Dim intResult As Integer
        Dim blnErrors As Boolean
        If strPID = "0000001" Then
            'first do validation
            If Not ValidateTextboxLength(txtFName, errP) Then
                blnErrors = True
            End If
            If Not ValidateTextboxLength(txtLName, errP) Then
                blnErrors = True
            End If
            If Not ValidateTextboxLength(txtEmail, errP) Then
                blnErrors = True
            End If
        End If
        If Not ValidateTextboxLength(txtEvent, errP) Then
            blnErrors = True
        End If
        If blnErrors Then
            Exit Sub
        End If

        With objRSVPs.CurrentObject
            .EventID = lstEvents.SelectedItem.ToString
            .FName = txtFName.Text
            .LName = txtLName.Text
            .Email = txtEmail.Text
        End With
        Try
            Me.Cursor = Cursors.WaitCursor
            intResult = objRSVPs.Save

            If intResult = 1 Then
                tslStatus.Text = "RSVP record saved"
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to save RSVP record: " & ex.ToString, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Cursor = Cursors.Default
        blnReloading = True
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim RoleReport As New frmRSVPReport
        Me.Cursor = Cursors.WaitCursor
        RoleReport.Display()
        Me.Cursor = Cursors.Default
    End Sub
End Class