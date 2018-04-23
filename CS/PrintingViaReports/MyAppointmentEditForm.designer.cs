// Developer Express Code Central Example:
// Printing appointment details using the XtraReports Suite
// 
// This example illustrates how you can print the appointment details for the
// appointments currently displayed in the Scheduler by means of the XtraReports
// Suite.
// The key point is to obtain a collection of appointments and assign it to
// the report's DataSource
// (http://documentation.devexpress.com/#XtraReports/DevExpressXtraReportsUIXtraReportBase_DataSourcetopic).
// To accomplish this, the GetAppointments
// (http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerSchedulerStorageBase_GetAppointmentstopic)
// method is used to get a collection of appointments which fall within the time
// range specified by the GetVisibleIntervals
// (http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerSchedulerViewBase_GetVisibleIntervalstopic)
// method.
// To display custom fields in the report, the custom fields
// (http://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraSchedulerNativeCustomFieldtopic)
// should be exposed as common object properties. So a wrapper class Task is
// implemented solely for this purpose. Using the SetAppointmentFactory
// (http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerAppointmentStorageBase_SetAppointmentFactorytopic)
// method, Scheduler's Appointment objects are replaced with the Task class
// instances. A TaskCollection class holds Task objects and can be used as the
// report's data source.
// Note that you should have a valid license to the
// XtraReports Suite
// (http://documentation.devexpress.com/#XtraReports/CustomDocument2162) to be able
// to use this approach in your application.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E1183


namespace PrintingViaReports {
    partial class MyAppointmentEditForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.checkAllDay = new DevExpress.XtraEditors.CheckEdit();
            this.timeEnd = new DevExpress.XtraEditors.TimeEdit();
            this.timeStart = new DevExpress.XtraEditors.TimeEdit();
            this.dtEnd = new DevExpress.XtraEditors.DateEdit();
            this.dtStart = new DevExpress.XtraEditors.DateEdit();
            this.txCustomText = new DevExpress.XtraEditors.TextEdit();
            this.lblCustomColor = new System.Windows.Forms.Label();
            this.lblCustomText = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblLabel = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.edStatus = new DevExpress.XtraScheduler.UI.AppointmentStatusEdit();
            this.edLabel = new DevExpress.XtraScheduler.UI.AppointmentLabelEdit();
            this.txSubject = new DevExpress.XtraEditors.TextEdit();
            this.lblSubject = new System.Windows.Forms.Label();
            this.btnRecurrence = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.lblEnd = new System.Windows.Forms.Label();
            this.edtCustomColor = new DevExpress.XtraEditors.ColorEdit();
            this.edtDescription = new DevExpress.XtraEditors.MemoExEdit();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.checkAllDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txCustomText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edLabel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtCustomColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // checkAllDay
            // 
            this.checkAllDay.Location = new System.Drawing.Point(94, 92);
            this.checkAllDay.Name = "checkAllDay";
            this.checkAllDay.Properties.Caption = "All day event";
            this.checkAllDay.Size = new System.Drawing.Size(88, 19);
            this.checkAllDay.TabIndex = 23;
            this.checkAllDay.CheckedChanged += new System.EventHandler(this.checkAllDay_CheckedChanged);
            // 
            // timeEnd
            // 
            
            this.timeEnd.Location = new System.Drawing.Point(196, 68);
            this.timeEnd.Name = "timeEnd";
            this.timeEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEnd.Size = new System.Drawing.Size(90, 20);
            this.timeEnd.TabIndex = 21;
            this.timeEnd.EditValueChanged += new System.EventHandler(this.timeEnd_EditValueChanged);
            // 
            // timeStart
            // 
            
            this.timeStart.Location = new System.Drawing.Point(196, 44);
            this.timeStart.Name = "timeStart";
            this.timeStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeStart.Size = new System.Drawing.Size(90, 20);
            this.timeStart.TabIndex = 19;
            this.timeStart.EditValueChanged += new System.EventHandler(this.timeStart_EditValueChanged);
            // 
            // dtEnd
            // 
            
            this.dtEnd.Location = new System.Drawing.Point(94, 68);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtEnd.Size = new System.Drawing.Size(96, 20);
            this.dtEnd.TabIndex = 20;
            this.dtEnd.EditValueChanged += new System.EventHandler(this.dtEnd_EditValueChanged);
            // 
            // dtStart
            // 
            
            this.dtStart.Location = new System.Drawing.Point(94, 44);
            this.dtStart.Name = "dtStart";
            this.dtStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtStart.Size = new System.Drawing.Size(96, 20);
            this.dtStart.TabIndex = 18;
            this.dtStart.EditValueChanged += new System.EventHandler(this.dtStart_EditValueChanged);
            // 
            // txCustomText
            // 
            this.txCustomText.EditValue = "";
            this.txCustomText.Location = new System.Drawing.Point(94, 192);
            this.txCustomText.Name = "txCustomText";
            this.txCustomText.Size = new System.Drawing.Size(192, 20);
            this.txCustomText.TabIndex = 26;
            // 
            // lblCustomColor
            // 
            this.lblCustomColor.Location = new System.Drawing.Point(6, 220);
            this.lblCustomColor.Name = "lblCustomColor";
            this.lblCustomColor.Size = new System.Drawing.Size(80, 19);
            this.lblCustomColor.TabIndex = 35;
            this.lblCustomColor.Text = "Custom color:";
            // 
            // lblCustomText
            // 
            this.lblCustomText.Location = new System.Drawing.Point(6, 195);
            this.lblCustomText.Name = "lblCustomText";
            this.lblCustomText.Size = new System.Drawing.Size(80, 19);
            this.lblCustomText.TabIndex = 34;
            this.lblCustomText.Text = "Custom text:";
            // 
            // lblStart
            // 
            this.lblStart.Location = new System.Drawing.Point(6, 47);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(56, 18);
            this.lblStart.TabIndex = 33;
            this.lblStart.Text = "Start:";
            // 
            // lblLabel
            // 
            this.lblLabel.Location = new System.Drawing.Point(6, 143);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(48, 19);
            this.lblLabel.TabIndex = 31;
            this.lblLabel.Text = "Label:";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(6, 118);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 18);
            this.lblStatus.TabIndex = 28;
            this.lblStatus.Text = "Status:";
            // 
            // edStatus
            // 
            this.edStatus.Location = new System.Drawing.Point(94, 115);
            this.edStatus.Name = "edStatus";
            this.edStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edStatus.Size = new System.Drawing.Size(192, 20);
            this.edStatus.TabIndex = 24;
            // 
            // edLabel
            // 
            this.edLabel.Location = new System.Drawing.Point(94, 140);
            this.edLabel.Name = "edLabel";
            this.edLabel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edLabel.Size = new System.Drawing.Size(192, 20);
            this.edLabel.TabIndex = 25;
            // 
            // txSubject
            // 
            this.txSubject.EditValue = "";
            this.txSubject.Location = new System.Drawing.Point(94, 12);
            this.txSubject.Name = "txSubject";
            this.txSubject.Size = new System.Drawing.Size(192, 20);
            this.txSubject.TabIndex = 17;
            // 
            // lblSubject
            // 
            this.lblSubject.Location = new System.Drawing.Point(6, 13);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(48, 18);
            this.lblSubject.TabIndex = 22;
            this.lblSubject.Text = "Subject:";
            // 
            // btnRecurrence
            // 
            this.btnRecurrence.Location = new System.Drawing.Point(206, 259);
            this.btnRecurrence.Name = "btnRecurrence";
            this.btnRecurrence.Size = new System.Drawing.Size(80, 27);
            this.btnRecurrence.TabIndex = 32;
            this.btnRecurrence.Text = "Recurrence";
            this.btnRecurrence.Click += new System.EventHandler(this.btnRecurrence_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(105, 259);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(9, 259);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 27);
            this.btnOK.TabIndex = 29;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblEnd
            // 
            this.lblEnd.Location = new System.Drawing.Point(6, 71);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(48, 18);
            this.lblEnd.TabIndex = 36;
            this.lblEnd.Text = "End:";
            // 
            // edtCustomColor
            // 
            this.edtCustomColor.EditValue = System.Drawing.Color.Empty;
            this.edtCustomColor.Location = new System.Drawing.Point(94, 217);
            this.edtCustomColor.Name = "edtCustomColor";
            this.edtCustomColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edtCustomColor.Size = new System.Drawing.Size(192, 20);
            this.edtCustomColor.TabIndex = 37;
            // 
            // edtDescription
            // 
            this.edtDescription.Location = new System.Drawing.Point(94, 166);
            this.edtDescription.Name = "edtDescription";
            this.edtDescription.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edtDescription.Size = new System.Drawing.Size(192, 20);
            this.edtDescription.TabIndex = 38;
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(9, 173);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(57, 13);
            this.lblDescription.TabIndex = 39;
            this.lblDescription.Text = "Description:";
            // 
            // MyAppointmentEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 293);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.edtDescription);
            this.Controls.Add(this.edtCustomColor);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.checkAllDay);
            this.Controls.Add(this.timeEnd);
            this.Controls.Add(this.timeStart);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.txCustomText);
            this.Controls.Add(this.lblCustomColor);
            this.Controls.Add(this.lblCustomText);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.edStatus);
            this.Controls.Add(this.edLabel);
            this.Controls.Add(this.txSubject);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.btnRecurrence);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MyAppointmentEditForm";
            this.Text = "My Appointment Form";
            ((System.ComponentModel.ISupportInitialize)(this.checkAllDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txCustomText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edLabel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtCustomColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtDescription.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit checkAllDay;
        private DevExpress.XtraEditors.TimeEdit timeEnd;
        private DevExpress.XtraEditors.TimeEdit timeStart;
        private DevExpress.XtraEditors.DateEdit dtEnd;
        private DevExpress.XtraEditors.DateEdit dtStart;
        private DevExpress.XtraEditors.TextEdit txCustomText;
        private System.Windows.Forms.Label lblCustomColor;
        private System.Windows.Forms.Label lblCustomText;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.Label lblStatus;
        private DevExpress.XtraScheduler.UI.AppointmentStatusEdit edStatus;
        private DevExpress.XtraScheduler.UI.AppointmentLabelEdit edLabel;
        private DevExpress.XtraEditors.TextEdit txSubject;
        private System.Windows.Forms.Label lblSubject;
        private DevExpress.XtraEditors.SimpleButton btnRecurrence;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.Label lblEnd;
        private DevExpress.XtraEditors.ColorEdit edtCustomColor;
        private DevExpress.XtraEditors.MemoExEdit edtDescription;
        private DevExpress.XtraEditors.LabelControl lblDescription;
    }
}