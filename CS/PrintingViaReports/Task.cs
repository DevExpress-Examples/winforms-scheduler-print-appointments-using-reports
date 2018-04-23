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
    public class Task : Appointment {

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
