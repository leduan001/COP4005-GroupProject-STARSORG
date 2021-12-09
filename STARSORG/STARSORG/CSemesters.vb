Imports System.Data.SqlClient

Public Class CSemesters
    Private _Semester As CSemester
    Public Function GetAllSemesters() As SqlDataReader
        Dim objDR As SqlDataReader
        objDR = myDB.GetDataReaderBySP("sp_getAllSemesters", Nothing)
        Return objDR
    End Function

    Private Function FillObject(objDR As SqlDataReader) As CSemester
        If objDR.Read Then
            With _Semester
                .SemesterID = objDR.Item("SemesterID")
                .SemesterDesc = objDR.Item("SemesterDesc")
            End With
        Else 'no record was returned
            'nothing to do here
        End If
        objDR.Close()
        Return _Semester
    End Function
End Class
