namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// mtBackupApproverEdit
    /// </summary>
    public static MtBackupApproverEdit mtBackupApproverEdit
    {
        get => HttpData.Get<MtBackupApproverEdit>("mtBackupApproverEdit")!;
        set => HttpData["mtBackupApproverEdit"] = value;
    }

    /// <summary>
    /// Page class for mt_backup_approver
    /// </summary>
    public class MtBackupApproverEdit : MtBackupApproverEditBase
    {
        // Constructor
        public MtBackupApproverEdit(Controller controller) : base(controller)
        {
        }

        // Constructor
        public MtBackupApproverEdit() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class MtBackupApproverEditBase : MtBackupApprover
    {
        // Page ID
        public string PageID = "edit";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "mt_backup_approver";

        // Page object name
        public string PageObjName = "mtBackupApproverEdit";

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
        public MtBackupApproverEditBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-desktop-table ew-edit-table";

            // Language object
            Language = ResolveLanguage();

            // Table object (mtBackupApprover)
            if (mtBackupApprover == null || mtBackupApprover is MtBackupApprover)
                mtBackupApprover = this;

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
        public string PageName => "MtBackupApproverEdit";

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
            _UserName.SetVisibility();
            Full_Name.SetVisibility();
            Position.SetVisibility();
            Departement.SetVisibility();
            Division.SetVisibility();
            Area.SetVisibility();
            Business_Area.SetVisibility();
            Report_To.SetVisibility();
            Business_Area_Desc.SetVisibility();
        }

        // Constructor
        public MtBackupApproverEditBase(Controller? controller = null): this() { // DN
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
                            result.Add("view", pageName == "MtBackupApproverView" ? "1" : "0"); // If View page, no primary button
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

        public int DisplayRecords = 1; // Number of display records

        public int StartRecord;

        public int StopRecord;

        public int TotalRecords = -1;

        public int RecordRange = 10;

        public int RecordCount;

        public Dictionary<string, string> RecordKeys = new ();

        public string FormClassName = "ew-form ew-edit-form overlay-wrapper";

        public bool IsModal = false;

        public bool IsMobileOrModal = false;

        public string DbMasterFilter = "";

        public string DbDetailFilter = "";

        public DbDataReader? Recordset; // DN

        #pragma warning disable 219
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
            bool loaded = false;
            bool postBack = false;
            StringValues sv;
            object? rv;

            // Set up current action and primary key
            if (IsApi()) {
                loaded = true;

                // Load key from form
                string[] keyValues = {};
                if (RouteValues.TryGetValue("key", out object? k))
                    keyValues = ConvertToString(k).Split('/');
                if (RouteValues.TryGetValue("id", out rv)) { // DN
                    id.FormValue = UrlDecode(rv); // DN
                    id.OldValue = id.FormValue;
                } else if (CurrentForm.HasValue("x_id")) {
                    id.FormValue = CurrentForm.GetValue("x_id");
                    id.OldValue = id.FormValue;
                } else if (!Empty(keyValues)) {
                    id.OldValue = ConvertToString(keyValues[0]);
                } else {
                    loaded = false; // Unable to load key
                }

                // Load record
                if (loaded)
                    loaded = await LoadRow();
                if (!loaded) {
                    FailureMessage = Language.Phrase("NoRecord"); // Set no record message
                    return Terminate();
                }
                CurrentAction = "update"; // Update record directly
                OldKey = GetKey(true); // Get from CurrentValue
                postBack = true;
            } else {
                if (!Empty(Post("action"))) {
                    CurrentAction = Post("action"); // Get action code
                    if (!IsShow) // Not reload record, handle as postback
                        postBack = true;

                    // Get key from Form
                    if (Post(OldKeyName, out sv))
                        SetKey(sv.ToString(), IsShow);
                } else {
                    CurrentAction = "show"; // Default action is display

                    // Load key from QueryString
                    bool loadByQuery = false;
                    if (RouteValues.TryGetValue("id", out rv)) { // DN
                        id.QueryValue = UrlDecode(rv); // DN
                        loadByQuery = true;
                    } else if (Get("id", out sv)) {
                        id.QueryValue = sv.ToString();
                        loadByQuery = true;
                    } else {
                        id.CurrentValue = DbNullValue;
                    }
                }

                // Load recordset
                if (IsShow) {
                    // Load current record
                    loaded = await LoadRow();
                OldKey = loaded ? GetKey(true) : ""; // Get from CurrentValue
            }
        }

        // Process form if post back
        if (postBack) {
            await LoadFormValues(); // Get form values
            if (IsApi() && RouteValues.TryGetValue("key", out object? k)) {
                var keyValues = ConvertToString(k).Split('/');
                id.FormValue = ConvertToString(keyValues[0]);
            }
        }

        // Validate form if post back
        if (postBack) {
            if (!await ValidateForm()) {
                EventCancelled = true; // Event cancelled
                RestoreFormValues();
                if (IsApi())
                    return Terminate();
                else
                    CurrentAction = ""; // Form error, reset action
            }
        }

        // Perform current action
        switch (CurrentAction) {
                case "show": // Get a record to display
                        if (!loaded) { // Load record based on key
                            if (Empty(FailureMessage))
                                FailureMessage = Language.Phrase("NoRecord"); // No record found
                            return Terminate("MtBackupApproverList"); // No matching record, return to list
                        }
                    break;
                case "update": // Update // DN
                    CloseRecordset(); // DN
                    string returnUrl = ReturnUrl;
                    if (GetPageName(returnUrl) == "MtBackupApproverList")
                        returnUrl = AddMasterUrl(ListUrl); // List page, return to List page with correct master key if necessary
                    SendEmail = true; // Send email on update success
                    var res = await EditRow();
                    if (res) { // Update record based on key
                        if (Empty(SuccessMessage))
                            SuccessMessage = Language.Phrase("UpdateSuccess"); // Update success

                        // Handle UseAjaxActions with return page
                        if (IsModal && UseAjaxActions) {
                            IsModal = false;
                            if (GetPageName(returnUrl) != "MtBackupApproverList") {
                                TempData["Return-Url"] = returnUrl; // Save return URL
                                returnUrl = "MtBackupApproverList"; // Return list page content
                            }
                        }
                        if (IsJsonResponse()) {
                            ClearMessages(); // Clear messages for Json response
                            return res;
                        } else {
                            return Terminate(returnUrl); // Return to caller
                        }
                    } else if (IsApi()) { // API request, return
                        return Terminate();
                    } else if (IsModal && UseAjaxActions) { // Return JSON error message
                        return Controller.Json(new { success = false, error = GetFailureMessage() });
                    } else if (FailureMessage == Language.Phrase("NoRecord")) {
                        return Terminate(returnUrl); // Return to caller
                    } else {
                        EventCancelled = true; // Event cancelled
                        RestoreFormValues(); // Restore form values if update failed
                    }
                    break;
            }

            // Set up Breadcrumb
            SetupBreadcrumb();

            // Render the record
            RowType = RowType.Edit; // Render as Edit
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
                mtBackupApproverEdit?.PageRender();
            }
            return PageResult();
        }
        #pragma warning restore 219

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
            if (CurrentForm.HasValue("o_id"))
                id.OldValue = CurrentForm.GetValue("o_id");

            // Check field name 'UserName' before field var 'x__UserName'
            val = CurrentForm.HasValue("UserName") ? CurrentForm.GetValue("UserName") : CurrentForm.GetValue("x__UserName");
            if (!_UserName.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("UserName") && !CurrentForm.HasValue("x__UserName")) // DN
                    _UserName.Visible = false; // Disable update for API request
                else
                    _UserName.SetFormValue(val);
            }

            // Check field name 'Full_Name' before field var 'x_Full_Name'
            val = CurrentForm.HasValue("Full_Name") ? CurrentForm.GetValue("Full_Name") : CurrentForm.GetValue("x_Full_Name");
            if (!Full_Name.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Full_Name") && !CurrentForm.HasValue("x_Full_Name")) // DN
                    Full_Name.Visible = false; // Disable update for API request
                else
                    Full_Name.SetFormValue(val);
            }

            // Check field name 'Position' before field var 'x_Position'
            val = CurrentForm.HasValue("Position") ? CurrentForm.GetValue("Position") : CurrentForm.GetValue("x_Position");
            if (!Position.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Position") && !CurrentForm.HasValue("x_Position")) // DN
                    Position.Visible = false; // Disable update for API request
                else
                    Position.SetFormValue(val);
            }

            // Check field name 'Departement' before field var 'x_Departement'
            val = CurrentForm.HasValue("Departement") ? CurrentForm.GetValue("Departement") : CurrentForm.GetValue("x_Departement");
            if (!Departement.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Departement") && !CurrentForm.HasValue("x_Departement")) // DN
                    Departement.Visible = false; // Disable update for API request
                else
                    Departement.SetFormValue(val);
            }

            // Check field name 'Division' before field var 'x_Division'
            val = CurrentForm.HasValue("Division") ? CurrentForm.GetValue("Division") : CurrentForm.GetValue("x_Division");
            if (!Division.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Division") && !CurrentForm.HasValue("x_Division")) // DN
                    Division.Visible = false; // Disable update for API request
                else
                    Division.SetFormValue(val);
            }

            // Check field name 'Area' before field var 'x_Area'
            val = CurrentForm.HasValue("Area") ? CurrentForm.GetValue("Area") : CurrentForm.GetValue("x_Area");
            if (!Area.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Area") && !CurrentForm.HasValue("x_Area")) // DN
                    Area.Visible = false; // Disable update for API request
                else
                    Area.SetFormValue(val);
            }

            // Check field name 'Business_Area' before field var 'x_Business_Area'
            val = CurrentForm.HasValue("Business_Area") ? CurrentForm.GetValue("Business_Area") : CurrentForm.GetValue("x_Business_Area");
            if (!Business_Area.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Business_Area") && !CurrentForm.HasValue("x_Business_Area")) // DN
                    Business_Area.Visible = false; // Disable update for API request
                else
                    Business_Area.SetFormValue(val);
            }

            // Check field name 'Report_To' before field var 'x_Report_To'
            val = CurrentForm.HasValue("Report_To") ? CurrentForm.GetValue("Report_To") : CurrentForm.GetValue("x_Report_To");
            if (!Report_To.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Report_To") && !CurrentForm.HasValue("x_Report_To")) // DN
                    Report_To.Visible = false; // Disable update for API request
                else
                    Report_To.SetFormValue(val);
            }

            // Check field name 'Business_Area_Desc' before field var 'x_Business_Area_Desc'
            val = CurrentForm.HasValue("Business_Area_Desc") ? CurrentForm.GetValue("Business_Area_Desc") : CurrentForm.GetValue("x_Business_Area_Desc");
            if (!Business_Area_Desc.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Business_Area_Desc") && !CurrentForm.HasValue("x_Business_Area_Desc")) // DN
                    Business_Area_Desc.Visible = false; // Disable update for API request
                else
                    Business_Area_Desc.SetFormValue(val);
            }
        }
        #pragma warning restore 1998

        // Restore form values
        public void RestoreFormValues()
        {
            id.CurrentValue = id.FormValue;
            _UserName.CurrentValue = _UserName.FormValue;
            Full_Name.CurrentValue = Full_Name.FormValue;
            Position.CurrentValue = Position.FormValue;
            Departement.CurrentValue = Departement.FormValue;
            Division.CurrentValue = Division.FormValue;
            Area.CurrentValue = Area.FormValue;
            Business_Area.CurrentValue = Business_Area.FormValue;
            Report_To.CurrentValue = Report_To.FormValue;
            Business_Area_Desc.CurrentValue = Business_Area_Desc.FormValue;
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
            _UserName.SetDbValue(row["UserName"]);
            Full_Name.SetDbValue(row["Full_Name"]);
            Position.SetDbValue(row["Position"]);
            Departement.SetDbValue(row["Departement"]);
            Division.SetDbValue(row["Division"]);
            Area.SetDbValue(row["Area"]);
            Business_Area.SetDbValue(row["Business_Area"]);
            Report_To.SetDbValue(row["Report_To"]);
            Business_Area_Desc.SetDbValue(row["Business_Area_Desc"]);
        }
        #pragma warning restore 162, 168, 1998, 4014

        // Return a row with default values
        protected Dictionary<string, object> NewRow() {
            var row = new Dictionary<string, object>();
            row.Add("id", id.DefaultValue ?? DbNullValue); // DN
            row.Add("UserName", _UserName.DefaultValue ?? DbNullValue); // DN
            row.Add("Full_Name", Full_Name.DefaultValue ?? DbNullValue); // DN
            row.Add("Position", Position.DefaultValue ?? DbNullValue); // DN
            row.Add("Departement", Departement.DefaultValue ?? DbNullValue); // DN
            row.Add("Division", Division.DefaultValue ?? DbNullValue); // DN
            row.Add("Area", Area.DefaultValue ?? DbNullValue); // DN
            row.Add("Business_Area", Business_Area.DefaultValue ?? DbNullValue); // DN
            row.Add("Report_To", Report_To.DefaultValue ?? DbNullValue); // DN
            row.Add("Business_Area_Desc", Business_Area_Desc.DefaultValue ?? DbNullValue); // DN
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

            // UserName
            _UserName.RowCssClass = "row";

            // Full_Name
            Full_Name.RowCssClass = "row";

            // Position
            Position.RowCssClass = "row";

            // Departement
            Departement.RowCssClass = "row";

            // Division
            Division.RowCssClass = "row";

            // Area
            Area.RowCssClass = "row";

            // Business_Area
            Business_Area.RowCssClass = "row";

            // Report_To
            Report_To.RowCssClass = "row";

            // Business_Area_Desc
            Business_Area_Desc.RowCssClass = "row";

            // View row
            if (RowType == RowType.View) {
                // id
                id.ViewValue = id.CurrentValue;
                id.ViewValue = FormatNumber(id.ViewValue, id.FormatPattern);
                id.ViewCustomAttributes = "";

                // UserName
                _UserName.ViewValue = ConvertToString(_UserName.CurrentValue); // DN
                _UserName.ViewCustomAttributes = "";

                // Full_Name
                Full_Name.ViewValue = ConvertToString(Full_Name.CurrentValue); // DN
                Full_Name.ViewCustomAttributes = "";

                // Position
                Position.ViewValue = ConvertToString(Position.CurrentValue); // DN
                Position.ViewCustomAttributes = "";

                // Departement
                Departement.ViewValue = ConvertToString(Departement.CurrentValue); // DN
                Departement.ViewCustomAttributes = "";

                // Division
                Division.ViewValue = ConvertToString(Division.CurrentValue); // DN
                Division.ViewCustomAttributes = "";

                // Area
                Area.ViewValue = ConvertToString(Area.CurrentValue); // DN
                Area.ViewCustomAttributes = "";

                // Business_Area
                Business_Area.ViewValue = ConvertToString(Business_Area.CurrentValue); // DN
                Business_Area.ViewCustomAttributes = "";

                // Report_To
                Report_To.ViewValue = ConvertToString(Report_To.CurrentValue); // DN
                Report_To.ViewCustomAttributes = "";

                // Business_Area_Desc
                Business_Area_Desc.ViewValue = ConvertToString(Business_Area_Desc.CurrentValue); // DN
                Business_Area_Desc.ViewCustomAttributes = "";

                // id
                id.HrefValue = "";

                // UserName
                _UserName.HrefValue = "";

                // Full_Name
                Full_Name.HrefValue = "";

                // Position
                Position.HrefValue = "";

                // Departement
                Departement.HrefValue = "";

                // Division
                Division.HrefValue = "";

                // Area
                Area.HrefValue = "";

                // Business_Area
                Business_Area.HrefValue = "";

                // Report_To
                Report_To.HrefValue = "";

                // Business_Area_Desc
                Business_Area_Desc.HrefValue = "";
            } else if (RowType == RowType.Edit) {
                // id
                id.SetupEditAttributes();
                id.EditValue = id.CurrentValue; // DN
                id.PlaceHolder = RemoveHtml(id.Caption);

                // UserName
                _UserName.SetupEditAttributes();
                if (!_UserName.Raw)
                    _UserName.CurrentValue = HtmlDecode(_UserName.CurrentValue);
                _UserName.EditValue = HtmlEncode(_UserName.CurrentValue);
                _UserName.PlaceHolder = RemoveHtml(_UserName.Caption);

                // Full_Name
                Full_Name.SetupEditAttributes();
                if (!Full_Name.Raw)
                    Full_Name.CurrentValue = HtmlDecode(Full_Name.CurrentValue);
                Full_Name.EditValue = HtmlEncode(Full_Name.CurrentValue);
                Full_Name.PlaceHolder = RemoveHtml(Full_Name.Caption);

                // Position
                Position.SetupEditAttributes();
                if (!Position.Raw)
                    Position.CurrentValue = HtmlDecode(Position.CurrentValue);
                Position.EditValue = HtmlEncode(Position.CurrentValue);
                Position.PlaceHolder = RemoveHtml(Position.Caption);

                // Departement
                Departement.SetupEditAttributes();
                if (!Departement.Raw)
                    Departement.CurrentValue = HtmlDecode(Departement.CurrentValue);
                Departement.EditValue = HtmlEncode(Departement.CurrentValue);
                Departement.PlaceHolder = RemoveHtml(Departement.Caption);

                // Division
                Division.SetupEditAttributes();
                if (!Division.Raw)
                    Division.CurrentValue = HtmlDecode(Division.CurrentValue);
                Division.EditValue = HtmlEncode(Division.CurrentValue);
                Division.PlaceHolder = RemoveHtml(Division.Caption);

                // Area
                Area.SetupEditAttributes();
                if (!Area.Raw)
                    Area.CurrentValue = HtmlDecode(Area.CurrentValue);
                Area.EditValue = HtmlEncode(Area.CurrentValue);
                Area.PlaceHolder = RemoveHtml(Area.Caption);

                // Business_Area
                Business_Area.SetupEditAttributes();
                if (!Business_Area.Raw)
                    Business_Area.CurrentValue = HtmlDecode(Business_Area.CurrentValue);
                Business_Area.EditValue = HtmlEncode(Business_Area.CurrentValue);
                Business_Area.PlaceHolder = RemoveHtml(Business_Area.Caption);

                // Report_To
                Report_To.SetupEditAttributes();
                if (!Report_To.Raw)
                    Report_To.CurrentValue = HtmlDecode(Report_To.CurrentValue);
                Report_To.EditValue = HtmlEncode(Report_To.CurrentValue);
                Report_To.PlaceHolder = RemoveHtml(Report_To.Caption);

                // Business_Area_Desc
                Business_Area_Desc.SetupEditAttributes();
                if (!Business_Area_Desc.Raw)
                    Business_Area_Desc.CurrentValue = HtmlDecode(Business_Area_Desc.CurrentValue);
                Business_Area_Desc.EditValue = HtmlEncode(Business_Area_Desc.CurrentValue);
                Business_Area_Desc.PlaceHolder = RemoveHtml(Business_Area_Desc.Caption);

                // Edit refer script

                // id
                id.HrefValue = "";

                // UserName
                _UserName.HrefValue = "";

                // Full_Name
                Full_Name.HrefValue = "";

                // Position
                Position.HrefValue = "";

                // Departement
                Departement.HrefValue = "";

                // Division
                Division.HrefValue = "";

                // Area
                Area.HrefValue = "";

                // Business_Area
                Business_Area.HrefValue = "";

                // Report_To
                Report_To.HrefValue = "";

                // Business_Area_Desc
                Business_Area_Desc.HrefValue = "";
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
            if (_UserName.Required) {
                if (!_UserName.IsDetailKey && Empty(_UserName.FormValue)) {
                    _UserName.AddErrorMessage(ConvertToString(_UserName.RequiredErrorMessage).Replace("%s", _UserName.Caption));
                }
            }
            if (Full_Name.Required) {
                if (!Full_Name.IsDetailKey && Empty(Full_Name.FormValue)) {
                    Full_Name.AddErrorMessage(ConvertToString(Full_Name.RequiredErrorMessage).Replace("%s", Full_Name.Caption));
                }
            }
            if (Position.Required) {
                if (!Position.IsDetailKey && Empty(Position.FormValue)) {
                    Position.AddErrorMessage(ConvertToString(Position.RequiredErrorMessage).Replace("%s", Position.Caption));
                }
            }
            if (Departement.Required) {
                if (!Departement.IsDetailKey && Empty(Departement.FormValue)) {
                    Departement.AddErrorMessage(ConvertToString(Departement.RequiredErrorMessage).Replace("%s", Departement.Caption));
                }
            }
            if (Division.Required) {
                if (!Division.IsDetailKey && Empty(Division.FormValue)) {
                    Division.AddErrorMessage(ConvertToString(Division.RequiredErrorMessage).Replace("%s", Division.Caption));
                }
            }
            if (Area.Required) {
                if (!Area.IsDetailKey && Empty(Area.FormValue)) {
                    Area.AddErrorMessage(ConvertToString(Area.RequiredErrorMessage).Replace("%s", Area.Caption));
                }
            }
            if (Business_Area.Required) {
                if (!Business_Area.IsDetailKey && Empty(Business_Area.FormValue)) {
                    Business_Area.AddErrorMessage(ConvertToString(Business_Area.RequiredErrorMessage).Replace("%s", Business_Area.Caption));
                }
            }
            if (Report_To.Required) {
                if (!Report_To.IsDetailKey && Empty(Report_To.FormValue)) {
                    Report_To.AddErrorMessage(ConvertToString(Report_To.RequiredErrorMessage).Replace("%s", Report_To.Caption));
                }
            }
            if (Business_Area_Desc.Required) {
                if (!Business_Area_Desc.IsDetailKey && Empty(Business_Area_Desc.FormValue)) {
                    Business_Area_Desc.AddErrorMessage(ConvertToString(Business_Area_Desc.RequiredErrorMessage).Replace("%s", Business_Area_Desc.Caption));
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
            id.SetDbValue(rsnew, id.CurrentValue, id.ReadOnly);

            // UserName
            _UserName.SetDbValue(rsnew, _UserName.CurrentValue, _UserName.ReadOnly);

            // Full_Name
            Full_Name.SetDbValue(rsnew, Full_Name.CurrentValue, Full_Name.ReadOnly);

            // Position
            Position.SetDbValue(rsnew, Position.CurrentValue, Position.ReadOnly);

            // Departement
            Departement.SetDbValue(rsnew, Departement.CurrentValue, Departement.ReadOnly);

            // Division
            Division.SetDbValue(rsnew, Division.CurrentValue, Division.ReadOnly);

            // Area
            Area.SetDbValue(rsnew, Area.CurrentValue, Area.ReadOnly);

            // Business_Area
            Business_Area.SetDbValue(rsnew, Business_Area.CurrentValue, Business_Area.ReadOnly);

            // Report_To
            Report_To.SetDbValue(rsnew, Report_To.CurrentValue, Report_To.ReadOnly);

            // Business_Area_Desc
            Business_Area_Desc.SetDbValue(rsnew, Business_Area_Desc.CurrentValue, Business_Area_Desc.ReadOnly);

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
            breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("MtBackupApproverList")), "", TableVar, true);
            string pageId = "edit";
            breadcrumb.Add("edit", pageId, url);
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

        // Set up starting record parameters
        public void SetupStartRecord()
        {
            // Exit if DisplayRecords = 0
            if (DisplayRecords == 0)
                return;
            string pageNo = Get(Config.TablePageNumber);
            string startRec = Get(Config.TableStartRec);
            bool infiniteScroll = false;
            string recordNo = !Empty(pageNo) ? pageNo : startRec; // Record number = page number or start record
            if (!Empty(recordNo) && IsNumeric(recordNo))
                StartRecord = ConvertToInt(recordNo);
            else
                StartRecord = StartRecordNumber;

            // Check if correct start record counter
            if (StartRecord <= 0) // Avoid invalid start record counter
                StartRecord = 1; // Reset start record counter
            else if (StartRecord > TotalRecords) // Avoid starting record > total records
                StartRecord = ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1; // Point to last page first record
            else if ((StartRecord - 1) % DisplayRecords != 0)
                StartRecord = ((StartRecord - 1) / DisplayRecords) * DisplayRecords + 1; // Point to page boundary
            if (!infiniteScroll)
                StartRecordNumber = StartRecord;
        }

        // Get page count
        public int PageCount
        {
            get {
                return ConvertToInt(Math.Ceiling((double)TotalRecords / DisplayRecords));
            }
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
