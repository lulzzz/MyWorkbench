using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using MyWorkbench.BusinessObjects.Lookups;
using DevExpress.Data.Filtering;
using MyWorkbench.BusinessObjects;
using MyWorkbench.BusinessObjects.Accounts;
using MyWorkbench.BusinessObjects.Inventory;
using System.Globalization;
using Ignyt.BusinessInterface;

namespace MyWorkbench.Module.DatabaseUpdate
{
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }

        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();

            if (ObjectSpace.FindObject<Settings>(null) == null)
                this.CreateSettings();

            this.CreateStatuses();
            this.CreateInventoryLocations();
            this.CreateItems();
            this.PopulateCultures();
            this.CreateAccountCategories();
            this.CreateVATTypes();
            this.Update();
        }

        private void CreateItems()
        {
            Item item = ObjectSpace.FindObject<Item>(new BinaryOperator("StockCode", "SAM001"));

            if (item == null)
            {
                item = ObjectSpace.CreateObject<Item>();
                item.CostPrice = 100;
                item.Description = "Widget";
                item.SellingPrice = 200;
                item.StockCode = "SAM001";
            }
        }

        private void CreateInventoryLocations()
        {
            InventoryLocation invLocation = ObjectSpace.FindObject<InventoryLocation>(new BinaryOperator("DefaultLocation", true));

            if (invLocation == null)
            {
                invLocation = ObjectSpace.CreateObject<InventoryLocation>();
                invLocation.Description = "Primary Warehouse";
                invLocation.DefaultLocation = true;
            }
        }

        private void CreateStatuses()
        {
            Status status = ObjectSpace.FindObject<Status>(null);

            if (status == null)
            {
                CreateStatus("Not Started", "#d3d3d3", false, true, WorkFlowType.All);
                CreateStatus("In Progress", "#a5c25c", false, false, WorkFlowType.All);
                CreateStatus("Completed", "#144955", true, false, WorkFlowType.All);
                CreateStatus("Cancelled", "#8b0807", false, false, WorkFlowType.All);
                CreateStatus("Decline", "#8b0807", false, false, WorkFlowType.All);
                CreateStatus("Rescheduled", "#d3d3d3", false, false, WorkFlowType.All);
                CreateStatus("Postponed", "#FFFF00", false, false, WorkFlowType.All);
            }
        }

        private void CreateStatus(string description, string color, bool IsCompleted, bool IsDefault, WorkFlowType WorkFlowType)
        {
            Status status = ObjectSpace.CreateObject<Status>();
            status.Description = description;
            status.Color = System.Drawing.ColorTranslator.FromHtml(color);
            status.IsCompleted = IsCompleted;
            status.IsDefault = IsDefault;
            status.WorkFlowType = WorkFlowType;
            status.Save();
            status.Session.CommitTransaction();
        }

        private void CreateSettings()
        {
            if (ObjectSpace.FindObject<Settings>(null) == null)
            {
                Settings result = ObjectSpace.CreateObject<Settings>();
                result.CompanyName = "My Workbench Company";
                result.CompanyName = "My Workbench Company Description";
                PhysicalAddress address = ObjectSpace.CreateObject<PhysicalAddress>();
                address.Street = "10 My Workbench Streetr";
                address.City = "Johannesburg";
                address.Suburb = "Fictionville";
                result.Address = address;
                result.EmailAddress = "info@acme.com";
                result.PhoneNo = "+27111234567";
                result.ApplicationName = "My Workbench";
                result.ApplicationID = "4d71a4e1-3675-46d2-856a-74d006a6b093";
                result.DefaultDepositPercentage = 50;
                result.VatRegistered = false;

                // Settings Prefix
                SettingsPrefix settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(Project);
                settingsPrefix.Prefix = "P";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(JobCard);
                settingsPrefix.Prefix = "J";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(Quote);
                settingsPrefix.Prefix = "Q";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(Invoice);
                settingsPrefix.Prefix = "I";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(CreditNote);
                settingsPrefix.Prefix = "CR";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(Purchase);
                settingsPrefix.Prefix = "P";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(RequestForQuote);
                settingsPrefix.Prefix = "RFQ";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(Ticket);
                settingsPrefix.Prefix = "T";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(Sale);
                settingsPrefix.Prefix = "S";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(RecurringInvoice);
                settingsPrefix.Prefix = "RI";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(RecurringJobCard);
                settingsPrefix.Prefix = "RJ";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(SupplierInvoice);
                settingsPrefix.Prefix = "SI";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(SupplierCreditNote);
                settingsPrefix.Prefix = "SC";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(MyWorkbench.BusinessObjects.Task);
                settingsPrefix.Prefix = "A";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(Booking);
                settingsPrefix.Prefix = "B";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(RecurringBooking);
                settingsPrefix.Prefix = "RB";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                settingsPrefix.DataType = typeof(RecurringTask);
                settingsPrefix.Prefix = "RT";
                settingsPrefix.SystemGenerated = true;
                result.SystemPrefixes.Add(settingsPrefix);

                result.Save();

                // Settings Terms
                SettingsTerms settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(Project);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Project Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(JobCard);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Jobcard Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(Quote);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Quote Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(Invoice);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Invoice Terms.Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(CreditNote);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Credit Note Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(Purchase);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Purchase Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(SupplierInvoice);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Supplier Invoice Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(SupplierCreditNote);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Supplier Credit Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(RequestForQuote);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Request For Quote Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(Ticket);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Ticket Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(MassInventoryMovement);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Inventory Movement Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(Sale);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Sale Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(RecurringInvoice);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Recurring Invoice Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(RecurringJobCard);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Recurring JobCard Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(Booking);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Booking Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(MyWorkbench.BusinessObjects.Task);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Task Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(MyWorkbench.BusinessObjects.RecurringBooking);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Recurring Booking Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                settingsTerms.DataType = typeof(MyWorkbench.BusinessObjects.RecurringTask);
                settingsTerms.SystemGenerated = true;
                settingsTerms.Terms = "These are the default Recurring Task Terms. Please replace them with your terms in settings";
                result.SystemTerms.Add(settingsTerms);

                result.Save();
            }
        }

        private void Update()
        {
            Settings result = ObjectSpace.FindObject<Settings>(null);

            if (result != null)
            {
                if (ObjectSpace.FindObject<SettingsPrefix>(new BinaryOperator("Prefix", "S")) == null)
                {
                    SettingsPrefix settingsPrefix = ObjectSpace.CreateObject<SettingsPrefix>();
                    settingsPrefix.DataType = typeof(Sale);
                    settingsPrefix.Prefix = "S";
                    settingsPrefix.SystemGenerated = true;
                    result.SystemPrefixes.Add(settingsPrefix);

                    SettingsTerms settingsTerms = ObjectSpace.CreateObject<SettingsTerms>();
                    settingsTerms.DataType = typeof(Sale);
                    settingsTerms.SystemGenerated = true;
                    settingsTerms.Terms = "These are the default Sale Terms. Please replace them with your terms in settings";
                    result.SystemTerms.Add(settingsTerms);
                }
            }
        }

        private void PopulateCultures()
        {
            foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                this.CreateCulture(cultureInfo.Name, cultureInfo.DisplayName, cultureInfo.TwoLetterISOLanguageName, cultureInfo.ThreeLetterISOLanguageName);
            }
        }

        private Culture CreateCulture(string Name, string DisplayName, string TwoLetterISOLanguageName, string ThreeLetterISOLanguageName)
        {
            Culture culture = ObjectSpace.FindObject<Culture>(new BinaryOperator("Name", Name));

            if (culture == null)
            {
                culture = ObjectSpace.CreateObject<Culture>();
                culture.Name = Name;
                culture.DisplayName = DisplayName;
                culture.TwoLetterISOLanguageName = TwoLetterISOLanguageName;
                culture.ThreeLetterISOLanguageName = ThreeLetterISOLanguageName;

                culture.Save();
            }

            return culture;
        }

        private void CreateAccountCategories()
        {
            AccountCategory accountCategory = ObjectSpace.FindObject<AccountCategory>(null);

            if (accountCategory == null)
            {
                CreateAccountCategory("Non-item based sales.", "Sales");
                CreateAccountCategory("Any costs associated with Sales. Used to calculate gross profit.", "Cost Of Sales");
                CreateAccountCategory("Income received such as interest and discount received.", "Other Income");
                CreateAccountCategory("Cost incurred. Advertising, rent, stationary, and so on.", "Expenses");
                CreateAccountCategory("Taxes levied on the net income of the company.", "Income Tax");
                CreateAccountCategory("Items of value lasting for an extended period of time such as property.", "Non Current Assets");
                CreateAccountCategory("Assets expected to be sold or used in under a year such as cash.", "Current Assets");
                CreateAccountCategory("Liabilities to be settled in the future. loans, mortgages and so on.", "Non Current Liabilities");
                CreateAccountCategory("Liabilities expected to be settled within a year such as tax owed.", "Current Liabilities");
                CreateAccountCategory("Owner's interest.", "Owners Equity");
            }
        }

        private void CreateAccountCategory(string description, string name)
        {
            AccountCategory category = ObjectSpace.CreateObject<AccountCategory>();
            category.Description = description;
            category.Name = name;
            category.Save();
            category.Session.CommitTransaction();
        }

        private void CreateVATTypes()
        {
            VATType vatType = ObjectSpace.FindObject<VATType>(null);

            if (vatType == null)
            {
                CreateVATType("Non-item based sales.", "NoVAT",0);
                CreateVATType("Old Standard Rate(14.00%)", "OldStandardRate",0.14);
                CreateVATType("Standard Rate(15.00%)", "StandardRate",0.15);
                CreateVATType("Standard Rate(Capital Goods)(15.00%)", "StandardRateCapitalGoods",0.15);
                CreateVATType("Old Standard Rate(Capital Goods)(14.00%)", "OldStandardRateCapitalGoods",0.14);
                CreateVATType("Zero Rate(00.00%)", "ZeroRate",0);
                CreateVATType("Zero Rate Export(00.00%)", "ZeroRateExport",0);
                CreateVATType("Exempt And Non Supplies(14.00%)", "ExemptAndNonSupplies",0.14);
                CreateVATType("Old Export Of Second Hand Goods(15.00%)", "OldExportOfSecondHandGoods",0.15);
                CreateVATType("Export Of Second Hand Goods(15.00%)", "ExportOfSecondHandGoods",0.15);
                CreateVATType("Change In Use(15.00%)", "ChangeInUse",0.15);
                CreateVATType("Old Change In Use(14.00%)", "OldChangeInUse",0.14);
                CreateVATType("Goods And Services Imported(100.00%)", "GoodsAndServicesImported",100);
                CreateVATType("Capital Goods Imported(100.00%)", "CapitalGoodsImported",100);
                CreateVATType("VAT Adjustments(100.00%)", "VATAdjustments",100);
                CreateVATType("Manual VAT", "ManualVAT",0);
                CreateVATType("Manual VAT(Capital Goods)", "ManualVATCapitalGoods",0);
            }
        }

        private void CreateVATType(string description, string name, double value)
        {
            VATType vATType = ObjectSpace.CreateObject<VATType>();
            vATType.Description = description;
            vATType.Name = name;
            vATType.Value = value;
            vATType.Save();
            vATType.Session.CommitTransaction();
        }
    }
}