namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// trRequestDetailGrid
    /// </summary>
    public static TrRequestDetailGrid trRequestDetailGrid
    {
        get => HttpData.Get<TrRequestDetailGrid>("trRequestDetailGrid")!;
        set => HttpData["trRequestDetailGrid"] = value;
    }

    /// <summary>
    /// Page class for tr_request_detail
    /// </summary>
    public class TrRequestDetailGrid : TrRequestDetailGridBase
    {
        // Constructor
        public TrRequestDetailGrid() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TrRequestDetailGridBase : TrRequestDetail
    {
        // Page ID
        public string PageID = "grid";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "tr_request_detail";

        // Page object name
        public string PageObjName = "trRequestDetailGrid";

        // Title
        public string? Title = null; // Title for <title> tag

        // Grid form hidden field names
        public string FormName = "ftr_request_detailgrid";

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
        public TrRequestDetailGridBase()
        {
            // CSS class name as context
            TableVar = "tr_request_detail";
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
            AddUrl = "TrRequestDetailAdd";

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
        public string PageName => "TrRequestDetailGrid";

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
                FormName = "ftr_request_detailgrid";

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
                        item.Visible = Security.AllowDelete(CurrentProjectID + TableName);
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
                trRequestDetailGrid?.PageRender();
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
            SAP_Unit_Price.SetFormValue("", false); // Clear form value
            Extd_SAP_Price.SetFormValue("", false); // Clear form value
            GP_SAP_Price.SetFormValue("", false); // Clear form value
            Override_Unit_Price.SetFormValue("", false); // Clear form value
            Extd_Override_Price.SetFormValue("", false); // Clear form value
            GP_Override_Price.SetFormValue("", false); // Clear form value
            Override_Core.SetFormValue("", false); // Clear form value
            Override_Percent.SetFormValue("", false); // Clear form value
            Core_Life_Ind.SetFormValue("", false); // Clear form value
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
            if (CurrentForm.HasValue("x_Item_No") && CurrentForm.HasValue("o_Item_No") && !SameString(Item_No.CurrentValue, Item_No.DefaultValue) &&
            !(Item_No.IsForeignKey && CurrentMasterTable != "" && SameString(Item_No.CurrentValue, Item_No.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Part_No") && CurrentForm.HasValue("o_Part_No") && !SameString(Part_No.CurrentValue, Part_No.DefaultValue) &&
            !(Part_No.IsForeignKey && CurrentMasterTable != "" && SameString(Part_No.CurrentValue, Part_No.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Part_Description") && CurrentForm.HasValue("o_Part_Description") && !SameString(Part_Description.CurrentValue, Part_Description.DefaultValue) &&
            !(Part_Description.IsForeignKey && CurrentMasterTable != "" && SameString(Part_Description.CurrentValue, Part_Description.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Qty") && CurrentForm.HasValue("o_Qty") && !SameString(Qty.CurrentValue, Qty.DefaultValue) &&
            !(Qty.IsForeignKey && CurrentMasterTable != "" && SameString(Qty.CurrentValue, Qty.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_SAP_Unit_Price") && CurrentForm.HasValue("o_SAP_Unit_Price") && !SameString(SAP_Unit_Price.CurrentValue, SAP_Unit_Price.DefaultValue) &&
            !(SAP_Unit_Price.IsForeignKey && CurrentMasterTable != "" && SameString(SAP_Unit_Price.CurrentValue, SAP_Unit_Price.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Extd_SAP_Price") && CurrentForm.HasValue("o_Extd_SAP_Price") && !SameString(Extd_SAP_Price.CurrentValue, Extd_SAP_Price.DefaultValue) &&
            !(Extd_SAP_Price.IsForeignKey && CurrentMasterTable != "" && SameString(Extd_SAP_Price.CurrentValue, Extd_SAP_Price.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_GP_SAP_Price") && CurrentForm.HasValue("o_GP_SAP_Price") && !SameString(GP_SAP_Price.CurrentValue, GP_SAP_Price.DefaultValue) &&
            !(GP_SAP_Price.IsForeignKey && CurrentMasterTable != "" && SameString(GP_SAP_Price.CurrentValue, GP_SAP_Price.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Override_Unit_Price") && CurrentForm.HasValue("o_Override_Unit_Price") && !SameString(Override_Unit_Price.CurrentValue, Override_Unit_Price.DefaultValue) &&
            !(Override_Unit_Price.IsForeignKey && CurrentMasterTable != "" && SameString(Override_Unit_Price.CurrentValue, Override_Unit_Price.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Extd_Override_Price") && CurrentForm.HasValue("o_Extd_Override_Price") && !SameString(Extd_Override_Price.CurrentValue, Extd_Override_Price.DefaultValue) &&
            !(Extd_Override_Price.IsForeignKey && CurrentMasterTable != "" && SameString(Extd_Override_Price.CurrentValue, Extd_Override_Price.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_GP_Override_Price") && CurrentForm.HasValue("o_GP_Override_Price") && !SameString(GP_Override_Price.CurrentValue, GP_Override_Price.DefaultValue) &&
            !(GP_Override_Price.IsForeignKey && CurrentMasterTable != "" && SameString(GP_Override_Price.CurrentValue, GP_Override_Price.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Override_Core") && CurrentForm.HasValue("o_Override_Core") && !SameString(Override_Core.CurrentValue, Override_Core.DefaultValue) &&
            !(Override_Core.IsForeignKey && CurrentMasterTable != "" && SameString(Override_Core.CurrentValue, Override_Core.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Override_Percent") && CurrentForm.HasValue("o_Override_Percent") && !SameString(Override_Percent.CurrentValue, Override_Percent.DefaultValue) &&
            !(Override_Percent.IsForeignKey && CurrentMasterTable != "" && SameString(Override_Percent.CurrentValue, Override_Percent.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Core_Life_Ind") && CurrentForm.HasValue("o_Core_Life_Ind") && !SameString(Core_Life_Ind.CurrentValue, Core_Life_Ind.DefaultValue) &&
            !(Core_Life_Ind.IsForeignKey && CurrentMasterTable != "" && SameString(Core_Life_Ind.CurrentValue, Core_Life_Ind.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_Notes") && CurrentForm.HasValue("o_Notes") && !SameString(Notes.CurrentValue, Notes.DefaultValue) &&
            !(Notes.IsForeignKey && CurrentMasterTable != "" && SameString(Notes.CurrentValue, Notes.SessionValue)))
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
            Item_No.ClearErrorMessage();
            Part_No.ClearErrorMessage();
            Part_Description.ClearErrorMessage();
            Qty.ClearErrorMessage();
            SAP_Unit_Price.ClearErrorMessage();
            Extd_SAP_Price.ClearErrorMessage();
            GP_SAP_Price.ClearErrorMessage();
            Override_Unit_Price.ClearErrorMessage();
            Extd_Override_Price.ClearErrorMessage();
            GP_Override_Price.ClearErrorMessage();
            Override_Core.ClearErrorMessage();
            Override_Percent.ClearErrorMessage();
            Core_Life_Ind.ClearErrorMessage();
            Notes.ClearErrorMessage();
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

            // "edit"
            item = ListOptions.Add("edit");
            item.CssClass = "text-nowrap";
            item.Visible = Security.CanEdit;
            item.OnLeft = true;

            // "copy"
            item = ListOptions.Add("copy");
            item.CssClass = "text-nowrap";
            item.Visible = Security.CanAdd;
            item.OnLeft = true;

            // "delete"
            item = ListOptions.Add("delete");
            item.CssClass = "text-nowrap";
            item.Visible = Security.CanDelete;
            item.OnLeft = true;

            // "sequence"
            item = ListOptions.Add("sequence");
            item.CssClass = "text-nowrap";
            item.Visible = true;
            item.OnLeft = true; // Always on left
            item.ShowInDropDown = false;
            item.ShowInButtonGroup = false;

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
                    if (!Security.AllowDelete(CurrentProjectID + TableName) && IsNumeric(RowIndex) && (RowAction == "" || RowAction == "edit")) { // Do not allow delete existing record
                        listOption?.SetBody("&nbsp;");
                    } else {
                        listOption?.SetBody("<a class=\"ew-grid-link ew-grid-delete\" title=\"" + Language.Phrase("DeleteLink", true) + "\" data-caption=\"" + Language.Phrase("DeleteLink", true) + "\" data-ew-action=\"delete-grid-row\" data-rowindex=\"" + RowIndex + "\">" + Language.Phrase("DeleteLink") + "</a>");
                    }
                }
            }

            // "sequence"
            listOption = ListOptions["sequence"];
            listOption?.SetBody(FormatSequenceNumber(RecordCount));
            if (CurrentMode == "view") {
            // "view"
            listOption = ListOptions["view"];
            string viewcaption = Language.Phrase("ViewLink", true);
            isVisible = Security.CanView;
            if (isVisible) {
                if (ModalView && !IsMobile())
                    listOption?.SetBody($@"<a class=""ew-row-link ew-view"" title=""{viewcaption}"" data-table=""tr_request_detail"" data-caption=""{viewcaption}"" data-ew-action=""modal"" data-action=""view"" data-ajax=""" + (UseAjaxActions ? "true" : "false") + "\" data-url=\"" + HtmlEncode(AppPath(ViewUrl)) + "\" data-btn=\"null\">" + Language.Phrase("ViewLink") + "</a>");
                else
                    listOption?.SetBody($@"<a class=""ew-row-link ew-view"" title=""{viewcaption}"" data-caption=""{viewcaption}"" href=""" + HtmlEncode(AppPath(ViewUrl)) + "\">" + Language.Phrase("ViewLink") + "</a>");
            } else {
                listOption?.Clear();
            }

            // "edit"
            listOption = ListOptions["edit"];
            string editcaption = Language.Phrase("EditLink", true);
            isVisible = Security.CanEdit;
            if (isVisible) {
                if (ModalEdit && !IsMobile())
                    listOption?.SetBody($@"<a class=""ew-row-link ew-edit"" title=""{editcaption}"" data-table=""tr_request_detail"" data-caption=""{editcaption}"" data-ew-action=""modal"" data-action=""edit"" data-ajax=""" + (UseAjaxActions ? "true" : "false") + "\" data-url=\"" + HtmlEncode(AppPath(EditUrl)) + "\" data-btn=\"SaveBtn\">" + Language.Phrase("EditLink") + "</a>");
                else
                    listOption?.SetBody($@"<a class=""ew-row-link ew-edit"" title=""{editcaption}"" data-caption=""{editcaption}"" href=""" + HtmlEncode(AppPath(EditUrl)) + "\">" + Language.Phrase("EditLink") + "</a>");
            } else {
                listOption?.Clear();
            }

            // "copy"
            listOption = ListOptions["copy"];
            string copycaption = Language.Phrase("CopyLink", true);
            isVisible = Security.CanAdd;
            if (isVisible) {
                if (ModalAdd && !IsMobile())
                    listOption?.SetBody($@"<a class=""ew-row-link ew-copy"" title=""{copycaption}"" data-table=""tr_request_detail"" data-caption=""{copycaption}"" data-ew-action=""modal"" data-action=""add"" data-ajax=""" + (UseAjaxActions ? "true" : "false") + "\" data-url=\"" + HtmlEncode(AppPath(CopyUrl)) + "\" data-btn=\"AddBtn\">" + Language.Phrase("CopyLink") + "</a>");
                else
                    listOption?.SetBody($@"<a class=""ew-row-link ew-copy"" title=""{copycaption}"" data-caption=""{copycaption}"" href=""" + HtmlEncode(AppPath(CopyUrl)) + "\">" + Language.Phrase("CopyLink") + "</a>");
            } else {
                listOption?.Clear();
            }

            // "delete"
            listOption = ListOptions["delete"];
            isVisible = Security.CanDelete;
            if (isVisible) {
                string deleteCaption = Language.Phrase("DeleteLink");
                string deleteTitle = Language.Phrase("DeleteLink", true);
                if (UseAjaxActions)
                    listOption?.SetBody($@"<a class=""ew-row-link ew-delete"" data-ew-action=""inline"" data-action=""delete"" title=""{deleteTitle}"" data-caption=""{deleteTitle}"" data-key=""" + HtmlEncode(GetKey(true)) + "\" data-url=\"" + HtmlEncode(AppPath(DeleteUrl)) + "\">" + deleteCaption + "</a>");
                else
                    listOption?.SetBody(@"<a class=""ew-row-link ew-delete""" +
                        (InlineDelete ? @" data-ew-action=""inline-delete""" : "") +
                        $@" title=""{deleteTitle}"" data-caption=""{deleteTitle}"" href=""" + HtmlEncode(AppPath(DeleteUrl)) + "\">" + deleteCaption + "</a>");
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

            // Add
            if (CurrentMode == "view") { // Check view mode
                item = option.Add("add");
                string addTitle = Language.Phrase("AddLink", true);
                AddUrl = GetAddUrl();
                if (ModalAdd && !IsMobile())
                    item.Body = $@"<a class=""ew-add-edit ew-add"" title=""{addTitle}"" data-table=""tr_request_detail"" data-caption=""{addTitle}"" data-ew-action=""modal"" data-action=""add"" data-ajax=""" + (UseAjaxActions ? "true" : "false") + "\" data-url=\"" + HtmlEncode(AppPath(AddUrl)) + "\" data-btn=\"AddBtn\">" + Language.Phrase("AddLink") + "</a>";
                else
                    item.Body = $@"<a class=""ew-add-edit ew-add"" title=\""{addTitle}"" data-caption=""{addTitle}"" href=""" + HtmlEncode(AppPath(AddUrl)) + "\">" + Language.Phrase("AddLink") + "</a>";
                item.Visible = (AddUrl != "" && Security.CanAdd);
            }
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
                        item.Visible = Security.CanAdd;
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
                    RowAttrs.Add("id", "r0_tr_request_detail");
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
            RowAttrs.Add("data-rowindex", ConvertToString(trRequestDetailGrid.RowCount));
            RowAttrs.Add("data-key", GetKey(true));
            RowAttrs.Add("id", "r" + ConvertToString(trRequestDetailGrid.RowCount) + "_tr_request_detail");
            RowAttrs.Add("data-rowtype", ConvertToString((int)RowType));
            RowAttrs.AppendClass(trRequestDetailGrid.RowCount % 2 != 1 ? "ew-table-alt-row" : "");
            if (IsAdd && trRequestDetailGrid.RowType == RowType.Add || IsEdit && trRequestDetailGrid.RowType == RowType.Edit) // Inline-Add/Edit row
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
            request_id.Upload.Index = ConvertToInt(RowIndex);
        }

        #pragma warning disable 1998
        // Load form values
        protected async Task LoadFormValues() {
            if (CurrentForm == null)
                return;
            CurrentForm.FormName = FormName;
            bool validate = !Config.ServerValidate;
            string val;

            // Check field name 'Item_No' before field var 'x_Item_No'
            val = CurrentForm.HasValue("Item_No") ? CurrentForm.GetValue("Item_No") : CurrentForm.GetValue("x_Item_No");
            if (!Item_No.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Item_No") && !CurrentForm.HasValue("x_Item_No")) // DN
                    Item_No.Visible = false; // Disable update for API request
                else
                    Item_No.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Item_No"))
                Item_No.OldValue = CurrentForm.GetValue("o_Item_No");

            // Check field name 'Part_No' before field var 'x_Part_No'
            val = CurrentForm.HasValue("Part_No") ? CurrentForm.GetValue("Part_No") : CurrentForm.GetValue("x_Part_No");
            if (!Part_No.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Part_No") && !CurrentForm.HasValue("x_Part_No")) // DN
                    Part_No.Visible = false; // Disable update for API request
                else
                    Part_No.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Part_No"))
                Part_No.OldValue = CurrentForm.GetValue("o_Part_No");

            // Check field name 'Part_Description' before field var 'x_Part_Description'
            val = CurrentForm.HasValue("Part_Description") ? CurrentForm.GetValue("Part_Description") : CurrentForm.GetValue("x_Part_Description");
            if (!Part_Description.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Part_Description") && !CurrentForm.HasValue("x_Part_Description")) // DN
                    Part_Description.Visible = false; // Disable update for API request
                else
                    Part_Description.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Part_Description"))
                Part_Description.OldValue = CurrentForm.GetValue("o_Part_Description");

            // Check field name 'Qty' before field var 'x_Qty'
            val = CurrentForm.HasValue("Qty") ? CurrentForm.GetValue("Qty") : CurrentForm.GetValue("x_Qty");
            if (!Qty.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Qty") && !CurrentForm.HasValue("x_Qty")) // DN
                    Qty.Visible = false; // Disable update for API request
                else
                    Qty.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Qty"))
                Qty.OldValue = CurrentForm.GetValue("o_Qty");

            // Check field name 'SAP_Unit_Price' before field var 'x_SAP_Unit_Price'
            val = CurrentForm.HasValue("SAP_Unit_Price") ? CurrentForm.GetValue("SAP_Unit_Price") : CurrentForm.GetValue("x_SAP_Unit_Price");
            if (!SAP_Unit_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("SAP_Unit_Price") && !CurrentForm.HasValue("x_SAP_Unit_Price")) // DN
                    SAP_Unit_Price.Visible = false; // Disable update for API request
                else
                    SAP_Unit_Price.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_SAP_Unit_Price"))
                SAP_Unit_Price.OldValue = CurrentForm.GetValue("o_SAP_Unit_Price");

            // Check field name 'Extd_SAP_Price' before field var 'x_Extd_SAP_Price'
            val = CurrentForm.HasValue("Extd_SAP_Price") ? CurrentForm.GetValue("Extd_SAP_Price") : CurrentForm.GetValue("x_Extd_SAP_Price");
            if (!Extd_SAP_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Extd_SAP_Price") && !CurrentForm.HasValue("x_Extd_SAP_Price")) // DN
                    Extd_SAP_Price.Visible = false; // Disable update for API request
                else
                    Extd_SAP_Price.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Extd_SAP_Price"))
                Extd_SAP_Price.OldValue = CurrentForm.GetValue("o_Extd_SAP_Price");

            // Check field name 'GP_SAP_Price' before field var 'x_GP_SAP_Price'
            val = CurrentForm.HasValue("GP_SAP_Price") ? CurrentForm.GetValue("GP_SAP_Price") : CurrentForm.GetValue("x_GP_SAP_Price");
            if (!GP_SAP_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("GP_SAP_Price") && !CurrentForm.HasValue("x_GP_SAP_Price")) // DN
                    GP_SAP_Price.Visible = false; // Disable update for API request
                else
                    GP_SAP_Price.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_GP_SAP_Price"))
                GP_SAP_Price.OldValue = CurrentForm.GetValue("o_GP_SAP_Price");

            // Check field name 'Override_Unit_Price' before field var 'x_Override_Unit_Price'
            val = CurrentForm.HasValue("Override_Unit_Price") ? CurrentForm.GetValue("Override_Unit_Price") : CurrentForm.GetValue("x_Override_Unit_Price");
            if (!Override_Unit_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Override_Unit_Price") && !CurrentForm.HasValue("x_Override_Unit_Price")) // DN
                    Override_Unit_Price.Visible = false; // Disable update for API request
                else
                    Override_Unit_Price.SetFormValue(val, true, validate);
            }
            if (CurrentForm.HasValue("o_Override_Unit_Price"))
                Override_Unit_Price.OldValue = CurrentForm.GetValue("o_Override_Unit_Price");

            // Check field name 'Extd_Override_Price' before field var 'x_Extd_Override_Price'
            val = CurrentForm.HasValue("Extd_Override_Price") ? CurrentForm.GetValue("Extd_Override_Price") : CurrentForm.GetValue("x_Extd_Override_Price");
            if (!Extd_Override_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Extd_Override_Price") && !CurrentForm.HasValue("x_Extd_Override_Price")) // DN
                    Extd_Override_Price.Visible = false; // Disable update for API request
                else
                    Extd_Override_Price.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Extd_Override_Price"))
                Extd_Override_Price.OldValue = CurrentForm.GetValue("o_Extd_Override_Price");

            // Check field name 'GP_Override_Price' before field var 'x_GP_Override_Price'
            val = CurrentForm.HasValue("GP_Override_Price") ? CurrentForm.GetValue("GP_Override_Price") : CurrentForm.GetValue("x_GP_Override_Price");
            if (!GP_Override_Price.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("GP_Override_Price") && !CurrentForm.HasValue("x_GP_Override_Price")) // DN
                    GP_Override_Price.Visible = false; // Disable update for API request
                else
                    GP_Override_Price.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_GP_Override_Price"))
                GP_Override_Price.OldValue = CurrentForm.GetValue("o_GP_Override_Price");

            // Check field name 'Override_Core' before field var 'x_Override_Core'
            val = CurrentForm.HasValue("Override_Core") ? CurrentForm.GetValue("Override_Core") : CurrentForm.GetValue("x_Override_Core");
            if (!Override_Core.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Override_Core") && !CurrentForm.HasValue("x_Override_Core")) // DN
                    Override_Core.Visible = false; // Disable update for API request
                else
                    Override_Core.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Override_Core"))
                Override_Core.OldValue = CurrentForm.GetValue("o_Override_Core");

            // Check field name 'Override_Percent' before field var 'x_Override_Percent'
            val = CurrentForm.HasValue("Override_Percent") ? CurrentForm.GetValue("Override_Percent") : CurrentForm.GetValue("x_Override_Percent");
            if (!Override_Percent.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Override_Percent") && !CurrentForm.HasValue("x_Override_Percent")) // DN
                    Override_Percent.Visible = false; // Disable update for API request
                else
                    Override_Percent.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Override_Percent"))
                Override_Percent.OldValue = CurrentForm.GetValue("o_Override_Percent");

            // Check field name 'Core_Life_Ind' before field var 'x_Core_Life_Ind'
            val = CurrentForm.HasValue("Core_Life_Ind") ? CurrentForm.GetValue("Core_Life_Ind") : CurrentForm.GetValue("x_Core_Life_Ind");
            if (!Core_Life_Ind.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Core_Life_Ind") && !CurrentForm.HasValue("x_Core_Life_Ind")) // DN
                    Core_Life_Ind.Visible = false; // Disable update for API request
                else
                    Core_Life_Ind.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Core_Life_Ind"))
                Core_Life_Ind.OldValue = CurrentForm.GetValue("o_Core_Life_Ind");

            // Check field name 'Notes' before field var 'x_Notes'
            val = CurrentForm.HasValue("Notes") ? CurrentForm.GetValue("Notes") : CurrentForm.GetValue("x_Notes");
            if (!Notes.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Notes") && !CurrentForm.HasValue("x_Notes")) // DN
                    Notes.Visible = false; // Disable update for API request
                else
                    Notes.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_Notes"))
                Notes.OldValue = CurrentForm.GetValue("o_Notes");

            // Check field name 'id' before field var 'x_id'
            val = CurrentForm.HasValue("id") ? CurrentForm.GetValue("id") : CurrentForm.GetValue("x_id");
            if (!id.IsDetailKey)
                id.SetFormValue(CurrentForm.GetValue("x_id"));
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
            request_id.Upload.Index = ConvertToInt(RowIndex);
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
            } else if (RowType == RowType.Add) {
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
                if (!Empty(Qty.EditValue) && IsNumeric(Qty.EditValue)) {
                    Qty.EditValue = FormatNumber(Qty.EditValue, null);
                }

                // SAP_Unit_Price
                SAP_Unit_Price.SetupEditAttributes();
                SAP_Unit_Price.EditValue = SAP_Unit_Price.CurrentValue; // DN
                SAP_Unit_Price.PlaceHolder = RemoveHtml(SAP_Unit_Price.Caption);
                if (!Empty(SAP_Unit_Price.EditValue) && IsNumeric(SAP_Unit_Price.EditValue)) {
                    SAP_Unit_Price.EditValue = FormatNumber(SAP_Unit_Price.EditValue, null);
                }

                // Extd_SAP_Price
                Extd_SAP_Price.SetupEditAttributes();
                Extd_SAP_Price.EditValue = Extd_SAP_Price.CurrentValue; // DN
                Extd_SAP_Price.PlaceHolder = RemoveHtml(Extd_SAP_Price.Caption);
                if (!Empty(Extd_SAP_Price.EditValue) && IsNumeric(Extd_SAP_Price.EditValue)) {
                    Extd_SAP_Price.EditValue = FormatNumber(Extd_SAP_Price.EditValue, null);
                }

                // GP_SAP_Price
                GP_SAP_Price.SetupEditAttributes();
                GP_SAP_Price.EditValue = GP_SAP_Price.CurrentValue; // DN
                GP_SAP_Price.PlaceHolder = RemoveHtml(GP_SAP_Price.Caption);
                if (!Empty(GP_SAP_Price.EditValue) && IsNumeric(GP_SAP_Price.EditValue)) {
                    GP_SAP_Price.EditValue = FormatNumber(GP_SAP_Price.EditValue, null);
                }

                // Override_Unit_Price
                Override_Unit_Price.SetupEditAttributes();
                Override_Unit_Price.EditValue = Override_Unit_Price.CurrentValue; // DN
                Override_Unit_Price.PlaceHolder = RemoveHtml(Override_Unit_Price.Caption);
                if (!Empty(Override_Unit_Price.EditValue) && IsNumeric(Override_Unit_Price.EditValue)) {
                    Override_Unit_Price.EditValue = FormatNumber(Override_Unit_Price.EditValue, null);
                }

                // Extd_Override_Price
                Extd_Override_Price.SetupEditAttributes();
                Extd_Override_Price.EditValue = Extd_Override_Price.CurrentValue; // DN
                Extd_Override_Price.PlaceHolder = RemoveHtml(Extd_Override_Price.Caption);
                if (!Empty(Extd_Override_Price.EditValue) && IsNumeric(Extd_Override_Price.EditValue)) {
                    Extd_Override_Price.EditValue = FormatNumber(Extd_Override_Price.EditValue, null);
                }

                // GP_Override_Price
                GP_Override_Price.SetupEditAttributes();
                GP_Override_Price.EditValue = GP_Override_Price.CurrentValue; // DN
                GP_Override_Price.PlaceHolder = RemoveHtml(GP_Override_Price.Caption);
                if (!Empty(GP_Override_Price.EditValue) && IsNumeric(GP_Override_Price.EditValue)) {
                    GP_Override_Price.EditValue = FormatNumber(GP_Override_Price.EditValue, null);
                }

                // Override_Core
                Override_Core.SetupEditAttributes();
                Override_Core.EditValue = Override_Core.CurrentValue; // DN
                Override_Core.PlaceHolder = RemoveHtml(Override_Core.Caption);
                if (!Empty(Override_Core.EditValue) && IsNumeric(Override_Core.EditValue)) {
                    Override_Core.EditValue = FormatNumber(Override_Core.EditValue, null);
                }

                // Override_Percent
                Override_Percent.SetupEditAttributes();
                Override_Percent.EditValue = Override_Percent.CurrentValue; // DN
                Override_Percent.PlaceHolder = RemoveHtml(Override_Percent.Caption);
                if (!Empty(Override_Percent.EditValue) && IsNumeric(Override_Percent.EditValue)) {
                    Override_Percent.EditValue = FormatNumber(Override_Percent.EditValue, null);
                }

                // Core_Life_Ind
                Core_Life_Ind.SetupEditAttributes();
                Core_Life_Ind.EditValue = Core_Life_Ind.CurrentValue; // DN
                Core_Life_Ind.PlaceHolder = RemoveHtml(Core_Life_Ind.Caption);
                if (!Empty(Core_Life_Ind.EditValue) && IsNumeric(Core_Life_Ind.EditValue)) {
                    Core_Life_Ind.EditValue = FormatNumber(Core_Life_Ind.EditValue, null);
                }

                // Notes
                Notes.SetupEditAttributes();
                Notes.EditValue = Notes.CurrentValue; // DN
                Notes.PlaceHolder = RemoveHtml(Notes.Caption);

                // Add refer script

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
            } else if (RowType == RowType.Edit) {
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
                if (!Empty(Override_Unit_Price.EditValue) && IsNumeric(Override_Unit_Price.EditValue)) {
                    Override_Unit_Price.EditValue = FormatNumber(Override_Unit_Price.EditValue, null);
                }

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
            // Check if validation required
            if (!Config.ServerValidate)
                return true;
            bool validateForm = true;
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
            if (SAP_Unit_Price.Required) {
                if (!SAP_Unit_Price.IsDetailKey && Empty(SAP_Unit_Price.FormValue)) {
                    SAP_Unit_Price.AddErrorMessage(ConvertToString(SAP_Unit_Price.RequiredErrorMessage).Replace("%s", SAP_Unit_Price.Caption));
                }
            }
            if (Extd_SAP_Price.Required) {
                if (!Extd_SAP_Price.IsDetailKey && Empty(Extd_SAP_Price.FormValue)) {
                    Extd_SAP_Price.AddErrorMessage(ConvertToString(Extd_SAP_Price.RequiredErrorMessage).Replace("%s", Extd_SAP_Price.Caption));
                }
            }
            if (GP_SAP_Price.Required) {
                if (!GP_SAP_Price.IsDetailKey && Empty(GP_SAP_Price.FormValue)) {
                    GP_SAP_Price.AddErrorMessage(ConvertToString(GP_SAP_Price.RequiredErrorMessage).Replace("%s", GP_SAP_Price.Caption));
                }
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
            if (GP_Override_Price.Required) {
                if (!GP_Override_Price.IsDetailKey && Empty(GP_Override_Price.FormValue)) {
                    GP_Override_Price.AddErrorMessage(ConvertToString(GP_Override_Price.RequiredErrorMessage).Replace("%s", GP_Override_Price.Caption));
                }
            }
            if (Override_Core.Required) {
                if (!Override_Core.IsDetailKey && Empty(Override_Core.FormValue)) {
                    Override_Core.AddErrorMessage(ConvertToString(Override_Core.RequiredErrorMessage).Replace("%s", Override_Core.Caption));
                }
            }
            if (Override_Percent.Required) {
                if (!Override_Percent.IsDetailKey && Empty(Override_Percent.FormValue)) {
                    Override_Percent.AddErrorMessage(ConvertToString(Override_Percent.RequiredErrorMessage).Replace("%s", Override_Percent.Caption));
                }
            }
            if (Core_Life_Ind.Required) {
                if (!Core_Life_Ind.IsDetailKey && Empty(Core_Life_Ind.FormValue)) {
                    Core_Life_Ind.AddErrorMessage(ConvertToString(Core_Life_Ind.RequiredErrorMessage).Replace("%s", Core_Life_Ind.Caption));
                }
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

            // Override_Unit_Price
            Override_Unit_Price.SetDbValue(rsnew, Override_Unit_Price.CurrentValue, Override_Unit_Price.ReadOnly);

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
