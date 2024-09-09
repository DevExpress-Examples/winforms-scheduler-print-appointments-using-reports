<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128636620/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1183)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# WinForms Scheduler - Use DevExpress Reports to print appointments

This example uses [DevExpress Reports](https://www.devexpress.com/subscriptions/reporting/) to print user appointments.

The key point is to obtain a collection of appointments and assign it to the report's [DataSource](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XtraReportBase.DataSource) property. The example uses the [GetAppointments](https://docs.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerDataStorage.GetAppointments.overloads) method to obtain appointments that fall within the time range specified by the [GetVisibleIntervals](https://docs.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerViewBase.GetVisibleIntervals) method.

To display custom fields in the report, the example exposes [custom fields](https://docs.devexpress.com/WindowsForms/17137/controls-and-libraries/scheduler/data-binding/mappings/custom-fields) (see the `Task` class). The [SetAppointmentFactory](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraScheduler.AppointmentStorageBase.SetAppointmentFactory(DevExpress.XtraScheduler.IAppointmentFactory)) method replaces Scheduler Appointment objects with `Task` class instances.

> **Important**
>
> You should own a [DevExpress Reporting](https://www.devexpress.com/buy/net/) license.


## Files to Review

* [CustomEvents.cs](./CS/PrintingViaReports/Data/CustomEvents.cs) (VB: [CustomEvents.vb](./VB/PrintingViaReports/Data/CustomEvents.vb))
* [Form1.cs](./CS/PrintingViaReports/Form1.cs) (VB: [Form1.vb](./VB/PrintingViaReports/Form1.vb))
* [Form1.vb](./CS/PrintingViaReports/Form1.vb) (VB: [Form1.vb](./VB/PrintingViaReports/Form1.vb))
* [MyAppointmentEditForm.cs](./CS/PrintingViaReports/MyAppointmentEditForm.cs) (VB: [MyAppointmentEditForm.vb](./VB/PrintingViaReports/MyAppointmentEditForm.vb))
* [Task.cs](./CS/PrintingViaReports/Task.cs) (VB: [Task.vb](./VB/PrintingViaReports/Task.vb))
* [XtraReport1.cs](./CS/PrintingViaReports/XtraReport1.cs) (VB: [XtraReport1.vb](./VB/PrintingViaReports/XtraReport1.vb))


## Documentation

* [DevExpress Reporting for WinForms](https://docs.devexpress.com/XtraReports/1198/winforms-reporting)
* [Reports - WinForms Scheduler](https://docs.devexpress.com/WindowsForms/8372/controls-and-libraries/scheduler/scheduler-reporting)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-print-appointments-using-reports&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-print-appointments-using-reports&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
