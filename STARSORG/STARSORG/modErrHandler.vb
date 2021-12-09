Module modErrHandler
    'useful routines for input validation
    Public Function ValidateTextboxLength(ByRef obj As TextBox, ByRef errP As ErrorProvider) As Boolean
        'this procedure validates that a textbox is not empty
        If obj.Text.Length = 0 Then
            errP.SetIconAlignment(obj, ErrorIconAlignment.MiddleRight)
            errP.SetError(obj, "You must enter a value here")
            obj.Focus()
            Return False
        Else
            errP.SetError(obj, "")
            Return True
        End If
    End Function
    Public Function ValidateTextboxWhiteSpace(ByRef obj As TextBox, ByRef errP As ErrorProvider) As Boolean
        'this procedure validates that a textbox has no whitespaces
        If Trim(obj.Text) = "" Then
            errP.SetIconAlignment(obj, ErrorIconAlignment.MiddleRight)
            errP.SetError(obj, "You must enter a value here")
            obj.Focus()
            Return False
        Else
            errP.SetError(obj, "")
            Return True
        End If
    End Function

    Public Function ValidateTextBoxNumeric(ByRef obj As TextBox, ByRef errP As ErrorProvider) As Boolean
        'this procedure validates that a text box has a numeric value
        If Not IsNumeric(obj.Text) Then 'IsNumeric also handles empty values and will return false, so no need to test length
            errP.SetIconAlignment(obj, ErrorIconAlignment.MiddleRight)
            errP.SetError(obj, "You must enter a numeric value here")
            obj.Focus()
            Return False
        Else
            errP.SetError(obj, "")
            Return True
        End If
    End Function
    Public Function validateTextBoxDate(ByRef obj As TextBox, ByRef errp As ErrorProvider) As Boolean
        'this procedure validates that a textbox has a valid date value
        If Not IsDate(obj.Text) Then
            errp.SetIconAlignment(obj, ErrorIconAlignment.MiddleLeft)
            errp.SetError(obj, "You must enter a valid date value here")
            obj.Focus()
            Return False
        Else
            errp.SetError(obj, "")
            Return True
        End If
    End Function
    Public Function ValidateCombo(ByRef obj As ComboBox, ByRef errp As ErrorProvider) As Boolean
        If obj.SelectedIndex = -1 Then
            errp.SetIconAlignment(obj, ErrorIconAlignment.MiddleRight)
            errp.SetError(obj, "You must make a selection here")
            obj.Focus()
            Return False
        Else
            errp.SetError(obj, "")
            Return True
        End If
    End Function
    Public Function ValidateMaskedTextBoxLength(ByRef obj As MaskedTextBox, ByRef errP As ErrorProvider) As Boolean
        'same validation as a textbox that is empty
        If obj.Text.Length = 0 Then
            errP.SetIconAlignment(obj, ErrorIconAlignment.MiddleLeft)
            errP.SetError(obj, "You must enter a value here")
            obj.Focus()
            Return False
        Else
            errP.SetError(obj, "")
            Return True
        End If
    End Function
End Module
