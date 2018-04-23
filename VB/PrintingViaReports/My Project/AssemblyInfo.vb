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

Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.
<Assembly: AssemblyTitle("Reports")>
<Assembly: AssemblyDescription("")>
<Assembly: AssemblyConfiguration("")>
<Assembly: AssemblyCompany("-")>
<Assembly: AssemblyProduct("Reports")>
<Assembly: AssemblyCopyright("Copyright © - 2008")>
<Assembly: AssemblyTrademark("")>
<Assembly: AssemblyCulture("")>

' Setting ComVisible to false makes the types in this assembly not visible 
' to COM components.  If you need to access a type in this assembly from 
' COM, set the ComVisible attribute to true on that type.
<Assembly: ComVisible(False)>

' The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("7ff06cbd-42b0-4c6a-acd3-e15138e85351")>

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
<Assembly: AssemblyVersion("1.0.0.0")>
<Assembly: AssemblyFileVersion("1.0.0.0")>
