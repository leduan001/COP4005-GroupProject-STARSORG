Imports System.Data.SqlClient

Public Class CEventTypes
    Private _EventType As CEventType
    Public Function GetAllEventTypes() As SqlDataReader
        Dim objDR As SqlDataReader
        objDR = myDB.GetDataReaderBySP("sp_getAllEventTypes", Nothing)
        Return objDR
    End Function
    Public ReadOnly Property CurrentObject() As CEventType
        Get
            Return _EventType
        End Get
    End Property
    Private Function FillObject(objDR As SqlDataReader) As CEventType
        If objDR.Read Then
            With _EventType
                .EventTypeID = objDR.Item("EventTypeID")
                .EventTypeDesc = objDR.Item("EventTypeDescription")
            End With
        Else 'no record was returned
            'nothing to do here
        End If
        objDR.Close()
        Return _EventType
    End Function
End Class
