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
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraReports.UserDesigner

Namespace PrintingViaReports

    Public Partial Class Form1
        Inherits Form

#Region "InitialDataConstants"
        Public Shared RandomInstance As Random = New Random()

        Public Shared Users As String() = New String() {"Peter Dolan", "Ryan Fischer", "Richard Fisher", "Tom Hamlett", "Mark Hamilton", "Steve Lee", "Jimmy Lewis", "Jeffrey W McClain", "Andrew Miller", "Dave Murrel", "Bert Parkins", "Mike Roller", "Ray Shipman", "Paul Bailey", "Brad Barnes", "Carl Lucas", "Jerry Campbell"}

#End Region
        Public Sub New()
            InitializeComponent()
            InitializeTasks()
            UpdateControls()
        End Sub

#Region "#initializetasks"
        Private Sub InitializeTasks()
            schedulerStorage1.SetAppointmentFactory(New TaskFactory())
            FillResources(schedulerStorage1, 5)
            SetMappings()
            SetDataSource()
            HighlightFirstAppointment()
        End Sub

#End Region  ' #initializetasks
        Private Sub SetMappings()
            schedulerStorage1.Appointments.Mappings.Start = "StartTime"
            schedulerStorage1.Appointments.Mappings.End = "EndTime"
            schedulerStorage1.Appointments.Mappings.Subject = "Subject"
            schedulerStorage1.Appointments.Mappings.AllDay = "AllDay"
            schedulerStorage1.Appointments.Mappings.Description = "Description"
            schedulerStorage1.Appointments.Mappings.Label = "Label"
            schedulerStorage1.Appointments.Mappings.Location = "Location"
            schedulerStorage1.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo"
            schedulerStorage1.Appointments.Mappings.ReminderInfo = "ReminderInfo"
            schedulerStorage1.Appointments.Mappings.ResourceId = "OwnerId"
            schedulerStorage1.Appointments.Mappings.Status = "Status"
            schedulerStorage1.Appointments.Mappings.Type = "EventType"
            Dim customTextMapping As AppointmentCustomFieldMapping = New AppointmentCustomFieldMapping("CustomTextField", "CustomText")
            Dim customColorMapping As AppointmentCustomFieldMapping = New AppointmentCustomFieldMapping("CustomColorField", "CustomColor")
            schedulerStorage1.Appointments.CustomFieldMappings.Add(customTextMapping)
            schedulerStorage1.Appointments.CustomFieldMappings.Add(customColorMapping)
        End Sub

        Private Sub SetDataSource()
            Dim eventList As CustomEventList = New CustomEventList()
            GenerateEvents(eventList)
            schedulerStorage1.Appointments.DataSource = eventList
        End Sub

#Region "#highlight"
        Private Sub HighlightFirstAppointment()
            schedulerControl1.Start = Date.Now
            Dim abc As AppointmentBaseCollection = schedulerStorage1.GetAppointments(schedulerControl1.ActiveView.GetVisibleIntervals())
            If abc.Count > 0 Then CType(abc(0), Task).CustomColor = Color.IndianRed
        End Sub

#End Region  ' #highlight
#Region "InitialDataLoad"
        Private Sub FillResources(ByVal storage As SchedulerStorage, ByVal count As Integer)
            Dim resources As ResourceCollection = storage.Resources.Items
            storage.BeginUpdate()
            Try
                Dim cnt As Integer = Math.Min(count, Users.Length)
                For i As Integer = 1 To cnt
                    resources.Add(New Resource(Guid.NewGuid(), Users(i - 1)))
                Next
            Finally
                storage.EndUpdate()
            End Try
        End Sub

        Private Sub GenerateEvents(ByVal eventList As CustomEventList)
            Dim count As Integer = schedulerStorage1.Resources.Count
            For i As Integer = 0 To count - 1
                Dim resource As Resource = schedulerStorage1.Resources(i)
                Dim subjPrefix As String = resource.Caption & "'s "
                eventList.Add(CreateEvent(eventList, subjPrefix & "meeting", resource.Id, 2, 5))
                eventList.Add(CreateEvent(eventList, subjPrefix & "travel", resource.Id, 3, 6))
                eventList.Add(CreateEvent(eventList, subjPrefix & "phone call", resource.Id, 0, 10))
            Next
        End Sub

        Private Function CreateEvent(ByVal eventList As CustomEventList, ByVal subject As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer) As CustomEvent
            Dim apt As CustomEvent = New CustomEvent(eventList)
            apt.Subject = subject
            apt.OwnerId = resourceId
            Dim rnd As Random = RandomInstance
            Dim rangeInMinutes As Integer = 60 * 24
            apt.StartTime = Date.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
            apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4))
            apt.Status = status
            apt.Label = label
            Dim duration As TimeSpan = apt.EndTime - apt.StartTime
            apt.Description = subject & " lasts for " & duration.ToString()
            apt.CustomText = "TEST"
            apt.CustomColor = Color.Transparent
            Return apt
        End Function

#End Region
#Region "Update Controls"
        Private Sub UpdateControls()
            cbView.EditValue = schedulerControl1.ActiveViewType
            rgrpGrouping.EditValue = schedulerControl1.GroupType
        End Sub

        Private Sub rgrpGrouping_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            schedulerControl1.GroupType = CType(rgrpGrouping.EditValue, SchedulerGroupType)
        End Sub

        Private Sub schedulerControl_ActiveViewChanged(ByVal sender As Object, ByVal e As EventArgs)
            cbView.EditValue = schedulerControl1.ActiveViewType
        End Sub

        Private Sub cbView_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            schedulerControl1.ActiveViewType = CType(cbView.EditValue, SchedulerViewType)
        End Sub

#End Region
        Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim xr As XtraReport1 = New XtraReport1()
            ' Assign the report's data source which is the collection of Task objects.
            Dim abc As AppointmentBaseCollection = schedulerStorage1.GetAppointments(schedulerControl1.ActiveView.GetVisibleIntervals())
            Dim tc As TaskCollection = New TaskCollection()
            tc.AddAppointmentRange(abc)
            xr.DataSource = tc
            ' Create a report and preview it.
            xr.CreateDocument()
            Dim designForm As XRDesignFormEx = New XRDesignFormEx()
            designForm.OpenReport(xr)
            designForm.DesignPanel.SelectedTabIndex = 1
            designForm.ShowDialog()
        End Sub

        Private Sub schedulerControl1_EditAppointmentFormShowing(ByVal sender As Object, ByVal e As AppointmentFormEventArgs)
            Dim apt As Appointment = e.Appointment
            ' Required to open the recurrence form via context menu.
            Dim openRecurrenceForm As Boolean = apt.IsRecurring AndAlso schedulerStorage1.Appointments.IsNewAppointment(apt)
            ' Create a custom form.
            Dim myForm As MyAppointmentEditForm = New MyAppointmentEditForm(CType(sender, SchedulerControl), apt, openRecurrenceForm)
            Try
                ' Required for skin support.
                myForm.LookAndFeel.ParentLookAndFeel = schedulerControl1.LookAndFeel
                e.DialogResult = myForm.ShowDialog()
                schedulerControl1.Refresh()
                e.Handled = True
            Finally
                myForm.Dispose()
            End Try
        End Sub

        Private Sub schedulerControl1_AppointmentViewInfoCustomizing(ByVal sender As Object, ByVal e As AppointmentViewInfoCustomizingEventArgs)
            Dim custColor As Color = CType(e.ViewInfo.Appointment.CustomFields("CustomColorField"), Color)
            ' Change the appointment color if the custom color value is not default.
            If custColor <> Color.Transparent Then e.ViewInfo.Appearance.BackColor = custColor
        End Sub

        Private Sub schedulerControl1_InitNewAppointment(ByVal sender As Object, ByVal e As AppointmentEventArgs)
            ' Set default custom color value for all newly created appointments.
            e.Appointment.CustomFields("CustomColorField") = Color.Transparent
        End Sub
    End Class
End Namespace
