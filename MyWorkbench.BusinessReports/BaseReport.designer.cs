namespace MyWorkbench.BusinessReports {
    partial class BaseReport {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.CompanyTable = new DevExpress.XtraReports.UI.XRTable();
            this.CompanyTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.CompanyTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblCompanyPhone = new DevExpress.XtraReports.UI.XRTableCell();
            this.CompanyPhoneNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.CompanyTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblCompanyFax = new DevExpress.XtraReports.UI.XRTableCell();
            this.CompanyFaxNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.CompanyTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblCompanyEmail = new DevExpress.XtraReports.UI.XRTableCell();
            this.CompanyEmail = new DevExpress.XtraReports.UI.XRTableCell();
            this.CompanyTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblCompanyReg = new DevExpress.XtraReports.UI.XRTableCell();
            this.CompanyReg = new DevExpress.XtraReports.UI.XRTableCell();
            this.CompanyTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblCompanyVAT = new DevExpress.XtraReports.UI.XRTableCell();
            this.CompanyVAT = new DevExpress.XtraReports.UI.XRTableCell();
            this.CompanyAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.DocumentTable = new DevExpress.XtraReports.UI.XRTable();
            this.DocumentTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DocumentTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DocumentTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblReportHeader = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.BlueBold = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Paragraph = new DevExpress.XtraReports.UI.XRControlStyle();
            this.RedBold = new DevExpress.XtraReports.UI.XRControlStyle();
            this.ParagraphRightAlign = new DevExpress.XtraReports.UI.XRControlStyle();
            this.BlackBold = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 32F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1,
            this.DocumentTable,
            this.lblReportHeader,
            this.xrPictureBox1});
            this.PageHeader.HeightF = 274F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrPanel1
            // 
            this.xrPanel1.BorderColor = System.Drawing.Color.LightGray;
            this.xrPanel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPanel1.CanShrink = true;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.CompanyTable,
            this.CompanyAddress});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 118F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(630F, 156F);
            this.xrPanel1.StylePriority.UseBorderColor = false;
            this.xrPanel1.StylePriority.UseBorders = false;
            // 
            // CompanyTable
            // 
            this.CompanyTable.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.CompanyTable.LocationFloat = new DevExpress.Utils.PointFloat(3.000002F, 2.000015F);
            this.CompanyTable.Name = "CompanyTable";
            this.CompanyTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.CompanyTableRow1,
            this.CompanyTableRow2,
            this.CompanyTableRow3,
            this.CompanyTableRow4,
            this.CompanyTableRow5,
            this.CompanyTableRow6});
            this.CompanyTable.SizeF = new System.Drawing.SizeF(327.625F, 150F);
            this.CompanyTable.StylePriority.UseBorders = false;
            // 
            // CompanyTableRow1
            // 
            this.CompanyTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2});
            this.CompanyTableRow1.Name = "CompanyTableRow1";
            this.CompanyTableRow1.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StyleName = "BlueBold";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "[CompanyName]";
            this.xrTableCell2.Weight = 2.34375D;
            // 
            // CompanyTableRow2
            // 
            this.CompanyTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblCompanyPhone,
            this.CompanyPhoneNo});
            this.CompanyTableRow2.Name = "CompanyTableRow2";
            this.CompanyTableRow2.Weight = 1D;
            // 
            // lblCompanyPhone
            // 
            this.lblCompanyPhone.Name = "lblCompanyPhone";
            this.lblCompanyPhone.StyleName = "BlackBold";
            this.lblCompanyPhone.Text = "Phone";
            this.lblCompanyPhone.Weight = 0.610544722042922D;
            // 
            // CompanyPhoneNo
            // 
            this.CompanyPhoneNo.Name = "CompanyPhoneNo";
            this.CompanyPhoneNo.StyleName = "Paragraph";
            this.CompanyPhoneNo.StylePriority.UseTextAlignment = false;
            this.CompanyPhoneNo.Text = "[PhoneNo]";
            this.CompanyPhoneNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.CompanyPhoneNo.Weight = 1.7332052779570781D;
            // 
            // CompanyTableRow3
            // 
            this.CompanyTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblCompanyFax,
            this.CompanyFaxNo});
            this.CompanyTableRow3.Name = "CompanyTableRow3";
            this.CompanyTableRow3.Weight = 1D;
            // 
            // lblCompanyFax
            // 
            this.lblCompanyFax.CanShrink = true;
            this.lblCompanyFax.Name = "lblCompanyFax";
            this.lblCompanyFax.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.lblCompanyFax.StyleName = "BlackBold";
            this.lblCompanyFax.Text = "Fax";
            this.lblCompanyFax.Weight = 0.610544722042922D;
            // 
            // CompanyFaxNo
            // 
            this.CompanyFaxNo.CanShrink = true;
            this.CompanyFaxNo.Name = "CompanyFaxNo";
            this.CompanyFaxNo.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.CompanyFaxNo.StyleName = "Paragraph";
            this.CompanyFaxNo.StylePriority.UseTextAlignment = false;
            this.CompanyFaxNo.Text = "[FaxNo]";
            this.CompanyFaxNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.CompanyFaxNo.Weight = 1.7332052779570781D;
            // 
            // CompanyTableRow4
            // 
            this.CompanyTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblCompanyEmail,
            this.CompanyEmail});
            this.CompanyTableRow4.Name = "CompanyTableRow4";
            this.CompanyTableRow4.Weight = 1D;
            // 
            // lblCompanyEmail
            // 
            this.lblCompanyEmail.CanShrink = true;
            this.lblCompanyEmail.Name = "lblCompanyEmail";
            this.lblCompanyEmail.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.lblCompanyEmail.StyleName = "BlackBold";
            this.lblCompanyEmail.Text = "Email";
            this.lblCompanyEmail.Weight = 0.610544722042922D;
            // 
            // CompanyEmail
            // 
            this.CompanyEmail.CanShrink = true;
            this.CompanyEmail.Name = "CompanyEmail";
            this.CompanyEmail.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.CompanyEmail.StyleName = "Paragraph";
            this.CompanyEmail.StylePriority.UseTextAlignment = false;
            this.CompanyEmail.Text = "[EmailAddress]";
            this.CompanyEmail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.CompanyEmail.Weight = 1.7332052779570781D;
            // 
            // CompanyTableRow5
            // 
            this.CompanyTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblCompanyReg,
            this.CompanyReg});
            this.CompanyTableRow5.Name = "CompanyTableRow5";
            this.CompanyTableRow5.Weight = 1D;
            // 
            // lblCompanyReg
            // 
            this.lblCompanyReg.CanShrink = true;
            this.lblCompanyReg.Name = "lblCompanyReg";
            this.lblCompanyReg.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.lblCompanyReg.StyleName = "BlackBold";
            this.lblCompanyReg.Text = "Reg No";
            this.lblCompanyReg.Weight = 0.610544722042922D;
            // 
            // CompanyReg
            // 
            this.CompanyReg.CanShrink = true;
            this.CompanyReg.Name = "CompanyReg";
            this.CompanyReg.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.CompanyReg.StyleName = "Paragraph";
            this.CompanyReg.StylePriority.UseTextAlignment = false;
            this.CompanyReg.Text = "[RegistrationNo]";
            this.CompanyReg.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.CompanyReg.Weight = 1.7332052779570781D;
            // 
            // CompanyTableRow6
            // 
            this.CompanyTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblCompanyVAT,
            this.CompanyVAT});
            this.CompanyTableRow6.Name = "CompanyTableRow6";
            this.CompanyTableRow6.Weight = 1D;
            // 
            // lblCompanyVAT
            // 
            this.lblCompanyVAT.CanShrink = true;
            this.lblCompanyVAT.Name = "lblCompanyVAT";
            this.lblCompanyVAT.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.lblCompanyVAT.StyleName = "BlackBold";
            this.lblCompanyVAT.Text = "VAT No";
            this.lblCompanyVAT.Weight = 0.610544722042922D;
            // 
            // CompanyVAT
            // 
            this.CompanyVAT.CanShrink = true;
            this.CompanyVAT.Name = "CompanyVAT";
            this.CompanyVAT.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.CompanyVAT.StyleName = "Paragraph";
            this.CompanyVAT.StylePriority.UseTextAlignment = false;
            this.CompanyVAT.Text = "[VATNo]";
            this.CompanyVAT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.CompanyVAT.Weight = 1.7332052779570781D;
            // 
            // CompanyAddress
            // 
            this.CompanyAddress.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.CompanyAddress.LocationFloat = new DevExpress.Utils.PointFloat(338.8333F, 27.00002F);
            this.CompanyAddress.Multiline = true;
            this.CompanyAddress.Name = "CompanyAddress";
            this.CompanyAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.CompanyAddress.SizeF = new System.Drawing.SizeF(289.1663F, 25.00002F);
            this.CompanyAddress.StyleName = "Paragraph";
            this.CompanyAddress.StylePriority.UseBorders = false;
            this.CompanyAddress.Text = "[Address.FormattedFullAddress]";
            // 
            // DocumentTable
            // 
            this.DocumentTable.LocationFloat = new DevExpress.Utils.PointFloat(348.8333F, 38.54167F);
            this.DocumentTable.Name = "DocumentTable";
            this.DocumentTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.DocumentTableRow1,
            this.DocumentTableRow2,
            this.DocumentTableRow3});
            this.DocumentTable.SizeF = new System.Drawing.SizeF(291.1665F, 75F);
            // 
            // DocumentTableRow1
            // 
            this.DocumentTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell4});
            this.DocumentTableRow1.Name = "DocumentTableRow1";
            this.DocumentTableRow1.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StyleName = "BlueBold";
            this.xrTableCell3.Text = "Document No";
            this.xrTableCell3.Weight = 0.76466720609180694D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StyleName = "Paragraph";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell4.Weight = 1.5790827939081933D;
            // 
            // DocumentTableRow2
            // 
            this.DocumentTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell6});
            this.DocumentTableRow2.Name = "DocumentTableRow2";
            this.DocumentTableRow2.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.CanShrink = true;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrTableCell5.StyleName = "BlueBold";
            this.xrTableCell5.Text = "Date";
            this.xrTableCell5.Weight = 0.76466747169818616D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.CanShrink = true;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrTableCell6.StyleName = "Paragraph";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell6.Weight = 1.579082528301814D;
            // 
            // DocumentTableRow3
            // 
            this.DocumentTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell10});
            this.DocumentTableRow3.Name = "DocumentTableRow3";
            this.DocumentTableRow3.Weight = 1D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.CanShrink = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrTableCell9.StyleName = "BlueBold";
            this.xrTableCell9.Text = "PO Number";
            this.xrTableCell9.Weight = 0.76466747169818616D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.CanShrink = true;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrTableCell10.StyleName = "Paragraph";
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell10.Weight = 1.579082528301814D;
            // 
            // lblReportHeader
            // 
            this.lblReportHeader.Font = new System.Drawing.Font("Calibri", 18F);
            this.lblReportHeader.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(348.8333F, 0F);
            this.lblReportHeader.Name = "lblReportHeader";
            this.lblReportHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblReportHeader.SizeF = new System.Drawing.SizeF(291.1663F, 38.54168F);
            this.lblReportHeader.StylePriority.UseForeColor = false;
            this.lblReportHeader.StylePriority.UseTextAlignment = false;
            this.lblReportHeader.Text = "INVOICE";
            this.lblReportHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(304.3749F, 113.5417F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // BlueBold
            // 
            this.BlueBold.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.BlueBold.ForeColor = System.Drawing.Color.DodgerBlue;
            this.BlueBold.Name = "BlueBold";
            this.BlueBold.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // Paragraph
            // 
            this.Paragraph.Font = new System.Drawing.Font("Calibri", 10F);
            this.Paragraph.Name = "Paragraph";
            this.Paragraph.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // RedBold
            // 
            this.RedBold.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.RedBold.ForeColor = System.Drawing.Color.Red;
            this.RedBold.Name = "RedBold";
            this.RedBold.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ParagraphRightAlign
            // 
            this.ParagraphRightAlign.Font = new System.Drawing.Font("Calibri", 10F);
            this.ParagraphRightAlign.Name = "ParagraphRightAlign";
            this.ParagraphRightAlign.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // BlackBold
            // 
            this.BlackBold.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.BlackBold.Name = "BlackBold";
            this.BlackBold.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageFooter
            // 
            this.PageFooter.HeightF = 0F;
            this.PageFooter.Name = "PageFooter";
            // 
            // ReportFooter
            // 
            this.ReportFooter.HeightF = 0F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // ReportHeader
            // 
            this.ReportHeader.HeightF = 0F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // BaseReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.ReportFooter,
            this.ReportHeader});
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 32, 100);
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.BlueBold,
            this.Paragraph,
            this.RedBold,
            this.ParagraphRightAlign,
            this.BlackBold});
            this.Version = "18.1";
            ((System.ComponentModel.ISupportInitialize)(this.CompanyTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRControlStyle BlueBold;
        private DevExpress.XtraReports.UI.XRControlStyle Paragraph;
        private DevExpress.XtraReports.UI.XRControlStyle RedBold;
        private DevExpress.XtraReports.UI.XRControlStyle ParagraphRightAlign;
        private DevExpress.XtraReports.UI.XRControlStyle BlackBold;
        public DevExpress.XtraReports.UI.DetailBand Detail;
        public DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        public DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        public DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        public DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        public DevExpress.XtraReports.UI.XRTable CompanyTable;
        private DevExpress.XtraReports.UI.XRTableRow CompanyTableRow1;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableRow CompanyTableRow2;
        public DevExpress.XtraReports.UI.XRTableCell lblCompanyPhone;
        public DevExpress.XtraReports.UI.XRTableCell CompanyPhoneNo;
        private DevExpress.XtraReports.UI.XRTableRow CompanyTableRow3;
        public DevExpress.XtraReports.UI.XRTableCell lblCompanyFax;
        public DevExpress.XtraReports.UI.XRTableCell CompanyFaxNo;
        private DevExpress.XtraReports.UI.XRTableRow CompanyTableRow4;
        public DevExpress.XtraReports.UI.XRTableCell lblCompanyEmail;
        public DevExpress.XtraReports.UI.XRTableCell CompanyEmail;
        private DevExpress.XtraReports.UI.XRTableRow CompanyTableRow5;
        public DevExpress.XtraReports.UI.XRTableCell lblCompanyReg;
        public DevExpress.XtraReports.UI.XRTableCell CompanyReg;
        private DevExpress.XtraReports.UI.XRTableRow CompanyTableRow6;
        public DevExpress.XtraReports.UI.XRTableCell lblCompanyVAT;
        public DevExpress.XtraReports.UI.XRTableCell CompanyVAT;
        public DevExpress.XtraReports.UI.XRLabel CompanyAddress;
        public DevExpress.XtraReports.UI.XRTable DocumentTable;
        private DevExpress.XtraReports.UI.XRTableRow DocumentTableRow1;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableRow DocumentTableRow2;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableRow DocumentTableRow3;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        public DevExpress.XtraReports.UI.XRLabel lblReportHeader;
        public DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
    }
}
