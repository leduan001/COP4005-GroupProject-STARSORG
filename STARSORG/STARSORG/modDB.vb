Imports System.Data.SqlClient
Module modDB
    Public objSQLConn As SqlConnection
    Public objSQLCommand As SqlCommand
    Public gstrConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=E:\COP4005 - Group Project\STARSORG\STARSDB.mdf;Integrated Security=True" ' gstrConn -> stands for "global string"
    'C:\Users\ledua\Documents\GitHub\sample-8-leduan001\HenryBooks path has to be changed if accessing from a different computer
    'keeping it in a jump drive resolves this issue, the drive letter might have to be changed
End Module
