'dealing with database
Imports System.Data.SqlClient
Imports System.IO
Public Class frmMember
    'form will be pushing the data in and out of the class object that represents the table
    Private objMembers As CMembers
    Private objMemberRoles As CMemberRoles
    Private blnClearing As Boolean
    Private blnReloading As Boolean
    Private dt As DataTable
    Private sqlDA As SqlDataAdapter
    Dim strImage As String
#Region "Toolbar Stuff"
    Private Sub tsbProxy_MouseEnter(sender As Object, e As EventArgs) Handles tsbCourse.MouseEnter, tsbEvent.MouseEnter, tsbHelp.MouseEnter, tsbHome.MouseEnter, tsbLogOut.MouseEnter, tsbMember.MouseEnter, tsbRole.MouseEnter, tsbRSVP.MouseEnter, tsbSemester.MouseEnter, tsbTutor.MouseEnter, tsbMemberRoles.MouseEnter
        'you need to do this only because we are not putting out image in the Image Property of the toolbar buttons
        Dim tsbProxy As ToolStripButton
        tsbProxy = DirectCast(sender, ToolStripButton)
        tsbProxy.DisplayStyle = ToolStripItemDisplayStyle.Text
    End Sub
    Private Sub tsbProxy_MouseLeave(sender As Object, e As EventArgs) Handles tsbCourse.MouseLeave, tsbEvent.MouseLeave, tsbHelp.MouseLeave, tsbHome.MouseLeave, tsbLogOut.MouseLeave, tsbMember.MouseLeave, tsbRole.MouseLeave, tsbRSVP.MouseLeave, tsbSemester.MouseLeave, tsbTutor.MouseLeave, tsbMemberRoles.MouseLeave
        'you need to do this only because we are not putting out image in the Image Property of the toolbar buttons
        Dim tsbProxy As ToolStripButton
        tsbProxy = DirectCast(sender, ToolStripButton)
        tsbProxy.DisplayStyle = ToolStripItemDisplayStyle.Image
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

    Private Sub tsbHome_Click(sender As Object, e As EventArgs) Handles tsbHome.Click
        intNextAction = ACTION_HOME
        Me.Hide()
    End Sub

    Private Sub tsbLogOut_Click(sender As Object, e As EventArgs) Handles tsbLogOut.Click
        intNextAction = ACTION_LOGOUT
        Me.Hide()
    End Sub

    Private Sub tsbMember_Click(sender As Object, e As EventArgs) Handles tsbMember.Click
        'already on this event
        'no action needed
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

    Private Sub tsbMemberRoles_Click(sender As Object, e As EventArgs) Handles tsbMemberRoles.Click
        intNextAction = ACTION_MBRROLE
        Me.Hide()
    End Sub
#End Region



#Region "Textboxes"
    Private Sub txtBoxes_GotFocus(sender As Object, e As EventArgs) Handles txtSearchLName.GotFocus, txtPID.GotFocus, txtFName.GotFocus, txtLName.GotFocus, txtMI.GotFocus, txtEmail.GotFocus, txtPhone.GotFocus, txtPhotoPath.GotFocus
        Dim txtBox As TextBox
        txtBox = DirectCast(sender, TextBox)
        txtBox.SelectAll()
    End Sub
    Private Sub txtDesc_LostFocus(sender As Object, e As EventArgs) Handles txtSearchLName.LostFocus, txtPID.LostFocus, txtFName.LostFocus, txtLName.LostFocus, txtMI.LostFocus, txtEmail.LostFocus, txtPhone.LostFocus, txtPhotoPath.LostFocus
        Dim txtBox As TextBox
        txtBox = DirectCast(sender, TextBox)
        txtBox.DeselectAll()
    End Sub
#End Region

    Private Sub LoadMembers()
        'we are going to need to need the ability to load the list box with any exisiting members records that are in the DB
        Dim objDR As SqlDataReader
        lstMembers.Items.Clear()
        Try
            objDR = objMembers.GetAllMembers()
            Do While objDR.Read
                If objDR.Item("PID") <> "0000001" And objDR.Item("PID") <> "9999999" Then
                    lstMembers.Items.Add(objDR.Item("PID")) 'whatever your Stored Procedure returns will go in the ""
                    'picMember.Load(objDR.Item("PhotoPath"))
                End If
            Loop
            objDR.Close()
        Catch ex As Exception
            'could have CDB throw error and trap it here
        End Try
        'Remeber: Form is dealing with the individual record through the CurrentObject Property of the CRoles which exposes CRole object
        If objMembers.CurrentObject.PID <> "" Then
            lstMembers.SelectedIndex = lstMembers.FindStringExact(objMembers.CurrentObject.PID)
        End If
        blnReloading = False
    End Sub

    Private Sub frmMember_Load(sender As Object, e As EventArgs) Handles Me.Load
        'we want to update and load the roles listbox everytime this form is shown
        objMembers = New CMembers
        objMemberRoles = New CMemberRoles
        ofdOpen.DefaultExt = "*.png*"
        ofdOpen.Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg"
        ofdOpen.FilterIndex = 1
        ofdOpen.InitialDirectory = Application.StartupPath
        picMember.SizeMode = PictureBoxSizeMode.Zoom
        picMember.Load("Resources\STARS National LOGO.png")
    End Sub

    Private Sub frmMember_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ClearScreenControls(Me)
        LoadMembers()
        grpEdit.Enabled = False
    End Sub
    Private Sub lstMembers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMembers.SelectedIndexChanged
        If blnClearing Then
            Exit Sub
        End If
        If blnReloading Then
            sslStatus.Text = ""
            Exit Sub
        End If
        If lstMembers.SelectedIndex = -1 Then
            'no selection 
        End If
        chkNew.Checked = False
        LoadSelectedRecord()
        grpEdit.Enabled = True
    End Sub
    Private Sub LoadSelectedRecord()
        Try
            objMembers.GetMemberByPID(lstMembers.SelectedItem.ToString)
            With objMembers.CurrentObject
                txtPID.Text = .PID
                txtFName.Text = .FName
                txtLName.Text = .LName
                txtMI.Text = .MI
                txtEmail.Text = .Email
                txtPhone.Text = .Phone
                txtPhotoPath.Text = .PhotoPath
            End With
            If txtPhotoPath.Text <> "" Then
                picMember.SizeMode = PictureBoxSizeMode.Zoom
                picMember.Load(txtPhotoPath.Text)
            Else
                picMember.SizeMode = PictureBoxSizeMode.Zoom
                picMember.Load("Resources\STARS National LOGO.png")
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading Member values: " & ex.ToString, "Program error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub chkNew_CheckedChanged(sender As Object, e As EventArgs) Handles chkNew.CheckedChanged
        If blnClearing Then
            Exit Sub
        End If
        If chkNew.Checked Then 'checkbox turned on
            sslStatus.Text = ""
            txtSearchLName.Clear()
            txtPID.Clear()
            txtFName.Clear()
            txtLName.Clear()
            txtMI.Clear()
            txtEmail.Clear()
            txtPhone.Clear()
            txtPhotoPath.Clear()
            grpMembers.Enabled = False
            grpEdit.Enabled = True
            txtPID.Focus()
            objMembers.CreateNewMember()

            picMember.SizeMode = PictureBoxSizeMode.Zoom
            picMember.Load("Resources\STARS National LOGO.png")
        Else 'checkbox turned off
            grpMembers.Enabled = True
            grpEdit.Enabled = False
            objMembers.CurrentObject.IsNewMember = False
        End If
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim intResult As Integer
        Dim blnErrors As Boolean

        sslStatus.Text = ""
        '---------add your validation code here -------------
        If Not ValidateTextBoxLength(txtPID, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextBoxLength(txtFName, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextBoxLength(txtLName, errP) Then
            blnErrors = True
        End If
        If Not ValidateTextBoxLength(txtEmail, errP) Then
            blnErrors = True
        End If
        If blnErrors Then
            Exit Sub
        End If
        'to communicate with GUI, we need to load the CRole object
        With objMembers.CurrentObject
            .PID = txtPID.Text
            .FName = txtFName.Text
            .LName = txtLName.Text
            .MI = txtMI.Text
            .Email = txtEmail.Text
            .Phone = txtPhone.Text
            .PhotoPath = txtPhotoPath.Text
        End With
        Try
            Me.Cursor = Cursors.WaitCursor
            intResult = objMembers.Save
            If intResult = 1 Then
                sslStatus.Text = "Member record saved"
                'add member role
                objMemberRoles.CurrentObject.PID = txtPID.Text
                objMemberRoles.CurrentObject.RoleID = "Member"
                objMemberRoles.CurrentObject.SemesterID = "fa16"
                objMemberRoles.Save()
            End If
            If intResult = -1 Then 'role ID was not unique when adding a new record
                MessageBox.Show("PID must be unique. Unable to save Member record", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                sslStatus.Text = "Error"
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to save Member record" & ex.ToString, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            sslStatus.Text = "Error"
        End Try

        Me.Cursor = Cursors.Default
        blnReloading = True
        LoadMembers() 'reload so that a newly saved record will appear in the list
        grpMembers.Enabled = True 'in case it was disabled for a new record
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        blnClearing = True
        sslStatus.Text = ""
        chkNew.Checked = False
        errP.Clear()
        If lstMembers.SelectedIndex <> -1 Then
            LoadSelectedRecord() 'reload what was selected in case user had messed up the form
        Else
            grpEdit.Enabled = False
        End If
        blnClearing = False
        objMembers.CurrentObject.IsNewMember = False
        grpMembers.Enabled = True

        picMember.SizeMode = PictureBoxSizeMode.Zoom
        picMember.Load("Resources\STARS National LOGO.png")
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        lstMembers.Items.Clear()
        errP.Clear()
        Dim blnErrors As Boolean
        Dim params As New ArrayList
        If txtSearchLName.Text.Length = 0 Then 'missing the search value 
            errP.SetError(txtSearchLName, "You must enter a search value here")
            blnErrors = True
        End If
        If blnErrors Then
            Exit Sub
        End If

        params.Add(New SqlParameter("@lastname", txtLName.Text))
        sqlDA = myDB.GetDataAdapterBySP("dbo.sp_getMembersByLName", params)
        Dim objDR As SqlDataReader
        lstMembers.Items.Clear()
        Try
            objDR = objMembers.GetMembersByLName(txtSearchLName.Text)
            Do While objDR.Read
                'lstMembers.Items.Add(objDR.Item("LName") & ", " & objDR.Item("FName")) 'whatever your Stored Procedure returns will go in the ""
                If objDR.Item("PID") <> "0000001" And objDR.Item("PID") <> "9999999" Then
                    lstMembers.Items.Add(objDR.Item("PID"))
                End If
            Loop
            objDR.Close()
        Catch ex As Exception
            'could have CDB throw error and trap it here
        End Try

    End Sub
    Private Sub btnFullList_Click(sender As Object, e As EventArgs) Handles btnFullList.Click
        LoadMembers()
        grpEdit.Enabled = False
        txtSearchLName.Text = ""
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        ofdOpen.ShowDialog()
        txtPhotoPath.Clear()
        If ofdOpen.FileName = "" Then
            sslStatus.Text = "File open action canceled"
            Exit Sub
        End If
        sslStatus.Text = ofdOpen.FileName & " opened"
        strImage = ofdOpen.FileName
        picMember.SizeMode = PictureBoxSizeMode.Zoom
        picMember.Load(strImage)
        txtPhotoPath.Text = strImage
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim MemberReport As New frmMemberReport
        If lstMembers.Items.Count = 0 Then
            MessageBox.Show("There are no records to print!")
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        MemberReport.Display()
        Me.Cursor = Cursors.Default
    End Sub
End Class