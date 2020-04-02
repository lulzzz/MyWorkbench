namespace MyWorkbench.BusinessReports
{
    partial class RequestForQuoteReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.collectionDataSourceQuote = new DevExpress.Persistent.Base.ReportsV2.CollectionDataSource();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.ItemVATAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.ItemDescription = new DevExpress.XtraReports.UI.XRLabel();
            this.ItemStockCode = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeaderItemType = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.ItemType = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooterItemType = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.NotesTable = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.QuoteEquipmentInvisible = new DevExpress.XtraReports.UI.FormattingRule();
            this.QuoteEquipmentVisible = new DevExpress.XtraReports.UI.FormattingRule();
            this.collectionDataSourceSettings = new DevExpress.Persistent.Base.ReportsV2.CollectionDataSource();
            this.ImagesVisible = new DevExpress.XtraReports.UI.FormattingRule();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSourceQuote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotesTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSourceSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2});
            this.Detail.HeightF = 12.08334F;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2});
            this.PageFooter.HeightF = 23F;
            // 
            // CompanyTable
            // 
            this.CompanyTable.StylePriority.UseBorders = false;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.collectionDataSourceSettings, "CompanyName")});
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Text = "";
            // 
            // CompanyPhoneNo
            // 
            this.CompanyPhoneNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.collectionDataSourceSettings, "PhoneNo")});
            this.CompanyPhoneNo.StylePriority.UseTextAlignment = false;
            this.CompanyPhoneNo.Text = "";
            // 
            // CompanyFaxNo
            // 
            this.CompanyFaxNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.collectionDataSourceSettings, "FaxNo")});
            this.CompanyFaxNo.StylePriority.UseTextAlignment = false;
            this.CompanyFaxNo.Text = "";
            // 
            // CompanyEmail
            // 
            this.CompanyEmail.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.collectionDataSourceSettings, "EmailAddress")});
            this.CompanyEmail.StylePriority.UseTextAlignment = false;
            this.CompanyEmail.Text = "";
            // 
            // CompanyReg
            // 
            this.CompanyReg.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.collectionDataSourceSettings, "RegistrationNo")});
            this.CompanyReg.StylePriority.UseTextAlignment = false;
            this.CompanyReg.Text = "";
            // 
            // CompanyVAT
            // 
            this.CompanyVAT.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.collectionDataSourceSettings, "VatNo")});
            this.CompanyVAT.StylePriority.UseTextAlignment = false;
            this.CompanyVAT.Text = "";
            // 
            // CompanyAddress
            // 
            this.CompanyAddress.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.collectionDataSourceSettings, "Address.FullAddress")});
            this.CompanyAddress.StylePriority.UseBorders = false;
            this.CompanyAddress.StylePriority.UseTextAlignment = false;
            this.CompanyAddress.Text = "";
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "No")});
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Issued")});
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.TextFormatString = "{0:d}";
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Text = "Currency";
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Currency.Description")});
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            // 
            // lblReportHeader
            // 
            this.lblReportHeader.StylePriority.UseForeColor = false;
            this.lblReportHeader.StylePriority.UseTextAlignment = false;
            this.lblReportHeader.Text = "QUOTE REQUEST";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", this.collectionDataSourceSettings, "Logo")});
            // 
            // collectionDataSourceQuote
            // 
            this.collectionDataSourceQuote.Name = "collectionDataSourceQuote";
            this.collectionDataSourceQuote.ObjectTypeName = "MyWorkbench.BusinessObjects.Accounts.Quote";
            this.collectionDataSourceQuote.TopReturnedRecords = 0;
            // 
            // xrLine2
            // 
            this.xrLine2.BackColor = System.Drawing.Color.DodgerBlue;
            this.xrLine2.BorderColor = System.Drawing.Color.DodgerBlue;
            this.xrLine2.BorderWidth = 0F;
            this.xrLine2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(650F, 2F);
            this.xrLine2.StylePriority.UseBackColor = false;
            this.xrLine2.StylePriority.UseBorderColor = false;
            this.xrLine2.StylePriority.UseBorderWidth = false;
            this.xrLine2.StylePriority.UseForeColor = false;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader1,
            this.GroupHeaderItemType,
            this.GroupFooterItemType,
            this.GroupFooter1});
            this.DetailReport.DataMember = "Items";
            this.DetailReport.DataSource = this.collectionDataSourceQuote;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.ItemVATAmount,
            this.ItemDescription,
            this.ItemStockCode});
            this.Detail1.HeightF = 23F;
            this.Detail1.Name = "Detail1";
            // 
            // ItemVATAmount
            // 
            this.ItemVATAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Items.Quantity", "{0:N2}")});
            this.ItemVATAmount.Font = new System.Drawing.Font("Calibri", 10F);
            this.ItemVATAmount.LocationFloat = new DevExpress.Utils.PointFloat(411.0417F, 0F);
            this.ItemVATAmount.Name = "ItemVATAmount";
            this.ItemVATAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ItemVATAmount.SizeF = new System.Drawing.SizeF(238.9585F, 23F);
            this.ItemVATAmount.StyleName = "ParagraphRightAlign";
            this.ItemVATAmount.StylePriority.UseFont = false;
            this.ItemVATAmount.StylePriority.UseTextAlignment = false;
            this.ItemVATAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // ItemDescription
            // 
            this.ItemDescription.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Items.Description")});
            this.ItemDescription.Font = new System.Drawing.Font("Calibri", 10F);
            this.ItemDescription.LocationFloat = new DevExpress.Utils.PointFloat(110F, 0F);
            this.ItemDescription.Multiline = true;
            this.ItemDescription.Name = "ItemDescription";
            this.ItemDescription.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ItemDescription.SizeF = new System.Drawing.SizeF(301.0417F, 23F);
            this.ItemDescription.StyleName = "Paragraph";
            this.ItemDescription.StylePriority.UseFont = false;
            // 
            // ItemStockCode
            // 
            this.ItemStockCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Items.Item.StockCode")});
            this.ItemStockCode.Font = new System.Drawing.Font("Calibri", 10F);
            this.ItemStockCode.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.ItemStockCode.Name = "ItemStockCode";
            this.ItemStockCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ItemStockCode.SizeF = new System.Drawing.SizeF(110F, 23F);
            this.ItemStockCode.StyleName = "Paragraph";
            this.ItemStockCode.StylePriority.UseFont = false;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel11,
            this.xrLabel3,
            this.xrLabel12});
            this.GroupHeader1.HeightF = 23F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrLabel11
            // 
            this.xrLabel11.BackColor = System.Drawing.Color.DodgerBlue;
            this.xrLabel11.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel11.ForeColor = System.Drawing.Color.White;
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(4.768372E-05F, 0F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(110F, 23F);
            this.xrLabel11.StylePriority.UseBackColor = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseForeColor = false;
            this.xrLabel11.Text = "Item";
            // 
            // xrLabel3
            // 
            this.xrLabel3.BackColor = System.Drawing.Color.DodgerBlue;
            this.xrLabel3.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.ForeColor = System.Drawing.Color.White;
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(411.0417F, 0F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(238.5F, 23F);
            this.xrLabel3.StylePriority.UseBackColor = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseForeColor = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Qty";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel12
            // 
            this.xrLabel12.BackColor = System.Drawing.Color.DodgerBlue;
            this.xrLabel12.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.ForeColor = System.Drawing.Color.White;
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(110F, 0F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(301.0417F, 23F);
            this.xrLabel12.StylePriority.UseBackColor = false;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseForeColor = false;
            this.xrLabel12.Text = "Description";
            // 
            // GroupHeaderItemType
            // 
            this.GroupHeaderItemType.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.ItemType});
            this.GroupHeaderItemType.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Item.ItemType", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeaderItemType.HeightF = 23F;
            this.GroupHeaderItemType.KeepTogether = true;
            this.GroupHeaderItemType.Name = "GroupHeaderItemType";
            // 
            // ItemType
            // 
            this.ItemType.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Items.Item.ItemType")});
            this.ItemType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemType.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.ItemType.Name = "ItemType";
            this.ItemType.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ItemType.SizeF = new System.Drawing.SizeF(650.0001F, 23F);
            this.ItemType.StyleName = "BlackBold";
            this.ItemType.StylePriority.UseFont = false;
            this.ItemType.StylePriority.UseTextAlignment = false;
            this.ItemType.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupFooterItemType
            // 
            this.GroupFooterItemType.HeightF = 0F;
            this.GroupFooterItemType.Name = "GroupFooterItemType";
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.NotesTable});
            this.GroupFooter1.HeightF = 77.50009F;
            this.GroupFooter1.Level = 1;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // xrLine1
            // 
            this.xrLine1.BackColor = System.Drawing.Color.DodgerBlue;
            this.xrLine1.BorderColor = System.Drawing.Color.DodgerBlue;
            this.xrLine1.BorderWidth = 0F;
            this.xrLine1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(415.1667F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(234.8333F, 2F);
            this.xrLine1.StylePriority.UseBackColor = false;
            this.xrLine1.StylePriority.UseBorderColor = false;
            this.xrLine1.StylePriority.UseBorderWidth = false;
            this.xrLine1.StylePriority.UseForeColor = false;
            // 
            // NotesTable
            // 
            this.NotesTable.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.NotesTable.Name = "NotesTable";
            this.NotesTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow14,
            this.xrTableRow15});
            this.NotesTable.SizeF = new System.Drawing.SizeF(649.5416F, 50F);
            this.NotesTable.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.NotesTable_BeforePrint);
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell31,
            this.xrTableCell32});
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 1D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StyleName = "BlueBold";
            this.xrTableCell31.Text = "Notes";
            this.xrTableCell31.Weight = 0.18693299515629119D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Notes")});
            this.xrTableCell32.Multiline = true;
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StyleName = "Paragraph";
            this.xrTableCell32.Weight = 1.8130670048437088D;
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell34});
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 1D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Terms")});
            this.xrTableCell34.Multiline = true;
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StyleName = "Paragraph";
            this.xrTableCell34.Weight = 2D;
            // 
            // QuoteEquipmentInvisible
            // 
            this.QuoteEquipmentInvisible.Condition = "[QuoteEquipments] == null";
            this.QuoteEquipmentInvisible.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.QuoteEquipmentInvisible.Name = "QuoteEquipmentInvisible";
            // 
            // QuoteEquipmentVisible
            // 
            this.QuoteEquipmentVisible.Condition = "[QuoteEquipments] != null";
            this.QuoteEquipmentVisible.Formatting.Visible = DevExpress.Utils.DefaultBoolean.True;
            this.QuoteEquipmentVisible.Name = "QuoteEquipmentVisible";
            // 
            // collectionDataSourceSettings
            // 
            this.collectionDataSourceSettings.Name = "collectionDataSourceSettings";
            this.collectionDataSourceSettings.ObjectTypeName = "MyWorkbench.BusinessObjects.Settings";
            this.collectionDataSourceSettings.TopReturnedRecords = 0;
            // 
            // ImagesVisible
            // 
            this.ImagesVisible.Condition = "[QuoteImages] == null";
            this.ImagesVisible.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.ImagesVisible.Name = "ImagesVisible";
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(550F, 0F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // RequestForQuoteReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.DetailReport,
            this.PageFooter,
            this.ReportHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.collectionDataSourceQuote,
            this.collectionDataSourceSettings});
            this.DataSource = this.collectionDataSourceQuote;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.QuoteEquipmentInvisible,
            this.QuoteEquipmentVisible,
            this.ImagesVisible});
            this.Version = "18.1";
            this.AfterPrint += new System.EventHandler(this.RequestForQuote_AfterPrint);
            this.Controls.SetChildIndex(this.ReportHeader, 0);
            this.Controls.SetChildIndex(this.PageFooter, 0);
            this.Controls.SetChildIndex(this.DetailReport, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.CompanyTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSourceQuote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotesTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSourceSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }


        #endregion

        protected DevExpress.Persistent.Base.ReportsV2.CollectionDataSource collectionDataSourceQuote;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeaderItemType;
        private DevExpress.XtraReports.UI.XRLabel ItemType;
        private DevExpress.XtraReports.UI.XRLabel ItemVATAmount;
        private DevExpress.XtraReports.UI.XRLabel ItemDescription;
        private DevExpress.XtraReports.UI.XRLabel ItemStockCode;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooterItemType;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRTable NotesTable;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow14;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell31;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell32;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow15;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell34;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.FormattingRule QuoteEquipmentInvisible;
        private DevExpress.XtraReports.UI.FormattingRule QuoteEquipmentVisible;
        protected DevExpress.Persistent.Base.ReportsV2.CollectionDataSource collectionDataSourceSettings;
        private DevExpress.XtraReports.UI.FormattingRule ImagesVisible;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
    }
}
