' Developer Express Code Central Example:
' Printing appointment details using the XtraReports Suite
' 
' This example illustrates how you can print the appointment details for the
' appointments currently displayed in the Scheduler by means of the XtraReports
' Suite.
' The key point is to obtain a collection of appointments and assign it to
' the report's DataSource
' (http://documentation.devexpress.com/#XtraReports/DevExpressXtraReportsUIXtraReportBase_DataSourcetopic).
' To accomplish this, the GetAppointments
' (http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerSchedulerStorageBase_GetAppointmentstopic)
' method is used to get a collection of appointments which fall within the time
' range specified by the GetVisibleIntervals
' (http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerSchedulerViewBase_GetVisibleIntervalstopic)
' method.
' To display custom fields in the report, the custom fields
' (http://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraSchedulerNativeCustomFieldtopic)
' should be exposed as common object properties. So a wrapper class Task is
' implemented solely for this purpose. Using the SetAppointmentFactory
' (http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerAppointmentStorageBase_SetAppointmentFactorytopic)
' method, Scheduler's Appointment objects are replaced with the Task class
' instances. A TaskCollection class holds Task objects and can be used as the
' report's data source.
' Note that you should have a valid license to the
' XtraReports Suite
' (http://documentation.devexpress.com/#XtraReports/CustomDocument2162) to be able
' to use this approach in your application.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E1183
Imports System.Drawing
Imports System.ComponentModel
Imports DevExpress.XtraReports.UI

Namespace PrintingViaReports

    Public Partial Class XtraReport1
        Inherits XtraReport

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub xrLabel6_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
            ' Fill the label with color
            Dim curColor As Color = Color.FromArgb(Integer.Parse(CType(sender, XRLabel).Text))
            CType(sender, XRLabel).BackColor = curColor
            CType(sender, XRLabel).ForeColor = curColor
        End Sub

        Private Sub xrLabel7_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
            Dim curColor As Color = CType(GetCurrentColumnValue("CustomColor"), Color)
            CType(sender, XRLabel).ForeColor = curColor
            CType(sender, XRLabel).Text = CType(sender, XRLabel).Text & Microsoft.VisualBasic.Constants.vbLf & Microsoft.VisualBasic.Constants.vbCr & curColor.Name & " color"
        End Sub
    End Class
End Namespace
