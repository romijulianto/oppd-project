namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// trRequestUpdate
    /// </summary>
    public static TrRequestUpdate trRequestUpdate
    {
        get => HttpData.Get<TrRequestUpdate>("trRequestUpdate")!;
        set => HttpData["trRequestUpdate"] = value;
    }

    /// <summary>
    /// Page class for tr_request
    /// </summary>
    public class TrRequestUpdate : TrRequestUpdateBase
    {
        // Constructor
        public TrRequestUpdate(Controller controller) : base(controller)
        {
        }

        // Constructor
        public TrRequestUpdate() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TrRequestUpdateBase : TrRequest
    {
        // Page ID
        public string PageID = "update";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "tr_request";

        // Page object name
        public string PageObjName = "trRequestUpdate";

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
        public TrRequestUpdateBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-desktop-table ew-update-table";

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
        public string PageName => "TrRequestUpdate";

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
            id.SetVisibility();
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
        public TrRequestUpdateBase(Controller? controller = null): this() { // DN
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
                    // Handle modal response (Assume return to modal for simplicity)
                    if (IsModal) { // Show as modal
                        var result = new Dictionary<string, string> { {"url", GetUrl(url)}, {"modal", "1"} };
                        string pageName = GetPageName(url);
                        if (pageName != ListUrl) { // Not List page
                            result.Add("caption", GetModalCaption(pageName));
                            result.Add("view", pageName == "TrRequestView" ? "1" : "0"); // If View page, no primary button
                        } else { // List page
                            // result.Add("list", PageID == "search" ? "1" : "0"); // Refresh List page if current page is Search page
                            result.Add("error", FailureMessage); // List page should not be shown as modal => error
                            ClearFailureMessage();
                        }
                        return Controller.Json(result);
                    } else {
                        SaveDebugMessage();
                        return Controller.LocalRedirect(AppPath(url));
                    }
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

        #pragma warning disable 219
        /// <summary>
        /// Lookup data from table
        /// </summary>
        public async Task<Dictionary<string, object>> Lookup(Dictionary<string, string>? dict = null)
        {
            Language = ResolveLanguage();
            Security = ResolveSecurity();
            string? v;

            // Get lookup object
            string fieldName = IsDictionary(dict) && dict.TryGetValue("field", out v) && v != null ? v : Post("field");
            var lookupField = FieldByName(fieldName);
            var lookup = lookupField?.Lookup;
            if (lookup == null) // DN
                return new Dictionary<string, object>();
            string lookupType = IsDictionary(dict) && dict.TryGetValue("ajax", out v) && v != null ? v : (Post("ajax") ?? "unknown");
            int pageSize = -1;
            int offset = -1;
            string searchValue = "";
            if (SameText(lookupType, "modal") || SameText(lookupType, "filter")) {
                searchValue = IsDictionary(dict) && (dict.TryGetValue("q", out v) && v != null || dict.TryGetValue("sv", out v) && v != null)
                    ? v
                    : (Param("q") ?? Post("sv"));
                pageSize = IsDictionary(dict) && (dict.TryGetValue("n", out v) || dict.TryGetValue("recperpage", out v))
                    ? ConvertToInt(v)
                    : (IsNumeric(Param("n")) ? Param<int>("n") : (Post("recperpage", out StringValues rpp) ? ConvertToInt(rpp.ToString()) : 10));
            } else if (SameText(lookupType, "autosuggest")) {
                searchValue = IsDictionary(dict) && dict.TryGetValue("q", out v) && v != null ? v : Param("q");
                pageSize = IsDictionary(dict) && dict.TryGetValue("n", out v) ? ConvertToInt(v) : (IsNumeric(Param("n")) ? Param<int>("n") : -1);
                if (pageSize <= 0)
                    pageSize = Config.AutoSuggestMaxEntries;
            }
            int start = IsDictionary(dict) && dict.TryGetValue("start", out v) ? ConvertToInt(v) : (IsNumeric(Param("start")) ? Param<int>("start") : -1);
            int page = IsDictionary(dict) && dict.TryGetValue("page", out v) ? ConvertToInt(v) : (IsNumeric(Param("page")) ? Param<int>("page") : -1);
            offset = start >= 0 ? start : (page > 0 && pageSize > 0 ? (page - 1) * pageSize : 0);
            string userSelect = Decrypt(IsDictionary(dict) && dict.TryGetValue("s", out v) && v != null ? v : Post("s"));
            string userFilter = Decrypt(IsDictionary(dict) && dict.TryGetValue("f", out v) && v != null ? v : Post("f"));
            string userOrderBy = Decrypt(IsDictionary(dict) && dict.TryGetValue("o", out v) && v != null ? v : Post("o"));

            // Selected records from modal, skip parent/filter fields and show all records
            lookup.LookupType = lookupType; // Lookup type
            lookup.FilterValues.Clear(); // Clear filter values first
            StringValues keys = IsDictionary(dict) && dict.TryGetValue("keys", out v) && !Empty(v)
                ? (StringValues)v
                : (Post("keys[]", out StringValues k) ? (StringValues)k : StringValues.Empty);
            if (!Empty(keys)) { // Selected records from modal
                lookup.FilterFields = new (); // Skip parent fields if any
                pageSize = -1; // Show all records
                lookup.FilterValues.Add(String.Join(",", keys.ToArray()));
            } else { // Lookup values
                string lookupValue = IsDictionary(dict) && (dict.TryGetValue("v0", out v) && v != null || dict.TryGetValue("lookupValue", out v) && v != null)
                    ? v
                    : (Post<string>("v0") ?? Post("lookupValue"));
                lookup.FilterValues.Add(lookupValue);
            }
            int cnt = IsDictionary(lookup.FilterFields) ? lookup.FilterFields.Count : 0;
            for (int i = 1; i <= cnt; i++) {
                var val = UrlDecode(IsDictionary(dict) && dict.TryGetValue("v" + i, out v) ? v : Post("v" + i));
                if (val != null) // DN
                    lookup.FilterValues.Add(val);
            }
            lookup.SearchValue = searchValue;
            lookup.PageSize = pageSize;
            lookup.Offset = offset;
            if (userSelect != "")
                lookup.UserSelect = userSelect;
            if (userFilter != "")
                lookup.UserFilter = userFilter;
            if (userOrderBy != "")
                lookup.UserOrderBy = userOrderBy;
            return await lookup.ToJson(this);
        }
        #pragma warning restore 219

    public string FormClassName = "ew-form ew-update-form";

    public bool IsModal = false;

    public bool IsMobileOrModal = false;

    public DbDataReader? Recordset;

    public List<string> RecordKeys = new ();

    public bool Disabled; // DN

    public int TotalRecords;

    public int UpdateCount = 0;

    /// <summary>
    /// Page run
    /// </summary>
    /// <returns>Page result</returns>
    public override async Task<IActionResult> Run()
    {
            // Is modal
            IsModal = Param<bool>("modal");
            UseLayout = UseLayout && !IsModal;

            // Use layout
            if (!Empty(Param("layout")) && !Param<bool>("layout"))
                UseLayout = false;

            // User profile
            Profile = ResolveProfile();

            // Security
            Security = ResolveSecurity();
            if (TableVar != "")
                Security.LoadTablePermissions(TableVar);

            // Create form object
            CurrentForm ??= new ();
            await CurrentForm.Init();
            CurrentAction = Param("action"); // Set up current action
            SetVisibility();
            id.Required = false;

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

        // Check modal
        if (IsModal)
            SkipHeaderFooter = true;
        IsMobileOrModal = IsMobile() || IsModal;

        // Set up Breadcrumb
        SetupBreadcrumb();

        // Try to load keys from list form
        RecordKeys = GetRecordKeys(); // Load record keys
        if (Post("action", out StringValues sv) && !Empty(sv)) {
            // Get action
            CurrentAction = sv.ToString();
            await LoadFormValues(); // Get form values

            // Validate form
            if (!await ValidateForm()) {
                CurrentAction = "show"; // Form error, reset action
                if (!HasInvalidFields()) // No fields selected
                    FailureMessage = Language.Phrase("NoFieldSelected");
            }
        } else {
            await LoadMultiUpdateValues(); // Load initial values to form
        }
        if (RecordKeys.Count <= 0)
            return Terminate("TrRequestList"); // No records selected, return to list
        if (IsUpdate) {
            if (await UpdateRows()) { // Update Records based on key
                if (Empty(SuccessMessage))
                    SuccessMessage = Language.Phrase("UpdateSuccess"); // Set update success message

                // Do not return Json for UseAjaxActions
                if (IsModal && UseAjaxActions)
                    IsModal = false;
                return Terminate(ReturnUrl); // Return to caller
            } else if (IsModal && UseAjaxActions) { // Return JSON error message
                return Controller.Json(new { success = false, error = GetFailureMessage() });
            } else {
                RestoreFormValues(); // Restore form values
            }
        }

        // Render row
        RowType = RowType.Edit; // Render edit
        ResetAttributes();
        await RenderRow();

            // Set LoginStatus, Page Rendering and Page Render
            if (!IsApi() && !IsTerminated) {
                SetupLoginStatus(); // Setup login status

                // Pass login status to client side
                SetClientVar("login", LoginStatus);

                // Global Page Rendering event
                PageRendering();

                // Page Render event
                trRequestUpdate?.PageRender();
            }
        return PageResult();
    }

    // Load initial values to form if field values are identical in all selected records
    public async Task LoadMultiUpdateValues()
    {
        CurrentFilter = GetFilterFromRecordKeys();
        int i = 1;

        // Load recordset
        using var rs = await LoadRecordset();
        try {
            while (rs != null && await rs.ReadAsync()) {
                if (i == 1) {
                    id.SetDbValue(rs["id"]);
                    Request_No.SetDbValue(rs["Request_No"]);
                    Reference_Doc.SetDbValue(rs["Reference_Doc"]);
                    Reason.SetDbValue(rs["Reason"]);
                    Request_By.SetDbValue(rs["Request_By"]);
                    Request_By_Name.SetDbValue(rs["Request_By_Name"]);
                    Request_By_Position.SetDbValue(rs["Request_By_Position"]);
                    Request_By_Branch.SetDbValue(rs["Request_By_Branch"]);
                    Request_By_Region.SetDbValue(rs["Request_By_Region"]);
                    Request_Industry.SetDbValue(rs["Request_Industry"]);
                    Customer_ID.SetDbValue(rs["Customer_ID"]);
                    Customer_Name.SetDbValue(rs["Customer_Name"]);
                    SAP_Total.SetDbValue(rs["SAP_Total"]);
                    Override_Total.SetDbValue(rs["Override_Total"]);
                    Variance_Total.SetDbValue(rs["Variance_Total"]);
                    Variance_Percent.SetDbValue(rs["Variance_Percent"]);
                    Notes.SetDbValue(rs["Notes"]);
                    Next_Approver.SetDbValue(rs["Next_Approver"]);
                    Request_PIC.SetDbValue(rs["Request_PIC"]);
                    Request_Status.SetDbValue(rs["Request_Status"]);
                    List_Approver.SetDbValue(rs["List_Approver"]);
                    Last_Update_By.SetDbValue(rs["Last_Update_By"]);
                    Created_By.SetDbValue(rs["Created_By"]);
                    Created_Date.SetDbValue(rs["Created_Date"]);
                    Request_Currency.SetDbValue(rs["Request_Currency"]);
                } else {
                    if (!CompareValue(id.DbValue, rs["id"]))
                        id.CurrentValue = DbNullValue;
                    if (!CompareValue(Request_No.DbValue, rs["Request_No"]))
                        Request_No.CurrentValue = DbNullValue;
                    if (!CompareValue(Reference_Doc.DbValue, rs["Reference_Doc"]))
                        Reference_Doc.CurrentValue = DbNullValue;
                    if (!CompareValue(Reason.DbValue, rs["Reason"]))
                        Reason.CurrentValue = DbNullValue;
                    if (!CompareValue(Request_By.DbValue, rs["Request_By"]))
                        Request_By.CurrentValue = DbNullValue;
                    if (!CompareValue(Request_By_Name.DbValue, rs["Request_By_Name"]))
                        Request_By_Name.CurrentValue = DbNullValue;
                    if (!CompareValue(Request_By_Position.DbValue, rs["Request_By_Position"]))
                        Request_By_Position.CurrentValue = DbNullValue;
                    if (!CompareValue(Request_By_Branch.DbValue, rs["Request_By_Branch"]))
                        Request_By_Branch.CurrentValue = DbNullValue;
                    if (!CompareValue(Request_By_Region.DbValue, rs["Request_By_Region"]))
                        Request_By_Region.CurrentValue = DbNullValue;
                    if (!CompareValue(Request_Industry.DbValue, rs["Request_Industry"]))
                        Request_Industry.CurrentValue = DbNullValue;
                    if (!CompareValue(Customer_ID.DbValue, rs["Customer_ID"]))
                        Customer_ID.CurrentValue = DbNullValue;
                    if (!CompareValue(Customer_Name.DbValue, rs["Customer_Name"]))
                        Customer_Name.CurrentValue = DbNullValue;
                    if (!CompareValue(SAP_Total.DbValue, rs["SAP_Total"]))
                        SAP_Total.CurrentValue = DbNullValue;
                    if (!CompareValue(Override_Total.DbValue, rs["Override_Total"]))
                        Override_Total.CurrentValue = DbNullValue;
                    if (!CompareValue(Variance_Total.DbValue, rs["Variance_Total"]))
                        Variance_Total.CurrentValue = DbNullValue;
                    if (!CompareValue(Variance_Percent.DbValue, rs["Variance_Percent"]))
                        Variance_Percent.CurrentValue = DbNullValue;
                    if (!CompareValue(Notes.DbValue, rs["Notes"]))
                        Notes.CurrentValue = DbNullValue;
                    if (!CompareValue(Next_Approver.DbValue, rs["Next_Approver"]))
                        Next_Approver.CurrentValue = DbNullValue;
                    if (!CompareValue(Request_PIC.DbValue, rs["Request_PIC"]))
                        Request_PIC.CurrentValue = DbNullValue;
                    if (!CompareValue(Request_Status.DbValue, rs["Request_Status"]))
                        Request_Status.CurrentValue = DbNullValue;
                    if (!CompareValue(List_Approver.DbValue, rs["List_Approver"]))
                        List_Approver.CurrentValue = DbNullValue;
                    if (!CompareValue(Last_Update_By.DbValue, rs["Last_Update_By"]))
                        Last_Update_By.CurrentValue = DbNullValue;
                    if (!CompareValue(Created_By.DbValue, rs["Created_By"]))
                        Created_By.CurrentValue = DbNullValue;
                    if (!CompareValue(Created_Date.DbValue, rs["Created_Date"]))
                        Created_Date.CurrentValue = DbNullValue;
                    if (!CompareValue(Request_Currency.DbValue, rs["Request_Currency"]))
                        Request_Currency.CurrentValue = DbNullValue;
                }
                i++;
            }
        } catch {
            if (Config.Debug)
                throw;
        }
    }

    // Set up key value
    public bool SetupKeyValues(object key)
    {
        string keyFld = "";
        keyFld = ConvertToString(key);
        if (!IsNumeric(keyFld))
            return false;
        id.OldValue = keyFld;
        return true;
    }

    // Update all selected rows
    public async Task<bool> UpdateRows()
    {
        bool result = false;
        List<string> successKeys = new ();
        List<string> failKeys = new ();
        string thisKey = "";
        string sql;
        List<Dictionary<string, object>> rsold, rsnew;
        if (UseTransaction)
            Connection.BeginTrans(); // Begin transaction

        // Get old recordset
        CurrentFilter = GetFilterFromRecordKeys(false);
        sql = CurrentSql;
        rsold = await Connection.GetRowsAsync(sql);

        // Update all rows
        try {
            foreach (string recordKey in RecordKeys) {
                if (SetupKeyValues(recordKey)) {
                    thisKey = recordKey;
                    SendEmail = false; // Do not send email on update success
                    UpdateCount++; // Update record count for records being updated
                    result = await EditRow(); // Update this row
                } else {
                    result = false;
                }
                if (!result) {
                    if (UseTransaction) { // Update failed
                        successKeys.Clear();
                        break;
                    }
                    failKeys.Add(thisKey);
                } else {
                    successKeys.Add(thisKey);
                }
            }
        } catch (Exception e) {
            FailureMessage = e.Message;
            result = false;
        }

        // Check if any rows updated
        if (successKeys.Count > 0) {
            if (UseTransaction)
                Connection.CommitTrans(); // Commit transaction

            // Set warning message if update some records failed
            if (failKeys.Count > 0)
                WarningMessage = Language.Phrase("UpdateSomeRecordsFailed").Replace("%k", String.Join(", ", failKeys));

            // Get new recordset
            rsnew = await Connection.GetRowsAsync(sql);
        } else {
            if (UseTransaction)
                Connection.RollbackTrans(); // Rollback transaction
        }
        return result;
    }

        // Confirm page
        public bool ConfirmPage = false; // DN

        #pragma warning disable 1998
        // Get upload files
        public async Task GetUploadFiles()
        {
            // Get upload data
        }
        #pragma warning restore 1998

        #pragma warning disable 1998
        // Load form values
        protected async Task LoadFormValues() {
            if (CurrentForm == null)
                return;
            bool validate = !Config.ServerValidate;
            string val;

            // Check field name 'id' before field var 'x_id'
            val = CurrentForm.HasValue("id") ? CurrentForm.GetValue("id") : CurrentForm.GetValue("x_id");
            if (!id.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("id") && !CurrentForm.HasValue("x_id")) // DN
                    id.Visible = false; // Disable update for API request
                else
                    id.SetFormValue(val, true, validate);
            }
            id.MultiUpdate = CurrentForm.GetValue("u_id");

            // Check field name 'Request_No' before field var 'x_Request_No'
            val = CurrentForm.HasValue("Request_No") ? CurrentForm.GetValue("Request_No") : CurrentForm.GetValue("x_Request_No");
            if (!Request_No.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_No") && !CurrentForm.HasValue("x_Request_No")) // DN
                    Request_No.Visible = false; // Disable update for API request
                else
                    Request_No.SetFormValue(val);
            }
            Request_No.MultiUpdate = CurrentForm.GetValue("u_Request_No");

            // Check field name 'Reference_Doc' before field var 'x_Reference_Doc'
            val = CurrentForm.HasValue("Reference_Doc") ? CurrentForm.GetValue("Reference_Doc") : CurrentForm.GetValue("x_Reference_Doc");
            if (!Reference_Doc.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Reference_Doc") && !CurrentForm.HasValue("x_Reference_Doc")) // DN
                    Reference_Doc.Visible = false; // Disable update for API request
                else
                    Reference_Doc.SetFormValue(val);
            }
            Reference_Doc.MultiUpdate = CurrentForm.GetValue("u_Reference_Doc");

            // Check field name 'Reason' before field var 'x_Reason'
            val = CurrentForm.HasValue("Reason") ? CurrentForm.GetValue("Reason") : CurrentForm.GetValue("x_Reason");
            if (!Reason.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Reason") && !CurrentForm.HasValue("x_Reason")) // DN
                    Reason.Visible = false; // Disable update for API request
                else
                    Reason.SetFormValue(val);
            }
            Reason.MultiUpdate = CurrentForm.GetValue("u_Reason");

            // Check field name 'Request_By' before field var 'x_Request_By'
            val = CurrentForm.HasValue("Request_By") ? CurrentForm.GetValue("Request_By") : CurrentForm.GetValue("x_Request_By");
            if (!Request_By.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_By") && !CurrentForm.HasValue("x_Request_By")) // DN
                    Request_By.Visible = false; // Disable update for API request
                else
                    Request_By.SetFormValue(val);
            }
            Request_By.MultiUpdate = CurrentForm.GetValue("u_Request_By");

            // Check field name 'Request_By_Name' before field var 'x_Request_By_Name'
            val = CurrentForm.HasValue("Request_By_Name") ? CurrentForm.GetValue("Request_By_Name") : CurrentForm.GetValue("x_Request_By_Name");
            if (!Request_By_Name.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_By_Name") && !CurrentForm.HasValue("x_Request_By_Name")) // DN
                    Request_By_Name.Visible = false; // Disable update for API request
                else
                    Request_By_Name.SetFormValue(val);
            }
            Request_By_Name.MultiUpdate = CurrentForm.GetValue("u_Request_By_Name");

            // Check field name 'Request_By_Position' before field var 'x_Request_By_Position'
            val = CurrentForm.HasValue("Request_By_Position") ? CurrentForm.GetValue("Request_By_Position") : CurrentForm.GetValue("x_Request_By_Position");
            if (!Request_By_Position.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_By_Position") && !CurrentForm.HasValue("x_Request_By_Position")) // DN
                    Request_By_Position.Visible = false; // Disable update for API request
                else
                    Request_By_Position.SetFormValue(val);
            }
            Request_By_Position.MultiUpdate = CurrentForm.GetValue("u_Request_By_Position");

            // Check field name 'Request_By_Branch' before field var 'x_Request_By_Branch'
            val = CurrentForm.HasValue("Request_By_Branch") ? CurrentForm.GetValue("Request_By_Branch") : CurrentForm.GetValue("x_Request_By_Branch");
            if (!Request_By_Branch.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_By_Branch") && !CurrentForm.HasValue("x_Request_By_Branch")) // DN
                    Request_By_Branch.Visible = false; // Disable update for API request
                else
                    Request_By_Branch.SetFormValue(val);
            }
            Request_By_Branch.MultiUpdate = CurrentForm.GetValue("u_Request_By_Branch");

            // Check field name 'Request_By_Region' before field var 'x_Request_By_Region'
            val = CurrentForm.HasValue("Request_By_Region") ? CurrentForm.GetValue("Request_By_Region") : CurrentForm.GetValue("x_Request_By_Region");
            if (!Request_By_Region.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_By_Region") && !CurrentForm.HasValue("x_Request_By_Region")) // DN
                    Request_By_Region.Visible = false; // Disable update for API request
                else
                    Request_By_Region.SetFormValue(val);
            }
            Request_By_Region.MultiUpdate = CurrentForm.GetValue("u_Request_By_Region");

            // Check field name 'Request_Industry' before field var 'x_Request_Industry'
            val = CurrentForm.HasValue("Request_Industry") ? CurrentForm.GetValue("Request_Industry") : CurrentForm.GetValue("x_Request_Industry");
            if (!Request_Industry.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_Industry") && !CurrentForm.HasValue("x_Request_Industry")) // DN
                    Request_Industry.Visible = false; // Disable update for API request
                else
                    Request_Industry.SetFormValue(val);
            }
            Request_Industry.MultiUpdate = CurrentForm.GetValue("u_Request_Industry");

            // Check field name 'Customer_ID' before field var 'x_Customer_ID'
            val = CurrentForm.HasValue("Customer_ID") ? CurrentForm.GetValue("Customer_ID") : CurrentForm.GetValue("x_Customer_ID");
            if (!Customer_ID.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Customer_ID") && !CurrentForm.HasValue("x_Customer_ID")) // DN
                    Customer_ID.Visible = false; // Disable update for API request
                else
                    Customer_ID.SetFormValue(val);
            }
            Customer_ID.MultiUpdate = CurrentForm.GetValue("u_Customer_ID");

            // Check field name 'Customer_Name' before field var 'x_Customer_Name'
            val = CurrentForm.HasValue("Customer_Name") ? CurrentForm.GetValue("Customer_Name") : CurrentForm.GetValue("x_Customer_Name");
            if (!Customer_Name.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Customer_Name") && !CurrentForm.HasValue("x_Customer_Name")) // DN
                    Customer_Name.Visible = false; // Disable update for API request
                else
                    Customer_Name.SetFormValue(val);
            }
            Customer_Name.MultiUpdate = CurrentForm.GetValue("u_Customer_Name");

            // Check field name 'SAP_Total' before field var 'x_SAP_Total'
            val = CurrentForm.HasValue("SAP_Total") ? CurrentForm.GetValue("SAP_Total") : CurrentForm.GetValue("x_SAP_Total");
            if (!SAP_Total.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("SAP_Total") && !CurrentForm.HasValue("x_SAP_Total")) // DN
                    SAP_Total.Visible = false; // Disable update for API request
                else
                    SAP_Total.SetFormValue(val, true, validate);
            }
            SAP_Total.MultiUpdate = CurrentForm.GetValue("u_SAP_Total");

            // Check field name 'Override_Total' before field var 'x_Override_Total'
            val = CurrentForm.HasValue("Override_Total") ? CurrentForm.GetValue("Override_Total") : CurrentForm.GetValue("x_Override_Total");
            if (!Override_Total.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Override_Total") && !CurrentForm.HasValue("x_Override_Total")) // DN
                    Override_Total.Visible = false; // Disable update for API request
                else
                    Override_Total.SetFormValue(val, true, validate);
            }
            Override_Total.MultiUpdate = CurrentForm.GetValue("u_Override_Total");

            // Check field name 'Variance_Total' before field var 'x_Variance_Total'
            val = CurrentForm.HasValue("Variance_Total") ? CurrentForm.GetValue("Variance_Total") : CurrentForm.GetValue("x_Variance_Total");
            if (!Variance_Total.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Variance_Total") && !CurrentForm.HasValue("x_Variance_Total")) // DN
                    Variance_Total.Visible = false; // Disable update for API request
                else
                    Variance_Total.SetFormValue(val, true, validate);
            }
            Variance_Total.MultiUpdate = CurrentForm.GetValue("u_Variance_Total");

            // Check field name 'Variance_Percent' before field var 'x_Variance_Percent'
            val = CurrentForm.HasValue("Variance_Percent") ? CurrentForm.GetValue("Variance_Percent") : CurrentForm.GetValue("x_Variance_Percent");
            if (!Variance_Percent.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Variance_Percent") && !CurrentForm.HasValue("x_Variance_Percent")) // DN
                    Variance_Percent.Visible = false; // Disable update for API request
                else
                    Variance_Percent.SetFormValue(val, true, validate);
            }
            Variance_Percent.MultiUpdate = CurrentForm.GetValue("u_Variance_Percent");

            // Check field name 'Notes' before field var 'x_Notes'
            val = CurrentForm.HasValue("Notes") ? CurrentForm.GetValue("Notes") : CurrentForm.GetValue("x_Notes");
            if (!Notes.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Notes") && !CurrentForm.HasValue("x_Notes")) // DN
                    Notes.Visible = false; // Disable update for API request
                else
                    Notes.SetFormValue(val);
            }
            Notes.MultiUpdate = CurrentForm.GetValue("u_Notes");

            // Check field name 'Next_Approver' before field var 'x_Next_Approver'
            val = CurrentForm.HasValue("Next_Approver") ? CurrentForm.GetValue("Next_Approver") : CurrentForm.GetValue("x_Next_Approver");
            if (!Next_Approver.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Next_Approver") && !CurrentForm.HasValue("x_Next_Approver")) // DN
                    Next_Approver.Visible = false; // Disable update for API request
                else
                    Next_Approver.SetFormValue(val);
            }
            Next_Approver.MultiUpdate = CurrentForm.GetValue("u_Next_Approver");

            // Check field name 'Request_PIC' before field var 'x_Request_PIC'
            val = CurrentForm.HasValue("Request_PIC") ? CurrentForm.GetValue("Request_PIC") : CurrentForm.GetValue("x_Request_PIC");
            if (!Request_PIC.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_PIC") && !CurrentForm.HasValue("x_Request_PIC")) // DN
                    Request_PIC.Visible = false; // Disable update for API request
                else
                    Request_PIC.SetFormValue(val);
            }
            Request_PIC.MultiUpdate = CurrentForm.GetValue("u_Request_PIC");

            // Check field name 'Request_Status' before field var 'x_Request_Status'
            val = CurrentForm.HasValue("Request_Status") ? CurrentForm.GetValue("Request_Status") : CurrentForm.GetValue("x_Request_Status");
            if (!Request_Status.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_Status") && !CurrentForm.HasValue("x_Request_Status")) // DN
                    Request_Status.Visible = false; // Disable update for API request
                else
                    Request_Status.SetFormValue(val);
            }
            Request_Status.MultiUpdate = CurrentForm.GetValue("u_Request_Status");

            // Check field name 'List_Approver' before field var 'x_List_Approver'
            val = CurrentForm.HasValue("List_Approver") ? CurrentForm.GetValue("List_Approver") : CurrentForm.GetValue("x_List_Approver");
            if (!List_Approver.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("List_Approver") && !CurrentForm.HasValue("x_List_Approver")) // DN
                    List_Approver.Visible = false; // Disable update for API request
                else
                    List_Approver.SetFormValue(val);
            }
            List_Approver.MultiUpdate = CurrentForm.GetValue("u_List_Approver");

            // Check field name 'Last_Update_By' before field var 'x_Last_Update_By'
            val = CurrentForm.HasValue("Last_Update_By") ? CurrentForm.GetValue("Last_Update_By") : CurrentForm.GetValue("x_Last_Update_By");
            if (!Last_Update_By.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Last_Update_By") && !CurrentForm.HasValue("x_Last_Update_By")) // DN
                    Last_Update_By.Visible = false; // Disable update for API request
                else
                    Last_Update_By.SetFormValue(val);
            }
            Last_Update_By.MultiUpdate = CurrentForm.GetValue("u_Last_Update_By");

            // Check field name 'Created_By' before field var 'x_Created_By'
            val = CurrentForm.HasValue("Created_By") ? CurrentForm.GetValue("Created_By") : CurrentForm.GetValue("x_Created_By");
            if (!Created_By.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Created_By") && !CurrentForm.HasValue("x_Created_By")) // DN
                    Created_By.Visible = false; // Disable update for API request
                else
                    Created_By.SetFormValue(val);
            }
            Created_By.MultiUpdate = CurrentForm.GetValue("u_Created_By");

            // Check field name 'Created_Date' before field var 'x_Created_Date'
            val = CurrentForm.HasValue("Created_Date") ? CurrentForm.GetValue("Created_Date") : CurrentForm.GetValue("x_Created_Date");
            if (!Created_Date.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Created_Date") && !CurrentForm.HasValue("x_Created_Date")) // DN
                    Created_Date.Visible = false; // Disable update for API request
                else
                    Created_Date.SetFormValue(val);
                Created_Date.CurrentValue = UnformatDateTime(Created_Date.CurrentValue, Created_Date.FormatPattern);
            }
            Created_Date.MultiUpdate = CurrentForm.GetValue("u_Created_Date");

            // Check field name 'Request_Currency' before field var 'x_Request_Currency'
            val = CurrentForm.HasValue("Request_Currency") ? CurrentForm.GetValue("Request_Currency") : CurrentForm.GetValue("x_Request_Currency");
            if (!Request_Currency.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_Currency") && !CurrentForm.HasValue("x_Request_Currency")) // DN
                    Request_Currency.Visible = false; // Disable update for API request
                else
                    Request_Currency.SetFormValue(val);
            }
            Request_Currency.MultiUpdate = CurrentForm.GetValue("u_Request_Currency");
        }
        #pragma warning restore 1998

        // Restore form values
        public void RestoreFormValues()
        {
            id.CurrentValue = id.FormValue;
            Request_No.CurrentValue = Request_No.FormValue;
            Reference_Doc.CurrentValue = Reference_Doc.FormValue;
            Reason.CurrentValue = Reason.FormValue;
            Request_By.CurrentValue = Request_By.FormValue;
            Request_By_Name.CurrentValue = Request_By_Name.FormValue;
            Request_By_Position.CurrentValue = Request_By_Position.FormValue;
            Request_By_Branch.CurrentValue = Request_By_Branch.FormValue;
            Request_By_Region.CurrentValue = Request_By_Region.FormValue;
            Request_Industry.CurrentValue = Request_Industry.FormValue;
            Customer_ID.CurrentValue = Customer_ID.FormValue;
            Customer_Name.CurrentValue = Customer_Name.FormValue;
            SAP_Total.CurrentValue = SAP_Total.FormValue;
            Override_Total.CurrentValue = Override_Total.FormValue;
            Variance_Total.CurrentValue = Variance_Total.FormValue;
            Variance_Percent.CurrentValue = Variance_Percent.FormValue;
            Notes.CurrentValue = Notes.FormValue;
            Next_Approver.CurrentValue = Next_Approver.FormValue;
            Request_PIC.CurrentValue = Request_PIC.FormValue;
            Request_Status.CurrentValue = Request_Status.FormValue;
            List_Approver.CurrentValue = List_Approver.FormValue;
            Last_Update_By.CurrentValue = Last_Update_By.FormValue;
            Created_By.CurrentValue = Created_By.FormValue;
            Created_Date.CurrentValue = Created_Date.FormValue;
            Created_Date.CurrentValue = UnformatDateTime(Created_Date.CurrentValue, Created_Date.FormatPattern);
            Request_Currency.CurrentValue = Request_Currency.FormValue;
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
            id.RowCssClass = "row";

            // Request_No
            Request_No.RowCssClass = "row";

            // Reference_Doc
            Reference_Doc.RowCssClass = "row";

            // Reason
            Reason.RowCssClass = "row";

            // Request_By
            Request_By.RowCssClass = "row";

            // Request_By_Name
            Request_By_Name.RowCssClass = "row";

            // Request_By_Position
            Request_By_Position.RowCssClass = "row";

            // Request_By_Branch
            Request_By_Branch.RowCssClass = "row";

            // Request_By_Region
            Request_By_Region.RowCssClass = "row";

            // Request_Industry
            Request_Industry.RowCssClass = "row";

            // Customer_ID
            Customer_ID.RowCssClass = "row";

            // Customer_Name
            Customer_Name.RowCssClass = "row";

            // SAP_Total
            SAP_Total.RowCssClass = "row";

            // Override_Total
            Override_Total.RowCssClass = "row";

            // Variance_Total
            Variance_Total.RowCssClass = "row";

            // Variance_Percent
            Variance_Percent.RowCssClass = "row";

            // Notes
            Notes.RowCssClass = "row";

            // Next_Approver
            Next_Approver.RowCssClass = "row";

            // Request_PIC
            Request_PIC.RowCssClass = "row";

            // Request_Status
            Request_Status.RowCssClass = "row";

            // List_Approver
            List_Approver.RowCssClass = "row";

            // Last_Update_By
            Last_Update_By.RowCssClass = "row";

            // Created_By
            Created_By.RowCssClass = "row";

            // Created_Date
            Created_Date.RowCssClass = "row";

            // ETL_Date
            ETL_Date.RowCssClass = "row";

            // Request_Currency
            Request_Currency.RowCssClass = "row";

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

                // Request_Currency
                Request_Currency.HrefValue = "";
                Request_Currency.TooltipValue = "";
            } else if (RowType == RowType.Edit) {
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
                    if (curVal == "") {
                        filterWrk = "0=1";
                    } else {
                        filterWrk = SearchFilter("[Reason_Type]", "=", Reason.CurrentValue, DataType.String, "");
                    }
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

                // Request_Currency
                Request_Currency.SetupEditAttributes();
                if (!Request_Currency.Raw)
                    Request_Currency.CurrentValue = HtmlDecode(Request_Currency.CurrentValue);
                Request_Currency.EditValue = HtmlEncode(Request_Currency.CurrentValue);
                Request_Currency.PlaceHolder = RemoveHtml(Request_Currency.Caption);

                // Edit refer script

                // id
                id.HrefValue = "";

                // Request_No
                Request_No.HrefValue = "";

                // Reference_Doc
                Reference_Doc.HrefValue = "";

                // Reason
                Reason.HrefValue = "";

                // Request_By
                Request_By.HrefValue = "";

                // Request_By_Name
                Request_By_Name.HrefValue = "";

                // Request_By_Position
                Request_By_Position.HrefValue = "";

                // Request_By_Branch
                Request_By_Branch.HrefValue = "";

                // Request_By_Region
                Request_By_Region.HrefValue = "";

                // Request_Industry
                Request_Industry.HrefValue = "";

                // Customer_ID
                Customer_ID.HrefValue = "";

                // Customer_Name
                Customer_Name.HrefValue = "";

                // SAP_Total
                SAP_Total.HrefValue = "";

                // Override_Total
                Override_Total.HrefValue = "";

                // Variance_Total
                Variance_Total.HrefValue = "";

                // Variance_Percent
                Variance_Percent.HrefValue = "";

                // Notes
                Notes.HrefValue = "";

                // Next_Approver
                Next_Approver.HrefValue = "";

                // Request_PIC
                Request_PIC.HrefValue = "";

                // Request_Status
                Request_Status.HrefValue = "";

                // List_Approver
                List_Approver.HrefValue = "";

                // Last_Update_By
                Last_Update_By.HrefValue = "";

                // Created_By
                Created_By.HrefValue = "";

                // Created_Date
                Created_Date.HrefValue = "";

                // Request_Currency
                Request_Currency.HrefValue = "";
            }
            if (RowType == RowType.Add || RowType == RowType.Edit || RowType == RowType.Search) // Add/Edit/Search row
                SetupFieldTitles();

            // Call Row Rendered event
            if (RowType != RowType.AggregateInit)
                RowRendered();
        }
        #pragma warning restore 1998

        #pragma warning disable 1998
        // Validate form
        protected async Task<bool> ValidateForm() {
            int updateCnt = 0;
            if (id.MultiUpdateSelected)
                updateCnt++;
            if (Request_No.MultiUpdateSelected)
                updateCnt++;
            if (Reference_Doc.MultiUpdateSelected)
                updateCnt++;
            if (Reason.MultiUpdateSelected)
                updateCnt++;
            if (Request_By.MultiUpdateSelected)
                updateCnt++;
            if (Request_By_Name.MultiUpdateSelected)
                updateCnt++;
            if (Request_By_Position.MultiUpdateSelected)
                updateCnt++;
            if (Request_By_Branch.MultiUpdateSelected)
                updateCnt++;
            if (Request_By_Region.MultiUpdateSelected)
                updateCnt++;
            if (Request_Industry.MultiUpdateSelected)
                updateCnt++;
            if (Customer_ID.MultiUpdateSelected)
                updateCnt++;
            if (Customer_Name.MultiUpdateSelected)
                updateCnt++;
            if (SAP_Total.MultiUpdateSelected)
                updateCnt++;
            if (Override_Total.MultiUpdateSelected)
                updateCnt++;
            if (Variance_Total.MultiUpdateSelected)
                updateCnt++;
            if (Variance_Percent.MultiUpdateSelected)
                updateCnt++;
            if (Notes.MultiUpdateSelected)
                updateCnt++;
            if (Next_Approver.MultiUpdateSelected)
                updateCnt++;
            if (Request_PIC.MultiUpdateSelected)
                updateCnt++;
            if (Request_Status.MultiUpdateSelected)
                updateCnt++;
            if (List_Approver.MultiUpdateSelected)
                updateCnt++;
            if (Last_Update_By.MultiUpdateSelected)
                updateCnt++;
            if (Created_By.MultiUpdateSelected)
                updateCnt++;
            if (Created_Date.MultiUpdateSelected)
                updateCnt++;
            if (Request_Currency.MultiUpdateSelected)
                updateCnt++;
            if (updateCnt == 0) {
                return false;
            }

            // Check if validation required
            if (!Config.ServerValidate)
                return true;
            bool validateForm = true;
            if (id.Required) {
                if (id.MultiUpdate != "" && !id.IsDetailKey && Empty(id.FormValue)) {
                    id.AddErrorMessage(ConvertToString(id.RequiredErrorMessage).Replace("%s", id.Caption));
                }
            }
            if (id.MultiUpdate != "") {
                if (!CheckInteger(id.FormValue)) {
                    id.AddErrorMessage(id.GetErrorMessage(false));
                }
            }
            if (Request_No.Required) {
                if (Request_No.MultiUpdate != "" && !Request_No.IsDetailKey && Empty(Request_No.FormValue)) {
                    Request_No.AddErrorMessage(ConvertToString(Request_No.RequiredErrorMessage).Replace("%s", Request_No.Caption));
                }
            }
            if (Reference_Doc.Required) {
                if (Reference_Doc.MultiUpdate != "" && !Reference_Doc.IsDetailKey && Empty(Reference_Doc.FormValue)) {
                    Reference_Doc.AddErrorMessage(ConvertToString(Reference_Doc.RequiredErrorMessage).Replace("%s", Reference_Doc.Caption));
                }
            }
            if (Reason.Required) {
                if (Reason.MultiUpdate != "" && !Reason.IsDetailKey && Empty(Reason.FormValue)) {
                    Reason.AddErrorMessage(ConvertToString(Reason.RequiredErrorMessage).Replace("%s", Reason.Caption));
                }
            }
            if (Request_By.Required) {
                if (Request_By.MultiUpdate != "" && !Request_By.IsDetailKey && Empty(Request_By.FormValue)) {
                    Request_By.AddErrorMessage(ConvertToString(Request_By.RequiredErrorMessage).Replace("%s", Request_By.Caption));
                }
            }
            if (Request_By_Name.Required) {
                if (Request_By_Name.MultiUpdate != "" && !Request_By_Name.IsDetailKey && Empty(Request_By_Name.FormValue)) {
                    Request_By_Name.AddErrorMessage(ConvertToString(Request_By_Name.RequiredErrorMessage).Replace("%s", Request_By_Name.Caption));
                }
            }
            if (Request_By_Position.Required) {
                if (Request_By_Position.MultiUpdate != "" && !Request_By_Position.IsDetailKey && Empty(Request_By_Position.FormValue)) {
                    Request_By_Position.AddErrorMessage(ConvertToString(Request_By_Position.RequiredErrorMessage).Replace("%s", Request_By_Position.Caption));
                }
            }
            if (Request_By_Branch.Required) {
                if (Request_By_Branch.MultiUpdate != "" && !Request_By_Branch.IsDetailKey && Empty(Request_By_Branch.FormValue)) {
                    Request_By_Branch.AddErrorMessage(ConvertToString(Request_By_Branch.RequiredErrorMessage).Replace("%s", Request_By_Branch.Caption));
                }
            }
            if (Request_By_Region.Required) {
                if (Request_By_Region.MultiUpdate != "" && !Request_By_Region.IsDetailKey && Empty(Request_By_Region.FormValue)) {
                    Request_By_Region.AddErrorMessage(ConvertToString(Request_By_Region.RequiredErrorMessage).Replace("%s", Request_By_Region.Caption));
                }
            }
            if (Request_Industry.Required) {
                if (Request_Industry.MultiUpdate != "" && !Request_Industry.IsDetailKey && Empty(Request_Industry.FormValue)) {
                    Request_Industry.AddErrorMessage(ConvertToString(Request_Industry.RequiredErrorMessage).Replace("%s", Request_Industry.Caption));
                }
            }
            if (Customer_ID.Required) {
                if (Customer_ID.MultiUpdate != "" && !Customer_ID.IsDetailKey && Empty(Customer_ID.FormValue)) {
                    Customer_ID.AddErrorMessage(ConvertToString(Customer_ID.RequiredErrorMessage).Replace("%s", Customer_ID.Caption));
                }
            }
            if (Customer_Name.Required) {
                if (Customer_Name.MultiUpdate != "" && !Customer_Name.IsDetailKey && Empty(Customer_Name.FormValue)) {
                    Customer_Name.AddErrorMessage(ConvertToString(Customer_Name.RequiredErrorMessage).Replace("%s", Customer_Name.Caption));
                }
            }
            if (SAP_Total.Required) {
                if (SAP_Total.MultiUpdate != "" && !SAP_Total.IsDetailKey && Empty(SAP_Total.FormValue)) {
                    SAP_Total.AddErrorMessage(ConvertToString(SAP_Total.RequiredErrorMessage).Replace("%s", SAP_Total.Caption));
                }
            }
            if (SAP_Total.MultiUpdate != "") {
                if (!CheckNumber(SAP_Total.FormValue)) {
                    SAP_Total.AddErrorMessage(SAP_Total.GetErrorMessage(false));
                }
            }
            if (Override_Total.Required) {
                if (Override_Total.MultiUpdate != "" && !Override_Total.IsDetailKey && Empty(Override_Total.FormValue)) {
                    Override_Total.AddErrorMessage(ConvertToString(Override_Total.RequiredErrorMessage).Replace("%s", Override_Total.Caption));
                }
            }
            if (Override_Total.MultiUpdate != "") {
                if (!CheckNumber(Override_Total.FormValue)) {
                    Override_Total.AddErrorMessage(Override_Total.GetErrorMessage(false));
                }
            }
            if (Variance_Total.Required) {
                if (Variance_Total.MultiUpdate != "" && !Variance_Total.IsDetailKey && Empty(Variance_Total.FormValue)) {
                    Variance_Total.AddErrorMessage(ConvertToString(Variance_Total.RequiredErrorMessage).Replace("%s", Variance_Total.Caption));
                }
            }
            if (Variance_Total.MultiUpdate != "") {
                if (!CheckNumber(Variance_Total.FormValue)) {
                    Variance_Total.AddErrorMessage(Variance_Total.GetErrorMessage(false));
                }
            }
            if (Variance_Percent.Required) {
                if (Variance_Percent.MultiUpdate != "" && !Variance_Percent.IsDetailKey && Empty(Variance_Percent.FormValue)) {
                    Variance_Percent.AddErrorMessage(ConvertToString(Variance_Percent.RequiredErrorMessage).Replace("%s", Variance_Percent.Caption));
                }
            }
            if (Variance_Percent.MultiUpdate != "") {
                if (!CheckNumber(Variance_Percent.FormValue)) {
                    Variance_Percent.AddErrorMessage(Variance_Percent.GetErrorMessage(false));
                }
            }
            if (Notes.Required) {
                if (Notes.MultiUpdate != "" && !Notes.IsDetailKey && Empty(Notes.FormValue)) {
                    Notes.AddErrorMessage(ConvertToString(Notes.RequiredErrorMessage).Replace("%s", Notes.Caption));
                }
            }
            if (Next_Approver.Required) {
                if (Next_Approver.MultiUpdate != "" && !Next_Approver.IsDetailKey && Empty(Next_Approver.FormValue)) {
                    Next_Approver.AddErrorMessage(ConvertToString(Next_Approver.RequiredErrorMessage).Replace("%s", Next_Approver.Caption));
                }
            }
            if (Request_PIC.Required) {
                if (Request_PIC.MultiUpdate != "" && !Request_PIC.IsDetailKey && Empty(Request_PIC.FormValue)) {
                    Request_PIC.AddErrorMessage(ConvertToString(Request_PIC.RequiredErrorMessage).Replace("%s", Request_PIC.Caption));
                }
            }
            if (Request_Status.Required) {
                if (Request_Status.MultiUpdate != "" && !Request_Status.IsDetailKey && Empty(Request_Status.FormValue)) {
                    Request_Status.AddErrorMessage(ConvertToString(Request_Status.RequiredErrorMessage).Replace("%s", Request_Status.Caption));
                }
            }
            if (List_Approver.Required) {
                if (List_Approver.MultiUpdate != "" && !List_Approver.IsDetailKey && Empty(List_Approver.FormValue)) {
                    List_Approver.AddErrorMessage(ConvertToString(List_Approver.RequiredErrorMessage).Replace("%s", List_Approver.Caption));
                }
            }
            if (Last_Update_By.Required) {
                if (Last_Update_By.MultiUpdate != "" && !Last_Update_By.IsDetailKey && Empty(Last_Update_By.FormValue)) {
                    Last_Update_By.AddErrorMessage(ConvertToString(Last_Update_By.RequiredErrorMessage).Replace("%s", Last_Update_By.Caption));
                }
            }
            if (Created_By.Required) {
                if (Created_By.MultiUpdate != "" && !Created_By.IsDetailKey && Empty(Created_By.FormValue)) {
                    Created_By.AddErrorMessage(ConvertToString(Created_By.RequiredErrorMessage).Replace("%s", Created_By.Caption));
                }
            }
            if (Created_Date.Required) {
                if (Created_Date.MultiUpdate != "" && !Created_Date.IsDetailKey && Empty(Created_Date.FormValue)) {
                    Created_Date.AddErrorMessage(ConvertToString(Created_Date.RequiredErrorMessage).Replace("%s", Created_Date.Caption));
                }
            }
            if (Request_Currency.Required) {
                if (Request_Currency.MultiUpdate != "" && !Request_Currency.IsDetailKey && Empty(Request_Currency.FormValue)) {
                    Request_Currency.AddErrorMessage(ConvertToString(Request_Currency.RequiredErrorMessage).Replace("%s", Request_Currency.Caption));
                }
            }

            // Return validate result
            validateForm = validateForm && !HasInvalidFields();

            // Call Form CustomValidate event
            string formCustomError = "";
            validateForm = validateForm && FormCustomValidate(ref formCustomError);
            if (!Empty(formCustomError))
                FailureMessage = formCustomError;
            return validateForm;
        }
        #pragma warning restore 1998

        // Update record based on key values
        #pragma warning disable 168, 219

        protected async Task<JsonBoolResult> EditRow() { // DN
            bool result = false;
            Dictionary<string, object> rsold;
            string oldKeyFilter = GetRecordFilter();
            string filter = ApplyUserIDFilters(oldKeyFilter);

            // Load old row
            CurrentFilter = filter;
            string sql = CurrentSql;
            try {
                using var rsedit = await Connection.GetDataReaderAsync(sql);
                if (rsedit == null || !await rsedit.ReadAsync()) {
                    FailureMessage = Language.Phrase("NoRecord"); // Set no record message
                    return JsonBoolResult.FalseResult;
                }
                rsold = Connection.GetRow(rsedit);
                LoadDbValues(rsold);
            } catch (Exception e) {
                if (Config.Debug)
                    throw;
                FailureMessage = e.Message;
                return JsonBoolResult.FalseResult;
            }

            // Set new row
            Dictionary<string, object> rsnew = new ();

            // id
            id.SetDbValue(rsnew, id.CurrentValue, id.ReadOnly || id.MultiUpdate != "1");

            // Request_No
            Request_No.SetDbValue(rsnew, Request_No.CurrentValue, Request_No.ReadOnly || Request_No.MultiUpdate != "1");

            // Reference_Doc
            Reference_Doc.SetDbValue(rsnew, Reference_Doc.CurrentValue, Reference_Doc.ReadOnly || Reference_Doc.MultiUpdate != "1");

            // Reason
            Reason.SetDbValue(rsnew, Reason.CurrentValue, Reason.ReadOnly || Reason.MultiUpdate != "1");

            // Request_By
            Request_By.CurrentValue = Request_By.GetAutoUpdateValue();
            Request_By.SetDbValue(rsnew, Request_By.CurrentValue, false);

            // Request_By_Name
            Request_By_Name.CurrentValue = Request_By_Name.GetAutoUpdateValue();
            Request_By_Name.SetDbValue(rsnew, Request_By_Name.CurrentValue, false);

            // Request_By_Position
            Request_By_Position.CurrentValue = Request_By_Position.GetAutoUpdateValue();
            Request_By_Position.SetDbValue(rsnew, Request_By_Position.CurrentValue, false);

            // Request_By_Branch
            Request_By_Branch.SetDbValue(rsnew, Request_By_Branch.CurrentValue, Request_By_Branch.ReadOnly || Request_By_Branch.MultiUpdate != "1");

            // Request_By_Region
            Request_By_Region.SetDbValue(rsnew, Request_By_Region.CurrentValue, Request_By_Region.ReadOnly || Request_By_Region.MultiUpdate != "1");

            // Request_Industry
            Request_Industry.SetDbValue(rsnew, Request_Industry.CurrentValue, Request_Industry.ReadOnly || Request_Industry.MultiUpdate != "1");

            // Customer_ID
            Customer_ID.SetDbValue(rsnew, Customer_ID.CurrentValue, Customer_ID.ReadOnly || Customer_ID.MultiUpdate != "1");

            // Customer_Name
            Customer_Name.SetDbValue(rsnew, Customer_Name.CurrentValue, Customer_Name.ReadOnly || Customer_Name.MultiUpdate != "1");

            // SAP_Total
            SAP_Total.SetDbValue(rsnew, SAP_Total.CurrentValue, SAP_Total.ReadOnly || SAP_Total.MultiUpdate != "1");

            // Override_Total
            Override_Total.SetDbValue(rsnew, Override_Total.CurrentValue, Override_Total.ReadOnly || Override_Total.MultiUpdate != "1");

            // Variance_Total
            Variance_Total.SetDbValue(rsnew, Variance_Total.CurrentValue, Variance_Total.ReadOnly || Variance_Total.MultiUpdate != "1");

            // Variance_Percent
            Variance_Percent.SetDbValue(rsnew, Variance_Percent.CurrentValue, Variance_Percent.ReadOnly || Variance_Percent.MultiUpdate != "1");

            // Notes
            Notes.SetDbValue(rsnew, Notes.CurrentValue, Notes.ReadOnly || Notes.MultiUpdate != "1");

            // Next_Approver
            Next_Approver.SetDbValue(rsnew, Next_Approver.CurrentValue, Next_Approver.ReadOnly || Next_Approver.MultiUpdate != "1");

            // Request_PIC
            Request_PIC.SetDbValue(rsnew, Request_PIC.CurrentValue, Request_PIC.ReadOnly || Request_PIC.MultiUpdate != "1");

            // Request_Status
            Request_Status.SetDbValue(rsnew, Request_Status.CurrentValue, Request_Status.ReadOnly || Request_Status.MultiUpdate != "1");

            // List_Approver
            List_Approver.SetDbValue(rsnew, List_Approver.CurrentValue, List_Approver.ReadOnly || List_Approver.MultiUpdate != "1");

            // Last_Update_By
            Last_Update_By.CurrentValue = Last_Update_By.GetAutoUpdateValue();
            Last_Update_By.SetDbValue(rsnew, Last_Update_By.CurrentValue, false);

            // Created_By
            Created_By.CurrentValue = Created_By.GetAutoUpdateValue();
            Created_By.SetDbValue(rsnew, Created_By.CurrentValue, false);

            // Created_Date
            Created_Date.CurrentValue = Created_Date.GetAutoUpdateValue();
            Created_Date.SetDbValue(rsnew, ConvertToDateTime(Created_Date.CurrentValue, Created_Date.FormatPattern), false);

            // Request_Currency
            Request_Currency.SetDbValue(rsnew, Request_Currency.CurrentValue, Request_Currency.ReadOnly || Request_Currency.MultiUpdate != "1");

            // Update current values
            SetCurrentValues(rsnew);

            // Check field with unique index (id)
            if (!Empty(id.CurrentValue)) {
                string filterChk = "([id] = " + AdjustSql(id.CurrentValue, DbId) + ")";
                filterChk = filterChk + " AND NOT (" + filter + ")";
                try {
                    using var rschk = await LoadReader(filterChk);
                    if (rschk?.HasRows ?? false) {
                        var idxErrMsg = Language.Phrase("DupIndex").Replace("%f", id.Caption);
                        idxErrMsg = idxErrMsg.Replace("%v", ConvertToString(id.CurrentValue));
                        FailureMessage = idxErrMsg;
                        return JsonBoolResult.FalseResult;
                    }
                } catch (Exception e) {
                    if (Config.Debug)
                        throw;
                    FailureMessage = e.Message;
                    return JsonBoolResult.FalseResult;
                }
            }

            // Call Row Updating event
            bool updateRow = RowUpdating(rsold, rsnew);

            // Check for duplicate key when key changed
            if (updateRow) {
                string newKeyFilter = GetRecordFilter(rsnew);
                if (newKeyFilter != oldKeyFilter) {
                    using var rsChk = await LoadReader(newKeyFilter);
                    if (rsChk?.HasRows ?? false) { // DN
                        FailureMessage = Language.Phrase("DupKey").Replace("%f", newKeyFilter);
                        updateRow = false;
                    }
                }
            }
            if (updateRow) {
                try {
                    if (rsnew.Count > 0)
                        result = await UpdateAsync(rsnew, null, rsold) > 0;
                    else
                        result = true;
                    if (result) {
                    }
                } catch (Exception e) {
                    if (Config.Debug)
                        throw;
                    FailureMessage = e.Message;
                    return JsonBoolResult.FalseResult;
                }
            } else {
                if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {
                    // Use the message, do nothing
                } else if (!Empty(CancelMessage)) {
                    FailureMessage = CancelMessage;
                    CancelMessage = "";
                } else {
                    FailureMessage = Language.Phrase("UpdateCancelled");
                }
                result = false;
            }

            // Call Row Updated event
            if (result)
                RowUpdated(rsold, rsnew);

            // Write JSON for API request
            Dictionary<string, object> d = new ();
            d.Add("success", result);
            if (IsJsonResponse() && result) {
                if (GetRecordFromDictionary(rsnew) is var row && row != null) {
                    string table = TableVar;
                    d.Add(table, row);
                }
                d.Add("action", Config.ApiEditAction);
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
            string pageId = "update";
            breadcrumb.Add("update", pageId, url);
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

        // Form Custom Validate event
        public virtual bool FormCustomValidate(ref string customError) {
            //Return error message in customError
            return true;
        }
    } // End page class
} // End Partial class
