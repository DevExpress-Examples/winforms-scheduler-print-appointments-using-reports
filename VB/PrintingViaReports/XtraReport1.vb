Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports DevExpress.XtraReports.UI

Namespace PrintingViaReports
    Partial Public Class XtraReport1
        Inherits DevExpress.XtraReports.UI.XtraReport
        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub xrLabel6_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles xrLabel6.BeforePrint
            ' Fill the label with color
            Dim curColor As Color = Color.FromArgb(Int32.Parse((CType(sender, XRLabel)).Text))
            CType(sender, XRLabel).BackColor = curColor
            CType(sender, XRLabel).ForeColor = curColor
        End Sub

        Private Sub xrLabel7_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles xrLabel7.BeforePrint
            Dim curColor As Color = CType(GetCurrentColumnValue("CustomColor"), Color)
            CType(sender, XRLabel).ForeColor = curColor
            CType(sender, XRLabel).Text = (CType(sender, XRLabel)).Text + Constants.vbLf + Constants.vbCr + curColor.Name & " color"

        End Sub



    End Class
End Namespace
