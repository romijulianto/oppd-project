namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// trRequest
    /// </summary>
    [MaybeNull]
    public static TrRequest trRequest
    {
        get => HttpData.Resolve<TrRequest>("tr_request");
        set => HttpData["tr_request"] = value;
    }

    /// <summary>
    /// Table class for tr_request
    /// </summary>
    public class TrRequest : DbTable, IQueryFactory
    {
        public int RowCount = 0; // DN

        public bool UseSessionForListSql = true;

        // Column CSS classes
        public string LeftColumnClass = "col-sm-2 col-form-label ew-label";

        public string RightColumnClass = "col-sm-10";

        public string OffsetColumnClass = "col-sm-10 offset-sm-2";

        public string TableLeftColumnClass = "w-col-2";

        // Ajax / Modal
        public bool UseAjaxActions = false;

        public bool ModalSearch = false;

        public bool ModalView = false;

        public bool ModalAdd = false;

        public bool ModalEdit = false;

        public bool ModalUpdate = false;

        public bool InlineDelete = false;

        public bool ModalGridAdd = false;

        public bool ModalGridEdit = false;

        public bool ModalMultiEdit = false;

        public readonly DbField<SqlDbType> id;

        public readonly DbField<SqlDbType> Request_No;

        public readonly DbField<SqlDbType> Reference_Doc;

        public readonly DbField<SqlDbType> Reason;

        public readonly DbField<SqlDbType> Request_By;

        public readonly DbField<SqlDbType> Request_By_Name;

        public readonly DbField<SqlDbType> Request_By_Position;

        public readonly DbField<SqlDbType> Request_By_Branch;

        public readonly DbField<SqlDbType> Request_By_Region;

        public readonly DbField<SqlDbType> Request_Industry;

        public readonly DbField<SqlDbType> Customer_ID;

        public readonly DbField<SqlDbType> Customer_Name;

        public readonly DbField<SqlDbType> SAP_Total;

        public readonly DbField<SqlDbType> Override_Total;

        public readonly DbField<SqlDbType> Variance_Total;

        public readonly DbField<SqlDbType> Variance_Percent;

        public readonly DbField<SqlDbType> Notes;

        public readonly DbField<SqlDbType> Next_Approver;

        public readonly DbField<SqlDbType> Request_PIC;

        public readonly DbField<SqlDbType> Request_Status;

        public readonly DbField<SqlDbType> List_Approver;

        public readonly DbField<SqlDbType> Last_Update_By;

        public readonly DbField<SqlDbType> Created_By;

        public readonly DbField<SqlDbType> Created_Date;

        public readonly DbField<SqlDbType> ETL_Date;

        public readonly DbField<SqlDbType> Request_Currency;

        // Constructor
        public TrRequest()
        {
            // Language object // DN
            Language = ResolveLanguage();
            TableVar = "tr_request";
            Name = "tr_request";
            Type = "TABLE";
            UpdateTable = "dbo.tr_request"; // Update Table
            DbId = "DB"; // DN
            ExportAll = true;
            ExportPageBreakCount = 0; // Page break per every n record (PDF only)
            ExportPageOrientation = "portrait"; // Page orientation (PDF only)
            ExportPageSize = "a4"; // Page size (PDF only)
            ExportColumnWidths = new float[] {  }; // Column widths (PDF only) // DN
            ExportExcelPageOrientation = ""; // Page orientation (EPPlus only)
            ExportExcelPageSize = ""; // Page size (EPPlus only)
            ExportWordPageOrientation = ""; // Page orientation (Word only)
            DetailAdd = true; // Allow detail add
            DetailEdit = true; // Allow detail edit
            DetailView = true; // Allow detail view
            ShowMultipleDetails = true; // Show multiple details
            GridAddRowCount = 5;
            SearchHighlight = true;
            AllowAddDeleteRow = true; // Allow add/delete row
            UseAjaxActions = UseAjaxActions || Config.UseAjaxActions;
            UseColumnVisibility = true;
            UserIdAllowSecurity = Config.DefaultUserIdAllowSecurity; // User ID Allow

            // id
            id = new (this, "x_id", 3, SqlDbType.Int) {
                Name = "id",
                Expression = "[id]",
                BasicSearchExpression = "CAST([id] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[id]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                IsPrimaryKey = true, // Primary key field
                IsForeignKey = true, // Foreign key field
                Nullable = false, // NOT NULL field
                Required = true, // Required field
                DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN" },
                CustomMessage = Language.FieldPhrase("tr_request", "id", "CustomMsg"),
                IsUpload = false
            };
            Fields.Add("id", id);

            // Request_No
            Request_No = new (this, "x_Request_No", 202, SqlDbType.NVarChar) {
                Name = "Request_No",
                Expression = "[Request_No]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Request_No]",
                DateTimeFormat = -1,
                VirtualExpression = "[Request_No]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Request_No", "CustomMsg"),
                IsUpload = false
            };
            Request_No.Lookup = new Lookup<DbField>(Request_No, "tr_request", true, "Request_No", new List<string> {"Request_No", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Request_No", Request_No);

            // Reference_Doc
            Reference_Doc = new (this, "x_Reference_Doc", 202, SqlDbType.NVarChar) {
                Name = "Reference_Doc",
                Expression = "[Reference_Doc]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Reference_Doc]",
                DateTimeFormat = -1,
                VirtualExpression = "[Reference_Doc]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Reference_Doc", "CustomMsg"),
                IsUpload = false
            };
            Reference_Doc.Lookup = new Lookup<DbField>(Reference_Doc, "tr_request", true, "Reference_Doc", new List<string> {"Reference_Doc", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Reference_Doc", Reference_Doc);

            // Reason
            Reason = new (this, "x_Reason", 202, SqlDbType.NVarChar) {
                Name = "Reason",
                Expression = "[Reason]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Reason]",
                DateTimeFormat = -1,
                VirtualExpression = "[Reason]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "SELECT",
                InputTextType = "text",
                UsePleaseSelect = true, // Use PleaseSelect by default
                PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Reason", "CustomMsg"),
                IsUpload = false
            };
            Reason.Lookup = new Lookup<DbField>(Reason, "mt_reason", true, "Reason_Type", new List<string> {"Reason_Type", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Reason_Type]");
            Fields.Add("Reason", Reason);

            // Request_By
            Request_By = new (this, "x_Request_By", 202, SqlDbType.NVarChar) {
                Name = "Request_By",
                Expression = "[Request_By]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Request_By]",
                DateTimeFormat = -1,
                VirtualExpression = "[Request_By]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Request_By", "CustomMsg"),
                IsUpload = false
            };
            Request_By.Lookup = new Lookup<DbField>(Request_By, "tr_request", true, "Request_By", new List<string> {"Request_By", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Request_By.GetAutoUpdateValue = () => CurrentUserID();
            Fields.Add("Request_By", Request_By);

            // Request_By_Name
            Request_By_Name = new (this, "x_Request_By_Name", 202, SqlDbType.NVarChar) {
                Name = "Request_By_Name",
                Expression = "[Request_By_Name]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Request_By_Name]",
                DateTimeFormat = -1,
                VirtualExpression = "[Request_By_Name]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Request_By_Name", "CustomMsg"),
                IsUpload = false
            };
            Request_By_Name.Lookup = new Lookup<DbField>(Request_By_Name, "tr_request", true, "Request_By_Name", new List<string> {"Request_By_Name", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Request_By_Name.GetAutoUpdateValue = () => CurrentUserName();
            Fields.Add("Request_By_Name", Request_By_Name);

            // Request_By_Position
            Request_By_Position = new (this, "x_Request_By_Position", 202, SqlDbType.NVarChar) {
                Name = "Request_By_Position",
                Expression = "[Request_By_Position]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Request_By_Position]",
                DateTimeFormat = -1,
                VirtualExpression = "[Request_By_Position]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Request_By_Position", "CustomMsg"),
                IsUpload = false
            };
            Request_By_Position.Lookup = new Lookup<DbField>(Request_By_Position, "tr_request", true, "Request_By_Position", new List<string> {"Request_By_Position", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Request_By_Position.GetAutoUpdateValue = () => CurrentUserLevel();
            Fields.Add("Request_By_Position", Request_By_Position);

            // Request_By_Branch
            Request_By_Branch = new (this, "x_Request_By_Branch", 202, SqlDbType.NVarChar) {
                Name = "Request_By_Branch",
                Expression = "[Request_By_Branch]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Request_By_Branch]",
                DateTimeFormat = -1,
                VirtualExpression = "[Request_By_Branch]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Request_By_Branch", "CustomMsg"),
                IsUpload = false
            };
            Request_By_Branch.Lookup = new Lookup<DbField>(Request_By_Branch, "tr_request", true, "Request_By_Branch", new List<string> {"Request_By_Branch", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Request_By_Branch", Request_By_Branch);

            // Request_By_Region
            Request_By_Region = new (this, "x_Request_By_Region", 202, SqlDbType.NVarChar) {
                Name = "Request_By_Region",
                Expression = "[Request_By_Region]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Request_By_Region]",
                DateTimeFormat = -1,
                VirtualExpression = "[Request_By_Region]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Request_By_Region", "CustomMsg"),
                IsUpload = false
            };
            Request_By_Region.Lookup = new Lookup<DbField>(Request_By_Region, "tr_request", true, "Request_By_Region", new List<string> {"Request_By_Region", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Request_By_Region", Request_By_Region);

            // Request_Industry
            Request_Industry = new (this, "x_Request_Industry", 202, SqlDbType.NVarChar) {
                Name = "Request_Industry",
                Expression = "[Request_Industry]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Request_Industry]",
                DateTimeFormat = -1,
                VirtualExpression = "[Request_Industry]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Request_Industry", "CustomMsg"),
                IsUpload = false
            };
            Request_Industry.Lookup = new Lookup<DbField>(Request_Industry, "tr_request", true, "Request_Industry", new List<string> {"Request_Industry", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Request_Industry", Request_Industry);

            // Customer_ID
            Customer_ID = new (this, "x_Customer_ID", 202, SqlDbType.NVarChar) {
                Name = "Customer_ID",
                Expression = "[Customer_ID]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Customer_ID]",
                DateTimeFormat = -1,
                VirtualExpression = "[Customer_ID]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Customer_ID", "CustomMsg"),
                IsUpload = false
            };
            Customer_ID.Lookup = new Lookup<DbField>(Customer_ID, "tr_request", true, "Customer_ID", new List<string> {"Customer_ID", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Customer_ID", Customer_ID);

            // Customer_Name
            Customer_Name = new (this, "x_Customer_Name", 202, SqlDbType.NVarChar) {
                Name = "Customer_Name",
                Expression = "[Customer_Name]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Customer_Name]",
                DateTimeFormat = -1,
                VirtualExpression = "[Customer_Name]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Customer_Name", "CustomMsg"),
                IsUpload = false
            };
            Customer_Name.Lookup = new Lookup<DbField>(Customer_Name, "tr_request", true, "Customer_Name", new List<string> {"Customer_Name", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Customer_Name", Customer_Name);

            // SAP_Total
            SAP_Total = new (this, "x_SAP_Total", 5, SqlDbType.Float) {
                Name = "SAP_Total",
                Expression = "[SAP_Total]",
                BasicSearchExpression = "CAST([SAP_Total] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[SAP_Total]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                DefaultErrorMessage = Language.Phrase("IncorrectFloat"),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "SAP_Total", "CustomMsg"),
                IsUpload = false
            };
            SAP_Total.Lookup = new Lookup<DbField>(SAP_Total, "tr_request", true, "SAP_Total", new List<string> {"SAP_Total", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("SAP_Total", SAP_Total);

            // Override_Total
            Override_Total = new (this, "x_Override_Total", 5, SqlDbType.Float) {
                Name = "Override_Total",
                Expression = "[Override_Total]",
                BasicSearchExpression = "CAST([Override_Total] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[Override_Total]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                DefaultErrorMessage = Language.Phrase("IncorrectFloat"),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Override_Total", "CustomMsg"),
                IsUpload = false
            };
            Override_Total.Lookup = new Lookup<DbField>(Override_Total, "tr_request", true, "Override_Total", new List<string> {"Override_Total", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Override_Total", Override_Total);

            // Variance_Total
            Variance_Total = new (this, "x_Variance_Total", 5, SqlDbType.Float) {
                Name = "Variance_Total",
                Expression = "[Variance_Total]",
                BasicSearchExpression = "CAST([Variance_Total] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[Variance_Total]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                DefaultErrorMessage = Language.Phrase("IncorrectFloat"),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Variance_Total", "CustomMsg"),
                IsUpload = false
            };
            Variance_Total.Lookup = new Lookup<DbField>(Variance_Total, "tr_request", true, "Variance_Total", new List<string> {"Variance_Total", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Variance_Total", Variance_Total);

            // Variance_Percent
            Variance_Percent = new (this, "x_Variance_Percent", 5, SqlDbType.Float) {
                Name = "Variance_Percent",
                Expression = "[Variance_Percent]",
                BasicSearchExpression = "CAST([Variance_Percent] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[Variance_Percent]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                DefaultErrorMessage = Language.Phrase("IncorrectFloat"),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Variance_Percent", "CustomMsg"),
                IsUpload = false
            };
            Variance_Percent.Lookup = new Lookup<DbField>(Variance_Percent, "tr_request", true, "Variance_Percent", new List<string> {"Variance_Percent", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Variance_Percent", Variance_Percent);

            // Notes
            Notes = new (this, "x_Notes", 202, SqlDbType.NVarChar) {
                Name = "Notes",
                Expression = "[Notes]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Notes]",
                DateTimeFormat = -1,
                VirtualExpression = "[Notes]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXTAREA",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Notes", "CustomMsg"),
                IsUpload = false
            };
            Notes.Lookup = new Lookup<DbField>(Notes, "tr_request", true, "Notes", new List<string> {"Notes", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Notes", Notes);

            // Next_Approver
            Next_Approver = new (this, "x_Next_Approver", 202, SqlDbType.NVarChar) {
                Name = "Next_Approver",
                Expression = "[Next_Approver]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Next_Approver]",
                DateTimeFormat = -1,
                VirtualExpression = "[Next_Approver]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Next_Approver", "CustomMsg"),
                IsUpload = false
            };
            Next_Approver.Lookup = new Lookup<DbField>(Next_Approver, "tr_request", true, "Next_Approver", new List<string> {"Next_Approver", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Next_Approver", Next_Approver);

            // Request_PIC
            Request_PIC = new (this, "x_Request_PIC", 202, SqlDbType.NVarChar) {
                Name = "Request_PIC",
                Expression = "[Request_PIC]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Request_PIC]",
                DateTimeFormat = -1,
                VirtualExpression = "[Request_PIC]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Request_PIC", "CustomMsg"),
                IsUpload = false
            };
            Request_PIC.Lookup = new Lookup<DbField>(Request_PIC, "tr_request", true, "Request_PIC", new List<string> {"Request_PIC", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Request_PIC", Request_PIC);

            // Request_Status
            Request_Status = new (this, "x_Request_Status", 202, SqlDbType.NVarChar) {
                Name = "Request_Status",
                Expression = "[Request_Status]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Request_Status]",
                DateTimeFormat = -1,
                VirtualExpression = "[Request_Status]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "SELECT",
                InputTextType = "text",
                UsePleaseSelect = true, // Use PleaseSelect by default
                PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
                UseFilter = true, // Table header filter
                OptionCount = 3,
                SearchOperators = new () { "=", "<>", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Request_Status", "CustomMsg"),
                IsUpload = false
            };
            Request_Status.Lookup = new Lookup<DbField>(Request_Status, "tr_request", true, "Request_Status", new List<string> {"Request_Status", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Request_Status", Request_Status);

            // List_Approver
            List_Approver = new (this, "x_List_Approver", 202, SqlDbType.NVarChar) {
                Name = "List_Approver",
                Expression = "[List_Approver]",
                UseBasicSearch = true,
                BasicSearchExpression = "[List_Approver]",
                DateTimeFormat = -1,
                VirtualExpression = "[List_Approver]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "List_Approver", "CustomMsg"),
                IsUpload = false
            };
            List_Approver.Lookup = new Lookup<DbField>(List_Approver, "tr_request", true, "List_Approver", new List<string> {"List_Approver", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("List_Approver", List_Approver);

            // Last_Update_By
            Last_Update_By = new (this, "x_Last_Update_By", 202, SqlDbType.NVarChar) {
                Name = "Last_Update_By",
                Expression = "[Last_Update_By]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Last_Update_By]",
                DateTimeFormat = -1,
                VirtualExpression = "[Last_Update_By]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Last_Update_By", "CustomMsg"),
                IsUpload = false
            };
            Last_Update_By.Lookup = new Lookup<DbField>(Last_Update_By, "tr_request", true, "Last_Update_By", new List<string> {"Last_Update_By", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Last_Update_By.GetAutoUpdateValue = () => CurrentUserID();
            Fields.Add("Last_Update_By", Last_Update_By);

            // Created_By
            Created_By = new (this, "x_Created_By", 202, SqlDbType.NVarChar) {
                Name = "Created_By",
                Expression = "[Created_By]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Created_By]",
                DateTimeFormat = -1,
                VirtualExpression = "[Created_By]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Created_By", "CustomMsg"),
                IsUpload = false
            };
            Created_By.Lookup = new Lookup<DbField>(Created_By, "tr_request", true, "Created_By", new List<string> {"Created_By", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Created_By.GetAutoUpdateValue = () => CurrentUserID();
            Fields.Add("Created_By", Created_By);

            // Created_Date
            Created_Date = new (this, "x_Created_Date", 135, SqlDbType.DateTime) {
                Name = "Created_Date",
                Expression = "[Created_Date]",
                BasicSearchExpression = CastDateFieldForLike("[Created_Date]", 0, "DB"),
                DateTimeFormat = 0,
                VirtualExpression = "[Created_Date]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                DefaultErrorMessage = ConvertToString(Language.Phrase("IncorrectDate")).Replace("%s", CurrentDateTimeFormat.ShortDatePattern),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Created_Date", "CustomMsg"),
                IsUpload = false
            };
            Created_Date.Lookup = new Lookup<DbField>(Created_Date, "tr_request", true, "Created_Date", new List<string> {"Created_Date", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Created_Date.GetAutoUpdateValue = () => CurrentDateTime();
            Fields.Add("Created_Date", Created_Date);

            // ETL_Date
            ETL_Date = new (this, "x_ETL_Date", 135, SqlDbType.DateTime) {
                Name = "ETL_Date",
                Expression = "[ETL_Date]",
                BasicSearchExpression = CastDateFieldForLike("[ETL_Date]", 0, "DB"),
                DateTimeFormat = 0,
                VirtualExpression = "[ETL_Date]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                Sortable = false, // Allow sort
                DefaultErrorMessage = ConvertToString(Language.Phrase("IncorrectDate")).Replace("%s", CurrentDateTimeFormat.ShortDatePattern),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "ETL_Date", "CustomMsg"),
                IsUpload = false
            };
            Fields.Add("ETL_Date", ETL_Date);

            // Request_Currency
            Request_Currency = new (this, "x_Request_Currency", 202, SqlDbType.NVarChar) {
                Name = "Request_Currency",
                Expression = "[Request_Currency]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Request_Currency]",
                DateTimeFormat = -1,
                VirtualExpression = "[Request_Currency]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request", "Request_Currency", "CustomMsg"),
                IsUpload = false
            };
            Fields.Add("Request_Currency", Request_Currency);

            // Call Table Load event
            TableLoad();
        }

        // Set Field Visibility
        public override bool GetFieldVisibility(string fldname)
        {
            var fld = FieldByName(fldname);
            return fld?.Visible ?? false; // Returns original value
        }

        // Invoke method // DN
        public object? Invoke(string name, object?[]? parameters = null)
        {
            var mi = this.GetType().GetMethod(name);
            if (mi != null)
                return IsAsyncMethod(mi)
                    ? InvokeAsync(mi, parameters).GetAwaiter().GetResult()
                    : mi.Invoke(this, parameters);
            return null;
        }

        // Invoke async method // DN
        public async Task<object?> InvokeAsync(MethodInfo? mi, object?[]? parameters = null)
        {
            if (mi != null) {
                dynamic? awaitable = mi.Invoke(this, parameters);
                if (awaitable != null) {
                    await awaitable;
                    return awaitable.GetAwaiter().GetResult();
                }
            }
            return null;
        }

        #pragma warning disable 1998
        // Invoke async method // DN
        public async Task<object> InvokeAsync(string name, object[]? parameters = null) => InvokeAsync(this.GetType().GetMethod(name), parameters);
        #pragma warning restore 1998

        // Check if Invoke async method // DN
        public bool IsAsyncMethod(MethodInfo? mi)
        {
            if (mi != null) {
                Type attType = typeof(AsyncStateMachineAttribute);
                return mi.GetCustomAttribute(attType) is AsyncStateMachineAttribute;
            }
            return false;
        }

        // Check if Invoke async method // DN
        public bool IsAsyncMethod(string name) => IsAsyncMethod(this.GetType().GetMethod(name));

        #pragma warning disable 618
        // Connection
        public virtual DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> Connection => GetConnection(DbId);
        #pragma warning restore 618

        // Get query factory
        public QueryFactory GetQueryFactory(bool main) => Connection.GetQueryFactory(main);

        // Get query builder
        public QueryBuilder GetQueryBuilder(bool main, string output = "")
        {
            var qf = GetQueryFactory(main);
            var qb = (XQuery)qf.Query(UpdateTable);
            if (output != "")
                qb.Compiler = Connection.GetCompiler(output); // Replace the compiler
            return qb;
        }

        // Get query builder with output Id (use secondary connection)
        public QueryBuilder GetQueryBuilder(string output) => GetQueryBuilder(false, output);

        // Get query builder without output Id (use secondary connection)
        public QueryBuilder GetQueryBuilder() => GetQueryBuilder(false);

        // Set left column class (must be predefined col-*-* classes of Bootstrap grid system)
        public void SetLeftColumnClass(string columnClass)
        {
            Match m = Regex.Match(columnClass, @"^col\-(\w+)\-(\d+)$");
            if (m.Success) {
                LeftColumnClass = columnClass + " col-form-label ew-label";
                RightColumnClass = "col-" + m.Groups[1].Value + "-" + ConvertToString(12 - ConvertToInt(m.Groups[2].Value));
                OffsetColumnClass = RightColumnClass + " " + columnClass.Replace("col-", "offset-");
                TableLeftColumnClass = Regex.Replace(columnClass, @"/^col-\w+-(\d+)$/", "w-col-$1"); // Change to w-col-*
            }
        }

        // Single column sort
        public void UpdateSort(DbField fld)
        {
            string lastSort, sortField, curSort, orderBy;
            if (CurrentOrder == fld.Name) {
                sortField = fld.Expression;
                lastSort = fld.Sort;
                if ((new[] { "ASC", "DESC", "NO" }).Contains(CurrentOrderType)) {
                    curSort = CurrentOrderType;
                } else {
                    curSort = lastSort;
                }
                orderBy = (new[] { "ASC", "DESC" }).Contains(curSort) ? sortField + " " + curSort : "";
                SessionOrderBy = orderBy; // Save to Session
            }
        }

        // Update field sort
        public void UpdateFieldSort()
        {
            string orderBy = SessionOrderBy; // Get ORDER BY from Session
            var flds = GetSortFields(orderBy);
            foreach (var (key, field) in Fields) {
                string fldSort = "";
                foreach (var fld in flds) {
                    if (fld[0] == field.Expression || fld[0] == field.VirtualExpression)
                        fldSort = fld[1];
                }
                field.Sort = fldSort;
            }
        }

        // Current detail table name
        public string CurrentDetailTable
        {
            get => Session.GetString(Config.ProjectName + "_" + TableVar + "_" + Config.TableDetailTable);
            set => Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableDetailTable] = value;
        }

        // List of current detail table names
        public List<string> CurrentDetailTables => CurrentDetailTable.Split(',').ToList();

        // Get detail URL
        public string DetailUrl
        {
            get {
                string detailUrl = "";
                if (CurrentDetailTable == "tr_request_detail" && trRequestDetail != null) {
                    detailUrl = trRequestDetail.ListUrl + "?" + Config.TableShowMaster + "=" + TableVar;
                    detailUrl += "&" + ForeignKeyUrl("fk_id", id.CurrentValue);
                }
                if (CurrentDetailTable == "tr_request_approver" && trRequestApprover != null) {
                    detailUrl = trRequestApprover.ListUrl + "?" + Config.TableShowMaster + "=" + TableVar;
                    detailUrl += "&" + ForeignKeyUrl("fk_id", id.CurrentValue);
                }
                if (CurrentDetailTable == "tr_request_approval_history" && trRequestApprovalHistory != null) {
                    detailUrl = trRequestApprovalHistory.ListUrl + "?" + Config.TableShowMaster + "=" + TableVar;
                    detailUrl += "&" + ForeignKeyUrl("fk_id", id.CurrentValue);
                }
                if (Empty(detailUrl))
                    detailUrl = "TrRequestList";
                return detailUrl;
            }
        }

        #pragma warning disable 1998
        // Render X Axis for chart
        public async Task<Dictionary<string, object>> RenderChartXAxis(string chartVar, Dictionary<string, object> chartRow)
        {
            return chartRow;
        }
        #pragma warning restore 1998

        // Table level SQL
        // FROM
        private string? _sqlFrom = null;

        public string SqlFrom
        {
            get => _sqlFrom ?? "dbo.tr_request";
            set => _sqlFrom = value;
        }

        // SELECT
        private string? _sqlSelect = null;

        public string SqlSelect { // Select
            get => _sqlSelect ?? "SELECT * FROM " + SqlFrom;
            set => _sqlSelect = value;
        }

        // WHERE // DN
        private string? _sqlWhere = null;

        public string SqlWhere
        {
            get {
                string where = "";
                return _sqlWhere ?? where;
            }
            set {
                _sqlWhere = value;
            }
        }

        // Group By
        private string? _sqlGroupBy = null;

        public string SqlGroupBy
        {
            get => _sqlGroupBy ?? "";
            set => _sqlGroupBy = value;
        }

        // Having
        private string? _sqlHaving = null;

        public string SqlHaving
        {
            get => _sqlHaving ?? "";
            set => _sqlHaving = value;
        }

        // Order By
        private string? _sqlOrderBy = null;

        public string SqlOrderBy
        {
            get => _sqlOrderBy ?? "";
            set => _sqlOrderBy = value;
        }

        // Apply User ID filters
        public string ApplyUserIDFilters(string filter, string id = "")
        {
            return filter;
        }

        // Check if User ID security allows view all
        public bool UserIDAllow(string id = "")
        {
            int allow = UserIdAllowSecurity;
            return id switch {
                "add" => ((allow & 1) == 1),
                "copy" => ((allow & 1) == 1),
                "gridadd" => ((allow & 1) == 1),
                "register" => ((allow & 1) == 1),
                "addopt" => ((allow & 1) == 1),
                "edit" => ((allow & 4) == 4),
                "gridedit" => ((allow & 4) == 4),
                "update" => ((allow & 4) == 4),
                "changepassword" => ((allow & 4) == 4),
                "resetpassword" => ((allow & 4) == 4),
                "delete" => ((allow & 2) == 2),
                "view" => ((allow & 32) == 32),
                "search" => ((allow & 64) == 64),
                "lookup" => ((allow & 256) == 256),
                _ => ((allow & 8) == 8)
            };
        }

        /// <summary>
        /// Get record count by reading data reader (Async) // DN
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="c">Connection</param>
        /// <returns>Record count</returns>
        public async Task<int> GetRecordCountAsync(string sql, dynamic? c = null) {
            try {
                var cnt = 0;
                var conn = c ?? Connection;
                using var dr = await conn.OpenDataReaderAsync(sql);
                if (dr != null) {
                    while (await dr.ReadAsync())
                        cnt++;
                }
                return cnt;
            } catch {
                if (Config.Debug)
                    throw;
                return -1;
            }
        }

        /// <summary>
        /// Get record count by reading data reader // DN
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="c">Connection</param>
        /// <returns>Record count</returns>
        public int GetRecordCount(string sql, dynamic? c = null) => GetRecordCountAsync(sql, c).GetAwaiter().GetResult();

        /// <summary>
        /// Try to get record count by SELECT COUNT(*) (Async) // DN
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="c">Connection</param>
        /// <returns>Record count</returns>
        public async Task<int> TryGetRecordCountAsync(string sql, dynamic? c = null)
        {
            string orderBy = OrderBy;
            var conn = c ?? Connection;
            sql = Regex.Replace(sql, @"/\*BeginOrderBy\*/[\s\S]+/\*EndOrderBy\*/", "", RegexOptions.IgnoreCase).Trim(); // Remove ORDER BY clause (MSSQL)
            if (!Empty(orderBy) && sql.EndsWith(orderBy))
                sql = sql.Substring(0, sql.Length - orderBy.Length); // Remove ORDER BY clause
            try {
                string sqlcnt;
                if ((new[] { "TABLE", "VIEW", "LINKTABLE" }).Contains(Type) && sql.StartsWith(SqlSelect)) { // Handle Custom Field
                    sqlcnt = "SELECT COUNT(*) FROM " + SqlFrom + sql.Substring(SqlSelect.Length);
                } else {
                    sqlcnt = "SELECT COUNT(*) FROM (" + sql + ") EW_COUNT_TABLE";
                }
                return Convert.ToInt32(await conn?.ExecuteScalarAsync(sqlcnt));
            } catch {
                return await GetRecordCountAsync(sql, conn);
            }
        }

        /// <summary>
        /// Try to get record count by SELECT COUNT(*) // DN
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="c">Connection</param>
        /// <returns>Record count</returns>
        public int TryGetRecordCount(string sql, dynamic? c = null) => TryGetRecordCountAsync(sql, c).GetAwaiter().GetResult();

        // Get SQL
        public string GetSql(string where, string orderBy = "") => BuildSelectSql(SqlSelect, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, where, orderBy);

        // Table SQL
        public string CurrentSql
        {
            get {
                string filter = CurrentFilter;
                filter = ApplyUserIDFilters(filter); // Add User ID filter
                string sort = SessionOrderBy;
                return GetSql(filter, sort);
            }
        }

        // Table SQL with List page filter
        public string ListSql
        {
            get {
                string sort = "";
                string select = "";
                string filter = UseSessionForListSql ? SessionWhere : "";
                AddFilter(ref filter, CurrentFilter);
                RecordsetSelecting(ref filter);
                filter = ApplyUserIDFilters(filter); // Add User ID filter
                select = SqlSelect;
                sort = UseSessionForListSql ? SessionOrderBy : "";
                return BuildSelectSql(select, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, filter, sort);
            }
        }

        // Get ORDER BY clause
        public string OrderBy
        {
            get {
                string sort = SessionOrderBy;
                return BuildSelectSql("", "", "", "", SqlOrderBy, "", sort);
            }
        }

        /// <summary>
        /// Get record count based on filter (for detail record count in master table pages) (Async) // DN
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>Record count</returns>
        public async Task<int> LoadRecordCountAsync(string filter) => await TryGetRecordCountAsync(GetSql(filter));

        /// <summary>
        /// Get record count based on filter (for detail record count in master table pages) // DN
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>Record count</returns>
        public int LoadRecordCount(string filter) => TryGetRecordCount(GetSql(filter));

        /// <summary>
        /// Get record count (for current List page) (Async) // DN
        /// </summary>
        /// <returns>Record count</returns>
        public async Task<int> ListRecordCountAsync() => await TryGetRecordCountAsync(ListSql);

        /// <summary>
        /// Get record count (for current List page) // DN
        /// </summary>
        /// <returns>Record count</returns>
        public int ListRecordCount() => TryGetRecordCount(ListSql);

        /// <summary>
        /// Insert (Async)
        /// </summary>
        /// <param name="data">Data to be inserted (IDictionary|Anonymous)</param>
        /// <returns>Row affected</returns>
        public async Task<int> InsertAsync(object data)
        {
            int result = 0;
            IDictionary<string, object> row;
            if (data is IDictionary<string, object> d)
                row = d;
            else if (IsAnonymousType(data))
                row = ConvertToDictionary<object>(data);
            else
                throw new ArgumentException("Data must be of anonymous type or Dictionary<string, object> type", "data");
            row = row.Where(kvp => {
                var fld = FieldByName(kvp.Key);
                return fld != null && !fld.IsCustom;
            }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            if (row.Count == 0)
                return -1;
            var queryBuilder = GetQueryBuilder();
            try {
                result = await queryBuilder.InsertAsync(row);
            } catch (Exception e) {
                CurrentPage?.SetFailureMessage(e.Message);
                if (Config.Debug)
                    throw;
            }
            return result;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">Data to be inserted (IDictionary|Anonymous)</param>
        /// <returns>Row affected</returns>
        public int Insert(object data) => InsertAsync(data).GetAwaiter().GetResult();

        /// <summary>
        /// Update (Async)
        /// </summary>
        /// <param name="data">Data to be updated (IDictionary|Anonymous)</param>
        /// <returns>Row affected</returns>
        public async Task<int> UpdateAsync(object data)
        {
            IDictionary<string, object> row;
            if (data is IDictionary<string, object> d)
                row = d;
            else if (IsAnonymousType(data))
                row = ConvertToDictionary<object>(data);
            else
                throw new ArgumentException("Data must be of anonymous type or Dictionary<string, object> type", "data");
            var where = GetRowFilterAsDictionary(row);
            return where != null ? await UpdateAsync(row, where) : -1; // Prevent update all record
        }

        /// <summary>
        /// Update (Async)
        /// </summary>
        /// <param name="data">Data to be updated (IDictionary|Anonymous)</param>
        /// <param name="where">Where (IDictionary|Anonymous|string)</param>
        /// <returns>Row affected</returns>
        public async Task<int> UpdateAsync(object data, object? where) => await UpdateAsync(data, where, null);

        #pragma warning disable 168, 219
        /// <summary>
        /// Update (Async)
        /// </summary>
        /// <param name="data">Data to be updated (IDictionary|Anonymous)</param>
        /// <param name="where">Where (IDictionary|Anonymous)</param>
        /// <param name="rsold">Old record</param>
        /// <returns>Row affected</returns>
        public async Task<int> UpdateAsync(object data, object? where, Dictionary<string, object>? rsold)
        {
            int result = -1;
            IDictionary<string, object> row;
            if (data is IDictionary<string, object> d)
                row = d;
            else if (IsAnonymousType(data))
                row = ConvertToDictionary<object>(data);
            else
                throw new ArgumentException("Data must be of anonymous type or Dictionary<string, object> type", "data");
            bool cascadeUpdate = false;
            DbDataReader? drwrk;
            int updateResult;
            Dictionary<string, object> rscascade = new ();
            if (rsold != null) {
                // Cascade Update detail table 'tr_request_detail'
                cascadeUpdate = false;
                rscascade.Clear();
                if (row.ContainsKey("id") && !Empty(row["id"]) && !SameString(rsold["id"], row["id"])) {
                    cascadeUpdate = true;
                    rscascade.Add("request_id", row["id"]);
                }
                if (cascadeUpdate) {
                    Dictionary<string, object>? rskey = new ();
                    trRequestDetail = Resolve("tr_request_detail")!;
                    if (trRequestDetail != null) {
                        var rows = await trRequestDetail.Connection.GetRowsAsync(trRequestDetail.GetSql("[request_id] = " + QuotedValue(rsold["id"], DataType.Number, "DB")));
                        foreach (var rsdtlold in rows) {
                            rskey = trRequestDetail.GetRowFilterAsDictionary(rsdtlold);
                            var rsdtlnew = new Dictionary<string, object>(rsdtlold);
                            foreach (var (key, value) in rscascade)
                                rsdtlnew[key] = value;
                            bool update = trRequestDetail.Invoke("RowUpdating", new object[] { rsdtlold, rsdtlnew }) is bool b && b; // Call Row Updating event
                            if (update)
                                update = await trRequestDetail.UpdateAsync(rscascade, rskey, rsdtlold) >= 0; // Update
                            if (!update)
                                return -1;
                            trRequestDetail.Invoke("RowUpdated", new object[] { rsdtlold, rsdtlnew }); // Call Row Updated event
                        }
                    }
                }

                // Cascade Update detail table 'tr_request_approver'
                cascadeUpdate = false;
                rscascade.Clear();
                if (row.ContainsKey("id") && !Empty(row["id"]) && !SameString(rsold["id"], row["id"])) {
                    cascadeUpdate = true;
                    rscascade.Add("request_id", row["id"]);
                }
                if (cascadeUpdate) {
                    Dictionary<string, object>? rskey = new ();
                    trRequestApprover = Resolve("tr_request_approver")!;
                    if (trRequestApprover != null) {
                        var rows = await trRequestApprover.Connection.GetRowsAsync(trRequestApprover.GetSql("[request_id] = " + QuotedValue(rsold["id"], DataType.Number, "DB")));
                        foreach (var rsdtlold in rows) {
                            rskey = trRequestApprover.GetRowFilterAsDictionary(rsdtlold);
                            var rsdtlnew = new Dictionary<string, object>(rsdtlold);
                            foreach (var (key, value) in rscascade)
                                rsdtlnew[key] = value;
                            bool update = trRequestApprover.Invoke("RowUpdating", new object[] { rsdtlold, rsdtlnew }) is bool b && b; // Call Row Updating event
                            if (update)
                                update = await trRequestApprover.UpdateAsync(rscascade, rskey, rsdtlold) >= 0; // Update
                            if (!update)
                                return -1;
                            trRequestApprover.Invoke("RowUpdated", new object[] { rsdtlold, rsdtlnew }); // Call Row Updated event
                        }
                    }
                }

                // Cascade Update detail table 'tr_request_approval_history'
                cascadeUpdate = false;
                rscascade.Clear();
                if (row.ContainsKey("id") && !Empty(row["id"]) && !SameString(rsold["id"], row["id"])) {
                    cascadeUpdate = true;
                    rscascade.Add("request_id", row["id"]);
                }
                if (cascadeUpdate) {
                    Dictionary<string, object>? rskey = new ();
                    trRequestApprovalHistory = Resolve("tr_request_approval_history")!;
                    if (trRequestApprovalHistory != null) {
                        var rows = await trRequestApprovalHistory.Connection.GetRowsAsync(trRequestApprovalHistory.GetSql("[request_id] = " + QuotedValue(rsold["id"], DataType.Number, "DB")));
                        foreach (var rsdtlold in rows) {
                            rskey = trRequestApprovalHistory.GetRowFilterAsDictionary(rsdtlold);
                            var rsdtlnew = new Dictionary<string, object>(rsdtlold);
                            foreach (var (key, value) in rscascade)
                                rsdtlnew[key] = value;
                            bool update = trRequestApprovalHistory.Invoke("RowUpdating", new object[] { rsdtlold, rsdtlnew }) is bool b && b; // Call Row Updating event
                            if (update)
                                update = await trRequestApprovalHistory.UpdateAsync(rscascade, rskey, rsdtlold) >= 0; // Update
                            if (!update)
                                return -1;
                            trRequestApprovalHistory.Invoke("RowUpdated", new object[] { rsdtlold, rsdtlnew }); // Call Row Updated event
                        }
                    }
                }
            }
            row = row.Where(kvp => FieldByName(kvp.Key) is DbField fld && !fld.IsCustom).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            if (row.Count == 0)
                return -1;
            var queryBuilder = GetQueryBuilder();
            string filter = CurrentFilter;
            if (!Empty(filter))
                queryBuilder.WhereRaw(filter);
            if (IsAnonymousType(where))
                queryBuilder.Where(where);
            else if (where is IDictionary<string, object> dict)
                queryBuilder.Where(dict);
            else if (where is string)
                queryBuilder.WhereRaw((string)where);
            if (rsold != null && GetRowFilterAsDictionary(rsold) is IDictionary<string, object> rsoldFilter) // Add filter from old record
                queryBuilder.Where(rsoldFilter);
            if (queryBuilder.HasComponent("where")) // Prevent update all records
                result = await queryBuilder.UpdateAsync(row);
            return result;
        }
        #pragma warning restore 168, 219

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">Data to be updated (IDictionary|Anonymous)</param>
        /// <returns>Row affected</returns>
        public int Update(object data) => UpdateAsync(data).GetAwaiter().GetResult();

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">Data to be updated (IDictionary|Anonymous)</param>
        /// <param name="where">Where (IDictionary|Anonymous|string)</param>
        /// <returns>Row affected</returns>
        public int Update(object data, object where) => UpdateAsync(data, where).GetAwaiter().GetResult();

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">Data to be updated (IDictionary|Anonymous)</param>
        /// <param name="where">Where (IDictionary|Anonymous|string)</param>
        /// <param name="rsold">Old record</param>
        /// <returns>Row affected</returns>
        public int Update(object data, object where, Dictionary<string, object> rsold) => UpdateAsync(data, where, rsold).GetAwaiter().GetResult();

        #pragma warning disable 168, 1998
        /// <summary>
        /// Delete (Async)
        /// </summary>
        /// <param name="data">Data to be removed (IDictionary|Anonymous)</param>
        /// <param name="where">Where (IDictionary|Anonymous|string)</param>
        /// <returns>Row affected</returns>
        public async Task<int> DeleteAsync(object? data, object? where = null)
        {
            bool delete = true;
            IDictionary<string, object>? row = null;
            if (data is IDictionary<string, object> d)
                row = d;
            else if (IsAnonymousType(data))
                row = ConvertToDictionary<object>(data);
            List<Dictionary<string, object>>? dtlrows;
            if (row != null) {
                // Cascade delete detail table 'tr_request_detail'
                trRequestDetail = Resolve("tr_request_detail")!;
                if (trRequestDetail != null) {
                    dtlrows = await Connection.GetRowsAsync(trRequestDetail.GetSql("[request_id] = " + QuotedValue(row["id"], DataType.Number, "DB")));
                    delete = dtlrows.All(dtlrow => trRequestDetail.Invoke("RowDeleting", new object[] { dtlrow }) is bool b && b); // Call Row Deleting event
                    if (delete) {
                        foreach (var dtlrow in dtlrows) {
                            delete = ConvertToBool(await trRequestDetail.DeleteAsync(dtlrow)); // Delete
                            if (!delete)
                                break;
                        }
                    }
                    if (delete)
                        dtlrows.ForEach(dtlrow => trRequestDetail.Invoke("RowDeleted", new object[] { dtlrow })); // Call Row Deleted event
                }

                // Cascade delete detail table 'tr_request_approver'
                trRequestApprover = Resolve("tr_request_approver")!;
                if (trRequestApprover != null) {
                    dtlrows = await Connection.GetRowsAsync(trRequestApprover.GetSql("[request_id] = " + QuotedValue(row["id"], DataType.Number, "DB")));
                    delete = dtlrows.All(dtlrow => trRequestApprover.Invoke("RowDeleting", new object[] { dtlrow }) is bool b && b); // Call Row Deleting event
                    if (delete) {
                        foreach (var dtlrow in dtlrows) {
                            delete = ConvertToBool(await trRequestApprover.DeleteAsync(dtlrow)); // Delete
                            if (!delete)
                                break;
                        }
                    }
                    if (delete)
                        dtlrows.ForEach(dtlrow => trRequestApprover.Invoke("RowDeleted", new object[] { dtlrow })); // Call Row Deleted event
                }

                // Cascade delete detail table 'tr_request_approval_history'
                trRequestApprovalHistory = Resolve("tr_request_approval_history")!;
                if (trRequestApprovalHistory != null) {
                    dtlrows = await Connection.GetRowsAsync(trRequestApprovalHistory.GetSql("[request_id] = " + QuotedValue(row["id"], DataType.Number, "DB")));
                    delete = dtlrows.All(dtlrow => trRequestApprovalHistory.Invoke("RowDeleting", new object[] { dtlrow }) is bool b && b); // Call Row Deleting event
                    if (delete) {
                        foreach (var dtlrow in dtlrows) {
                            delete = ConvertToBool(await trRequestApprovalHistory.DeleteAsync(dtlrow)); // Delete
                            if (!delete)
                                break;
                        }
                    }
                    if (delete)
                        dtlrows.ForEach(dtlrow => trRequestApprovalHistory.Invoke("RowDeleted", new object[] { dtlrow })); // Call Row Deleted event
                }
            }
            var queryBuilder = GetQueryBuilder(true); // Use main connection
            if (GetRowFilterAsDictionary(row) is IDictionary<string, object> rowFilter)
                queryBuilder.Where(rowFilter);
            if (IsAnonymousType(where))
                queryBuilder.Where(where);
            else if (where is IDictionary<string, object> dict)
                queryBuilder.Where(dict);
            else if (where is string)
                queryBuilder.WhereRaw((string)where);
            int result = delete && queryBuilder.HasComponent("where") // Avoid delete if no WHERE clause
                ? await queryBuilder.DeleteAsync(Connection.Transaction)
                : -1;
            return result;
        }
        #pragma warning restore 168, 1998

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">Data to be removed (IDictionary|Anonymous)</param>
        /// <param name="where">Where (IDictionary|Anonymous|string)</param>
        /// <returns>Row affected</returns>
        public int Delete(object data, object? where = null) => DeleteAsync(data, where).GetAwaiter().GetResult();

        // Load DbValue from recordset
        public void LoadDbValues(Dictionary<string, object>? row)
        {
            if (row == null)
                return;
            try {
                id.DbValue = row["id"]; // Set DB value only
                Request_No.DbValue = row["Request_No"]; // Set DB value only
                Reference_Doc.DbValue = row["Reference_Doc"]; // Set DB value only
                Reason.DbValue = row["Reason"]; // Set DB value only
                Request_By.DbValue = row["Request_By"]; // Set DB value only
                Request_By_Name.DbValue = row["Request_By_Name"]; // Set DB value only
                Request_By_Position.DbValue = row["Request_By_Position"]; // Set DB value only
                Request_By_Branch.DbValue = row["Request_By_Branch"]; // Set DB value only
                Request_By_Region.DbValue = row["Request_By_Region"]; // Set DB value only
                Request_Industry.DbValue = row["Request_Industry"]; // Set DB value only
                Customer_ID.DbValue = row["Customer_ID"]; // Set DB value only
                Customer_Name.DbValue = row["Customer_Name"]; // Set DB value only
                SAP_Total.DbValue = row["SAP_Total"]; // Set DB value only
                Override_Total.DbValue = row["Override_Total"]; // Set DB value only
                Variance_Total.DbValue = row["Variance_Total"]; // Set DB value only
                Variance_Percent.DbValue = row["Variance_Percent"]; // Set DB value only
                Notes.DbValue = row["Notes"]; // Set DB value only
                Next_Approver.DbValue = row["Next_Approver"]; // Set DB value only
                Request_PIC.DbValue = row["Request_PIC"]; // Set DB value only
                Request_Status.DbValue = row["Request_Status"]; // Set DB value only
                List_Approver.DbValue = row["List_Approver"]; // Set DB value only
                Last_Update_By.DbValue = row["Last_Update_By"]; // Set DB value only
                Created_By.DbValue = row["Created_By"]; // Set DB value only
                Created_Date.DbValue = row["Created_Date"]; // Set DB value only
                ETL_Date.DbValue = row["ETL_Date"]; // Set DB value only
                Request_Currency.DbValue = row["Request_Currency"]; // Set DB value only
            } catch {}
        }

        public void DeleteUploadedFiles(Dictionary<string, object> row)
        {
            LoadDbValues(row);
        }

        // Record filter WHERE clause
        private string _sqlKeyFilter => "[id] = @id@";

        #pragma warning disable 168, 219
        // Get record filter as string
        public string GetRecordFilter(Dictionary<string, object>? row = null, bool current = false)
        {
            string keyFilter = _sqlKeyFilter;
            object? val = null, result = "";
            val = row?.TryGetValue("id", out result) ?? false
                ? result
                : !Empty(id.OldValue) && !current ? id.OldValue : id.CurrentValue;
            if (!IsNumeric(val))
                return "0=1"; // Invalid key
            if (val == null)
                return "0=1"; // Invalid key
            keyFilter = keyFilter.Replace("@id@", AdjustSql(val, DbId)); // Replace key value
            return keyFilter;
        }

        // Get record filter as Dictionary // DN
        public Dictionary<string, object>? GetRowFilterAsDictionary(IDictionary<string, object>? row = null)
        {
            Dictionary<string, object>? keyFilter = new ();
            object? val = null, result;
            val = row?.TryGetValue("id", out result) ?? false
                ? result
                : !Empty(id.OldValue) ? id.OldValue : id.CurrentValue;
            if (!IsNumeric(val))
                return null; // Invalid key
            if (Empty(val))
                return null; // Invalid key
            keyFilter.Add("id", val); // Add key value
            return keyFilter.Count > 0 ? keyFilter : null;
        }
        #pragma warning restore 168, 219

        // Return URL
        public string ReturnUrl
        {
            get {
                string name = Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl;
                // Get referer URL automatically
                if (!Empty(ReferUrl()) && !SameText(ReferPage(), CurrentPageName()) &&
                    !SameText(ReferPage(), "login")) {// Referer not same page or login page
                        Session[name] = ReferUrl(); // Save to Session
                }
                if (!Empty(Session[name])) {
                    return Session.GetString(name);
                } else {
                    return "TrRequestList";
                }
            }
            set {
                Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl] = value;
            }
        }

        // Get modal caption
        public string GetModalCaption(string pageName)
        {
            if (SameString(pageName, "TrRequestView"))
                return Language.Phrase("View");
            else if (SameString(pageName, "TrRequestEdit"))
                return Language.Phrase("Edit");
            else if (SameString(pageName, "TrRequestAdd"))
                return Language.Phrase("Add");
            else
                return "";
        }

        // Default route URL
        public string DefaultRouteUrl
        {
            get {
                return "TrRequestList";
            }
        }

        // API page name
        public string GetApiPageName(string action)
        {
            return action.ToLowerInvariant() switch {
                Config.ApiViewAction => "TrRequestView",
                Config.ApiAddAction => "TrRequestAdd",
                Config.ApiEditAction => "TrRequestEdit",
                Config.ApiDeleteAction => "TrRequestDelete",
                Config.ApiListAction => "TrRequestList",
                _ => String.Empty
            };
        }

        // Current URL
        public string GetCurrentUrl(string parm = "")
        {
            string url = CurrentPageName();
            if (!Empty(parm))
                url = KeyUrl(url, parm);
            else
                url = KeyUrl(url, Config.TableShowDetail + "=");
            return AddMasterUrl(url);
        }

        // List URL
        public string ListUrl => "TrRequestList";

        // View URL
        public string ViewUrl => GetViewUrl();

        // View URL
        public string GetViewUrl(string parm = "")
        {
            string url = "";
            if (!Empty(parm))
                url = KeyUrl("TrRequestView", parm);
            else
                url = KeyUrl("TrRequestView", Config.TableShowDetail + "=");
            return AddMasterUrl(url);
        }

        // Add URL
        public string AddUrl { get; set; } = "TrRequestAdd";

        // Add URL
        public string GetAddUrl(string parm = "")
        {
            string url = "";
            if (!Empty(parm))
                url = "TrRequestAdd?" + parm;
            else
                url = "TrRequestAdd";
            return AppPath(AddMasterUrl(url));
        }

        // Edit URL
        public string EditUrl => GetEditUrl();

        // Edit URL (with parameter)
        public string GetEditUrl(string parm = "")
        {
            string url = "";
            if (!Empty(parm))
                url = KeyUrl("TrRequestEdit", parm);
            else
                url = KeyUrl("TrRequestEdit", Config.TableShowDetail + "=");
            return AppPath(AddMasterUrl(url)); // DN
        }

        // Inline edit URL
        public string InlineEditUrl =>
            AppPath(AddMasterUrl(KeyUrl("TrRequestList", "action=edit"))); // DN

        // Copy URL
        public string CopyUrl => GetCopyUrl();

        // Copy URL
        public string GetCopyUrl(string parm = "")
        {
            string url = "";
            if (!Empty(parm))
                url = KeyUrl("TrRequestAdd", parm);
            else
                url = KeyUrl("TrRequestAdd", Config.TableShowDetail + "=");
            return AppPath(AddMasterUrl(url)); // DN
        }

        // Inline copy URL
        public string InlineCopyUrl =>
            AppPath(AddMasterUrl(KeyUrl("TrRequestList", "action=copy"))); // DN

        // Delete URL
        public string DeleteUrl => UseAjaxActions && Param<bool>("infinitescroll") && CurrentPageID() == "list"
            ? AppPath(KeyUrl(Config.ApiUrl + Config.ApiDeleteAction + "/" + TableVar))
            : AppPath(KeyUrl("TrRequestDelete")); // DN

        // Add master URL
        public string AddMasterUrl(string url)
        {
            return url;
        }

        // Get primary key as JSON
        public string KeyToJson(bool htmlEncode = false)
        {
            string json = "";
            json += "\"id\":" + ConvertToJson(id.CurrentValue, "number", true);
            json = "{" + json + "}";
            if (htmlEncode)
                json = HtmlEncode(json);
            return json;
        }

        // Add key value to URL
        public string KeyUrl(string url, string parm = "") { // DN
            if (!IsNull(id.CurrentValue)) {
                url += "/" + id.CurrentValue;
            } else {
                return "javascript:ew.alert(ew.language.phrase('InvalidRecord'));";
            }
            if (Empty(parm))
                return url;
            else
                return url + "?" + parm;
        }

        // Render sort
        public string RenderFieldHeader(DbField fld)
        {
            string sortUrl = "";
            string attrs = "";
            if (fld.Sortable) {
                sortUrl = SortUrl(fld);
                attrs = " role=\"button\" data-ew-action=\"sort\" data-ajax=\"" + (UseAjaxActions ? "true" : "false") + "\" data-sort-url=\"" + sortUrl + "\" data-sort-type=\"1\"";
                if (!Empty(ContextClass)) // Add context
                    attrs += " data-context=\"" + HtmlEncode(ContextClass) + "\"";
            }
            string html = "<div class=\"ew-table-header-caption\"" + attrs + ">" + fld.Caption + "</div>";
            if (!Empty(sortUrl)) {
                html += "<div class=\"ew-table-header-sort\">" + fld.SortIcon + "</div>";
            }
            if (!IsExport() && fld.UseFilter && Security.CanSearch) {
                html += "<div class=\"ew-filter-dropdown-btn\" data-ew-action=\"filter\" data-table=\"" + fld.TableVar + "\" data-field=\"" + fld.FieldVar +
                    "\"><div class=\"ew-table-header-filter\" role=\"button\" aria-haspopup=\"true\">" + Language.Phrase("Filter") + "</div></div>";
            }
            html = "<div class=\"ew-table-header-btn\">" + html + "</div>";
            if (UseCustomTemplate) {
                string scriptId = ("tpc_{id}").Replace("{id}", fld.TableVar + "_" + fld.Param);
                html = "<template id=\"" + scriptId + "\">" + html + "</template>";
            }
            return html;
        }

        // Sort URL (already URL-encoded)
        public string SortUrl(DbField fld)
        {
            if (!Empty(CurrentAction) || !Empty(Export) ||
                (new[] {141, 201, 203, 128, 204, 205}).Contains(fld.Type)) { // Unsortable data type
                return "";
            } else if (fld.Sortable) {
                string urlParm = "order=" + UrlEncode(fld.Name) + "&amp;ordertype=" + fld.NextSort;
                if (DashboardReport)
                    urlParm += "&amp;" + Config.PageDashboard + "=true";
                return AddMasterUrl(CurrentDashboardPageUrl() + "?" + urlParm);
            }
            return "";
        }

        #pragma warning disable 168, 219
        // Get key as string
        public string GetKey(bool current = false)
        {
            List<string> keys = new ();
            string val;
            val = current ? ConvertToString(id.CurrentValue) ?? "" : ConvertToString(id.OldValue) ?? "";
            if (Empty(val))
                return String.Empty;
            keys.Add(val);
            return String.Join(Config.CompositeKeySeparator, keys);
        }

        // Get record filter as string // DN
        public string GetKey(IDictionary<string, object> row)
        {
            List<string> keys = new ();
            object? val = null, result;
            val = row?.TryGetValue("id", out result) ?? false ? ConvertToString(result) : null;
            if (Empty(val))
                return String.Empty; // Invalid key
            keys.Add(ConvertToString(val)); // Add key value
            return String.Join(Config.CompositeKeySeparator, keys);
        }
        #pragma warning restore 168, 219

        // Set key
        public void SetKey(string key, bool current = false)
        {
            OldKey = key;
            string[] keys = OldKey.Split(Convert.ToChar(Config.CompositeKeySeparator));
            if (keys.Length == 1) {
                if (current) {
                    id.CurrentValue = keys[0];
                } else {
                    id.OldValue = keys[0];
                }
            }
        }

        #pragma warning disable 168
        // Get record keys
        public List<string> GetRecordKeys()
        {
            List<string> result = new ();
            StringValues sv;
            List<string> keysList = new ();
            if (Post("key_m[]", out sv) || Get("key_m[]", out sv)) { // DN
                keysList = ((StringValues)sv).Select(k => ConvertToString(k)).ToList();
            } else if (RouteValues.Count > 0 || Query.Count > 0 || Form.Count > 0) { // DN
                string key = "";
                string[] keyValues = {};
                if (IsApi() && RouteValues.TryGetValue("key", out object? k)) {
                    string str = ConvertToString(k);
                    keyValues = str.Split('/');
                }
                if (RouteValues.TryGetValue("id", out object? v0)) { // id // DN
                    key = UrlDecode(v0); // DN
                } else if (IsApi() && !Empty(keyValues)) {
                    key = keyValues[0];
                } else {
                    key = Param("id");
                }
                keysList.Add(key);
            }
            // Check keys
            foreach (var keys in keysList) {
                if (!IsNumeric(keys)) // id
                    continue;
                result.Add(keys);
            }
            return result;
        }
        #pragma warning restore 168

        // Get filter from records
        public string GetFilterFromRecords(IEnumerable<Dictionary<string, object>> rows) =>
            String.Join(" OR ", rows.Select(row => "(" + GetRecordFilter(row) + ")"));

        // Get filter from record keys
        public string GetFilterFromRecordKeys(bool setCurrent = true)
        {
            List<string> recordKeys = GetRecordKeys();
            string keyFilter = "";
            foreach (var keys in recordKeys) {
                if (!Empty(keyFilter))
                    keyFilter += " OR ";
                if (setCurrent)
                    id.CurrentValue = keys;
                else
                    id.OldValue = keys;
                keyFilter += "(" + GetRecordFilter() + ")";
            }
            return keyFilter;
        }

        #pragma warning disable 618
        // Load rows based on filter // DN
        public async Task<DbDataReader?> LoadReader(string filter, string sort = "", DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType>? conn = null)
        {
            // Set up filter (SQL WHERE clause) and get return SQL
            string sql = GetSql(filter, sort);
            try {
                var dr = await (conn ?? Connection).OpenDataReaderAsync(sql);
                if (dr?.HasRows ?? false)
                    return dr;
                else
                    dr?.Close(); // Close unused data reader // DN
            } catch {}
            return null;
        }
        #pragma warning restore 618

        // Load row values from recordset
        public void LoadListRowValues(DbDataReader? dr)
        {
            if (dr == null)
                return;
            id.SetDbValue(dr["id"]);
            Request_No.SetDbValue(dr["Request_No"]);
            Reference_Doc.SetDbValue(dr["Reference_Doc"]);
            Reason.SetDbValue(dr["Reason"]);
            Request_By.SetDbValue(dr["Request_By"]);
            Request_By_Name.SetDbValue(dr["Request_By_Name"]);
            Request_By_Position.SetDbValue(dr["Request_By_Position"]);
            Request_By_Branch.SetDbValue(dr["Request_By_Branch"]);
            Request_By_Region.SetDbValue(dr["Request_By_Region"]);
            Request_Industry.SetDbValue(dr["Request_Industry"]);
            Customer_ID.SetDbValue(dr["Customer_ID"]);
            Customer_Name.SetDbValue(dr["Customer_Name"]);
            SAP_Total.SetDbValue(dr["SAP_Total"]);
            Override_Total.SetDbValue(dr["Override_Total"]);
            Variance_Total.SetDbValue(dr["Variance_Total"]);
            Variance_Percent.SetDbValue(dr["Variance_Percent"]);
            Notes.SetDbValue(dr["Notes"]);
            Next_Approver.SetDbValue(dr["Next_Approver"]);
            Request_PIC.SetDbValue(dr["Request_PIC"]);
            Request_Status.SetDbValue(dr["Request_Status"]);
            List_Approver.SetDbValue(dr["List_Approver"]);
            Last_Update_By.SetDbValue(dr["Last_Update_By"]);
            Created_By.SetDbValue(dr["Created_By"]);
            Created_Date.SetDbValue(dr["Created_Date"]);
            ETL_Date.SetDbValue(dr["ETL_Date"]);
            Request_Currency.SetDbValue(dr["Request_Currency"]);
        }

        // Render list content
        public async Task<string> RenderListContent(string filter)
        {
            string pageName = "TrRequestList";
            dynamic? page = CreateInstance(pageName, new object[] { Controller }); // DN
            if (page != null) {
                page.UseLayout = false; // DN
                await page.LoadRecordsetFromFilter(filter);
                string html = await GetViewOutput(pageName, page, false);
                page.Terminate(); // Terminate page and clean up
                return html;
            }
            return "";
        }

        #pragma warning disable 1998
        // Render list row values
        public async Task RenderListRow()
        {
            // Call Row Rendering event
            RowRendering();

            // Common render codes

            // id
            id.CellCssStyle = "white-space: nowrap;";

            // Request_No
            Request_No.CellCssStyle = "white-space: nowrap;";

            // Reference_Doc
            Reference_Doc.CellCssStyle = "white-space: nowrap;";

            // Reason
            Reason.CellCssStyle = "white-space: nowrap;";

            // Request_By
            Request_By.CellCssStyle = "white-space: nowrap;";

            // Request_By_Name
            Request_By_Name.CellCssStyle = "white-space: nowrap;";

            // Request_By_Position
            Request_By_Position.CellCssStyle = "white-space: nowrap;";

            // Request_By_Branch
            Request_By_Branch.CellCssStyle = "white-space: nowrap;";

            // Request_By_Region
            Request_By_Region.CellCssStyle = "white-space: nowrap;";

            // Request_Industry
            Request_Industry.CellCssStyle = "white-space: nowrap;";

            // Customer_ID
            Customer_ID.CellCssStyle = "white-space: nowrap;";

            // Customer_Name
            Customer_Name.CellCssStyle = "white-space: nowrap;";

            // SAP_Total
            SAP_Total.CellCssStyle = "white-space: nowrap;";

            // Override_Total
            Override_Total.CellCssStyle = "white-space: nowrap;";

            // Variance_Total
            Variance_Total.CellCssStyle = "white-space: nowrap;";

            // Variance_Percent
            Variance_Percent.CellCssStyle = "white-space: nowrap;";

            // Notes
            Notes.CellCssStyle = "white-space: nowrap;";

            // Next_Approver
            Next_Approver.CellCssStyle = "white-space: nowrap;";

            // Request_PIC
            Request_PIC.CellCssStyle = "white-space: nowrap;";

            // Request_Status
            Request_Status.CellCssStyle = "white-space: nowrap;";

            // List_Approver
            List_Approver.CellCssStyle = "white-space: nowrap;";

            // Last_Update_By
            Last_Update_By.CellCssStyle = "white-space: nowrap;";

            // Created_By
            Created_By.CellCssStyle = "white-space: nowrap;";

            // Created_Date
            Created_Date.CellCssStyle = "white-space: nowrap;";

            // ETL_Date
            ETL_Date.CellCssStyle = "white-space: nowrap;";

            // Request_Currency

            // id
            id.ViewValue = id.CurrentValue;
            id.ViewValue = FormatNumber(id.ViewValue, id.FormatPattern);
            id.ViewCustomAttributes = "";

            // Request_No
            Request_No.ViewValue = ConvertToString(Request_No.CurrentValue); // DN
            Request_No.ViewCustomAttributes = "";

            // Reference_Doc
            Reference_Doc.ViewValue = ConvertToString(Reference_Doc.CurrentValue); // DN
            Reference_Doc.ViewCustomAttributes = "";

            // Reason
            curVal = ConvertToString(Reason.CurrentValue);
            if (!Empty(curVal)) {
                if (Reason.Lookup != null && IsDictionary(Reason.Lookup?.Options) && Reason.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                    Reason.ViewValue = Reason.LookupCacheOption(curVal);
                } else { // Lookup from database // DN
                    filterWrk = SearchFilter("[Reason_Type]", "=", Reason.CurrentValue, DataType.String, "");
                    sqlWrk = Reason.Lookup?.GetSql(false, filterWrk, null, this, true, true);
                    rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                    if (rswrk?.Count > 0 && Reason.Lookup != null) { // Lookup values found
                        var listwrk = Reason.Lookup?.RenderViewRow(rswrk[0]);
                        Reason.ViewValue = Reason.HighlightLookup(ConvertToString(rswrk[0]), Reason.DisplayValue(listwrk));
                    } else {
                        Reason.ViewValue = Reason.CurrentValue;
                    }
                }
            } else {
                Reason.ViewValue = DbNullValue;
            }
            Reason.ViewCustomAttributes = "";

            // Request_By
            Request_By.ViewValue = ConvertToString(Request_By.CurrentValue); // DN
            Request_By.ViewCustomAttributes = "";

            // Request_By_Name
            Request_By_Name.ViewValue = ConvertToString(Request_By_Name.CurrentValue); // DN
            Request_By_Name.ViewCustomAttributes = "";

            // Request_By_Position
            Request_By_Position.ViewValue = ConvertToString(Request_By_Position.CurrentValue); // DN
            Request_By_Position.ViewCustomAttributes = "";

            // Request_By_Branch
            Request_By_Branch.ViewValue = ConvertToString(Request_By_Branch.CurrentValue); // DN
            Request_By_Branch.ViewCustomAttributes = "";

            // Request_By_Region
            Request_By_Region.ViewValue = ConvertToString(Request_By_Region.CurrentValue); // DN
            Request_By_Region.ViewCustomAttributes = "";

            // Request_Industry
            Request_Industry.ViewValue = ConvertToString(Request_Industry.CurrentValue); // DN
            Request_Industry.ViewCustomAttributes = "";

            // Customer_ID
            Customer_ID.ViewValue = ConvertToString(Customer_ID.CurrentValue); // DN
            Customer_ID.ViewCustomAttributes = "";

            // Customer_Name
            Customer_Name.ViewValue = ConvertToString(Customer_Name.CurrentValue); // DN
            Customer_Name.ViewCustomAttributes = "";

            // SAP_Total
            SAP_Total.ViewValue = SAP_Total.CurrentValue;
            SAP_Total.ViewValue = FormatNumber(SAP_Total.ViewValue, SAP_Total.FormatPattern);
            SAP_Total.ViewCustomAttributes = "";

            // Override_Total
            Override_Total.ViewValue = Override_Total.CurrentValue;
            Override_Total.ViewValue = FormatNumber(Override_Total.ViewValue, Override_Total.FormatPattern);
            Override_Total.ViewCustomAttributes = "";

            // Variance_Total
            Variance_Total.ViewValue = Variance_Total.CurrentValue;
            Variance_Total.ViewValue = FormatNumber(Variance_Total.ViewValue, Variance_Total.FormatPattern);
            Variance_Total.ViewCustomAttributes = "";

            // Variance_Percent
            Variance_Percent.ViewValue = Variance_Percent.CurrentValue;
            Variance_Percent.ViewValue = FormatNumber(Variance_Percent.ViewValue, Variance_Percent.FormatPattern);
            Variance_Percent.ViewCustomAttributes = "";

            // Notes
            Notes.ViewValue = Notes.CurrentValue;
            Notes.ViewCustomAttributes = "";

            // Next_Approver
            Next_Approver.ViewValue = ConvertToString(Next_Approver.CurrentValue); // DN
            Next_Approver.ViewCustomAttributes = "";

            // Request_PIC
            Request_PIC.ViewValue = ConvertToString(Request_PIC.CurrentValue); // DN
            Request_PIC.ViewCustomAttributes = "";

            // Request_Status
            if (!Empty(Request_Status.CurrentValue)) {
                Request_Status.ViewValue = Request_Status.HighlightLookup(ConvertToString(Request_Status.CurrentValue), Request_Status.OptionCaption(ConvertToString(Request_Status.CurrentValue)));
            } else {
                Request_Status.ViewValue = DbNullValue;
            }
            Request_Status.ViewCustomAttributes = "";

            // List_Approver
            List_Approver.ViewValue = ConvertToString(List_Approver.CurrentValue); // DN
            List_Approver.ViewCustomAttributes = "";

            // Last_Update_By
            Last_Update_By.ViewValue = ConvertToString(Last_Update_By.CurrentValue); // DN
            Last_Update_By.ViewCustomAttributes = "";

            // Created_By
            Created_By.ViewValue = ConvertToString(Created_By.CurrentValue); // DN
            Created_By.ViewCustomAttributes = "";

            // Created_Date
            Created_Date.ViewValue = Created_Date.CurrentValue;
            Created_Date.ViewValue = FormatDateTime(Created_Date.ViewValue, Created_Date.FormatPattern);
            Created_Date.ViewCustomAttributes = "";

            // ETL_Date
            ETL_Date.ViewValue = ETL_Date.CurrentValue;
            ETL_Date.ViewValue = FormatDateTime(ETL_Date.ViewValue, ETL_Date.FormatPattern);
            ETL_Date.ViewCustomAttributes = "";

            // Request_Currency
            Request_Currency.ViewValue = ConvertToString(Request_Currency.CurrentValue); // DN
            Request_Currency.ViewCustomAttributes = "";

            // id
            id.HrefValue = "";
            id.TooltipValue = "";

            // Request_No
            Request_No.HrefValue = "";
            Request_No.TooltipValue = "";

            // Reference_Doc
            Reference_Doc.HrefValue = "";
            Reference_Doc.TooltipValue = "";

            // Reason
            Reason.HrefValue = "";
            Reason.TooltipValue = "";

            // Request_By
            Request_By.HrefValue = "";
            Request_By.TooltipValue = "";

            // Request_By_Name
            Request_By_Name.HrefValue = "";
            Request_By_Name.TooltipValue = "";

            // Request_By_Position
            Request_By_Position.HrefValue = "";
            Request_By_Position.TooltipValue = "";

            // Request_By_Branch
            Request_By_Branch.HrefValue = "";
            Request_By_Branch.TooltipValue = "";

            // Request_By_Region
            Request_By_Region.HrefValue = "";
            Request_By_Region.TooltipValue = "";

            // Request_Industry
            Request_Industry.HrefValue = "";
            Request_Industry.TooltipValue = "";

            // Customer_ID
            Customer_ID.HrefValue = "";
            Customer_ID.TooltipValue = "";

            // Customer_Name
            Customer_Name.HrefValue = "";
            Customer_Name.TooltipValue = "";

            // SAP_Total
            SAP_Total.HrefValue = "";
            SAP_Total.TooltipValue = "";

            // Override_Total
            Override_Total.HrefValue = "";
            Override_Total.TooltipValue = "";

            // Variance_Total
            Variance_Total.HrefValue = "";
            Variance_Total.TooltipValue = "";

            // Variance_Percent
            Variance_Percent.HrefValue = "";
            Variance_Percent.TooltipValue = "";

            // Notes
            Notes.HrefValue = "";
            Notes.TooltipValue = "";

            // Next_Approver
            Next_Approver.HrefValue = "";
            Next_Approver.TooltipValue = "";

            // Request_PIC
            Request_PIC.HrefValue = "";
            Request_PIC.TooltipValue = "";

            // Request_Status
            Request_Status.HrefValue = "";
            Request_Status.TooltipValue = "";

            // List_Approver
            List_Approver.HrefValue = "";
            List_Approver.TooltipValue = "";

            // Last_Update_By
            Last_Update_By.HrefValue = "";
            Last_Update_By.TooltipValue = "";

            // Created_By
            Created_By.HrefValue = "";
            Created_By.TooltipValue = "";

            // Created_Date
            Created_Date.HrefValue = "";
            Created_Date.TooltipValue = "";

            // ETL_Date
            ETL_Date.HrefValue = "";
            ETL_Date.TooltipValue = "";

            // Request_Currency
            Request_Currency.HrefValue = "";
            Request_Currency.TooltipValue = "";

            // Call Row Rendered event
            RowRendered();

            // Save data for Custom Template
            Rows.Add(CustomTemplateFieldValues());
        }
        #pragma warning restore 1998

        #pragma warning disable 1998
        // Render edit row values
        public async Task RenderEditRow()
        {
            // Call Row Rendering event
            RowRendering();

            // id
            id.SetupEditAttributes();
            id.EditValue = id.CurrentValue; // DN
            id.PlaceHolder = RemoveHtml(id.Caption);

            // Request_No
            Request_No.SetupEditAttributes();
            if (!Request_No.Raw)
                Request_No.CurrentValue = HtmlDecode(Request_No.CurrentValue);
            Request_No.EditValue = HtmlEncode(Request_No.CurrentValue);
            Request_No.PlaceHolder = RemoveHtml(Request_No.Caption);

            // Reference_Doc
            Reference_Doc.SetupEditAttributes();
            if (!Reference_Doc.Raw)
                Reference_Doc.CurrentValue = HtmlDecode(Reference_Doc.CurrentValue);
            Reference_Doc.EditValue = HtmlEncode(Reference_Doc.CurrentValue);
            Reference_Doc.PlaceHolder = RemoveHtml(Reference_Doc.Caption);

            // Reason
            Reason.SetupEditAttributes();
            curVal = ConvertToString(Reason.CurrentValue)?.Trim() ?? "";
            if (Reason.Lookup != null && IsDictionary(Reason.Lookup?.Options) && Reason.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                Reason.EditValue = Reason.Lookup?.Options.Values.ToList();
            } else { // Lookup from database
                filterWrk = "";
                sqlWrk = Reason.Lookup?.GetSql(true, filterWrk, null, this, false, true);
                rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                Reason.EditValue = rswrk;
            }
            Reason.PlaceHolder = RemoveHtml(Reason.Caption);

            // Request_By

            // Request_By_Name

            // Request_By_Position

            // Request_By_Branch
            Request_By_Branch.SetupEditAttributes();
            if (!Request_By_Branch.Raw)
                Request_By_Branch.CurrentValue = HtmlDecode(Request_By_Branch.CurrentValue);
            Request_By_Branch.EditValue = HtmlEncode(Request_By_Branch.CurrentValue);
            Request_By_Branch.PlaceHolder = RemoveHtml(Request_By_Branch.Caption);

            // Request_By_Region
            Request_By_Region.SetupEditAttributes();
            if (!Request_By_Region.Raw)
                Request_By_Region.CurrentValue = HtmlDecode(Request_By_Region.CurrentValue);
            Request_By_Region.EditValue = HtmlEncode(Request_By_Region.CurrentValue);
            Request_By_Region.PlaceHolder = RemoveHtml(Request_By_Region.Caption);

            // Request_Industry
            Request_Industry.SetupEditAttributes();
            if (!Request_Industry.Raw)
                Request_Industry.CurrentValue = HtmlDecode(Request_Industry.CurrentValue);
            Request_Industry.EditValue = HtmlEncode(Request_Industry.CurrentValue);
            Request_Industry.PlaceHolder = RemoveHtml(Request_Industry.Caption);

            // Customer_ID
            Customer_ID.SetupEditAttributes();
            if (!Customer_ID.Raw)
                Customer_ID.CurrentValue = HtmlDecode(Customer_ID.CurrentValue);
            Customer_ID.EditValue = HtmlEncode(Customer_ID.CurrentValue);
            Customer_ID.PlaceHolder = RemoveHtml(Customer_ID.Caption);

            // Customer_Name
            Customer_Name.SetupEditAttributes();
            if (!Customer_Name.Raw)
                Customer_Name.CurrentValue = HtmlDecode(Customer_Name.CurrentValue);
            Customer_Name.EditValue = HtmlEncode(Customer_Name.CurrentValue);
            Customer_Name.PlaceHolder = RemoveHtml(Customer_Name.Caption);

            // SAP_Total
            SAP_Total.SetupEditAttributes();
            SAP_Total.EditValue = SAP_Total.CurrentValue; // DN
            SAP_Total.PlaceHolder = RemoveHtml(SAP_Total.Caption);
            if (!Empty(SAP_Total.EditValue) && IsNumeric(SAP_Total.EditValue))
                SAP_Total.EditValue = FormatNumber(SAP_Total.EditValue, null);

            // Override_Total
            Override_Total.SetupEditAttributes();
            Override_Total.EditValue = Override_Total.CurrentValue; // DN
            Override_Total.PlaceHolder = RemoveHtml(Override_Total.Caption);
            if (!Empty(Override_Total.EditValue) && IsNumeric(Override_Total.EditValue))
                Override_Total.EditValue = FormatNumber(Override_Total.EditValue, null);

            // Variance_Total
            Variance_Total.SetupEditAttributes();
            Variance_Total.EditValue = Variance_Total.CurrentValue; // DN
            Variance_Total.PlaceHolder = RemoveHtml(Variance_Total.Caption);
            if (!Empty(Variance_Total.EditValue) && IsNumeric(Variance_Total.EditValue))
                Variance_Total.EditValue = FormatNumber(Variance_Total.EditValue, null);

            // Variance_Percent
            Variance_Percent.SetupEditAttributes();
            Variance_Percent.EditValue = Variance_Percent.CurrentValue; // DN
            Variance_Percent.PlaceHolder = RemoveHtml(Variance_Percent.Caption);
            if (!Empty(Variance_Percent.EditValue) && IsNumeric(Variance_Percent.EditValue))
                Variance_Percent.EditValue = FormatNumber(Variance_Percent.EditValue, null);

            // Notes
            Notes.SetupEditAttributes();
            Notes.EditValue = Notes.CurrentValue; // DN
            Notes.PlaceHolder = RemoveHtml(Notes.Caption);

            // Next_Approver
            Next_Approver.SetupEditAttributes();
            if (!Next_Approver.Raw)
                Next_Approver.CurrentValue = HtmlDecode(Next_Approver.CurrentValue);
            Next_Approver.EditValue = HtmlEncode(Next_Approver.CurrentValue);
            Next_Approver.PlaceHolder = RemoveHtml(Next_Approver.Caption);

            // Request_PIC
            Request_PIC.SetupEditAttributes();
            if (!Request_PIC.Raw)
                Request_PIC.CurrentValue = HtmlDecode(Request_PIC.CurrentValue);
            Request_PIC.EditValue = HtmlEncode(Request_PIC.CurrentValue);
            Request_PIC.PlaceHolder = RemoveHtml(Request_PIC.Caption);

            // Request_Status
            Request_Status.SetupEditAttributes();
            Request_Status.EditValue = Request_Status.Options(true);
            Request_Status.PlaceHolder = RemoveHtml(Request_Status.Caption);

            // List_Approver
            List_Approver.SetupEditAttributes();
            if (!List_Approver.Raw)
                List_Approver.CurrentValue = HtmlDecode(List_Approver.CurrentValue);
            List_Approver.EditValue = HtmlEncode(List_Approver.CurrentValue);
            List_Approver.PlaceHolder = RemoveHtml(List_Approver.Caption);

            // Last_Update_By

            // Created_By

            // Created_Date

            // ETL_Date
            ETL_Date.SetupEditAttributes();
            ETL_Date.EditValue = FormatDateTime(ETL_Date.CurrentValue, ETL_Date.FormatPattern); // DN
            ETL_Date.PlaceHolder = RemoveHtml(ETL_Date.Caption);

            // Request_Currency
            Request_Currency.SetupEditAttributes();
            if (!Request_Currency.Raw)
                Request_Currency.CurrentValue = HtmlDecode(Request_Currency.CurrentValue);
            Request_Currency.EditValue = HtmlEncode(Request_Currency.CurrentValue);
            Request_Currency.PlaceHolder = RemoveHtml(Request_Currency.Caption);

            // Call Row Rendered event
            RowRendered();
        }
        #pragma warning restore 1998

        // Aggregate list row values
        public void AggregateListRowValues()
        {
        }

        #pragma warning disable 1998
        // Aggregate list row (for rendering)
        public async Task AggregateListRow()
        {
            // Call Row Rendered event
            RowRendered();
        }
        #pragma warning restore 1998

        // Export data in HTML/CSV/Word/Excel/Email/PDF format
        public async Task ExportDocument(dynamic doc, DbDataReader dataReader, int startRec, int stopRec, string exportType = "")
        {
            if (doc == null)
                return;
            if (dataReader == null)
                return;
            if (!doc.ExportCustom) {
                // Write header
                doc.ExportTableHeader();
                if (doc.Horizontal) { // Horizontal format, write header
                    doc.BeginExportRow();
                    if (exportType == "view") {
                        doc.ExportCaption(Request_No);
                        doc.ExportCaption(Reference_Doc);
                        doc.ExportCaption(Reason);
                        doc.ExportCaption(Request_By);
                        doc.ExportCaption(Request_By_Name);
                        doc.ExportCaption(Request_By_Position);
                        doc.ExportCaption(Request_By_Branch);
                        doc.ExportCaption(Request_By_Region);
                        doc.ExportCaption(Request_Industry);
                        doc.ExportCaption(Customer_ID);
                        doc.ExportCaption(Customer_Name);
                        doc.ExportCaption(SAP_Total);
                        doc.ExportCaption(Override_Total);
                        doc.ExportCaption(Variance_Total);
                        doc.ExportCaption(Variance_Percent);
                        doc.ExportCaption(Notes);
                        doc.ExportCaption(Next_Approver);
                        doc.ExportCaption(Request_PIC);
                        doc.ExportCaption(Request_Status);
                        doc.ExportCaption(List_Approver);
                        doc.ExportCaption(Last_Update_By);
                        doc.ExportCaption(Created_By);
                        doc.ExportCaption(Created_Date);
                        doc.ExportCaption(Request_Currency);
                    } else {
                        doc.ExportCaption(id);
                        doc.ExportCaption(Request_No);
                        doc.ExportCaption(Reference_Doc);
                        doc.ExportCaption(Reason);
                        doc.ExportCaption(Request_By);
                        doc.ExportCaption(Request_By_Name);
                        doc.ExportCaption(Request_By_Position);
                        doc.ExportCaption(Request_By_Branch);
                        doc.ExportCaption(Request_By_Region);
                        doc.ExportCaption(Request_Industry);
                        doc.ExportCaption(Customer_ID);
                        doc.ExportCaption(Customer_Name);
                        doc.ExportCaption(SAP_Total);
                        doc.ExportCaption(Override_Total);
                        doc.ExportCaption(Variance_Total);
                        doc.ExportCaption(Variance_Percent);
                        doc.ExportCaption(Notes);
                        doc.ExportCaption(Next_Approver);
                        doc.ExportCaption(Request_PIC);
                        doc.ExportCaption(Request_Status);
                        doc.ExportCaption(List_Approver);
                        doc.ExportCaption(Last_Update_By);
                        doc.ExportCaption(Created_By);
                        doc.ExportCaption(Created_Date);
                        doc.ExportCaption(Request_Currency);
                    }
                    doc.EndExportRow();
                }
            }
            int recCnt = startRec - 1;
            // Read first record for View Page // DN
            if (exportType == "view") {
                await dataReader.ReadAsync();
            // Move to and read first record for list page // DN
            } else {
                if (Connection.SelectOffset) {
                    await dataReader.ReadAsync();
                } else {
                    for (int i = 0; i < startRec; i++) // Move to the start record and use do-while loop
                        await dataReader.ReadAsync();
                }
            }
            int rowcnt = 0; // DN
            do { // DN
                recCnt++;
                if (recCnt >= startRec) {
                    rowcnt = recCnt - startRec + 1;

                    // Page break
                    if (ExportPageBreakCount > 0) {
                        if (rowcnt > 1 && (rowcnt - 1) % ExportPageBreakCount == 0)
                            doc.ExportPageBreak();
                    }
                    LoadListRowValues(dataReader);

                    // Render row
                    RowType = RowType.View; // Render view
                    ResetAttributes();
                    await RenderListRow();
                    if (!doc.ExportCustom) {
                        doc.BeginExportRow(rowcnt); // Allow CSS styles if enabled
                        if (exportType == "view") {
                            await doc.ExportField(Request_No);
                            await doc.ExportField(Reference_Doc);
                            await doc.ExportField(Reason);
                            await doc.ExportField(Request_By);
                            await doc.ExportField(Request_By_Name);
                            await doc.ExportField(Request_By_Position);
                            await doc.ExportField(Request_By_Branch);
                            await doc.ExportField(Request_By_Region);
                            await doc.ExportField(Request_Industry);
                            await doc.ExportField(Customer_ID);
                            await doc.ExportField(Customer_Name);
                            await doc.ExportField(SAP_Total);
                            await doc.ExportField(Override_Total);
                            await doc.ExportField(Variance_Total);
                            await doc.ExportField(Variance_Percent);
                            await doc.ExportField(Notes);
                            await doc.ExportField(Next_Approver);
                            await doc.ExportField(Request_PIC);
                            await doc.ExportField(Request_Status);
                            await doc.ExportField(List_Approver);
                            await doc.ExportField(Last_Update_By);
                            await doc.ExportField(Created_By);
                            await doc.ExportField(Created_Date);
                            await doc.ExportField(Request_Currency);
                        } else {
                            await doc.ExportField(id);
                            await doc.ExportField(Request_No);
                            await doc.ExportField(Reference_Doc);
                            await doc.ExportField(Reason);
                            await doc.ExportField(Request_By);
                            await doc.ExportField(Request_By_Name);
                            await doc.ExportField(Request_By_Position);
                            await doc.ExportField(Request_By_Branch);
                            await doc.ExportField(Request_By_Region);
                            await doc.ExportField(Request_Industry);
                            await doc.ExportField(Customer_ID);
                            await doc.ExportField(Customer_Name);
                            await doc.ExportField(SAP_Total);
                            await doc.ExportField(Override_Total);
                            await doc.ExportField(Variance_Total);
                            await doc.ExportField(Variance_Percent);
                            await doc.ExportField(Notes);
                            await doc.ExportField(Next_Approver);
                            await doc.ExportField(Request_PIC);
                            await doc.ExportField(Request_Status);
                            await doc.ExportField(List_Approver);
                            await doc.ExportField(Last_Update_By);
                            await doc.ExportField(Created_By);
                            await doc.ExportField(Created_Date);
                            await doc.ExportField(Request_Currency);
                        }
                        doc.EndExportRow(rowcnt);
                    }
                }

                // Call Row Export server event
                if (doc.ExportCustom)
                    RowExport(doc, dataReader);
            } while (recCnt < stopRec && await dataReader.ReadAsync()); // DN
            if (!doc.ExportCustom)
                doc.ExportTableFooter();
        }

        // Table filter
        private string? _tableFilter = null;

        public string TableFilter
        {
            get => _tableFilter ?? "";
            set => _tableFilter = value;
        }

        // TblBasicSearchDefault
        private string? _tableBasicSearchDefault = null;

        public string TableBasicSearchDefault
        {
            get => _tableBasicSearchDefault ?? "";
            set => _tableBasicSearchDefault = value;
        }

        #pragma warning disable 1998
        // Get file data
        public async Task<IActionResult> GetFileData(string fldparm, string[] keys, bool resize, int width = -1, int height = -1)
        {
            if (width < 0)
                width = Config.ThumbnailDefaultWidth;
            if (height < 0)
                height = Config.ThumbnailDefaultHeight;

            // No binary fields
            return JsonBoolResult.FalseResult; // Incorrect key
        }
        #pragma warning restore 1998

        // Table level events

        // Table Load event
        public void TableLoad()
        {
            // Enter your code here
        }

        // Recordset Selecting event
        public void RecordsetSelecting(ref string filter) {
            // Enter your code here
        }

        // Recordset Selected event
        public void RecordsetSelected(DbDataReader rs) {
            // Enter your code here
        }

        // Recordset Searching event
        public void RecordsetSearching(ref string filter) {
            // Enter your code here
        }

        // Recordset Search Validated event
        public void RecordsetSearchValidated() {
            // Enter your code here
        }

        // Row_Selecting event
        public void RowSelecting(ref string filter) {
            // Enter your code here
        }

        // Row Selected event
        public void RowSelected(Dictionary<string, object> row) {
            //Log("Row Selected");
        }

        // Row Inserting event
        public bool RowInserting(Dictionary<string, object>? rsold, Dictionary<string, object> rsnew) {
            // Enter your code here
            // To cancel, set return value to False and error message to CancelMessage
            return true;
        }

        // Row Inserted event
        public void RowInserted(Dictionary<string, object>? rsold, Dictionary<string, object> rsnew) {
            if(rsnew["Request_Status"].ToString()=="Submitted")
            {
                Execute("EXEC [dbo].[SP_override_approval] '" + this.id.CurrentValue.ToString() + "'");
        	}
        }

        // Row Updating event
        public bool RowUpdating(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {
            // Enter your code here
            // To cancel, set return value to False and error message to CancelMessage
            return true;
        }

        // Row Updated event
        public void RowUpdated(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {
            //Log("Row Updated");
        }

        // Row Update Conflict event
        public bool RowUpdateConflict(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {
            // Enter your code here
            // To ignore conflict, set return value to false
            return true;
        }

        // Recordset Deleting event
        public bool RowDeleting(Dictionary<string, object> rs) {
            // Enter your code here
            // To cancel, set return value to False and error message to CancelMessage
            return true;
        }

        // Row Deleted event
        public void RowDeleted(Dictionary<string, object> rs) {
            //Log("Row Deleted");
        }

        // Row Export event
        // doc = export document object
        public virtual void RowExport(dynamic doc, DbDataReader rs) {
            //doc.Text.Append("<div>" + MyField.ViewValue +"</div>"); // Build HTML with field value: rs["MyField"] or MyField.ViewValue
        }

        // Email Sending event
        public virtual bool EmailSending(Email email, dynamic? args) {
            //Log(email);
            return true;
        }

        // Lookup Selecting event
        public void LookupSelecting(DbField fld, ref string filter) {
            // Enter your code here
        }

        // Row Rendering event
        public void RowRendering() {
            // Enter your code here
        }

        // Row Rendered event
        public void RowRendered() {
            //VarDump(<FieldName>); // View field properties
        }

        // User ID Filtering event
        public void UserIdFiltering(ref string filter) {
            // Enter your code here
        }
    }
} // End Partial class
