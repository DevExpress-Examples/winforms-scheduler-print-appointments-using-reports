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
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace PrintingViaReports{

    public class CustomEvent : IEditableObject {
        DateTime fStart;
        DateTime fEnd;
        string fSubject;
        int fStatus;
        string fDescription;
        long fLabel;
        string fLocation;
        bool fAllday;
        int fEventType;
        string fRecurrenceInfo;
        string fReminderInfo;
        object fOwnerId;

        string fCustomText;
        Color fCustomColor;
        

        CustomEventList events;
        bool committed = false;

        public CustomEvent(CustomEventList events) {
            this.events = events;
        }

        private void OnListChanged() {
            int index = events.IndexOf(this);
            events.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
        }

        public DateTime StartTime { get { return fStart; } set { fStart = value; } }
        public DateTime EndTime { get { return fEnd; } set { fEnd = value; } }
        public string Subject { get { return fSubject; } set { fSubject = value; } }
        public int Status { get { return fStatus; } set { fStatus = value; } }
        public string Description { get { return fDescription; } set { fDescription = value; } }
        public long Label { get { return fLabel; } set { fLabel = value; } }
        public string Location { get { return fLocation; } set { fLocation = value; } }
        public bool AllDay { get { return fAllday; } set { fAllday = value; } }
        public int EventType { get { return fEventType; } set { fEventType = value; } }
        public string RecurrenceInfo { get { return fRecurrenceInfo; } set { fRecurrenceInfo = value; } }
        public string ReminderInfo { get { return fReminderInfo; } set { fReminderInfo = value; } }
        public object OwnerId { get { return fOwnerId; } set { fOwnerId = value; } }

        public string CustomText { get { return fCustomText; } set { fCustomText = value; } }
        public Color CustomColor { get { return fCustomColor; } set {fCustomColor = value; }
        }


        public void BeginEdit() {
        }
        public void CancelEdit() {
            if (!committed) {
                ((IList)events).Remove(this);
            }
        }
        public void EndEdit() {
            committed = true;
        }
    }

    public class CustomEventList : CollectionBase, IBindingList {
        public CustomEvent this[int idx] { get { return (CustomEvent)base.List[idx]; } }

        public new void Clear() {
            base.Clear();
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        public void Add(CustomEvent appointment) {
            base.List.Add(appointment);
        }
        public int IndexOf(CustomEvent appointment) {
            return List.IndexOf(appointment);
        }
        public object AddNew() {
            CustomEvent app = new CustomEvent(this);
            List.Add(app);
            return app;
        }
        public bool AllowEdit { get { return true; } }
        public bool AllowNew { get { return true; } }
        public bool AllowRemove { get { return true; } }

        private ListChangedEventHandler listChangedHandler;
        public event ListChangedEventHandler ListChanged {
            add { listChangedHandler += value; }
            remove { listChangedHandler -= value; }
        }
        internal void OnListChanged(ListChangedEventArgs args) {
            if (listChangedHandler != null) {
                listChangedHandler(this, args);
            }
        }
        protected override void OnRemoveComplete(int index, object value) {
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
        }
        protected override void OnInsertComplete(int index, object value) {
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
        }

        public void AddIndex(PropertyDescriptor pd) { throw new NotSupportedException(); }
        public void ApplySort(PropertyDescriptor pd, ListSortDirection dir) { throw new NotSupportedException(); }
        public int Find(PropertyDescriptor property, object key) { throw new NotSupportedException(); }
        public bool IsSorted { get { return false; } }
        public void RemoveIndex(PropertyDescriptor pd) { throw new NotSupportedException(); }
        public void RemoveSort() { throw new NotSupportedException(); }
        public ListSortDirection SortDirection { get { throw new NotSupportedException(); } }
        public PropertyDescriptor SortProperty { get { throw new NotSupportedException(); } }
        public bool SupportsChangeNotification { get { return true; } }
        public bool SupportsSearching { get { return false; } }
        public bool SupportsSorting { get { return false; } }
    }
}
