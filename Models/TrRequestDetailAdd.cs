namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// trRequestDetailAdd
    /// </summary>
    public static TrRequestDetailAdd trRequestDetailAdd
    {
        get => HttpData.Get<TrRequestDetailAdd>("trRequestDetailAdd")!;
        set => HttpData["trRequestDetailAdd"] = value;
    }

    /// <summary>
    /// Page class for tr_request_detail
    /// </summary>
    public class TrRequestDetailAdd : TrRequestDetailAddBase
    {
        // Constructor
        public TrRequestDetailAdd(Controller controller) : base(controller)
        {
        }

        // Constructor
        public TrRequestDetailAdd() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TrRequestDetailAddBase : TrRequestDetail
    {
        // Page ID
        public string PageID = "add";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "tr_request_detail";

        // Page object name
        public string PageObjName = "trRequestDetailAdd";

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
        public TrRequestDetailAddBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-desktop-table ew-add-table";

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
        public string PageName => "TrRequestDetailAdd";

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
        public TrRequestDetailAddBase(Controller? controller = null): this() { // DN
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

        // Properties
        public string FormClassName = "ew-form ew-add-form";

        public bool IsModal = false;

        public bool IsMobileOrModal = false;

        public string DbMasterFilter = "";

        public string DbDetailFilter = "";

        public int StartRecord;

        public DbDataReader? Recordset = null; // Reserved // DN

        public bool CopyRecord;

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

            // Set up master/detail parameters
            // NOTE: must be after loadOldRecord to prevent master key values overwritten
            SetupMasterParms();

            // Load form values
            if (postBack) {
                await LoadFormValues(); // Load form values
            }

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
                        return Terminate("TrRequestDetailList"); // No matching record, return to List page // DN
                    }
                    break;
                case "insert": // Add new record // DN
                    SendEmail = true; // Send email on add success
                    var res = await AddRow(rsold);
                    if (res) { // Add successful
                        if (Empty(SuccessMessage) && Post("addopt") != "1") // Skip success message for addopt (done in JavaScript)
                            SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
                        string returnUrl = "";
                        returnUrl = ReturnUrl;
                        if (GetPageName(returnUrl) == "TrRequestDetailList")
                            returnUrl = AddMasterUrl(ListUrl); // List page, return to List page with correct master key if necessary
                        else if (GetPageName(returnUrl) == "TrRequestDetailView")
                            returnUrl = ViewUrl; // View page, return to View page with key URL directly

                        // Handle UseAjaxActions
                        if (IsModal && UseAjaxActions) {
                            IsModal = false;
                            if (GetPageName(returnUrl) != "TrRequestDetailList") {
                                TempData["Return-Url"] = returnUrl; // Save return URL
                                returnUrl = "TrRequestDetailList"; // Return list page content
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
                trRequestDetailAdd?.PageRender();
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
            request_id.Upload.Index = CurrentForm.Index;
            if (!await request_id.Upload.UploadFile()) // DN
                End(request_id.Upload.Message);
            request_id.CurrentValue = request_id.Upload.FileName;
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
                    id.SetFormValue(val);
            }

            // Check field name 'Item_No' before field var 'x_Item_No'
            val = CurrentForm.HasValue("Item_No") ? CurrentForm.GetValue("Item_No") : CurrentForm.GetValue("x_Item_No");
            if (!Item_No.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Item_No") && !CurrentForm.HasValue("x_Item_No")) // DN
                    Item_No.Visible = false; // Disable update for API request
                else
                    Item_No.SetFormValue(val);
            }

            // Check field name 'Part_No' before field var 'x_Part_No'
            val = CurrentForm.HasValue("Part_No") ? CurrentForm.GetValue("Part_No") : CurrentForm.GetValue("x_Part_No");
            if (!Part_No.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Part_No") && !CurrentForm.HasValue("x_Part_No")) // DN
                    Part_No.Visible = false; // Disable update for API request
                else
                    Part_No.SetFormValue(val);
            }

            // Check field name 'Part_Description' before field var 'x_Part_Description'
            val = CurrentForm.HasValue("Part_Description") ? CurrentForm.GetValue("Part_Description") : CurrentForm.GetValue("x_Part_Description");
            if (!Part_Description.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Part_Description") && !CurrentForm.HasValue("x_Part_Description")) // DN
                    Part_Description.Visible = false; // Disable update for API request
                else
                    Part_Description.SetFormValue(val);
            }

            // Check field name 'Qty' before field var 'x_Qty'
            val = CurrentForm.HasValue("Qty") ? CurrentForm.GetValue("Qty") : CurrentForm.GetValue("x_Qty");
            if (!Qty.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Qty") && !CurrentForm.HasValue("x_Qty")) // DN
                    Qty.Visible = false; // Disable update for API request
                else
                    Qty.SetFormValue(val, true, validate);
            }

            // Check field name 'SAP_Unit_Price' before field var 'x_SAP_Unit_Price'
            val = CurrentForm.HasValue("SAP_Unit_Price") ? CurrentForm.GetValue("SAP_Unit_Price") : CurrentForm.GetValue("x_SAP_Unit_Price");
            if (!SAP_Unit_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("SAP_Unit_Price") && !CurrentForm.HasValue("x_SAP_Unit_Price")) // DN
                    SAP_Unit_Price.Visible = false; // Disable update for API request
                else
                    SAP_Unit_Price.SetFormValue(val, true, validate);
            }

            // Check field name 'Extd_SAP_Price' before field var 'x_Extd_SAP_Price'
            val = CurrentForm.HasValue("Extd_SAP_Price") ? CurrentForm.GetValue("Extd_SAP_Price") : CurrentForm.GetValue("x_Extd_SAP_Price");
            if (!Extd_SAP_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Extd_SAP_Price") && !CurrentForm.HasValue("x_Extd_SAP_Price")) // DN
                    Extd_SAP_Price.Visible = false; // Disable update for API request
                else
                    Extd_SAP_Price.SetFormValue(val, true, validate);
            }

            // Check field name 'GP_SAP_Price' before field var 'x_GP_SAP_Price'
            val = CurrentForm.HasValue("GP_SAP_Price") ? CurrentForm.GetValue("GP_SAP_Price") : CurrentForm.GetValue("x_GP_SAP_Price");
            if (!GP_SAP_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("GP_SAP_Price") && !CurrentForm.HasValue("x_GP_SAP_Price")) // DN
                    GP_SAP_Price.Visible = false; // Disable update for API request
                else
                    GP_SAP_Price.SetFormValue(val, true, validate);
            }

            // Check field name 'Override_Unit_Price' before field var 'x_Override_Unit_Price'
            val = CurrentForm.HasValue("Override_Unit_Price") ? CurrentForm.GetValue("Override_Unit_Price") : CurrentForm.GetValue("x_Override_Unit_Price");
            if (!Override_Unit_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Override_Unit_Price") && !CurrentForm.HasValue("x_Override_Unit_Price")) // DN
                    Override_Unit_Price.Visible = false; // Disable update for API request
                else
                    Override_Unit_Price.SetFormValue(val, true, validate);
            }

            // Check field name 'Extd_Override_Price' before field var 'x_Extd_Override_Price'
            val = CurrentForm.HasValue("Extd_Override_Price") ? CurrentForm.GetValue("Extd_Override_Price") : CurrentForm.GetValue("x_Extd_Override_Price");
            if (!Extd_Override_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Extd_Override_Price") && !CurrentForm.HasValue("x_Extd_Override_Price")) // DN
                    Extd_Override_Price.Visible = false; // Disable update for API request
                else
                    Extd_Override_Price.SetFormValue(val, true, validate);
            }

            // Check field name 'GP_Override_Price' before field var 'x_GP_Override_Price'
            val = CurrentForm.HasValue("GP_Override_Price") ? CurrentForm.GetValue("GP_Override_Price") : CurrentForm.GetValue("x_GP_Override_Price");
            if (!GP_Override_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("GP_Override_Price") && !CurrentForm.HasValue("x_GP_Override_Price")) // DN
                    GP_Override_Price.Visible = false; // Disable update for API request
                else
                    GP_Override_Price.SetFormValue(val, true, validate);
            }

            // Check field name 'Override_Core' before field var 'x_Override_Core'
            val = CurrentForm.HasValue("Override_Core") ? CurrentForm.GetValue("Override_Core") : CurrentForm.GetValue("x_Override_Core");
            if (!Override_Core.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Override_Core") && !CurrentForm.HasValue("x_Override_Core")) // DN
                    Override_Core.Visible = false; // Disable update for API request
                else
                    Override_Core.SetFormValue(val, true, validate);
            }

            // Check field name 'Override_Percent' before field var 'x_Override_Percent'
            val = CurrentForm.HasValue("Override_Percent") ? CurrentForm.GetValue("Override_Percent") : CurrentForm.GetValue("x_Override_Percent");
            if (!Override_Percent.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Override_Percent") && !CurrentForm.HasValue("x_Override_Percent")) // DN
                    Override_Percent.Visible = false; // Disable update for API request
                else
                    Override_Percent.SetFormValue(val, true, validate);
            }

            // Check field name 'Core_Life_Ind' before field var 'x_Core_Life_Ind'
            val = CurrentForm.HasValue("Core_Life_Ind") ? CurrentForm.GetValue("Core_Life_Ind") : CurrentForm.GetValue("x_Core_Life_Ind");
            if (!Core_Life_Ind.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Core_Life_Ind") && !CurrentForm.HasValue("x_Core_Life_Ind")) // DN
                    Core_Life_Ind.Visible = false; // Disable update for API request
                else
                    Core_Life_Ind.SetFormValue(val, true, validate);
            }

            // Check field name 'Notes' before field var 'x_Notes'
            val = CurrentForm.HasValue("Notes") ? CurrentForm.GetValue("Notes") : CurrentForm.GetValue("x_Notes");
            if (!Notes.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Notes") && !CurrentForm.HasValue("x_Notes")) // DN
                    Notes.Visible = false; // Disable update for API request
                else
                    Notes.SetFormValue(val);
            }
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
            } else if (RowType == RowType.Add) {
                // id
                id.SetupEditAttributes();
                id.EditValue = id.CurrentValue; // DN
                id.PlaceHolder = RemoveHtml(id.Caption);
                if (!Empty(id.EditValue) && IsNumeric(id.EditValue))
                    id.EditValue = FormatNumber(id.EditValue, null);

                // request_id
                request_id.SetupEditAttributes();
                if (!Empty(request_id.SessionValue)) {
                    request_id.CurrentValue = ForeignKeyValue(request_id.SessionValue);
                    if (!IsNull(request_id.Upload.DbValue)) {
                        request_id.ViewValue = request_id.Upload.DbValue;
                    } else {
                        request_id.ViewValue = "";
                    }
                    request_id.ViewCustomAttributes = "";
                } else {
                    if (!IsNull(request_id.Upload.DbValue)) {
                        request_id.EditValue = request_id.Upload.DbValue;
                    } else {
                        request_id.EditValue = "";
                    }
                    if (!Empty(request_id.CurrentValue))
                            request_id.Upload.FileName = ConvertToString(request_id.CurrentValue);
                    request_id.Upload.DbValue = DbNullValue;
                    if ((IsShow || IsCopy) && !EventCancelled)
                        await RenderUploadField(request_id);
                }

                // Item_No
                Item_No.SetupEditAttributes();
                if (!Item_No.Raw)
                    Item_No.CurrentValue = HtmlDecode(Item_No.CurrentValue);
                Item_No.EditValue = HtmlEncode(Item_No.CurrentValue);
                Item_No.PlaceHolder = RemoveHtml(Item_No.Caption);

                // Part_No
                Part_No.SetupEditAttributes();
                if (!Part_No.Raw)
                    Part_No.CurrentValue = HtmlDecode(Part_No.CurrentValue);
                Part_No.EditValue = HtmlEncode(Part_No.CurrentValue);
                Part_No.PlaceHolder = RemoveHtml(Part_No.Caption);

                // Part_Description
                Part_Description.SetupEditAttributes();
                if (!Part_Description.Raw)
                    Part_Description.CurrentValue = HtmlDecode(Part_Description.CurrentValue);
                Part_Description.EditValue = HtmlEncode(Part_Description.CurrentValue);
                Part_Description.PlaceHolder = RemoveHtml(Part_Description.Caption);

                // Qty
                Qty.SetupEditAttributes();
                Qty.EditValue = Qty.CurrentValue; // DN
                Qty.PlaceHolder = RemoveHtml(Qty.Caption);
                if (!Empty(Qty.EditValue) && IsNumeric(Qty.EditValue))
                    Qty.EditValue = FormatNumber(Qty.EditValue, null);

                // SAP_Unit_Price
                SAP_Unit_Price.SetupEditAttributes();
                SAP_Unit_Price.EditValue = SAP_Unit_Price.CurrentValue; // DN
                SAP_Unit_Price.PlaceHolder = RemoveHtml(SAP_Unit_Price.Caption);
                if (!Empty(SAP_Unit_Price.EditValue) && IsNumeric(SAP_Unit_Price.EditValue))
                    SAP_Unit_Price.EditValue = FormatNumber(SAP_Unit_Price.EditValue, null);

                // Extd_SAP_Price
                Extd_SAP_Price.SetupEditAttributes();
                Extd_SAP_Price.EditValue = Extd_SAP_Price.CurrentValue; // DN
                Extd_SAP_Price.PlaceHolder = RemoveHtml(Extd_SAP_Price.Caption);
                if (!Empty(Extd_SAP_Price.EditValue) && IsNumeric(Extd_SAP_Price.EditValue))
                    Extd_SAP_Price.EditValue = FormatNumber(Extd_SAP_Price.EditValue, null);

                // GP_SAP_Price
                GP_SAP_Price.SetupEditAttributes();
                GP_SAP_Price.EditValue = GP_SAP_Price.CurrentValue; // DN
                GP_SAP_Price.PlaceHolder = RemoveHtml(GP_SAP_Price.Caption);
                if (!Empty(GP_SAP_Price.EditValue) && IsNumeric(GP_SAP_Price.EditValue))
                    GP_SAP_Price.EditValue = FormatNumber(GP_SAP_Price.EditValue, null);

                // Override_Unit_Price
                Override_Unit_Price.SetupEditAttributes();
                Override_Unit_Price.EditValue = Override_Unit_Price.CurrentValue; // DN
                Override_Unit_Price.PlaceHolder = RemoveHtml(Override_Unit_Price.Caption);
                if (!Empty(Override_Unit_Price.EditValue) && IsNumeric(Override_Unit_Price.EditValue))
                    Override_Unit_Price.EditValue = FormatNumber(Override_Unit_Price.EditValue, null);

                // Extd_Override_Price
                Extd_Override_Price.SetupEditAttributes();
                Extd_Override_Price.EditValue = Extd_Override_Price.CurrentValue; // DN
                Extd_Override_Price.PlaceHolder = RemoveHtml(Extd_Override_Price.Caption);
                if (!Empty(Extd_Override_Price.EditValue) && IsNumeric(Extd_Override_Price.EditValue))
                    Extd_Override_Price.EditValue = FormatNumber(Extd_Override_Price.EditValue, null);

                // GP_Override_Price
                GP_Override_Price.SetupEditAttributes();
                GP_Override_Price.EditValue = GP_Override_Price.CurrentValue; // DN
                GP_Override_Price.PlaceHolder = RemoveHtml(GP_Override_Price.Caption);
                if (!Empty(GP_Override_Price.EditValue) && IsNumeric(GP_Override_Price.EditValue))
                    GP_Override_Price.EditValue = FormatNumber(GP_Override_Price.EditValue, null);

                // Override_Core
                Override_Core.SetupEditAttributes();
                Override_Core.EditValue = Override_Core.CurrentValue; // DN
                Override_Core.PlaceHolder = RemoveHtml(Override_Core.Caption);
                if (!Empty(Override_Core.EditValue) && IsNumeric(Override_Core.EditValue))
                    Override_Core.EditValue = FormatNumber(Override_Core.EditValue, null);

                // Override_Percent
                Override_Percent.SetupEditAttributes();
                Override_Percent.EditValue = Override_Percent.CurrentValue; // DN
                Override_Percent.PlaceHolder = RemoveHtml(Override_Percent.Caption);
                if (!Empty(Override_Percent.EditValue) && IsNumeric(Override_Percent.EditValue))
                    Override_Percent.EditValue = FormatNumber(Override_Percent.EditValue, null);

                // Core_Life_Ind
                Core_Life_Ind.SetupEditAttributes();
                Core_Life_Ind.EditValue = Core_Life_Ind.CurrentValue; // DN
                Core_Life_Ind.PlaceHolder = RemoveHtml(Core_Life_Ind.Caption);
                if (!Empty(Core_Life_Ind.EditValue) && IsNumeric(Core_Life_Ind.EditValue))
                    Core_Life_Ind.EditValue = FormatNumber(Core_Life_Ind.EditValue, null);

                // Notes
                Notes.SetupEditAttributes();
                Notes.EditValue = Notes.CurrentValue; // DN
                Notes.PlaceHolder = RemoveHtml(Notes.Caption);

                // Add refer script

                // id
                id.HrefValue = "";

                // request_id
                request_id.HrefValue = "";
                request_id.ExportHrefValue = request_id.UploadPath + request_id.Upload.DbValue;

                // Item_No
                Item_No.HrefValue = "";

                // Part_No
                Part_No.HrefValue = "";

                // Part_Description
                Part_Description.HrefValue = "";

                // Qty
                Qty.HrefValue = "";

                // SAP_Unit_Price
                SAP_Unit_Price.HrefValue = "";

                // Extd_SAP_Price
                Extd_SAP_Price.HrefValue = "";

                // GP_SAP_Price
                GP_SAP_Price.HrefValue = "";

                // Override_Unit_Price
                Override_Unit_Price.HrefValue = "";

                // Extd_Override_Price
                Extd_Override_Price.HrefValue = "";

                // GP_Override_Price
                GP_Override_Price.HrefValue = "";

                // Override_Core
                Override_Core.HrefValue = "";

                // Override_Percent
                Override_Percent.HrefValue = "";

                // Core_Life_Ind
                Core_Life_Ind.HrefValue = "";

                // Notes
                Notes.HrefValue = "";
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
            if (request_id.Required) {
                if (request_id.Upload.FileName == "" && !request_id.Upload.KeepFile) {
                    request_id.AddErrorMessage(ConvertToString(request_id.RequiredErrorMessage).Replace("%s", request_id.Caption));
                }
            }
            if (Item_No.Required) {
                if (!Item_No.IsDetailKey && Empty(Item_No.FormValue)) {
                    Item_No.AddErrorMessage(ConvertToString(Item_No.RequiredErrorMessage).Replace("%s", Item_No.Caption));
                }
            }
            if (Part_No.Required) {
                if (!Part_No.IsDetailKey && Empty(Part_No.FormValue)) {
                    Part_No.AddErrorMessage(ConvertToString(Part_No.RequiredErrorMessage).Replace("%s", Part_No.Caption));
                }
            }
            if (Part_Description.Required) {
                if (!Part_Description.IsDetailKey && Empty(Part_Description.FormValue)) {
                    Part_Description.AddErrorMessage(ConvertToString(Part_Description.RequiredErrorMessage).Replace("%s", Part_Description.Caption));
                }
            }
            if (Qty.Required) {
                if (!Qty.IsDetailKey && Empty(Qty.FormValue)) {
                    Qty.AddErrorMessage(ConvertToString(Qty.RequiredErrorMessage).Replace("%s", Qty.Caption));
                }
            }
            if (!CheckInteger(Qty.FormValue)) {
                Qty.AddErrorMessage(Qty.GetErrorMessage(false));
            }
            if (SAP_Unit_Price.Required) {
                if (!SAP_Unit_Price.IsDetailKey && Empty(SAP_Unit_Price.FormValue)) {
                    SAP_Unit_Price.AddErrorMessage(ConvertToString(SAP_Unit_Price.RequiredErrorMessage).Replace("%s", SAP_Unit_Price.Caption));
                }
            }
            if (!CheckNumber(SAP_Unit_Price.FormValue)) {
                SAP_Unit_Price.AddErrorMessage(SAP_Unit_Price.GetErrorMessage(false));
            }
            if (Extd_SAP_Price.Required) {
                if (!Extd_SAP_Price.IsDetailKey && Empty(Extd_SAP_Price.FormValue)) {
                    Extd_SAP_Price.AddErrorMessage(ConvertToString(Extd_SAP_Price.RequiredErrorMessage).Replace("%s", Extd_SAP_Price.Caption));
                }
            }
            if (!CheckNumber(Extd_SAP_Price.FormValue)) {
                Extd_SAP_Price.AddErrorMessage(Extd_SAP_Price.GetErrorMessage(false));
            }
            if (GP_SAP_Price.Required) {
                if (!GP_SAP_Price.IsDetailKey && Empty(GP_SAP_Price.FormValue)) {
                    GP_SAP_Price.AddErrorMessage(ConvertToString(GP_SAP_Price.RequiredErrorMessage).Replace("%s", GP_SAP_Price.Caption));
                }
            }
            if (!CheckNumber(GP_SAP_Price.FormValue)) {
                GP_SAP_Price.AddErrorMessage(GP_SAP_Price.GetErrorMessage(false));
            }
            if (Override_Unit_Price.Required) {
                if (!Override_Unit_Price.IsDetailKey && Empty(Override_Unit_Price.FormValue)) {
                    Override_Unit_Price.AddErrorMessage(ConvertToString(Override_Unit_Price.RequiredErrorMessage).Replace("%s", Override_Unit_Price.Caption));
                }
            }
            if (!CheckNumber(Override_Unit_Price.FormValue)) {
                Override_Unit_Price.AddErrorMessage(Override_Unit_Price.GetErrorMessage(false));
            }
            if (Extd_Override_Price.Required) {
                if (!Extd_Override_Price.IsDetailKey && Empty(Extd_Override_Price.FormValue)) {
                    Extd_Override_Price.AddErrorMessage(ConvertToString(Extd_Override_Price.RequiredErrorMessage).Replace("%s", Extd_Override_Price.Caption));
                }
            }
            if (!CheckNumber(Extd_Override_Price.FormValue)) {
                Extd_Override_Price.AddErrorMessage(Extd_Override_Price.GetErrorMessage(false));
            }
            if (GP_Override_Price.Required) {
                if (!GP_Override_Price.IsDetailKey && Empty(GP_Override_Price.FormValue)) {
                    GP_Override_Price.AddErrorMessage(ConvertToString(GP_Override_Price.RequiredErrorMessage).Replace("%s", GP_Override_Price.Caption));
                }
            }
            if (!CheckNumber(GP_Override_Price.FormValue)) {
                GP_Override_Price.AddErrorMessage(GP_Override_Price.GetErrorMessage(false));
            }
            if (Override_Core.Required) {
                if (!Override_Core.IsDetailKey && Empty(Override_Core.FormValue)) {
                    Override_Core.AddErrorMessage(ConvertToString(Override_Core.RequiredErrorMessage).Replace("%s", Override_Core.Caption));
                }
            }
            if (!CheckNumber(Override_Core.FormValue)) {
                Override_Core.AddErrorMessage(Override_Core.GetErrorMessage(false));
            }
            if (Override_Percent.Required) {
                if (!Override_Percent.IsDetailKey && Empty(Override_Percent.FormValue)) {
                    Override_Percent.AddErrorMessage(ConvertToString(Override_Percent.RequiredErrorMessage).Replace("%s", Override_Percent.Caption));
                }
            }
            if (!CheckNumber(Override_Percent.FormValue)) {
                Override_Percent.AddErrorMessage(Override_Percent.GetErrorMessage(false));
            }
            if (Core_Life_Ind.Required) {
                if (!Core_Life_Ind.IsDetailKey && Empty(Core_Life_Ind.FormValue)) {
                    Core_Life_Ind.AddErrorMessage(ConvertToString(Core_Life_Ind.RequiredErrorMessage).Replace("%s", Core_Life_Ind.Caption));
                }
            }
            if (!CheckNumber(Core_Life_Ind.FormValue)) {
                Core_Life_Ind.AddErrorMessage(Core_Life_Ind.GetErrorMessage(false));
            }
            if (Notes.Required) {
                if (!Notes.IsDetailKey && Empty(Notes.FormValue)) {
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

        // Add record
        #pragma warning disable 168, 219

        protected async Task<JsonBoolResult> AddRow(Dictionary<string, object>? rsold = null) { // DN
            bool result = false;

            // Set new row
            Dictionary<string, object> rsnew = new ();
            try {
                // id
                id.SetDbValue(rsnew, id.CurrentValue);

                // request_id
                if (request_id.Visible && !request_id.Upload.KeepFile) {
                    request_id.Upload.DbValue = ""; // No need to delete old file
                    if (Empty(request_id.Upload.FileName)) {
                        rsnew["request_id"] = DbNullValue;
                    } else {
                        FixUploadTempFileNames(request_id);
                        rsnew["request_id"] = request_id.Upload.FileName;
                    }
                }

                // Item_No
                Item_No.SetDbValue(rsnew, Item_No.CurrentValue);

                // Part_No
                Part_No.SetDbValue(rsnew, Part_No.CurrentValue);

                // Part_Description
                Part_Description.SetDbValue(rsnew, Part_Description.CurrentValue);

                // Qty
                Qty.SetDbValue(rsnew, Qty.CurrentValue);

                // SAP_Unit_Price
                SAP_Unit_Price.SetDbValue(rsnew, SAP_Unit_Price.CurrentValue);

                // Extd_SAP_Price
                Extd_SAP_Price.SetDbValue(rsnew, Extd_SAP_Price.CurrentValue);

                // GP_SAP_Price
                GP_SAP_Price.SetDbValue(rsnew, GP_SAP_Price.CurrentValue);

                // Override_Unit_Price
                Override_Unit_Price.SetDbValue(rsnew, Override_Unit_Price.CurrentValue);

                // Extd_Override_Price
                Extd_Override_Price.SetDbValue(rsnew, Extd_Override_Price.CurrentValue);

                // GP_Override_Price
                GP_Override_Price.SetDbValue(rsnew, GP_Override_Price.CurrentValue);

                // Override_Core
                Override_Core.SetDbValue(rsnew, Override_Core.CurrentValue);

                // Override_Percent
                Override_Percent.SetDbValue(rsnew, Override_Percent.CurrentValue);

                // Core_Life_Ind
                Core_Life_Ind.SetDbValue(rsnew, Core_Life_Ind.CurrentValue);

                // Notes
                Notes.SetDbValue(rsnew, Notes.CurrentValue);
            } catch (Exception e) {
                if (Config.Debug)
                    throw;
                FailureMessage = e.Message;
                return JsonBoolResult.FalseResult;
            }
            if (request_id.Visible && !request_id.Upload.KeepFile) {
                if (!Empty(request_id.Upload.FileName)) {
                    request_id.Upload.DbValue = DbNullValue;
                    FixUploadFileNames(request_id);
                    request_id.SetDbValue(rsnew, request_id.Upload.FileName);
                }
            }

            // Update current values
            SetCurrentValues(rsnew);
            string? masterFilter;
            Dictionary<string, object?> detailKeys;
            if (!Empty(id.CurrentValue)) { // Check field with unique index
                var filter = "(id = " + AdjustSql(id.CurrentValue, DbId) + ")";
                using var rschk = await LoadReader(filter);
                if (rschk?.HasRows ?? false) { // DN
                    FailureMessage = Language.Phrase("DupIndex").Replace("%f", id.Caption).Replace("%v", ConvertToString(id.CurrentValue));
                    return JsonBoolResult.FalseResult;
                }
            }
            bool validMasterRecord;

            // Check referential integrity for master table 'tr_request'
            validMasterRecord = true;
            detailKeys = new ();
            detailKeys.Add("request_id", request_id.CurrentValue);
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
                if (result) {
                    if (request_id.Visible && !request_id.Upload.KeepFile) {
                        request_id.Upload.DbValue = DbNullValue;
                        if (!await SaveUploadFiles(request_id, ConvertToString(rsnew["request_id"]), false))
                        {
                            FailureMessage = Language.Phrase("UploadError7");
                            return JsonBoolResult.FalseResult;
                        }
                    }
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
            string pageId = IsCopy ? "Copy" : "Add";
            breadcrumb.Add("add", pageId, url);
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
