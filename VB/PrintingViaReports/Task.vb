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
Imports System.Collections.Generic
Imports DevExpress.XtraScheduler
Imports System.Drawing

'using DevExpress.XtraScheduler.Native;
Namespace PrintingViaReports

#Region "#appointmentfactory"
    Public Class TaskFactory
        Implements IAppointmentFactory

        Public Function CreateAppointment(ByVal type As AppointmentType) As Appointment Implements IAppointmentFactory.CreateAppointment
            Dim task As Task = New Task(type)
            Return task
        End Function
    End Class

    Public Class Task
        Inherits Appointment

        Public Sub New()
        End Sub

        Public Sub New(ByVal type As AppointmentType)
            MyBase.New(type)
        End Sub

        ' Convert custom fields into the Task properties
        Public Property CustomText As String
            Get
                Return CStr(CustomFields("CustomTextField"))
            End Get

            Set(ByVal value As String)
                CustomFields("CustomTextField") = value
            End Set
        End Property

        Public Property CustomColor As Color
            Get
                Return CType(CustomFields("CustomColorField"), Color)
            End Get

            Set(ByVal value As Color)
                CustomFields("CustomColorField") = value
            End Set
        End Property

        Public ReadOnly Property CustomColorARGB As Integer
            Get
                Return CType(CustomFields("CustomColorField"), Color).ToArgb()
            End Get
        End Property
    End Class

    Public Class TaskCollection
        Inherits List(Of Task)

        Public Sub AddAppointment(ByVal appointment As Appointment)
            Dim task As Task = CType(appointment, Task)
            Add(task)
        End Sub

        Public Overridable Sub AddAppointmentRange(ByVal collection As AppointmentBaseCollection)
            For Each item As Appointment In collection
                AddAppointment(item)
            Next
        End Sub
    End Class
#End Region  ' #appointmentfactory
End Namespace
