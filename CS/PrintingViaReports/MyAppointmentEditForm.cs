using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.UI;

namespace PrintingViaReports {
    public partial class MyAppointmentEditForm : DevExpress.XtraEditors.XtraForm {

        SchedulerControl control;
        Appointment apt;
        bool openRecurrenceForm = false;
        int suspendUpdateCount;


        // The MyAppointmentFormController class is inherited from
        // the AppointmentFormController to add custom properties.
        // See its declaration below.
        MyAppointmentFormController controller;

        protected AppointmentStorage Appointments {
            get { return control.Storage.Appointments; }
        }
        protected bool IsUpdateSuspended { get { return suspendUpdateCount > 0; } }


        public MyAppointmentEditForm(SchedulerControl control, Appointment apt, bool openRecurrenceForm) {
            this.openRecurrenceForm = openRecurrenceForm;
            this.controller = new MyAppointmentFormController(control, apt);
            this.apt = apt;
            this.control = control;
            InitializeComponent();
            UpdateForm();
        }

        #region Recurrence
        private void MyAppointmentEditForm_Activated(object sender, System.EventArgs e) {
            // Required to show the recurrence form.
            if (openRecurrenceForm) {
                openRecurrenceForm = false;
                OnRecurrenceButton();
            }
        }
        private void btnRecurrence_Click(object sender, System.EventArgs e) {
            OnRecurrenceButton();
        }

        void OnRecurrenceButton() {
            ShowRecurrenceForm();
        }

        void ShowRecurrenceForm() {

            if (!control.SupportsRecurrence)
                return;

            // Prepare to edit the appointment's recurrence.
            Appointment editedAptCopy = controller.EditedAppointmentCopy;
            Appointment editedPattern = controller.EditedPattern;
            Appointment patternCopy = controller.PrepareToRecurrenceEdit();

            AppointmentRecurrenceForm dlg = new AppointmentRecurrenceForm(patternCopy, control.OptionsView.FirstDayOfWeek,controller);

            // Required for skin support.
            dlg.LookAndFeel.ParentLookAndFeel = this.LookAndFeel.ParentLookAndFeel;

            DialogResult result = dlg.ShowDialog(this);
            dlg.Dispose();

            if (result == DialogResult.Abort)
                controller.RemoveRecurrence();
            else
                if (result == DialogResult.OK) {
                    controller.ApplyRecurrence(patternCopy);
                    if (controller.EditedAppointmentCopy != editedAptCopy)
                        UpdateForm();
                }
            UpdateIntervalControls();
        }

        #endregion

        #region Form control events

        private void dtStart_EditValueChanged(object sender, System.EventArgs e) {
            if (!IsUpdateSuspended)
                controller.DisplayStart = dtStart.DateTime.Date + timeStart.Time.TimeOfDay;
            UpdateIntervalControls();
        }

        private void timeStart_EditValueChanged(object sender, System.EventArgs e) {
            if (!IsUpdateSuspended)
                controller.DisplayStart = dtStart.DateTime.Date + timeStart.Time.TimeOfDay;
            UpdateIntervalControls();
        }
        private void timeEnd_EditValueChanged(object sender, System.EventArgs e) {
            if (IsUpdateSuspended) return;
            if (IsIntervalValid())
                controller.DisplayEnd = dtEnd.DateTime.Date + timeEnd.Time.TimeOfDay;
            else
                timeEnd.EditValue = new DateTime(controller.DisplayEnd.TimeOfDay.Ticks);
;
        }
        private void dtEnd_EditValueChanged(object sender, System.EventArgs e) {
            if (IsUpdateSuspended) return;
            if (IsIntervalValid())
                controller.DisplayEnd = dtEnd.DateTime.Date + timeEnd.Time.TimeOfDay;
            else
                dtEnd.EditValue = controller.DisplayEnd.Date;
        }
        bool IsIntervalValid() {
            DateTime start = dtStart.DateTime + timeStart.Time.TimeOfDay;
            DateTime end = dtEnd.DateTime + timeEnd.Time.TimeOfDay;
            return end >= start;
        }

        private void checkAllDay_CheckedChanged(object sender, System.EventArgs e) {
            controller.AllDay = this.checkAllDay.Checked;
            if (!IsUpdateSuspended)
                UpdateAppointmentStatus();

            UpdateIntervalControls();
        }
#endregion

        #region Updating Form
        protected void SuspendUpdate() {
            suspendUpdateCount++;
        }
        protected void ResumeUpdate() {
            if (suspendUpdateCount > 0)
                suspendUpdateCount--;
        }
        
        void UpdateForm() {
			SuspendUpdate();
			try {
				txSubject.Text = controller.Subject;
				edStatus.Status = Appointments.Statuses[controller.StatusId];
				edLabel.Label = Appointments.Labels[controller.LabelId];
			
				dtStart.DateTime = controller.DisplayStart.Date;
				dtEnd.DateTime= controller.DisplayEnd.Date;

                timeStart.Time = new DateTime(controller.DisplayStart.TimeOfDay.Ticks);
                timeEnd.Time = new DateTime(controller.DisplayEnd.TimeOfDay.Ticks);
				checkAllDay.Checked = controller.AllDay;

				edStatus.Storage = control.Storage;
				edLabel.Storage = control.Storage;

                edtDescription.Text = controller.Description;

				txCustomText.Text = controller.CustomText;
				edtCustomColor.Color = controller.CustomColor;
			} finally {
				ResumeUpdate();
			}
			UpdateIntervalControls();
		}

        protected virtual void UpdateIntervalControls() {
            if (IsUpdateSuspended)
                return;

            SuspendUpdate();
            try {
                dtStart.EditValue = controller.DisplayStart.Date;
                dtEnd.EditValue = controller.DisplayEnd.Date;
                timeStart.EditValue = new DateTime(controller.DisplayStart.TimeOfDay.Ticks);
                timeEnd.EditValue = new DateTime(controller.DisplayEnd.TimeOfDay.Ticks);


                timeStart.Visible = !controller.AllDay;
                timeEnd.Visible = !controller.AllDay;
                timeStart.Enabled = !controller.AllDay;
                timeEnd.Enabled = !controller.AllDay;

            }
            finally {
                ResumeUpdate();
            }
        }
        
        void UpdateAppointmentStatus() {
            AppointmentStatus currentStatus = edStatus.Status;
            AppointmentStatus newStatus = controller.UpdateAppointmentStatus(currentStatus);
            if (newStatus != currentStatus)
                edStatus.Status = newStatus;
        }

        #endregion

        #region Save changes
        private void btnOK_Click(object sender, System.EventArgs e) {
            // Required to check the appointment for conflicts.
            if (!controller.IsConflictResolved())
                return;

            controller.Subject = txSubject.Text;
            controller.SetStatus(edStatus.Status);
            controller.SetLabel(edLabel.Label);
            controller.AllDay = this.checkAllDay.Checked;
            controller.DisplayStart = this.dtStart.DateTime.Date + this.timeStart.Time.TimeOfDay;
            controller.DisplayEnd = this.dtEnd.DateTime.Date + this.timeEnd.Time.TimeOfDay;
            controller.Description = edtDescription.Text;
            controller.CustomText = txCustomText.Text;
            controller.CustomColor = edtCustomColor.Color;

            // Save all changes of the editing appointment.
            controller.ApplyChanges();
        }
        #endregion

        #region MyAppointmentFormController
   
	public class MyAppointmentFormController : AppointmentFormController {

        public string CustomText { get { return (string)EditedAppointmentCopy.CustomFields["CustomTextField"]; } set { EditedAppointmentCopy.CustomFields["CustomTextField"] = value; } }
        public Color CustomColor { get { return (Color)EditedAppointmentCopy.CustomFields["CustomColorField"]; } set { EditedAppointmentCopy.CustomFields["CustomColorField"] = value; } }

        string SourceCustomText { get { return (string)SourceAppointment.CustomFields["CustomTextField"]; } set { SourceAppointment.CustomFields["CustomTextField"] = value; } }
        Color SourceCustomColor { get { return (Color)SourceAppointment.CustomFields["CustomColorField"]; } set { SourceAppointment.CustomFields["CustomColorField"] = value; } }

        public MyAppointmentFormController(SchedulerControl control, Appointment apt) : base(control, apt) {
        }

		public override bool IsAppointmentChanged() {
			if(base.IsAppointmentChanged())
				return true;
			return SourceCustomText != CustomText ||
				SourceCustomColor != CustomColor;
		}

		protected override void ApplyCustomFieldsValues() {
			SourceCustomText = CustomText;
			SourceCustomColor = CustomColor;
		}
    }
        #endregion
}
}