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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace PrintingViaReports {
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport {
        public XtraReport1() {
            InitializeComponent();
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            // Fill the label with color
            Color curColor = Color.FromArgb(Int32.Parse(((XRLabel)sender).Text));
            ((XRLabel)sender).BackColor = curColor;
            ((XRLabel)sender).ForeColor = curColor;
        }

        private void xrLabel7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            Color curColor = (Color)GetCurrentColumnValue("CustomColor");
            ((XRLabel)sender).ForeColor = curColor;
            ((XRLabel)sender).Text = ((XRLabel)sender).Text + "\n\r" + curColor.Name + " color";

        }

       

    }
}
