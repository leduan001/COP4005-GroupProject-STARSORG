Imports System.Data.SqlClient

Public Class CEvent
    Private _mstrEventID As String
    Private _mstrEventDesc As String
    Private _mstrEventType As String
    Private _mstrSemester As String
    Private _mstrStartDate As Date
    Private _mstrEndDate As Date
    Private _mstrLocation As String
    Private _isNewEvent As Boolean

    Public Sub New()
        _mstrEventID = ""
        _mstrEventDesc = ""
        _mstrEventType = ""
        _mstrSemester = ""
        _mstrLocation = ""
    End Sub

#Region "Exposed Properties"
    Public Property EventID As String
        Get
            Return _mstrEventID
        End Get
        Set(strVal As String)
            _mstrEventID = strVal
        End Set
    End Property

    Public Property EventDescription As String
        Get
            Return _mstrEventDesc
        End Get
        Set(strVal As String)
            _mstrEventDesc = strVal
        End Set
    End Property

    Public Property EventTypeID As String
        Get
            Return _mstrEventType
        End Get
        Set(strVal As String)
            _mstrEventType = strVal
        End Set
    End Property

    Public Property SemesterID As String
        Get
            Return _mstrSemester
        End Get
        Set(strVal As String)
            _mstrSemester = strVal
        End Set
    End Property

    Public Property StartDate As Date
        Get
            Return _mstrStartDate
        End Get
        Set(dteVal As Date)
            _mstrStartDate = dteVal
        End Set
    End Property

    Public Property EndDate As Date
        Get
            Return _mstrEndDate
        End Get
        Set(dteVal As Date)
            _mstrEndDate = dteVal
        End Set
    End Property

    Public Property Location As String
        Get
            Return _mstrLocation
        End Get
        Set(strVal As String)
            _mstrLocation = strVal
        End Set
    End Property

    Public Property IsNewEvent As Boolean
        Get
            Return _isNewEvent
        End Get
        Set(blnVal As Boolean)
            _isNewEvent = blnVal
        End Set
    End Property

    Public ReadOnly Property GetSaveParameters() As ArrayList
        Get
            Dim params As New ArrayList
            params.Add(New SqlParameter("eventID", _mstrEventID))
            params.Add(New SqlParameter("eventDescription", _mstrEventDesc))
            params.Add(New SqlParameter("eventTypeID", _mstrEventType))
            params.Add(New SqlParameter("semesterID", _mstrSemester))
            params.Add(New SqlParameter("startDate", _mstrStartDate))
            params.Add(New SqlParameter("endDate", _mstrEndDate))
            params.Add(New SqlParameter("location", _mstrLocation))
            Return params
        End Get
    End Property
#End Region
    Public Function Save() As Integer
        If IsNewEvent Then
            Dim params As New ArrayList
            params.Add(New SqlParameter("eventID", _mstrEventID))
            Dim strResult As String = myDB.GetSingleValueFromSP("sp_CheckEventIDExists", params)
            If Not strResult = 0 Then
                Return -1 'Not Unique
            End If
            Return myDB.ExecSP("sp_saveEvents", GetSaveParameters())
        End If
        Return myDB.ExecSP("sp_saveEvents", GetSaveParameters())
    End Function

    Public Function GetReportData() As SqlDataAdapter
        Return myDB.GetDataAdapterBySP("dbo.sp_getEvents", Nothing)
    End Function
End Class
