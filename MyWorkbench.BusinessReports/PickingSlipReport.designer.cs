namespace MyWorkbench.BusinessReports
{
    partial class PickingSlipReport
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
            this.collectionDataSourceMassInventoryMovement = new DevExpress.Persistent.Base.ReportsV2.CollectionDataSource();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.AdditionalTable = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.AdditionalTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.AdditionalTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.AdditionalTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.DetailReport3 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.NotesTable = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.collectionDataSourceSettings = new DevExpress.Persistent.Base.ReportsV2.CollectionDataSource();
            this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSourceMassInventoryMovement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotesTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSourceSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.AdditionalTable,
            this.xrLine1});
            this.Detail.HeightF = 108.2083F;
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 113.5417F;
            this.PageHeader.PrintOn = DevExpress.XtraReports.UI.PrintOnPages.NotWithReportHeader;
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
            new DevExpress.XtraReports.UI.XRBinding("Text", this.collectionDataSourceSettings, "Address.FormattedFullAddress")});
            this.CompanyAddress.StylePriority.UseBorders = false;
            this.CompanyAddress.StylePriority.UseTextAlignment = false;
            this.CompanyAddress.Text = "";
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PickingSlipNumber")});
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DateOfIssue")});
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Text = "";
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            // 
            // lblReportHeader
            // 
            this.lblReportHeader.StylePriority.UseForeColor = false;
            this.lblReportHeader.StylePriority.UseTextAlignment = false;
            this.lblReportHeader.Text = "PICKING SLIP";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", this.collectionDataSourceSettings, "Logo")});
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(310.5848F, 105.4583F);
            // 
            // collectionDataSourceMassInventoryMovement
            // 
            this.collectionDataSourceMassInventoryMovement.Name = "collectionDataSourceMassInventoryMovement";
            this.collectionDataSourceMassInventoryMovement.ObjectTypeName = "MyWorkbench.BusinessObjects.MassInventoryMovement";
            this.collectionDataSourceMassInventoryMovement.TopReturnedRecords = 0;
            // 
            // xrLine1
            // 
            this.xrLine1.BackColor = System.Drawing.Color.DodgerBlue;
            this.xrLine1.BorderColor = System.Drawing.Color.DodgerBlue;
            this.xrLine1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(650F, 3F);
            this.xrLine1.StylePriority.UseBackColor = false;
            this.xrLine1.StylePriority.UseBorderColor = false;
            this.xrLine1.StylePriority.UseForeColor = false;
            // 
            // AdditionalTable
            // 
            this.AdditionalTable.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 10.00001F);
            this.AdditionalTable.Name = "AdditionalTable";
            this.AdditionalTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6,
            this.AdditionalTableRow1,
            this.AdditionalTableRow2,
            this.AdditionalTableRow3});
            this.AdditionalTable.SizeF = new System.Drawing.SizeF(314.5833F, 91F);
            this.AdditionalTable.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.AdditionalTable_BeforePrint);
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StyleName = "BlueBold";
            this.xrTableCell17.Text = "Info";
            this.xrTableCell17.Weight = 2D;
            // 
            // AdditionalTableRow1
            // 
            this.AdditionalTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18,
            this.xrTableCell19});
            this.AdditionalTableRow1.Name = "AdditionalTableRow1";
            this.AdditionalTableRow1.Weight = 0.88D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StyleName = "BlackBold";
            this.xrTableCell18.Text = "From";
            this.xrTableCell18.Weight = 0.63576086311742153D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "FromLocation.Location")});
            this.xrTableCell19.Multiline = true;
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StyleName = "Paragraph";
            this.xrTableCell19.Weight = 1.3642391368825784D;
            // 
            // AdditionalTableRow2
            // 
            this.AdditionalTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell21});
            this.AdditionalTableRow2.Name = "AdditionalTableRow2";
            this.AdditionalTableRow2.Weight = 0.88000000000000012D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StyleName = "BlackBold";
            this.xrTableCell20.Text = "To";
            this.xrTableCell20.Weight = 0.63576086311742153D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ToLocation.Location")});
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StyleName = "Paragraph";
            this.xrTableCell21.Weight = 1.3642391368825784D;
            // 
            // AdditionalTableRow3
            // 
            this.AdditionalTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell22,
            this.xrTableCell23});
            this.AdditionalTableRow3.Name = "AdditionalTableRow3";
            this.AdditionalTableRow3.Weight = 0.88D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StyleName = "BlackBold";
            this.xrTableCell22.Text = "Reason";
            this.xrTableCell22.Weight = 0.63576144517457833D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Reason")});
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StyleName = "Paragraph";
            this.xrTableCell23.Weight = 1.3642385548254215D;
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.HeightF = 0F;
            this.GroupHeader3.Name = "GroupHeader3";
            // 
            // DetailReport3
            // 
            this.DetailReport3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.GroupFooter2});
            this.DetailReport3.DataMember = "MassInventoryMovementItem";
            this.DetailReport3.DataSource = this.collectionDataSourceMassInventoryMovement;
            this.DetailReport3.Level = 1;
            this.DetailReport3.Name = "DetailReport3";
            // 
            // Detail4
            // 
            this.Detail4.HeightF = 0F;
            this.Detail4.Name = "Detail4";
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.NotesTable,
            this.xrLine2});
            this.GroupFooter2.HeightF = 32.29167F;
            this.GroupFooter2.Name = "GroupFooter2";
            this.GroupFooter2.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            this.GroupFooter2.PrintAtBottom = true;
            // 
            // NotesTable
            // 
            this.NotesTable.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 5F);
            this.NotesTable.Name = "NotesTable";
            this.NotesTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow15});
            this.NotesTable.SizeF = new System.Drawing.SizeF(629.9974F, 25F);
            this.NotesTable.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.NotesTable_BeforePrint);
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell33,
            this.xrTableCell34});
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 1D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StyleName = "BlueBold";
            this.xrTableCell33.Text = "Terms";
            this.xrTableCell33.Weight = 0.12943167331300803D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Terms")});
            this.xrTableCell34.Multiline = true;
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StyleName = "Paragraph";
            this.xrTableCell34.Weight = 1.8705683266869919D;
            // 
            // xrLine2
            // 
            this.xrLine2.BackColor = System.Drawing.Color.DodgerBlue;
            this.xrLine2.BorderColor = System.Drawing.Color.DodgerBlue;
            this.xrLine2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(650F, 3F);
            this.xrLine2.StylePriority.UseBackColor = false;
            this.xrLine2.StylePriority.UseBorderColor = false;
            this.xrLine2.StylePriority.UseForeColor = false;
            // 
            // collectionDataSourceSettings
            // 
            this.collectionDataSourceSettings.Name = "collectionDataSourceSettings";
            this.collectionDataSourceSettings.ObjectTypeName = "MyWorkbench.BusinessObjects.Settings";
            this.collectionDataSourceSettings.TopReturnedRecords = 0;
            // 
            // DetailReport1
            // 
            this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupHeader2});
            this.DetailReport1.DataMember = "MassInventoryMovementItems";
            this.DetailReport1.DataSource = this.collectionDataSourceMassInventoryMovement;
            this.DetailReport1.Level = 0;
            this.DetailReport1.Name = "DetailReport1";
            // 
            // Detail2
            // 
            this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLabel5,
            this.xrLabel6});
            this.Detail2.HeightF = 23F;
            this.Detail2.Name = "Detail2";
            // 
            // xrLabel4
            // 
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MassInventoryMovementItems.Quantity")});
            this.xrLabel4.Font = new System.Drawing.Font("Calibri", 10F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(561.4587F, 0F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(88.54129F, 23F);
            this.xrLabel4.StylePriority.UseFont = false;
            // 
            // xrLabel5
            // 
            this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MassInventoryMovementItems.Item.Description")});
            this.xrLabel5.Font = new System.Drawing.Font("Calibri", 10F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(151.8333F, 0F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(409.6248F, 23F);
            this.xrLabel5.StylePriority.UseFont = false;
            // 
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MassInventoryMovementItems.Item.StockCode")});
            this.xrLabel6.Font = new System.Drawing.Font("Calibri", 10F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 0F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(151.8333F, 23F);
            this.xrLabel6.StylePriority.UseFont = false;
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel2,
            this.xrLabel3});
            this.GroupHeader2.HeightF = 23F;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // xrLabel1
            // 
            this.xrLabel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.xrLabel1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.ForeColor = System.Drawing.Color.White;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(151.8333F, 23F);
            this.xrLabel1.StylePriority.UseBackColor = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseForeColor = false;
            this.xrLabel1.Text = "Item";
            // 
            // xrLabel2
            // 
            this.xrLabel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.xrLabel2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.ForeColor = System.Drawing.Color.White;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(561.4581F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(88.54153F, 23F);
            this.xrLabel2.StylePriority.UseBackColor = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseForeColor = false;
            this.xrLabel2.Text = "Quantity";
            // 
            // xrLabel3
            // 
            this.xrLabel3.BackColor = System.Drawing.Color.DodgerBlue;
            this.xrLabel3.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.ForeColor = System.Drawing.Color.White;
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(151.8333F, 0F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(409.6248F, 23F);
            this.xrLabel3.StylePriority.UseBackColor = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseForeColor = false;
            this.xrLabel3.Text = "Description";
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
            // PickingSlipReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.GroupHeader3,
            this.DetailReport3,
            this.PageFooter,
            this.ReportHeader,
            this.DetailReport1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.collectionDataSourceMassInventoryMovement,
            this.collectionDataSourceSettings});
            this.DataSource = this.collectionDataSourceMassInventoryMovement;
            this.Version = "18.1";
            this.AfterPrint += new System.EventHandler(this.PickingSlipReport_AfterPrint);
            this.Controls.SetChildIndex(this.DetailReport1, 0);
            this.Controls.SetChildIndex(this.ReportHeader, 0);
            this.Controls.SetChildIndex(this.PageFooter, 0);
            this.Controls.SetChildIndex(this.DetailReport3, 0);
            this.Controls.SetChildIndex(this.GroupHeader3, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.CompanyTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSourceMassInventoryMovement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdditionalTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotesTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSourceSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }


        #endregion

        protected DevExpress.Persistent.Base.ReportsV2.CollectionDataSource collectionDataSourceMassInventoryMovement;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRTable AdditionalTable;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell17;
        private DevExpress.XtraReports.UI.XRTableRow AdditionalTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell18;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell19;
        private DevExpress.XtraReports.UI.XRTableRow AdditionalTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell20;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell21;
        private DevExpress.XtraReports.UI.XRTableRow AdditionalTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell22;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell23;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader3;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport3;
        private DevExpress.XtraReports.UI.DetailBand Detail4;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter2;
        private DevExpress.XtraReports.UI.XRTable NotesTable;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow15;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell33;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell34;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        protected DevExpress.Persistent.Base.ReportsV2.CollectionDataSource collectionDataSourceSettings;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport1;
        private DevExpress.XtraReports.UI.DetailBand Detail2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
    }
}
