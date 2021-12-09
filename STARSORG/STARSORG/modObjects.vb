Module modObjects
    Public Sub ClearScreenControls(ByVal objContainer As Control) 'Public Sub ClearScreenControls(ByVal f As Form) changed to include groupBox and other containers, not just forms
        'this procedure will clear all controls on the form passed in as the argument
        'not all control types have been implemented here - add as needed
        Dim obj As Control
        Dim strControlType As String
        For Each obj In objContainer.Controls ' a form is a container and so it has a control's collection
            'figure out what kind it is
            strControlType = TypeName(obj) 'typeName returns the class name of the control
            Select Case strControlType
                Case "TextBox"  'find the class type in the form design tab by clicking on the control and checking the first line in the properties pane e.g. Systems.Windows.Forms.TextBox
                    Dim cntrl As TextBox
                    cntrl = DirectCast(obj, TextBox) 'controls have different ways to clear. For textbox there is Clear(), empty string, and vbNullString
                    cntrl.Clear()
                Case "CheckBox"
                    Dim cntrl As CheckBox
                    cntrl = DirectCast(obj, CheckBox)
                    cntrl.Checked = False 'or cntrl.CheckedState = Unchecked
                Case "ComboBox"
                    Dim cntrl As ComboBox
                    cntrl = DirectCast(obj, ComboBox)
                    cntrl.SelectedIndex = -1 'clear only the selection, not the list contents
                Case "RadioButton"
                    Dim cntrl As RadioButton
                    cntrl = DirectCast(obj, RadioButton)
                    cntrl.Checked = False
                Case "MaskedTextBox"
                    Dim cntrl As MaskedTextBox
                    cntrl = DirectCast(obj, MaskedTextBox)
                    cntrl.Clear()
                Case "GroupBox"
                    'must recurse through its controls collection
                    Dim cntrl As GroupBox
                    cntrl = DirectCast(obj, GroupBox)
                    ClearScreenControls(cntrl)
                Case Else
                    'do nothing, or add some error trapping code if needed
            End Select
        Next
    End Sub
End Module
