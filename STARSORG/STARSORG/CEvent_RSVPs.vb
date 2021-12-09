Imports System.Data.SqlClient

Public Class CEvent_RSVPs
    Private _RSVP As CEvent_RSVP

    Public Sub New()
        _RSVP = New CEvent_RSVP
    End Sub

    Public ReadOnly Property CurrentObject() As CEvent_RSVP
        Get
            Return _RSVP
        End Get
    End Property

    Public Sub Clear()
        _RSVP = New CEvent_RSVP
    End Sub

    Public Sub CreateNewRSVP()
        Clear()
        _RSVP.IsNewRSVP = True
    End Sub

    Public Function Save() As Integer
        Return _RSVP.Save()
    End Function

    Public Function GetAllRSVPs() As SqlDataReader
        Dim objDR As SqlDataReader
        objDR = myDB.GetDataReaderBySP("sp_getEvents", Nothing)
        Return objDR
    End Function

    Public Function GetRSVPsByEventID(strID As String) As CEvent_RSVP
        Dim params As New ArrayList
        params.Add(New SqlParameter("eventID", strID))
        FillObject(myDB.GetDataReaderBySP("sp_getRSVPsByEventID", params))
        Return _RSVP
    End Function

    Private Function FillObject(objDR As SqlDataReader) As CEvent_RSVP
        If objDR.Read Then
            With _RSVP
                .EventID = objDR.Item("EventID")
                .FName = objDR.Item("FName")
                .LName = objDR.Item("LName")
                .Email = objDR.Item("Email")
            End With
        Else

        End If
        objDR.Close()
        Return _RSVP
    End Function
End Class