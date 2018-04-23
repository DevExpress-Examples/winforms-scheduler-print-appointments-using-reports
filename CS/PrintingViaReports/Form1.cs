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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.LookAndFeel;
using DevExpress.XtraReports.UserDesigner;


namespace PrintingViaReports
{
    public partial class Form1 : Form
    {

        #region InitialDataConstants
        public static Random RandomInstance = new Random();
        public static string[] Users = new string[] {"Peter Dolan", "Ryan Fischer", "Richard Fisher", 
                                                 "Tom Hamlett", "Mark Hamilton", "Steve Lee", "Jimmy Lewis", "Jeffrey W McClain", 
                                                 "Andrew Miller", "Dave Murrel", "Bert Parkins", "Mike Roller", "Ray Shipman", 
                                                 "Paul Bailey", "Brad Barnes", "Carl Lucas", "Jerry Campbell"};
        #endregion

        public Form1()
        {
            InitializeComponent();
            InitializeTasks();
            UpdateControls();
        }
        #region #initializetasks
        private void InitializeTasks()
        {
            schedulerStorage1.SetAppointmentFactory(new TaskFactory());
            FillResources(schedulerStorage1, 5);
            SetMappings();
            SetDataSource();
            HighlightFirstAppointment();
        }
        #endregion #initializetasks
        private void SetMappings()
        {
            this.schedulerStorage1.Appointments.Mappings.Start = "StartTime";
            this.schedulerStorage1.Appointments.Mappings.End = "EndTime";
            this.schedulerStorage1.Appointments.Mappings.Subject = "Subject";
            this.schedulerStorage1.Appointments.Mappings.AllDay = "AllDay";
            this.schedulerStorage1.Appointments.Mappings.Description = "Description";
            this.schedulerStorage1.Appointments.Mappings.Label = "Label";
            this.schedulerStorage1.Appointments.Mappings.Location = "Location";
            this.schedulerStorage1.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo";
            this.schedulerStorage1.Appointments.Mappings.ReminderInfo = "ReminderInfo";
            this.schedulerStorage1.Appointments.Mappings.ResourceId = "OwnerId";
            this.schedulerStorage1.Appointments.Mappings.Status = "Status";
            this.schedulerStorage1.Appointments.Mappings.Type = "EventType";

            AppointmentCustomFieldMapping customTextMapping = new AppointmentCustomFieldMapping("CustomTextField", "CustomText");
            AppointmentCustomFieldMapping customColorMapping = new AppointmentCustomFieldMapping("CustomColorField", "CustomColor");
            schedulerStorage1.Appointments.CustomFieldMappings.Add(customTextMapping);
            schedulerStorage1.Appointments.CustomFieldMappings.Add(customColorMapping);
        }
        private void SetDataSource()
        {
            CustomEventList eventList = new CustomEventList();
            GenerateEvents(eventList);
            this.schedulerStorage1.Appointments.DataSource = eventList;
        }
        #region #highlight
        private void HighlightFirstAppointment()
        {
            schedulerControl1.Start = DateTime.Now;
            AppointmentBaseCollection abc = schedulerStorage1.GetAppointments(schedulerControl1.ActiveView.GetVisibleIntervals());
            if (abc.Count > 0)
                ((Task)abc[0]).CustomColor = Color.IndianRed;
        }
        #endregion #highlight

        #region InitialDataLoad
        void FillResources(SchedulerStorage storage, int count)
        {
            ResourceCollection resources = storage.Resources.Items;
            storage.BeginUpdate();
            try
            {
                int cnt = Math.Min(count, Users.Length);
                for (int i = 1; i <= cnt; i++)
                    resources.Add(new Resource(Guid.NewGuid(), Users[i - 1]));
            }
            finally
            {
                storage.EndUpdate();
            }
        }


        void GenerateEvents(CustomEventList eventList)
        {
            int count = schedulerStorage1.Resources.Count;
            for (int i = 0; i < count; i++)
            {
                Resource resource = schedulerStorage1.Resources[i];
                string subjPrefix = resource.Caption + "'s ";
                eventList.Add(CreateEvent(eventList, subjPrefix + "meeting", resource.Id, 2, 5));
                eventList.Add(CreateEvent(eventList, subjPrefix + "travel", resource.Id, 3, 6));
                eventList.Add(CreateEvent(eventList, subjPrefix + "phone call", resource.Id, 0, 10));
            }
        }
        CustomEvent CreateEvent(CustomEventList eventList, string subject, object resourceId, int status, int label)
        {
            CustomEvent apt = new CustomEvent(eventList);

            apt.Subject = subject;
            apt.OwnerId = resourceId;
            Random rnd = RandomInstance;
            int rangeInMinutes = 60 * 24;
            apt.StartTime = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes));
            apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes / 4));
            apt.Status = status;
            apt.Label = label;
            TimeSpan duration = apt.EndTime - apt.StartTime;
            apt.Description = subject + " lasts for " + duration.ToString();
            apt.CustomText = "TEST";
            apt.CustomColor = Color.Transparent;
            return apt;
        }
        #endregion
        #region Update Controls
        private void UpdateControls()
        {
            cbView.EditValue = schedulerControl1.ActiveViewType;
            rgrpGrouping.EditValue = schedulerControl1.GroupType;
        }
        private void rgrpGrouping_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            schedulerControl1.GroupType = (SchedulerGroupType)rgrpGrouping.EditValue;
        }

        private void schedulerControl_ActiveViewChanged(object sender, System.EventArgs e)
        {
            cbView.EditValue = schedulerControl1.ActiveViewType;
        }

        private void cbView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            schedulerControl1.ActiveViewType = (SchedulerViewType)cbView.EditValue;
        }
       #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            XtraReport1 xr = new XtraReport1();
            // Assign the report's data source which is the collection of Task objects.
            AppointmentBaseCollection abc = schedulerStorage1.GetAppointments(schedulerControl1.ActiveView.GetVisibleIntervals());
            TaskCollection tc = new TaskCollection();
            tc.AddAppointmentRange(abc);
            xr.DataSource = tc;
            // Create a report and preview it.
            xr.CreateDocument();
            XRDesignFormEx designForm = new XRDesignFormEx();
            designForm.OpenReport(xr);
            designForm.DesignPanel.SelectedTabIndex = 1;
            designForm.ShowDialog();

        }

        private void schedulerControl1_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            Appointment apt = e.Appointment;
            // Required to open the recurrence form via context menu.
            bool openRecurrenceForm = apt.IsRecurring && schedulerStorage1.Appointments.IsNewAppointment(apt);
            // Create a custom form.
            MyAppointmentEditForm myForm = new MyAppointmentEditForm((SchedulerControl)sender, apt, openRecurrenceForm);

            try
            {
                // Required for skin support.
                myForm.LookAndFeel.ParentLookAndFeel = schedulerControl1.LookAndFeel;

                e.DialogResult = myForm.ShowDialog();
                schedulerControl1.Refresh();
                e.Handled = true;
            }
            finally
            {
                myForm.Dispose();
            }
        }

        private void schedulerControl1_AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e)
        {
            Color custColor = (Color)e.ViewInfo.Appointment.CustomFields["CustomColorField"];
            // Change the appointment color if the custom color value is not default.
            if (custColor != Color.Transparent)
                e.ViewInfo.Appearance.BackColor = custColor;
        }

        private void schedulerControl1_InitNewAppointment(object sender, AppointmentEventArgs e)
        {
            // Set default custom color value for all newly created appointments.
            e.Appointment.CustomFields["CustomColorField"] = Color.Transparent;
        }

    }
}