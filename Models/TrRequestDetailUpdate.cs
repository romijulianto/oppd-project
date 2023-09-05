namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// trRequestDetailUpdate
    /// </summary>
    public static TrRequestDetailUpdate trRequestDetailUpdate
    {
        get => HttpData.Get<TrRequestDetailUpdate>("trRequestDetailUpdate")!;
        set => HttpData["trRequestDetailUpdate"] = value;
    }

    /// <summary>
    /// Page class for tr_request_detail
    /// </summary>
    public class TrRequestDetailUpdate : TrRequestDetailUpdateBase
    {
        // Constructor
        public TrRequestDetailUpdate(Controller controller) : base(controller)
        {
        }

        // Constructor
        public TrRequestDetailUpdate() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TrRequestDetailUpdateBase : TrRequestDetail
    {
        // Page ID
        public string PageID = "update";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "tr_request_detail";

        // Page object name
        public string PageObjName = "trRequestDetailUpdate";

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
        public TrRequestDetailUpdateBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-desktop-table ew-update-table";

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
        public string PageName => "TrRequestDetailUpdate";

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
            request_id.SetVisibility();
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
        public TrRequestDetailUpdateBase(Controller? controller = null): this() { // DN
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
                            result.Add("view", pageName == "TrRequestDetailView" ? "1" : "0"); // If View page, no primary button
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
            return Terminate("TrRequestDetailList"); // No records selected, return to list
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
                trRequestDetailUpdate?.PageRender();
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
                    Item_No.SetDbValue(rs["Item_No"]);
                    Part_No.SetDbValue(rs["Part_No"]);
                    Part_Description.SetDbValue(rs["Part_Description"]);
                    Qty.SetDbValue(rs["Qty"]);
                    SAP_Unit_Price.SetDbValue(rs["SAP_Unit_Price"]);
                    Extd_SAP_Price.SetDbValue(rs["Extd_SAP_Price"]);
                    GP_SAP_Price.SetDbValue(rs["GP_SAP_Price"]);
                    Override_Unit_Price.SetDbValue(rs["Override_Unit_Price"]);
                    Extd_Override_Price.SetDbValue(rs["Extd_Override_Price"]);
                    GP_Override_Price.SetDbValue(rs["GP_Override_Price"]);
                    Override_Core.SetDbValue(rs["Override_Core"]);
                    Override_Percent.SetDbValue(rs["Override_Percent"]);
                    Core_Life_Ind.SetDbValue(rs["Core_Life_Ind"]);
                    Notes.SetDbValue(rs["Notes"]);
                } else {
                    if (!CompareValue(id.DbValue, rs["id"]))
                        id.CurrentValue = DbNullValue;
                    if (!CompareValue(Item_No.DbValue, rs["Item_No"]))
                        Item_No.CurrentValue = DbNullValue;
                    if (!CompareValue(Part_No.DbValue, rs["Part_No"]))
                        Part_No.CurrentValue = DbNullValue;
                    if (!CompareValue(Part_Description.DbValue, rs["Part_Description"]))
                        Part_Description.CurrentValue = DbNullValue;
                    if (!CompareValue(Qty.DbValue, rs["Qty"]))
                        Qty.CurrentValue = DbNullValue;
                    if (!CompareValue(SAP_Unit_Price.DbValue, rs["SAP_Unit_Price"]))
                        SAP_Unit_Price.CurrentValue = DbNullValue;
                    if (!CompareValue(Extd_SAP_Price.DbValue, rs["Extd_SAP_Price"]))
                        Extd_SAP_Price.CurrentValue = DbNullValue;
                    if (!CompareValue(GP_SAP_Price.DbValue, rs["GP_SAP_Price"]))
                        GP_SAP_Price.CurrentValue = DbNullValue;
                    if (!CompareValue(Override_Unit_Price.DbValue, rs["Override_Unit_Price"]))
                        Override_Unit_Price.CurrentValue = DbNullValue;
                    if (!CompareValue(Extd_Override_Price.DbValue, rs["Extd_Override_Price"]))
                        Extd_Override_Price.CurrentValue = DbNullValue;
                    if (!CompareValue(GP_Override_Price.DbValue, rs["GP_Override_Price"]))
                        GP_Override_Price.CurrentValue = DbNullValue;
                    if (!CompareValue(Override_Core.DbValue, rs["Override_Core"]))
                        Override_Core.CurrentValue = DbNullValue;
                    if (!CompareValue(Override_Percent.DbValue, rs["Override_Percent"]))
                        Override_Percent.CurrentValue = DbNullValue;
                    if (!CompareValue(Core_Life_Ind.DbValue, rs["Core_Life_Ind"]))
                        Core_Life_Ind.CurrentValue = DbNullValue;
                    if (!CompareValue(Notes.DbValue, rs["Notes"]))
                        Notes.CurrentValue = DbNullValue;
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
            request_id.Upload.Index = CurrentForm.Index;
            if (!await request_id.Upload.UploadFile()) // DN
                End(request_id.Upload.Message);
            request_id.CurrentValue = request_id.Upload.FileName;
            request_id.MultiUpdate = CurrentForm.GetValue("u_request_id");
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
                    id.SetFormValue(val);
            }
            id.MultiUpdate = CurrentForm.GetValue("u_id");

            // Check field name 'Item_No' before field var 'x_Item_No'
            val = CurrentForm.HasValue("Item_No") ? CurrentForm.GetValue("Item_No") : CurrentForm.GetValue("x_Item_No");
            if (!Item_No.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Item_No") && !CurrentForm.HasValue("x_Item_No")) // DN
                    Item_No.Visible = false; // Disable update for API request
                else
                    Item_No.SetFormValue(val);
            }
            Item_No.MultiUpdate = CurrentForm.GetValue("u_Item_No");

            // Check field name 'Part_No' before field var 'x_Part_No'
            val = CurrentForm.HasValue("Part_No") ? CurrentForm.GetValue("Part_No") : CurrentForm.GetValue("x_Part_No");
            if (!Part_No.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Part_No") && !CurrentForm.HasValue("x_Part_No")) // DN
                    Part_No.Visible = false; // Disable update for API request
                else
                    Part_No.SetFormValue(val);
            }
            Part_No.MultiUpdate = CurrentForm.GetValue("u_Part_No");

            // Check field name 'Part_Description' before field var 'x_Part_Description'
            val = CurrentForm.HasValue("Part_Description") ? CurrentForm.GetValue("Part_Description") : CurrentForm.GetValue("x_Part_Description");
            if (!Part_Description.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Part_Description") && !CurrentForm.HasValue("x_Part_Description")) // DN
                    Part_Description.Visible = false; // Disable update for API request
                else
                    Part_Description.SetFormValue(val);
            }
            Part_Description.MultiUpdate = CurrentForm.GetValue("u_Part_Description");

            // Check field name 'Qty' before field var 'x_Qty'
            val = CurrentForm.HasValue("Qty") ? CurrentForm.GetValue("Qty") : CurrentForm.GetValue("x_Qty");
            if (!Qty.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Qty") && !CurrentForm.HasValue("x_Qty")) // DN
                    Qty.Visible = false; // Disable update for API request
                else
                    Qty.SetFormValue(val);
            }
            Qty.MultiUpdate = CurrentForm.GetValue("u_Qty");

            // Check field name 'SAP_Unit_Price' before field var 'x_SAP_Unit_Price'
            val = CurrentForm.HasValue("SAP_Unit_Price") ? CurrentForm.GetValue("SAP_Unit_Price") : CurrentForm.GetValue("x_SAP_Unit_Price");
            if (!SAP_Unit_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("SAP_Unit_Price") && !CurrentForm.HasValue("x_SAP_Unit_Price")) // DN
                    SAP_Unit_Price.Visible = false; // Disable update for API request
                else
                    SAP_Unit_Price.SetFormValue(val);
            }
            SAP_Unit_Price.MultiUpdate = CurrentForm.GetValue("u_SAP_Unit_Price");

            // Check field name 'Extd_SAP_Price' before field var 'x_Extd_SAP_Price'
            val = CurrentForm.HasValue("Extd_SAP_Price") ? CurrentForm.GetValue("Extd_SAP_Price") : CurrentForm.GetValue("x_Extd_SAP_Price");
            if (!Extd_SAP_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Extd_SAP_Price") && !CurrentForm.HasValue("x_Extd_SAP_Price")) // DN
                    Extd_SAP_Price.Visible = false; // Disable update for API request
                else
                    Extd_SAP_Price.SetFormValue(val);
            }
            Extd_SAP_Price.MultiUpdate = CurrentForm.GetValue("u_Extd_SAP_Price");

            // Check field name 'GP_SAP_Price' before field var 'x_GP_SAP_Price'
            val = CurrentForm.HasValue("GP_SAP_Price") ? CurrentForm.GetValue("GP_SAP_Price") : CurrentForm.GetValue("x_GP_SAP_Price");
            if (!GP_SAP_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("GP_SAP_Price") && !CurrentForm.HasValue("x_GP_SAP_Price")) // DN
                    GP_SAP_Price.Visible = false; // Disable update for API request
                else
                    GP_SAP_Price.SetFormValue(val);
            }
            GP_SAP_Price.MultiUpdate = CurrentForm.GetValue("u_GP_SAP_Price");

            // Check field name 'Override_Unit_Price' before field var 'x_Override_Unit_Price'
            val = CurrentForm.HasValue("Override_Unit_Price") ? CurrentForm.GetValue("Override_Unit_Price") : CurrentForm.GetValue("x_Override_Unit_Price");
            if (!Override_Unit_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Override_Unit_Price") && !CurrentForm.HasValue("x_Override_Unit_Price")) // DN
                    Override_Unit_Price.Visible = false; // Disable update for API request
                else
                    Override_Unit_Price.SetFormValue(val, true, validate);
            }
            Override_Unit_Price.MultiUpdate = CurrentForm.GetValue("u_Override_Unit_Price");

            // Check field name 'Extd_Override_Price' before field var 'x_Extd_Override_Price'
            val = CurrentForm.HasValue("Extd_Override_Price") ? CurrentForm.GetValue("Extd_Override_Price") : CurrentForm.GetValue("x_Extd_Override_Price");
            if (!Extd_Override_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Extd_Override_Price") && !CurrentForm.HasValue("x_Extd_Override_Price")) // DN
                    Extd_Override_Price.Visible = false; // Disable update for API request
                else
                    Extd_Override_Price.SetFormValue(val);
            }
            Extd_Override_Price.MultiUpdate = CurrentForm.GetValue("u_Extd_Override_Price");

            // Check field name 'GP_Override_Price' before field var 'x_GP_Override_Price'
            val = CurrentForm.HasValue("GP_Override_Price") ? CurrentForm.GetValue("GP_Override_Price") : CurrentForm.GetValue("x_GP_Override_Price");
            if (!GP_Override_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("GP_Override_Price") && !CurrentForm.HasValue("x_GP_Override_Price")) // DN
                    GP_Override_Price.Visible = false; // Disable update for API request
                else
                    GP_Override_Price.SetFormValue(val);
            }
            GP_Override_Price.MultiUpdate = CurrentForm.GetValue("u_GP_Override_Price");

            // Check field name 'Override_Core' before field var 'x_Override_Core'
            val = CurrentForm.HasValue("Override_Core") ? CurrentForm.GetValue("Override_Core") : CurrentForm.GetValue("x_Override_Core");
            if (!Override_Core.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Override_Core") && !CurrentForm.HasValue("x_Override_Core")) // DN
                    Override_Core.Visible = false; // Disable update for API request
                else
                    Override_Core.SetFormValue(val);
            }
            Override_Core.MultiUpdate = CurrentForm.GetValue("u_Override_Core");

            // Check field name 'Override_Percent' before field var 'x_Override_Percent'
            val = CurrentForm.HasValue("Override_Percent") ? CurrentForm.GetValue("Override_Percent") : CurrentForm.GetValue("x_Override_Percent");
            if (!Override_Percent.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Override_Percent") && !CurrentForm.HasValue("x_Override_Percent")) // DN
                    Override_Percent.Visible = false; // Disable update for API request
                else
                    Override_Percent.SetFormValue(val);
            }
            Override_Percent.MultiUpdate = CurrentForm.GetValue("u_Override_Percent");

            // Check field name 'Core_Life_Ind' before field var 'x_Core_Life_Ind'
            val = CurrentForm.HasValue("Core_Life_Ind") ? CurrentForm.GetValue("Core_Life_Ind") : CurrentForm.GetValue("x_Core_Life_Ind");
            if (!Core_Life_Ind.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Core_Life_Ind") && !CurrentForm.HasValue("x_Core_Life_Ind")) // DN
                    Core_Life_Ind.Visible = false; // Disable update for API request
                else
                    Core_Life_Ind.SetFormValue(val);
            }
            Core_Life_Ind.MultiUpdate = CurrentForm.GetValue("u_Core_Life_Ind");

            // Check field name 'Notes' before field var 'x_Notes'
            val = CurrentForm.HasValue("Notes") ? CurrentForm.GetValue("Notes") : CurrentForm.GetValue("x_Notes");
            if (!Notes.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Notes") && !CurrentForm.HasValue("x_Notes")) // DN
                    Notes.Visible = false; // Disable update for API request
                else
                    Notes.SetFormValue(val);
            }
            Notes.MultiUpdate = CurrentForm.GetValue("u_Notes");
            await GetUploadFiles(); // Get upload files
        }
        #pragma warning restore 1998

        // Restore form values
        public void RestoreFormValues()
        {
            id.CurrentValue = id.FormValue;
            Item_No.CurrentValue = Item_No.FormValue;
            Part_No.CurrentValue = Part_No.FormValue;
            Part_Description.CurrentValue = Part_Description.FormValue;
            Qty.CurrentValue = Qty.FormValue;
            SAP_Unit_Price.CurrentValue = SAP_Unit_Price.FormValue;
            Extd_SAP_Price.CurrentValue = Extd_SAP_Price.FormValue;
            GP_SAP_Price.CurrentValue = GP_SAP_Price.FormValue;
            Override_Unit_Price.CurrentValue = Override_Unit_Price.FormValue;
            Extd_Override_Price.CurrentValue = Extd_Override_Price.FormValue;
            GP_Override_Price.CurrentValue = GP_Override_Price.FormValue;
            Override_Core.CurrentValue = Override_Core.FormValue;
            Override_Percent.CurrentValue = Override_Percent.FormValue;
            Core_Life_Ind.CurrentValue = Core_Life_Ind.FormValue;
            Notes.CurrentValue = Notes.FormValue;
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
            id.RowCssClass = "row";

            // request_id
            request_id.RowCssClass = "row";

            // Item_No
            Item_No.RowCssClass = "row";

            // Part_No
            Part_No.RowCssClass = "row";

            // Part_Description
            Part_Description.RowCssClass = "row";

            // Qty
            Qty.RowCssClass = "row";

            // SAP_Unit_Price
            SAP_Unit_Price.RowCssClass = "row";

            // Extd_SAP_Price
            Extd_SAP_Price.RowCssClass = "row";

            // GP_SAP_Price
            GP_SAP_Price.RowCssClass = "row";

            // Override_Unit_Price
            Override_Unit_Price.RowCssClass = "row";

            // Extd_Override_Price
            Extd_Override_Price.RowCssClass = "row";

            // GP_Override_Price
            GP_Override_Price.RowCssClass = "row";

            // Override_Core
            Override_Core.RowCssClass = "row";

            // Override_Percent
            Override_Percent.RowCssClass = "row";

            // Core_Life_Ind
            Core_Life_Ind.RowCssClass = "row";

            // Notes
            Notes.RowCssClass = "row";

            // View row
            if (RowType == RowType.View) {
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
            } else if (RowType == RowType.Edit) {
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

                // Edit refer script

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
            if (request_id.MultiUpdateSelected)
                updateCnt++;
            if (Item_No.MultiUpdateSelected)
                updateCnt++;
            if (Part_No.MultiUpdateSelected)
                updateCnt++;
            if (Part_Description.MultiUpdateSelected)
                updateCnt++;
            if (Qty.MultiUpdateSelected)
                updateCnt++;
            if (SAP_Unit_Price.MultiUpdateSelected)
                updateCnt++;
            if (Extd_SAP_Price.MultiUpdateSelected)
                updateCnt++;
            if (GP_SAP_Price.MultiUpdateSelected)
                updateCnt++;
            if (Override_Unit_Price.MultiUpdateSelected)
                updateCnt++;
            if (Extd_Override_Price.MultiUpdateSelected)
                updateCnt++;
            if (GP_Override_Price.MultiUpdateSelected)
                updateCnt++;
            if (Override_Core.MultiUpdateSelected)
                updateCnt++;
            if (Override_Percent.MultiUpdateSelected)
                updateCnt++;
            if (Core_Life_Ind.MultiUpdateSelected)
                updateCnt++;
            if (Notes.MultiUpdateSelected)
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
            if (request_id.Required) {
                if (request_id.MultiUpdate != "" && request_id.Upload.FileName == "" && !request_id.Upload.KeepFile) {
                    request_id.AddErrorMessage(ConvertToString(request_id.RequiredErrorMessage).Replace("%s", request_id.Caption));
                }
            }
            if (Item_No.Required) {
                if (Item_No.MultiUpdate != "" && !Item_No.IsDetailKey && Empty(Item_No.FormValue)) {
                    Item_No.AddErrorMessage(ConvertToString(Item_No.RequiredErrorMessage).Replace("%s", Item_No.Caption));
                }
            }
            if (Part_No.Required) {
                if (Part_No.MultiUpdate != "" && !Part_No.IsDetailKey && Empty(Part_No.FormValue)) {
                    Part_No.AddErrorMessage(ConvertToString(Part_No.RequiredErrorMessage).Replace("%s", Part_No.Caption));
                }
            }
            if (Part_Description.Required) {
                if (Part_Description.MultiUpdate != "" && !Part_Description.IsDetailKey && Empty(Part_Description.FormValue)) {
                    Part_Description.AddErrorMessage(ConvertToString(Part_Description.RequiredErrorMessage).Replace("%s", Part_Description.Caption));
                }
            }
            if (Qty.Required) {
                if (Qty.MultiUpdate != "" && !Qty.IsDetailKey && Empty(Qty.FormValue)) {
                    Qty.AddErrorMessage(ConvertToString(Qty.RequiredErrorMessage).Replace("%s", Qty.Caption));
                }
            }
            if (SAP_Unit_Price.Required) {
                if (SAP_Unit_Price.MultiUpdate != "" && !SAP_Unit_Price.IsDetailKey && Empty(SAP_Unit_Price.FormValue)) {
                    SAP_Unit_Price.AddErrorMessage(ConvertToString(SAP_Unit_Price.RequiredErrorMessage).Replace("%s", SAP_Unit_Price.Caption));
                }
            }
            if (Extd_SAP_Price.Required) {
                if (Extd_SAP_Price.MultiUpdate != "" && !Extd_SAP_Price.IsDetailKey && Empty(Extd_SAP_Price.FormValue)) {
                    Extd_SAP_Price.AddErrorMessage(ConvertToString(Extd_SAP_Price.RequiredErrorMessage).Replace("%s", Extd_SAP_Price.Caption));
                }
            }
            if (GP_SAP_Price.Required) {
                if (GP_SAP_Price.MultiUpdate != "" && !GP_SAP_Price.IsDetailKey && Empty(GP_SAP_Price.FormValue)) {
                    GP_SAP_Price.AddErrorMessage(ConvertToString(GP_SAP_Price.RequiredErrorMessage).Replace("%s", GP_SAP_Price.Caption));
                }
            }
            if (Override_Unit_Price.Required) {
                if (Override_Unit_Price.MultiUpdate != "" && !Override_Unit_Price.IsDetailKey && Empty(Override_Unit_Price.FormValue)) {
                    Override_Unit_Price.AddErrorMessage(ConvertToString(Override_Unit_Price.RequiredErrorMessage).Replace("%s", Override_Unit_Price.Caption));
                }
            }
            if (Override_Unit_Price.MultiUpdate != "") {
                if (!CheckNumber(Override_Unit_Price.FormValue)) {
                    Override_Unit_Price.AddErrorMessage(Override_Unit_Price.GetErrorMessage(false));
                }
            }
            if (Extd_Override_Price.Required) {
                if (Extd_Override_Price.MultiUpdate != "" && !Extd_Override_Price.IsDetailKey && Empty(Extd_Override_Price.FormValue)) {
                    Extd_Override_Price.AddErrorMessage(ConvertToString(Extd_Override_Price.RequiredErrorMessage).Replace("%s", Extd_Override_Price.Caption));
                }
            }
            if (GP_Override_Price.Required) {
                if (GP_Override_Price.MultiUpdate != "" && !GP_Override_Price.IsDetailKey && Empty(GP_Override_Price.FormValue)) {
                    GP_Override_Price.AddErrorMessage(ConvertToString(GP_Override_Price.RequiredErrorMessage).Replace("%s", GP_Override_Price.Caption));
                }
            }
            if (Override_Core.Required) {
                if (Override_Core.MultiUpdate != "" && !Override_Core.IsDetailKey && Empty(Override_Core.FormValue)) {
                    Override_Core.AddErrorMessage(ConvertToString(Override_Core.RequiredErrorMessage).Replace("%s", Override_Core.Caption));
                }
            }
            if (Override_Percent.Required) {
                if (Override_Percent.MultiUpdate != "" && !Override_Percent.IsDetailKey && Empty(Override_Percent.FormValue)) {
                    Override_Percent.AddErrorMessage(ConvertToString(Override_Percent.RequiredErrorMessage).Replace("%s", Override_Percent.Caption));
                }
            }
            if (Core_Life_Ind.Required) {
                if (Core_Life_Ind.MultiUpdate != "" && !Core_Life_Ind.IsDetailKey && Empty(Core_Life_Ind.FormValue)) {
                    Core_Life_Ind.AddErrorMessage(ConvertToString(Core_Life_Ind.RequiredErrorMessage).Replace("%s", Core_Life_Ind.Caption));
                }
            }
            if (Notes.Required) {
                if (Notes.MultiUpdate != "" && !Notes.IsDetailKey && Empty(Notes.FormValue)) {
                    Notes.AddErrorMessage(ConvertToString(Notes.RequiredErrorMessage).Replace("%s", Notes.Caption));
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

            // Override_Unit_Price
            Override_Unit_Price.SetDbValue(rsnew, Override_Unit_Price.CurrentValue, Override_Unit_Price.ReadOnly || Override_Unit_Price.MultiUpdate != "1");

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
            bool validMasterRecord;
            object keyValue;
            object? v;
            string? masterFilter;
            Dictionary<string, object?> detailKeys;

            // Check referential integrity for master table 'tr_request'
            detailKeys = new ();
            keyValue = rsnew.TryGetValue("request_id", out v) ? v : rsold["request_id"];
            detailKeys.Add("request_id", keyValue);
            masterFilter = MasterFilter(trRequest, detailKeys); // DN
            if (!Empty(masterFilter) && trRequest != null) {
                using var rsmaster = await Connection.GetDataReaderAsync(trRequest.GetSql(masterFilter)); // DN
                validMasterRecord = rsmaster?.HasRows ?? false;
            } else { // Allow null value if not required field
                validMasterRecord = masterFilter == null;
            }
            if (!validMasterRecord) {
                FailureMessage = Language.Phrase("RelatedRecordRequired").Replace("%t", "tr_request");
                return JsonBoolResult.FalseResult;
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
            breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("TrRequestDetailList")), "", TableVar, true);
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
