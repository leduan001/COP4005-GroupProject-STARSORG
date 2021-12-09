Imports System.Data.SqlClient
Public Class frmEvent
    Private objEvents As CEvents
    Private blnClearing As Boolean
    Private blnReloading As Boolean
    Private HomeInfo As frmMain
    Private RoleInfo As frmRole
    Private objEventType As CEventTypes
    Private objSemester As CSemesters
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
        'nothing to do here
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
        intNextAction = ACTION_RSVP
        Me.Hide()
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
        RoleInfo = New frmRole
        HomeInfo = New frmMain
        objEvents = New CEvents
        objSemester = New CSemesters
        objEventType = New CEventTypes

    End Sub
    Private Sub frmEvent_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'refresh the list each time this form is shown
        ClearScreenControls(Me)
        LoadEvents()
        LoadComboBoxChoices()
        grpEventEdit.Enabled = False
    End Sub
    Private Sub lstEvents_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstEvents.SelectedIndexChanged
        txtEventID.Enabled = True
        txtDesc.Enabled = True
        cboSemesterID.Enabled = True
        dtpStart.Enabled = True
        dtpEnd.Enabled = True
        txtLocation.Enabled = True
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
        chkNew.Checked = False
        LoadSelectedRecord()
        If DateTime.Compare(dtpEnd.Value, Today.Date) < 0 Then
            txtEventID.Enabled = False
            txtDesc.Enabled = False
            cboSemesterID.Enabled = False
            dtpStart.Enabled = False
            dtpEnd.Enabled = False
            txtLocation.Enabled = False
        End If
        grpEventEdit.Enabled = True
    End Sub
    Private Sub LoadComboBoxChoices()
        cboEventTypeID.Items.Clear()
        cboSemesterID.Items.Clear()
        Dim objReader As SqlDataReader
        Try
            objReader = objEventType.GetAllEventTypes
            Do While objReader.Read
                cboEventTypeID.Items.Add(objReader.Item("EventTypeID"))
            Loop
            objReader.Close()
        Catch ex As Exception

        End Try
        'If objEventType.CurrentObject.EventTypeID <> "" Then
        '    cboEventTypeID.SelectedIndex = cboEventTypeID.FindStringExact(objEventType.CurrentObject.EventTypeID)
        'End If
        'blnReloading = False
        Try
            objReader = objSemester.GetAllSemesters()
            Do While objReader.Read
                cboSemesterID.Items.Add(objReader.Item("SemesterID"))
            Loop
            objReader.Close()
        Catch ex As Exception

        End Try
        'If objSemester.CurrentObject.SemesterID <> "" Then
        '    cboSemesterID.SelectedIndex = cboSemesterID.FindStringExact(objSemester.CurrentObject.SemesterID)
        'End If
        'blnReloading = False

    End Sub


    Private Sub LoadSelectedRecord()
        Try
            objEvents.GetEventByEventID(lstEvents.SelectedItem.ToString)
            With objEvents.CurrentObject
                txtEventID.Text = .EventID
                txtDesc.Text = .EventDescription
                cboEventTypeID.SelectedValue = .EventTypeID
                cboSemesterID.SelectedValue = .SemesterID
                dtpStart.Value = .StartDate
                dtpEnd.Value = .EndDate
                txtLocation.Text = .Location
            End With
        Catch ex As Exception
            MessageBox.Show("Error Loading Event Values", "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub chkNew_CheckedChanged(sender As Object, e As EventArgs) Handles chkNew.CheckedChanged
        txtEventID.Enabled = True
        txtDesc.Enabled = True
        cboSemesterID.Enabled = True
        dtpStart.Enabled = True
        dtpEnd.Enabled = True
        txtLocation.Enabled = True
        errP.Clear()
        If blnClearing Then
            Exit Sub
        End If
        If chkNew.Checked Then
            tslStatus.Text = ""
            txtEventID.Clear()
            txtDesc.Clear()
            cboEventTypeID.SelectedIndex = -1
            cboSemesterID.SelectedIndex = -1
            dtpStart.Value = Today.Date
            dtpEnd.Value = Today.Date
            txtLocation.Clear()
            lstEvents.SelectedIndex = -1
            grpEvents.Enabled = False
            grpEventEdit.Enabled = True
            objEvents.CreateNewEvent()
            txtEventID.Focus()
        Else
            grpEvents.Enabled = True
            grpEventEdit.Enabled = False
            objEvents.CurrentObject.IsNewEvent = False

        End If
    End Sub
    Private Sub LoadEvents()
        Dim objReader As SqlDataReader
        lstEvents.Items.Clear()
        Try
            objReader = objEvents.GetAllEvents()
            Do While objReader.Read
                lstEvents.Items.Add(objReader.Item("EventID"))
            Loop
            objReader.Close()
        Catch ex As Exception

        End Try
        If objEvents.CurrentObject.EventID <> "" Then
            lstEvents.SelectedIndex = lstEvents.FindStringExact(objEvents.CurrentObject.EventID)
        End If
        blnReloading = False
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim intResult As Integer
        Dim blnErrors As Boolean
        'first do validation
        If Not ValidateTextBoxLength(txtEventID, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextBoxLength(txtDesc, errP) Then
            blnErrors = True
        End If
        If Not ValidateCombo(cboEventTypeID, errP) Then
            blnErrors = True
        End If
        If Not ValidateCombo(cboSemesterID, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextBoxLength(txtLocation, errP) Then
            blnErrors = True
        End If
        If blnErrors Then
            Exit Sub
        End If
        'load the current object from the textboxes
        With objEvents.CurrentObject
            .EventID = txtEventID.Text
            .EventDescription = txtDesc.Text
            .EventTypeID = cboEventTypeID.SelectedItem.ToString
            .SemesterID = cboSemesterID.SelectedItem.ToString
            .StartDate = dtpStart.Value
            .EndDate = dtpEnd.Value
            .Location = txtLocation.Text
        End With
        'MessageBox.Show(objEvents.CurrentObject.EventID)
        Try
            Me.Cursor = Cursors.WaitCursor
            intResult = objEvents.Save

            If intResult = 1 Then
                tslStatus.Text = "Event record saved"
            End If
            If intResult = -1 Then 'ID not unique
                MessageBox.Show("EventID must be unique. Unable to save Event record", "Database Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tslStatus.Text = "Error"
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to save Event record: " & ex.ToString, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Cursor = Cursors.Default
        blnReloading = True
        LoadEvents() 'reload so that a newly saved record will appear
        chkNew.Checked = False
        grpEvents.Enabled = True 'in case it was disabled for a new record
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        tslStatus.Text = ""
        txtEventID.Clear()
        txtDesc.Clear()
        cboEventTypeID.SelectedIndex = -1
        cboSemesterID.SelectedIndex = -1
        dtpStart.Value = Today.Date
        dtpEnd.Value = Today.Date
        txtLocation.Clear()
        lstEvents.SelectedIndex = -1
        grpEvents.Enabled = False
        grpEventEdit.Enabled = True
        objEvents.CreateNewEvent()
        txtEventID.Focus()
    End Sub
End Class