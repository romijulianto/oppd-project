namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// vTrRequestList
    /// </summary>
    public static VTrRequestList vTrRequestList
    {
        get => HttpData.Get<VTrRequestList>("vTrRequestList")!;
        set => HttpData["vTrRequestList"] = value;
    }

    /// <summary>
    /// Page class for v_tr_request
    /// </summary>
    public class VTrRequestList : VTrRequestListBase
    {
        // Constructor
        public VTrRequestList(Controller controller) : base(controller)
        {
        }

        // Constructor
        public VTrRequestList() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class VTrRequestListBase : VTrRequest
    {
        // Page ID
        public string PageID = "list";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "v_tr_request";

        // Page object name
        public string PageObjName = "vTrRequestList";

        // Title
        public string? Title = null; // Title for <title> tag

        // Grid form hidden field names
        public string FormName = "fv_tr_requestlist";

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
        public VTrRequestListBase()
        {
            // CSS class name as context
            TableVar = "v_tr_request";
            ContextClass = CheckClassName(TableVar);
            TableGridClass = AppendClass(TableGridClass, ContextClass);
            FormActionName = Config.FormRowActionName;
            FormBlankRowName = Config.FormBlankRowName;
            FormKeyCountName = Config.FormKeyCountName;

            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-bordered table-hover table-sm ew-table";

            // CSS class name as context
            ContextClass = CheckClassName(TableVar);
            TableGridClass = AppendClass(TableGridClass, ContextClass);

            // Language object
            Language = ResolveLanguage();

            // Table object (vTrRequest)
            if (vTrRequest == null || vTrRequest is VTrRequest)
                vTrRequest = this;

            // Initialize URLs
            AddUrl = "VTrRequestAdd?" + Config.TableShowDetail + "=";
            InlineAddUrl = PageUrl + "action=add";
            GridAddUrl = PageUrl + "action=gridadd";
            GridEditUrl = PageUrl + "action=gridedit";
            MultiEditUrl = PageUrl + "action=multiedit";
            MultiDeleteUrl = "VTrRequestDelete";
            MultiUpdateUrl = "VTrRequestUpdate";

            // Start time
            StartTime = Environment.TickCount;

            // Debug message
            LoadDebugMessage();

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

            // Other options
            OtherOptions["detail"] = new () { TagClassName = "ew-detail-option" };
            OtherOptions["action"] = new () { TagClassName = "ew-action-option" };

            // Column visibility
            OtherOptions["column"] = new () {
                TableVar = TableVar,
                TagClassName = "ew-columns-option",
                ButtonGroupClass = "ew-column-dropdown",
                UseDropDownButton = true,
                DropDownButtonPhrase = "Columns",
                DropDownAutoClose = "outside",
                UseButtonGroup = false
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
        public string PageName => "VTrRequestList";

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

        // Update URLs
        public string InlineAddUrl = "";

        public string GridAddUrl = "";

        public string GridEditUrl = "";

        public string MultiEditUrl = "";

        public string MultiDeleteUrl = "";

        public string MultiUpdateUrl = "";

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
            List_Approver.SetVisibility();
            Last_Update_By.SetVisibility();
            Created_By.SetVisibility();
            Created_Date.SetVisibility();
            ETL_Date.SetVisibility();
            Request_Status.SetVisibility();
            Request_PIC.SetVisibility();
        }

        // Constructor
        public VTrRequestListBase(Controller? controller = null): this() { // DN
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
                            result.Add("view", pageName == "VTrRequestView" ? "1" : "0"); // If View page, no primary button
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

        /// <summary>
        /// Run chart
        /// </summary>
        /// <param name="chartVar">Chart variable name</param>
        /// <returns>Page result</returns>
        public async Task<IActionResult> RunChart(string chartVar = "") { // DN
            IActionResult res = await Run();
            DbChart? chart = ChartByParam(chartVar);
            if (!IsTerminated && chart != null) {
                string chartClass = (chart.PageBreakType == "before") ? "ew-chart-bottom" : "ew-chart-top";
                int chartWidth = Query.TryGetValue("width", out StringValues sv) ? ConvertToInt(sv) : -1;
                int chartHeight = Query.TryGetValue("height", out StringValues sv2) ? ConvertToInt(sv2) : -1;
                return Controller.Content(ConvertToString(await chart.Render(chartClass, chartWidth, chartHeight)), "text/html", Encoding.UTF8);
            }
            return res;
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
                            if (fld.DataType == DataType.Memo && fld.MemoMaxLength > 0 && !Empty(val))
                                val = TruncateMemo(val, fld.MemoMaxLength, fld.TruncateMemoRemoveHtml);
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

        public string TopContentClass = "ew-top";

        public string MiddleContentClass = "ew-middle";

        public string BottomContentClass = "ew-bottom";

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

        /// <summary>
        /// Load recordset from filter
        /// <param name="filter">Record filter</param>
        /// </summary>
        public async Task LoadRecordsetFromFilter(string filter)
        {
            // Set up list options
            await SetupListOptions();

            // Search options
            SetupSearchOptions();

            // Other options
            SetupOtherOptions();

            // Set visibility
            SetVisibility();

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

            // Get export parameters
            string custom = "";
            if (!Empty(Param("export"))) {
                Export = Param("export");
                custom = Param("custom");
            } else {
                ExportReturnUrl = CurrentUrl();
            }
            ExportType = Export; // Get export parameter, used in header
            if (!Empty(ExportType))
                SkipHeaderFooter = true;
            if (!Empty(Export) && !SameText(Export, "print") && Empty(custom)) // No layout for export // DN
                UseLayout = false;
            CurrentAction = Param("action"); // Set up current action

            // Get grid add count
            int gridaddcnt = Get<int>(Config.TableGridAddRowCount);
            if (gridaddcnt > 0)
                GridAddRowCount = gridaddcnt;

            // Set up list options
            await SetupListOptions();

            // Setup export options
            SetupExportOptions();

            // Setup import options
            SetupImportOptions();
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

            // Setup other options
            SetupOtherOptions();

            // Set up custom action (compatible with old version)
            ListActions.Add(CustomActions);

            // Update form name to avoid conflict
            if (IsModal)
                FormName = "fv_tr_requestgrid";

            // Set up infinite scroll
            UseInfiniteScroll = Param<bool>("infinitescroll");

            // Search filters
            string srchAdvanced = ""; // Advanced search filter
            string srchBasic = ""; // Basic search filter
            string filter = ""; // Filter
            string query = ""; // Query builder

            // Get command
            Command = Get("cmd").ToLower();

            // Process list action first
            var result = await ProcessListAction();
            if (result is not EmptyResult) // Ajax request
                return result;

            // Set up records per page
            SetupDisplayRecords();

            // Handle reset command
            ResetCommand();

            // Set up Breadcrumb
            if (!IsExport())
                SetupBreadcrumb();

            // Process import
            if (IsImport) {
                if (!Config.Debug)
                    Response?.Clear();
                return await Import(Param(Config.ApiFileTokenName), Param<bool>("rollback"));
            }

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

            // Hide options
            if (IsExport() || !(Empty(CurrentAction) || IsSearch)) {
                ExportOptions.HideAllOptions();
                FilterOptions.HideAllOptions();
                ImportOptions.HideAllOptions();
            }

            // Hide other options
            if (IsExport()) {
                foreach (var (key, value) in OtherOptions)
                    value.HideAllOptions();
            }

            // Get default search criteria
            AddFilter(ref DefaultSearchWhere, BasicSearchWhere(true));
            AddFilter(ref DefaultSearchWhere, AdvancedSearchWhere(true));

            // Get basic search values
            LoadBasicSearchValues();

            // Get and validate search values for advanced search
            if (Empty(UserAction)) // Skip if user action
                LoadSearchValues(); // Get search values

            // Process filter list
            var filterResult = await ProcessFilterList();
            if (filterResult != null) {
                // Clean output buffer
                if (!Config.Debug)
                    Response?.Clear();
                return Controller.Json(filterResult);
            }
            CurrentForm?.ResetIndex();
            if (!ValidateSearch()) {
                // Nothing to do
            }

            // Restore search parms from Session if not searching / reset / export
            if ((IsExport() || Command != "search" && Command != "reset" && Command != "resetall") && Command != "json" && CheckSearchParms())
                RestoreSearchParms();

            // Call Recordset SearchValidated event
            RecordsetSearchValidated();

            // Set up sorting order
            SetupSortOrder();

            // Get basic search criteria
            if (!HasInvalidFields())
                srchBasic = BasicSearchWhere();

            // Get search criteria for advanced search
            if (!HasInvalidFields())
                srchAdvanced = AdvancedSearchWhere();

            // Get query builder criteria
            query = QueryBuilderWhere();

            // Restore display records
            if (Command != "json" && (RecordsPerPage == -1 || RecordsPerPage > 0)) {
                DisplayRecords = RecordsPerPage; // Restore from Session
            } else {
                DisplayRecords = 10; // Load default
                RecordsPerPage = DisplayRecords; // Save default to session
            }

            // Load search default if no existing search criteria
            if (!CheckSearchParms() && Empty(query)) {
                // Load basic search from default
                BasicSearch.LoadDefault();
                if (!Empty(BasicSearch.Keyword))
                    srchBasic = BasicSearchWhere(); // Save to session

                // Load advanced search from default
                if (LoadAdvancedSearchDefault())
                    srchAdvanced = AdvancedSearchWhere(); // Save to session
            }

            // Restore search settings from Session
            if (!HasInvalidFields())
                LoadAdvancedSearch();

            // Build search criteria
            if (!Empty(query)) {
                AddFilter(ref SearchWhere, query);
            } else {
                AddFilter(ref SearchWhere, srchAdvanced);
                AddFilter(ref SearchWhere, srchBasic);
            }

            // Call Recordset Searching event
            RecordsetSearching(ref SearchWhere);

            // Save search criteria
            if (Command == "search" && !RestoreSearch) {
                SessionSearchWhere = SearchWhere; // Save to Session (rename as SessionSearchWhere property)
                StartRecord = 1; // Reset start record counter
                StartRecordNumber = StartRecord;
            } else if (Command != "json" && Empty(query)) {
                SearchWhere = SessionSearchWhere;
            }

            // Build filter
            filter = "";
            if (!Security.CanList)
                filter = "(0=1)"; // Filter all records
            AddFilter(ref filter, DbDetailFilter);
            AddFilter(ref filter, SearchWhere);

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
                CurrentFilter = "0=1";
                StartRecord = 1;
                DisplayRecords = GridAddRowCount;
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
                if (DisplayRecords <= 0 || (IsExport() && ExportAll)) // Display all records
                    DisplayRecords = TotalRecords;
                if (!(IsExport() && ExportAll)) // Set up start record position
                    SetupStartRecord();

                // Recordset
                bool selectLimit = UseSelectLimit;

                // Set up list action columns, must be before LoadRecordset // DN
                foreach (var (key, act) in ListActions.Items.Where(kvp => kvp.Value.Allowed)) {
                    if (act.Select == Config.ActionMultiple && ListOptions["checkbox"] is ListOption listOpt) { // Show checkbox column if multiple action
                        listOpt.Visible = true;
                    } else if (act.Select == Config.ActionSingle) { // Show list action column
                            ListOptions["listactions"]?.SetVisible(true); // Set visible if any list action is allowed
                    }
                }
                if (selectLimit)
                    Recordset = await LoadRecordset(StartRecord - 1, DisplayRecords);

                // Set no record found message
                if ((Empty(CurrentAction) || IsSearch) && TotalRecords == 0) {
                    if (!Security.CanList)
                        WarningMessage = DeniedMessage();
                    if (SearchWhere == "0=101")
                        WarningMessage = Language.Phrase("EnterSearchCriteria");
                    else
                        WarningMessage = Language.Phrase("NoRecord");
                }
            }

            // Search options
            SetupSearchOptions();

            // Set up search panel class
            if (!Empty(SearchWhere)) {
                if (!Empty(query)) { // Hide search panel if using QueryBuilder
                    SearchPanelClass = RemoveClass(SearchPanelClass, "show");
                } else {
                    SearchPanelClass = AppendClass(SearchPanelClass, "show");
                }
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
                SetupLoginStatus(); // Setup login status

                // Pass login status to client side
                SetClientVar("login", LoginStatus);

                // Global Page Rendering event
                PageRendering();

                // Page Render event
                vTrRequestList?.PageRender();
            }
            return PageResult();
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
            SAP_Total.SetFormValue("", false); // Clear form value
            Override_Total.SetFormValue("", false); // Clear form value
            Variance_Total.SetFormValue("", false); // Clear form value
            Variance_Percent.SetFormValue("", false); // Clear form value
            LastAction = CurrentAction; // Save last action
            CurrentAction = ""; // Clear action
            Session[Config.SessionInlineMode] = ""; // Clear inline mode
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

        // Check if empty row
        public bool EmptyRow() => false;

        #pragma warning disable 162, 1998
        // Get list of filters
        public async Task<string> GetFilterList()
        {
            string filterList = "";

            // Initialize
            var filters = new JObject(); // DN
            filters.Merge(JObject.Parse(id.AdvancedSearch.ToJson())); // Field id
            filters.Merge(JObject.Parse(Request_No.AdvancedSearch.ToJson())); // Field Request_No
            filters.Merge(JObject.Parse(Reference_Doc.AdvancedSearch.ToJson())); // Field Reference_Doc
            filters.Merge(JObject.Parse(Reason.AdvancedSearch.ToJson())); // Field Reason
            filters.Merge(JObject.Parse(Request_By.AdvancedSearch.ToJson())); // Field Request_By
            filters.Merge(JObject.Parse(Request_By_Name.AdvancedSearch.ToJson())); // Field Request_By_Name
            filters.Merge(JObject.Parse(Request_By_Position.AdvancedSearch.ToJson())); // Field Request_By_Position
            filters.Merge(JObject.Parse(Request_By_Branch.AdvancedSearch.ToJson())); // Field Request_By_Branch
            filters.Merge(JObject.Parse(Request_By_Region.AdvancedSearch.ToJson())); // Field Request_By_Region
            filters.Merge(JObject.Parse(Request_Industry.AdvancedSearch.ToJson())); // Field Request_Industry
            filters.Merge(JObject.Parse(Customer_ID.AdvancedSearch.ToJson())); // Field Customer_ID
            filters.Merge(JObject.Parse(Customer_Name.AdvancedSearch.ToJson())); // Field Customer_Name
            filters.Merge(JObject.Parse(SAP_Total.AdvancedSearch.ToJson())); // Field SAP_Total
            filters.Merge(JObject.Parse(Override_Total.AdvancedSearch.ToJson())); // Field Override_Total
            filters.Merge(JObject.Parse(Variance_Total.AdvancedSearch.ToJson())); // Field Variance_Total
            filters.Merge(JObject.Parse(Variance_Percent.AdvancedSearch.ToJson())); // Field Variance_Percent
            filters.Merge(JObject.Parse(Notes.AdvancedSearch.ToJson())); // Field Notes
            filters.Merge(JObject.Parse(Next_Approver.AdvancedSearch.ToJson())); // Field Next_Approver
            filters.Merge(JObject.Parse(List_Approver.AdvancedSearch.ToJson())); // Field List_Approver
            filters.Merge(JObject.Parse(Last_Update_By.AdvancedSearch.ToJson())); // Field Last_Update_By
            filters.Merge(JObject.Parse(Created_By.AdvancedSearch.ToJson())); // Field Created_By
            filters.Merge(JObject.Parse(Created_Date.AdvancedSearch.ToJson())); // Field Created_Date
            filters.Merge(JObject.Parse(ETL_Date.AdvancedSearch.ToJson())); // Field ETL_Date
            filters.Merge(JObject.Parse(Request_Status.AdvancedSearch.ToJson())); // Field Request_Status
            filters.Merge(JObject.Parse(Request_PIC.AdvancedSearch.ToJson())); // Field Request_PIC
            filters.Merge(JObject.Parse(BasicSearch.ToJson()));

            // Return filter list in JSON
            if (filters.HasValues)
                filterList = "\"data\":" + filters.ToString();
            return (filterList != "") ? "{" + filterList + "}" : "null";
        }

        // Process filter list
        protected async Task<object?> ProcessFilterList() {
            if (Post("cmd") == "resetfilter") {
                RestoreFilterList();
            }
            return null;
        }
        #pragma warning restore 162, 1998

        // Restore list of filters
        protected bool RestoreFilterList() {
            // Return if not reset filter
            if (Post("cmd") != "resetfilter")
                return false;
            var filter = JsonConvert.DeserializeObject<Dictionary<string, string>>(Post("filter"));
            Command = "search";
            string? sv;

            // Field id
            if (filter?.TryGetValue("x_id", out sv) ?? false) {
                id.AdvancedSearch.SearchValue = sv;
                id.AdvancedSearch.SearchOperator = filter["z_id"];
                id.AdvancedSearch.SearchCondition = filter["v_id"];
                id.AdvancedSearch.SearchValue2 = filter["y_id"];
                id.AdvancedSearch.SearchOperator2 = filter["w_id"];
                id.AdvancedSearch.Save();
            }

            // Field Request_No
            if (filter?.TryGetValue("x_Request_No", out sv) ?? false) {
                Request_No.AdvancedSearch.SearchValue = sv;
                Request_No.AdvancedSearch.SearchOperator = filter["z_Request_No"];
                Request_No.AdvancedSearch.SearchCondition = filter["v_Request_No"];
                Request_No.AdvancedSearch.SearchValue2 = filter["y_Request_No"];
                Request_No.AdvancedSearch.SearchOperator2 = filter["w_Request_No"];
                Request_No.AdvancedSearch.Save();
            }

            // Field Reference_Doc
            if (filter?.TryGetValue("x_Reference_Doc", out sv) ?? false) {
                Reference_Doc.AdvancedSearch.SearchValue = sv;
                Reference_Doc.AdvancedSearch.SearchOperator = filter["z_Reference_Doc"];
                Reference_Doc.AdvancedSearch.SearchCondition = filter["v_Reference_Doc"];
                Reference_Doc.AdvancedSearch.SearchValue2 = filter["y_Reference_Doc"];
                Reference_Doc.AdvancedSearch.SearchOperator2 = filter["w_Reference_Doc"];
                Reference_Doc.AdvancedSearch.Save();
            }

            // Field Reason
            if (filter?.TryGetValue("x_Reason", out sv) ?? false) {
                Reason.AdvancedSearch.SearchValue = sv;
                Reason.AdvancedSearch.SearchOperator = filter["z_Reason"];
                Reason.AdvancedSearch.SearchCondition = filter["v_Reason"];
                Reason.AdvancedSearch.SearchValue2 = filter["y_Reason"];
                Reason.AdvancedSearch.SearchOperator2 = filter["w_Reason"];
                Reason.AdvancedSearch.Save();
            }

            // Field Request_By
            if (filter?.TryGetValue("x_Request_By", out sv) ?? false) {
                Request_By.AdvancedSearch.SearchValue = sv;
                Request_By.AdvancedSearch.SearchOperator = filter["z_Request_By"];
                Request_By.AdvancedSearch.SearchCondition = filter["v_Request_By"];
                Request_By.AdvancedSearch.SearchValue2 = filter["y_Request_By"];
                Request_By.AdvancedSearch.SearchOperator2 = filter["w_Request_By"];
                Request_By.AdvancedSearch.Save();
            }

            // Field Request_By_Name
            if (filter?.TryGetValue("x_Request_By_Name", out sv) ?? false) {
                Request_By_Name.AdvancedSearch.SearchValue = sv;
                Request_By_Name.AdvancedSearch.SearchOperator = filter["z_Request_By_Name"];
                Request_By_Name.AdvancedSearch.SearchCondition = filter["v_Request_By_Name"];
                Request_By_Name.AdvancedSearch.SearchValue2 = filter["y_Request_By_Name"];
                Request_By_Name.AdvancedSearch.SearchOperator2 = filter["w_Request_By_Name"];
                Request_By_Name.AdvancedSearch.Save();
            }

            // Field Request_By_Position
            if (filter?.TryGetValue("x_Request_By_Position", out sv) ?? false) {
                Request_By_Position.AdvancedSearch.SearchValue = sv;
                Request_By_Position.AdvancedSearch.SearchOperator = filter["z_Request_By_Position"];
                Request_By_Position.AdvancedSearch.SearchCondition = filter["v_Request_By_Position"];
                Request_By_Position.AdvancedSearch.SearchValue2 = filter["y_Request_By_Position"];
                Request_By_Position.AdvancedSearch.SearchOperator2 = filter["w_Request_By_Position"];
                Request_By_Position.AdvancedSearch.Save();
            }

            // Field Request_By_Branch
            if (filter?.TryGetValue("x_Request_By_Branch", out sv) ?? false) {
                Request_By_Branch.AdvancedSearch.SearchValue = sv;
                Request_By_Branch.AdvancedSearch.SearchOperator = filter["z_Request_By_Branch"];
                Request_By_Branch.AdvancedSearch.SearchCondition = filter["v_Request_By_Branch"];
                Request_By_Branch.AdvancedSearch.SearchValue2 = filter["y_Request_By_Branch"];
                Request_By_Branch.AdvancedSearch.SearchOperator2 = filter["w_Request_By_Branch"];
                Request_By_Branch.AdvancedSearch.Save();
            }

            // Field Request_By_Region
            if (filter?.TryGetValue("x_Request_By_Region", out sv) ?? false) {
                Request_By_Region.AdvancedSearch.SearchValue = sv;
                Request_By_Region.AdvancedSearch.SearchOperator = filter["z_Request_By_Region"];
                Request_By_Region.AdvancedSearch.SearchCondition = filter["v_Request_By_Region"];
                Request_By_Region.AdvancedSearch.SearchValue2 = filter["y_Request_By_Region"];
                Request_By_Region.AdvancedSearch.SearchOperator2 = filter["w_Request_By_Region"];
                Request_By_Region.AdvancedSearch.Save();
            }

            // Field Request_Industry
            if (filter?.TryGetValue("x_Request_Industry", out sv) ?? false) {
                Request_Industry.AdvancedSearch.SearchValue = sv;
                Request_Industry.AdvancedSearch.SearchOperator = filter["z_Request_Industry"];
                Request_Industry.AdvancedSearch.SearchCondition = filter["v_Request_Industry"];
                Request_Industry.AdvancedSearch.SearchValue2 = filter["y_Request_Industry"];
                Request_Industry.AdvancedSearch.SearchOperator2 = filter["w_Request_Industry"];
                Request_Industry.AdvancedSearch.Save();
            }

            // Field Customer_ID
            if (filter?.TryGetValue("x_Customer_ID", out sv) ?? false) {
                Customer_ID.AdvancedSearch.SearchValue = sv;
                Customer_ID.AdvancedSearch.SearchOperator = filter["z_Customer_ID"];
                Customer_ID.AdvancedSearch.SearchCondition = filter["v_Customer_ID"];
                Customer_ID.AdvancedSearch.SearchValue2 = filter["y_Customer_ID"];
                Customer_ID.AdvancedSearch.SearchOperator2 = filter["w_Customer_ID"];
                Customer_ID.AdvancedSearch.Save();
            }

            // Field Customer_Name
            if (filter?.TryGetValue("x_Customer_Name", out sv) ?? false) {
                Customer_Name.AdvancedSearch.SearchValue = sv;
                Customer_Name.AdvancedSearch.SearchOperator = filter["z_Customer_Name"];
                Customer_Name.AdvancedSearch.SearchCondition = filter["v_Customer_Name"];
                Customer_Name.AdvancedSearch.SearchValue2 = filter["y_Customer_Name"];
                Customer_Name.AdvancedSearch.SearchOperator2 = filter["w_Customer_Name"];
                Customer_Name.AdvancedSearch.Save();
            }

            // Field SAP_Total
            if (filter?.TryGetValue("x_SAP_Total", out sv) ?? false) {
                SAP_Total.AdvancedSearch.SearchValue = sv;
                SAP_Total.AdvancedSearch.SearchOperator = filter["z_SAP_Total"];
                SAP_Total.AdvancedSearch.SearchCondition = filter["v_SAP_Total"];
                SAP_Total.AdvancedSearch.SearchValue2 = filter["y_SAP_Total"];
                SAP_Total.AdvancedSearch.SearchOperator2 = filter["w_SAP_Total"];
                SAP_Total.AdvancedSearch.Save();
            }

            // Field Override_Total
            if (filter?.TryGetValue("x_Override_Total", out sv) ?? false) {
                Override_Total.AdvancedSearch.SearchValue = sv;
                Override_Total.AdvancedSearch.SearchOperator = filter["z_Override_Total"];
                Override_Total.AdvancedSearch.SearchCondition = filter["v_Override_Total"];
                Override_Total.AdvancedSearch.SearchValue2 = filter["y_Override_Total"];
                Override_Total.AdvancedSearch.SearchOperator2 = filter["w_Override_Total"];
                Override_Total.AdvancedSearch.Save();
            }

            // Field Variance_Total
            if (filter?.TryGetValue("x_Variance_Total", out sv) ?? false) {
                Variance_Total.AdvancedSearch.SearchValue = sv;
                Variance_Total.AdvancedSearch.SearchOperator = filter["z_Variance_Total"];
                Variance_Total.AdvancedSearch.SearchCondition = filter["v_Variance_Total"];
                Variance_Total.AdvancedSearch.SearchValue2 = filter["y_Variance_Total"];
                Variance_Total.AdvancedSearch.SearchOperator2 = filter["w_Variance_Total"];
                Variance_Total.AdvancedSearch.Save();
            }

            // Field Variance_Percent
            if (filter?.TryGetValue("x_Variance_Percent", out sv) ?? false) {
                Variance_Percent.AdvancedSearch.SearchValue = sv;
                Variance_Percent.AdvancedSearch.SearchOperator = filter["z_Variance_Percent"];
                Variance_Percent.AdvancedSearch.SearchCondition = filter["v_Variance_Percent"];
                Variance_Percent.AdvancedSearch.SearchValue2 = filter["y_Variance_Percent"];
                Variance_Percent.AdvancedSearch.SearchOperator2 = filter["w_Variance_Percent"];
                Variance_Percent.AdvancedSearch.Save();
            }

            // Field Notes
            if (filter?.TryGetValue("x_Notes", out sv) ?? false) {
                Notes.AdvancedSearch.SearchValue = sv;
                Notes.AdvancedSearch.SearchOperator = filter["z_Notes"];
                Notes.AdvancedSearch.SearchCondition = filter["v_Notes"];
                Notes.AdvancedSearch.SearchValue2 = filter["y_Notes"];
                Notes.AdvancedSearch.SearchOperator2 = filter["w_Notes"];
                Notes.AdvancedSearch.Save();
            }

            // Field Next_Approver
            if (filter?.TryGetValue("x_Next_Approver", out sv) ?? false) {
                Next_Approver.AdvancedSearch.SearchValue = sv;
                Next_Approver.AdvancedSearch.SearchOperator = filter["z_Next_Approver"];
                Next_Approver.AdvancedSearch.SearchCondition = filter["v_Next_Approver"];
                Next_Approver.AdvancedSearch.SearchValue2 = filter["y_Next_Approver"];
                Next_Approver.AdvancedSearch.SearchOperator2 = filter["w_Next_Approver"];
                Next_Approver.AdvancedSearch.Save();
            }

            // Field List_Approver
            if (filter?.TryGetValue("x_List_Approver", out sv) ?? false) {
                List_Approver.AdvancedSearch.SearchValue = sv;
                List_Approver.AdvancedSearch.SearchOperator = filter["z_List_Approver"];
                List_Approver.AdvancedSearch.SearchCondition = filter["v_List_Approver"];
                List_Approver.AdvancedSearch.SearchValue2 = filter["y_List_Approver"];
                List_Approver.AdvancedSearch.SearchOperator2 = filter["w_List_Approver"];
                List_Approver.AdvancedSearch.Save();
            }

            // Field Last_Update_By
            if (filter?.TryGetValue("x_Last_Update_By", out sv) ?? false) {
                Last_Update_By.AdvancedSearch.SearchValue = sv;
                Last_Update_By.AdvancedSearch.SearchOperator = filter["z_Last_Update_By"];
                Last_Update_By.AdvancedSearch.SearchCondition = filter["v_Last_Update_By"];
                Last_Update_By.AdvancedSearch.SearchValue2 = filter["y_Last_Update_By"];
                Last_Update_By.AdvancedSearch.SearchOperator2 = filter["w_Last_Update_By"];
                Last_Update_By.AdvancedSearch.Save();
            }

            // Field Created_By
            if (filter?.TryGetValue("x_Created_By", out sv) ?? false) {
                Created_By.AdvancedSearch.SearchValue = sv;
                Created_By.AdvancedSearch.SearchOperator = filter["z_Created_By"];
                Created_By.AdvancedSearch.SearchCondition = filter["v_Created_By"];
                Created_By.AdvancedSearch.SearchValue2 = filter["y_Created_By"];
                Created_By.AdvancedSearch.SearchOperator2 = filter["w_Created_By"];
                Created_By.AdvancedSearch.Save();
            }

            // Field Created_Date
            if (filter?.TryGetValue("x_Created_Date", out sv) ?? false) {
                Created_Date.AdvancedSearch.SearchValue = sv;
                Created_Date.AdvancedSearch.SearchOperator = filter["z_Created_Date"];
                Created_Date.AdvancedSearch.SearchCondition = filter["v_Created_Date"];
                Created_Date.AdvancedSearch.SearchValue2 = filter["y_Created_Date"];
                Created_Date.AdvancedSearch.SearchOperator2 = filter["w_Created_Date"];
                Created_Date.AdvancedSearch.Save();
            }

            // Field ETL_Date
            if (filter?.TryGetValue("x_ETL_Date", out sv) ?? false) {
                ETL_Date.AdvancedSearch.SearchValue = sv;
                ETL_Date.AdvancedSearch.SearchOperator = filter["z_ETL_Date"];
                ETL_Date.AdvancedSearch.SearchCondition = filter["v_ETL_Date"];
                ETL_Date.AdvancedSearch.SearchValue2 = filter["y_ETL_Date"];
                ETL_Date.AdvancedSearch.SearchOperator2 = filter["w_ETL_Date"];
                ETL_Date.AdvancedSearch.Save();
            }

            // Field Request_Status
            if (filter?.TryGetValue("x_Request_Status", out sv) ?? false) {
                Request_Status.AdvancedSearch.SearchValue = sv;
                Request_Status.AdvancedSearch.SearchOperator = filter["z_Request_Status"];
                Request_Status.AdvancedSearch.SearchCondition = filter["v_Request_Status"];
                Request_Status.AdvancedSearch.SearchValue2 = filter["y_Request_Status"];
                Request_Status.AdvancedSearch.SearchOperator2 = filter["w_Request_Status"];
                Request_Status.AdvancedSearch.Save();
            }

            // Field Request_PIC
            if (filter?.TryGetValue("x_Request_PIC", out sv) ?? false) {
                Request_PIC.AdvancedSearch.SearchValue = sv;
                Request_PIC.AdvancedSearch.SearchOperator = filter["z_Request_PIC"];
                Request_PIC.AdvancedSearch.SearchCondition = filter["v_Request_PIC"];
                Request_PIC.AdvancedSearch.SearchValue2 = filter["y_Request_PIC"];
                Request_PIC.AdvancedSearch.SearchOperator2 = filter["w_Request_PIC"];
                Request_PIC.AdvancedSearch.Save();
            }
            if (filter?.TryGetValue(Config.TableBasicSearch, out string? keyword) ?? false)
                BasicSearch.SessionKeyword = keyword;
            if (filter?.TryGetValue(Config.TableBasicSearchType, out string? type) ?? false)
                BasicSearch.SessionType = type;
            return true;
        }

        // Advanced search WHERE clause based on QueryString
        public string AdvancedSearchWhere(bool def = false) {
            string where = "";
            if (!Security.CanSearch)
                return "";
            BuildSearchSql(ref where, id, def, false); // id
            BuildSearchSql(ref where, Request_No, def, false); // Request_No
            BuildSearchSql(ref where, Reference_Doc, def, false); // Reference_Doc
            BuildSearchSql(ref where, Reason, def, false); // Reason
            BuildSearchSql(ref where, Request_By, def, false); // Request_By
            BuildSearchSql(ref where, Request_By_Name, def, false); // Request_By_Name
            BuildSearchSql(ref where, Request_By_Position, def, false); // Request_By_Position
            BuildSearchSql(ref where, Request_By_Branch, def, false); // Request_By_Branch
            BuildSearchSql(ref where, Request_By_Region, def, false); // Request_By_Region
            BuildSearchSql(ref where, Request_Industry, def, false); // Request_Industry
            BuildSearchSql(ref where, Customer_ID, def, false); // Customer_ID
            BuildSearchSql(ref where, Customer_Name, def, false); // Customer_Name
            BuildSearchSql(ref where, SAP_Total, def, false); // SAP_Total
            BuildSearchSql(ref where, Override_Total, def, false); // Override_Total
            BuildSearchSql(ref where, Variance_Total, def, false); // Variance_Total
            BuildSearchSql(ref where, Variance_Percent, def, false); // Variance_Percent
            BuildSearchSql(ref where, Notes, def, false); // Notes
            BuildSearchSql(ref where, Next_Approver, def, false); // Next_Approver
            BuildSearchSql(ref where, List_Approver, def, false); // List_Approver
            BuildSearchSql(ref where, Last_Update_By, def, false); // Last_Update_By
            BuildSearchSql(ref where, Created_By, def, false); // Created_By
            BuildSearchSql(ref where, Created_Date, def, false); // Created_Date
            BuildSearchSql(ref where, ETL_Date, def, false); // ETL_Date
            BuildSearchSql(ref where, Request_Status, def, false); // Request_Status
            BuildSearchSql(ref where, Request_PIC, def, false); // Request_PIC

            // Set up search command
            if (!def && !Empty(where) && (new[] { "", "reset", "resetall" }).Contains(Command))
                Command = "search";
            if (!def && Command == "search") {
                id.AdvancedSearch.Save(); // id
                Request_No.AdvancedSearch.Save(); // Request_No
                Reference_Doc.AdvancedSearch.Save(); // Reference_Doc
                Reason.AdvancedSearch.Save(); // Reason
                Request_By.AdvancedSearch.Save(); // Request_By
                Request_By_Name.AdvancedSearch.Save(); // Request_By_Name
                Request_By_Position.AdvancedSearch.Save(); // Request_By_Position
                Request_By_Branch.AdvancedSearch.Save(); // Request_By_Branch
                Request_By_Region.AdvancedSearch.Save(); // Request_By_Region
                Request_Industry.AdvancedSearch.Save(); // Request_Industry
                Customer_ID.AdvancedSearch.Save(); // Customer_ID
                Customer_Name.AdvancedSearch.Save(); // Customer_Name
                SAP_Total.AdvancedSearch.Save(); // SAP_Total
                Override_Total.AdvancedSearch.Save(); // Override_Total
                Variance_Total.AdvancedSearch.Save(); // Variance_Total
                Variance_Percent.AdvancedSearch.Save(); // Variance_Percent
                Notes.AdvancedSearch.Save(); // Notes
                Next_Approver.AdvancedSearch.Save(); // Next_Approver
                List_Approver.AdvancedSearch.Save(); // List_Approver
                Last_Update_By.AdvancedSearch.Save(); // Last_Update_By
                Created_By.AdvancedSearch.Save(); // Created_By
                Created_Date.AdvancedSearch.Save(); // Created_Date
                ETL_Date.AdvancedSearch.Save(); // ETL_Date
                Request_Status.AdvancedSearch.Save(); // Request_Status
                Request_PIC.AdvancedSearch.Save(); // Request_PIC

                // Clear rules for QueryBuilder
                SessionRules = "";
            }
            return where;
        }

        // Parse query builder rule function
        protected string ParseRules(Dictionary<string, object>? group, string fieldName = "") {
            if (group == null)
                return "";
            string condition = group.ContainsKey("condition") ? ConvertToString(group["condition"]) : "AND";
            if (!(new [] { "AND", "OR" }).Contains(condition))
                throw new System.Exception("Unable to build SQL query with condition '" + condition + "'");
            List<string> parts = new ();
            string where = "";
            var groupRules = group.ContainsKey("rules") ? group["rules"] : null;
            if (groupRules is IEnumerable<object> rules) {
                foreach (object rule in rules) {
                    var subRules = JObject.FromObject(rule).ToObject<Dictionary<string, object>>();
                    if (subRules == null)
                        continue;
                    if (subRules.ContainsKey("rules")) {
                        parts.Add("(" + " " + ParseRules(subRules, fieldName) + " " + ")" + " ");
                    } else {
                        string field = subRules.ContainsKey("field") ? ConvertToString(subRules["field"]) : "";
                        var fld = FieldByParam(field);
                        if (fld == null)
                            throw new System.Exception("Failed to find field '" + field + "'");
                        if (Empty(fieldName) || fld.Name == fieldName) { // Field name not specified or matched field name
                            string opr = subRules.ContainsKey("operator") ? ConvertToString(subRules["operator"]) : "";
                            string fldOpr = Config.ClientSearchOperators.FirstOrDefault(o => o.Value == opr).Key;
                            Dictionary<string, object>? ope = Config.QueryBuilderOperators.ContainsKey(opr) ? Config.QueryBuilderOperators[opr] : null;
                            if (ope == null || Empty(fldOpr))
                                throw new System.Exception("Unknown SQL operation for operator '" + opr + "'");
                            int nb_inputs = ope.ContainsKey("nb_inputs") ? ConvertToInt(ope["nb_inputs"]) : 0;
                            object val = subRules.ContainsKey("value") ? subRules["value"] : "";
                            if (nb_inputs > 0 && !Empty(val) || IsNullOrEmptyOperator(fldOpr)) {
                                string fldVal = val is List<object> list
                                    ? (list[0] is IEnumerable<string> ? String.Join(Config.MultipleOptionSeparator, list[0]) : ConvertToString(list[0]))
                                    : ConvertToString(val);
                                bool useFilter = fld.UseFilter; // Query builder does not use filter
                                try {
                                    if (fld.IsMultiSelect) {
                                        parts.Add(!Empty(fldVal) ? GetMultiSearchSql(fld, fldOpr, ConvertSearchValue(fldVal, fldOpr, fld), DbId) : "");
                                    } else {
                                        string fldVal2 = fldOpr.Contains("BETWEEN")
                                            ? (val is List<object> list2 && list2.Count > 1
                                                ? (list2[1] is IEnumerable<string> ? String.Join(Config.MultipleOptionSeparator, list2[1]) : ConvertToString(list2[1]))
                                                : "")
                                            : ""; // BETWEEN
                                        parts.Add(GetSearchSql(
                                            fld,
                                            ConvertSearchValue(fldVal, fldOpr, fld), // fldVal
                                            fldOpr,
                                            "", // fldCond not used
                                            ConvertSearchValue(fldVal2, fldOpr, fld), // $fldVal2
                                            "", // fldOpr2 not used
                                            DbId
                                        ));
                                    }
                                } finally {
                                    fld.UseFilter = useFilter;
                                }
                            }
                        }
                    }
                }
                where = String.Join(" " + condition + " ", parts);
                bool not = group.ContainsKey("not") ? ConvertToBool(group["not"]) : false;
                if (not)
                    where = "NOT (" + where + ")";
            }
            return where;
        }

        // Quey builder WHERE clause
        public string QueryBuilderWhere(string fieldName = "")
        {
            if (!Security.CanSearch)
                return "";

            // Get rules by query builder
            string rules = "";
            if (Post("rules", out StringValues sv))
                rules = sv.ToString();
            else
                rules = SessionRules;

            // Decode and parse rules
            string where = !Empty(rules) ? ParseRules(JsonConvert.DeserializeObject<Dictionary<string, object>>(rules), fieldName) : "";

            // Clear other search and save rules to session
            if (!Empty(where) && Empty(fieldName)) { // Skip if get query for specific field
                ResetSearchParms();
                SessionRules = rules;
            }

            // Return query
            return where;
        }

        // Build search SQL
        public void BuildSearchSql(ref string where, DbField fld, bool def, bool multiValue)
        {
            string fldParm = fld.Param;
            string fldVal = def ? ConvertToString(fld.AdvancedSearch.SearchValueDefault) : ConvertToString(fld.AdvancedSearch.SearchValue);
            string fldOpr = def ? fld.AdvancedSearch.SearchOperatorDefault : fld.AdvancedSearch.SearchOperator;
            string fldCond = def ? fld.AdvancedSearch.SearchConditionDefault : fld.AdvancedSearch.SearchCondition;
            string fldVal2 = def ? ConvertToString(fld.AdvancedSearch.SearchValue2Default) : ConvertToString(fld.AdvancedSearch.SearchValue2);
            string fldOpr2 = def ? fld.AdvancedSearch.SearchOperator2Default : fld.AdvancedSearch.SearchOperator2;
            fldVal = ConvertSearchValue(fldVal, fldOpr, fld);
            fldVal2 = ConvertSearchValue(fldVal2, fldOpr2, fld);
            fldOpr = ConvertSearchOperator(fldOpr, fld, fldVal);
            fldOpr2 = ConvertSearchOperator(fldOpr2, fld, fldVal2);
            string wrk = "";
            if (Config.SearchMultiValueOption == 1 && !fld.UseFilter || !IsMultiSearchOperator(fldOpr))
                multiValue = false;
            if (multiValue) {
                wrk = !Empty(fldVal) ? GetMultiSearchSql(fld, fldOpr, fldVal, DbId) : ""; // Field value 1
                string wrk2 = !Empty(fldVal2) ? GetMultiSearchSql(fld, fldOpr2, fldVal2, DbId) : ""; // Field value 2
                AddFilter(ref wrk, wrk2, fldCond);
            } else {
                wrk = GetSearchSql(fld, fldVal, fldOpr, fldCond, fldVal2, fldOpr2, DbId);
            }
            string cond = SearchOption == "AUTO" && (new[] {"AND", "OR"}).Contains(BasicSearch.Type)
                ? BasicSearch.Type
                : SameText(SearchOption, "OR") ? "OR" : "AND";
            AddFilter(ref where, wrk, cond);
        }

        // Show list of filters
        public void ShowFilterList()
        {
            // Initialize
            string filterList = "",
                filter = "",
                captionClass = IsExport("email") ? "ew-filter-caption-email" : "ew-filter-caption",
                captionSuffix = IsExport("email") ? ": " : "";

            // Field Request_No
            filter = QueryBuilderWhere("Request_No");
            if (Empty(filter))
                BuildSearchSql(ref filter, Request_No, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Request_No.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Reference_Doc
            filter = QueryBuilderWhere("Reference_Doc");
            if (Empty(filter))
                BuildSearchSql(ref filter, Reference_Doc, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Reference_Doc.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Reason
            filter = QueryBuilderWhere("Reason");
            if (Empty(filter))
                BuildSearchSql(ref filter, Reason, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Reason.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Request_By
            filter = QueryBuilderWhere("Request_By");
            if (Empty(filter))
                BuildSearchSql(ref filter, Request_By, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Request_By.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Request_By_Name
            filter = QueryBuilderWhere("Request_By_Name");
            if (Empty(filter))
                BuildSearchSql(ref filter, Request_By_Name, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Request_By_Name.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Request_By_Position
            filter = QueryBuilderWhere("Request_By_Position");
            if (Empty(filter))
                BuildSearchSql(ref filter, Request_By_Position, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Request_By_Position.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Request_By_Branch
            filter = QueryBuilderWhere("Request_By_Branch");
            if (Empty(filter))
                BuildSearchSql(ref filter, Request_By_Branch, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Request_By_Branch.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Request_By_Region
            filter = QueryBuilderWhere("Request_By_Region");
            if (Empty(filter))
                BuildSearchSql(ref filter, Request_By_Region, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Request_By_Region.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Request_Industry
            filter = QueryBuilderWhere("Request_Industry");
            if (Empty(filter))
                BuildSearchSql(ref filter, Request_Industry, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Request_Industry.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Customer_ID
            filter = QueryBuilderWhere("Customer_ID");
            if (Empty(filter))
                BuildSearchSql(ref filter, Customer_ID, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Customer_ID.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Customer_Name
            filter = QueryBuilderWhere("Customer_Name");
            if (Empty(filter))
                BuildSearchSql(ref filter, Customer_Name, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Customer_Name.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field SAP_Total
            filter = QueryBuilderWhere("SAP_Total");
            if (Empty(filter))
                BuildSearchSql(ref filter, SAP_Total, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + SAP_Total.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Override_Total
            filter = QueryBuilderWhere("Override_Total");
            if (Empty(filter))
                BuildSearchSql(ref filter, Override_Total, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Override_Total.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Variance_Total
            filter = QueryBuilderWhere("Variance_Total");
            if (Empty(filter))
                BuildSearchSql(ref filter, Variance_Total, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Variance_Total.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Variance_Percent
            filter = QueryBuilderWhere("Variance_Percent");
            if (Empty(filter))
                BuildSearchSql(ref filter, Variance_Percent, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Variance_Percent.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Notes
            filter = QueryBuilderWhere("Notes");
            if (Empty(filter))
                BuildSearchSql(ref filter, Notes, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Notes.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Next_Approver
            filter = QueryBuilderWhere("Next_Approver");
            if (Empty(filter))
                BuildSearchSql(ref filter, Next_Approver, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Next_Approver.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field List_Approver
            filter = QueryBuilderWhere("List_Approver");
            if (Empty(filter))
                BuildSearchSql(ref filter, List_Approver, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + List_Approver.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Last_Update_By
            filter = QueryBuilderWhere("Last_Update_By");
            if (Empty(filter))
                BuildSearchSql(ref filter, Last_Update_By, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Last_Update_By.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Created_By
            filter = QueryBuilderWhere("Created_By");
            if (Empty(filter))
                BuildSearchSql(ref filter, Created_By, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Created_By.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Created_Date
            filter = QueryBuilderWhere("Created_Date");
            if (Empty(filter))
                BuildSearchSql(ref filter, Created_Date, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Created_Date.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field ETL_Date
            filter = QueryBuilderWhere("ETL_Date");
            if (Empty(filter))
                BuildSearchSql(ref filter, ETL_Date, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + ETL_Date.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Request_Status
            filter = QueryBuilderWhere("Request_Status");
            if (Empty(filter))
                BuildSearchSql(ref filter, Request_Status, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Request_Status.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Request_PIC
            filter = QueryBuilderWhere("Request_PIC");
            if (Empty(filter))
                BuildSearchSql(ref filter, Request_PIC, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Request_PIC.Caption + "</span>" + captionSuffix + filter + "</div>";
            if (!Empty(BasicSearch.Keyword))
                filterList += "<div><span class=\"" + captionClass + "\">" + Language.Phrase("BasicSearchKeyword") + "</span>" + captionSuffix + BasicSearch.Keyword + "</div>";

            // Show Filters
            if (!Empty(filterList)) {
                string message = "<div id=\"ew-filter-list\" class=\"callout callout-info d-table\"><div id=\"ew-current-filters\">" +
                    Language.Phrase("CurrentFilters") + "</div>" + filterList + "</div>";
                MessageShowing(ref message, "");
                Write(message);
            } else { // Output empty tag
                Write("<div id=\"ew-filter-list\"></div>");
            }
        }

        // Return basic search WHERE clause based on search keyword and type
        public string BasicSearchWhere(bool def = false) {
            string searchStr = "";
            if (!Security.CanSearch)
                return "";

            // Fields to search
            List<DbField> searchFlds = new ();
            searchFlds.Add(Request_No);
            searchFlds.Add(Reference_Doc);
            searchFlds.Add(Reason);
            searchFlds.Add(Request_By);
            searchFlds.Add(Request_By_Name);
            searchFlds.Add(Request_By_Position);
            searchFlds.Add(Request_By_Branch);
            searchFlds.Add(Request_By_Region);
            searchFlds.Add(Request_Industry);
            searchFlds.Add(Customer_ID);
            searchFlds.Add(Customer_Name);
            searchFlds.Add(Notes);
            searchFlds.Add(Next_Approver);
            searchFlds.Add(List_Approver);
            searchFlds.Add(Last_Update_By);
            searchFlds.Add(Created_By);
            searchFlds.Add(Request_Status);
            searchFlds.Add(Request_PIC);
            string searchKeyword = def ? BasicSearch.KeywordDefault : BasicSearch.Keyword;
            string searchType = def ? BasicSearch.TypeDefault : BasicSearch.Type;

            // Get search SQL
            if (!Empty(searchKeyword)) {
                List<string> list = BasicSearch.KeywordList(def);
                searchStr = GetQuickSearchFilter(searchFlds, list, searchType, BasicSearch.BasicSearchAnyFields, DbId);
                if (!def && (new[] {"", "reset", "resetall"}).Contains(Command))
                    Command = "search";
            }
            if (!def && Command == "search") {
                BasicSearch.SessionKeyword = searchKeyword;
                BasicSearch.SessionType = searchType;

                // Clear rules for QueryBuilder
                SessionRules = "";
            }
            return searchStr;
        }

        // Check if search parm exists
        protected bool CheckSearchParms() {
            // Check basic search
            if (BasicSearch.IssetSession)
                return true;
            if (id.AdvancedSearch.IssetSession)
                return true;
            if (Request_No.AdvancedSearch.IssetSession)
                return true;
            if (Reference_Doc.AdvancedSearch.IssetSession)
                return true;
            if (Reason.AdvancedSearch.IssetSession)
                return true;
            if (Request_By.AdvancedSearch.IssetSession)
                return true;
            if (Request_By_Name.AdvancedSearch.IssetSession)
                return true;
            if (Request_By_Position.AdvancedSearch.IssetSession)
                return true;
            if (Request_By_Branch.AdvancedSearch.IssetSession)
                return true;
            if (Request_By_Region.AdvancedSearch.IssetSession)
                return true;
            if (Request_Industry.AdvancedSearch.IssetSession)
                return true;
            if (Customer_ID.AdvancedSearch.IssetSession)
                return true;
            if (Customer_Name.AdvancedSearch.IssetSession)
                return true;
            if (SAP_Total.AdvancedSearch.IssetSession)
                return true;
            if (Override_Total.AdvancedSearch.IssetSession)
                return true;
            if (Variance_Total.AdvancedSearch.IssetSession)
                return true;
            if (Variance_Percent.AdvancedSearch.IssetSession)
                return true;
            if (Notes.AdvancedSearch.IssetSession)
                return true;
            if (Next_Approver.AdvancedSearch.IssetSession)
                return true;
            if (List_Approver.AdvancedSearch.IssetSession)
                return true;
            if (Last_Update_By.AdvancedSearch.IssetSession)
                return true;
            if (Created_By.AdvancedSearch.IssetSession)
                return true;
            if (Created_Date.AdvancedSearch.IssetSession)
                return true;
            if (ETL_Date.AdvancedSearch.IssetSession)
                return true;
            if (Request_Status.AdvancedSearch.IssetSession)
                return true;
            if (Request_PIC.AdvancedSearch.IssetSession)
                return true;
            return false;
        }

        // Clear all search parameters
        protected void ResetSearchParms() {
            SearchWhere = "";
            SessionSearchWhere = SearchWhere;

            // Clear basic search parameters
            ResetBasicSearchParms();

            // Clear advanced search parameters
            ResetAdvancedSearchParms();

            // Clear queryBuilder
            SessionRules = "";
        }

        // Load advanced search default values
        protected bool LoadAdvancedSearchDefault() {
        return false;
        }

        // Clear all basic search parameters
        protected void ResetBasicSearchParms() {
            BasicSearch.UnsetSession();
        }

        // Clear all advanced search parameters
        protected void ResetAdvancedSearchParms() {
            id.AdvancedSearch.UnsetSession();
            Request_No.AdvancedSearch.UnsetSession();
            Reference_Doc.AdvancedSearch.UnsetSession();
            Reason.AdvancedSearch.UnsetSession();
            Request_By.AdvancedSearch.UnsetSession();
            Request_By_Name.AdvancedSearch.UnsetSession();
            Request_By_Position.AdvancedSearch.UnsetSession();
            Request_By_Branch.AdvancedSearch.UnsetSession();
            Request_By_Region.AdvancedSearch.UnsetSession();
            Request_Industry.AdvancedSearch.UnsetSession();
            Customer_ID.AdvancedSearch.UnsetSession();
            Customer_Name.AdvancedSearch.UnsetSession();
            SAP_Total.AdvancedSearch.UnsetSession();
            Override_Total.AdvancedSearch.UnsetSession();
            Variance_Total.AdvancedSearch.UnsetSession();
            Variance_Percent.AdvancedSearch.UnsetSession();
            Notes.AdvancedSearch.UnsetSession();
            Next_Approver.AdvancedSearch.UnsetSession();
            List_Approver.AdvancedSearch.UnsetSession();
            Last_Update_By.AdvancedSearch.UnsetSession();
            Created_By.AdvancedSearch.UnsetSession();
            Created_Date.AdvancedSearch.UnsetSession();
            ETL_Date.AdvancedSearch.UnsetSession();
            Request_Status.AdvancedSearch.UnsetSession();
            Request_PIC.AdvancedSearch.UnsetSession();
        }

        // Restore all search parameters
        protected void RestoreSearchParms() {
            RestoreSearch = true;

            // Restore basic search values
            BasicSearch.Load();

            // Restore advanced search values
            id.AdvancedSearch.Load();
            Request_No.AdvancedSearch.Load();
            Reference_Doc.AdvancedSearch.Load();
            Reason.AdvancedSearch.Load();
            Request_By.AdvancedSearch.Load();
            Request_By_Name.AdvancedSearch.Load();
            Request_By_Position.AdvancedSearch.Load();
            Request_By_Branch.AdvancedSearch.Load();
            Request_By_Region.AdvancedSearch.Load();
            Request_Industry.AdvancedSearch.Load();
            Customer_ID.AdvancedSearch.Load();
            Customer_Name.AdvancedSearch.Load();
            SAP_Total.AdvancedSearch.Load();
            Override_Total.AdvancedSearch.Load();
            Variance_Total.AdvancedSearch.Load();
            Variance_Percent.AdvancedSearch.Load();
            Notes.AdvancedSearch.Load();
            Next_Approver.AdvancedSearch.Load();
            List_Approver.AdvancedSearch.Load();
            Last_Update_By.AdvancedSearch.Load();
            Created_By.AdvancedSearch.Load();
            Created_Date.AdvancedSearch.Load();
            ETL_Date.AdvancedSearch.Load();
            Request_Status.AdvancedSearch.Load();
            Request_PIC.AdvancedSearch.Load();
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
                UpdateSort(Request_No); // Request_No
                UpdateSort(Reference_Doc); // Reference_Doc
                UpdateSort(Reason); // Reason
                UpdateSort(Request_By); // Request_By
                UpdateSort(Request_By_Name); // Request_By_Name
                UpdateSort(Request_By_Position); // Request_By_Position
                UpdateSort(Request_By_Branch); // Request_By_Branch
                UpdateSort(Request_By_Region); // Request_By_Region
                UpdateSort(Request_Industry); // Request_Industry
                UpdateSort(Customer_ID); // Customer_ID
                UpdateSort(Customer_Name); // Customer_Name
                UpdateSort(SAP_Total); // SAP_Total
                UpdateSort(Override_Total); // Override_Total
                UpdateSort(Variance_Total); // Variance_Total
                UpdateSort(Variance_Percent); // Variance_Percent
                UpdateSort(Notes); // Notes
                UpdateSort(Next_Approver); // Next_Approver
                UpdateSort(List_Approver); // List_Approver
                UpdateSort(Last_Update_By); // Last_Update_By
                UpdateSort(Created_By); // Created_By
                UpdateSort(Created_Date); // Created_Date
                UpdateSort(ETL_Date); // ETL_Date
                UpdateSort(Request_Status); // Request_Status
                UpdateSort(Request_PIC); // Request_PIC
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
                // Reset search criteria
                if (SameText(Command, "reset") || SameText(Command, "resetall"))
                    ResetSearchParms();

                // Reset (clear) sorting order
                if (SameText(Command, "resetsort")) {
                    string orderBy = "";
                    SessionOrderBy = orderBy;
                    id.Sort = "";
                    Request_No.Sort = "";
                    Reference_Doc.Sort = "";
                    Reason.Sort = "";
                    Request_By.Sort = "";
                    Request_By_Name.Sort = "";
                    Request_By_Position.Sort = "";
                    Request_By_Branch.Sort = "";
                    Request_By_Region.Sort = "";
                    Request_Industry.Sort = "";
                    Customer_ID.Sort = "";
                    Customer_Name.Sort = "";
                    SAP_Total.Sort = "";
                    Override_Total.Sort = "";
                    Variance_Total.Sort = "";
                    Variance_Percent.Sort = "";
                    Notes.Sort = "";
                    Next_Approver.Sort = "";
                    List_Approver.Sort = "";
                    Last_Update_By.Sort = "";
                    Created_By.Sort = "";
                    Created_Date.Sort = "";
                    ETL_Date.Sort = "";
                    Request_Status.Sort = "";
                    Request_PIC.Sort = "";
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

            // "detail_tr_request_detail"
            item = ListOptions.Add("detail_tr_request_detail");
            item.CssClass = "text-nowrap";
            item.Visible = Security.AllowList(CurrentProjectID + "tr_request_detail");
            item.OnLeft = true;
            item.ShowInButtonGroup = false;
            trRequestDetailGrid = Resolve("TrRequestDetailGrid")!;

            // "detail_tr_request_approver"
            item = ListOptions.Add("detail_tr_request_approver");
            item.CssClass = "text-nowrap";
            item.Visible = Security.AllowList(CurrentProjectID + "tr_request_approver");
            item.OnLeft = true;
            item.ShowInButtonGroup = false;
            trRequestApproverGrid = Resolve("TrRequestApproverGrid")!;

            // "detail_tr_request_approval_history"
            item = ListOptions.Add("detail_tr_request_approval_history");
            item.CssClass = "text-nowrap";
            item.Visible = Security.AllowList(CurrentProjectID + "tr_request_approval_history");
            item.OnLeft = true;
            item.ShowInButtonGroup = false;
            trRequestApprovalHistoryGrid = Resolve("TrRequestApprovalHistoryGrid")!;

            // Multiple details
            if (ShowMultipleDetails) {
                item = ListOptions.Add("details");
                item.CssClass = "text-nowrap";
                item.Visible = ShowMultipleDetails && ListOptions.DetailVisible();
                item.OnLeft = true;
                item.ShowInButtonGroup = false;
            }

            // Set up detail pages
            var _pages = new SubPages();
            _pages.Add("tr_request_detail");
            _pages.Add("tr_request_approver");
            _pages.Add("tr_request_approval_history");
            DetailPages = _pages;

            // List actions
            item = ListOptions.Add("listactions");
            item.CssClass = "text-nowrap";
            item.OnLeft = true;
            item.Visible = false;
            item.ShowInButtonGroup = false;
            item.ShowInDropDown = false;

            // "checkbox"
            item = ListOptions.Add("checkbox");
            item.CssStyle = "white-space: nowrap; text-align: center; vertical-align: middle; margin: 0px;";
            item.Visible = false;
            item.OnLeft = true;
            item.Header = "<div class=\"form-check\"><input type=\"checkbox\" name=\"key\" id=\"key\" class=\"form-check-input\" data-ew-action=\"select-all-keys\"></div>";
            if (item.OnLeft)
                item.MoveTo(0);
            item.ShowInDropDown = false;
            item.ShowInButtonGroup = false;

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

            // Call ListOptions Load event
            ListOptionsLoad();
            SetupListOptionsExt();
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

            // "sequence"
            listOption = ListOptions["sequence"];
            listOption?.SetBody(FormatSequenceNumber(RecordCount));

            // "view"
            listOption = ListOptions["view"];
            string viewcaption = Language.Phrase("ViewLink", true);
            isVisible = Security.CanView;
            if (isVisible) {
                if (ModalView && !IsMobile())
                    listOption?.SetBody($@"<a class=""ew-row-link ew-view"" title=""{viewcaption}"" data-table=""v_tr_request"" data-caption=""{viewcaption}"" data-ew-action=""modal"" data-action=""view"" data-ajax=""" + (UseAjaxActions ? "true" : "false") + "\" data-url=\"" + HtmlEncode(AppPath(ViewUrl)) + "\" data-btn=\"null\">" + Language.Phrase("ViewLink") + "</a>");
                else
                    listOption?.SetBody($@"<a class=""ew-row-link ew-view"" title=""{viewcaption}"" data-caption=""{viewcaption}"" href=""" + HtmlEncode(AppPath(ViewUrl)) + "\">" + Language.Phrase("ViewLink") + "</a>");
            } else {
                listOption?.Clear();
            }

            // Set up list action buttons
            listOption = ListOptions["listactions"];
            if (listOption != null && !IsExport() && CurrentAction == "") {
                string body = "";
                var links = new List<string>();
                foreach (var (key, act) in ListActions.Items) {
                    if (act.Select == Config.ActionSingle && act.Allowed) {
                        var action = act.Action;
                        string caption = act.Caption;
                        var icon = (act.Icon != "") ? "<i class=\"" + HtmlEncode(act.Icon.Replace(" ew-icon", "")) + "\" data-caption=\"" + HtmlTitle(caption) + "\"></i> " : "";
                        string link = "<li><button type=\"button\" class=\"dropdown-item ew-action ew-list-action\" data-caption=\"" + HtmlTitle(caption) + "\" data-ew-action=\"submit\" form=\"fv_tr_requestlist\" data-key=\"" + KeyToJson(true) + "\"" + act.ToDataAttrs() + ">" + icon + " " + caption + "</button></li>";
                        if (!Empty(link)) {
                            links.Add(link);
                            if (Empty(body)) // Setup first button
                                body = "<button type=\"button\" class=\"btn btn-default ew-action ew-list-action\" title=\"" + HtmlTitle(caption) + "\" data-caption=\"" + HtmlTitle(caption) + "\" data-ew-action=\"submit\" form=\"fv_tr_requestlist\" data-key=\"" + KeyToJson(true) + "\"" + act.ToDataAttrs() + ">" + icon + caption + "</button>";
                        }
                    }
                }
                if (links.Count > 1) { // More than one buttons, use dropdown
                    body = "<button type=\"button\" class=\"dropdown-toggle btn btn-default ew-actions\" title=\"" + Language.Phrase("ListActionButton", true) + "\" data-bs-toggle=\"dropdown\">" + Language.Phrase("ListActionButton") + "</button>";
                    string content = links.Aggregate("", (result, link) => result + "<li>" + link + "</li>");
                    body += "<ul class=\"dropdown-menu" + (listOption?.OnLeft ?? false ? "" : " dropdown-menu-right") + "\">" + content + "</ul>";
                    body = "<div class=\"btn-group btn-group-sm\">" + body + "</div>";
                }
                if (links.Count > 0)
                    listOption?.SetBody(body);
            }
            var detailViewTblVar = "";
            var detailCopyTblVar = "";
            var detailEditTblVar = "";
            dynamic? detailPage = null;
            string detailFilter = "";

            // "detail_tr_request_detail"
            listOption = ListOptions["detail_tr_request_detail"];
            isVisible = Security.AllowList(CurrentProjectID + "tr_request_detail");
            if (isVisible) {
                string caption, title, url;
                var body = Language.Phrase("DetailLink") + Language.TablePhrase("tr_request_detail", "TblCaption");
                body = "<a class=\"btn btn-default ew-row-link ew-detail" + (ListOptions.UseDropDownButton ? " dropdown-toggle" : "") + "\" data-action=\"list\" href=\"" + HtmlEncode(AppPath("TrRequestDetailList?" + Config.TableShowMaster + "=v_tr_request&" + ForeignKeyUrl("fk_id", id.CurrentValue) + "")) + "\">" + body + "</a>";
                string links = "";
                detailPage = Resolve("TrRequestDetailGrid")!;
                if (detailPage?.DetailView && Security.CanView && Security.AllowView(CurrentProjectID + "v_tr_request") ?? false) {
                    caption = Language.Phrase("MasterDetailViewLink", null);
                    title = Language.Phrase("MasterDetailViewLink", true);
                    url = GetViewUrl(Config.TableShowDetail + "=tr_request_detail");
                    links += "<li><a class=\"dropdown-item ew-row-link ew-detail-view\" data-action=\"view\" data-caption=\"" + title + "\" href=\"" + HtmlEncode(AppPath(url)) + "\">" + caption + "</a></li>";
                    if (!Empty(detailViewTblVar))
                        detailViewTblVar += ",";
                    detailViewTblVar += "tr_request_detail";
                }
                if (!Empty(links)) {
                    body += "<button type=\"button\" class=\"dropdown-toggle btn btn-default ew-detail\" data-bs-toggle=\"dropdown\"></button>";
                    body += "<ul class=\"dropdown-menu\">" + links + "</ul>";
                } else {
                    body = Regex.Replace(body, @"\b\s+dropdown-toggle\b", "");
                }
                body = "<div class=\"btn-group btn-group-sm ew-btn-group\">" + body + "</div>";
                listOption?.SetBody(body);
                if (ShowMultipleDetails)
                    listOption?.SetVisible(false);
            }

            // "detail_tr_request_approver"
            listOption = ListOptions["detail_tr_request_approver"];
            isVisible = Security.AllowList(CurrentProjectID + "tr_request_approver");
            if (isVisible) {
                string caption, title, url;
                var body = Language.Phrase("DetailLink") + Language.TablePhrase("tr_request_approver", "TblCaption");
                body = "<a class=\"btn btn-default ew-row-link ew-detail" + (ListOptions.UseDropDownButton ? " dropdown-toggle" : "") + "\" data-action=\"list\" href=\"" + HtmlEncode(AppPath("TrRequestApproverList?" + Config.TableShowMaster + "=v_tr_request&" + ForeignKeyUrl("fk_id", id.CurrentValue) + "")) + "\">" + body + "</a>";
                string links = "";
                detailPage = Resolve("TrRequestApproverGrid")!;
                if (detailPage?.DetailView && Security.CanView && Security.AllowView(CurrentProjectID + "v_tr_request") ?? false) {
                    caption = Language.Phrase("MasterDetailViewLink", null);
                    title = Language.Phrase("MasterDetailViewLink", true);
                    url = GetViewUrl(Config.TableShowDetail + "=tr_request_approver");
                    links += "<li><a class=\"dropdown-item ew-row-link ew-detail-view\" data-action=\"view\" data-caption=\"" + title + "\" href=\"" + HtmlEncode(AppPath(url)) + "\">" + caption + "</a></li>";
                    if (!Empty(detailViewTblVar))
                        detailViewTblVar += ",";
                    detailViewTblVar += "tr_request_approver";
                }
                if (!Empty(links)) {
                    body += "<button type=\"button\" class=\"dropdown-toggle btn btn-default ew-detail\" data-bs-toggle=\"dropdown\"></button>";
                    body += "<ul class=\"dropdown-menu\">" + links + "</ul>";
                } else {
                    body = Regex.Replace(body, @"\b\s+dropdown-toggle\b", "");
                }
                body = "<div class=\"btn-group btn-group-sm ew-btn-group\">" + body + "</div>";
                listOption?.SetBody(body);
                if (ShowMultipleDetails)
                    listOption?.SetVisible(false);
            }

            // "detail_tr_request_approval_history"
            listOption = ListOptions["detail_tr_request_approval_history"];
            isVisible = Security.AllowList(CurrentProjectID + "tr_request_approval_history");
            if (isVisible) {
                string caption, title, url;
                var body = Language.Phrase("DetailLink") + Language.TablePhrase("tr_request_approval_history", "TblCaption");
                body = "<a class=\"btn btn-default ew-row-link ew-detail" + (ListOptions.UseDropDownButton ? " dropdown-toggle" : "") + "\" data-action=\"list\" href=\"" + HtmlEncode(AppPath("TrRequestApprovalHistoryList?" + Config.TableShowMaster + "=v_tr_request&" + ForeignKeyUrl("fk_id", id.CurrentValue) + "")) + "\">" + body + "</a>";
                string links = "";
                detailPage = Resolve("TrRequestApprovalHistoryGrid")!;
                if (detailPage?.DetailView && Security.CanView && Security.AllowView(CurrentProjectID + "v_tr_request") ?? false) {
                    caption = Language.Phrase("MasterDetailViewLink", null);
                    title = Language.Phrase("MasterDetailViewLink", true);
                    url = GetViewUrl(Config.TableShowDetail + "=tr_request_approval_history");
                    links += "<li><a class=\"dropdown-item ew-row-link ew-detail-view\" data-action=\"view\" data-caption=\"" + title + "\" href=\"" + HtmlEncode(AppPath(url)) + "\">" + caption + "</a></li>";
                    if (!Empty(detailViewTblVar))
                        detailViewTblVar += ",";
                    detailViewTblVar += "tr_request_approval_history";
                }
                if (!Empty(links)) {
                    body += "<button type=\"button\" class=\"dropdown-toggle btn btn-default ew-detail\" data-bs-toggle=\"dropdown\"></button>";
                    body += "<ul class=\"dropdown-menu\">" + links + "</ul>";
                } else {
                    body = Regex.Replace(body, @"\b\s+dropdown-toggle\b", "");
                }
                body = "<div class=\"btn-group btn-group-sm ew-btn-group\">" + body + "</div>";
                listOption?.SetBody(body);
                if (ShowMultipleDetails)
                    listOption?.SetVisible(false);
            }
            if (ShowMultipleDetails) {
                var body = Language.Phrase("MultipleMasterDetails");
                body = "<div class=\"btn-group btn-group-sm ew-btn-group\">";
                string links = "";
                if (!Empty(detailViewTblVar)) {
                    links += "<li><a class=\"dropdown-item ew-row-link ew-detail-view\" data-action=\"view\" data-caption=\"" + HtmlEncode(Language.Phrase("MasterDetailViewLink", true)) + "\" href=\"" + HtmlEncode(AppPath(GetViewUrl(Config.TableShowDetail + "=" + detailViewTblVar))) + "\">" + Language.Phrase("MasterDetailViewLink", null) + "</a></li>";
                }
                if (!Empty(detailEditTblVar)) {
                    links += "<li><a class=\"dropdown-item ew-row-link ew-detail-edit\" data-action=\"edit\" data-caption=\"" + HtmlEncode(Language.Phrase("MasterDetailEditLink", true)) + "\" href=\"" + HtmlEncode(AppPath(GetEditUrl(Config.TableShowDetail + "=" + detailEditTblVar))) + "\">" + Language.Phrase("MasterDetailEditLink", null) + "</a></li>";
                }
                if (!Empty(detailCopyTblVar)) {
                    links += "<li><a class=\"dropdown-item ew-row-link ew-detail-copy\" data-action=\"add\" data-caption=\"" + HtmlEncode(Language.Phrase("MasterDetailCopyLink", true)) + "\" href=\"" + HtmlEncode(AppPath(GetCopyUrl(Config.TableShowDetail + "=" + detailCopyTblVar))) + "\">" + Language.Phrase("MasterDetailCopyLink", null) + "</a></li>";
                }
                if (!Empty(links)) {
                    body += "<button type=\"button\" class=\"dropdown-toggle btn btn-default ew-master-detail\" title=\"" + HtmlEncode(Language.Phrase("MultipleMasterDetails", true)) + "\" data-bs-toggle=\"dropdown\">" + Language.Phrase("MultipleMasterDetails") + "</button>";
                    body += "<ul class=\"dropdown-menu ew-dropdown-menu\">" + links + "</ul>";
                }
                body += "</div>";
                // Multiple details
                listOption = ListOptions["details"];
                listOption?.SetBody(body);
            }

            // "checkbox"
            listOption = ListOptions["checkbox"];
            listOption?.SetBody("<div class=\"form-check\"><input type=\"checkbox\" id=\"key_m_" + RowCount + "\" name=\"key_m[]\" class=\"form-check-input ew-multi-select\" value=\"" + HtmlEncode(id.CurrentValue) + "\" data-ew-action=\"select-key\"></div>");
            RenderListOptionsExt();

            // Call ListOptions Rendered event
            ListOptionsRendered();
        }

        // Render list options (extensions)
        protected void RenderListOptionsExt() {
            // Preview extension
            string links = "", btngrps = "", sqlwrk, detaillnk, link, url, btngrp, caption, title, icon, detailFilter;
            List<string> masterKeys = new ();
            ListOption? option;
            dynamic? detailTbl = null, detailPage = null;
            masterKeys.Clear();
            sqlwrk = KeyFilter(Resolve("tr_request_detail")!.request_id, id.CurrentValue, id.DataType, DbId);
            masterKeys.Add(ConvertToString(id.CurrentValue));

            // Column "detail_tr_request_detail"
            if ((DetailPages?["tr_request_detail"]?.Visible ?? false) && Security.AllowList(CurrentProjectID + "tr_request_detail")) {
                link = "";
                option = ListOptions["detail_tr_request_detail"];
                url = "TrRequestDetailPreview?t=v_tr_request&f=" + Encrypt(sqlwrk + "|" + String.Join("|", masterKeys));
                btngrp = "<ul data-table=\"tr_request_detail\" data-url=\"" + AppPath(url) + "\" class=\"ew-detail-btn-group dropdown-menu\">";
                if (Security.AllowList(CurrentProjectID + "v_tr_request")) {
                    string label = Language.TablePhrase("tr_request_detail", "TblCaption");
                    link = "<button class=\"nav-link\" data-bs-toggle=\"tab\" data-table=\"tr_request_detail\" data-url=\"" + AppPath(url) + "\" type=\"button\" role=\"tab\" aria-selected=\"false\">" + label + "</button>";
                    detaillnk = AppPath("TrRequestDetailList?" + Config.TableShowMaster + "=v_tr_request&" + ForeignKeyUrl("fk_id", id.CurrentValue) + "");
                    title = Language.TablePhrase("tr_request_detail", "TblCaption");
                    caption = Language.Phrase("MasterDetailListLink");
                    caption += "&nbsp;" + title;
                    btngrp += "<a class=\"dropdown-item\" href=\"#\" data-ew-action=\"redirect\" data-url=\"" + HtmlEncode(detaillnk) + "\">" + caption + "</a>";
                }
                detailPage = Resolve("TrRequestDetailGrid")!;
                if (detailPage?.DetailView && Security.CanView && Security.AllowView(CurrentProjectID + "v_tr_request") ?? false) {
                    caption = Language.Phrase("MasterDetailViewLink", null);
                    title = Language.Phrase("MasterDetailViewLink", true);
                    url = AppPath(GetViewUrl(Config.TableShowDetail + "=tr_request_detail"));
                    btngrp += "<a class=\"dropdown-item\" href=\"#\" data-ew-action=\"redirect\" data-url=\"" + HtmlEncode(url) + "\">" + caption + "</a>";
                }
                btngrp += "</ul>";
                if (link != "") {
                    link = "<li class=\"nav-item\">" + btngrp + link + "</li>";  // Note: Place btngrp before link
                    links += link;
                    option?.AddBody("<div class=\"ew-preview d-none\">" + link + "</div>");
                }
            }
            masterKeys.Clear();
            sqlwrk = KeyFilter(Resolve("tr_request_approver")!.request_id, id.CurrentValue, id.DataType, DbId);
            masterKeys.Add(ConvertToString(id.CurrentValue));

            // Column "detail_tr_request_approver"
            if ((DetailPages?["tr_request_approver"]?.Visible ?? false) && Security.AllowList(CurrentProjectID + "tr_request_approver")) {
                link = "";
                option = ListOptions["detail_tr_request_approver"];
                url = "TrRequestApproverPreview?t=v_tr_request&f=" + Encrypt(sqlwrk + "|" + String.Join("|", masterKeys));
                btngrp = "<ul data-table=\"tr_request_approver\" data-url=\"" + AppPath(url) + "\" class=\"ew-detail-btn-group dropdown-menu\">";
                if (Security.AllowList(CurrentProjectID + "v_tr_request")) {
                    string label = Language.TablePhrase("tr_request_approver", "TblCaption");
                    link = "<button class=\"nav-link\" data-bs-toggle=\"tab\" data-table=\"tr_request_approver\" data-url=\"" + AppPath(url) + "\" type=\"button\" role=\"tab\" aria-selected=\"false\">" + label + "</button>";
                    detaillnk = AppPath("TrRequestApproverList?" + Config.TableShowMaster + "=v_tr_request&" + ForeignKeyUrl("fk_id", id.CurrentValue) + "");
                    title = Language.TablePhrase("tr_request_approver", "TblCaption");
                    caption = Language.Phrase("MasterDetailListLink");
                    caption += "&nbsp;" + title;
                    btngrp += "<a class=\"dropdown-item\" href=\"#\" data-ew-action=\"redirect\" data-url=\"" + HtmlEncode(detaillnk) + "\">" + caption + "</a>";
                }
                detailPage = Resolve("TrRequestApproverGrid")!;
                if (detailPage?.DetailView && Security.CanView && Security.AllowView(CurrentProjectID + "v_tr_request") ?? false) {
                    caption = Language.Phrase("MasterDetailViewLink", null);
                    title = Language.Phrase("MasterDetailViewLink", true);
                    url = AppPath(GetViewUrl(Config.TableShowDetail + "=tr_request_approver"));
                    btngrp += "<a class=\"dropdown-item\" href=\"#\" data-ew-action=\"redirect\" data-url=\"" + HtmlEncode(url) + "\">" + caption + "</a>";
                }
                btngrp += "</ul>";
                if (link != "") {
                    link = "<li class=\"nav-item\">" + btngrp + link + "</li>";  // Note: Place btngrp before link
                    links += link;
                    option?.AddBody("<div class=\"ew-preview d-none\">" + link + "</div>");
                }
            }
            masterKeys.Clear();
            sqlwrk = KeyFilter(Resolve("tr_request_approval_history")!.request_id, id.CurrentValue, id.DataType, DbId);
            masterKeys.Add(ConvertToString(id.CurrentValue));

            // Column "detail_tr_request_approval_history"
            if ((DetailPages?["tr_request_approval_history"]?.Visible ?? false) && Security.AllowList(CurrentProjectID + "tr_request_approval_history")) {
                link = "";
                option = ListOptions["detail_tr_request_approval_history"];
                url = "TrRequestApprovalHistoryPreview?t=v_tr_request&f=" + Encrypt(sqlwrk + "|" + String.Join("|", masterKeys));
                btngrp = "<ul data-table=\"tr_request_approval_history\" data-url=\"" + AppPath(url) + "\" class=\"ew-detail-btn-group dropdown-menu\">";
                if (Security.AllowList(CurrentProjectID + "v_tr_request")) {
                    string label = Language.TablePhrase("tr_request_approval_history", "TblCaption");
                    link = "<button class=\"nav-link\" data-bs-toggle=\"tab\" data-table=\"tr_request_approval_history\" data-url=\"" + AppPath(url) + "\" type=\"button\" role=\"tab\" aria-selected=\"false\">" + label + "</button>";
                    detaillnk = AppPath("TrRequestApprovalHistoryList?" + Config.TableShowMaster + "=v_tr_request&" + ForeignKeyUrl("fk_id", id.CurrentValue) + "");
                    title = Language.TablePhrase("tr_request_approval_history", "TblCaption");
                    caption = Language.Phrase("MasterDetailListLink");
                    caption += "&nbsp;" + title;
                    btngrp += "<a class=\"dropdown-item\" href=\"#\" data-ew-action=\"redirect\" data-url=\"" + HtmlEncode(detaillnk) + "\">" + caption + "</a>";
                }
                detailPage = Resolve("TrRequestApprovalHistoryGrid")!;
                if (detailPage?.DetailView && Security.CanView && Security.AllowView(CurrentProjectID + "v_tr_request") ?? false) {
                    caption = Language.Phrase("MasterDetailViewLink", null);
                    title = Language.Phrase("MasterDetailViewLink", true);
                    url = AppPath(GetViewUrl(Config.TableShowDetail + "=tr_request_approval_history"));
                    btngrp += "<a class=\"dropdown-item\" href=\"#\" data-ew-action=\"redirect\" data-url=\"" + HtmlEncode(url) + "\">" + caption + "</a>";
                }
                btngrp += "</ul>";
                if (link != "") {
                    link = "<li class=\"nav-item\">" + btngrp + link + "</li>";  // Note: Place btngrp before link
                    links += link;
                    option?.AddBody("<div class=\"ew-preview d-none\">" + link + "</div>");
                }
            }

            // Add row attributes for expandable row
            if (RowType == RowType.View) {
                RowAttrs["data-widget"] = "expandable-table";
                RowAttrs["aria-expanded"] = "false";
            }

            // Column "preview"
            option = ListOptions["preview"];
            if (option == null) { // Add preview column
                option = ListOptions.Add("preview");
                option.OnLeft = true;
                option.MoveTo(option.OnLeft ? ListOptions.GetItemIndex("checkbox") + 1 : ListOptions.GetItemIndex("checkbox"));
                option.Visible = !(IsExport() || IsGridAdd || IsGridEdit || IsMultiEdit);
                option.ShowInDropDown = false;
                option.ShowInButtonGroup = false;
            }
            icon = "fa-solid fa-caret-right"; // Right
            if (SameText(GetPropertyValue(this, "MultiColumnLayout"), "table")) {
                option.CssStyle = "width: 1%;";
                if (!option.OnLeft)
                    icon = Regex.Replace(icon, @"\bright\b", "left");
            }
            if (IsRTL) { // Reverse
                if (Regex.IsMatch(icon, @"\bleft\b"))
                    icon = Regex.Replace(icon, @"\bleft\b", "right");
                else if (Regex.IsMatch(icon, @"\bright\b"))
                    icon = Regex.Replace(icon, @"\bright\b", "left");
            }
            option.SetBody("<i role=\"button\" class=\"ew-preview-btn expandable-table-caret ew-icon " + icon + "\"></i>" +
                "<div class=\"ew-preview d-none\">" + links + "</div>");
            if (option.Visible)
                option.Visible = links != "";

            // Column "details" (Multiple details)
            option = ListOptions["details"];
            option?.AddBody("<div class=\"ew-preview d-none\">" + links + "</div>");
            if (option?.Visible ?? false)
                option.Visible = links != "";
        }

        // Set up other options
        protected void SetupOtherOptions() {
            ListOptions option;
            ListOption item;
            var options = OtherOptions;
            option = options["action"];

            // Show column list for column visibility
            if (UseColumnVisibility) {
                option = OtherOptions["column"];
                item = option.AddGroupOption();
                item.Body = "";
                item.Visible = UseColumnVisibility;
                CreateColumnOption(option.Add("Request_No")); // DN
                CreateColumnOption(option.Add("Reference_Doc")); // DN
                CreateColumnOption(option.Add("Reason")); // DN
                CreateColumnOption(option.Add("Request_By")); // DN
                CreateColumnOption(option.Add("Request_By_Name")); // DN
                CreateColumnOption(option.Add("Request_By_Position")); // DN
                CreateColumnOption(option.Add("Request_By_Branch")); // DN
                CreateColumnOption(option.Add("Request_By_Region")); // DN
                CreateColumnOption(option.Add("Request_Industry")); // DN
                CreateColumnOption(option.Add("Customer_ID")); // DN
                CreateColumnOption(option.Add("Customer_Name")); // DN
                CreateColumnOption(option.Add("SAP_Total")); // DN
                CreateColumnOption(option.Add("Override_Total")); // DN
                CreateColumnOption(option.Add("Variance_Total")); // DN
                CreateColumnOption(option.Add("Variance_Percent")); // DN
                CreateColumnOption(option.Add("Notes")); // DN
                CreateColumnOption(option.Add("Next_Approver")); // DN
                CreateColumnOption(option.Add("List_Approver")); // DN
                CreateColumnOption(option.Add("Last_Update_By")); // DN
                CreateColumnOption(option.Add("Created_By")); // DN
                CreateColumnOption(option.Add("Created_Date")); // DN
                CreateColumnOption(option.Add("ETL_Date")); // DN
                CreateColumnOption(option.Add("Request_Status")); // DN
                CreateColumnOption(option.Add("Request_PIC")); // DN
            }

            // Set up options default
            foreach (var (key, opt) in options) {
                if (key != "column") { // Always use dropdown for column
                    opt.UseDropDownButton = true;
                    opt.UseButtonGroup = true;
                }
                //opt.ButtonClass = ""; // Class for button group
                item = opt.AddGroupOption();
                item.Body = "";
                item.Visible = false;
            }
            options["addedit"].DropDownButtonPhrase = "ButtonAddEdit";
            options["detail"].DropDownButtonPhrase = "ButtonDetails";
            options["action"].DropDownButtonPhrase = "ButtonActions";

            // Filter button
            item = FilterOptions.Add("savecurrentfilter");
            item.Body = "<a class=\"ew-save-filter\" data-form=\"fv_tr_requestsrch\" data-ew-action=\"none\">" + Language.Phrase("SaveCurrentFilter") + "</a>";
            item.Visible = true;
            item = FilterOptions.Add("deletefilter");
            item.Body = "<a class=\"ew-delete-filter\" data-form=\"fv_tr_requestsrch\" data-ew-action=\"none\">" + Language.Phrase("DeleteFilter") + "</a>";
            item.Visible = true;
            FilterOptions.UseDropDownButton = true;
            FilterOptions.UseButtonGroup = !FilterOptions.UseDropDownButton;
            FilterOptions.DropDownButtonPhrase = "Filters";

            // Add group option item
            item = FilterOptions.AddGroupOption();
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
                option = options["action"];

                // Set up list action buttons
                foreach (var (key, act) in ListActions.Items.Where(kvp => kvp.Value.Select == Config.ActionMultiple)) {
                    item = option.Add("custom_" + act.Action);
                    string caption = act.Caption;
                    var icon = (act.Icon != "") ? "<i class=\"" + HtmlEncode(act.Icon) + "\" data-caption=\"" + HtmlEncode(caption) + "\"></i>" + caption : caption;
                    item.Body = "<<button type=\"button\" class=\"btn btn-default ew-action ew-list-action\" title=\"" + HtmlEncode(caption) + "\" data-caption=\"" + HtmlEncode(caption) + "\" data-ew-action=\"submit\" form=\"fv_tr_requestlist\"" + act.ToDataAttrs() + ">" + icon + "</button>";
                    item.Visible = act.Allowed;
                }

                // Hide multi edit, grid edit and other options
                if (TotalRecords <= 0) {
                    option = options["addedit"];
                    option?["gridedit"]?.SetVisible(false);
                    option = options["action"];
                    option.HideAllOptions();
                }
        }

        // Process list action
        public async Task<IActionResult> ProcessListAction()
        {
            string filter = GetFilterFromRecordKeys();
            string userAction = Post("action");
            if (filter != "" && userAction != "") {
                // Check permission first
                string actionCaption = userAction;
                foreach (var (key, act) in ListActions.Items) {
                    if (SameString(key, userAction)) {
                        actionCaption = act.Caption;
                        if (CustomActions.ContainsKey(userAction)) {
                            UserAction = userAction;
                            CurrentAction = "";
                        }
                        if (!act.Allowed) {
                            string errmsg = Language.Phrase("CustomActionNotAllowed").Replace("%s", actionCaption);
                            if (Post("ajax") == userAction) // Ajax
                                return Controller.Content("<p class=\"text-danger\">" + errmsg + "</p>", "text/plain", Encoding.UTF8);
                            else
                                FailureMessage = errmsg;
                            return new EmptyResult();
                        }
                    }
                }
                CurrentFilter = filter;
                string sql = CurrentSql;
                var rsuser = await Connection.GetRowsAsync(sql);
                ActionValue = Post("actionvalue");

                // Call row custom action event
                if (rsuser != null) {
                    if (UseTransaction)
                        Connection.BeginTrans();
                    bool processed = true;
                    SelectedCount = rsuser.Count();
                    SelectedIndex = 0;
                    foreach (var row in rsuser) {
                        SelectedIndex++;
                        processed = RowCustomAction(userAction, row);
                        if (!processed)
                            break;
                    }
                    if (processed) {
                        if (UseTransaction)
                            Connection.CommitTrans(); // Commit the changes
                        if (Empty(SuccessMessage))
                            SuccessMessage = Language.Phrase("CustomActionCompleted").Replace("%s", actionCaption); // Set up success message
                    } else {
                        if (UseTransaction)
                            Connection.RollbackTrans(); // Rollback changes

                        // Set up error message
                        if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {
                            // Use the message, do nothing
                        } else if (!Empty(CancelMessage)) {
                            FailureMessage = CancelMessage;
                            CancelMessage = "";
                        } else {
                            FailureMessage = Language.Phrase("CustomActionFailed").Replace("%s", actionCaption);
                        }
                    }
                }
                CurrentAction = ""; // Clear action
                if (Post("ajax") == userAction) { // Ajax
                    if (ActionResult != null) // Action result set by Row CustomAction event // DN
                        return ActionResult;
                    string msg = "";
                    if (SuccessMessage != "") {
                        msg = "<p class=\"text-success\">" + SuccessMessage + "</p>";
                        ClearSuccessMessage(); // Clear message
                    }
                    if (FailureMessage != "") {
                        msg = "<p class=\"text-danger\">" + FailureMessage + "</p>";
                        ClearFailureMessage(); // Clear message
                    }
                    if (!Empty(msg))
                        return Controller.Content(msg, "text/plain", Encoding.UTF8);
                }
            }
            return new EmptyResult(); // Not ajax request
        }

        // Set up Grid
        public async Task SetupGrid()
        {
            if (ExportAll && IsExport()) {
                StopRecord = TotalRecords;
            } else {
                // Set the last record to display
                if (TotalRecords > StartRecord + DisplayRecords - 1) {
                    StopRecord = StartRecord + DisplayRecords - 1;
                } else {
                    StopRecord = TotalRecords;
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
                    RowAttrs.Add("id", "r0_v_tr_request");
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

            // Set up key count
            KeyCount = ConvertToInt(RowIndex);

            // Init row class and style
            ResetAttributes();
            CssClass = "";
            if (IsCopy && InlineRowCount == 0 && !await LoadRow()) { // Inline copy
                CurrentAction = "add";
            }
            if (IsAdd && InlineRowCount == 0 || IsGridAdd) {
                await LoadRowValues(); // Load default values
                OldKey = "";
                SetKey(OldKey);
            } else if (IsInlineInserted && UseInfiniteScroll) {
                // Nothing to do, just use current values
            } else if (!(IsCopy && InlineRowCount == 0)) {
                await LoadRowValues(Recordset); // Load row values
                if (IsGridEdit || IsMultiEdit) {
                    OldKey = GetKey(true); // Get from CurrentValue
                    SetKey(OldKey);
                }
            }
            RowType = RowType.View; // Render view
            if ((IsAdd || IsCopy) && InlineRowCount == 0 || IsGridAdd) // Add
                RowType = RowType.Add; // Render add

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
            RowAttrs.Add("data-rowindex", ConvertToString(vTrRequestList.RowCount));
            RowAttrs.Add("data-key", GetKey(true));
            RowAttrs.Add("id", "r" + ConvertToString(vTrRequestList.RowCount) + "_v_tr_request");
            RowAttrs.Add("data-rowtype", ConvertToString((int)RowType));
            RowAttrs.AppendClass(vTrRequestList.RowCount % 2 != 1 ? "ew-table-alt-row" : "");
            if (IsAdd && vTrRequestList.RowType == RowType.Add || IsEdit && vTrRequestList.RowType == RowType.Edit) // Inline-Add/Edit row
                RowAttrs.AppendClass("table-active");

            // Render row
            await RenderRow();

            // Render list options
            await RenderListOptions();
        }

        // Load basic search values // DN
        protected void LoadBasicSearchValues() {
            if (Get(Config.TableBasicSearch, out StringValues keyword))
                BasicSearch.Keyword = keyword.ToString();
            if (!Empty(BasicSearch.Keyword) && Empty(Command))
                Command = "search";
            if (Get(Config.TableBasicSearchType, out StringValues type))
                BasicSearch.Type = type.ToString();
        }

        // Load search values for validation // DN
        protected void LoadSearchValues() {
            // Load query builder rules
            string rules = Post("rules");
            if (!Empty(rules) && Empty(Command)) {
                QueryRules = rules;
                Command = "search";
            }

            // id
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_id"))
                    id.AdvancedSearch.SearchValue = Get("x_id");
                else
                    id.AdvancedSearch.SearchValue = Get("id"); // Default Value // DN
            if (!Empty(id.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_id"))
                id.AdvancedSearch.SearchOperator = Get("z_id");

            // Request_No
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Request_No"))
                    Request_No.AdvancedSearch.SearchValue = Get("x_Request_No");
                else
                    Request_No.AdvancedSearch.SearchValue = Get("Request_No"); // Default Value // DN
            if (!Empty(Request_No.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Request_No"))
                Request_No.AdvancedSearch.SearchOperator = Get("z_Request_No");

            // Reference_Doc
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Reference_Doc"))
                    Reference_Doc.AdvancedSearch.SearchValue = Get("x_Reference_Doc");
                else
                    Reference_Doc.AdvancedSearch.SearchValue = Get("Reference_Doc"); // Default Value // DN
            if (!Empty(Reference_Doc.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Reference_Doc"))
                Reference_Doc.AdvancedSearch.SearchOperator = Get("z_Reference_Doc");

            // Reason
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Reason"))
                    Reason.AdvancedSearch.SearchValue = Get("x_Reason");
                else
                    Reason.AdvancedSearch.SearchValue = Get("Reason"); // Default Value // DN
            if (!Empty(Reason.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Reason"))
                Reason.AdvancedSearch.SearchOperator = Get("z_Reason");

            // Request_By
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Request_By"))
                    Request_By.AdvancedSearch.SearchValue = Get("x_Request_By");
                else
                    Request_By.AdvancedSearch.SearchValue = Get("Request_By"); // Default Value // DN
            if (!Empty(Request_By.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Request_By"))
                Request_By.AdvancedSearch.SearchOperator = Get("z_Request_By");

            // Request_By_Name
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Request_By_Name"))
                    Request_By_Name.AdvancedSearch.SearchValue = Get("x_Request_By_Name");
                else
                    Request_By_Name.AdvancedSearch.SearchValue = Get("Request_By_Name"); // Default Value // DN
            if (!Empty(Request_By_Name.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Request_By_Name"))
                Request_By_Name.AdvancedSearch.SearchOperator = Get("z_Request_By_Name");

            // Request_By_Position
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Request_By_Position"))
                    Request_By_Position.AdvancedSearch.SearchValue = Get("x_Request_By_Position");
                else
                    Request_By_Position.AdvancedSearch.SearchValue = Get("Request_By_Position"); // Default Value // DN
            if (!Empty(Request_By_Position.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Request_By_Position"))
                Request_By_Position.AdvancedSearch.SearchOperator = Get("z_Request_By_Position");

            // Request_By_Branch
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Request_By_Branch"))
                    Request_By_Branch.AdvancedSearch.SearchValue = Get("x_Request_By_Branch");
                else
                    Request_By_Branch.AdvancedSearch.SearchValue = Get("Request_By_Branch"); // Default Value // DN
            if (!Empty(Request_By_Branch.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Request_By_Branch"))
                Request_By_Branch.AdvancedSearch.SearchOperator = Get("z_Request_By_Branch");

            // Request_By_Region
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Request_By_Region"))
                    Request_By_Region.AdvancedSearch.SearchValue = Get("x_Request_By_Region");
                else
                    Request_By_Region.AdvancedSearch.SearchValue = Get("Request_By_Region"); // Default Value // DN
            if (!Empty(Request_By_Region.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Request_By_Region"))
                Request_By_Region.AdvancedSearch.SearchOperator = Get("z_Request_By_Region");

            // Request_Industry
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Request_Industry"))
                    Request_Industry.AdvancedSearch.SearchValue = Get("x_Request_Industry");
                else
                    Request_Industry.AdvancedSearch.SearchValue = Get("Request_Industry"); // Default Value // DN
            if (!Empty(Request_Industry.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Request_Industry"))
                Request_Industry.AdvancedSearch.SearchOperator = Get("z_Request_Industry");

            // Customer_ID
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Customer_ID"))
                    Customer_ID.AdvancedSearch.SearchValue = Get("x_Customer_ID");
                else
                    Customer_ID.AdvancedSearch.SearchValue = Get("Customer_ID"); // Default Value // DN
            if (!Empty(Customer_ID.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Customer_ID"))
                Customer_ID.AdvancedSearch.SearchOperator = Get("z_Customer_ID");

            // Customer_Name
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Customer_Name"))
                    Customer_Name.AdvancedSearch.SearchValue = Get("x_Customer_Name");
                else
                    Customer_Name.AdvancedSearch.SearchValue = Get("Customer_Name"); // Default Value // DN
            if (!Empty(Customer_Name.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Customer_Name"))
                Customer_Name.AdvancedSearch.SearchOperator = Get("z_Customer_Name");

            // SAP_Total
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_SAP_Total"))
                    SAP_Total.AdvancedSearch.SearchValue = Get("x_SAP_Total");
                else
                    SAP_Total.AdvancedSearch.SearchValue = Get("SAP_Total"); // Default Value // DN
            if (!Empty(SAP_Total.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_SAP_Total"))
                SAP_Total.AdvancedSearch.SearchOperator = Get("z_SAP_Total");

            // Override_Total
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Override_Total"))
                    Override_Total.AdvancedSearch.SearchValue = Get("x_Override_Total");
                else
                    Override_Total.AdvancedSearch.SearchValue = Get("Override_Total"); // Default Value // DN
            if (!Empty(Override_Total.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Override_Total"))
                Override_Total.AdvancedSearch.SearchOperator = Get("z_Override_Total");

            // Variance_Total
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Variance_Total"))
                    Variance_Total.AdvancedSearch.SearchValue = Get("x_Variance_Total");
                else
                    Variance_Total.AdvancedSearch.SearchValue = Get("Variance_Total"); // Default Value // DN
            if (!Empty(Variance_Total.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Variance_Total"))
                Variance_Total.AdvancedSearch.SearchOperator = Get("z_Variance_Total");

            // Variance_Percent
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Variance_Percent"))
                    Variance_Percent.AdvancedSearch.SearchValue = Get("x_Variance_Percent");
                else
                    Variance_Percent.AdvancedSearch.SearchValue = Get("Variance_Percent"); // Default Value // DN
            if (!Empty(Variance_Percent.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Variance_Percent"))
                Variance_Percent.AdvancedSearch.SearchOperator = Get("z_Variance_Percent");

            // Notes
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Notes"))
                    Notes.AdvancedSearch.SearchValue = Get("x_Notes");
                else
                    Notes.AdvancedSearch.SearchValue = Get("Notes"); // Default Value // DN
            if (!Empty(Notes.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Notes"))
                Notes.AdvancedSearch.SearchOperator = Get("z_Notes");

            // Next_Approver
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Next_Approver"))
                    Next_Approver.AdvancedSearch.SearchValue = Get("x_Next_Approver");
                else
                    Next_Approver.AdvancedSearch.SearchValue = Get("Next_Approver"); // Default Value // DN
            if (!Empty(Next_Approver.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Next_Approver"))
                Next_Approver.AdvancedSearch.SearchOperator = Get("z_Next_Approver");

            // List_Approver
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_List_Approver"))
                    List_Approver.AdvancedSearch.SearchValue = Get("x_List_Approver");
                else
                    List_Approver.AdvancedSearch.SearchValue = Get("List_Approver"); // Default Value // DN
            if (!Empty(List_Approver.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_List_Approver"))
                List_Approver.AdvancedSearch.SearchOperator = Get("z_List_Approver");

            // Last_Update_By
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Last_Update_By"))
                    Last_Update_By.AdvancedSearch.SearchValue = Get("x_Last_Update_By");
                else
                    Last_Update_By.AdvancedSearch.SearchValue = Get("Last_Update_By"); // Default Value // DN
            if (!Empty(Last_Update_By.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Last_Update_By"))
                Last_Update_By.AdvancedSearch.SearchOperator = Get("z_Last_Update_By");

            // Created_By
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Created_By"))
                    Created_By.AdvancedSearch.SearchValue = Get("x_Created_By");
                else
                    Created_By.AdvancedSearch.SearchValue = Get("Created_By"); // Default Value // DN
            if (!Empty(Created_By.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Created_By"))
                Created_By.AdvancedSearch.SearchOperator = Get("z_Created_By");

            // Created_Date
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Created_Date"))
                    Created_Date.AdvancedSearch.SearchValue = Get("x_Created_Date");
                else
                    Created_Date.AdvancedSearch.SearchValue = Get("Created_Date"); // Default Value // DN
            if (!Empty(Created_Date.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Created_Date"))
                Created_Date.AdvancedSearch.SearchOperator = Get("z_Created_Date");

            // ETL_Date
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_ETL_Date"))
                    ETL_Date.AdvancedSearch.SearchValue = Get("x_ETL_Date");
                else
                    ETL_Date.AdvancedSearch.SearchValue = Get("ETL_Date"); // Default Value // DN
            if (!Empty(ETL_Date.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_ETL_Date"))
                ETL_Date.AdvancedSearch.SearchOperator = Get("z_ETL_Date");

            // Request_Status
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Request_Status"))
                    Request_Status.AdvancedSearch.SearchValue = Get("x_Request_Status");
                else
                    Request_Status.AdvancedSearch.SearchValue = Get("Request_Status"); // Default Value // DN
            if (!Empty(Request_Status.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Request_Status"))
                Request_Status.AdvancedSearch.SearchOperator = Get("z_Request_Status");

            // Request_PIC
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Request_PIC"))
                    Request_PIC.AdvancedSearch.SearchValue = Get("x_Request_PIC");
                else
                    Request_PIC.AdvancedSearch.SearchValue = Get("Request_PIC"); // Default Value // DN
            if (!Empty(Request_PIC.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Request_PIC"))
                Request_PIC.AdvancedSearch.SearchOperator = Get("z_Request_PIC");
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
            List_Approver.SetDbValue(row["List_Approver"]);
            Last_Update_By.SetDbValue(row["Last_Update_By"]);
            Created_By.SetDbValue(row["Created_By"]);
            Created_Date.SetDbValue(row["Created_Date"]);
            ETL_Date.SetDbValue(row["ETL_Date"]);
            Request_Status.SetDbValue(row["Request_Status"]);
            Request_PIC.SetDbValue(row["Request_PIC"]);
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
            row.Add("List_Approver", List_Approver.DefaultValue ?? DbNullValue); // DN
            row.Add("Last_Update_By", Last_Update_By.DefaultValue ?? DbNullValue); // DN
            row.Add("Created_By", Created_By.DefaultValue ?? DbNullValue); // DN
            row.Add("Created_Date", Created_Date.DefaultValue ?? DbNullValue); // DN
            row.Add("ETL_Date", ETL_Date.DefaultValue ?? DbNullValue); // DN
            row.Add("Request_Status", Request_Status.DefaultValue ?? DbNullValue); // DN
            row.Add("Request_PIC", Request_PIC.DefaultValue ?? DbNullValue); // DN
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

            // Request_Status
            Request_Status.CellCssStyle = "white-space: nowrap;";

            // Request_PIC
            Request_PIC.CellCssStyle = "white-space: nowrap;";

            // View row
            if (RowType == RowType.View) {
                // Request_No
                Request_No.ViewValue = ConvertToString(Request_No.CurrentValue); // DN
                Request_No.ViewCustomAttributes = "";

                // Reference_Doc
                Reference_Doc.ViewValue = ConvertToString(Reference_Doc.CurrentValue); // DN
                Reference_Doc.ViewCustomAttributes = "";

                // Reason
                Reason.ViewValue = ConvertToString(Reason.CurrentValue); // DN
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
                Notes.ViewValue = ConvertToString(Notes.CurrentValue); // DN
                Notes.ViewCustomAttributes = "";

                // Next_Approver
                Next_Approver.ViewValue = ConvertToString(Next_Approver.CurrentValue); // DN
                Next_Approver.ViewCustomAttributes = "";

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

                // ETL_Date
                ETL_Date.ViewValue = ETL_Date.CurrentValue;
                ETL_Date.ViewValue = FormatDateTime(ETL_Date.ViewValue, ETL_Date.FormatPattern);
                ETL_Date.ViewCustomAttributes = "";

                // Request_Status
                Request_Status.ViewValue = ConvertToString(Request_Status.CurrentValue); // DN
                Request_Status.ViewCustomAttributes = "";

                // Request_PIC
                Request_PIC.ViewValue = ConvertToString(Request_PIC.CurrentValue); // DN
                Request_PIC.ViewCustomAttributes = "";

                // Request_No
                Request_No.HrefValue = "";
                Request_No.TooltipValue = "";
                if (!IsExport())
                    Request_No.ViewValue = Request_No.HighlightValue(Request_No.ViewValue);

                // Reference_Doc
                Reference_Doc.HrefValue = "";
                Reference_Doc.TooltipValue = "";
                if (!IsExport())
                    Reference_Doc.ViewValue = Reference_Doc.HighlightValue(Reference_Doc.ViewValue);

                // Reason
                Reason.HrefValue = "";
                Reason.TooltipValue = "";
                if (!IsExport())
                    Reason.ViewValue = Reason.HighlightValue(Reason.ViewValue);

                // Request_By
                Request_By.HrefValue = "";
                Request_By.TooltipValue = "";
                if (!IsExport())
                    Request_By.ViewValue = Request_By.HighlightValue(Request_By.ViewValue);

                // Request_By_Name
                Request_By_Name.HrefValue = "";
                Request_By_Name.TooltipValue = "";
                if (!IsExport())
                    Request_By_Name.ViewValue = Request_By_Name.HighlightValue(Request_By_Name.ViewValue);

                // Request_By_Position
                Request_By_Position.HrefValue = "";
                Request_By_Position.TooltipValue = "";
                if (!IsExport())
                    Request_By_Position.ViewValue = Request_By_Position.HighlightValue(Request_By_Position.ViewValue);

                // Request_By_Branch
                Request_By_Branch.HrefValue = "";
                Request_By_Branch.TooltipValue = "";
                if (!IsExport())
                    Request_By_Branch.ViewValue = Request_By_Branch.HighlightValue(Request_By_Branch.ViewValue);

                // Request_By_Region
                Request_By_Region.HrefValue = "";
                Request_By_Region.TooltipValue = "";
                if (!IsExport())
                    Request_By_Region.ViewValue = Request_By_Region.HighlightValue(Request_By_Region.ViewValue);

                // Request_Industry
                Request_Industry.HrefValue = "";
                Request_Industry.TooltipValue = "";
                if (!IsExport())
                    Request_Industry.ViewValue = Request_Industry.HighlightValue(Request_Industry.ViewValue);

                // Customer_ID
                Customer_ID.HrefValue = "";
                Customer_ID.TooltipValue = "";
                if (!IsExport())
                    Customer_ID.ViewValue = Customer_ID.HighlightValue(Customer_ID.ViewValue);

                // Customer_Name
                Customer_Name.HrefValue = "";
                Customer_Name.TooltipValue = "";
                if (!IsExport())
                    Customer_Name.ViewValue = Customer_Name.HighlightValue(Customer_Name.ViewValue);

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
                if (!IsExport())
                    Notes.ViewValue = Notes.HighlightValue(Notes.ViewValue);

                // Next_Approver
                Next_Approver.HrefValue = "";
                Next_Approver.TooltipValue = "";
                if (!IsExport())
                    Next_Approver.ViewValue = Next_Approver.HighlightValue(Next_Approver.ViewValue);

                // List_Approver
                List_Approver.HrefValue = "";
                List_Approver.TooltipValue = "";
                if (!IsExport())
                    List_Approver.ViewValue = List_Approver.HighlightValue(List_Approver.ViewValue);

                // Last_Update_By
                Last_Update_By.HrefValue = "";
                Last_Update_By.TooltipValue = "";
                if (!IsExport())
                    Last_Update_By.ViewValue = Last_Update_By.HighlightValue(Last_Update_By.ViewValue);

                // Created_By
                Created_By.HrefValue = "";
                Created_By.TooltipValue = "";
                if (!IsExport())
                    Created_By.ViewValue = Created_By.HighlightValue(Created_By.ViewValue);

                // Created_Date
                Created_Date.HrefValue = "";
                Created_Date.TooltipValue = "";

                // ETL_Date
                ETL_Date.HrefValue = "";
                ETL_Date.TooltipValue = "";

                // Request_Status
                Request_Status.HrefValue = "";
                Request_Status.TooltipValue = "";
                if (!IsExport())
                    Request_Status.ViewValue = Request_Status.HighlightValue(Request_Status.ViewValue);

                // Request_PIC
                Request_PIC.HrefValue = "";
                Request_PIC.TooltipValue = "";
                if (!IsExport())
                    Request_PIC.ViewValue = Request_PIC.HighlightValue(Request_PIC.ViewValue);
            }

            // Call Row Rendered event
            if (RowType != RowType.AggregateInit)
                RowRendered();
        }
        #pragma warning restore 1998

        // Validate search
        protected bool ValidateSearch() {
            // Check if validation required
            if (!Config.ServerValidate)
                return true;

            // Return validate result
            bool validateSearch = !HasInvalidFields();

            // Call Form CustomValidate event
            string formCustomError = "";
            validateSearch = validateSearch && FormCustomValidate(ref formCustomError);
            if (!Empty(formCustomError))
                FailureMessage = formCustomError;
            return validateSearch;
        }

        /// <summary>
        /// Send event
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="type">Type</param>
        public void SendEvent(object data, string type = "message")
        {
            string str = "event: " + type + "\n" + "data: " + ConvertToJson(data) + "\n\n";
            ResponseWrite(str).Wait();
            // Flush the output buffer and send echoed messages to the browser
            Response?.Body.Flush();
        }

        /// <summary>
        /// Import file
        /// </summary>
        /// <param name="token">File token to locate the uploaded import file</param>
        /// <param name="rollback">Try import and then rollback</param>
        /// <returns>Action result</returns>
        public async Task<JsonBoolResult> Import(string token, bool rollback = false)
        {
            if (!Security.CanImport)
                return JsonBoolResult.FalseResult; // Import not allowed

            // Check if valid token
            if (Empty(token))
                return JsonBoolResult.FalseResult;

            // Get uploaded files by token
            var files = GetUploadedFileNames(token, true).Where(file => Path.GetExtension(file).ToLower() != ".txt"); // Ignore log file
            var exts = Config.ImportFileAllowedExtensions.Split(',');
            int totCnt = 0, totSuccessCnt = 0, totFailCnt = 0;
            object? ov;
            bool firstRowIsHeader = false;
            var list = new List<Dictionary<string, object>>();
            var results = new Dictionary<string, object> { { Config.ApiFileTokenName, token }, { "files", list }, { "success", false } };

            // Add HTTP headers for SSE
            AddHeader(HeaderNames.CacheControl, "no-store");
            AddHeader(HeaderNames.ContentType, "text/event-stream");

            // Import records
            foreach (var file in files) {
                string fileName = Path.GetFileName(file);
                Dictionary<string, object> res = new () { { Config.ApiFileTokenName, token }, { "file", fileName }, { "success", false } };
                string ext = Path.GetExtension(file)?.ToLower().Substring(1) ?? "";
                if (!exts.Contains(ext)) {
                    res.Add("error", Language.Phrase("ImportInvalidFileExtension").Replace("%e", ext));
                    SendEvent(res, "error");
                    return new JsonBoolResult(res, false);
                }

                // Set up options for Page Importing event

                // Default Options
                var options = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase) {
                    { "file", file },
                    { "activeSheet", 0 },
                    { "headerRowNumber", 0 },
                    { "headers", new List<string?>() },
                    { "offset", 0 },
                    { "limit", 0 }
                };

                // Get optional data from POST
                foreach (string key in Form.Keys) {
                    if (!(key == "action" || key == "token" || key == "filetoken"))
                        options.Add(key, Form[key]);
                }

                // Get data
                byte[] tempData;
                if (ext == "csv") {
                    options.Add("encoding", Config.ImportCsvEncoding);
                    options.Add("culture", Config.ImportCsvCulture);
                    options.Add("delimiter", Config.ImportCsvDelimiter);
                    options.Add("textQualifier", Config.ImportCsvTextQualifier);
                    options.Add("eol", Config.ImportCsvEol);
                    options.Add("dataTypes", new eDataTypes[] {});
                    options.Add("skipLinesBeginning", 0);
                    options.Add("skipLinesEnd", 0);
                }

                // Create a new Excel package
                using var excelPackage = new ExcelPackage();

                // Call Page Importing server event
                if (!PageImporting(excelPackage, options)) {
                    SendEvent(res, "error");
                    return new JsonBoolResult(res, false);
                }
                if (ext == "csv") { // CSV file
                    ExcelTextFormat format = new ();
                    if (options.TryGetValue("culture", out ov) && ov is CultureInfo)
                        format.Culture = (CultureInfo)ov;
                    if (options.TryGetValue("delimiter", out ov))
                        format.Delimiter = Convert.ToChar(ov);
                    if (options.TryGetValue("textQualifier", out ov))
                        format.TextQualifier = Convert.ToChar(ov);
                    if (options.TryGetValue("dataTypes", out ov))
                        format.DataTypes = (eDataTypes[])ov;
                    if (options.TryGetValue("eol", out ov))
                        format.EOL = ConvertToString(ov);
                    if (options.TryGetValue("skipLinesBeginning", out ov))
                        format.SkipLinesBeginning = ConvertToInt(ov);
                    if (options.TryGetValue("skipLinesEnd", out ov))
                        format.SkipLinesEnd = ConvertToInt(ov);

                    // Read file
                    string csvText;
                    if (options.TryGetValue("encoding", out ov) && ov is Encoding)
                        csvText = await FileReadAllTextWithEncoding(file, (Encoding)ov);
                    else
                        csvText = await FileReadAllText(file);

                    // Append EOL if the file has no EOL at end of file
                    if (!csvText.EndsWith(format.EOL))
                        csvText += format.EOL;

                    // Create a worksheet
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                    // Load the CSV data into cell A1
                    worksheet.Cells["A1"].LoadFromText(csvText, format);
                    tempData = excelPackage.GetAsByteArray();
                } else { // Excel file
                    tempData = await FileReadAllBytes(file);
                }
                int activeSheet = -1, offset = 0, limit = 0;
                if (options.TryGetValue("offset", out ov))
                    offset = ConvertToInt(ov);
                if (options.TryGetValue("limit", out ov))
                    limit = ConvertToInt(ov);
                // Get active worksheet for Excel
                if (ext == "xlsx" && options.TryGetValue("activeSheet", out ov))
                    activeSheet = ConvertToInt(ov);

                // Create a new Excel package in a memorystream
                using var stream = new MemoryStream(tempData);
                using var ep = new ExcelPackage(stream);
                ExcelWorksheet ws = (activeSheet > -1) ? ep.Workbook.Worksheets[activeSheet] : ep.Workbook.Worksheets.First();
                if (ws == null) {
                    var result = new { success = false, error = Language.Phrase("WorksheetNotExist") };
                    SendEvent(res, "error");
                    return new JsonBoolResult(result, false);
                }

                // Get column headers
                List<string> headers = new ();
                if (options.TryGetValue("headers", out ov) && ov is List<string>)
                    headers = (List<string>)ov;
                int headerRow = 0;
                int highestColumn = ws.Dimension.End.Column;
                int highestRow = ws.Dimension.End.Row;
                if (!IsList(headers) || headers.Count == 0) { // Undetermined, load from header row
                    headerRow = ConvertToInt(options["headerRowNumber"]) + 1;
                    headers = GetImportHeaders(ws, headerRow, highestColumn);
                }
                if (!IsList(headers) || headers.Count == 0) { // Unable to load header
                    res.Add("error", Language.Phrase("ImportNoHeaderRow"));
                    SendEvent(res, "error");
                    return new JsonBoolResult(res, false);
                }
                if (headers.Any(name => !Fields.ContainsKey(name))) {
                    res.Add("error", Language.Phrase("ImportInvalidFieldName").Replace("%f", String.Join(", ", headers.Where(name => !Fields.ContainsKey(name)))));
                    SendEvent(res, "error");
                    return new JsonBoolResult(res, false);
                }
                int startRow = headerRow + 1;
                int endRow = highestRow;
                if (offset > 0)
                    startRow += offset;
                if (limit > 0) {
                    endRow = startRow + limit - 1;
                    if (endRow > highestRow)
                        endRow = highestRow;
                }
                var records = new List<List<object>>();
                if (endRow >= startRow)
                    records = GetImportRecords(ws, startRow, endRow, highestColumn);
                int recordCnt = records.Count;
                int cnt = 0, successCnt = 0, failCnt = 0;
                var failList = new Dictionary<string, string>();
                res.Add("totalCount", recordCnt);
                res.Add("count", cnt);
                res.Add("successCount", successCnt);
                res.Add("failCount", 0);

                // Begin transaction
                if (ImportUseTransaction)
                    Connection.BeginTrans();

                // Process records
                foreach (var values in records) {
                    bool importSuccess = false;
                    try {
                        var row = new Dictionary<string, object>();
                        for (int k = 0; k < highestColumn && k < headers.Count; k++) {
                            if (!Empty(headers[k]) && !Empty(values[k])) { // Skip empty values // DN
                                var fld = FieldByName(headers[k]);
                                if (fld?.IsBoolean ?? false) // Fix boolean field // DN
                                    values[k] = Connection.IsMsSql ? ConvertToBool(values[k]) : ConvertToString(values[k]);
                                row.Add(headers[k], values[k]);
                            }
                        }
                        cnt++;
                        res["count"] = cnt;
                        res.Add("row", row);
                        if (await ImportRow(row, cnt)) {
                            successCnt++;
                            importSuccess = true;
                            res["success"] = true;
                            res.Add("error", false);
                        } else {
                            failCnt++;
                            failList.Add("row" + cnt, FailureMessage);
                            ClearFailureMessage(); // Clear error message
                            res["success"] = false;
                            res.Add("error", FailureMessage);
                        }
                    } catch (Exception e) {
                        failCnt++;
                        if (!failList.TryGetValue("row" + cnt, out string? fm) || fm == "")
                            failList["row" + cnt] = e.Message;
                        res.Add("error", e.Message);
                        res["success"] = false;
                    }

                    // Reset count if import fail and use transaction
                    if (!importSuccess && ImportUseTransaction) {
                        successCnt = 0;
                        failCnt = cnt;
                    }

                    // Save progress to memory cache // DN
                    res["successCount"] = successCnt;
                    res["failCount"] = failCnt;
                    SendEvent(res);
                    res.Remove("row");
                    res.Remove("error");

                    // No need to process further if import fail and use transaction
                    if (!importSuccess && ImportUseTransaction)
                        break;
                }
                res.Add("failList", failList);

                // Commit/Rollback transaction
                if (ImportUseTransaction) {
                    if (failCnt > 0) // Rollback
                        Connection.RollbackTrans();
                    else // Commit
                        Connection.CommitTrans();
                }
                totCnt += cnt;
                totSuccessCnt += successCnt;
                totFailCnt += failCnt;

                // Call Page Imported server event
                PageImported(ep, res);
                if (totCnt > 0 && totFailCnt == 0) { // Clean up if all records imported
                    results["success"] = true;
                } else {
                    results["success"] = false;
                }
                list.Add(res);
            }
            var ret = (bool)results["success"];
            if (ret)
                CleanUploadTempPaths(token);
            SendEvent(results, "complete"); // All files imported
            return new JsonBoolResult(results, ret);
        }

        /// <summary>
        /// Get import header
        /// </summary>
        /// <param name="ws">EPPlus worksheet</param>
        /// <param name="rowIdx">Row index for header row</param>
        /// <param name="highestColumn">Highest number of column</param>
        /// <returns>The column headers</returns>
        protected List<string> GetImportHeaders(ExcelWorksheet ws, int rowIdx, int highestColumn) =>
            ws.Cells[rowIdx, 1, rowIdx, highestColumn].Select(cell => cell.Value.ToString()).Select(v => ConvertToString(v)).ToList();

        /// <summary>
        /// Get import records
        /// </summary>
        /// <param name="ws">EPPlus worksheet</param>
        /// <param name="startRowIdx">Start row index</param>
        /// <param name="endRowIdx">End row index</param>
        /// <param name="highestColumn">Highest number of Column</param>
        /// <returns>The records to import</returns>
        protected List<List<object>> GetImportRecords(ExcelWorksheet ws, int startRowIdx, int endRowIdx, int highestColumn) {
            object[,] values = (object[,])ws.Cells[startRowIdx, 1, endRowIdx, highestColumn].Value;
            int bound0 = values.GetUpperBound(0), bound1 = values.GetUpperBound(1);
            var ar = new List<List<object>>();
            List<object> record;
            for (int i = 0; i <= bound0; i++) {
                record = new List<object>();
                for (int j = 0; j <= bound1; j++)
                    record.Add(values[i, j]);
                ar.Add(record);
            }
            return ar;
        }

        /// <summary>
        /// Import a row
        /// </summary>
        /// <param name="row">Record to import</param>
        /// <param name="cnt">Imported record count</param>
        /// <returns>Whether the record is imported</returns>
        protected async Task<bool> ImportRow(Dictionary<string, object> row, int cnt) {
            // Call Row Import server event
            if (!RowImport(row, cnt))
                return false;

            // Check field values
            foreach (var (key, value) in row) {
                if (Fields[key] is DbField<SqlDbType> fld && !CheckValue(fld, value)) {
                    FailureMessage = Language.Phrase("ImportInvalidFieldValue").Replace("%f", fld.Name).Replace("%v", ConvertToString(value));
                    return false;
                }
            }

            // Insert/Update to database
            var oldrow = await LoadRow(row);
            if (!ImportInsertOnly && !Empty(oldrow)) {
                // Call Row Updating event
                bool updateRow = RowUpdating(oldrow, row);
                if (updateRow) {
                    updateRow = ConvertToBool(await UpdateAsync(row, null, oldrow));
                    if (updateRow) // Call Row Updated event
                        RowUpdated(oldrow, row);
                }
                return updateRow;
            } else {
                // Call Row Inserting event
                bool insertRow = RowInserting(oldrow, row);
                if (insertRow) {
                    insertRow = ConvertToBool(await InsertAsync(row));
                    if (insertRow)
                        RowInserted(oldrow, row); // Call Row Updated event
                }
                return insertRow;
            }
        }

        /// <summary>
        /// Check field value
        /// </summary>
        /// <param name="fld">Field object</param>
        /// <param name="value">Field value to check</param>
        /// <returns>Whether the field values is valid</returns>
        protected bool CheckValue(DbField fld, object value) {
            if (fld.DataType == DataType.Number && !IsNumeric(value))
                return false;
            else if (fld.DataType == DataType.Date && !CheckDate(ConvertToString(value)))
                return false;
            return true;
        }

        // Load row (for import)
        protected async Task<Dictionary<string, object>?> LoadRow(Dictionary<string, object> row) {
            string filter = GetRecordFilter(row);
            if (Empty(filter)) // No primary key
                return null;
            CurrentFilter = filter;
            string sql = CurrentSql;
            try {
                return await Connection.GetRowAsync(sql, true); // Use main connection
            } catch {
                return null;
            }
        }

        // Load advanced search
        public void LoadAdvancedSearch()
        {
            id.AdvancedSearch.Load();
            Request_No.AdvancedSearch.Load();
            Reference_Doc.AdvancedSearch.Load();
            Reason.AdvancedSearch.Load();
            Request_By.AdvancedSearch.Load();
            Request_By_Name.AdvancedSearch.Load();
            Request_By_Position.AdvancedSearch.Load();
            Request_By_Branch.AdvancedSearch.Load();
            Request_By_Region.AdvancedSearch.Load();
            Request_Industry.AdvancedSearch.Load();
            Customer_ID.AdvancedSearch.Load();
            Customer_Name.AdvancedSearch.Load();
            SAP_Total.AdvancedSearch.Load();
            Override_Total.AdvancedSearch.Load();
            Variance_Total.AdvancedSearch.Load();
            Variance_Percent.AdvancedSearch.Load();
            Notes.AdvancedSearch.Load();
            Next_Approver.AdvancedSearch.Load();
            List_Approver.AdvancedSearch.Load();
            Last_Update_By.AdvancedSearch.Load();
            Created_By.AdvancedSearch.Load();
            Created_Date.AdvancedSearch.Load();
            ETL_Date.AdvancedSearch.Load();
            Request_Status.AdvancedSearch.Load();
            Request_PIC.AdvancedSearch.Load();
        }

        // Get export HTML tag
        protected string GetExportTag(string type, bool custom = false) {
            string exportUrl = AppPath(CurrentPageName()); // DN
            if (type == "print" || custom) { // Printer friendly / custom export
                exportUrl += "?export=" + type + (custom ? "&amp;custom=1" : "");
            } else {
                exportUrl = AppPath(Config.ApiUrl + Config.ApiExportAction + "/" + type + "/" + TableVar);
            }
            if (SameText(type, "excel")) {
                if (custom)
                    return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-excel\" title=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\" form=\"fv_tr_requestlist\" data-url=\"" + exportUrl + "\" data-ew-action=\"export\" data-export=\"excel\" data-custom=\"true\" data-export-selected=\"false\">" + Language.Phrase("ExportToExcel") + "</button>";
                else
                    return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-excel\" title=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\">" + Language.Phrase("ExportToExcel") + "</a>";
            } else if (SameText(type, "word")) {
                if (custom)
                    return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-word\" title=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\" form=\"fv_tr_requestlist\" data-url=\"" + exportUrl + "\" data-ew-action=\"export\" data-export=\"word\" data-custom=\"true\" data-export-selected=\"false\">" + Language.Phrase("ExportToWord") + "</button>";
                else
                    return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-word\" title=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\">" + Language.Phrase("ExportToWord") + "</a>";
            } else if (SameText(type, "pdf")) {
                if (custom)
                    return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-pdf\" title=\"" + HtmlEncode(Language.Phrase("ExportToPdf", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToPdf", true)) + "\" form=\"fv_tr_requestlist\" data-url=\"" + exportUrl + "\" data-ew-action=\"export\" data-export=\"pdf\" data-custom=\"true\" data-export-selected=\"false\">" + Language.Phrase("ExportToPDF") + "</button>";
                else
                    return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-pdf\" title=\"" + HtmlEncode(Language.Phrase("ExportToPdf", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToPdf", true)) + "\">" + Language.Phrase("ExportToPDF") + "</a>";
            } else if (SameText(type, "html")) {
                return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-html\" title=\"" + HtmlEncode(Language.Phrase("ExportToHtml", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToHtml", true)) + "\">" + Language.Phrase("ExportToHtml") + "</a>";
            } else if (SameText(type, "xml")) {
                return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-xml\" title=\"" + HtmlEncode(Language.Phrase("ExportToXml", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToXml", true)) + "\">" + Language.Phrase("ExportToXml") + "</a>";
            } else if (SameText(type, "csv")) {
                return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-csv\" title=\"" + HtmlEncode(Language.Phrase("ExportToCsv", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToCsv", true)) + "\">" + Language.Phrase("ExportToCsv") + "</a>";
            } else if (SameText(type, "email")) {
                string url = custom ? " data-url=\"" + exportUrl + "\"" : "";
                return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-email\" title=\"" + Language.Phrase("ExportToEmail", true) + "\" data-caption=\"" + Language.Phrase("ExportToEmail", true) + "\" form=\"fv_tr_requestlist\" data-ew-action=\"email\" data-custom=\"false\" data-hdr=\"" + Language.Phrase("ExportToEmail", true) + "\" data-export-selected=\"false\"" + url + ">" + Language.Phrase("ExportToEmail") + "</button>";
            } else if (SameText(type, "print")) {
                return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-print\" title=\"" + HtmlEncode(Language.Phrase("PrinterFriendly", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("PrinterFriendly", true)) + "\">" + Language.Phrase("PrinterFriendly") + "</a>";
            }
            return "";
        }

        // Set up export options
        protected void SetupExportOptions() {
            ListOption item;

            // Printer friendly
            item = ExportOptions.Add("print");
            item.Body = GetExportTag("print");
            item.Visible = false;

            // Export to Excel
            item = ExportOptions.Add("excel");
            item.Body = GetExportTag("excel");
            item.Visible = true;

            // Export to Word
            item = ExportOptions.Add("word");
            item.Body = GetExportTag("word");
            item.Visible = false;

            // Export to HTML
            item = ExportOptions.Add("html");
            item.Body = GetExportTag("html");
            item.Visible = false;

            // Export to XML
            item = ExportOptions.Add("xml");
            item.Body = GetExportTag("xml");
            item.Visible = false;

            // Export to CSV
            item = ExportOptions.Add("csv");
            item.Body = GetExportTag("csv");
            item.Visible = true;

            // Export to PDF
            item = ExportOptions.Add("pdf");
            item.Body = GetExportTag("pdf");
            item.Visible = false;

            // Export to Email
            item = ExportOptions.Add("email");
            item.Body = GetExportTag("email");
            item.Visible = false;

            // Drop down button for export
            ExportOptions.UseButtonGroup = true;
            ExportOptions.UseDropDownButton = true;
            if (ExportOptions.UseButtonGroup && IsMobile())
                ExportOptions.UseDropDownButton = true;
            ExportOptions.DropDownButtonPhrase = "ButtonExport";

            // Add group option item
            item = ExportOptions.AddGroupOption();
            item.Body = "";
            item.Visible = false;
            if (!Security.CanExport) // Export not allowed
                ExportOptions.HideAllOptions();
        }

        // Set up search options
        protected void SetupSearchOptions() {
            ListOption item;

            // Search button
            item = SearchOptions.Add("searchtoggle");
            var searchToggleClass = !Empty(SearchWhere) ? " active" : " active";
            item.Body = "<a class=\"btn btn-default ew-search-toggle" + searchToggleClass + "\" role=\"button\" title=\"" + Language.Phrase("SearchPanel") + "\" data-caption=\"" + Language.Phrase("SearchPanel") + "\" data-ew-action=\"search-toggle\" data-form=\"fv_tr_requestsrch\" aria-pressed=\"" + (searchToggleClass == " active" ? "true" : "false") + "\">" + Language.Phrase("SearchLink") + "</a>";
            item.Visible = true;

            // Show all button
            item = SearchOptions.Add("showall");
            if (UseCustomTemplate || !UseAjaxActions)
                item.Body = "<a class=\"btn btn-default ew-show-all\" role=\"button\" title=\"" + Language.Phrase("ShowAll") + "\" data-caption=\"" + Language.Phrase("ShowAll") + "\" href=\"" + AppPath(PageUrl) + "cmd=reset\">" + Language.Phrase("ShowAllBtn") + "</a>";
            else
                item.Body = "<a class=\"btn btn-default ew-show-all\" role=\"button\" title=\"" + Language.Phrase("ShowAll") + "\" data-caption=\"" + Language.Phrase("ShowAll") + "\" data-ew-action=\"refresh\" data-url=\"" + AppPath(PageUrl) + "cmd=reset\">" + Language.Phrase("ShowAllBtn") + "</a>";
            item.Visible = (SearchWhere != DefaultSearchWhere && SearchWhere != "0=101");

            // Advanced search button
            item = SearchOptions.Add("advancedsearch");
            if (ModalSearch && !IsMobile())
                item.Body = "<a class=\"btn btn-default ew-advanced-search\" title=\"" + Language.Phrase("AdvancedSearch", true) + "\" data-table=\"v_tr_request\" data-caption=\"" + Language.Phrase("AdvancedSearch", true) + "\" data-ew-action=\"modal\" data-url=\"" + AppPath("VTrRequestSearch") + "\" data-btn=\"SearchBtn\">" + Language.Phrase("AdvancedSearch", false) + "</a>";
            else
                item.Body = "<a class=\"btn btn-default ew-advanced-search\" title=\"" + Language.Phrase("AdvancedSearch", true) + "\" data-caption=\"" + Language.Phrase("AdvancedSearch", true) + "\" href=\"" + AppPath("VTrRequestSearch") + "\">" + Language.Phrase("AdvancedSearch", false) + "</a>";
            item.Visible = true;

            // Query builder button
            item = SearchOptions.Add("querybuilder");
            if (ModalSearch && !IsMobile())
                item.Body = "<a class=\"btn btn-default ew-query-builder\" title=\"" + Language.Phrase("QueryBuilder", true) + "\" data-table=\"v_tr_request\" data-caption=\"" + Language.Phrase("QueryBuilder", true) + "\" data-ew-action=\"modal\" data-url=\"" + AppPath("VTrRequestQuery") + "\" data-btn=\"SearchBtn\">" + Language.Phrase("QueryBuilder", false) + "</a>";
            else
                item.Body = "<a class=\"btn btn-default ew-query-builder\" title=\"" + Language.Phrase("QueryBuilder", true) + "\" data-caption=\"" + Language.Phrase("QueryBuilder", true) + "\" href=\"" + AppPath("VTrRequestQuery") + "\">" + Language.Phrase("QueryBuilder", false) + "</a>";
            item.Visible = true;

            // Search highlight button
            item = SearchOptions.Add("searchhighlight");
            item.Body = "<a class=\"btn btn-default ew-highlight active\" role=\"button\" title=\"" + Language.Phrase("Highlight") + "\" data-caption=\"" + Language.Phrase("Highlight") + "\" data-ew-action=\"highlight\" data-form=\"fv_tr_requestsrch\" data-name=\"" + HighlightName + "\">" + Language.Phrase("HighlightBtn") + "</a>";
            item.Visible = (!Empty(SearchWhere) && TotalRecords > 0);

            // Button group for search
            SearchOptions.UseDropDownButton = false;
            SearchOptions.UseButtonGroup = true;
            SearchOptions.DropDownButtonPhrase = "ButtonSearch";

            // Add group option item
            item = SearchOptions.AddGroupOption();
            item.Body = "";
            item.Visible = false;

            // Hide search options
            if (IsExport() || !Empty(CurrentAction) && CurrentAction != "search")
                SearchOptions.HideAllOptions();
            if (!Security.CanSearch) {
                SearchOptions.HideAllOptions();
                FilterOptions.HideAllOptions();
            }
        }

        // Check if any search fields
        public bool HasSearchFields()
        {
            return true;
        }

        // Render search options
        protected void RenderSearchOptions()
        {
            if (!HasSearchFields() && SearchOptions["searchtoggle"] is ListOption opt)
                opt.Visible = false;
        }

        // Set up import options
        protected void SetupImportOptions() {
            ListOption item;

            // Import
            item = ImportOptions.Add("import");
            item.Body = "<a class=\"ew-import-link ew-import\" role=\"button\" title=\"" + Language.Phrase("Import", true) + "\" data-caption=\"" + Language.Phrase("Import", true) + "\" data-ew-action=\"import\" data-hdr=\"" + Language.Phrase("Import", true) + "\">" + Language.Phrase("Import") + "</a>";
            item.Visible = Security.CanImport;
            ImportOptions.UseButtonGroup = true;
            ImportOptions.UseDropDownButton = false;
            ImportOptions.DropDownButtonPhrase = "ButtonImport";

            // Add group option item
            item = ImportOptions.AddGroupOption();
            item.Body = "";
            item.Visible = false;
        }

        #pragma warning disable 168

        /// <summary>
        /// Export data
        /// </summary>
        public async Task ExportData(dynamic? doc)
        {
            // Load recordset // DN
            DbDataReader? dr = null;
            if (TotalRecords < 0)
                TotalRecords = await ListRecordCountAsync();
            StartRecord = 1;

            // Export all
            if (ExportAll) {
                DisplayRecords = TotalRecords;
                StopRecord = TotalRecords;
            } else { // Export one page only
                SetupStartRecord(); // Set up start record position
                // Set the last record to display
                if (DisplayRecords < 0) {
                    StopRecord = TotalRecords;
                } else {
                    StopRecord = StartRecord + DisplayRecords - 1;
                }
            }
            CloseRecordset(); // DN
            dr = await LoadRecordset(StartRecord - 1, (DisplayRecords <= 0) ? TotalRecords : DisplayRecords); // DN
            if (doc == null) { // DN
                RemoveHeader("Content-Type"); // Remove header
                RemoveHeader("Content-Disposition");
                FailureMessage = Language.Phrase("ExportClassNotFound"); // Export class not found
                return;
            }

            // Call Page Exporting server event
            doc.ExportCustom = !PageExporting(ref doc);

            // Page header
            string header = PageHeader;
            PageDataRendering(ref header);
            doc.Text.Append(header);

            // Export
            if (dr != null)
                await ExportDocument(doc, dr, StartRecord, StopRecord, "");

            // Page footer
            string footer = PageFooter;
            PageDataRendered(ref footer);
            doc.Text.Append(footer);

            // Close recordset
            using (dr) {} // Dispose

            // Export header and footer
            await doc.ExportHeaderAndFooter();

            // Call Page Exported server event
            PageExported(doc);
        }
        #pragma warning restore 168

        // Set up Breadcrumb
        protected void SetupBreadcrumb() {
            var breadcrumb = new Breadcrumb();
            string url = CurrentUrl();
            url = Regex.Replace(url, @"\?cmd=reset(all)?$", ""); // Remove cmd=reset / cmd=resetall
            breadcrumb.Add("list", TableVar, url, "", TableVar, true);
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
            infiniteScroll = Param<bool>("infinitescroll");
            if (!Empty(pageNo) && IsNumeric(pageNo)) {
                int page = ConvertToInt(pageNo);
                StartRecord = (page - 1) * DisplayRecords + 1;
                if (StartRecord <= 0)
                    StartRecord = 1;
                else if (StartRecord >= ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1)
                    StartRecord = ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1;
            } else if (!Empty(startRec) && IsNumeric(startRec)) {
                StartRecord = ConvertToInt(startRec);
            } else if (!infiniteScroll) {
                StartRecord = StartRecordNumber;
            }

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

        // Page Exporting event
        // doc = export document object
        public virtual bool PageExporting(ref dynamic doc) {
            //doc.Text.Append("<p>" + "my header" + "</p>"); // Export header
            //return false; // Return false to skip default export and use Row_Export event
            return true; // Return true to use default export and skip Row_Export event
        }

        // Page Exported event
        // doc = export document object
        public virtual void PageExported(dynamic doc) {
            //doc.Text.Append("my footer"); // Export footer
            //Log("Text: {Text}", doc.Text.ToString());
        }

        public virtual bool PageImporting(ExcelPackage excelPackage, dynamic options) {
            //Log(excelPackage); // Import excelPackage
            //Log(options); // Show all options for importing
            //return false; // Return false to skip import
            return true;
        }

        // Row Import event
        public virtual bool RowImport(Dictionary<string, object> row, int cnt) {
            //Log(cnt); // Import record count
            //Log(row); // Import row
            //return false; // Return false to skip import
            return true;
        }

        // Page Imported event
        public virtual void PageImported(ExcelPackage excelPackage, Dictionary<string, object> result) {
            //Log(result); // Import result
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
