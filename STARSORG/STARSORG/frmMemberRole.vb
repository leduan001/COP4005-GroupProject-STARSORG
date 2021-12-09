Imports System.Data.SqlClient
Public Class frmMemberRole
    Private objMember As CMembers
    Private objSemester As CSemesters
    Private objRoles As CRoles
    Private blnClearing As Boolean
    Private blnReloading As Boolean
    Private MemberRoleReport As frmMemberRoleReport
    Private objMemberRoles As CMemberRoles
    Private arrRoleState(15) As Boolean
    Private strPID As String
    Private strSemester As String

#Region "Toolbar Stuff"
    Private Sub tsbProxy_MouseEnter(sender As Object, e As EventArgs) Handles tsbCourse.MouseEnter, tsbEvent.MouseEnter, tsbHelp.MouseEnter, tsbHome.MouseEnter, tsbLogOut.MouseEnter, tsbMember.MouseEnter, tsbRole.MouseEnter, tsbRSVP.MouseEnter, tsbSemester.MouseEnter, tsbTutor.MouseEnter, tsbMemberRoles.MouseEnter
        'We need to do this only because we are not putting our images in the image property but, 
        'instead in the backgrounImage property
        Dim tsbProxy As ToolStripButton
        tsbProxy = DirectCast(sender, ToolStripButton)
        tsbProxy.DisplayStyle = ToolStripItemDisplayStyle.Text
    End Sub

    Private Sub tsbProxy_MouseLeave(sender As Object, e As EventArgs) Handles tsbCourse.MouseLeave, tsbEvent.MouseLeave, tsbHelp.MouseLeave, tsbHome.MouseLeave, tsbLogOut.MouseLeave, tsbMember.MouseLeave, tsbRole.MouseLeave, tsbRSVP.MouseLeave, tsbSemester.MouseLeave, tsbTutor.MouseLeave, tsbMemberRoles.MouseLeave
        'We need to do this only because we are not putting our images in the image property but, 
        'instead in the backgrounImage property
        Dim tsbProxy As ToolStripButton
        tsbProxy = DirectCast(sender, ToolStripButton)
        tsbProxy.DisplayStyle = ToolStripItemDisplayStyle.Image
    End Sub

    Private Sub tsbHome_Click(sender As Object, e As EventArgs) Handles tsbHome.Click
        intNextAction = ACTION_HOME
        Me.Hide()
    End Sub

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

    Private Sub tsbLogout_Click(sender As Object, e As EventArgs) Handles tsbLogOut.Click
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
#End Region
    Public Sub LoadMembers()
        Dim objReader As SqlDataReader
        lstMember.Items.Clear()
        Try
            objReader = objMember.GetAllMembers
            Do While objReader.Read
                If objReader.Item("PID") <> "0000001" And objReader.Item("PID") <> "9999999" Then
                    lstMember.Items.Add(objReader.Item("PID"))
                End If

            Loop
            objReader.Close()
        Catch ex As Exception
            'currently CDB will display the error
        End Try
        If objMember.CurrentObject.PID <> "" Then
            lstMember.SelectedIndex = lstMember.FindStringExact(objMember.CurrentObject.PID)
        End If
        blnReloading = False
    End Sub

    Public Sub LoadSemesters()
        Dim objReader As SqlDataReader
        cboSemester.Items.Clear()
        Try
            objReader = objSemester.GetAllSemesters
            Do While objReader.Read
                cboSemester.Items.Add(objReader.Item("SemesterID"))
            Loop
            objReader.Close()
        Catch ex As Exception
            'currently CDB will display the error
        End Try
        blnReloading = False
    End Sub

    Public Sub LoadAllRoles()
        Dim objReader As SqlDataReader
        clbRoles.Items.Clear()
        Try
            objReader = objRoles.GetAllRoles()
            Do While objReader.Read
                clbRoles.Items.Add(objReader.Item("RoleID"))
            Loop
            objReader.Close()
        Catch ex As Exception
            'currently CDB will display the error
        End Try
        If objRoles.CurrentObject.RoleID <> "" Then
            clbRoles.SelectedIndex = clbRoles.FindStringExact(objRoles.CurrentObject.RoleID)
        End If
        blnReloading = False
        VerifyRoleCheckedState()
    End Sub

    'Public Sub LoadMemberRoles()
    '    'only add the Member's Roles to the dropdown
    '    Dim objReader As SqlDataReader
    '    Dim Member As String = lstMember.SelectedItem.ToString()
    '    Dim Semester As String = cboSemester.SelectedItem.ToString()
    '    lstRoles.Items.Clear()
    '    Try
    '        objReader = objMemberRoles.GetAllMemberRoles
    '        Do While objReader.Read
    '            If objMemberRoles.GetMemberRoleByPIDandSemesterID(Member, Semester).Equals(True) Then
    '                lstRoles.Items.Add(objReader.Item("RoleID"))
    '            End If
    '        Loop
    '        objReader.Close()
    '    Catch ex As Exception
    '        'currently CDB will display the error
    '    End Try
    'End Sub

    Private Sub VerifyRoleCheckedState()
        Dim objDR As SqlDataReader
        strPID = lstMember.SelectedItem.ToString()
        strSemester = cboSemester.SelectedItem.ToString()
        Try
            objDR = objMemberRoles.GetMemberRoleByPIDandSemesterID(strPID, strSemester)
            Do While objDR.Read
                For i = 0 To clbRoles.Items.Count - 1
                    If objDR.Item("RoleID") = clbRoles.Items(i) Then
                        arrRoleState(i) = True
                        clbRoles.SetItemCheckState(i, CheckState.Checked)
                        objMemberRoles.CreateNewRole()
                    Else
                        arrRoleState(i) = False
                    End If
                Next
            Loop
            objDR.Close()
        Catch ex As Exception
            'CD might throw the error And trap it here
        End Try
    End Sub

    Private Sub frmMemberRole_Load(sender As Object, e As EventArgs) Handles Me.Load
        objMember = New CMembers
        objSemester = New CSemesters
        objRoles = New CRoles
        objMemberRoles = New CMemberRoles
    End Sub

    Private Sub frmMemberRole_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Refresh the list each time this form is shown
        ClearScreenControls(Me)
        LoadMembers()
        LoadSemesters()
        grpSemester.Enabled = False
        grpRole.Enabled = False
        clbRoles.Enabled = False
        btnSave.Enabled = False
    End Sub

    Private Sub lstMember_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMember.SelectedIndexChanged
        If blnClearing Then
            Exit Sub
        End If
        If Not blnReloading Then
            tslStatus.Text = ""
        End If
        If lstMember.SelectedIndex = -1 Then 'nothing to do
            Exit Sub
        End If
        LoadSelectedRecord()
        grpSemester.Enabled = True
    End Sub

    Private Sub cboSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSemester.SelectedIndexChanged
        If blnClearing Then
            Exit Sub
        End If
        If Not blnReloading Then
            tslStatus.Text = ""
        End If
        If lstMember.SelectedIndex = -1 Then 'nothing to do
            Exit Sub
        End If
        LoadSelectedRecord()
        grpRole.Enabled = True
        clbRoles.Enabled = True
        btnSave.Enabled = True
        LoadAllRoles()
    End Sub

    Private Sub LoadSelectedRecord()
        Try
            objMember.GetMemberByPID(lstMember.SelectedItem.ToString)
            With objMember.CurrentObject
            End With
        Catch ex As Exception
            MessageBox.Show("Error loading Role Values", "Program error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        LoadMembers()
        LoadSemesters()
        grpSemester.Enabled = False
        grpRole.Enabled = False
        clbRoles.Enabled = False
        btnSave.Enabled = False
        lstMember.SelectedIndex = -1
        clbRoles.Items.Clear()
        strPID = ""
        strSemester = ""
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim intSaveResult As Integer
        Dim intDeleteResult As Integer
        Dim i As Integer
        For i = 0 To clbRoles.Items.Count - 1
            If clbRoles.GetItemChecked(i).Equals(True) And arrRoleState(i) = False Then 'Role checked = True while previous state was False
                With objMemberRoles.CurrentObject
                    .PID = lstMember.SelectedItem.ToString
                    .RoleID = clbRoles.Items(i).ToString
                    .SemesterID = cboSemester.SelectedItem.ToString
                End With
                Try
                    Me.Cursor = Cursors.WaitCursor
                    intSaveResult = objMemberRoles.Save
                    If intSaveResult = 1 Then
                        tslStatus.Text = "Member Role record saved"
                    End If
                    If intSaveResult = -1 Then
                        'MessageBox.Show("Unable to save Member Role record, record already exists", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        'tslStatus.Text = clbRoles.Items(i).ToString + " , already exists"
                    End If
                Catch ex As Exception
                    MessageBox.Show("Unable to save Member Role record: " & ex.ToString, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                Me.Cursor = Cursors.Default
            ElseIf clbRoles.GetItemChecked(i).Equals(False) And arrRoleState(i) = True Then 'Role checked = False while previous state was True
                'arrRoleState(i) = False
                With objMemberRoles.CurrentObject
                    .PID = lstMember.SelectedItem.ToString
                    .RoleID = clbRoles.Items(i).ToString
                    .SemesterID = cboSemester.SelectedItem.ToString
                End With
                Try
                    Me.Cursor = Cursors.WaitCursor
                    intDeleteResult = objMemberRoles.Delete
                    If intDeleteResult = 1 Then
                        tslStatus.Text = "Member Role record deleted"
                    End If
                    If intDeleteResult = -1 Then
                        MessageBox.Show("Unable to delete Member Role record, record does not exist", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tslStatus.Text = "Error"
                    End If
                Catch ex As Exception
                    MessageBox.Show("Unable to delete Member Role record: " & ex.ToString, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                Me.Cursor = Cursors.Default
            ElseIf clbRoles.GetItemChecked(i).Equals(False) And arrRoleState(i) = False Then 'Role checked = False while previous state was True
                'arrRoleState(i) = False
                With objMemberRoles.CurrentObject
                    .PID = lstMember.SelectedItem.ToString
                    .RoleID = clbRoles.Items(i).ToString
                    .SemesterID = cboSemester.SelectedItem.ToString
                End With
                Try
                    Me.Cursor = Cursors.WaitCursor
                    intDeleteResult = objMemberRoles.Delete
                    If intDeleteResult = 1 Then
                        tslStatus.Text = "Member Role record modified"
                    End If
                    If intDeleteResult = -1 Then
                        MessageBox.Show("Unable to delete Member Role record, record does not exist", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tslStatus.Text = "Error"
                    End If
                Catch ex As Exception
                    MessageBox.Show("Unable to delete Member Role record: " & ex.ToString, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                Me.Cursor = Cursors.Default
            Else
            End If
        Next
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        MemberRoleReport = New frmMemberRoleReport
        Me.Cursor = Cursors.WaitCursor
        MemberRoleReport.Display()
        Me.Cursor = Cursors.Default
    End Sub


End Class