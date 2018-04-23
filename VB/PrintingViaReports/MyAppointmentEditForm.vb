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
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.UI

Namespace PrintingViaReports
    Partial Public Class MyAppointmentEditForm
        Inherits DevExpress.XtraEditors.XtraForm

        Private control As SchedulerControl
        Private apt As Appointment
        Private openRecurrenceForm As Boolean = False
        Private suspendUpdateCount As Integer


        ' The MyAppointmentFormController class is inherited from
        ' the AppointmentFormController to add custom properties.
        ' See its declaration below.
        Private controller As MyAppointmentFormController

        Protected ReadOnly Property Appointments() As AppointmentStorage
            Get
                Return control.Storage.Appointments
            End Get
        End Property
        Protected ReadOnly Property IsUpdateSuspended() As Boolean
            Get
                Return suspendUpdateCount > 0
            End Get
        End Property


        Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment, ByVal openRecurrenceForm As Boolean)
            Me.openRecurrenceForm = openRecurrenceForm
            Me.controller = New MyAppointmentFormController(control, apt)
            Me.apt = apt
            Me.control = control
            InitializeComponent()
            UpdateForm()
        End Sub

        #Region "Recurrence"
        Private Sub MyAppointmentEditForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs)
            ' Required to show the recurrence form.
            If openRecurrenceForm Then
                openRecurrenceForm = False
                OnRecurrenceButton()
            End If
        End Sub
        Private Sub btnRecurrence_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecurrence.Click
            OnRecurrenceButton()
        End Sub

        Private Sub OnRecurrenceButton()
            ShowRecurrenceForm()
        End Sub

        Private Sub ShowRecurrenceForm()

            If Not control.SupportsRecurrence Then
                Return
            End If

            ' Prepare to edit the appointment's recurrence.
            Dim editedAptCopy As Appointment = controller.EditedAppointmentCopy
            Dim editedPattern As Appointment = controller.EditedPattern
            Dim patternCopy As Appointment = controller.PrepareToRecurrenceEdit()

            Dim dlg As New AppointmentRecurrenceForm(patternCopy, control.OptionsView.FirstDayOfWeek,controller)

            ' Required for skin support.
            dlg.LookAndFeel.ParentLookAndFeel = Me.LookAndFeel.ParentLookAndFeel

            Dim result As DialogResult = dlg.ShowDialog(Me)
            dlg.Dispose()

            If result = DialogResult.Abort Then
                controller.RemoveRecurrence()
            Else
                If result = DialogResult.OK Then
                    controller.ApplyRecurrence(patternCopy)
                    If controller.EditedAppointmentCopy IsNot editedAptCopy Then
                        UpdateForm()
                    End If
                End If
            End If
            UpdateIntervalControls()
        End Sub

        #End Region

        #Region "Form control events"

        Private Sub dtStart_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtStart.EditValueChanged
            If Not IsUpdateSuspended Then
                controller.DisplayStart = dtStart.DateTime.Date + timeStart.Time.TimeOfDay
            End If
            UpdateIntervalControls()
        End Sub

        Private Sub timeStart_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles timeStart.EditValueChanged
            If Not IsUpdateSuspended Then
                controller.DisplayStart = dtStart.DateTime.Date + timeStart.Time.TimeOfDay
            End If
            UpdateIntervalControls()
        End Sub
        Private Sub timeEnd_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles timeEnd.EditValueChanged
            If IsUpdateSuspended Then
                Return
            End If
            If IsIntervalValid() Then
                controller.DisplayEnd = dtEnd.DateTime.Date + timeEnd.Time.TimeOfDay
            Else
                timeEnd.EditValue = New Date(controller.DisplayEnd.TimeOfDay.Ticks)
            End If

        End Sub
        Private Sub dtEnd_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtEnd.EditValueChanged
            If IsUpdateSuspended Then
                Return
            End If
            If IsIntervalValid() Then
                controller.DisplayEnd = dtEnd.DateTime.Date + timeEnd.Time.TimeOfDay
            Else
                dtEnd.EditValue = controller.DisplayEnd.Date
            End If
        End Sub
        Private Function IsIntervalValid() As Boolean
            Dim start As Date = dtStart.DateTime + timeStart.Time.TimeOfDay
            Dim [end] As Date = dtEnd.DateTime + timeEnd.Time.TimeOfDay
            Return [end] >= start
        End Function

        Private Sub checkAllDay_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkAllDay.CheckedChanged
            controller.AllDay = Me.checkAllDay.Checked
            If Not IsUpdateSuspended Then
                UpdateAppointmentStatus()
            End If

            UpdateIntervalControls()
        End Sub
#End Region

        #Region "Updating Form"
        Protected Sub SuspendUpdate()
            suspendUpdateCount += 1
        End Sub
        Protected Sub ResumeUpdate()
            If suspendUpdateCount > 0 Then
                suspendUpdateCount -= 1
            End If
        End Sub

        Private Sub UpdateForm()
            SuspendUpdate()
            Try
                txSubject.Text = controller.Subject
                edStatus.Status = Appointments.Statuses.GetById(controller.StatusKey)
                edLabel.Label = Appointments.Labels.GetById(controller.LabelKey)

                dtStart.DateTime = controller.DisplayStart.Date
                dtEnd.DateTime= controller.DisplayEnd.Date

                timeStart.Time = New Date(controller.DisplayStart.TimeOfDay.Ticks)
                timeEnd.Time = New Date(controller.DisplayEnd.TimeOfDay.Ticks)
                checkAllDay.Checked = controller.AllDay

                edStatus.Storage = control.Storage
                edLabel.Storage = control.Storage

                edtDescription.Text = controller.Description

                txCustomText.Text = controller.CustomText
                edtCustomColor.Color = controller.CustomColor
            Finally
                ResumeUpdate()
            End Try
            UpdateIntervalControls()
        End Sub

        Protected Overridable Sub UpdateIntervalControls()
            If IsUpdateSuspended Then
                Return
            End If

            SuspendUpdate()
            Try
                dtStart.EditValue = controller.DisplayStart.Date
                dtEnd.EditValue = controller.DisplayEnd.Date
                timeStart.EditValue = New Date(controller.DisplayStart.TimeOfDay.Ticks)
                timeEnd.EditValue = New Date(controller.DisplayEnd.TimeOfDay.Ticks)


                timeStart.Visible = Not controller.AllDay
                timeEnd.Visible = Not controller.AllDay
                timeStart.Enabled = Not controller.AllDay
                timeEnd.Enabled = Not controller.AllDay

            Finally
                ResumeUpdate()
            End Try
        End Sub

        Private Sub UpdateAppointmentStatus()
            Dim currentStatus As AppointmentStatus = edStatus.Status
            Dim newStatus As AppointmentStatus = controller.UpdateAppointmentStatus(currentStatus)
            If newStatus IsNot currentStatus Then
                edStatus.Status = newStatus
            End If
        End Sub

        #End Region

        #Region "Save changes"
        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
            ' Required to check the appointment for conflicts.
            If Not controller.IsConflictResolved() Then
                Return
            End If

            controller.Subject = txSubject.Text
            controller.SetStatus(edStatus.Status)
            controller.SetLabel(edLabel.Label)
            controller.AllDay = Me.checkAllDay.Checked
            controller.DisplayStart = Me.dtStart.DateTime.Date + Me.timeStart.Time.TimeOfDay
            controller.DisplayEnd = Me.dtEnd.DateTime.Date + Me.timeEnd.Time.TimeOfDay
            controller.Description = edtDescription.Text
            controller.CustomText = txCustomText.Text
            controller.CustomColor = edtCustomColor.Color

            ' Save all changes of the editing appointment.
            controller.ApplyChanges()
        End Sub
        #End Region

        #Region "MyAppointmentFormController"

    Public Class MyAppointmentFormController
        Inherits AppointmentFormController

        Public Property CustomText() As String
            Get
                Return CStr(EditedAppointmentCopy.CustomFields("CustomTextField"))
            End Get
            Set(ByVal value As String)
                EditedAppointmentCopy.CustomFields("CustomTextField") = value
            End Set
        End Property
        Public Property CustomColor() As Color
            Get
                Return CType(EditedAppointmentCopy.CustomFields("CustomColorField"), Color)
            End Get
            Set(ByVal value As Color)
                EditedAppointmentCopy.CustomFields("CustomColorField") = value
            End Set
        End Property

        Private Property SourceCustomText() As String
            Get
                Return CStr(SourceAppointment.CustomFields("CustomTextField"))
            End Get
            Set(ByVal value As String)
                SourceAppointment.CustomFields("CustomTextField") = value
            End Set
        End Property
        Private Property SourceCustomColor() As Color
            Get
                Return CType(SourceAppointment.CustomFields("CustomColorField"), Color)
            End Get
            Set(ByVal value As Color)
                SourceAppointment.CustomFields("CustomColorField") = value
            End Set
        End Property

        Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment)
            MyBase.New(control, apt)
        End Sub

        Public Overrides Function IsAppointmentChanged() As Boolean
            If MyBase.IsAppointmentChanged() Then
                Return True
            End If
            Return SourceCustomText <> CustomText OrElse SourceCustomColor <> CustomColor
        End Function

        Protected Overrides Sub ApplyCustomFieldsValues()
            SourceCustomText = CustomText
            SourceCustomColor = CustomColor
        End Sub
    End Class
        #End Region
    End Class
End Namespace