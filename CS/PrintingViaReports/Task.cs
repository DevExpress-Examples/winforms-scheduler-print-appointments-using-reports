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
using System.Text;
using DevExpress.XtraScheduler;
using System.Drawing;
//using DevExpress.XtraScheduler.Native;

namespace PrintingViaReports
{
    #region #appointmentfactory
    public class TaskFactory : IAppointmentFactory
    {
        public Appointment CreateAppointment(AppointmentType type)
        {
            Task task = new Task(type);
            return task;
        }
    }
    public class Task : DevExpress.XtraScheduler.Internal.Implementations.AppointmentInstance {

        public Task()  { }
        public Task (AppointmentType type) : base(type){}
 
        // Convert custom fields into the Task properties
        public string CustomText {
            get { return (string)base.CustomFields["CustomTextField"]; }
            set { base.CustomFields["CustomTextField"] = value; }
        }
        public Color CustomColor {
            get { return (Color)base.CustomFields["CustomColorField"]; }
            set { base.CustomFields["CustomColorField"] = value; }
        }
        public int CustomColorARGB { get { return ((Color)base.CustomFields["CustomColorField"]).ToArgb(); } }
    }
    public class TaskCollection : List<Task> {

        public void AddAppointment(Appointment appointment) {
            Task task = (Task)appointment;
            base.Add(task);
        }

        public virtual void AddAppointmentRange(AppointmentBaseCollection collection) {
            foreach(Appointment item in collection)
                this.AddAppointment(item);
        }
    }
    #endregion #appointmentfactory
}
