namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// trRequestAdd
    /// </summary>
    public static TrRequestAdd trRequestAdd
    {
        get => HttpData.Get<TrRequestAdd>("trRequestAdd")!;
        set => HttpData["trRequestAdd"] = value;
    }

    /// <summary>
    /// Page class for tr_request
    /// </summary>
    public class TrRequestAdd : TrRequestAddBase
    {
        // Constructor
        public TrRequestAdd(Controller controller) : base(controller)
        {
        }

        // Constructor
        public TrRequestAdd() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TrRequestAddBase : TrRequest
    {
        // Page ID
        public string PageID = "add";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "tr_request";

        // Page object name
        public string PageObjName = "trRequestAdd";

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
        public TrRequestAddBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-desktop-table ew-add-table";

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
        public string PageName => "TrRequestAdd";

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
            Request_By.Visible = false;
            Request_By_Name.Visible = false;
            Request_By_Position.Visible = false;
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
            Created_By.Visible = false;
            Created_Date.Visible = false;
            ETL_Date.Visible = false;
            Request_Currency.SetVisibility();
        }

        // Constructor
        public TrRequestAddBase(Controller? controller = null): this() { // DN
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

        // Properties
        public string FormClassName = "ew-form ew-add-form";

        public bool IsModal = false;

        public bool IsMobileOrModal = false;

        public string DbMasterFilter = "";

        public string DbDetailFilter = "";

        public int StartRecord;

        public DbDataReader? Recordset = null; // Reserved // DN

        public bool CopyRecord;

        public SubPages? MultiPages; // Multi-Page object

        public SubPages? DetailPages; // Detail pages object

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

            // Set up multi page object
            SetupMultiPages();

            // Set up detail page object
            SetupDetailPages();

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

            // Load default values for add
            LoadDefaultValues();

            // Check modal
            if (IsModal)
                SkipHeaderFooter = true;
            IsMobileOrModal = IsMobile() || IsModal;
            bool postBack = false;
            StringValues sv;

            // Set up current action
            if (IsApi()) {
                CurrentAction = "insert"; // Add record directly
                postBack = true;
            } else if (!Empty(Post("action"))) {
                CurrentAction = Post("action"); // Get form action
                if (Post(OldKeyName, out sv))
                    SetKey(sv.ToString());
                postBack = true;
            } else {
                // Load key from QueryString
                string[] keyValues = {};
                object? rv;
                if (RouteValues.TryGetValue("key", out object? k))
                    keyValues = ConvertToString(k).Split('/'); // For Copy page
                if (RouteValues.TryGetValue("id", out rv)) { // DN
                    id.QueryValue = UrlDecode(rv); // DN
                } else if (Get("id", out sv)) {
                    id.QueryValue = sv.ToString();
                }
                OldKey = GetKey(true); // Get from CurrentValue
                CopyRecord = !Empty(OldKey);
                if (CopyRecord) {
                    CurrentAction = "copy"; // Copy record
                    SetKey(OldKey); // Set up record key
                } else {
                    CurrentAction = "show"; // Display blank record
                }
            }

            // Load old record / default values
            var rsold = await LoadOldRecord();

            // Load form values
            if (postBack) {
                await LoadFormValues(); // Load form values
            }

            // Set up detail parameters
            SetupDetailParms();

            // Validate form if post back
            if (postBack) {
                if (!await ValidateForm()) {
                    EventCancelled = true; // Event cancelled
                    RestoreFormValues(); // Restore form values
                    if (IsApi())
                        return Terminate();
                    else
                        CurrentAction = "show"; // Form error, reset action
                }
            }

            // Perform current action
            switch (CurrentAction) {
                case "copy": // Copy an existing record
                    if (rsold == null) { // Record not loaded
                        if (Empty(FailureMessage))
                            FailureMessage = Language.Phrase("NoRecord"); // No record found
                        return Terminate("TrRequestList"); // No matching record, return to List page // DN
                    }

                    // Set up detail parameters
                    SetupDetailParms();
                    break;
                case "insert": // Add new record // DN
                    SendEmail = true; // Send email on add success
                    var res = await AddRow(rsold);
                    if (res) { // Add successful
                        if (Empty(SuccessMessage) && Post("addopt") != "1") // Skip success message for addopt (done in JavaScript)
                            SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
                        string returnUrl = "";
                        returnUrl = "TrRequestList";
                        if (GetPageName(returnUrl) == "TrRequestList")
                            returnUrl = AddMasterUrl(ListUrl); // List page, return to List page with correct master key if necessary
                        else if (GetPageName(returnUrl) == "TrRequestView")
                            returnUrl = ViewUrl; // View page, return to View page with key URL directly

                        // Handle UseAjaxActions
                        if (IsModal && UseAjaxActions) {
                            IsModal = false;
                            if (GetPageName(returnUrl) != "TrRequestList") {
                                TempData["Return-Url"] = returnUrl; // Save return URL
                                returnUrl = "TrRequestList"; // Return list page content
                            }
                        }
                        if (IsJsonResponse()) { // Return to caller
                            ClearMessages(); // Clear messages for Json response
                            return res;
                        } else {
                            return Terminate(returnUrl);
                        }
                    } else if (IsApi()) { // API request, return
                        return Terminate();
                    } else {
                        EventCancelled = true; // Event cancelled
                        RestoreFormValues(); // Add failed, restore form values

                        // Set up detail parameters
                        SetupDetailParms();
                    }
                    break;
            }

            // Set up Breadcrumb
            SetupBreadcrumb();

            // Render row based on row type
            RowType = RowType.Add; // Render add type

            // Render row
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
                trRequestAdd?.PageRender();
            }
            return PageResult();
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

        // Load default values
        protected void LoadDefaultValues() {
        }

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

            // Check field name 'Request_No' before field var 'x_Request_No'
            val = CurrentForm.HasValue("Request_No") ? CurrentForm.GetValue("Request_No") : CurrentForm.GetValue("x_Request_No");
            if (!Request_No.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_No") && !CurrentForm.HasValue("x_Request_No")) // DN
                    Request_No.Visible = false; // Disable update for API request
                else
                    Request_No.SetFormValue(val);
            }

            // Check field name 'Reference_Doc' before field var 'x_Reference_Doc'
            val = CurrentForm.HasValue("Reference_Doc") ? CurrentForm.GetValue("Reference_Doc") : CurrentForm.GetValue("x_Reference_Doc");
            if (!Reference_Doc.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Reference_Doc") && !CurrentForm.HasValue("x_Reference_Doc")) // DN
                    Reference_Doc.Visible = false; // Disable update for API request
                else
                    Reference_Doc.SetFormValue(val);
            }

            // Check field name 'Reason' before field var 'x_Reason'
            val = CurrentForm.HasValue("Reason") ? CurrentForm.GetValue("Reason") : CurrentForm.GetValue("x_Reason");
            if (!Reason.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Reason") && !CurrentForm.HasValue("x_Reason")) // DN
                    Reason.Visible = false; // Disable update for API request
                else
                    Reason.SetFormValue(val);
            }

            // Check field name 'Request_By_Branch' before field var 'x_Request_By_Branch'
            val = CurrentForm.HasValue("Request_By_Branch") ? CurrentForm.GetValue("Request_By_Branch") : CurrentForm.GetValue("x_Request_By_Branch");
            if (!Request_By_Branch.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_By_Branch") && !CurrentForm.HasValue("x_Request_By_Branch")) // DN
                    Request_By_Branch.Visible = false; // Disable update for API request
                else
                    Request_By_Branch.SetFormValue(val);
            }

            // Check field name 'Request_By_Region' before field var 'x_Request_By_Region'
            val = CurrentForm.HasValue("Request_By_Region") ? CurrentForm.GetValue("Request_By_Region") : CurrentForm.GetValue("x_Request_By_Region");
            if (!Request_By_Region.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_By_Region") && !CurrentForm.HasValue("x_Request_By_Region")) // DN
                    Request_By_Region.Visible = false; // Disable update for API request
                else
                    Request_By_Region.SetFormValue(val);
            }

            // Check field name 'Request_Industry' before field var 'x_Request_Industry'
            val = CurrentForm.HasValue("Request_Industry") ? CurrentForm.GetValue("Request_Industry") : CurrentForm.GetValue("x_Request_Industry");
            if (!Request_Industry.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_Industry") && !CurrentForm.HasValue("x_Request_Industry")) // DN
                    Request_Industry.Visible = false; // Disable update for API request
                else
                    Request_Industry.SetFormValue(val);
            }

            // Check field name 'Customer_ID' before field var 'x_Customer_ID'
            val = CurrentForm.HasValue("Customer_ID") ? CurrentForm.GetValue("Customer_ID") : CurrentForm.GetValue("x_Customer_ID");
            if (!Customer_ID.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Customer_ID") && !CurrentForm.HasValue("x_Customer_ID")) // DN
                    Customer_ID.Visible = false; // Disable update for API request
                else
                    Customer_ID.SetFormValue(val);
            }

            // Check field name 'Customer_Name' before field var 'x_Customer_Name'
            val = CurrentForm.HasValue("Customer_Name") ? CurrentForm.GetValue("Customer_Name") : CurrentForm.GetValue("x_Customer_Name");
            if (!Customer_Name.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Customer_Name") && !CurrentForm.HasValue("x_Customer_Name")) // DN
                    Customer_Name.Visible = false; // Disable update for API request
                else
                    Customer_Name.SetFormValue(val);
            }

            // Check field name 'SAP_Total' before field var 'x_SAP_Total'
            val = CurrentForm.HasValue("SAP_Total") ? CurrentForm.GetValue("SAP_Total") : CurrentForm.GetValue("x_SAP_Total");
            if (!SAP_Total.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("SAP_Total") && !CurrentForm.HasValue("x_SAP_Total")) // DN
                    SAP_Total.Visible = false; // Disable update for API request
                else
                    SAP_Total.SetFormValue(val, true, validate);
            }

            // Check field name 'Override_Total' before field var 'x_Override_Total'
            val = CurrentForm.HasValue("Override_Total") ? CurrentForm.GetValue("Override_Total") : CurrentForm.GetValue("x_Override_Total");
            if (!Override_Total.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Override_Total") && !CurrentForm.HasValue("x_Override_Total")) // DN
                    Override_Total.Visible = false; // Disable update for API request
                else
                    Override_Total.SetFormValue(val, true, validate);
            }

            // Check field name 'Variance_Total' before field var 'x_Variance_Total'
            val = CurrentForm.HasValue("Variance_Total") ? CurrentForm.GetValue("Variance_Total") : CurrentForm.GetValue("x_Variance_Total");
            if (!Variance_Total.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Variance_Total") && !CurrentForm.HasValue("x_Variance_Total")) // DN
                    Variance_Total.Visible = false; // Disable update for API request
                else
                    Variance_Total.SetFormValue(val, true, validate);
            }

            // Check field name 'Variance_Percent' before field var 'x_Variance_Percent'
            val = CurrentForm.HasValue("Variance_Percent") ? CurrentForm.GetValue("Variance_Percent") : CurrentForm.GetValue("x_Variance_Percent");
            if (!Variance_Percent.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Variance_Percent") && !CurrentForm.HasValue("x_Variance_Percent")) // DN
                    Variance_Percent.Visible = false; // Disable update for API request
                else
                    Variance_Percent.SetFormValue(val, true, validate);
            }

            // Check field name 'Notes' before field var 'x_Notes'
            val = CurrentForm.HasValue("Notes") ? CurrentForm.GetValue("Notes") : CurrentForm.GetValue("x_Notes");
            if (!Notes.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Notes") && !CurrentForm.HasValue("x_Notes")) // DN
                    Notes.Visible = false; // Disable update for API request
                else
                    Notes.SetFormValue(val);
            }

            // Check field name 'Next_Approver' before field var 'x_Next_Approver'
            val = CurrentForm.HasValue("Next_Approver") ? CurrentForm.GetValue("Next_Approver") : CurrentForm.GetValue("x_Next_Approver");
            if (!Next_Approver.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Next_Approver") && !CurrentForm.HasValue("x_Next_Approver")) // DN
                    Next_Approver.Visible = false; // Disable update for API request
                else
                    Next_Approver.SetFormValue(val);
            }

            // Check field name 'Request_PIC' before field var 'x_Request_PIC'
            val = CurrentForm.HasValue("Request_PIC") ? CurrentForm.GetValue("Request_PIC") : CurrentForm.GetValue("x_Request_PIC");
            if (!Request_PIC.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_PIC") && !CurrentForm.HasValue("x_Request_PIC")) // DN
                    Request_PIC.Visible = false; // Disable update for API request
                else
                    Request_PIC.SetFormValue(val);
            }

            // Check field name 'Request_Status' before field var 'x_Request_Status'
            val = CurrentForm.HasValue("Request_Status") ? CurrentForm.GetValue("Request_Status") : CurrentForm.GetValue("x_Request_Status");
            if (!Request_Status.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_Status") && !CurrentForm.HasValue("x_Request_Status")) // DN
                    Request_Status.Visible = false; // Disable update for API request
                else
                    Request_Status.SetFormValue(val);
            }

            // Check field name 'List_Approver' before field var 'x_List_Approver'
            val = CurrentForm.HasValue("List_Approver") ? CurrentForm.GetValue("List_Approver") : CurrentForm.GetValue("x_List_Approver");
            if (!List_Approver.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("List_Approver") && !CurrentForm.HasValue("x_List_Approver")) // DN
                    List_Approver.Visible = false; // Disable update for API request
                else
                    List_Approver.SetFormValue(val);
            }

            // Check field name 'Last_Update_By' before field var 'x_Last_Update_By'
            val = CurrentForm.HasValue("Last_Update_By") ? CurrentForm.GetValue("Last_Update_By") : CurrentForm.GetValue("x_Last_Update_By");
            if (!Last_Update_By.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Last_Update_By") && !CurrentForm.HasValue("x_Last_Update_By")) // DN
                    Last_Update_By.Visible = false; // Disable update for API request
                else
                    Last_Update_By.SetFormValue(val);
            }

            // Check field name 'Request_Currency' before field var 'x_Request_Currency'
            val = CurrentForm.HasValue("Request_Currency") ? CurrentForm.GetValue("Request_Currency") : CurrentForm.GetValue("x_Request_Currency");
            if (!Request_Currency.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Request_Currency") && !CurrentForm.HasValue("x_Request_Currency")) // DN
                    Request_Currency.Visible = false; // Disable update for API request
                else
                    Request_Currency.SetFormValue(val);
            }
        }
        #pragma warning restore 1998

        // Restore form values
        public void RestoreFormValues()
        {
            id.CurrentValue = id.FormValue;
            Request_No.CurrentValue = Request_No.FormValue;
            Reference_Doc.CurrentValue = Reference_Doc.FormValue;
            Reason.CurrentValue = Reason.FormValue;
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
            Request_Currency.CurrentValue = Request_Currency.FormValue;
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

        #pragma warning disable 618, 1998
        // Load old record
        protected async Task<Dictionary<string, object>?> LoadOldRecord(DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType>? cnn = null) {
            // Load old record
            Dictionary<string, object>? row = null;
            bool validKey = !Empty(OldKey);
            if (validKey) {
                SetKey(OldKey);
                CurrentFilter = GetRecordFilter();
                string sql = CurrentSql;
                try {
                    row = await (cnn ?? Connection).GetRowAsync(sql);
                } catch {
                    row = null;
                }
            }
            await LoadRowValues(row); // Load row values
            return row;
        }
        #pragma warning restore 618, 1998

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

                // Request_No
                Request_No.HrefValue = "";

                // Reference_Doc
                Reference_Doc.HrefValue = "";

                // Reason
                Reason.HrefValue = "";

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

                // Request_Currency
                Request_Currency.HrefValue = "";
            } else if (RowType == RowType.Add) {
                // id
                id.SetupEditAttributes();
                id.EditValue = id.CurrentValue; // DN
                id.PlaceHolder = RemoveHtml(id.Caption);
                if (!Empty(id.EditValue) && IsNumeric(id.EditValue))
                    id.EditValue = FormatNumber(id.EditValue, null);

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

                // Request_Currency
                Request_Currency.SetupEditAttributes();
                if (!Request_Currency.Raw)
                    Request_Currency.CurrentValue = HtmlDecode(Request_Currency.CurrentValue);
                Request_Currency.EditValue = HtmlEncode(Request_Currency.CurrentValue);
                Request_Currency.PlaceHolder = RemoveHtml(Request_Currency.Caption);

                // Add refer script

                // id
                id.HrefValue = "";

                // Request_No
                Request_No.HrefValue = "";

                // Reference_Doc
                Reference_Doc.HrefValue = "";

                // Reason
                Reason.HrefValue = "";

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
            // Check if validation required
            if (!Config.ServerValidate)
                return true;
            bool validateForm = true;
            if (id.Required) {
                if (!id.IsDetailKey && Empty(id.FormValue)) {
                    id.AddErrorMessage(ConvertToString(id.RequiredErrorMessage).Replace("%s", id.Caption));
                }
            }
            if (!CheckInteger(id.FormValue)) {
                id.AddErrorMessage(id.GetErrorMessage(false));
            }
            if (Request_No.Required) {
                if (!Request_No.IsDetailKey && Empty(Request_No.FormValue)) {
                    Request_No.AddErrorMessage(ConvertToString(Request_No.RequiredErrorMessage).Replace("%s", Request_No.Caption));
                }
            }
            if (Reference_Doc.Required) {
                if (!Reference_Doc.IsDetailKey && Empty(Reference_Doc.FormValue)) {
                    Reference_Doc.AddErrorMessage(ConvertToString(Reference_Doc.RequiredErrorMessage).Replace("%s", Reference_Doc.Caption));
                }
            }
            if (Reason.Required) {
                if (!Reason.IsDetailKey && Empty(Reason.FormValue)) {
                    Reason.AddErrorMessage(ConvertToString(Reason.RequiredErrorMessage).Replace("%s", Reason.Caption));
                }
            }
            if (Request_By_Branch.Required) {
                if (!Request_By_Branch.IsDetailKey && Empty(Request_By_Branch.FormValue)) {
                    Request_By_Branch.AddErrorMessage(ConvertToString(Request_By_Branch.RequiredErrorMessage).Replace("%s", Request_By_Branch.Caption));
                }
            }
            if (Request_By_Region.Required) {
                if (!Request_By_Region.IsDetailKey && Empty(Request_By_Region.FormValue)) {
                    Request_By_Region.AddErrorMessage(ConvertToString(Request_By_Region.RequiredErrorMessage).Replace("%s", Request_By_Region.Caption));
                }
            }
            if (Request_Industry.Required) {
                if (!Request_Industry.IsDetailKey && Empty(Request_Industry.FormValue)) {
                    Request_Industry.AddErrorMessage(ConvertToString(Request_Industry.RequiredErrorMessage).Replace("%s", Request_Industry.Caption));
                }
            }
            if (Customer_ID.Required) {
                if (!Customer_ID.IsDetailKey && Empty(Customer_ID.FormValue)) {
                    Customer_ID.AddErrorMessage(ConvertToString(Customer_ID.RequiredErrorMessage).Replace("%s", Customer_ID.Caption));
                }
            }
            if (Customer_Name.Required) {
                if (!Customer_Name.IsDetailKey && Empty(Customer_Name.FormValue)) {
                    Customer_Name.AddErrorMessage(ConvertToString(Customer_Name.RequiredErrorMessage).Replace("%s", Customer_Name.Caption));
                }
            }
            if (SAP_Total.Required) {
                if (!SAP_Total.IsDetailKey && Empty(SAP_Total.FormValue)) {
                    SAP_Total.AddErrorMessage(ConvertToString(SAP_Total.RequiredErrorMessage).Replace("%s", SAP_Total.Caption));
                }
            }
            if (!CheckNumber(SAP_Total.FormValue)) {
                SAP_Total.AddErrorMessage(SAP_Total.GetErrorMessage(false));
            }
            if (Override_Total.Required) {
                if (!Override_Total.IsDetailKey && Empty(Override_Total.FormValue)) {
                    Override_Total.AddErrorMessage(ConvertToString(Override_Total.RequiredErrorMessage).Replace("%s", Override_Total.Caption));
                }
            }
            if (!CheckNumber(Override_Total.FormValue)) {
                Override_Total.AddErrorMessage(Override_Total.GetErrorMessage(false));
            }
            if (Variance_Total.Required) {
                if (!Variance_Total.IsDetailKey && Empty(Variance_Total.FormValue)) {
                    Variance_Total.AddErrorMessage(ConvertToString(Variance_Total.RequiredErrorMessage).Replace("%s", Variance_Total.Caption));
                }
            }
            if (!CheckNumber(Variance_Total.FormValue)) {
                Variance_Total.AddErrorMessage(Variance_Total.GetErrorMessage(false));
            }
            if (Variance_Percent.Required) {
                if (!Variance_Percent.IsDetailKey && Empty(Variance_Percent.FormValue)) {
                    Variance_Percent.AddErrorMessage(ConvertToString(Variance_Percent.RequiredErrorMessage).Replace("%s", Variance_Percent.Caption));
                }
            }
            if (!CheckNumber(Variance_Percent.FormValue)) {
                Variance_Percent.AddErrorMessage(Variance_Percent.GetErrorMessage(false));
            }
            if (Notes.Required) {
                if (!Notes.IsDetailKey && Empty(Notes.FormValue)) {
                    Notes.AddErrorMessage(ConvertToString(Notes.RequiredErrorMessage).Replace("%s", Notes.Caption));
                }
            }
            if (Next_Approver.Required) {
                if (!Next_Approver.IsDetailKey && Empty(Next_Approver.FormValue)) {
                    Next_Approver.AddErrorMessage(ConvertToString(Next_Approver.RequiredErrorMessage).Replace("%s", Next_Approver.Caption));
                }
            }
            if (Request_PIC.Required) {
                if (!Request_PIC.IsDetailKey && Empty(Request_PIC.FormValue)) {
                    Request_PIC.AddErrorMessage(ConvertToString(Request_PIC.RequiredErrorMessage).Replace("%s", Request_PIC.Caption));
                }
            }
            if (Request_Status.Required) {
                if (!Request_Status.IsDetailKey && Empty(Request_Status.FormValue)) {
                    Request_Status.AddErrorMessage(ConvertToString(Request_Status.RequiredErrorMessage).Replace("%s", Request_Status.Caption));
                }
            }
            if (List_Approver.Required) {
                if (!List_Approver.IsDetailKey && Empty(List_Approver.FormValue)) {
                    List_Approver.AddErrorMessage(ConvertToString(List_Approver.RequiredErrorMessage).Replace("%s", List_Approver.Caption));
                }
            }
            if (Last_Update_By.Required) {
                if (!Last_Update_By.IsDetailKey && Empty(Last_Update_By.FormValue)) {
                    Last_Update_By.AddErrorMessage(ConvertToString(Last_Update_By.RequiredErrorMessage).Replace("%s", Last_Update_By.Caption));
                }
            }
            if (Request_Currency.Required) {
                if (!Request_Currency.IsDetailKey && Empty(Request_Currency.FormValue)) {
                    Request_Currency.AddErrorMessage(ConvertToString(Request_Currency.RequiredErrorMessage).Replace("%s", Request_Currency.Caption));
                }
            }

            // Validate detail grid
            var detailTblVar = CurrentDetailTables;
            if (detailTblVar.Contains("tr_request_detail") && trRequestDetail?.DetailAdd == true) {
                trRequestDetailGrid = Resolve("TrRequestDetailGrid")!; // Get detail page object
                if (trRequestDetailGrid != null)
                    validateForm = validateForm && await trRequestDetailGrid.ValidateGridForm();
            }
            if (detailTblVar.Contains("tr_request_approver") && trRequestApprover?.DetailAdd == true) {
                trRequestApproverGrid = Resolve("TrRequestApproverGrid")!; // Get detail page object
                if (trRequestApproverGrid != null)
                    validateForm = validateForm && await trRequestApproverGrid.ValidateGridForm();
            }
            if (detailTblVar.Contains("tr_request_approval_history") && trRequestApprovalHistory?.DetailAdd == true) {
                trRequestApprovalHistoryGrid = Resolve("TrRequestApprovalHistoryGrid")!; // Get detail page object
                if (trRequestApprovalHistoryGrid != null)
                    validateForm = validateForm && await trRequestApprovalHistoryGrid.ValidateGridForm();
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

        // Add record
        #pragma warning disable 168, 219

        protected async Task<JsonBoolResult> AddRow(Dictionary<string, object>? rsold = null) { // DN
            bool result = false;

            // Set new row
            Dictionary<string, object> rsnew = new ();
            try {
                // id
                id.SetDbValue(rsnew, id.CurrentValue);

                // Request_No
                Request_No.SetDbValue(rsnew, Request_No.CurrentValue);

                // Reference_Doc
                Reference_Doc.SetDbValue(rsnew, Reference_Doc.CurrentValue);

                // Reason
                Reason.SetDbValue(rsnew, Reason.CurrentValue);

                // Request_By_Branch
                Request_By_Branch.SetDbValue(rsnew, Request_By_Branch.CurrentValue);

                // Request_By_Region
                Request_By_Region.SetDbValue(rsnew, Request_By_Region.CurrentValue);

                // Request_Industry
                Request_Industry.SetDbValue(rsnew, Request_Industry.CurrentValue);

                // Customer_ID
                Customer_ID.SetDbValue(rsnew, Customer_ID.CurrentValue);

                // Customer_Name
                Customer_Name.SetDbValue(rsnew, Customer_Name.CurrentValue);

                // SAP_Total
                SAP_Total.SetDbValue(rsnew, SAP_Total.CurrentValue);

                // Override_Total
                Override_Total.SetDbValue(rsnew, Override_Total.CurrentValue);

                // Variance_Total
                Variance_Total.SetDbValue(rsnew, Variance_Total.CurrentValue);

                // Variance_Percent
                Variance_Percent.SetDbValue(rsnew, Variance_Percent.CurrentValue);

                // Notes
                Notes.SetDbValue(rsnew, Notes.CurrentValue);

                // Next_Approver
                Next_Approver.SetDbValue(rsnew, Next_Approver.CurrentValue);

                // Request_PIC
                Request_PIC.SetDbValue(rsnew, Request_PIC.CurrentValue);

                // Request_Status
                Request_Status.SetDbValue(rsnew, Request_Status.CurrentValue);

                // List_Approver
                List_Approver.SetDbValue(rsnew, List_Approver.CurrentValue);

                // Last_Update_By
                Last_Update_By.CurrentValue = Last_Update_By.GetAutoUpdateValue();
                Last_Update_By.SetDbValue(rsnew, Last_Update_By.CurrentValue, false);

                // Request_Currency
                Request_Currency.SetDbValue(rsnew, Request_Currency.CurrentValue);
            } catch (Exception e) {
                if (Config.Debug)
                    throw;
                FailureMessage = e.Message;
                return JsonBoolResult.FalseResult;
            }

            // Update current values
            SetCurrentValues(rsnew);
            if (!Empty(id.CurrentValue)) { // Check field with unique index
                var filter = "(id = " + AdjustSql(id.CurrentValue, DbId) + ")";
                using var rschk = await LoadReader(filter);
                if (rschk?.HasRows ?? false) { // DN
                    FailureMessage = Language.Phrase("DupIndex").Replace("%f", id.Caption).Replace("%v", ConvertToString(id.CurrentValue));
                    return JsonBoolResult.FalseResult;
                }
            }

            // Begin transaction
            if (!Empty(CurrentDetailTable) && UseTransaction)
                Connection.BeginTrans();

            // Load db values from rsold
            LoadDbValues(rsold);

            // Call Row Inserting event
            bool insertRow = RowInserting(rsold, rsnew);

            // Check if key value entered
            if (insertRow && ValidateKey && Empty(rsnew["id"])) {
                FailureMessage = Language.Phrase("InvalidKeyValue");
                insertRow = false;
            }

            // Check for duplicate key
            if (insertRow && ValidateKey) {
                string filter = GetRecordFilter(rsnew);
                using var rschk = await LoadReader(filter);
                if (rschk?.HasRows ?? false) { // DN
                    FailureMessage = Language.Phrase("DupKey").Replace("%f", filter);
                    insertRow = false;
                }
            }
            if (insertRow) {
                try {
                    result = ConvertToBool(await InsertAsync(rsnew));
                } catch (Exception e) {
                    if (Config.Debug)
                        throw;
                    FailureMessage = e.Message;
                    result = false;
                }
            } else {
                if (SuccessMessage != "" || FailureMessage != "") {
                    // Use the message, do nothing
                } else if (CancelMessage != "") {
                    FailureMessage = CancelMessage;
                    CancelMessage = "";
                } else {
                    FailureMessage = Language.Phrase("InsertCancelled");
                }
                result = false;
            }

            // Add detail records
            var detailTblVar = CurrentDetailTables;
            if (detailTblVar.Contains("tr_request_detail") && trRequestDetail?.DetailAdd == true && result) {
                trRequestDetail.request_id.SessionValue = ConvertToString(id.CurrentValue); // Set master key
                trRequestDetailGrid = Resolve("TrRequestDetailGrid")!; // Get detail page object
                if (trRequestDetailGrid != null) {
                    Security.LoadCurrentUserLevel(ProjectID + "tr_request_detail"); // Load user level of detail table
                    result = await trRequestDetailGrid.GridInsert();
                    Security.LoadCurrentUserLevel(ProjectID + TableName); // Restore user level of master table
                }
            }
            if (detailTblVar.Contains("tr_request_approver") && trRequestApprover?.DetailAdd == true && result) {
                trRequestApprover.request_id.SessionValue = ConvertToString(id.CurrentValue); // Set master key
                trRequestApproverGrid = Resolve("TrRequestApproverGrid")!; // Get detail page object
                if (trRequestApproverGrid != null) {
                    Security.LoadCurrentUserLevel(ProjectID + "tr_request_approver"); // Load user level of detail table
                    result = await trRequestApproverGrid.GridInsert();
                    Security.LoadCurrentUserLevel(ProjectID + TableName); // Restore user level of master table
                }
            }
            if (detailTblVar.Contains("tr_request_approval_history") && trRequestApprovalHistory?.DetailAdd == true && result) {
                trRequestApprovalHistory.request_id.SessionValue = ConvertToString(id.CurrentValue); // Set master key
                trRequestApprovalHistoryGrid = Resolve("TrRequestApprovalHistoryGrid")!; // Get detail page object
                if (trRequestApprovalHistoryGrid != null) {
                    Security.LoadCurrentUserLevel(ProjectID + "tr_request_approval_history"); // Load user level of detail table
                    result = await trRequestApprovalHistoryGrid.GridInsert();
                    Security.LoadCurrentUserLevel(ProjectID + TableName); // Restore user level of master table
                }
            }

            // Commit/Rollback transaction
            if (!Empty(CurrentDetailTable) && UseTransaction) {
                if (result) {
                    Connection.CommitTrans(); // Commit transaction
                } else {
                    Connection.RollbackTrans(); // Rollback transaction
                }
            }

            // Call Row Inserted event
            if (result)
                RowInserted(rsold, rsnew);

            // Write JSON for API request
            Dictionary<string, object> d = new ();
            d.Add("success", result);
            if (IsJsonResponse() && result) {
                if (GetRecordFromDictionary(rsnew) is var row && row != null) {
                    string table = TableVar;
                    d.Add(table, row);
                }
                d.Add("action", Config.ApiAddAction);
                d.Add("version", Config.ProductVersion);
                return new JsonBoolResult(d, result);
            }
            return new JsonBoolResult(d, result);
        }

        // Set up detail parms based on QueryString
        protected void SetupDetailParms() {
            StringValues detailTblVar = "";
            // Get the keys for master table
            if (Query.TryGetValue(Config.TableShowDetail, out detailTblVar)) { // Do not use Get()
                CurrentDetailTable = detailTblVar.ToString();
            } else {
                detailTblVar = CurrentDetailTable;
            }
            if (!Empty(detailTblVar)) {
                var detailTblVars = detailTblVar.ToString().Split(',');
                if (detailTblVars.Contains("tr_request_detail")) {
                    trRequestDetailGrid = Resolve("TrRequestDetailGrid")!;
                    if (trRequestDetailGrid?.DetailAdd ?? false) {
                        if (CopyRecord)
                            trRequestDetailGrid.CurrentMode = "copy";
                        else
                            trRequestDetailGrid.CurrentMode = "add";
                        trRequestDetailGrid.CurrentAction = "gridadd";

                        // Save current master table to detail table
                        trRequestDetailGrid.CurrentMasterTable = TableVar;
                        trRequestDetailGrid.StartRecordNumber = 1;
                        trRequestDetailGrid.request_id.IsDetailKey = true;
                        trRequestDetailGrid.request_id.CurrentValue = id.CurrentValue;
                        trRequestDetailGrid.request_id.SessionValue = trRequestDetailGrid.request_id.CurrentValue;
                    }
                }
                if (detailTblVars.Contains("tr_request_approver")) {
                    trRequestApproverGrid = Resolve("TrRequestApproverGrid")!;
                    if (trRequestApproverGrid?.DetailAdd ?? false) {
                        if (CopyRecord)
                            trRequestApproverGrid.CurrentMode = "copy";
                        else
                            trRequestApproverGrid.CurrentMode = "add";
                        trRequestApproverGrid.CurrentAction = "gridadd";

                        // Save current master table to detail table
                        trRequestApproverGrid.CurrentMasterTable = TableVar;
                        trRequestApproverGrid.StartRecordNumber = 1;
                        trRequestApproverGrid.request_id.IsDetailKey = true;
                        trRequestApproverGrid.request_id.CurrentValue = id.CurrentValue;
                        trRequestApproverGrid.request_id.SessionValue = trRequestApproverGrid.request_id.CurrentValue;
                    }
                }
                if (detailTblVars.Contains("tr_request_approval_history")) {
                    trRequestApprovalHistoryGrid = Resolve("TrRequestApprovalHistoryGrid")!;
                    if (trRequestApprovalHistoryGrid?.DetailAdd ?? false) {
                        if (CopyRecord)
                            trRequestApprovalHistoryGrid.CurrentMode = "copy";
                        else
                            trRequestApprovalHistoryGrid.CurrentMode = "add";
                        trRequestApprovalHistoryGrid.CurrentAction = "gridadd";

                        // Save current master table to detail table
                        trRequestApprovalHistoryGrid.CurrentMasterTable = TableVar;
                        trRequestApprovalHistoryGrid.StartRecordNumber = 1;
                        trRequestApprovalHistoryGrid.request_id.IsDetailKey = true;
                        trRequestApprovalHistoryGrid.request_id.CurrentValue = id.CurrentValue;
                        trRequestApprovalHistoryGrid.request_id.SessionValue = trRequestApprovalHistoryGrid.request_id.CurrentValue;
                    }
                }
            }
        }

        // Set up Breadcrumb
        protected void SetupBreadcrumb() {
            var breadcrumb = new Breadcrumb();
            string url = CurrentUrl();
            breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("TrRequestList")), "", TableVar, true);
            string pageId = IsCopy ? "Copy" : "Add";
            breadcrumb.Add("add", pageId, url);
            CurrentBreadcrumb = breadcrumb;
        }

        // Set up multi pages
        protected void SetupMultiPages() {
            var pages = new SubPages();
            pages.Style = "tabs";
            pages.Add(0);
            pages.Add(1);
            pages.Add(2);
            MultiPages = pages;
        }

        // Set up detail pages
        protected void SetupDetailPages() {
            var pages = new SubPages();
            pages.Style = "tabs";
            pages.Add("tr_request_detail");
            pages.Add("tr_request_approver");
            pages.Add("tr_request_approval_history");
            DetailPages = pages;
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
