Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraScheduler
Imports System.Drawing
'using DevExpress.XtraScheduler.Native;

Namespace PrintingViaReports
    #Region "#appointmentfactory"
    Public Class TaskFactory
        Implements IAppointmentFactory
        Public Function CreateAppointment(ByVal type As AppointmentType) As Appointment Implements IAppointmentFactory.CreateAppointment
            Dim task As Task = New Task(type)
            Return task
        End Function
    End Class
    Public Class Task
        Inherits Appointment

        Public Sub New()
        End Sub
        Public Sub New(ByVal type As AppointmentType)
            MyBase.New(type)
        End Sub

        ' Convert custom fields into the Task properties
        Public Property CustomText() As String
            Get
                Return CStr(MyBase.CustomFields("CustomTextField"))
            End Get
            Set(ByVal value As String)
                MyBase.CustomFields("CustomTextField") = value
            End Set
        End Property
        Public Property CustomColor() As Color
            Get
                Return CType(MyBase.CustomFields("CustomColorField"), Color)
            End Get
            Set(ByVal value As Color)
                MyBase.CustomFields("CustomColorField") = value
            End Set
        End Property
        Public ReadOnly Property CustomColorARGB() As Integer
            Get
                Return (CType(MyBase.CustomFields("CustomColorField"), Color)).ToArgb()
            End Get
        End Property
    End Class
    Public Class TaskCollection
        Inherits List(Of Task)

        Public Sub AddAppointment(ByVal appointment As Appointment)
            Dim task As Task = CType(appointment, Task)
            MyBase.Add(task)
        End Sub

        Public Overridable Sub AddAppointmentRange(ByVal collection As AppointmentBaseCollection)
            For Each item As Appointment In collection
                Me.AddAppointment(item)
            Next item
        End Sub
    End Class
    #End Region ' #appointmentfactory
End Namespace
