namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// trRequestDetailDelete
    /// </summary>
    public static TrRequestDetailDelete trRequestDetailDelete
    {
        get => HttpData.Get<TrRequestDetailDelete>("trRequestDetailDelete")!;
        set => HttpData["trRequestDetailDelete"] = value;
    }

    /// <summary>
    /// Page class for tr_request_detail
    /// </summary>
    public class TrRequestDetailDelete : TrRequestDetailDeleteBase
    {
        // Constructor
        public TrRequestDetailDelete(Controller controller) : base(controller)
        {
        }

        // Constructor
        public TrRequestDetailDelete() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TrRequestDetailDeleteBase : TrRequestDetail
    {
        // Page ID
        public string PageID = "delete";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "tr_request_detail";

        // Page object name
        public string PageObjName = "trRequestDetailDelete";

        // Title
        public string? Title = null; // Title for <title> tag

        // Page headings
        public string Heading = "";

        public string Subheading = "";

        public string PageHeader = "";

        public string PageFooter = "";

        // Token
        public string? Token = null; // DN

        public bool CheckToken = Config.CheckToken;

        // Action result // DN
        public IActionResult? ActionResult;

        // Cache // DN
        public IMemoryCache? Cache;

        // Page layout
        public bool UseLayout = true;

        // Page terminated // DN
        private bool _terminated = false;

        // Is terminated
        public bool IsTerminated => _terminated;

        // Is lookup
        public bool IsLookup => IsApi() && RouteValues.TryGetValue("controller", out object? name) && SameText(name, Config.ApiLookupAction);

        // Is AutoFill
        public bool IsAutoFill => IsLookup && SameText(Post("ajax"), "autofill");

        // Is AutoSuggest
        public bool IsAutoSuggest => IsLookup && SameText(Post("ajax"), "autosuggest");

        // Is modal lookup
        public bool IsModalLookup => IsLookup && SameText(Post("ajax"), "modal");

        // Page URL
        private string _pageUrl = "";

        // Constructor
        public TrRequestDetailDeleteBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-bordered table-hover table-sm ew-table";

            // Language object
            Language = ResolveLanguage();

            // Table object (trRequestDetail)
            if (trRequestDetail == null || trRequestDetail is TrRequestDetail)
                trRequestDetail = this;

            // Start time
            StartTime = Environment.TickCount;

            // Debug message
            LoadDebugMessage();

            // Open connection
            Conn = Connection; // DN

            // User table object (mt_user)
            UserTable = Resolve("usertable")!;
            UserTableConn = GetConnection(UserTable.DbId);
        }

        // Page action result
        public IActionResult PageResult()
        {
            if (ActionResult != null)
                return ActionResult;
            SetupMenus();
            return Controller.View();
        }

        // Page heading
        public string PageHeading
        {
            get {
                if (!Empty(Heading))
                    return Heading;
                else if (!Empty(Caption))
                    return Caption;
                else
                    return "";
            }
        }

        // Page subheading
        public string PageSubheading
        {
            get {
                if (!Empty(Subheading))
                    return Subheading;
                if (!Empty(TableName))
                    return Language.Phrase(PageID);
                return "";
            }
        }

        // Page name
        public string PageName => "TrRequestDetailDelete";

        // Page URL
        public string PageUrl
        {
            get {
                if (_pageUrl == "") {
                    _pageUrl = PageName + "?";
                }
                return _pageUrl;
            }
        }

        // Show Page Header
        public IHtmlContent ShowPageHeader()
        {
            string header = PageHeader;
            PageDataRendering(ref header);
            if (!Empty(header)) // Header exists, display
                return new HtmlString("<p id=\"ew-page-header\">" + header + "</p>");
            return HtmlString.Empty;
        }

        // Show Page Footer
        public IHtmlContent ShowPageFooter()
        {
            string footer = PageFooter;
            PageDataRendered(ref footer);
            if (!Empty(footer)) // Footer exists, display
                return new HtmlString("<p id=\"ew-page-footer\">" + footer + "</p>");
            return HtmlString.Empty;
        }

        // Valid post
        protected async Task<bool> ValidPost() => !CheckToken || !IsPost() || IsApi() || Antiforgery != null && HttpContext != null && await Antiforgery.IsRequestValidAsync(HttpContext);

        // Create token
        public void CreateToken()
        {
            Token ??= HttpContext != null ? Antiforgery?.GetAndStoreTokens(HttpContext).RequestToken : null;
            CurrentToken = Token ?? ""; // Save to global variable
        }

        // Set field visibility
        public void SetVisibility()
        {
            id.Visible = false;
            request_id.Visible = false;
            Item_No.SetVisibility();
            Part_No.SetVisibility();
            Part_Description.SetVisibility();
            Qty.SetVisibility();
            SAP_Unit_Price.SetVisibility();
            Extd_SAP_Price.SetVisibility();
            GP_SAP_Price.SetVisibility();
            Override_Unit_Price.SetVisibility();
            Extd_Override_Price.SetVisibility();
            GP_Override_Price.SetVisibility();
            Override_Core.SetVisibility();
            Override_Percent.SetVisibility();
            Core_Life_Ind.SetVisibility();
            Notes.SetVisibility();
        }

        // Constructor
        public TrRequestDetailDeleteBase(Controller? controller = null): this() { // DN
            if (controller != null)
                Controller = controller;
        }

        /// <summary>
        /// Terminate page
        /// </summary>
        /// <param name="url">URL to rediect to</param>
        /// <returns>Page result</returns>
        public override IActionResult Terminate(string url = "") { // DN
            if (_terminated) // DN
                return new EmptyResult();

            // Page Unload event
            PageUnload();

            // Global Page Unloaded event
            PageUnloaded();
            if (!IsApi())
                PageRedirecting(ref url);

            // Gargage collection
            Collect(); // DN

            // Terminate
            _terminated = true; // DN

            // Return for API
            if (IsApi()) {
                var result = new Dictionary<string, string> { { "version", Config.ProductVersion } };
                if (!Empty(url)) // Add url
                    result.Add("url", GetUrl(url));
                foreach (var (key, value) in GetMessages()) // Add messages
                    result.Add(key, value);
                return Controller.Json(result);
            } else if (ActionResult != null) { // Check action result
                return ActionResult;
            }

            // Go to URL if specified
            if (!Empty(url)) {
                if (!Config.Debug)
                    ResponseClear();
                if (Response != null && !Response.HasStarted) {
                    SaveDebugMessage();
                    return Controller.LocalRedirect(AppPath(url));
                }
            }
            return new EmptyResult();
        }

        // Get all records from datareader
        [return: NotNullIfNotNull("dr")]
        protected async Task<List<Dictionary<string, object>>> GetRecordsFromRecordset(DbDataReader? dr)
        {
            var rows = new List<Dictionary<string, object>>();
            while (dr != null && await dr.ReadAsync()) {
                await LoadRowValues(dr); // Set up DbValue/CurrentValue
                if (GetRecordFromDictionary(GetDictionary(dr)) is Dictionary<string, object> row)
                    rows.Add(row);
            }
            return rows;
        }

        // Get all records from the list of records
        #pragma warning disable 1998

        protected async Task<List<Dictionary<string, object>>> GetRecordsFromRecordset(List<Dictionary<string, object>>? list)
        {
            var rows = new List<Dictionary<string, object>>();
            if (list != null) {
                foreach (var row in list) {
                    if (GetRecordFromDictionary(row) is Dictionary<string, object> d)
                       rows.Add(row);
                }
            }
            return rows;
        }
        #pragma warning restore 1998

        // Get the first record from datareader
        [return: NotNullIfNotNull("dr")]
        protected async Task<Dictionary<string, object>?> GetRecordFromRecordset(DbDataReader? dr)
        {
            if (dr != null) {
                await LoadRowValues(dr); // Set up DbValue/CurrentValue
                return GetRecordFromDictionary(GetDictionary(dr));
            }
            return null;
        }

        // Get the first record from the list of records
        protected Dictionary<string, object>? GetRecordFromRecordset(List<Dictionary<string, object>>? list) =>
            list != null && list.Count > 0 ? GetRecordFromDictionary(list[0]) : null;

        // Get record from Dictionary
        protected Dictionary<string, object>? GetRecordFromDictionary(Dictionary<string, object>? dict) {
            if (dict == null)
                return null;
            var row = new Dictionary<string, object>();
            foreach (var (key, value) in dict) {
                if (Fields.TryGetValue(key, out DbField? fld)) {
                    if (fld.Visible || fld.IsPrimaryKey) { // Primary key or Visible
                        if (fld.HtmlTag == "FILE") { // Upload field
                            if (Empty(value)) {
                                // row[key] = null;
                            } else {
                                if (fld.DataType == DataType.Blob) {
                                    string url = FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + fld.Param + "/" + GetRecordKeyValue(dict)); // Query string format
                                    row[key] = new Dictionary<string, object> { { "type", ContentType((byte[])value) }, { "url", url }, { "name", fld.Param + ContentExtension((byte[])value) } };
                                } else if (!fld.UploadMultiple || !ConvertToString(value).Contains(Config.MultipleUploadSeparator)) { // Single file
                                    string url = FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + Encrypt(fld.PhysicalUploadPath + ConvertToString(value))); // Query string format
                                    row[key] = new Dictionary<string, object> { { "type", ContentType(ConvertToString(value)) }, { "url", url }, { "name", ConvertToString(value) } };
                                } else { // Multiple files
                                    var files = ConvertToString(value).Split(Config.MultipleUploadSeparator);
                                    row[key] = files.Where(file => !Empty(file)).Select(file => new Dictionary<string, object> { { "type", ContentType(file) }, { "url", FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + Encrypt(fld.PhysicalUploadPath + file)) }, { "name", file } });
                                }
                            }
                        } else {
                            string val = ConvertToString(value);
                            if (fld.DataType == DataType.Date && value is DateTime dt)
                                val = dt.ToString("s");
                            row[key] = ConvertToString(val);
                        }
                    }
                }
            }
            return row;
        }

        // Get record key value from array
        protected string GetRecordKeyValue(Dictionary<string, object> dict) {
            string key = "";
            key += UrlEncode(ConvertToString(dict.ContainsKey("id") ? dict["id"] : id.CurrentValue));
            return key;
        }

        // Hide fields for Add/Edit
        protected void HideFieldsForAddEdit() {
        }

        public string DbMasterFilter = "";

        public string DbDetailFilter = "";

        public int StartRecord;

        public int TotalRecords;

        public int RecordCount;

        public List<string> RecordKeys = new ();

        public DbDataReader? Recordset;

        public int StartRowCount = 1;

        public bool IsModal = false;

        /// <summary>
        /// Page run
        /// </summary>
        /// <returns>Page result</returns>
        public override async Task<IActionResult> Run()
        {
            // Use layout
            if (!Empty(Param("layout")) && !Param<bool>("layout"))
                UseLayout = false;

            // User profile
            Profile = ResolveProfile();

            // Security
            Security = ResolveSecurity();
            if (TableVar != "")
                Security.LoadTablePermissions(TableVar);
            CurrentAction = Param("action"); // Set up current action
            SetVisibility();

            // Do not use lookup cache
            if (!Config.LookupCachePageIds.Contains(PageID))
                SetUseLookupCache(false);

            // Global Page Loading event
            PageLoading();

            // Page Load event
            PageLoad();

            // Check token
            if (!await ValidPost())
                End(Language.Phrase("InvalidPostRequest"));

            // Check action result
            if (ActionResult != null) // Action result set by server event // DN
                return ActionResult;

            // Create token
            CreateToken();

            // Hide fields for add/edit
            if (!UseAjaxActions)
                HideFieldsForAddEdit();
            // Use inline delete
            if (UseAjaxActions)
                InlineDelete = true;

            // Set up master/detail parameters
            SetupMasterParms();

            // Set up Breadcrumb
            SetupBreadcrumb();

            // Load key parameters
            RecordKeys = GetRecordKeys(); // Load record keys
            string filter = GetFilterFromRecordKeys();
            if (Empty(filter))
                return Terminate("TrRequestDetailList"); // Prevent SQL injection, return to List page

            // Set up filter (WHERE Clause)
            CurrentFilter = filter;

            // Get action
            if (IsApi()) {
                CurrentAction = "delete"; // Delete record directly
            } else if (!Empty(Param("action"))) {
                CurrentAction = Param("action") == "delete" ? "delete" : "show";
            } else {
                CurrentAction = InlineDelete ?
                    "delete" : // Delete record directly
                    "show"; // Display record
            }
            if (IsDelete) { // DN
                SendEmail = true; // Send email on delete success
                var res = await DeleteRows();
                if (res) { // Delete rows
                    if (Empty(SuccessMessage))
                        SuccessMessage = Language.Phrase("DeleteSuccess"); // Set up success message
                    if (IsJsonResponse()) {
                        ClearMessages(); // Clear messages for Json response
                        return res;
                    } else {
                        return Terminate(ReturnUrl); // Return to caller
                    }
                } else { // Delete failed
                    if (IsJsonResponse()) {
                        return Terminate();
                    }
                    // Return JSON error message if UseAjaxActions
                    if (UseAjaxActions)
                        return Controller.Json(new { success = false, error = GetFailureMessage() });
                    if (InlineDelete)
                        return Terminate(ReturnUrl); // Return to caller
                    else
                        CurrentAction = "show"; // Display record
                }
            }
            if (IsShow) { // Load records for display // DN
                Recordset = await LoadRecordset();
                TotalRecords = await ListRecordCountAsync(); // Get record count
                if (TotalRecords <= 0) { // No record found, exit
                    CloseRecordset(); // DN
                    return Terminate("TrRequestDetailList"); // Return to list
                }
            }

            // Set LoginStatus, Page Rendering and Page Render
            if (!IsApi() && !IsTerminated) {
                SetupLoginStatus(); // Setup login status

                // Pass login status to client side
                SetClientVar("login", LoginStatus);

                // Global Page Rendering event
                PageRendering();

                // Page Render event
                trRequestDetailDelete?.PageRender();
            }
            return PageResult();
        }

        // Load recordset // DN
        public async Task<DbDataReader?> LoadRecordset(int offset = -1, int rowcnt = -1)
        {
            // Load list page SQL
            string sql = ListSql;

            // Load recordset // DN
            var dr = await Connection.SelectLimit(sql, rowcnt, offset, !Empty(OrderBy) || !Empty(SessionOrderBy));

            // Call Recordset Selected event
            RecordsetSelected(dr);
            return dr;
        }

        // Load rows // DN
        public async Task<List<Dictionary<string, object>>> LoadRows(int offset = -1, int rowcnt = -1)
        {
            // Load list page SQL
            string sql = ListSql;

            // Load rows // DN
            using var dr = await Connection.SelectLimit(sql, rowcnt, offset, !Empty(OrderBy) || !Empty(SessionOrderBy));
            var rows = await Connection.GetRowsAsync(dr);
            dr.Close(); // Close datareader before return
            return rows;
        }

        // Load row based on key values
        public async Task<bool> LoadRow()
        {
            string filter = GetRecordFilter();

            // Call Row Selecting event
            RowSelecting(ref filter);

            // Load SQL based on filter
            CurrentFilter = filter;
            string sql = CurrentSql;
            bool res = false;
            try {
                var row = await Connection.GetRowAsync(sql);
                if (row != null) {
                    await LoadRowValues(row);
                    res = true;
                } else {
                    return false;
                }
            } catch {
                if (Config.Debug)
                    throw;
            }
            return res;
        }

        #pragma warning disable 162, 168, 1998, 4014
        // Load row values from data reader
        public async Task LoadRowValues(DbDataReader? dr = null) => await LoadRowValues(GetDictionary(dr));

        // Load row values from recordset
        public async Task LoadRowValues(Dictionary<string, object>? row)
        {
            row ??= NewRow();

            // Call Row Selected event
            RowSelected(row);
            id.SetDbValue(row["id"]);
            request_id.Upload.DbValue = row["request_id"];
            request_id.SetDbValue(request_id.Upload.DbValue);
            Item_No.SetDbValue(row["Item_No"]);
            Part_No.SetDbValue(row["Part_No"]);
            Part_Description.SetDbValue(row["Part_Description"]);
            Qty.SetDbValue(row["Qty"]);
            SAP_Unit_Price.SetDbValue(row["SAP_Unit_Price"]);
            Extd_SAP_Price.SetDbValue(row["Extd_SAP_Price"]);
            GP_SAP_Price.SetDbValue(row["GP_SAP_Price"]);
            Override_Unit_Price.SetDbValue(row["Override_Unit_Price"]);
            Extd_Override_Price.SetDbValue(row["Extd_Override_Price"]);
            GP_Override_Price.SetDbValue(row["GP_Override_Price"]);
            Override_Core.SetDbValue(row["Override_Core"]);
            Override_Percent.SetDbValue(row["Override_Percent"]);
            Core_Life_Ind.SetDbValue(row["Core_Life_Ind"]);
            Notes.SetDbValue(row["Notes"]);
        }
        #pragma warning restore 162, 168, 1998, 4014

        // Return a row with default values
        protected Dictionary<string, object> NewRow() {
            var row = new Dictionary<string, object>();
            row.Add("id", id.DefaultValue ?? DbNullValue); // DN
            row.Add("request_id", request_id.DefaultValue ?? DbNullValue); // DN
            row.Add("Item_No", Item_No.DefaultValue ?? DbNullValue); // DN
            row.Add("Part_No", Part_No.DefaultValue ?? DbNullValue); // DN
            row.Add("Part_Description", Part_Description.DefaultValue ?? DbNullValue); // DN
            row.Add("Qty", Qty.DefaultValue ?? DbNullValue); // DN
            row.Add("SAP_Unit_Price", SAP_Unit_Price.DefaultValue ?? DbNullValue); // DN
            row.Add("Extd_SAP_Price", Extd_SAP_Price.DefaultValue ?? DbNullValue); // DN
            row.Add("GP_SAP_Price", GP_SAP_Price.DefaultValue ?? DbNullValue); // DN
            row.Add("Override_Unit_Price", Override_Unit_Price.DefaultValue ?? DbNullValue); // DN
            row.Add("Extd_Override_Price", Extd_Override_Price.DefaultValue ?? DbNullValue); // DN
            row.Add("GP_Override_Price", GP_Override_Price.DefaultValue ?? DbNullValue); // DN
            row.Add("Override_Core", Override_Core.DefaultValue ?? DbNullValue); // DN
            row.Add("Override_Percent", Override_Percent.DefaultValue ?? DbNullValue); // DN
            row.Add("Core_Life_Ind", Core_Life_Ind.DefaultValue ?? DbNullValue); // DN
            row.Add("Notes", Notes.DefaultValue ?? DbNullValue); // DN
            return row;
        }

        #pragma warning disable 1998
        // Render row values based on field settings
        public async Task RenderRow()
        {
            // Call Row Rendering event
            RowRendering();

            // Common render codes for all row types

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

            // View row
            if (RowType == RowType.View) {
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
            }

            // Call Row Rendered event
            if (RowType != RowType.AggregateInit)
                RowRendered();
        }
        #pragma warning restore 1998

        // Delete records (based on current filter)
        protected async Task<JsonBoolResult> DeleteRows() { // DN
            if (!Security.CanDelete) {
                FailureMessage = Language.Phrase("NoDeletePermission"); // No delete permission
                return JsonBoolResult.FalseResult; // No delete permission
            }
            List<Dictionary<string, object>>? rsold = null;
            bool result = true;
            try {
                string sql = CurrentSql;
                using var rs = await Connection.GetDataReaderAsync(sql);
                if (rs == null) {
                    return JsonBoolResult.FalseResult;
                } else if (!rs.HasRows) {
                    FailureMessage = Language.Phrase("NoRecord"); // No record found
                    return JsonBoolResult.FalseResult;
                } else { // Clone old rows
                    rsold = await Connection.GetRowsAsync(rs);
                }
            } catch (Exception e) {
                if (Config.Debug)
                    throw;
                FailureMessage = e.Message;
                return JsonBoolResult.FalseResult;
            }
            if (UseTransaction)
                Connection.BeginTrans();
            List<string> successKeys = new (), failKeys = new ();
            try {
                // Call Row Deleting event
                if (result)
                    result = rsold.All(row => RowDeleting(row));
                if (result) {
                    foreach (var row in rsold) {
                        try {
                            result = await DeleteAsync(row) > 0;
                        } catch (Exception e) {
                            if (Config.Debug)
                                throw;
                            FailureMessage = e.Message; // Set up error message
                            result = false;
                        }
                        if (!result) {
                            if (UseTransaction) {
                                successKeys.Clear();
                                break;
                            }
                            failKeys.Add(GetKey(row)); // DN
                        } else {
                            if (Config.DeleteUploadFiles)
                                DeleteUploadedFiles(row);
                            RowDeleted(row);
                            successKeys.Add(GetKey(row)); // DN
                        }
                    }
                }
                result = successKeys.Count > 0;
                if (!result) {
                    // Set up error message
                    if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {
                        // Use the message, do nothing
                    } else if (!Empty(CancelMessage)) {
                        FailureMessage = CancelMessage;
                        CancelMessage = "";
                    } else {
                        FailureMessage = Language.Phrase("DeleteCancelled");
                    }
                }
            } catch (Exception e) {
                FailureMessage = e.Message;
                result = false;
            }
            if (result) {
                if (UseTransaction)
                    Connection.CommitTrans(); // Commit the changes

                // Set warning message if delete some records failed
                if (failKeys.Count > 0)
                    WarningMessage = Language.Phrase("DeleteRecordsFailed").Replace("%k", String.Join(", ", failKeys));
            } else {
                if (UseTransaction)
                    Connection.RollbackTrans(); // Rollback changes
            }

            // Write JSON for API request
            Dictionary<string, object> d = new ();
            d.Add("success", result);
            if (IsJsonResponse() && result) {
                var rows = await GetRecordsFromRecordset(rsold);
                string table = TableVar;
                d.Add(table, RouteValues.Count > 2 && rows.Count == 1 ? rows[0] : rows); // If single-delete, route values are controller/action/id (count > 2)
                d.Add("action", Config.ApiDeleteAction);
                d.Add("version", Config.ProductVersion);
                return new JsonBoolResult(d, true);
            }
            return new JsonBoolResult(d, result);
        }

        // Set up master/detail based on QueryString
        protected void SetupMasterParms() {
            bool validMaster = false;
            StringValues masterTblVar;
            StringValues fk;
            Dictionary<string, object> foreignKeys = new ();

            // Get the keys for master table
            if (Query.TryGetValue(Config.TableShowMaster, out masterTblVar) || Query.TryGetValue(Config.TableMaster, out masterTblVar)) { // Do not use Get()
                if (Empty(masterTblVar)) {
                    validMaster = true;
                    DbMasterFilter = "";
                    DbDetailFilter = "";
                }
                if (masterTblVar == "v_tr_request") {
                    validMaster = true;
                    if (vTrRequest != null && (Get("fk_id", out fk) || Get("request_id", out fk))) {
                        vTrRequest.id.QueryValue = fk;
                        request_id.QueryValue = vTrRequest.id.QueryValue;
                        request_id.SessionValue = request_id.QueryValue;
                        foreignKeys.Add("request_id", fk);
                        if (!IsNumeric(request_id.QueryValue))
                            validMaster = false;
                    } else {
                        validMaster = false;
                    }
                }
                if (masterTblVar == "tr_request") {
                    validMaster = true;
                    if (trRequest != null && (Get("fk_id", out fk) || Get("request_id", out fk))) {
                        trRequest.id.QueryValue = fk;
                        request_id.QueryValue = trRequest.id.QueryValue;
                        request_id.SessionValue = request_id.QueryValue;
                        foreignKeys.Add("request_id", fk);
                        if (!IsNumeric(request_id.QueryValue))
                            validMaster = false;
                    } else {
                        validMaster = false;
                    }
                }
            } else if (Form.TryGetValue(Config.TableShowMaster, out masterTblVar) || Form.TryGetValue(Config.TableMaster, out masterTblVar)) {
                if (masterTblVar == "") {
                    validMaster = true;
                    DbMasterFilter = "";
                    DbDetailFilter = "";
                }
                if (masterTblVar == "v_tr_request") {
                    validMaster = true;
                    if (vTrRequest != null && (Post("fk_id", out fk) || Post("request_id", out fk))) {
                        vTrRequest.id.FormValue = fk;
                        request_id.FormValue = vTrRequest.id.FormValue;
                        request_id.SessionValue = request_id.FormValue;
                        foreignKeys.Add("request_id", fk);
                        if (!IsNumeric(request_id.FormValue))
                            validMaster = false;
                    } else {
                        validMaster = false;
                    }
                }
                if (masterTblVar == "tr_request") {
                    validMaster = true;
                    if (trRequest != null && (Post("fk_id", out fk) || Post("request_id", out fk))) {
                        trRequest.id.FormValue = fk;
                        request_id.FormValue = trRequest.id.FormValue;
                        request_id.SessionValue = request_id.FormValue;
                        foreignKeys.Add("request_id", fk);
                        if (!IsNumeric(request_id.FormValue))
                            validMaster = false;
                    } else {
                        validMaster = false;
                    }
                }
            }
            if (validMaster) {
                // Save current master table
                CurrentMasterTable = masterTblVar.ToString();

                // Reset start record counter (new master key)
                if (!IsAddOrEdit) {
                    StartRecord = 1;
                    StartRecordNumber = StartRecord;
                }

                // Clear previous master key from Session
                if (masterTblVar != "v_tr_request") {
                    if (!foreignKeys.ContainsKey("request_id")) // Not current foreign key
                        request_id.SessionValue = "";
                }
                if (masterTblVar != "tr_request") {
                    if (!foreignKeys.ContainsKey("request_id")) // Not current foreign key
                        request_id.SessionValue = "";
                }
            }
            DbMasterFilter = MasterFilterFromSession; // Get master filter from session
            DbDetailFilter = DetailFilterFromSession; // Get detail filter from session
        }

        // Set up Breadcrumb
        protected void SetupBreadcrumb() {
            var breadcrumb = new Breadcrumb();
            string url = CurrentUrl();
            breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("TrRequestDetailList")), "", TableVar, true);
            string pageId = "delete";
            breadcrumb.Add("delete", pageId, url);
            CurrentBreadcrumb = breadcrumb;
        }

        // Setup lookup options
        public async Task SetupLookupOptions(DbField fld)
        {
            if (fld.Lookup == null)
                return;
            Func<string>? lookupFilter = null;
            dynamic conn = Connection;
            if (fld.Lookup.Options.Count is int c && c == 0) {
                // Always call to Lookup.GetSql so that user can setup Lookup.Options in Lookup Selecting server event
                var sql = fld.Lookup.GetSql(false, "", lookupFilter, this);

                // Set up lookup cache
                if (!fld.HasLookupOptions && fld.UseLookupCache && !Empty(sql) && fld.Lookup.ParentFields.Count == 0 && fld.Lookup.Options.Count == 0) {
                    int totalCnt = await TryGetRecordCountAsync(sql, conn);
                    if (totalCnt > fld.LookupCacheCount) // Total count > cache count, do not cache
                        return;
                    var dict = new Dictionary<string, Dictionary<string, object>>();
                    var values = new List<object>();
                    List<Dictionary<string, object>> rs = await conn.GetRowsAsync(sql);
                    if (rs != null) {
                        for (int i = 0; i < rs.Count; i++) {
                            var row = rs[i];
                            row = fld.Lookup?.RenderViewRow(row, Resolve(fld.Lookup.LinkTable));
                            string key = row?.Values.First()?.ToString() ?? String.Empty;
                            if (!dict.ContainsKey(key) && row != null)
                                dict.Add(key, row);
                        }
                    }
                    fld.Lookup?.SetOptions(dict);
                }
            }
        }

        // Close recordset
        public void CloseRecordset()
        {
            using (Recordset) {} // Dispose
        }

        // Page Load event
        public virtual void PageLoad() {
            //Log("Page Load");
        }

        // Page Unload event
        public virtual void PageUnload() {
            //Log("Page Unload");
        }

        // Page Redirecting event
        public virtual void PageRedirecting(ref string url) {
            //url = newurl;
        }

        // Message Showing event
        // type = ""|"success"|"failure"|"warning"
        public virtual void MessageShowing(ref string msg, string type) {
            // Note: Do not change msg outside the following 4 cases.
            if (type == "success") {
                //msg = "your success message";
            } else if (type == "failure") {
                //msg = "your failure message";
            } else if (type == "warning") {
                //msg = "your warning message";
            } else {
                //msg = "your message";
            }
        }

        // Page Load event
        public virtual void PageRender() {
            //Log("Page Render");
        }

        // Page Data Rendering event
        public virtual void PageDataRendering(ref string header) {
            // Example:
            //header = "your header";
        }

        // Page Data Rendered event
        public virtual void PageDataRendered(ref string footer) {
            // Example:
            //footer = "your footer";
        }

        // Page Breaking event
        public void PageBreaking(ref bool brk, ref string content) {
            // Example:
            //	brk = false; // Skip page break, or
            //	content = "<div style=\"page-break-after:always;\">&nbsp;</div>"; // Modify page break content
        }
    } // End page class
} // End Partial class
