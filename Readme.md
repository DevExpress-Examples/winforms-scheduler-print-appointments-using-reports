<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128636620/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1183)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CustomEvents.cs](./CS/PrintingViaReports/Data/CustomEvents.cs) (VB: [CustomEvents.vb](./VB/PrintingViaReports/Data/CustomEvents.vb))
* [Form1.cs](./CS/PrintingViaReports/Form1.cs) (VB: [Form1.vb](./VB/PrintingViaReports/Form1.vb))
* [MyAppointmentEditForm.cs](./CS/PrintingViaReports/MyAppointmentEditForm.cs) (VB: [MyAppointmentEditForm.vb](./VB/PrintingViaReports/MyAppointmentEditForm.vb))
* [Program.cs](./CS/PrintingViaReports/Program.cs) (VB: [Program.vb](./VB/PrintingViaReports/Program.vb))
* [Task.cs](./CS/PrintingViaReports/Task.cs) (VB: [Task.vb](./VB/PrintingViaReports/Task.vb))
* [XtraReport1.cs](./CS/PrintingViaReports/XtraReport1.cs) (VB: [XtraReport1.vb](./VB/PrintingViaReports/XtraReport1.vb))
<!-- default file list end -->
# Printing appointment details using the XtraReports Suite


<p>This example illustrates how you can print the appointment details for the appointments currently displayed in the Scheduler by means of the <a href="http://help.devexpress.com/#XtraReports/CustomDocument2586">XtraReports Suite</a>.<br /> The key point is to obtain a collection of appointments and assign it to the report's <a href="http://documentation.devexpress.com/#XtraReports/DevExpressXtraReportsUIXtraReportBase_DataSourcetopic">DataSource</a>. To accomplish this, the <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerSchedulerStorageBase_GetAppointmentstopic">GetAppointments</a> method is used to get a collection of appointments which fall within the time range specified by the <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerSchedulerViewBase_GetVisibleIntervalstopic">GetVisibleIntervals</a> method.</p>
<p>To display custom fields in the report, the <a href="http://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraSchedulerNativeCustomFieldtopic">custom fields</a> should be exposed as common object properties. So a wrapper class <strong>Task</strong> is implemented solely for this purpose. Using the <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerAppointmentStorageBase_SetAppointmentFactorytopic">SetAppointmentFactory</a> method, Scheduler's Appointment objects are replaced with the Task class instances. A <strong>TaskCollection</strong> class holds Task objects and can be used as the report's data source.</p>
<p>Note that you should have a valid license to the <a href="http://documentation.devexpress.com/#XtraReports/CustomDocument2162">XtraReports Suite</a>.</p>

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-print-appointments-using-reports&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-print-appointments-using-reports&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
