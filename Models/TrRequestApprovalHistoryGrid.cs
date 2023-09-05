namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// trRequestApprovalHistoryGrid
    /// </summary>
    public static TrRequestApprovalHistoryGrid trRequestApprovalHistoryGrid
    {
        get => HttpData.Get<TrRequestApprovalHistoryGrid>("trRequestApprovalHistoryGrid")!;
        set => HttpData["trRequestApprovalHistoryGrid"] = value;
    }

    /// <summary>
    /// Page class for tr_request_approval_history
    /// </summary>
    public class TrRequestApprovalHistoryGrid : TrRequestApprovalHistoryGridBase
    {
        // Constructor
        public TrRequestApprovalHistoryGrid() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TrRequestApprovalHistoryGridBase : TrRequestApprovalHistory
    {
        // Page ID
        public string PageID = "grid";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "tr_request_approval_history";

        // Page object name
        public string PageObjName = "trRequestApprovalHistoryGrid";

        // Title
        public string? Title = null; // Title for <title> tag

        // Grid form hidden field names
        public string FormName = "ftr_request_approval_historygrid";

        public string FormActionName = "";

        public string FormBlankRowName = "";

        public string FormKeyCountName = "";

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
        public TrRequestApprovalHistoryGridBase()
        {
            // CSS class name as context
            TableVar = "tr_request_approval_history";
            ContextClass = CheckClassName(TableVar);
            TableGridClass = AppendClass(TableGridClass, ContextClass);
            FormActionName = Config.FormRowActionName;
            FormBlankRowName = Config.FormBlankRowName;
            FormKeyCountName = Config.FormKeyCountName;

            // Initialize
            FormActionName += "_" + FormName;
            OldKeyName += "_" + FormName;
            FormBlankRowName += "_" + FormName;
            FormKeyCountName += "_" + FormName;

            // Table CSS class
            TableClass = "table table-bordered table-hover table-sm ew-table";

            // CSS class name as context
            ContextClass = CheckClassName(TableVar);
            TableGridClass = AppendClass(TableGridClass, ContextClass);

            // Language object
            Language = ResolveLanguage();
            AddUrl = "TrRequestApprovalHistoryAdd";

            // Open connection
            Conn = Connection; // DN

            // User table object (mt_user)
            UserTable = Resolve("usertable")!;
            UserTableConn = GetConnection(UserTable.DbId);

            // Other options
            OtherOptions["addedit"] = new () {
                TagClassName = "ew-add-edit-option",
                UseDropDownButton = false,
                DropDownButtonPhrase = "ButtonAddEdit",
                UseButtonGroup = true
            };
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
        public string PageName => "TrRequestApprovalHistoryGrid";

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
            _UserName.SetVisibility();
            Full_Name.SetVisibility();
            Position_Name.SetVisibility();
            Status_Approval.SetVisibility();
            Approval_Date.SetVisibility();
            Approval_Notes.SetVisibility();
            Reject_Notes.SetVisibility();
            Last_Update_By.SetVisibility();
            Created_By.SetVisibility();
            Created_Date.SetVisibility();
            ETL_Date.SetVisibility();
        }

        /// <summary>
        /// Terminate page
        /// </summary>
        /// <param name="url">URL to rediect to</param>
        /// <returns>Page result</returns>
        public override IActionResult Terminate(string url = "") { // DN
            if (_terminated) // DN
                return new EmptyResult();
            if (Empty(url))
                return new EmptyResult();
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
            if (IsAdd || IsCopy || IsGridAdd)
                id.Visible = false;
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
        private Pager? _pager; // DN

        public int SelectedCount = 0;

        public int SelectedIndex = 0;

        #pragma warning disable 169

        public bool ShowOtherOptions = false;

        private DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType>? _connection;
        #pragma warning restore 169

        public int DisplayRecords = 10; // Number of display records

        public int StartRecord;

        public int StopRecord;

        public int TotalRecords = -1;

        public int RecordRange = 10;

        public string PageSizes = "10,20,50,100,-1"; // Page sizes (comma separated)

        public string DefaultSearchWhere = ""; // Default search WHERE clause

        public string SearchWhere = ""; // Search WHERE clause

        public string SearchPanelClass = "ew-search-panel collapse show"; // Search panel class

        public int SearchColumnCount = 0; // For extended search

        public int SearchFieldsPerRow = 1; // For extended search

        public int RecordCount = 0; // Record count

        public int InlineRowCount = 0;

        public int StartRowCount = 1;

        public List<Tuple<string, Dictionary<string, string>>> Attributes = new (); // Row attributes and cell attributes

        public object RowIndex = 0; // Row index

        public int KeyCount = 0; // Key count

        public string MultiColumnGridClass = "row-cols-md";

        public string MultiColumnEditClass = "col-12 w-100";

        public string MultiColumnCardClass = "card h-100 ew-card";

        public string MultiColumnListOptionsPosition = "bottom-start";

        public string DbMasterFilter = ""; // Master filter

        public string DbDetailFilter = ""; // Detail filter

        public bool MasterRecordExists;

        public string MultiSelectKey = "";

        public string UserAction = ""; // User action

        public bool RestoreSearch = false;

        public SubPages? DetailPages; // Detail pages object

        public DbDataReader? Recordset;

        public List<string> RecordKeys = new ();

        public bool IsModal = false;

        private string FilterForModalActions = "";

        private bool UseInfiniteScroll = false;

        // Pager
        public Pager Pager
        {
            get {
                _pager ??= new PrevNextPager(this, StartRecord, RecordsPerPage, TotalRecords, PageSizes, RecordRange, AutoHidePager, AutoHidePageSizeSelector);
                return _pager;
            }
        }

        #pragma warning disable 618
        // Connection
        public override DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> Connection
        {
            get {
                _connection ??= GetConnection2(DbId);
                return _connection;
            }
        }
        #pragma warning restore 618

        /// <summary>
        /// Load recordset from filter
        /// <param name="filter">Record filter</param>
        /// </summary>
        public async Task LoadRecordsetFromFilter(string filter)
        {
            // Set up list options
            await SetupListOptions();

            // Load recordset
            TotalRecords = LoadRecordCount(filter);
            StartRecord = 1;
            StopRecord = DisplayRecords;
            CurrentFilter = filter;
            Recordset = await LoadRecordset();
        }

        #pragma warning disable 219
        /// <summary>
        /// Page run
        /// </summary>
        /// <returns>Page result</returns>
        public override async Task<IActionResult> Run()
        {
            // Multi column button position
            MultiColumnListOptionsPosition = Config.MultiColumnListOptionsPosition;
            DashboardReport = DashboardReport || Param<bool>(Config.PageDashboard);

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

            // Get grid add count
            int gridaddcnt = Get<int>(Config.TableGridAddRowCount);
            if (gridaddcnt > 0)
                GridAddRowCount = gridaddcnt;

            // Set up list options
            await SetupListOptions();
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

            // Set up master detail parameters
            SetupMasterParms();

            // Setup other options
            SetupOtherOptions();

            // Load default values for add
            LoadDefaultValues();

            // Update form name to avoid conflict
            if (IsModal)
                FormName = "ftr_request_approval_historygrid";

            // Set up infinite scroll
            UseInfiniteScroll = Param<bool>("infinitescroll");

            // Search filters
            string srchAdvanced = ""; // Advanced search filter
            string srchBasic = ""; // Basic search filter
            string filter = ""; // Filter
            string query = ""; // Query builder

            // Get command
            Command = Get("cmd").ToLower();

            // Set up records per page
            SetupDisplayRecords();

            // Handle reset command
            ResetCommand();

            // Hide list options
            if (IsExport()) {
                ListOptions.HideAllOptions(new () {"sequence"});
                ListOptions.UseDropDownButton = false; // Disable drop down button
                ListOptions.UseButtonGroup = false; // Disable button group
            } else if (IsGridAdd || IsGridEdit || IsMultiEdit || IsConfirm) {
                ListOptions.HideAllOptions();
                ListOptions.UseDropDownButton = false; // Disable drop down button
                ListOptions.UseButtonGroup = false; // Disable button group
            }

            // Show grid delete link for grid add / grid edit
            if (AllowAddDeleteRow) {
                if (IsGridAdd || IsGridEdit) {
                    var item = ListOptions["griddelete"];
                    if (item != null)
                        item.Visible = false;
                }
            }

            // Set up sorting order
            SetupSortOrder();

            // Restore display records
            if (Command != "json" && (RecordsPerPage == -1 || RecordsPerPage > 0)) {
                DisplayRecords = RecordsPerPage; // Restore from Session
            } else {
                DisplayRecords = 10; // Load default
                RecordsPerPage = DisplayRecords; // Save default to session
            }

            // Build filter
            filter = "";
            if (!Security.CanList)
                filter = "(0=1)"; // Filter all records

            // Restore master/detail filter from session
            DbMasterFilter = MasterFilterFromSession;
            DbDetailFilter = DetailFilterFromSession;
            AddFilter(ref filter, DbDetailFilter);
            AddFilter(ref filter, SearchWhere);

            // Load master record
            if (CurrentMode != "add" && !Empty(MasterFilterFromSession) && CurrentMasterTable == "v_tr_request") {
                vTrRequest = Resolve("v_tr_request")!;
                if (vTrRequest != null) {
                    using (var rsmaster = await vTrRequest.LoadReader(DbMasterFilter)) { // Note: Use {}
                        MasterRecordExists = rsmaster != null && await rsmaster.ReadAsync();
                        if (!MasterRecordExists) {
                            FailureMessage = Language.Phrase("NoRecord"); // Set no record found
                            return Terminate("VTrRequestList"); // Return to master page
                        } else {
                            vTrRequest.LoadListRowValues(rsmaster);
                        }
                    }
                    vTrRequest.RowType = RowType.Master; // Master row
                    await vTrRequest.RenderListRow(); // Note: Do it outside "using" // DN
                }
            }

            // Load master record
            if (CurrentMode != "add" && !Empty(MasterFilterFromSession) && CurrentMasterTable == "tr_request") {
                trRequest = Resolve("tr_request")!;
                if (trRequest != null) {
                    using (var rsmaster = await trRequest.LoadReader(DbMasterFilter)) { // Note: Use {}
                        MasterRecordExists = rsmaster != null && await rsmaster.ReadAsync();
                        if (!MasterRecordExists) {
                            FailureMessage = Language.Phrase("NoRecord"); // Set no record found
                            return Terminate("TrRequestList"); // Return to master page
                        } else {
                            trRequest.LoadListRowValues(rsmaster);
                        }
                    }
                    trRequest.RowType = RowType.Master; // Master row
                    await trRequest.RenderListRow(); // Note: Do it outside "using" // DN
                }
            }

            // Set up filter
            if (Command == "json") {
                UseSessionForListSql = false; // Do not use session for ListSql
                CurrentFilter = filter;
            } else {
                SessionWhere = filter;
                CurrentFilter = "";
            }
            Filter = ApplyUserIDFilters(filter);
            if (IsGridAdd) {
                if (CurrentMode == "copy") {
                    TotalRecords = await ListRecordCountAsync();
                    Recordset = await LoadRecordset(StartRecord - 1, TotalRecords);
                    StartRecord = 1;
                    DisplayRecords = TotalRecords;
                } else {
                    CurrentFilter = "0=1";
                    StartRecord = 1;
                    DisplayRecords = GridAddRowCount;
                }
                TotalRecords = DisplayRecords;
                StopRecord = DisplayRecords;
            } else if ((IsEdit || IsCopy || IsInlineInserted || IsInlineUpdated) && UseInfiniteScroll) { // Get current record only
                CurrentFilter = IsInlineUpdated ? GetRecordFilter() : GetFilterFromRecordKeys();
                TotalRecords = ListRecordCount();
                StartRecord = 1;
                StopRecord = DisplayRecords;
                Recordset = await LoadRecordset();
            } else if (
                UseInfiniteScroll && IsGridInserted ||
                UseInfiniteScroll && (IsGridEdit || IsGridUpdated) ||
                IsMultiEdit ||
                UseInfiniteScroll && IsMultiUpdated
            ) { // Get current records only
                CurrentFilter = FilterForModalActions; // Restore filter
                TotalRecords = ListRecordCount();
                StartRecord = 1;
                StopRecord = DisplayRecords;
                Recordset = await LoadRecordset();
            } else {
                TotalRecords = await ListRecordCountAsync();
                StopRecord = DisplayRecords;
                StartRecord = 1;
                DisplayRecords = TotalRecords; // Display all records

                // Recordset
                bool selectLimit = UseSelectLimit;
                if (selectLimit)
                    Recordset = await LoadRecordset(StartRecord - 1, DisplayRecords);
            }

            // API list action
            if (IsApi()) {
                if (CurrentPageName().EndsWith(Config.ApiListAction)) { // DN
                    if (!IsExport()) {
                        if (!Connection.SelectOffset && Recordset != null) { // DN
                            for (var i = 1; i <= StartRecord - 1; i++) // Move to first record
                                await Recordset.ReadAsync();
                        }
                        using (Recordset) {
                            return Controller.Json(new Dictionary<string, object> { {"success", true}, {TableVar, await GetRecordsFromRecordset(Recordset)}, {"totalRecordCount", TotalRecords}, {"version", Config.ProductVersion} });
                        }
                    }
                } else if (!Empty(FailureMessage)) {
                    return Controller.Json(new { success = false, error = GetFailureMessage() });
                }
                return new EmptyResult();
            }

            // Render other options
            RenderOtherOptions();

            // Set ReturnUrl in header if necessary
            if (TempData["Return-Url"] != null)
                AddHeader("Return-Url", ConvertToString(TempData["Return-Url"]));

            // Set LoginStatus, Page Rendering and Page Render
            if (!IsApi() && !IsTerminated) {
                // Pass login status to client side
                SetClientVar("login", LoginStatus);

                // Global Page Rendering event
                PageRendering();

                // Page Render event
                trRequestApprovalHistoryGrid?.PageRender();
            }
            return new EmptyResult();
        }
        #pragma warning restore 219

        // Get page number
        public int PageNumber => DisplayRecords > 0 && StartRecord > 0 ? ConvertToInt(Math.Ceiling((double)StartRecord / DisplayRecords)) : 1;

        // Set up number of records displayed per page
        protected void SetupDisplayRecords() {
            string wrk = Get(Config.TableRecordsPerPage);
            if (!Empty(wrk)) {
                if (IsNumeric(wrk)) {
                    DisplayRecords = ConvertToInt(wrk);
                } else {
                    if (SameText(wrk, "all")) { // Display all records
                        DisplayRecords = -1;
                    } else {
                        DisplayRecords = 10; // Non-numeric, load default
                    }
                }
                RecordsPerPage = DisplayRecords; // Save to Session
                // Reset start position
                StartRecord = 1;
                StartRecordNumber = StartRecord;
            }
        }

        // Exit inline mode
        protected void ClearInlineMode() {
            LastAction = CurrentAction; // Save last action
            CurrentAction = ""; // Clear action
            Session[Config.SessionInlineMode] = ""; // Clear inline mode
        }

        // Switch to grid add mode
        protected void GridAddMode() {
            CurrentAction = "gridadd";
            Session[Config.SessionInlineMode] = "gridadd"; // Enabled grid add
            HideFieldsForAddEdit();
        }

        // Switch to grid edit mode
        protected void GridEditMode() {
            CurrentAction = "gridedit";
            Session[Config.SessionInlineMode] = "gridedit"; // Enabled grid edit
            HideFieldsForAddEdit();
        }

        // Perform update to grid
        public async Task<bool> GridUpdate()
        {
            bool gridUpdate = true;

            // Get old recordset
            CurrentFilter = BuildKeyFilter();
            if (Empty(CurrentFilter))
                CurrentFilter = "0=1";
            string sql = CurrentSql;
            List<Dictionary<string, object>> rsold = await Connection.GetRowsAsync(sql);

            // Call Grid Updating event
            if (!GridUpdating(rsold)) {
                if (Empty(FailureMessage))
                    FailureMessage = Language.Phrase("GridEditCancelled"); // Set grid edit cancelled message
                EventCancelled = true;
                return false;
            }
            string wrkFilter = "";
            string key = "";

            // Update row index and get row key
            CurrentForm?.ResetIndex();
            int rowcnt = CurrentForm?.GetInt(FormKeyCountName) ?? 0;

            // Load default values for emptyRow checking // DN
            LoadDefaultValues();

            // Update all rows based on key
            try {
                for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {
                    CurrentForm!.Index = rowindex;
                    SetKey(CurrentForm.GetValue(OldKeyName));
                    string rowaction = CurrentForm.GetValue(FormActionName);

                    // Load all values and keys
                    if (rowaction != "insertdelete" && rowaction != "hide") { // Skip insert then deleted rows / hidden rows for grid edit
                        await LoadFormValues(); // Get form values
                        if (Empty(rowaction) || rowaction == "edit" || rowaction == "delete") {
                            gridUpdate = !Empty(OldKey); // Key must not be empty
                        } else {
                            gridUpdate = true;
                        }

                        // Skip empty row
                        if (rowaction == "insert" && EmptyRow()) {
                            // No action required
                        } else if (gridUpdate) { // Validate form and insert/update/delete record
                            if (rowaction == "delete") {
                                CurrentFilter = GetRecordFilter();
                                gridUpdate = await DeleteRows(); // Delete this row
                            } else {
                                if (rowaction == "insert") {
                                    gridUpdate = await AddRow(); // Insert this row
                                } else {
                                    if (!Empty(OldKey)) {
                                        SendEmail = false; // Do not send email on update success
                                        gridUpdate = await EditRow(); // Update this row
                                    }
                                } // End update
                                if (gridUpdate) // Get inserted or updated filter
                                    AddFilter(ref wrkFilter, GetRecordFilter(), "OR");
                            }
                        }
                        if (gridUpdate) {
                            if (!Empty(key))
                                key += ", ";
                            key += OldKey;
                        } else {
                            EventCancelled = true;
                            break;
                        }
                    }
                }
            } catch (Exception e) {
                FailureMessage = e.Message;
                gridUpdate = false;
            }
            if (gridUpdate) {
                FilterForModalActions = wrkFilter;

                // Get new recordset
                List<Dictionary<string, object>> rsnew = await Connection.GetRowsAsync(sql, true); // Use main connection (faster) // DN

                // Call Grid Updated event
                GridUpdated(rsold, rsnew);
                ClearInlineMode(); // Clear inline edit mode
            } else {
                if (Empty(FailureMessage))
                    FailureMessage = Language.Phrase("UpdateFailed"); // Set update failed message
            }
            return gridUpdate;
        }

        // Build filter for all keys
        protected string BuildKeyFilter() {
            string wrkFilter = "";

            // Update row index and get row key
            int rowindex = 1;
            CurrentForm!.Index = rowindex;
            string thisKey = CurrentForm.GetValue(OldKeyName);
            while (!Empty(thisKey)) {
                SetKey(thisKey);
                if (!Empty(OldKey)) {
                    string filter = GetRecordFilter();
                    if (!Empty(wrkFilter))
                        wrkFilter += " OR ";
                    wrkFilter += filter;
                } else {
                    wrkFilter = "0=1";
                    break;
                }

                // Update row index and get row key
                rowindex++; // next row
                CurrentForm!.Index = rowindex;
                thisKey = CurrentForm.GetValue(OldKeyName);
            }
            return wrkFilter;
        }

        // Perform Grid Add
        #pragma warning disable 168, 219

        public async Task<bool> GridInsert()
        {
            int addcnt = 0;
            bool gridInsert = false;

            // Call Grid Inserting event
            if (!GridInserting()) {
                if (Empty(FailureMessage))
                    FailureMessage = Language.Phrase("GridAddCancelled"); // Set grid add cancelled message
                EventCancelled = true;
                return false;
            }

            // Init key filter
            string wrkFilter = "";
            string key = "";

            // Get row count
            CurrentForm?.ResetIndex();
            int rowcnt = CurrentForm?.GetInt(FormKeyCountName) ?? 0;

            // Load default values for emptyRow checking // DN
            LoadDefaultValues();

            // Insert all rows
            try {
                for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {
                    // Load current row values
                    CurrentForm!.Index = rowindex;
                    string rowaction = CurrentForm.GetValue(FormActionName);
                    Dictionary<string, object>? rsold = null;
                    if (!Empty(rowaction) && rowaction != "insert")
                        continue; // Skip
                    if (rowaction == "insert") {
                        OldKey = CurrentForm.GetValue(OldKeyName);
                        rsold = await LoadOldRecord(); // Load old record
                    }
                    await LoadFormValues(); // Get form values
                    if (!EmptyRow()) {
                        addcnt++;
                        SendEmail = false; // Do not send email on insert success
                        gridInsert = await AddRow(rsold); // Insert row (already validated by validateGridForm())
                        if (gridInsert) {
                            if (!Empty(key))
                                key += Config.CompositeKeySeparator;
                            key += ConvertToString(id.CurrentValue);

                            // Add filter for this record
                            AddFilter(ref wrkFilter, GetRecordFilter(), "OR");
                        } else {
                            EventCancelled = true;
                            break;
                        }
                    }
                }
                if (addcnt == 0) { // No record inserted
                    ClearInlineMode(); // Clear grid add mode and return
                    return true;
                }
            } catch (Exception e) {
                FailureMessage = e.Message;
                gridInsert = false;
            }
            if (gridInsert) {
                // Get new recordset
                CurrentFilter = wrkFilter;
                FilterForModalActions = wrkFilter;
                string sql = CurrentSql;
                List<Dictionary<string, object>> rsnew = await Connection.GetRowsAsync(sql, true); // Use main connection (faster) // DN

                // Call Grid Inserted event
                GridInserted(rsnew);
                ClearInlineMode(); // Clear grid add mode
            } else {
                if (Empty(FailureMessage))
                    FailureMessage = Language.Phrase("InsertFailed"); // Set insert failed message
            }
            return gridInsert;
        }
        #pragma warning restore 168, 219

        // Check if empty row
        public bool EmptyRow()
        {
            if (CurrentForm == null)
                return true;
            if (CurrentForm.HasValue("x__UserName") && CurrentForm.HasValue("o__UserName") && !SameString(_UserName.CurrentValue, _UserName.DefaultValue) &&
            !(_UserName.IsForeignKey && CurrentMasterTable != "" && SameString(_UserName.CurrentValue, _UserName.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Full_Name") && CurrentForm.HasValue("o_Full_Name") && !SameString(Full_Name.CurrentValue, Full_Name.DefaultValue) &&
            !(Full_Name.IsForeignKey && CurrentMasterTable != "" && SameString(Full_Name.CurrentValue, Full_Name.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Position_Name") && CurrentForm.HasValue("o_Position_Name") && !SameString(Position_Name.CurrentValue, Position_Name.DefaultValue) &&
            !(Position_Name.IsForeignKey && CurrentMasterTable != "" && SameString(Position_Name.CurrentValue, Position_Name.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Status_Approval") && CurrentForm.HasValue("o_Status_Approval") && !SameString(Status_Approval.CurrentValue, Status_Approval.DefaultValue) &&
            !(Status_Approval.IsForeignKey && CurrentMasterTable != "" && SameString(Status_Approval.CurrentValue, Status_Approval.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Approval_Date") && CurrentForm.HasValue("o_Approval_Date") && !SameString(FormatDateTime(Approval_Date.CurrentValue, Approval_Date.FormatPattern), FormatDateTime(Approval_Date.DefaultValue, Approval_Date.FormatPattern)) &&
            !(Approval_Date.IsForeignKey && CurrentMasterTable != "" && SameString(FormatDateTime(Approval_Date.CurrentValue, Approval_Date.FormatPattern), FormatDateTime(Approval_Date.SessionValue, Approval_Date.FormatPattern))))
                return false;
            if (CurrentForm.HasValue("x_Approval_Notes") && CurrentForm.HasValue("o_Approval_Notes") && !SameString(Approval_Notes.CurrentValue, Approval_Notes.DefaultValue) &&
            !(Approval_Notes.IsForeignKey && CurrentMasterTable != "" && SameString(Approval_Notes.CurrentValue, Approval_Notes.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Reject_Notes") && CurrentForm.HasValue("o_Reject_Notes") && !SameString(Reject_Notes.CurrentValue, Reject_Notes.DefaultValue) &&
            !(Reject_Notes.IsForeignKey && CurrentMasterTable != "" && SameString(Reject_Notes.CurrentValue, Reject_Notes.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Last_Update_By") && CurrentForm.HasValue("o_Last_Update_By") && !SameString(FormatDateTime(Last_Update_By.CurrentValue, Last_Update_By.FormatPattern), FormatDateTime(Last_Update_By.DefaultValue, Last_Update_By.FormatPattern)) &&
            !(Last_Update_By.IsForeignKey && CurrentMasterTable != "" && SameString(FormatDateTime(Last_Update_By.CurrentValue, Last_Update_By.FormatPattern), FormatDateTime(Last_Update_By.SessionValue, Last_Update_By.FormatPattern))))
                return false;
            if (CurrentForm.HasValue("x_Created_By") && CurrentForm.HasValue("o_Created_By") && !SameString(Created_By.CurrentValue, Created_By.DefaultValue) &&
            !(Created_By.IsForeignKey && CurrentMasterTable != "" && SameString(Created_By.CurrentValue, Created_By.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Created_Date") && CurrentForm.HasValue("o_Created_Date") && !SameString(FormatDateTime(Created_Date.CurrentValue, Created_Date.FormatPattern), FormatDateTime(Created_Date.DefaultValue, Created_Date.FormatPattern)) &&
            !(Created_Date.IsForeignKey && CurrentMasterTable != "" && SameString(FormatDateTime(Created_Date.CurrentValue, Created_Date.FormatPattern), FormatDateTime(Created_Date.SessionValue, Created_Date.FormatPattern))))
                return false;
            if (CurrentForm.HasValue("x_ETL_Date") && CurrentForm.HasValue("o_ETL_Date") && !SameString(FormatDateTime(ETL_Date.CurrentValue, ETL_Date.FormatPattern), FormatDateTime(ETL_Date.DefaultValue, ETL_Date.FormatPattern)) &&
            !(ETL_Date.IsForeignKey && CurrentMasterTable != "" && SameString(FormatDateTime(ETL_Date.CurrentValue, ETL_Date.FormatPattern), FormatDateTime(ETL_Date.SessionValue, ETL_Date.FormatPattern))))
                return false;
            return true;
        }

        // Validate grid form
        public async Task<bool> ValidateGridForm()
        {
            // Get row count
            CurrentForm?.ResetIndex();
            int rowcnt = CurrentForm?.GetInt(FormKeyCountName) ?? 0;

            // Load default values for emptyRow checking
            LoadDefaultValues();

            // Validate all records
            for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {
                // Load current row values
                CurrentForm!.Index = rowindex;
                string rowaction = CurrentForm.GetValue(FormActionName);
                if (rowaction != "delete" && rowaction != "insertdelete" && rowaction != "hide") {
                    await LoadFormValues(); // Get form values
                    if (rowaction == "insert" && EmptyRow()) {
                        // Ignore
                    } else if (!await ValidateForm()) {
                        return false;
                    }
                }
            }
            return true;
        }

        // Get all form values of the grid
        public List<Dictionary<string, string?>> GetGridFormValues()
        {
            // Get row count
            CurrentForm?.ResetIndex();
            int rowcnt = CurrentForm?.GetInt(FormKeyCountName) ?? 0;
            List<Dictionary<string, string?>> rows = new ();

            // Loop through all records
            for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {
                // Load current row values
                CurrentForm!.Index = rowindex;
                string rowaction = CurrentForm.GetValue(FormActionName);
                if (rowaction != "delete" && rowaction != "insertdelete") {
                    LoadFormValues().GetAwaiter().GetResult(); // Load form values (sync)
                    if (rowaction == "insert" && EmptyRow()) {
                        // Ignore
                    } else {
                        rows.Add(GetFormValues()); // Return row as array
                    }
                }
            }
            return rows; // Return as array of array
        }

        // Restore form values for current row
        public async Task RestoreCurrentRowFormValues(object index)
        {
            // Get row based on current index
            if (index is int idx)
                CurrentForm!.Index = idx;
            string rowaction = CurrentForm.GetValue(FormActionName);
            await LoadFormValues(); // Load form values
            // Set up invalid status correctly
            ResetFormError();
            if (rowaction == "insert" && EmptyRow()) {
                // Ignore
            } else {
                await ValidateForm();
            }
        }

        // Reset form status
        public void ResetFormError()
        {
            _UserName.ClearErrorMessage();
            Full_Name.ClearErrorMessage();
            Position_Name.ClearErrorMessage();
            Status_Approval.ClearErrorMessage();
            Approval_Date.ClearErrorMessage();
            Approval_Notes.ClearErrorMessage();
            Reject_Notes.ClearErrorMessage();
            Last_Update_By.ClearErrorMessage();
            Created_By.ClearErrorMessage();
            Created_Date.ClearErrorMessage();
            ETL_Date.ClearErrorMessage();
        }

        // Set up sort parameters
        protected void SetupSortOrder() {
            // Load default Sorting Order
            if (Command != "json") {
                string defaultSort = ""; // Set up default sort
                if (Empty(SessionOrderBy) && !Empty(defaultSort))
                    SessionOrderBy = defaultSort;
            }

            // Check for "order" parameter
            if (Get("order", out StringValues sv)) {
                CurrentOrder = sv.ToString();
                CurrentOrderType = Get("ordertype");
                StartRecordNumber = 1; // Reset start position
            }

            // Update field sort
            UpdateFieldSort();
        }

        /// <summary>
        /// Reset command
        /// cmd=reset (Reset search parameters)
        /// cmd=resetall (Reset search and master/detail parameters)
        /// cmd=resetsort (Reset sort parameters)
        /// </summary>
        protected void ResetCommand() {
            // Get reset cmd
            if (Command.ToLower().StartsWith("reset")) {
                // Reset master/detail keys
                if (SameText(Command, "resetall")) {
                    CurrentMasterTable = ""; // Clear master table
                    DbMasterFilter = "";
                    DbDetailFilter = "";
                    request_id.SessionValue = "";
                    request_id.SessionValue = "";
                }

                // Reset (clear) sorting order
                if (SameText(Command, "resetsort")) {
                    string orderBy = "";
                    SessionOrderBy = orderBy;
                }

                // Reset start position
                StartRecord = 1;
                StartRecordNumber = StartRecord;
            }
        }

        #pragma warning disable 1998
        // Set up list options
        protected async Task SetupListOptions() {
            ListOption item;

            // "griddelete"
            if (AllowAddDeleteRow) {
                item = ListOptions.Add("griddelete");
                item.CssClass = "text-nowrap";
                item.OnLeft = true;
                item.Visible = false; // Default hidden
            }

            // Add group option item
            item = ListOptions.AddGroupOption();
            item.Body = "";
            item.OnLeft = true;
            item.Visible = false;

            // "view"
            item = ListOptions.Add("view");
            item.CssClass = "text-nowrap";
            item.Visible = Security.CanView;
            item.OnLeft = true;

            // Drop down button for ListOptions
            ListOptions.UseDropDownButton = true;
            ListOptions.DropDownButtonPhrase = "ButtonListOptions";
            ListOptions.UseButtonGroup = false;
            if (ListOptions.UseButtonGroup && IsMobile())
                ListOptions.UseDropDownButton = true;

            //ListOptions.ButtonClass = ""; // Class for button group
            ListOptions[ListOptions.GroupOptionName]?.SetVisible(ListOptions.GroupOptionVisible);
        }
        #pragma warning restore 1998

        // Set up list options (extensions)
        protected void SetupListOptionsExt() {
            // Preview extension
            ListOptions.HideDetailItemsForDropDown(); // Hide detail items for dropdown if necessary
        }

        // Add "hash" parameter to URL
        public string UrlAddHash(string url, string hash)
        {
            return UseAjaxActions ? url : UrlAddQuery(url, "hash=" + hash);
        }

        // Render list options
        #pragma warning disable 168, 219, 1998

        public async Task RenderListOptions()
        {
            ListOption? listOption;
            bool isVisible = false; // DN
            ListOptions.LoadDefault();

            // Call ListOptions Rendering event
            ListOptionsRendering();

            // Set up row action and key
            if (IsNumeric(RowIndex) && RowType != RowType.View) {
                CurrentForm!.Index = ConvertToInt(RowIndex);
                var actionName = FormActionName.Replace("k_", "k" + ConvertToString(RowIndex) + "_");
                var oldKeyName = OldKeyName.Replace("k_", "k" + ConvertToString(RowIndex) + "_");
                var blankRowName = FormBlankRowName.Replace("k_", "k" + ConvertToString(RowIndex) + "_");
                if (!Empty(RowAction))
                    MultiSelectKey += "<input type=\"hidden\" name=\"" + actionName + "\" id=\"" + actionName + "\" value=\"" + RowAction + "\">";
                string oldKey = GetKey(false); // Get from OldValue
                if (!Empty(oldKeyName) && !Empty(oldKey)) {
                    MultiSelectKey += "<input type=\"hidden\" name=\"" + oldKeyName + "\" id=\"" + oldKeyName + "\" value=\"" + HtmlEncode(oldKey) + "\">";
                }
                if (RowAction == "insert" && IsConfirm && EmptyRow())
                    MultiSelectKey += "<input type=\"hidden\" name=\"" + blankRowName + "\" id=\"" + blankRowName + "\" value=\"1\">";
            }

            // "delete"
            if (AllowAddDeleteRow) {
                if (CurrentMode == "add" || CurrentMode == "copy" || CurrentMode == "edit") {
                    var options = ListOptions;
                    options.UseButtonGroup = true; // Use button group for grid delete button
                    listOption = options["griddelete"];
                    if (IsNumeric(RowIndex) && (RowAction == "" || RowAction == "edit")) { // Do not allow delete existing record
                        listOption?.SetBody("&nbsp;");
                    } else {
                        listOption?.SetBody("<a class=\"ew-grid-link ew-grid-delete\" title=\"" + Language.Phrase("DeleteLink", true) + "\" data-caption=\"" + Language.Phrase("DeleteLink", true) + "\" data-ew-action=\"delete-grid-row\" data-rowindex=\"" + RowIndex + "\">" + Language.Phrase("DeleteLink") + "</a>");
                    }
                }
            }
            if (CurrentMode == "view") {
            // "view"
            listOption = ListOptions["view"];
            string viewcaption = Language.Phrase("ViewLink", true);
            isVisible = Security.CanView;
            if (isVisible) {
                if (ModalView && !IsMobile())
                    listOption?.SetBody($@"<a class=""ew-row-link ew-view"" title=""{viewcaption}"" data-table=""tr_request_approval_history"" data-caption=""{viewcaption}"" data-ew-action=""modal"" data-action=""view"" data-ajax=""" + (UseAjaxActions ? "true" : "false") + "\" data-url=\"" + HtmlEncode(AppPath(ViewUrl)) + "\" data-btn=\"null\">" + Language.Phrase("ViewLink") + "</a>");
                else
                    listOption?.SetBody($@"<a class=""ew-row-link ew-view"" title=""{viewcaption}"" data-caption=""{viewcaption}"" href=""" + HtmlEncode(AppPath(ViewUrl)) + "\">" + Language.Phrase("ViewLink") + "</a>");
            } else {
                listOption?.Clear();
            }
            } // End View mode
            RenderListOptionsExt();

            // Call ListOptions Rendered event
            ListOptionsRendered();
        }

        // Render list options (extensions)
        protected void RenderListOptionsExt() {
            // Render list options (to be implemented by extensions)
        }

        // Set up other options
        protected void SetupOtherOptions() {
            ListOptions option;
            ListOption item;
            option = OtherOptions["addedit"];
            option.UseDropDownButton = false;
            option.DropDownButtonPhrase = "ButtonAddEdit";
            option.UseButtonGroup = true;
            option.ButtonClass = ""; // Class for button group
            item = option.AddGroupOption();
            item.Body = "";
            item.Visible = false;
        }

        // Create new column option // DN
        public void CreateColumnOption(ListOption item)
        {
            var field = FieldByName(item.Name);
            if (field?.Visible ?? false) {
                item.Body = "<button class=\"dropdown-item\">" +
                    "<div class=\"form-check ew-dropdown-checkbox\">" +
                    "<div class=\"form-check-input ew-dropdown-check-input\" data-field=\"" + field.Param + "\"></div>" +
                    "<label class=\"form-check-label ew-dropdown-check-label\">" + field.Caption + "</label></div></button>";
            }
        }

        // Render other options
        public void RenderOtherOptions()
        {
            ListOptions option;
            ListOption? item;
            var options = OtherOptions;
                if ((CurrentMode == "add" || CurrentMode == "copy" || CurrentMode == "edit") && !IsConfirm) { // Check add/copy/edit mode
                    if (AllowAddDeleteRow) {
                        option = options["addedit"];
                        option.UseDropDownButton = false;
                        item = option.Add("addblankrow");
                        item.Body = "<a class=\"ew-add-edit ew-add-blank-row\" title=\"" + Language.Phrase("AddBlankRow", true) + "\" data-caption=\"" + Language.Phrase("AddBlankRow", true) + "\" data-ew-action=\"add-grid-row\">" + Language.Phrase("AddBlankRow") + "</a>";
                        item.Visible = false;
                        ShowOtherOptions = item.Visible;
                    }
                }
                if (CurrentMode == "view") { // Check view mode
                    option = options["addedit"];
                    item = option.GetItem("add");
                    ShowOtherOptions = !Empty(item) && item.Visible;
                }
        }

        // Set up Grid
        public async Task SetupGrid()
        {
            StartRecord = 1;
            StopRecord = TotalRecords; // Show all records

            // Restore number of post back records
            if (CurrentForm != null && (IsConfirm || EventCancelled)) {
                CurrentForm!.ResetIndex(); // DN
                if (CurrentForm!.HasValue(FormKeyCountName) && (IsGridAdd || IsGridEdit || IsConfirm)) {
                    KeyCount = CurrentForm.GetInt(FormKeyCountName);
                    StopRecord = StartRecord + KeyCount - 1;
                }
            }
            if (Recordset != null && Recordset.HasRows) {
                if (!Connection.SelectOffset) { // DN
                    for (int i = 1; i <= StartRecord - 1; i++) { // Move to first record
                        if (await Recordset.ReadAsync())
                            RecordCount++;
                    }
                } else {
                    RecordCount = StartRecord - 1;
                }
            } else if (IsGridAdd && !AllowAddDeleteRow && StopRecord == 0) { // Grid-Add with no records
                StopRecord = GridAddRowCount;
            } else if (IsAdd && TotalRecords == 0) { // Inline-Add with no records
                StopRecord = 1;
            }

            // Initialize aggregate
            RowType = RowType.AggregateInit;
            ResetAttributes();
            await RenderRow();
            if ((IsGridAdd || IsGridEdit)) // Render template row first
                RowIndex = "$rowindex$";
        }

        // Set up Row
        public async Task SetupRow()
        {
            if (IsGridAdd || IsGridEdit) {
                if (SameString(RowIndex, "$rowindex$")) { // Render template row first
                    await LoadRowValues();

                    // Set row properties
                    ResetAttributes();
                    RowAttrs.Add("data-rowindex", ConvertToString(RowIndex));
                    RowAttrs.Add("id", "r0_tr_request_approval_history");
                    RowAttrs.Add("data-rowtype", ConvertToString((int)RowType.Add));
                    RowAttrs.Add("data-inline", (IsAdd || IsCopy || IsEdit) ? "true" : "false");
                    RowAttrs.AppendClass("ew-template");

                    // Render row
                    RowType = RowType.Add;
                    await RenderRow();

                    // Render list options
                    await RenderListOptions();

                    // Reset record count for template row
                    RecordCount--;
                    return;
                }
            }
            if (CurrentForm != null && (IsGridAdd || IsGridEdit || IsConfirm || IsMultiEdit)) {
                RowIndex = ConvertToInt(RowIndex) + 1;
                CurrentForm!.SetIndex(ConvertToInt(RowIndex));
                if (CurrentForm!.HasValue(FormActionName) && (IsConfirm || EventCancelled))
                    RowAction = CurrentForm.GetValue(FormActionName);
                else if (IsGridAdd)
                    RowAction = "insert";
                else
                    RowAction = "";
            }

            // Set up key count
            KeyCount = ConvertToInt(RowIndex);

            // Init row class and style
            ResetAttributes();
            CssClass = "";
            if (IsGridAdd) {
                if (CurrentMode == "copy") {
                    await LoadRowValues(Recordset); // Load row values
                    OldKey = GetKey(true); // Get from CurrentValue
                } else {
                    await LoadRowValues(); // Load default values
                    OldKey = "";
                }
            } else {
                await LoadRowValues(Recordset); // Load row values
                OldKey = GetKey(true); // Get from CurrentValue
            }
            SetKey(OldKey);
            RowType = RowType.View; // Render view
            if ((IsAdd || IsCopy) && InlineRowCount == 0 || IsGridAdd) // Add
                RowType = RowType.Add; // Render add
            if (IsGridAdd && EventCancelled && !CurrentForm!.HasValue(FormBlankRowName)) // Insert failed
                await RestoreCurrentRowFormValues(RowIndex); // Restore form values
            if (IsGridEdit) { // Grid edit
                if (EventCancelled)
                    await RestoreCurrentRowFormValues(RowIndex); // Restore form values
                if (RowAction == "insert")
                    RowType = RowType.Add; // Render add
                else
                    RowType = RowType.Edit; // Render edit
            }
            if (IsGridEdit && (RowType == RowType.Edit || RowType == RowType.Add) && EventCancelled) // Update failed
                await RestoreCurrentRowFormValues(RowIndex); // Restore form values
            if (IsConfirm) // Confirm row
                await RestoreCurrentRowFormValues(RowIndex); // Restore form values

            // Inline Add/Copy row (row 0)
            if (RowType == RowType.Add && (IsAdd || IsCopy)) {
                InlineRowCount++;
                RecordCount--; // Reset record count for inline add/copy row
                if (TotalRecords == 0) // Reset stop record if no records
                    StopRecord = 0;
            } else {
                // Inline Edit row
                if (RowType == RowType.Edit && IsEdit)
                    InlineRowCount++;
                RowCount++; // Increment row count
            }

            // Set up row attributes
            RowAttrs.Add("data-rowindex", ConvertToString(trRequestApprovalHistoryGrid.RowCount));
            RowAttrs.Add("data-key", GetKey(true));
            RowAttrs.Add("id", "r" + ConvertToString(trRequestApprovalHistoryGrid.RowCount) + "_tr_request_approval_history");
            RowAttrs.Add("data-rowtype", ConvertToString((int)RowType));
            RowAttrs.AppendClass(trRequestApprovalHistoryGrid.RowCount % 2 != 1 ? "ew-table-alt-row" : "");
            if (IsAdd && trRequestApprovalHistoryGrid.RowType == RowType.Add || IsEdit && trRequestApprovalHistoryGrid.RowType == RowType.Edit) // Inline-Add/Edit row
                RowAttrs.AppendClass("table-active");

            // Render row
            await RenderRow();

            // Render list options
            await RenderListOptions();
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
            CurrentForm.FormName = FormName;
            bool validate = !Config.ServerValidate;
            string val;

            // Check field name 'UserName' before field var 'x__UserName'
            val = CurrentForm.HasValue("UserName") ? CurrentForm.GetValue("UserName") : CurrentForm.GetValue("x__UserName");
            if (!_UserName.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("UserName") && !CurrentForm.HasValue("x__UserName")) // DN
                    _UserName.Visible = false; // Disable update for API request
                else
                    _UserName.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o__UserName"))
                _UserName.OldValue = CurrentForm.GetValue("o__UserName");

            // Check field name 'Full_Name' before field var 'x_Full_Name'
            val = CurrentForm.HasValue("Full_Name") ? CurrentForm.GetValue("Full_Name") : CurrentForm.GetValue("x_Full_Name");
            if (!Full_Name.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Full_Name") && !CurrentForm.HasValue("x_Full_Name")) // DN
                    Full_Name.Visible = false; // Disable update for API request
                else
                    Full_Name.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Full_Name"))
                Full_Name.OldValue = CurrentForm.GetValue("o_Full_Name");

            // Check field name 'Position_Name' before field var 'x_Position_Name'
            val = CurrentForm.HasValue("Position_Name") ? CurrentForm.GetValue("Position_Name") : CurrentForm.GetValue("x_Position_Name");
            if (!Position_Name.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Position_Name") && !CurrentForm.HasValue("x_Position_Name")) // DN
                    Position_Name.Visible = false; // Disable update for API request
                else
                    Position_Name.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Position_Name"))
                Position_Name.OldValue = CurrentForm.GetValue("o_Position_Name");

            // Check field name 'Status_Approval' before field var 'x_Status_Approval'
            val = CurrentForm.HasValue("Status_Approval") ? CurrentForm.GetValue("Status_Approval") : CurrentForm.GetValue("x_Status_Approval");
            if (!Status_Approval.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Status_Approval") && !CurrentForm.HasValue("x_Status_Approval")) // DN
                    Status_Approval.Visible = false; // Disable update for API request
                else
                    Status_Approval.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Status_Approval"))
                Status_Approval.OldValue = CurrentForm.GetValue("o_Status_Approval");

            // Check field name 'Approval_Date' before field var 'x_Approval_Date'
            val = CurrentForm.HasValue("Approval_Date") ? CurrentForm.GetValue("Approval_Date") : CurrentForm.GetValue("x_Approval_Date");
            if (!Approval_Date.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Approval_Date") && !CurrentForm.HasValue("x_Approval_Date")) // DN
                    Approval_Date.Visible = false; // Disable update for API request
                else
                    Approval_Date.SetFormValue(val, true, validate);
                Approval_Date.CurrentValue = UnformatDateTime(Approval_Date.CurrentValue, Approval_Date.FormatPattern);
            }
            Approval_Date.OldValue = UnformatDateTime(CurrentForm.GetValue("o_Approval_Date"), Approval_Date.FormatPattern);

            // Check field name 'Approval_Notes' before field var 'x_Approval_Notes'
            val = CurrentForm.HasValue("Approval_Notes") ? CurrentForm.GetValue("Approval_Notes") : CurrentForm.GetValue("x_Approval_Notes");
            if (!Approval_Notes.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Approval_Notes") && !CurrentForm.HasValue("x_Approval_Notes")) // DN
                    Approval_Notes.Visible = false; // Disable update for API request
                else
                    Approval_Notes.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Approval_Notes"))
                Approval_Notes.OldValue = CurrentForm.GetValue("o_Approval_Notes");

            // Check field name 'Reject_Notes' before field var 'x_Reject_Notes'
            val = CurrentForm.HasValue("Reject_Notes") ? CurrentForm.GetValue("Reject_Notes") : CurrentForm.GetValue("x_Reject_Notes");
            if (!Reject_Notes.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Reject_Notes") && !CurrentForm.HasValue("x_Reject_Notes")) // DN
                    Reject_Notes.Visible = false; // Disable update for API request
                else
                    Reject_Notes.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Reject_Notes"))
                Reject_Notes.OldValue = CurrentForm.GetValue("o_Reject_Notes");

            // Check field name 'Last_Update_By' before field var 'x_Last_Update_By'
            val = CurrentForm.HasValue("Last_Update_By") ? CurrentForm.GetValue("Last_Update_By") : CurrentForm.GetValue("x_Last_Update_By");
            if (!Last_Update_By.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Last_Update_By") && !CurrentForm.HasValue("x_Last_Update_By")) // DN
                    Last_Update_By.Visible = false; // Disable update for API request
                else
                    Last_Update_By.SetFormValue(val, true, validate);
                Last_Update_By.CurrentValue = UnformatDateTime(Last_Update_By.CurrentValue, Last_Update_By.FormatPattern);
            }
            Last_Update_By.OldValue = UnformatDateTime(CurrentForm.GetValue("o_Last_Update_By"), Last_Update_By.FormatPattern);

            // Check field name 'Created_By' before field var 'x_Created_By'
            val = CurrentForm.HasValue("Created_By") ? CurrentForm.GetValue("Created_By") : CurrentForm.GetValue("x_Created_By");
            if (!Created_By.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Created_By") && !CurrentForm.HasValue("x_Created_By")) // DN
                    Created_By.Visible = false; // Disable update for API request
                else
                    Created_By.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Created_By"))
                Created_By.OldValue = CurrentForm.GetValue("o_Created_By");

            // Check field name 'Created_Date' before field var 'x_Created_Date'
            val = CurrentForm.HasValue("Created_Date") ? CurrentForm.GetValue("Created_Date") : CurrentForm.GetValue("x_Created_Date");
            if (!Created_Date.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Created_Date") && !CurrentForm.HasValue("x_Created_Date")) // DN
                    Created_Date.Visible = false; // Disable update for API request
                else
                    Created_Date.SetFormValue(val, true, validate);
                Created_Date.CurrentValue = UnformatDateTime(Created_Date.CurrentValue, Created_Date.FormatPattern);
            }
            Created_Date.OldValue = UnformatDateTime(CurrentForm.GetValue("o_Created_Date"), Created_Date.FormatPattern);

            // Check field name 'ETL_Date' before field var 'x_ETL_Date'
            val = CurrentForm.HasValue("ETL_Date") ? CurrentForm.GetValue("ETL_Date") : CurrentForm.GetValue("x_ETL_Date");
            if (!ETL_Date.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("ETL_Date") && !CurrentForm.HasValue("x_ETL_Date")) // DN
                    ETL_Date.Visible = false; // Disable update for API request
                else
                    ETL_Date.SetFormValue(val, true, validate);
                ETL_Date.CurrentValue = UnformatDateTime(ETL_Date.CurrentValue, ETL_Date.FormatPattern);
            }
            ETL_Date.OldValue = UnformatDateTime(CurrentForm.GetValue("o_ETL_Date"), ETL_Date.FormatPattern);

            // Check field name 'id' before field var 'x_id'
            val = CurrentForm.HasValue("id") ? CurrentForm.GetValue("id") : CurrentForm.GetValue("x_id");
            if (!id.IsDetailKey && !IsGridAdd && !IsAdd)
                id.SetFormValue(val);
        }
        #pragma warning restore 1998

        // Restore form values
        public void RestoreFormValues()
        {
            if (!IsGridAdd && !IsAdd)
                id.CurrentValue = id.FormValue;
            _UserName.CurrentValue = _UserName.FormValue;
            Full_Name.CurrentValue = Full_Name.FormValue;
            Position_Name.CurrentValue = Position_Name.FormValue;
            Status_Approval.CurrentValue = Status_Approval.FormValue;
            Approval_Date.CurrentValue = Approval_Date.FormValue;
            Approval_Date.CurrentValue = UnformatDateTime(Approval_Date.CurrentValue, Approval_Date.FormatPattern);
            Approval_Notes.CurrentValue = Approval_Notes.FormValue;
            Reject_Notes.CurrentValue = Reject_Notes.FormValue;
            Last_Update_By.CurrentValue = Last_Update_By.FormValue;
            Last_Update_By.CurrentValue = UnformatDateTime(Last_Update_By.CurrentValue, Last_Update_By.FormatPattern);
            Created_By.CurrentValue = Created_By.FormValue;
            Created_Date.CurrentValue = Created_Date.FormValue;
            Created_Date.CurrentValue = UnformatDateTime(Created_Date.CurrentValue, Created_Date.FormatPattern);
            ETL_Date.CurrentValue = ETL_Date.FormValue;
            ETL_Date.CurrentValue = UnformatDateTime(ETL_Date.CurrentValue, ETL_Date.FormatPattern);
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
            request_id.SetDbValue(row["request_id"]);
            _UserName.SetDbValue(row["UserName"]);
            Full_Name.SetDbValue(row["Full_Name"]);
            Position_Name.SetDbValue(row["Position_Name"]);
            Status_Approval.SetDbValue(row["Status_Approval"]);
            Approval_Date.SetDbValue(row["Approval_Date"]);
            Approval_Notes.SetDbValue(row["Approval_Notes"]);
            Reject_Notes.SetDbValue(row["Reject_Notes"]);
            Last_Update_By.SetDbValue(row["Last_Update_By"]);
            Created_By.SetDbValue(row["Created_By"]);
            Created_Date.SetDbValue(row["Created_Date"]);
            ETL_Date.SetDbValue(row["ETL_Date"]);
        }
        #pragma warning restore 162, 168, 1998, 4014

        // Return a row with default values
        protected Dictionary<string, object> NewRow() {
            var row = new Dictionary<string, object>();
            row.Add("id", id.DefaultValue ?? DbNullValue); // DN
            row.Add("request_id", request_id.DefaultValue ?? DbNullValue); // DN
            row.Add("UserName", _UserName.DefaultValue ?? DbNullValue); // DN
            row.Add("Full_Name", Full_Name.DefaultValue ?? DbNullValue); // DN
            row.Add("Position_Name", Position_Name.DefaultValue ?? DbNullValue); // DN
            row.Add("Status_Approval", Status_Approval.DefaultValue ?? DbNullValue); // DN
            row.Add("Approval_Date", Approval_Date.DefaultValue ?? DbNullValue); // DN
            row.Add("Approval_Notes", Approval_Notes.DefaultValue ?? DbNullValue); // DN
            row.Add("Reject_Notes", Reject_Notes.DefaultValue ?? DbNullValue); // DN
            row.Add("Last_Update_By", Last_Update_By.DefaultValue ?? DbNullValue); // DN
            row.Add("Created_By", Created_By.DefaultValue ?? DbNullValue); // DN
            row.Add("Created_Date", Created_Date.DefaultValue ?? DbNullValue); // DN
            row.Add("ETL_Date", ETL_Date.DefaultValue ?? DbNullValue); // DN
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

            // request_id

            // UserName

            // Full_Name

            // Position_Name

            // Status_Approval

            // Approval_Date

            // Approval_Notes

            // Reject_Notes

            // Last_Update_By

            // Created_By

            // Created_Date

            // ETL_Date

            // View row
            if (RowType == RowType.View) {
                // id
                id.ViewValue = id.CurrentValue;
                id.ViewValue = FormatNumber(id.ViewValue, id.FormatPattern);
                id.ViewCustomAttributes = "";

                // request_id
                request_id.ViewValue = ConvertToString(request_id.CurrentValue); // DN
                request_id.ViewCustomAttributes = "";

                // UserName
                _UserName.ViewValue = ConvertToString(_UserName.CurrentValue); // DN
                _UserName.ViewCustomAttributes = "";

                // Full_Name
                Full_Name.ViewValue = ConvertToString(Full_Name.CurrentValue); // DN
                Full_Name.ViewCustomAttributes = "";

                // Position_Name
                Position_Name.ViewValue = ConvertToString(Position_Name.CurrentValue); // DN
                Position_Name.ViewCustomAttributes = "";

                // Status_Approval
                Status_Approval.ViewValue = ConvertToString(Status_Approval.CurrentValue); // DN
                Status_Approval.ViewCustomAttributes = "";

                // Approval_Date
                Approval_Date.ViewValue = Approval_Date.CurrentValue;
                Approval_Date.ViewValue = FormatDateTime(Approval_Date.ViewValue, Approval_Date.FormatPattern);
                Approval_Date.ViewCustomAttributes = "";

                // Approval_Notes
                Approval_Notes.ViewValue = ConvertToString(Approval_Notes.CurrentValue); // DN
                Approval_Notes.ViewCustomAttributes = "";

                // Reject_Notes
                Reject_Notes.ViewValue = ConvertToString(Reject_Notes.CurrentValue); // DN
                Reject_Notes.ViewCustomAttributes = "";

                // Last_Update_By
                Last_Update_By.ViewValue = Last_Update_By.CurrentValue;
                Last_Update_By.ViewValue = FormatDateTime(Last_Update_By.ViewValue, Last_Update_By.FormatPattern);
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

                // UserName
                _UserName.HrefValue = "";
                _UserName.TooltipValue = "";

                // Full_Name
                Full_Name.HrefValue = "";
                Full_Name.TooltipValue = "";

                // Position_Name
                Position_Name.HrefValue = "";
                Position_Name.TooltipValue = "";

                // Status_Approval
                Status_Approval.HrefValue = "";
                Status_Approval.TooltipValue = "";

                // Approval_Date
                Approval_Date.HrefValue = "";
                Approval_Date.TooltipValue = "";

                // Approval_Notes
                Approval_Notes.HrefValue = "";
                Approval_Notes.TooltipValue = "";

                // Reject_Notes
                Reject_Notes.HrefValue = "";
                Reject_Notes.TooltipValue = "";

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
            } else if (RowType == RowType.Add) {
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

                // Position_Name
                Position_Name.SetupEditAttributes();
                if (!Position_Name.Raw)
                    Position_Name.CurrentValue = HtmlDecode(Position_Name.CurrentValue);
                Position_Name.EditValue = HtmlEncode(Position_Name.CurrentValue);
                Position_Name.PlaceHolder = RemoveHtml(Position_Name.Caption);

                // Status_Approval
                Status_Approval.SetupEditAttributes();
                if (!Status_Approval.Raw)
                    Status_Approval.CurrentValue = HtmlDecode(Status_Approval.CurrentValue);
                Status_Approval.EditValue = HtmlEncode(Status_Approval.CurrentValue);
                Status_Approval.PlaceHolder = RemoveHtml(Status_Approval.Caption);

                // Approval_Date
                Approval_Date.SetupEditAttributes();
                Approval_Date.EditValue = FormatDateTime(Approval_Date.CurrentValue, Approval_Date.FormatPattern); // DN
                Approval_Date.PlaceHolder = RemoveHtml(Approval_Date.Caption);

                // Approval_Notes
                Approval_Notes.SetupEditAttributes();
                if (!Approval_Notes.Raw)
                    Approval_Notes.CurrentValue = HtmlDecode(Approval_Notes.CurrentValue);
                Approval_Notes.EditValue = HtmlEncode(Approval_Notes.CurrentValue);
                Approval_Notes.PlaceHolder = RemoveHtml(Approval_Notes.Caption);

                // Reject_Notes
                Reject_Notes.SetupEditAttributes();
                if (!Reject_Notes.Raw)
                    Reject_Notes.CurrentValue = HtmlDecode(Reject_Notes.CurrentValue);
                Reject_Notes.EditValue = HtmlEncode(Reject_Notes.CurrentValue);
                Reject_Notes.PlaceHolder = RemoveHtml(Reject_Notes.Caption);

                // Last_Update_By
                Last_Update_By.SetupEditAttributes();
                Last_Update_By.EditValue = FormatDateTime(Last_Update_By.CurrentValue, Last_Update_By.FormatPattern); // DN
                Last_Update_By.PlaceHolder = RemoveHtml(Last_Update_By.Caption);

                // Created_By
                Created_By.SetupEditAttributes();
                if (!Created_By.Raw)
                    Created_By.CurrentValue = HtmlDecode(Created_By.CurrentValue);
                Created_By.EditValue = HtmlEncode(Created_By.CurrentValue);
                Created_By.PlaceHolder = RemoveHtml(Created_By.Caption);

                // Created_Date
                Created_Date.SetupEditAttributes();
                Created_Date.EditValue = FormatDateTime(Created_Date.CurrentValue, Created_Date.FormatPattern); // DN
                Created_Date.PlaceHolder = RemoveHtml(Created_Date.Caption);

                // ETL_Date
                ETL_Date.SetupEditAttributes();
                ETL_Date.EditValue = FormatDateTime(ETL_Date.CurrentValue, ETL_Date.FormatPattern); // DN
                ETL_Date.PlaceHolder = RemoveHtml(ETL_Date.Caption);

                // Add refer script

                // UserName
                _UserName.HrefValue = "";

                // Full_Name
                Full_Name.HrefValue = "";

                // Position_Name
                Position_Name.HrefValue = "";

                // Status_Approval
                Status_Approval.HrefValue = "";

                // Approval_Date
                Approval_Date.HrefValue = "";

                // Approval_Notes
                Approval_Notes.HrefValue = "";

                // Reject_Notes
                Reject_Notes.HrefValue = "";

                // Last_Update_By
                Last_Update_By.HrefValue = "";

                // Created_By
                Created_By.HrefValue = "";

                // Created_Date
                Created_Date.HrefValue = "";

                // ETL_Date
                ETL_Date.HrefValue = "";
            } else if (RowType == RowType.Edit) {
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

                // Position_Name
                Position_Name.SetupEditAttributes();
                if (!Position_Name.Raw)
                    Position_Name.CurrentValue = HtmlDecode(Position_Name.CurrentValue);
                Position_Name.EditValue = HtmlEncode(Position_Name.CurrentValue);
                Position_Name.PlaceHolder = RemoveHtml(Position_Name.Caption);

                // Status_Approval
                Status_Approval.SetupEditAttributes();
                if (!Status_Approval.Raw)
                    Status_Approval.CurrentValue = HtmlDecode(Status_Approval.CurrentValue);
                Status_Approval.EditValue = HtmlEncode(Status_Approval.CurrentValue);
                Status_Approval.PlaceHolder = RemoveHtml(Status_Approval.Caption);

                // Approval_Date
                Approval_Date.SetupEditAttributes();
                Approval_Date.EditValue = FormatDateTime(Approval_Date.CurrentValue, Approval_Date.FormatPattern); // DN
                Approval_Date.PlaceHolder = RemoveHtml(Approval_Date.Caption);

                // Approval_Notes
                Approval_Notes.SetupEditAttributes();
                if (!Approval_Notes.Raw)
                    Approval_Notes.CurrentValue = HtmlDecode(Approval_Notes.CurrentValue);
                Approval_Notes.EditValue = HtmlEncode(Approval_Notes.CurrentValue);
                Approval_Notes.PlaceHolder = RemoveHtml(Approval_Notes.Caption);

                // Reject_Notes
                Reject_Notes.SetupEditAttributes();
                if (!Reject_Notes.Raw)
                    Reject_Notes.CurrentValue = HtmlDecode(Reject_Notes.CurrentValue);
                Reject_Notes.EditValue = HtmlEncode(Reject_Notes.CurrentValue);
                Reject_Notes.PlaceHolder = RemoveHtml(Reject_Notes.Caption);

                // Last_Update_By
                Last_Update_By.SetupEditAttributes();
                Last_Update_By.EditValue = FormatDateTime(Last_Update_By.CurrentValue, Last_Update_By.FormatPattern); // DN
                Last_Update_By.PlaceHolder = RemoveHtml(Last_Update_By.Caption);

                // Created_By
                Created_By.SetupEditAttributes();
                if (!Created_By.Raw)
                    Created_By.CurrentValue = HtmlDecode(Created_By.CurrentValue);
                Created_By.EditValue = HtmlEncode(Created_By.CurrentValue);
                Created_By.PlaceHolder = RemoveHtml(Created_By.Caption);

                // Created_Date
                Created_Date.SetupEditAttributes();
                Created_Date.EditValue = FormatDateTime(Created_Date.CurrentValue, Created_Date.FormatPattern); // DN
                Created_Date.PlaceHolder = RemoveHtml(Created_Date.Caption);

                // ETL_Date
                ETL_Date.SetupEditAttributes();
                ETL_Date.EditValue = FormatDateTime(ETL_Date.CurrentValue, ETL_Date.FormatPattern); // DN
                ETL_Date.PlaceHolder = RemoveHtml(ETL_Date.Caption);

                // Edit refer script

                // UserName
                _UserName.HrefValue = "";

                // Full_Name
                Full_Name.HrefValue = "";

                // Position_Name
                Position_Name.HrefValue = "";

                // Status_Approval
                Status_Approval.HrefValue = "";

                // Approval_Date
                Approval_Date.HrefValue = "";

                // Approval_Notes
                Approval_Notes.HrefValue = "";

                // Reject_Notes
                Reject_Notes.HrefValue = "";

                // Last_Update_By
                Last_Update_By.HrefValue = "";

                // Created_By
                Created_By.HrefValue = "";

                // Created_Date
                Created_Date.HrefValue = "";

                // ETL_Date
                ETL_Date.HrefValue = "";
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
            if (Position_Name.Required) {
                if (!Position_Name.IsDetailKey && Empty(Position_Name.FormValue)) {
                    Position_Name.AddErrorMessage(ConvertToString(Position_Name.RequiredErrorMessage).Replace("%s", Position_Name.Caption));
                }
            }
            if (Status_Approval.Required) {
                if (!Status_Approval.IsDetailKey && Empty(Status_Approval.FormValue)) {
                    Status_Approval.AddErrorMessage(ConvertToString(Status_Approval.RequiredErrorMessage).Replace("%s", Status_Approval.Caption));
                }
            }
            if (Approval_Date.Required) {
                if (!Approval_Date.IsDetailKey && Empty(Approval_Date.FormValue)) {
                    Approval_Date.AddErrorMessage(ConvertToString(Approval_Date.RequiredErrorMessage).Replace("%s", Approval_Date.Caption));
                }
            }
            if (!CheckDate(Approval_Date.FormValue, Approval_Date.FormatPattern)) {
                Approval_Date.AddErrorMessage(Approval_Date.GetErrorMessage(false));
            }
            if (Approval_Notes.Required) {
                if (!Approval_Notes.IsDetailKey && Empty(Approval_Notes.FormValue)) {
                    Approval_Notes.AddErrorMessage(ConvertToString(Approval_Notes.RequiredErrorMessage).Replace("%s", Approval_Notes.Caption));
                }
            }
            if (Reject_Notes.Required) {
                if (!Reject_Notes.IsDetailKey && Empty(Reject_Notes.FormValue)) {
                    Reject_Notes.AddErrorMessage(ConvertToString(Reject_Notes.RequiredErrorMessage).Replace("%s", Reject_Notes.Caption));
                }
            }
            if (Last_Update_By.Required) {
                if (!Last_Update_By.IsDetailKey && Empty(Last_Update_By.FormValue)) {
                    Last_Update_By.AddErrorMessage(ConvertToString(Last_Update_By.RequiredErrorMessage).Replace("%s", Last_Update_By.Caption));
                }
            }
            if (!CheckDate(Last_Update_By.FormValue, Last_Update_By.FormatPattern)) {
                Last_Update_By.AddErrorMessage(Last_Update_By.GetErrorMessage(false));
            }
            if (Created_By.Required) {
                if (!Created_By.IsDetailKey && Empty(Created_By.FormValue)) {
                    Created_By.AddErrorMessage(ConvertToString(Created_By.RequiredErrorMessage).Replace("%s", Created_By.Caption));
                }
            }
            if (Created_Date.Required) {
                if (!Created_Date.IsDetailKey && Empty(Created_Date.FormValue)) {
                    Created_Date.AddErrorMessage(ConvertToString(Created_Date.RequiredErrorMessage).Replace("%s", Created_Date.Caption));
                }
            }
            if (!CheckDate(Created_Date.FormValue, Created_Date.FormatPattern)) {
                Created_Date.AddErrorMessage(Created_Date.GetErrorMessage(false));
            }
            if (ETL_Date.Required) {
                if (!ETL_Date.IsDetailKey && Empty(ETL_Date.FormValue)) {
                    ETL_Date.AddErrorMessage(ConvertToString(ETL_Date.RequiredErrorMessage).Replace("%s", ETL_Date.Caption));
                }
            }
            if (!CheckDate(ETL_Date.FormValue, ETL_Date.FormatPattern)) {
                ETL_Date.AddErrorMessage(ETL_Date.GetErrorMessage(false));
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

            // UserName
            _UserName.SetDbValue(rsnew, _UserName.CurrentValue, _UserName.ReadOnly);

            // Full_Name
            Full_Name.SetDbValue(rsnew, Full_Name.CurrentValue, Full_Name.ReadOnly);

            // Position_Name
            Position_Name.SetDbValue(rsnew, Position_Name.CurrentValue, Position_Name.ReadOnly);

            // Status_Approval
            Status_Approval.SetDbValue(rsnew, Status_Approval.CurrentValue, Status_Approval.ReadOnly);

            // Approval_Date
            Approval_Date.SetDbValue(rsnew, ConvertToDateTime(Approval_Date.CurrentValue, Approval_Date.FormatPattern), Approval_Date.ReadOnly);

            // Approval_Notes
            Approval_Notes.SetDbValue(rsnew, Approval_Notes.CurrentValue, Approval_Notes.ReadOnly);

            // Reject_Notes
            Reject_Notes.SetDbValue(rsnew, Reject_Notes.CurrentValue, Reject_Notes.ReadOnly);

            // Last_Update_By
            Last_Update_By.SetDbValue(rsnew, ConvertToDateTime(Last_Update_By.CurrentValue, Last_Update_By.FormatPattern), Last_Update_By.ReadOnly);

            // Created_By
            Created_By.SetDbValue(rsnew, Created_By.CurrentValue, Created_By.ReadOnly);

            // Created_Date
            Created_Date.SetDbValue(rsnew, ConvertToDateTime(Created_Date.CurrentValue, Created_Date.FormatPattern), Created_Date.ReadOnly);

            // ETL_Date
            ETL_Date.SetDbValue(rsnew, ConvertToDateTime(ETL_Date.CurrentValue, ETL_Date.FormatPattern), ETL_Date.ReadOnly);

            // Update current values
            SetCurrentValues(rsnew);
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

        // Add record
        #pragma warning disable 168, 219

        protected async Task<JsonBoolResult> AddRow(Dictionary<string, object>? rsold = null) { // DN
            bool result = false;

            // Set up foreign key field value from Session
            if (CurrentMasterTable == "v_tr_request") {
                request_id.CurrentValue = request_id.SessionValue;
            }
            if (CurrentMasterTable == "tr_request") {
                request_id.CurrentValue = request_id.SessionValue;
            }

            // Set new row
            Dictionary<string, object> rsnew = new ();
            try {
                // UserName
                _UserName.SetDbValue(rsnew, _UserName.CurrentValue);

                // Full_Name
                Full_Name.SetDbValue(rsnew, Full_Name.CurrentValue);

                // Position_Name
                Position_Name.SetDbValue(rsnew, Position_Name.CurrentValue);

                // Status_Approval
                Status_Approval.SetDbValue(rsnew, Status_Approval.CurrentValue);

                // Approval_Date
                Approval_Date.SetDbValue(rsnew, ConvertToDateTime(Approval_Date.CurrentValue, Approval_Date.FormatPattern));

                // Approval_Notes
                Approval_Notes.SetDbValue(rsnew, Approval_Notes.CurrentValue);

                // Reject_Notes
                Reject_Notes.SetDbValue(rsnew, Reject_Notes.CurrentValue);

                // Last_Update_By
                Last_Update_By.SetDbValue(rsnew, ConvertToDateTime(Last_Update_By.CurrentValue, Last_Update_By.FormatPattern));

                // Created_By
                Created_By.SetDbValue(rsnew, Created_By.CurrentValue);

                // Created_Date
                Created_Date.SetDbValue(rsnew, ConvertToDateTime(Created_Date.CurrentValue, Created_Date.FormatPattern));

                // ETL_Date
                ETL_Date.SetDbValue(rsnew, ConvertToDateTime(ETL_Date.CurrentValue, ETL_Date.FormatPattern));

                // request_id
                if (!Empty(request_id.SessionValue)) {
                    rsnew["request_id"] = request_id.SessionValue;
                }
            } catch (Exception e) {
                if (Config.Debug)
                    throw;
                FailureMessage = e.Message;
                return JsonBoolResult.FalseResult;
            }

            // Update current values
            SetCurrentValues(rsnew);
            string? masterFilter;
            Dictionary<string, object?> detailKeys;
            bool validMasterRecord;

            // Check referential integrity for master table 'tr_request'
            validMasterRecord = true;
            detailKeys = new ();
            detailKeys.Add("request_id", request_id.SessionValue);
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
            if (insertRow) {
                try {
                    result = ConvertToBool(await InsertAsync(rsnew));
                    rsnew["id"] = id.CurrentValue!;
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
            // Hide foreign keys
            string masterTblVar = CurrentMasterTable;
            if (masterTblVar == "v_tr_request") {
                request_id.Visible = false;

                //if (vTrRequest.EventCancelled) EventCancelled = true;
                if (Get<bool>("mastereventcancelled"))
                    EventCancelled = true;
            }
            if (masterTblVar == "tr_request") {
                request_id.Visible = false;

                //if (trRequest.EventCancelled) EventCancelled = true;
                if (Get<bool>("mastereventcancelled"))
                    EventCancelled = true;
            }
            DbMasterFilter = MasterFilterFromSession; // Get master filter from session
            DbDetailFilter = DetailFilterFromSession; // Get detail filter from session
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

        // ListOptions Load event
        public virtual void ListOptionsLoad() {
            // Example:
            //var opt = ListOptions.Add("new");
            //opt.Header = "xxx";
            //opt.OnLeft = true; // Link on left
            //opt.MoveTo(0); // Move to first column
        }

        // ListOptions Rendering event
        public virtual void ListOptionsRendering() {
            //xxxGrid.DetailAdd = (...condition...); // Set to true or false conditionally
            //xxxGrid.DetailEdit = (...condition...); // Set to true or false conditionally
            //xxxGrid.DetailView = (...condition...); // Set to true or false conditionally
        }

        // ListOptions Rendered event
        public virtual void ListOptionsRendered() {
            //Example:
            //ListOptions["new"].Body = "xxx";
        }

        // Row Custom Action event
        public virtual bool RowCustomAction(string action, Dictionary<string, object> row) {
            // Return false to abort
            return true;
        }

        // Grid Inserting event
        public virtual bool GridInserting() {
            // Enter your code here
            // To reject grid insert, set return value to false
            return true;
        }

        // Grid Inserted event
        public virtual void GridInserted(List<Dictionary<string, object>> rsnew) {
            //Log("Grid Inserted");
        }

        // Grid Updating event
        public virtual bool GridUpdating(List<Dictionary<string, object>> rsold) {
            // Enter your code here
            // To reject grid update, set return value to false
            return true;
        }

        // Grid Updated event
        public virtual void GridUpdated(List<Dictionary<string, object>> rsold, List<Dictionary<string, object>> rsnew) {
            //Log("Grid Updated");
        }
    } // End page class
} // End Partial class
