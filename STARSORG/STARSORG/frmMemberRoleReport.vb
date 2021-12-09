Imports Microsoft.Reporting.WinForms
Imports System.Data.SqlClient
Public Class frmMemberRoleReport
    Private ds As DataSet
    Private da As SqlDataAdapter
    Private MemberRole As CMemberRole
    Private Sub frmMemberRoleReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.rpvReport.RefreshReport()
    End Sub
    Public Sub Display()
        MemberRole = New CMemberRole
        rpvReport.LocalReport.ReportPath = AppDomain.CurrentDomain.BaseDirectory & "Reports\rptMemberRoleReport.rdlc"
        ds = New DataSet
        da = MemberRole.GetReportData()
        da.Fill(ds)
        rpvReport.LocalReport.DataSources.Add(New ReportDataSource("dsMemberRole", ds.Tables(0)))
        rpvReport.SetDisplayMode(DisplayMode.PrintLayout)
        rpvReport.RefreshReport()
        Me.Cursor = Cursors.Default
        Me.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        Me.Cursor = Cursors.Default
    End Sub
End Class