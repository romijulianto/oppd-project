namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// trRequestDetail
    /// </summary>
    [MaybeNull]
    public static TrRequestDetail trRequestDetail
    {
        get => HttpData.Resolve<TrRequestDetail>("tr_request_detail");
        set => HttpData["tr_request_detail"] = value;
    }

    /// <summary>
    /// Table class for tr_request_detail
    /// </summary>
    public class TrRequestDetail : DbTable, IQueryFactory
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

        public bool ModalEdit = true;

        public bool ModalUpdate = true;

        public bool InlineDelete = false;

        public bool ModalGridAdd = false;

        public bool ModalGridEdit = false;

        public bool ModalMultiEdit = false;

        public readonly DbField<SqlDbType> id;

        public readonly DbField<SqlDbType> request_id;

        public readonly DbField<SqlDbType> Item_No;

        public readonly DbField<SqlDbType> Part_No;

        public readonly DbField<SqlDbType> Part_Description;

        public readonly DbField<SqlDbType> Qty;

        public readonly DbField<SqlDbType> SAP_Unit_Price;

        public readonly DbField<SqlDbType> Extd_SAP_Price;

        public readonly DbField<SqlDbType> GP_SAP_Price;

        public readonly DbField<SqlDbType> Override_Unit_Price;

        public readonly DbField<SqlDbType> Extd_Override_Price;

        public readonly DbField<SqlDbType> GP_Override_Price;

        public readonly DbField<SqlDbType> Override_Core;

        public readonly DbField<SqlDbType> Override_Percent;

        public readonly DbField<SqlDbType> Core_Life_Ind;

        public readonly DbField<SqlDbType> Notes;

        // Constructor
        public TrRequestDetail()
        {
            // Language object // DN
            Language = ResolveLanguage();
            TableVar = "tr_request_detail";
            Name = "tr_request_detail";
            Type = "TABLE";
            UpdateTable = "dbo.tr_request_detail"; // Update Table
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
            ShowMultipleDetails = false; // Show multiple details
            GridAddRowCount = 5;
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
                HtmlTag = "HIDDEN",
                InputTextType = "text",
                IsPrimaryKey = true, // Primary key field
                Nullable = false, // NOT NULL field
                Sortable = false, // Allow sort
                DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
                SearchOperators = new () { "=", "<>" },
                CustomMessage = Language.FieldPhrase("tr_request_detail", "id", "CustomMsg"),
                IsUpload = false
            };
            Fields.Add("id", id);

            // request_id
            request_id = new (this, "x_request_id", 200, SqlDbType.VarChar) {
                Name = "request_id",
                Expression = "[request_id]",
                BasicSearchExpression = "[request_id]",
                DateTimeFormat = -1,
                VirtualExpression = "[request_id]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "FILE",
                InputTextType = "text",
                IsForeignKey = true, // Foreign key field
                Sortable = false, // Allow sort
                SearchOperators = new () { "=", "<>", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request_detail", "request_id", "CustomMsg"),
                IsUpload = true
            };
            Fields.Add("request_id", request_id);

            // Item_No
            Item_No = new (this, "x_Item_No", 202, SqlDbType.NVarChar) {
                Name = "Item_No",
                Expression = "[Item_No]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Item_No]",
                DateTimeFormat = -1,
                VirtualExpression = "[Item_No]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request_detail", "Item_No", "CustomMsg"),
                IsUpload = false
            };
            Item_No.Lookup = new Lookup<DbField>(Item_No, "tr_request_detail", true, "Item_No", new List<string> {"Item_No", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Item_No", Item_No);

            // Part_No
            Part_No = new (this, "x_Part_No", 202, SqlDbType.NVarChar) {
                Name = "Part_No",
                Expression = "[Part_No]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Part_No]",
                DateTimeFormat = -1,
                VirtualExpression = "[Part_No]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request_detail", "Part_No", "CustomMsg"),
                IsUpload = false
            };
            Part_No.Lookup = new Lookup<DbField>(Part_No, "tr_request_detail", true, "Part_No", new List<string> {"Part_No", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Part_No", Part_No);

            // Part_Description
            Part_Description = new (this, "x_Part_Description", 202, SqlDbType.NVarChar) {
                Name = "Part_Description",
                Expression = "[Part_Description]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Part_Description]",
                DateTimeFormat = -1,
                VirtualExpression = "[Part_Description]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request_detail", "Part_Description", "CustomMsg"),
                IsUpload = false
            };
            Part_Description.Lookup = new Lookup<DbField>(Part_Description, "tr_request_detail", true, "Part_Description", new List<string> {"Part_Description", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Part_Description", Part_Description);

            // Qty
            Qty = new (this, "x_Qty", 3, SqlDbType.Int) {
                Name = "Qty",
                Expression = "[Qty]",
                BasicSearchExpression = "CAST([Qty] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[Qty]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("tr_request_detail", "Qty", "CustomMsg"),
                IsUpload = false
            };
            Qty.Lookup = new Lookup<DbField>(Qty, "tr_request_detail", true, "Qty", new List<string> {"Qty", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Qty", Qty);

            // SAP_Unit_Price
            SAP_Unit_Price = new (this, "x_SAP_Unit_Price", 5, SqlDbType.Float) {
                Name = "SAP_Unit_Price",
                Expression = "[SAP_Unit_Price]",
                BasicSearchExpression = "CAST([SAP_Unit_Price] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[SAP_Unit_Price]",
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
                CustomMessage = Language.FieldPhrase("tr_request_detail", "SAP_Unit_Price", "CustomMsg"),
                IsUpload = false
            };
            SAP_Unit_Price.Lookup = new Lookup<DbField>(SAP_Unit_Price, "tr_request_detail", true, "SAP_Unit_Price", new List<string> {"SAP_Unit_Price", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("SAP_Unit_Price", SAP_Unit_Price);

            // Extd_SAP_Price
            Extd_SAP_Price = new (this, "x_Extd_SAP_Price", 5, SqlDbType.Float) {
                Name = "Extd_SAP_Price",
                Expression = "[Extd_SAP_Price]",
                BasicSearchExpression = "CAST([Extd_SAP_Price] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[Extd_SAP_Price]",
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
                CustomMessage = Language.FieldPhrase("tr_request_detail", "Extd_SAP_Price", "CustomMsg"),
                IsUpload = false
            };
            Extd_SAP_Price.Lookup = new Lookup<DbField>(Extd_SAP_Price, "tr_request_detail", true, "Extd_SAP_Price", new List<string> {"Extd_SAP_Price", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Extd_SAP_Price", Extd_SAP_Price);

            // GP_SAP_Price
            GP_SAP_Price = new (this, "x_GP_SAP_Price", 5, SqlDbType.Float) {
                Name = "GP_SAP_Price",
                Expression = "[GP_SAP_Price]",
                BasicSearchExpression = "CAST([GP_SAP_Price] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[GP_SAP_Price]",
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
                CustomMessage = Language.FieldPhrase("tr_request_detail", "GP_SAP_Price", "CustomMsg"),
                IsUpload = false
            };
            GP_SAP_Price.Lookup = new Lookup<DbField>(GP_SAP_Price, "tr_request_detail", true, "GP_SAP_Price", new List<string> {"GP_SAP_Price", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("GP_SAP_Price", GP_SAP_Price);

            // Override_Unit_Price
            Override_Unit_Price = new (this, "x_Override_Unit_Price", 5, SqlDbType.Float) {
                Name = "Override_Unit_Price",
                Expression = "[Override_Unit_Price]",
                BasicSearchExpression = "CAST([Override_Unit_Price] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[Override_Unit_Price]",
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
                CustomMessage = Language.FieldPhrase("tr_request_detail", "Override_Unit_Price", "CustomMsg"),
                IsUpload = false
            };
            Override_Unit_Price.Lookup = new Lookup<DbField>(Override_Unit_Price, "tr_request_detail", true, "Override_Unit_Price", new List<string> {"Override_Unit_Price", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Override_Unit_Price", Override_Unit_Price);

            // Extd_Override_Price
            Extd_Override_Price = new (this, "x_Extd_Override_Price", 5, SqlDbType.Float) {
                Name = "Extd_Override_Price",
                Expression = "[Extd_Override_Price]",
                BasicSearchExpression = "CAST([Extd_Override_Price] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[Extd_Override_Price]",
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
                CustomMessage = Language.FieldPhrase("tr_request_detail", "Extd_Override_Price", "CustomMsg"),
                IsUpload = false
            };
            Extd_Override_Price.Lookup = new Lookup<DbField>(Extd_Override_Price, "tr_request_detail", true, "Extd_Override_Price", new List<string> {"Extd_Override_Price", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Extd_Override_Price", Extd_Override_Price);

            // GP_Override_Price
            GP_Override_Price = new (this, "x_GP_Override_Price", 5, SqlDbType.Float) {
                Name = "GP_Override_Price",
                Expression = "[GP_Override_Price]",
                BasicSearchExpression = "CAST([GP_Override_Price] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[GP_Override_Price]",
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
                CustomMessage = Language.FieldPhrase("tr_request_detail", "GP_Override_Price", "CustomMsg"),
                IsUpload = false
            };
            GP_Override_Price.Lookup = new Lookup<DbField>(GP_Override_Price, "tr_request_detail", true, "GP_Override_Price", new List<string> {"GP_Override_Price", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("GP_Override_Price", GP_Override_Price);

            // Override_Core
            Override_Core = new (this, "x_Override_Core", 5, SqlDbType.Float) {
                Name = "Override_Core",
                Expression = "[Override_Core]",
                BasicSearchExpression = "CAST([Override_Core] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[Override_Core]",
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
                CustomMessage = Language.FieldPhrase("tr_request_detail", "Override_Core", "CustomMsg"),
                IsUpload = false
            };
            Override_Core.Lookup = new Lookup<DbField>(Override_Core, "tr_request_detail", true, "Override_Core", new List<string> {"Override_Core", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Override_Core", Override_Core);

            // Override_Percent
            Override_Percent = new (this, "x_Override_Percent", 5, SqlDbType.Float) {
                Name = "Override_Percent",
                Expression = "[Override_Percent]",
                BasicSearchExpression = "CAST([Override_Percent] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[Override_Percent]",
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
                CustomMessage = Language.FieldPhrase("tr_request_detail", "Override_Percent", "CustomMsg"),
                IsUpload = false
            };
            Override_Percent.Lookup = new Lookup<DbField>(Override_Percent, "tr_request_detail", true, "Override_Percent", new List<string> {"Override_Percent", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Override_Percent", Override_Percent);

            // Core_Life_Ind
            Core_Life_Ind = new (this, "x_Core_Life_Ind", 5, SqlDbType.Float) {
                Name = "Core_Life_Ind",
                Expression = "[Core_Life_Ind]",
                BasicSearchExpression = "CAST([Core_Life_Ind] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[Core_Life_Ind]",
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
                CustomMessage = Language.FieldPhrase("tr_request_detail", "Core_Life_Ind", "CustomMsg"),
                IsUpload = false
            };
            Core_Life_Ind.Lookup = new Lookup<DbField>(Core_Life_Ind, "tr_request_detail", true, "Core_Life_Ind", new List<string> {"Core_Life_Ind", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Core_Life_Ind", Core_Life_Ind);

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
                CustomMessage = Language.FieldPhrase("tr_request_detail", "Notes", "CustomMsg"),
                IsUpload = false
            };
            Notes.Lookup = new Lookup<DbField>(Notes, "tr_request_detail", true, "Notes", new List<string> {"Notes", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "");
            Fields.Add("Notes", Notes);

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

        // Current master table name
        public string CurrentMasterTable
        {
            get => Session.GetString(Config.ProjectName + "_" + TableVar + "_" + Config.TableMasterTable);
            set => Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableMasterTable] = value;
        }

        // Session master where clause
        public string MasterFilterFromSession
        {
            get { // Master filter
                string masterFilter = "";
            if (CurrentMasterTable == "v_tr_request") {
                dynamic masterTable = Resolve("v_tr_request")!;
                if (!Empty(request_id.SessionValue))
                    masterFilter += "" + KeyFilter(masterTable.id, request_id.SessionValue, masterTable.id.DataType, masterTable.DbId);
                else
                    return "";
            }
            if (CurrentMasterTable == "tr_request") {
                dynamic masterTable = Resolve("tr_request")!;
                if (!Empty(request_id.SessionValue))
                    masterFilter += "" + KeyFilter(masterTable.id, request_id.SessionValue, masterTable.id.DataType, masterTable.DbId);
                else
                    return "";
            }
                return masterFilter;
            }
        }

        // Session detail WHERE clause
        public string DetailFilterFromSession
        {
            get { // Detail filter
                string detailFilter = "";
                if (CurrentMasterTable == "v_tr_request") {
                    dynamic masterTable = Resolve("v_tr_request")!;
                    if (!Empty(request_id.SessionValue))
                        detailFilter += "" + KeyFilter(request_id, request_id.SessionValue, masterTable.id.DataType, DbId);
                    else
                        return "";
                }
                if (CurrentMasterTable == "tr_request") {
                    dynamic masterTable = Resolve("tr_request")!;
                    if (!Empty(request_id.SessionValue))
                        detailFilter += "" + KeyFilter(request_id, request_id.SessionValue, masterTable.id.DataType, DbId);
                    else
                        return "";
                }
                return detailFilter;
            }
        }

        // Master filter // DN
        public string? MasterFilter(dynamic? masterTable, Dictionary<string, object?> keys) // DN
        {
            bool validKeys = true;
            object key = "";
            switch (masterTable?.TableVar) {
            case "v_tr_request":
                key = keys["request_id"] ?? "";
                if (Empty(key)) {
                    if (masterTable.id.Required) // Required field and empty value
                        return ""; // Return empty filter
                    validKeys = false;
                } else if (!validKeys) { // Already has empty key
                    return ""; // Return empty filter
                }
                if (validKeys) {
                    return KeyFilter(masterTable.id, keys["request_id"], request_id.DataType, DbId);
                }
                break;
            case "tr_request":
                key = keys["request_id"] ?? "";
                if (Empty(key)) {
                    if (masterTable.id.Required) // Required field and empty value
                        return ""; // Return empty filter
                    validKeys = false;
                } else if (!validKeys) { // Already has empty key
                    return ""; // Return empty filter
                }
                if (validKeys) {
                    return KeyFilter(masterTable.id, keys["request_id"], request_id.DataType, DbId);
                }
                break;
            }
            return null; // All null values and no required fields
        }

        // Detail filter // DN
        public string DetailFilter(dynamic masterTable) // DN
        {
            return masterTable.TableVar switch {
                "v_tr_request" => KeyFilter(request_id, masterTable.id.DbValue, masterTable.id.DataType, masterTable.DbId),
                "tr_request" => KeyFilter(request_id, masterTable.id.DbValue, masterTable.id.DataType, masterTable.DbId),
                _ => ""
            };
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
            get => _sqlFrom ?? "dbo.tr_request_detail";
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
            Dictionary<string, object> rscascade = new ();
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
                request_id.Upload.DbValue = row["request_id"];
                Item_No.DbValue = row["Item_No"]; // Set DB value only
                Part_No.DbValue = row["Part_No"]; // Set DB value only
                Part_Description.DbValue = row["Part_Description"]; // Set DB value only
                Qty.DbValue = row["Qty"]; // Set DB value only
                SAP_Unit_Price.DbValue = row["SAP_Unit_Price"]; // Set DB value only
                Extd_SAP_Price.DbValue = row["Extd_SAP_Price"]; // Set DB value only
                GP_SAP_Price.DbValue = row["GP_SAP_Price"]; // Set DB value only
                Override_Unit_Price.DbValue = row["Override_Unit_Price"]; // Set DB value only
                Extd_Override_Price.DbValue = row["Extd_Override_Price"]; // Set DB value only
                GP_Override_Price.DbValue = row["GP_Override_Price"]; // Set DB value only
                Override_Core.DbValue = row["Override_Core"]; // Set DB value only
                Override_Percent.DbValue = row["Override_Percent"]; // Set DB value only
                Core_Life_Ind.DbValue = row["Core_Life_Ind"]; // Set DB value only
                Notes.DbValue = row["Notes"]; // Set DB value only
            } catch {}
        }

        public void DeleteUploadedFiles(Dictionary<string, object> row)
        {
            LoadDbValues(row);
            if (!Empty(row["request_id"])) {
                DeleteFile(request_id.OldPhysicalUploadPath + row["request_id"]);
            }
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
                    return "TrRequestDetailList";
                }
            }
            set {
                Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl] = value;
            }
        }

        // Get modal caption
        public string GetModalCaption(string pageName)
        {
            if (SameString(pageName, "TrRequestDetailView"))
                return Language.Phrase("View");
            else if (SameString(pageName, "TrRequestDetailEdit"))
                return Language.Phrase("Edit");
            else if (SameString(pageName, "TrRequestDetailAdd"))
                return Language.Phrase("Add");
            else
                return "";
        }

        // Default route URL
        public string DefaultRouteUrl
        {
            get {
                return "TrRequestDetailList";
            }
        }

        // API page name
        public string GetApiPageName(string action)
        {
            return action.ToLowerInvariant() switch {
                Config.ApiViewAction => "TrRequestDetailView",
                Config.ApiAddAction => "TrRequestDetailAdd",
                Config.ApiEditAction => "TrRequestDetailEdit",
                Config.ApiDeleteAction => "TrRequestDetailDelete",
                Config.ApiListAction => "TrRequestDetailList",
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
        public string ListUrl => "TrRequestDetailList";

        // View URL
        public string ViewUrl => GetViewUrl();

        // View URL
        public string GetViewUrl(string parm = "")
        {
            string url = "";
            if (!Empty(parm))
                url = KeyUrl("TrRequestDetailView", parm);
            else
                url = KeyUrl("TrRequestDetailView", Config.TableShowDetail + "=");
            return AddMasterUrl(url);
        }

        // Add URL
        public string AddUrl { get; set; } = "TrRequestDetailAdd";

        // Add URL
        public string GetAddUrl(string parm = "")
        {
            string url = "";
            if (!Empty(parm))
                url = "TrRequestDetailAdd?" + parm;
            else
                url = "TrRequestDetailAdd";
            return AppPath(AddMasterUrl(url));
        }

        // Edit URL
        public string EditUrl => GetEditUrl();

        // Edit URL (with parameter)
        public string GetEditUrl(string parm = "")
        {
            string url = "";
            url = KeyUrl("TrRequestDetailEdit", parm);
            return AppPath(AddMasterUrl(url)); // DN
        }

        // Inline edit URL
        public string InlineEditUrl =>
            AppPath(AddMasterUrl(KeyUrl("TrRequestDetailList", "action=edit"))); // DN

        // Copy URL
        public string CopyUrl => GetCopyUrl();

        // Copy URL
        public string GetCopyUrl(string parm = "")
        {
            string url = "";
            url = KeyUrl("TrRequestDetailAdd", parm);
            return AppPath(AddMasterUrl(url)); // DN
        }

        // Inline copy URL
        public string InlineCopyUrl =>
            AppPath(AddMasterUrl(KeyUrl("TrRequestDetailList", "action=copy"))); // DN

        // Delete URL
        public string DeleteUrl => UseAjaxActions && Param<bool>("infinitescroll") && CurrentPageID() == "list"
            ? AppPath(KeyUrl(Config.ApiUrl + Config.ApiDeleteAction + "/" + TableVar))
            : AppPath(KeyUrl("TrRequestDetailDelete")); // DN

        // Add master URL
        public string AddMasterUrl(string url)
        {
            if (CurrentMasterTable == "v_tr_request" && !url.Contains(Config.TableShowMaster + "=")) {
                url += (url.Contains("?") ? "&" : "?") + Config.TableShowMaster + "=" + CurrentMasterTable;
                url += "&" + ForeignKeyUrl("fk_id", request_id.SessionValue); // Use Session Value
            }
            if (CurrentMasterTable == "tr_request" && !url.Contains(Config.TableShowMaster + "=")) {
                url += (url.Contains("?") ? "&" : "?") + Config.TableShowMaster + "=" + CurrentMasterTable;
                url += "&" + ForeignKeyUrl("fk_id", request_id.SessionValue); // Use Session Value
            }
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
            request_id.Upload.DbValue = dr["request_id"];
            request_id.SetDbValue(request_id.Upload.DbValue);
            Item_No.SetDbValue(dr["Item_No"]);
            Part_No.SetDbValue(dr["Part_No"]);
            Part_Description.SetDbValue(dr["Part_Description"]);
            Qty.SetDbValue(dr["Qty"]);
            SAP_Unit_Price.SetDbValue(dr["SAP_Unit_Price"]);
            Extd_SAP_Price.SetDbValue(dr["Extd_SAP_Price"]);
            GP_SAP_Price.SetDbValue(dr["GP_SAP_Price"]);
            Override_Unit_Price.SetDbValue(dr["Override_Unit_Price"]);
            Extd_Override_Price.SetDbValue(dr["Extd_Override_Price"]);
            GP_Override_Price.SetDbValue(dr["GP_Override_Price"]);
            Override_Core.SetDbValue(dr["Override_Core"]);
            Override_Percent.SetDbValue(dr["Override_Percent"]);
            Core_Life_Ind.SetDbValue(dr["Core_Life_Ind"]);
            Notes.SetDbValue(dr["Notes"]);
        }

        // Render list content
        public async Task<string> RenderListContent(string filter)
        {
            string pageName = "TrRequestDetailList";
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

            // request_id
            request_id.CellCssStyle = "white-space: nowrap;";

            // Item_No
            Item_No.CellCssStyle = "white-space: nowrap;";

            // Part_No
            Part_No.CellCssStyle = "white-space: nowrap;";

            // Part_Description
            Part_Description.CellCssStyle = "white-space: nowrap;";

            // Qty
            Qty.CellCssStyle = "white-space: nowrap;";

            // SAP_Unit_Price
            SAP_Unit_Price.CellCssStyle = "white-space: nowrap;";

            // Extd_SAP_Price
            Extd_SAP_Price.CellCssStyle = "white-space: nowrap;";

            // GP_SAP_Price
            GP_SAP_Price.CellCssStyle = "white-space: nowrap;";

            // Override_Unit_Price
            Override_Unit_Price.CellCssStyle = "white-space: nowrap;";

            // Extd_Override_Price
            Extd_Override_Price.CellCssStyle = "white-space: nowrap;";

            // GP_Override_Price
            GP_Override_Price.CellCssStyle = "white-space: nowrap;";

            // Override_Core
            Override_Core.CellCssStyle = "white-space: nowrap;";

            // Override_Percent
            Override_Percent.CellCssStyle = "white-space: nowrap;";

            // Core_Life_Ind
            Core_Life_Ind.CellCssStyle = "white-space: nowrap;";

            // Notes
            Notes.CellCssStyle = "white-space: nowrap;";

            // id
            id.ViewValue = id.CurrentValue;
            id.ViewValue = FormatNumber(id.ViewValue, id.FormatPattern);
            id.ViewCustomAttributes = "";

            // request_id
            if (!IsNull(request_id.Upload.DbValue)) {
                request_id.ViewValue = request_id.Upload.DbValue;
            } else {
                request_id.ViewValue = "";
            }
            request_id.ViewCustomAttributes = "";

            // Item_No
            Item_No.ViewValue = ConvertToString(Item_No.CurrentValue); // DN
            Item_No.ViewCustomAttributes = "";

            // Part_No
            Part_No.ViewValue = ConvertToString(Part_No.CurrentValue); // DN
            Part_No.ViewCustomAttributes = "";

            // Part_Description
            Part_Description.ViewValue = ConvertToString(Part_Description.CurrentValue); // DN
            Part_Description.ViewCustomAttributes = "";

            // Qty
            Qty.ViewValue = Qty.CurrentValue;
            Qty.ViewValue = FormatNumber(Qty.ViewValue, Qty.FormatPattern);
            Qty.ViewCustomAttributes = "";

            // SAP_Unit_Price
            SAP_Unit_Price.ViewValue = SAP_Unit_Price.CurrentValue;
            SAP_Unit_Price.ViewValue = FormatNumber(SAP_Unit_Price.ViewValue, SAP_Unit_Price.FormatPattern);
            SAP_Unit_Price.CellCssStyle += "text-align: right;";
            SAP_Unit_Price.ViewCustomAttributes = "";

            // Extd_SAP_Price
            Extd_SAP_Price.ViewValue = Extd_SAP_Price.CurrentValue;
            Extd_SAP_Price.ViewValue = FormatNumber(Extd_SAP_Price.ViewValue, Extd_SAP_Price.FormatPattern);
            Extd_SAP_Price.CellCssStyle += "text-align: right;";
            Extd_SAP_Price.ViewCustomAttributes = "";

            // GP_SAP_Price
            GP_SAP_Price.ViewValue = GP_SAP_Price.CurrentValue;
            GP_SAP_Price.ViewValue = FormatNumber(GP_SAP_Price.ViewValue, GP_SAP_Price.FormatPattern);
            GP_SAP_Price.CellCssStyle += "text-align: right;";
            GP_SAP_Price.ViewCustomAttributes = "";

            // Override_Unit_Price
            Override_Unit_Price.ViewValue = Override_Unit_Price.CurrentValue;
            Override_Unit_Price.ViewValue = FormatNumber(Override_Unit_Price.ViewValue, Override_Unit_Price.FormatPattern);
            Override_Unit_Price.CellCssStyle += "text-align: right;";
            Override_Unit_Price.ViewCustomAttributes = "";

            // Extd_Override_Price
            Extd_Override_Price.ViewValue = Extd_Override_Price.CurrentValue;
            Extd_Override_Price.ViewValue = FormatNumber(Extd_Override_Price.ViewValue, Extd_Override_Price.FormatPattern);
            Extd_Override_Price.CellCssStyle += "text-align: right;";
            Extd_Override_Price.ViewCustomAttributes = "";

            // GP_Override_Price
            GP_Override_Price.ViewValue = GP_Override_Price.CurrentValue;
            GP_Override_Price.ViewValue = FormatNumber(GP_Override_Price.ViewValue, GP_Override_Price.FormatPattern);
            GP_Override_Price.CellCssStyle += "text-align: right;";
            GP_Override_Price.ViewCustomAttributes = "";

            // Override_Core
            Override_Core.ViewValue = Override_Core.CurrentValue;
            Override_Core.ViewValue = FormatNumber(Override_Core.ViewValue, Override_Core.FormatPattern);
            Override_Core.CellCssStyle += "text-align: right;";
            Override_Core.ViewCustomAttributes = "";

            // Override_Percent
            Override_Percent.ViewValue = Override_Percent.CurrentValue;
            Override_Percent.ViewValue = FormatNumber(Override_Percent.ViewValue, Override_Percent.FormatPattern);
            Override_Percent.CellCssStyle += "text-align: right;";
            Override_Percent.ViewCustomAttributes = "";

            // Core_Life_Ind
            Core_Life_Ind.ViewValue = Core_Life_Ind.CurrentValue;
            Core_Life_Ind.ViewValue = FormatNumber(Core_Life_Ind.ViewValue, Core_Life_Ind.FormatPattern);
            Core_Life_Ind.CellCssStyle += "text-align: right;";
            Core_Life_Ind.ViewCustomAttributes = "";

            // Notes
            Notes.ViewValue = Notes.CurrentValue;
            Notes.ViewCustomAttributes = "";

            // id
            id.HrefValue = "";
            id.TooltipValue = "";

            // request_id
            request_id.HrefValue = "";
            request_id.ExportHrefValue = request_id.UploadPath + request_id.Upload.DbValue;
            request_id.TooltipValue = "";

            // Item_No
            Item_No.HrefValue = "";
            Item_No.TooltipValue = "";

            // Part_No
            Part_No.HrefValue = "";
            Part_No.TooltipValue = "";

            // Part_Description
            Part_Description.HrefValue = "";
            Part_Description.TooltipValue = "";

            // Qty
            Qty.HrefValue = "";
            Qty.TooltipValue = "";

            // SAP_Unit_Price
            SAP_Unit_Price.HrefValue = "";
            SAP_Unit_Price.TooltipValue = "";

            // Extd_SAP_Price
            Extd_SAP_Price.HrefValue = "";
            Extd_SAP_Price.TooltipValue = "";

            // GP_SAP_Price
            GP_SAP_Price.HrefValue = "";
            GP_SAP_Price.TooltipValue = "";

            // Override_Unit_Price
            Override_Unit_Price.HrefValue = "";
            Override_Unit_Price.TooltipValue = "";

            // Extd_Override_Price
            Extd_Override_Price.HrefValue = "";
            Extd_Override_Price.TooltipValue = "";

            // GP_Override_Price
            GP_Override_Price.HrefValue = "";
            GP_Override_Price.TooltipValue = "";

            // Override_Core
            Override_Core.HrefValue = "";
            Override_Core.TooltipValue = "";

            // Override_Percent
            Override_Percent.HrefValue = "";
            Override_Percent.TooltipValue = "";

            // Core_Life_Ind
            Core_Life_Ind.HrefValue = "";
            Core_Life_Ind.TooltipValue = "";

            // Notes
            Notes.HrefValue = "";
            Notes.TooltipValue = "";

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
            id.CurrentValue = FormatNumber(id.CurrentValue, id.FormatPattern);
            if (!Empty(id.EditValue) && IsNumeric(id.EditValue))
                id.EditValue = FormatNumber(id.EditValue, null);

            // request_id
            request_id.SetupEditAttributes();
            if (!IsNull(request_id.Upload.DbValue)) {
                request_id.EditValue = request_id.Upload.DbValue;
            } else {
                request_id.EditValue = "";
            }
            request_id.ViewCustomAttributes = "";

            // Item_No
            Item_No.SetupEditAttributes();
            Item_No.EditValue = ConvertToString(Item_No.CurrentValue); // DN
            Item_No.ViewCustomAttributes = "";

            // Part_No
            Part_No.SetupEditAttributes();
            Part_No.EditValue = ConvertToString(Part_No.CurrentValue); // DN
            Part_No.ViewCustomAttributes = "";

            // Part_Description
            Part_Description.SetupEditAttributes();
            Part_Description.EditValue = ConvertToString(Part_Description.CurrentValue); // DN
            Part_Description.ViewCustomAttributes = "";

            // Qty
            Qty.SetupEditAttributes();
            Qty.EditValue = Qty.CurrentValue;
            Qty.EditValue = FormatNumber(Qty.EditValue, Qty.FormatPattern);
            Qty.ViewCustomAttributes = "";

            // SAP_Unit_Price
            SAP_Unit_Price.SetupEditAttributes();
            SAP_Unit_Price.EditValue = SAP_Unit_Price.CurrentValue;
            SAP_Unit_Price.EditValue = FormatNumber(SAP_Unit_Price.EditValue, SAP_Unit_Price.FormatPattern);
            SAP_Unit_Price.CellCssStyle += "text-align: right;";
            SAP_Unit_Price.ViewCustomAttributes = "";

            // Extd_SAP_Price
            Extd_SAP_Price.SetupEditAttributes();
            Extd_SAP_Price.EditValue = Extd_SAP_Price.CurrentValue;
            Extd_SAP_Price.EditValue = FormatNumber(Extd_SAP_Price.EditValue, Extd_SAP_Price.FormatPattern);
            Extd_SAP_Price.CellCssStyle += "text-align: right;";
            Extd_SAP_Price.ViewCustomAttributes = "";

            // GP_SAP_Price
            GP_SAP_Price.SetupEditAttributes();
            GP_SAP_Price.EditValue = GP_SAP_Price.CurrentValue;
            GP_SAP_Price.EditValue = FormatNumber(GP_SAP_Price.EditValue, GP_SAP_Price.FormatPattern);
            GP_SAP_Price.CellCssStyle += "text-align: right;";
            GP_SAP_Price.ViewCustomAttributes = "";

            // Override_Unit_Price
            Override_Unit_Price.SetupEditAttributes();
            Override_Unit_Price.EditValue = Override_Unit_Price.CurrentValue; // DN
            Override_Unit_Price.PlaceHolder = RemoveHtml(Override_Unit_Price.Caption);
            if (!Empty(Override_Unit_Price.EditValue) && IsNumeric(Override_Unit_Price.EditValue))
                Override_Unit_Price.EditValue = FormatNumber(Override_Unit_Price.EditValue, null);

            // Extd_Override_Price
            Extd_Override_Price.SetupEditAttributes();
            Extd_Override_Price.EditValue = Extd_Override_Price.CurrentValue;
            Extd_Override_Price.EditValue = FormatNumber(Extd_Override_Price.EditValue, Extd_Override_Price.FormatPattern);
            Extd_Override_Price.CellCssStyle += "text-align: right;";
            Extd_Override_Price.ViewCustomAttributes = "";

            // GP_Override_Price
            GP_Override_Price.SetupEditAttributes();
            GP_Override_Price.EditValue = GP_Override_Price.CurrentValue;
            GP_Override_Price.EditValue = FormatNumber(GP_Override_Price.EditValue, GP_Override_Price.FormatPattern);
            GP_Override_Price.CellCssStyle += "text-align: right;";
            GP_Override_Price.ViewCustomAttributes = "";

            // Override_Core
            Override_Core.SetupEditAttributes();
            Override_Core.EditValue = Override_Core.CurrentValue;
            Override_Core.EditValue = FormatNumber(Override_Core.EditValue, Override_Core.FormatPattern);
            Override_Core.CellCssStyle += "text-align: right;";
            Override_Core.ViewCustomAttributes = "";

            // Override_Percent
            Override_Percent.SetupEditAttributes();
            Override_Percent.EditValue = Override_Percent.CurrentValue;
            Override_Percent.EditValue = FormatNumber(Override_Percent.EditValue, Override_Percent.FormatPattern);
            Override_Percent.CellCssStyle += "text-align: right;";
            Override_Percent.ViewCustomAttributes = "";

            // Core_Life_Ind
            Core_Life_Ind.SetupEditAttributes();
            Core_Life_Ind.EditValue = Core_Life_Ind.CurrentValue;
            Core_Life_Ind.EditValue = FormatNumber(Core_Life_Ind.EditValue, Core_Life_Ind.FormatPattern);
            Core_Life_Ind.CellCssStyle += "text-align: right;";
            Core_Life_Ind.ViewCustomAttributes = "";

            // Notes
            Notes.SetupEditAttributes();
            Notes.EditValue = Notes.CurrentValue;
            Notes.ViewCustomAttributes = "";

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
                        doc.ExportCaption(id);
                        doc.ExportCaption(request_id);
                        doc.ExportCaption(Item_No);
                        doc.ExportCaption(Part_No);
                        doc.ExportCaption(Part_Description);
                        doc.ExportCaption(Qty);
                        doc.ExportCaption(SAP_Unit_Price);
                        doc.ExportCaption(Extd_SAP_Price);
                        doc.ExportCaption(GP_SAP_Price);
                        doc.ExportCaption(Override_Unit_Price);
                        doc.ExportCaption(Extd_Override_Price);
                        doc.ExportCaption(GP_Override_Price);
                        doc.ExportCaption(Override_Core);
                        doc.ExportCaption(Override_Percent);
                        doc.ExportCaption(Core_Life_Ind);
                        doc.ExportCaption(Notes);
                    } else {
                        doc.ExportCaption(Item_No);
                        doc.ExportCaption(Part_No);
                        doc.ExportCaption(Part_Description);
                        doc.ExportCaption(Qty);
                        doc.ExportCaption(SAP_Unit_Price);
                        doc.ExportCaption(Extd_SAP_Price);
                        doc.ExportCaption(GP_SAP_Price);
                        doc.ExportCaption(Override_Unit_Price);
                        doc.ExportCaption(Extd_Override_Price);
                        doc.ExportCaption(GP_Override_Price);
                        doc.ExportCaption(Override_Core);
                        doc.ExportCaption(Override_Percent);
                        doc.ExportCaption(Core_Life_Ind);
                        doc.ExportCaption(Notes);
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
                            await doc.ExportField(id);
                            await doc.ExportField(request_id);
                            await doc.ExportField(Item_No);
                            await doc.ExportField(Part_No);
                            await doc.ExportField(Part_Description);
                            await doc.ExportField(Qty);
                            await doc.ExportField(SAP_Unit_Price);
                            await doc.ExportField(Extd_SAP_Price);
                            await doc.ExportField(GP_SAP_Price);
                            await doc.ExportField(Override_Unit_Price);
                            await doc.ExportField(Extd_Override_Price);
                            await doc.ExportField(GP_Override_Price);
                            await doc.ExportField(Override_Core);
                            await doc.ExportField(Override_Percent);
                            await doc.ExportField(Core_Life_Ind);
                            await doc.ExportField(Notes);
                        } else {
                            await doc.ExportField(Item_No);
                            await doc.ExportField(Part_No);
                            await doc.ExportField(Part_Description);
                            await doc.ExportField(Qty);
                            await doc.ExportField(SAP_Unit_Price);
                            await doc.ExportField(Extd_SAP_Price);
                            await doc.ExportField(GP_SAP_Price);
                            await doc.ExportField(Override_Unit_Price);
                            await doc.ExportField(Extd_Override_Price);
                            await doc.ExportField(GP_Override_Price);
                            await doc.ExportField(Override_Core);
                            await doc.ExportField(Override_Percent);
                            await doc.ExportField(Core_Life_Ind);
                            await doc.ExportField(Notes);
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

            // Set up field names
            string fldName = "", fileNameFld = "", fileTypeFld = "";
            if (SameText(fldparm, "request_id")) {
                fldName = "request_id";
                fileNameFld = "request_id";
            } else {
                return JsonBoolResult.FalseResult; // Incorrect field
            }

            // Set up key values
            if (keys.Length == 1) {
                id.CurrentValue = keys[0];
            } else {
                return JsonBoolResult.FalseResult; // Incorrect key
            }

            // Set up filter (WHERE Clause)
            string filter = GetRecordFilter();
            CurrentFilter = filter;
            string sql = CurrentSql;
            using var rs = await Connection.GetDataReaderAsync(sql);
            if (rs != null && await rs.ReadAsync()) {
                var val = rs[fldName];
                if (!Empty(val)) {
                    if (Fields.TryGetValue(fldName, out DbField? fld) && fld != null) {
                        // Binary data
                        if (fld.IsBlob) {
                            byte[] data = (byte[])val;
                            if (resize && data.Length > 0)
                                ResizeBinary(ref data, ref width, ref height);
                            string? contentType = "";

                            // Write file type
                            if (!Empty(fileTypeFld) && !Empty(rs[fileTypeFld]))
                                contentType = ConvertToString(rs[fileTypeFld]);
                            else
                                contentType = ContentType(data);

                            // Write file data
                            if (data.Take(8).SequenceEqual(new byte[] {0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00}) && // Fix Office 2007 documents
                                !data.TakeLast(4).SequenceEqual(new byte[] {0x00, 0x00, 0x00, 0x00}))
                                    data.Concat(new byte[] {0x00, 0x00, 0x00, 0x00});

                            // Clear any debug message
                            // Response?.Clear();

                            // Return file content result // DN
                            string downloadFileName = !Empty(fileNameFld) && !Empty(rs[fileNameFld]) ?
                                ConvertToString(rs[fileNameFld]) :
                                DownloadFileName;
                            string ext = ContentExtension(data).Replace(".", ""); // Get file extension
                            if (ext == "pdf" && Config.EmbedPdf) // Embed Pdf // DN
                                downloadFileName = "";
                            if (!Empty(downloadFileName))
                                return Controller.File(data, contentType, downloadFileName);
                            else
                                return Controller.File(data, contentType);

                        // Upload to folder
                        } else {
                            List<string> files;
                            if (fld.UploadMultiple)
                                files = ConvertToString(val).Split(Config.MultipleUploadSeparator).ToList();
                            else
                                files = new () { ConvertToString(val) };
                            var mi = fld.GetType().GetMethod("GetUploadPath");
                            if (mi != null) // GetUploadPath
                                fld.UploadPath = ConvertToString(mi.Invoke(fld, null));
                            var result = files.ToDictionary(f => f, f => Config.EncryptFilePath
                                ? FullUrl(Config.ApiUrl + Config.ApiFileAction + "/" + TableVar + "/" + Encrypt(fld.PhysicalUploadPath + f))
                                : FullUrl(fld.HrefPath + f));
                            return new JsonBoolResult(new Dictionary<string, object> { { fld.Param, result } }, true);
                        }
                    }
                }
            }
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
            //Log("Row Inserted");
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
