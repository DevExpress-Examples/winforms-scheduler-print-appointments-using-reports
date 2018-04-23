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
