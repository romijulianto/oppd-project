namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// trRequestDelete
    /// </summary>
    public static TrRequestDelete trRequestDelete
    {
        get => HttpData.Get<TrRequestDelete>("trRequestDelete")!;
        set => HttpData["trRequestDelete"] = value;
    }

    /// <summary>
    /// Page class for tr_request
    /// </summary>
    public class TrRequestDelete : TrRequestDeleteBase
    {
        // Constructor
        public TrRequestDelete(Controller controller) : base(controller)
        {
        }

        // Constructor
        public TrRequestDelete() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TrRequestDeleteBase : TrRequest
    {
        // Page ID
        public string PageID = "delete";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "tr_request";

        // Page object name
        public string PageObjName = "trRequestDelete";

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
        public TrRequestDeleteBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-bordered table-hover table-sm ew-table";

            // Language object
            Language = ResolveLanguage();

            // Table object (trRequest)
            if (trRequest == null || trRequest is TrRequest)
                trRequest = this;

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
        public string PageName => "TrRequestDelete";

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
            Request_No.SetVisibility();
            Reference_Doc.SetVisibility();
            Reason.SetVisibility();
            Request_By.SetVisibility();
            Request_By_Name.SetVisibility();
            Request_By_Position.SetVisibility();
            Request_By_Branch.SetVisibility();
            Request_By_Region.SetVisibility();
            Request_Industry.SetVisibility();
            Customer_ID.SetVisibility();
            Customer_Name.SetVisibility();
            SAP_Total.SetVisibility();
            Override_Total.SetVisibility();
            Variance_Total.SetVisibility();
            Variance_Percent.SetVisibility();
            Notes.SetVisibility();
            Next_Approver.SetVisibility();
            Request_PIC.SetVisibility();
            Request_Status.SetVisibility();
            List_Approver.SetVisibility();
            Last_Update_By.SetVisibility();
            Created_By.SetVisibility();
            Created_Date.SetVisibility();
            ETL_Date.Visible = false;
            Request_Currency.SetVisibility();
        }

        // Constructor
        public TrRequestDeleteBase(Controller? controller = null): this() { // DN
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

            // Set up lookup cache
            await SetupLookupOptions(Reason);
            await SetupLookupOptions(Request_Status);

            // Set up Breadcrumb
            SetupBreadcrumb();

            // Load key parameters
            RecordKeys = GetRecordKeys(); // Load record keys
            string filter = GetFilterFromRecordKeys();
            if (Empty(filter))
                return Terminate("TrRequestList"); // Prevent SQL injection, return to List page

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
                    return Terminate("TrRequestList"); // Return to list
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
                trRequestDelete?.PageRender();
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
            Request_No.SetDbValue(row["Request_No"]);
            Reference_Doc.SetDbValue(row["Reference_Doc"]);
            Reason.SetDbValue(row["Reason"]);
            Request_By.SetDbValue(row["Request_By"]);
            Request_By_Name.SetDbValue(row["Request_By_Name"]);
            Request_By_Position.SetDbValue(row["Request_By_Position"]);
            Request_By_Branch.SetDbValue(row["Request_By_Branch"]);
            Request_By_Region.SetDbValue(row["Request_By_Region"]);
            Request_Industry.SetDbValue(row["Request_Industry"]);
            Customer_ID.SetDbValue(row["Customer_ID"]);
            Customer_Name.SetDbValue(row["Customer_Name"]);
            SAP_Total.SetDbValue(row["SAP_Total"]);
            Override_Total.SetDbValue(row["Override_Total"]);
            Variance_Total.SetDbValue(row["Variance_Total"]);
            Variance_Percent.SetDbValue(row["Variance_Percent"]);
            Notes.SetDbValue(row["Notes"]);
            Next_Approver.SetDbValue(row["Next_Approver"]);
            Request_PIC.SetDbValue(row["Request_PIC"]);
            Request_Status.SetDbValue(row["Request_Status"]);
            List_Approver.SetDbValue(row["List_Approver"]);
            Last_Update_By.SetDbValue(row["Last_Update_By"]);
            Created_By.SetDbValue(row["Created_By"]);
            Created_Date.SetDbValue(row["Created_Date"]);
            ETL_Date.SetDbValue(row["ETL_Date"]);
            Request_Currency.SetDbValue(row["Request_Currency"]);
        }
        #pragma warning restore 162, 168, 1998, 4014

        // Return a row with default values
        protected Dictionary<string, object> NewRow() {
            var row = new Dictionary<string, object>();
            row.Add("id", id.DefaultValue ?? DbNullValue); // DN
            row.Add("Request_No", Request_No.DefaultValue ?? DbNullValue); // DN
            row.Add("Reference_Doc", Reference_Doc.DefaultValue ?? DbNullValue); // DN
            row.Add("Reason", Reason.DefaultValue ?? DbNullValue); // DN
            row.Add("Request_By", Request_By.DefaultValue ?? DbNullValue); // DN
            row.Add("Request_By_Name", Request_By_Name.DefaultValue ?? DbNullValue); // DN
            row.Add("Request_By_Position", Request_By_Position.DefaultValue ?? DbNullValue); // DN
            row.Add("Request_By_Branch", Request_By_Branch.DefaultValue ?? DbNullValue); // DN
            row.Add("Request_By_Region", Request_By_Region.DefaultValue ?? DbNullValue); // DN
            row.Add("Request_Industry", Request_Industry.DefaultValue ?? DbNullValue); // DN
            row.Add("Customer_ID", Customer_ID.DefaultValue ?? DbNullValue); // DN
            row.Add("Customer_Name", Customer_Name.DefaultValue ?? DbNullValue); // DN
            row.Add("SAP_Total", SAP_Total.DefaultValue ?? DbNullValue); // DN
            row.Add("Override_Total", Override_Total.DefaultValue ?? DbNullValue); // DN
            row.Add("Variance_Total", Variance_Total.DefaultValue ?? DbNullValue); // DN
            row.Add("Variance_Percent", Variance_Percent.DefaultValue ?? DbNullValue); // DN
            row.Add("Notes", Notes.DefaultValue ?? DbNullValue); // DN
            row.Add("Next_Approver", Next_Approver.DefaultValue ?? DbNullValue); // DN
            row.Add("Request_PIC", Request_PIC.DefaultValue ?? DbNullValue); // DN
            row.Add("Request_Status", Request_Status.DefaultValue ?? DbNullValue); // DN
            row.Add("List_Approver", List_Approver.DefaultValue ?? DbNullValue); // DN
            row.Add("Last_Update_By", Last_Update_By.DefaultValue ?? DbNullValue); // DN
            row.Add("Created_By", Created_By.DefaultValue ?? DbNullValue); // DN
            row.Add("Created_Date", Created_Date.DefaultValue ?? DbNullValue); // DN
            row.Add("ETL_Date", ETL_Date.DefaultValue ?? DbNullValue); // DN
            row.Add("Request_Currency", Request_Currency.DefaultValue ?? DbNullValue); // DN
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

            // View row
            if (RowType == RowType.View) {
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

                // Request_Currency
                Request_Currency.ViewValue = ConvertToString(Request_Currency.CurrentValue); // DN
                Request_Currency.ViewCustomAttributes = "";

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

                // Request_Currency
                Request_Currency.HrefValue = "";
                Request_Currency.TooltipValue = "";
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

        // Set up Breadcrumb
        protected void SetupBreadcrumb() {
            var breadcrumb = new Breadcrumb();
            string url = CurrentUrl();
            breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("TrRequestList")), "", TableVar, true);
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
