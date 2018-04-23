Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraReports.UserDesigner


Namespace PrintingViaReports
	Partial Public Class Form1
		Inherits Form

		#Region "InitialDataConstants"
		Public Shared RandomInstance As New Random()
		Public Shared Users() As String = {"Peter Dolan", "Ryan Fischer", "Richard Fisher", "Tom Hamlett", "Mark Hamilton", "Steve Lee", "Jimmy Lewis", "Jeffrey W McClain", "Andrew Miller", "Dave Murrel", "Bert Parkins", "Mike Roller", "Ray Shipman", "Paul Bailey", "Brad Barnes", "Carl Lucas", "Jerry Campbell"}
		#End Region

		Public Sub New()
			InitializeComponent()
			schedulerStorage1.SetAppointmentFactory(New TaskFactory())
			FillResources(schedulerStorage1, 5)
			InitAppointments()
			schedulerControl1.Start = DateTime.Now
			HighlightFirstAppointment()
			UpdateControls()

		End Sub

		Private Sub HighlightFirstAppointment()
			Dim abc As AppointmentBaseCollection = schedulerStorage1.GetAppointments(schedulerControl1.ActiveView.GetVisibleIntervals())
			CType(abc(0), Task).CustomColor = Color.IndianRed
		End Sub

		#Region "InitialDataLoad"
		Private Sub FillResources(ByVal storage As SchedulerStorage, ByVal count As Integer)
			Dim resources As ResourceCollection = storage.Resources.Items
			storage.BeginUpdate()
			Try
				Dim cnt As Integer = Math.Min(count, Users.Length)
				For i As Integer = 1 To cnt
					resources.Add(New Resource(Guid.NewGuid(), Users(i - 1)))
				Next i
			Finally
				storage.EndUpdate()
			End Try
		End Sub
		Private Sub InitAppointments()
			Me.schedulerStorage1.Appointments.Mappings.Start = "StartTime"
			Me.schedulerStorage1.Appointments.Mappings.End = "EndTime"
			Me.schedulerStorage1.Appointments.Mappings.Subject = "Subject"
			Me.schedulerStorage1.Appointments.Mappings.AllDay = "AllDay"
			Me.schedulerStorage1.Appointments.Mappings.Description = "Description"
			Me.schedulerStorage1.Appointments.Mappings.Label = "Label"
			Me.schedulerStorage1.Appointments.Mappings.Location = "Location"
			Me.schedulerStorage1.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo"
			Me.schedulerStorage1.Appointments.Mappings.ReminderInfo = "ReminderInfo"
			Me.schedulerStorage1.Appointments.Mappings.ResourceId = "OwnerId"
			Me.schedulerStorage1.Appointments.Mappings.Status = "Status"
			Me.schedulerStorage1.Appointments.Mappings.Type = "EventType"

			Dim customTextMapping As New AppointmentCustomFieldMapping("CustomTextField", "CustomText")
			Dim customColorMapping As New AppointmentCustomFieldMapping("CustomColorField", "CustomColor")
			schedulerStorage1.Appointments.CustomFieldMappings.Add(customTextMapping)
			schedulerStorage1.Appointments.CustomFieldMappings.Add(customColorMapping)

			Dim eventList As New CustomEventList()
			GenerateEvents(eventList)
			Me.schedulerStorage1.Appointments.DataSource = eventList

		End Sub
		Private Sub GenerateEvents(ByVal eventList As CustomEventList)
			Dim count As Integer = schedulerStorage1.Resources.Count
			For i As Integer = 0 To count - 1
				Dim resource As Resource = schedulerStorage1.Resources(i)
				Dim subjPrefix As String = resource.Caption & "'s "
				eventList.Add(CreateEvent(eventList, subjPrefix & "meeting", resource.Id, 2, 5))
				eventList.Add(CreateEvent(eventList, subjPrefix & "travel", resource.Id, 3, 6))
				eventList.Add(CreateEvent(eventList, subjPrefix & "phone call", resource.Id, 0, 10))
			Next i
		End Sub
		Private Function CreateEvent(ByVal eventList As CustomEventList, ByVal subject As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer) As CustomEvent
			Dim apt As New CustomEvent(eventList)

			apt.Subject = subject
			apt.OwnerId = resourceId
			Dim rnd As Random = RandomInstance
			Dim rangeInMinutes As Integer = 60 * 24
			apt.StartTime = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
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
		Private Sub rgrpGrouping_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgrpGrouping.SelectedIndexChanged
			schedulerControl1.GroupType = CType(rgrpGrouping.EditValue, SchedulerGroupType)
		End Sub

		Private Sub schedulerControl_ActiveViewChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles schedulerControl1.ActiveViewChanged
			cbView.EditValue = schedulerControl1.ActiveViewType
		End Sub

		Private Sub cbView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbView.SelectedIndexChanged
			schedulerControl1.ActiveViewType = CType(cbView.EditValue, SchedulerViewType)
		End Sub
		#End Region

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			Dim xr As New XtraReport1()
			' Assign the report's data source which is the collection of Task objects.
			Dim abc As AppointmentBaseCollection = schedulerStorage1.GetAppointments(schedulerControl1.ActiveView.GetVisibleIntervals())
			Dim tc As New TaskCollection()
			tc.AddAppointmentRange(abc)
			xr.DataSource = tc
			' Create a report and preview it.
			xr.CreateDocument()
			Dim designForm As New XRDesignFormEx()
			designForm.OpenReport(xr)
			designForm.DesignPanel.SelectedTabIndex = 1
			designForm.ShowDialog()

		End Sub

		Private Sub schedulerControl1_EditAppointmentFormShowing(ByVal sender As Object, ByVal e As AppointmentFormEventArgs) Handles schedulerControl1.EditAppointmentFormShowing
			Dim apt As Appointment = e.Appointment
			' Required to open the recurrence form via context menu.
			Dim openRecurrenceForm As Boolean = apt.IsRecurring AndAlso schedulerStorage1.Appointments.IsNewAppointment(apt)
			' Create a custom form.
			Dim myForm As New MyAppointmentEditForm(CType(sender, SchedulerControl), apt, openRecurrenceForm)

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

		Private Sub schedulerControl1_AppointmentViewInfoCustomizing(ByVal sender As Object, ByVal e As AppointmentViewInfoCustomizingEventArgs) Handles schedulerControl1.AppointmentViewInfoCustomizing
			Dim custColor As Color = CType(e.ViewInfo.Appointment.CustomFields("CustomColorField"), Color)
			' Change the appointment color if the custom color value is not default.
			If custColor <> Color.Transparent Then
				e.ViewInfo.Appearance.BackColor = custColor
			End If
		End Sub

		Private Sub schedulerControl1_InitNewAppointment(ByVal sender As Object, ByVal e As AppointmentEventArgs) Handles schedulerControl1.InitNewAppointment
			' Set default custom color value for all newly created appointments.
			e.Appointment.CustomFields("CustomColorField") = Color.Transparent
		End Sub

	End Class
End Namespace