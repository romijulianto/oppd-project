namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// trRequestDetailList
    /// </summary>
    public static TrRequestDetailList trRequestDetailList
    {
        get => HttpData.Get<TrRequestDetailList>("trRequestDetailList")!;
        set => HttpData["trRequestDetailList"] = value;
    }

    /// <summary>
    /// Page class for tr_request_detail
    /// </summary>
    public class TrRequestDetailList : TrRequestDetailListBase
    {
        // Constructor
        public TrRequestDetailList(Controller controller) : base(controller)
        {
        }

        // Constructor
        public TrRequestDetailList() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TrRequestDetailListBase : TrRequestDetail
    {
        // Page ID
        public string PageID = "list";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "tr_request_detail";

        // Page object name
        public string PageObjName = "trRequestDetailList";

        // Title
        public string? Title = null; // Title for <title> tag

        // Grid form hidden field names
        public string FormName = "ftr_request_detaillist";

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
        public TrRequestDetailListBase()
        {
            // CSS class name as context
            TableVar = "tr_request_detail";
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

            // Table object (trRequestDetail)
            if (trRequestDetail == null || trRequestDetail is TrRequestDetail)
                trRequestDetail = this;

            // Initialize URLs
            AddUrl = "TrRequestDetailAdd";
            InlineAddUrl = PageUrl + "action=add";
            GridAddUrl = PageUrl + "action=gridadd";
            GridEditUrl = PageUrl + "action=gridedit";
            MultiEditUrl = PageUrl + "action=multiedit";
            MultiDeleteUrl = "TrRequestDetailDelete";
            MultiUpdateUrl = "TrRequestDetailUpdate";

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
        public string PageName => "TrRequestDetailList";

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
        public TrRequestDetailListBase(Controller? controller = null): this() { // DN
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

            // Set up custom action (compatible with old version)
            ListActions.Add(CustomActions);

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
                trRequestDetailList?.PageRender();
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
            filters.Merge(JObject.Parse(request_id.AdvancedSearch.ToJson())); // Field request_id
            filters.Merge(JObject.Parse(Item_No.AdvancedSearch.ToJson())); // Field Item_No
            filters.Merge(JObject.Parse(Part_No.AdvancedSearch.ToJson())); // Field Part_No
            filters.Merge(JObject.Parse(Part_Description.AdvancedSearch.ToJson())); // Field Part_Description
            filters.Merge(JObject.Parse(Qty.AdvancedSearch.ToJson())); // Field Qty
            filters.Merge(JObject.Parse(SAP_Unit_Price.AdvancedSearch.ToJson())); // Field SAP_Unit_Price
            filters.Merge(JObject.Parse(Extd_SAP_Price.AdvancedSearch.ToJson())); // Field Extd_SAP_Price
            filters.Merge(JObject.Parse(GP_SAP_Price.AdvancedSearch.ToJson())); // Field GP_SAP_Price
            filters.Merge(JObject.Parse(Override_Unit_Price.AdvancedSearch.ToJson())); // Field Override_Unit_Price
            filters.Merge(JObject.Parse(Extd_Override_Price.AdvancedSearch.ToJson())); // Field Extd_Override_Price
            filters.Merge(JObject.Parse(GP_Override_Price.AdvancedSearch.ToJson())); // Field GP_Override_Price
            filters.Merge(JObject.Parse(Override_Core.AdvancedSearch.ToJson())); // Field Override_Core
            filters.Merge(JObject.Parse(Override_Percent.AdvancedSearch.ToJson())); // Field Override_Percent
            filters.Merge(JObject.Parse(Core_Life_Ind.AdvancedSearch.ToJson())); // Field Core_Life_Ind
            filters.Merge(JObject.Parse(Notes.AdvancedSearch.ToJson())); // Field Notes
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

            // Field request_id
            if (filter?.TryGetValue("x_request_id", out sv) ?? false) {
                request_id.AdvancedSearch.SearchValue = sv;
                request_id.AdvancedSearch.SearchOperator = filter["z_request_id"];
                request_id.AdvancedSearch.SearchCondition = filter["v_request_id"];
                request_id.AdvancedSearch.SearchValue2 = filter["y_request_id"];
                request_id.AdvancedSearch.SearchOperator2 = filter["w_request_id"];
                request_id.AdvancedSearch.Save();
            }

            // Field Item_No
            if (filter?.TryGetValue("x_Item_No", out sv) ?? false) {
                Item_No.AdvancedSearch.SearchValue = sv;
                Item_No.AdvancedSearch.SearchOperator = filter["z_Item_No"];
                Item_No.AdvancedSearch.SearchCondition = filter["v_Item_No"];
                Item_No.AdvancedSearch.SearchValue2 = filter["y_Item_No"];
                Item_No.AdvancedSearch.SearchOperator2 = filter["w_Item_No"];
                Item_No.AdvancedSearch.Save();
            }

            // Field Part_No
            if (filter?.TryGetValue("x_Part_No", out sv) ?? false) {
                Part_No.AdvancedSearch.SearchValue = sv;
                Part_No.AdvancedSearch.SearchOperator = filter["z_Part_No"];
                Part_No.AdvancedSearch.SearchCondition = filter["v_Part_No"];
                Part_No.AdvancedSearch.SearchValue2 = filter["y_Part_No"];
                Part_No.AdvancedSearch.SearchOperator2 = filter["w_Part_No"];
                Part_No.AdvancedSearch.Save();
            }

            // Field Part_Description
            if (filter?.TryGetValue("x_Part_Description", out sv) ?? false) {
                Part_Description.AdvancedSearch.SearchValue = sv;
                Part_Description.AdvancedSearch.SearchOperator = filter["z_Part_Description"];
                Part_Description.AdvancedSearch.SearchCondition = filter["v_Part_Description"];
                Part_Description.AdvancedSearch.SearchValue2 = filter["y_Part_Description"];
                Part_Description.AdvancedSearch.SearchOperator2 = filter["w_Part_Description"];
                Part_Description.AdvancedSearch.Save();
            }

            // Field Qty
            if (filter?.TryGetValue("x_Qty", out sv) ?? false) {
                Qty.AdvancedSearch.SearchValue = sv;
                Qty.AdvancedSearch.SearchOperator = filter["z_Qty"];
                Qty.AdvancedSearch.SearchCondition = filter["v_Qty"];
                Qty.AdvancedSearch.SearchValue2 = filter["y_Qty"];
                Qty.AdvancedSearch.SearchOperator2 = filter["w_Qty"];
                Qty.AdvancedSearch.Save();
            }

            // Field SAP_Unit_Price
            if (filter?.TryGetValue("x_SAP_Unit_Price", out sv) ?? false) {
                SAP_Unit_Price.AdvancedSearch.SearchValue = sv;
                SAP_Unit_Price.AdvancedSearch.SearchOperator = filter["z_SAP_Unit_Price"];
                SAP_Unit_Price.AdvancedSearch.SearchCondition = filter["v_SAP_Unit_Price"];
                SAP_Unit_Price.AdvancedSearch.SearchValue2 = filter["y_SAP_Unit_Price"];
                SAP_Unit_Price.AdvancedSearch.SearchOperator2 = filter["w_SAP_Unit_Price"];
                SAP_Unit_Price.AdvancedSearch.Save();
            }

            // Field Extd_SAP_Price
            if (filter?.TryGetValue("x_Extd_SAP_Price", out sv) ?? false) {
                Extd_SAP_Price.AdvancedSearch.SearchValue = sv;
                Extd_SAP_Price.AdvancedSearch.SearchOperator = filter["z_Extd_SAP_Price"];
                Extd_SAP_Price.AdvancedSearch.SearchCondition = filter["v_Extd_SAP_Price"];
                Extd_SAP_Price.AdvancedSearch.SearchValue2 = filter["y_Extd_SAP_Price"];
                Extd_SAP_Price.AdvancedSearch.SearchOperator2 = filter["w_Extd_SAP_Price"];
                Extd_SAP_Price.AdvancedSearch.Save();
            }

            // Field GP_SAP_Price
            if (filter?.TryGetValue("x_GP_SAP_Price", out sv) ?? false) {
                GP_SAP_Price.AdvancedSearch.SearchValue = sv;
                GP_SAP_Price.AdvancedSearch.SearchOperator = filter["z_GP_SAP_Price"];
                GP_SAP_Price.AdvancedSearch.SearchCondition = filter["v_GP_SAP_Price"];
                GP_SAP_Price.AdvancedSearch.SearchValue2 = filter["y_GP_SAP_Price"];
                GP_SAP_Price.AdvancedSearch.SearchOperator2 = filter["w_GP_SAP_Price"];
                GP_SAP_Price.AdvancedSearch.Save();
            }

            // Field Override_Unit_Price
            if (filter?.TryGetValue("x_Override_Unit_Price", out sv) ?? false) {
                Override_Unit_Price.AdvancedSearch.SearchValue = sv;
                Override_Unit_Price.AdvancedSearch.SearchOperator = filter["z_Override_Unit_Price"];
                Override_Unit_Price.AdvancedSearch.SearchCondition = filter["v_Override_Unit_Price"];
                Override_Unit_Price.AdvancedSearch.SearchValue2 = filter["y_Override_Unit_Price"];
                Override_Unit_Price.AdvancedSearch.SearchOperator2 = filter["w_Override_Unit_Price"];
                Override_Unit_Price.AdvancedSearch.Save();
            }

            // Field Extd_Override_Price
            if (filter?.TryGetValue("x_Extd_Override_Price", out sv) ?? false) {
                Extd_Override_Price.AdvancedSearch.SearchValue = sv;
                Extd_Override_Price.AdvancedSearch.SearchOperator = filter["z_Extd_Override_Price"];
                Extd_Override_Price.AdvancedSearch.SearchCondition = filter["v_Extd_Override_Price"];
                Extd_Override_Price.AdvancedSearch.SearchValue2 = filter["y_Extd_Override_Price"];
                Extd_Override_Price.AdvancedSearch.SearchOperator2 = filter["w_Extd_Override_Price"];
                Extd_Override_Price.AdvancedSearch.Save();
            }

            // Field GP_Override_Price
            if (filter?.TryGetValue("x_GP_Override_Price", out sv) ?? false) {
                GP_Override_Price.AdvancedSearch.SearchValue = sv;
                GP_Override_Price.AdvancedSearch.SearchOperator = filter["z_GP_Override_Price"];
                GP_Override_Price.AdvancedSearch.SearchCondition = filter["v_GP_Override_Price"];
                GP_Override_Price.AdvancedSearch.SearchValue2 = filter["y_GP_Override_Price"];
                GP_Override_Price.AdvancedSearch.SearchOperator2 = filter["w_GP_Override_Price"];
                GP_Override_Price.AdvancedSearch.Save();
            }

            // Field Override_Core
            if (filter?.TryGetValue("x_Override_Core", out sv) ?? false) {
                Override_Core.AdvancedSearch.SearchValue = sv;
                Override_Core.AdvancedSearch.SearchOperator = filter["z_Override_Core"];
                Override_Core.AdvancedSearch.SearchCondition = filter["v_Override_Core"];
                Override_Core.AdvancedSearch.SearchValue2 = filter["y_Override_Core"];
                Override_Core.AdvancedSearch.SearchOperator2 = filter["w_Override_Core"];
                Override_Core.AdvancedSearch.Save();
            }

            // Field Override_Percent
            if (filter?.TryGetValue("x_Override_Percent", out sv) ?? false) {
                Override_Percent.AdvancedSearch.SearchValue = sv;
                Override_Percent.AdvancedSearch.SearchOperator = filter["z_Override_Percent"];
                Override_Percent.AdvancedSearch.SearchCondition = filter["v_Override_Percent"];
                Override_Percent.AdvancedSearch.SearchValue2 = filter["y_Override_Percent"];
                Override_Percent.AdvancedSearch.SearchOperator2 = filter["w_Override_Percent"];
                Override_Percent.AdvancedSearch.Save();
            }

            // Field Core_Life_Ind
            if (filter?.TryGetValue("x_Core_Life_Ind", out sv) ?? false) {
                Core_Life_Ind.AdvancedSearch.SearchValue = sv;
                Core_Life_Ind.AdvancedSearch.SearchOperator = filter["z_Core_Life_Ind"];
                Core_Life_Ind.AdvancedSearch.SearchCondition = filter["v_Core_Life_Ind"];
                Core_Life_Ind.AdvancedSearch.SearchValue2 = filter["y_Core_Life_Ind"];
                Core_Life_Ind.AdvancedSearch.SearchOperator2 = filter["w_Core_Life_Ind"];
                Core_Life_Ind.AdvancedSearch.Save();
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
            BuildSearchSql(ref where, request_id, def, false); // request_id
            BuildSearchSql(ref where, Item_No, def, true); // Item_No
            BuildSearchSql(ref where, Part_No, def, true); // Part_No
            BuildSearchSql(ref where, Part_Description, def, true); // Part_Description
            BuildSearchSql(ref where, Qty, def, true); // Qty
            BuildSearchSql(ref where, SAP_Unit_Price, def, true); // SAP_Unit_Price
            BuildSearchSql(ref where, Extd_SAP_Price, def, true); // Extd_SAP_Price
            BuildSearchSql(ref where, GP_SAP_Price, def, true); // GP_SAP_Price
            BuildSearchSql(ref where, Override_Unit_Price, def, true); // Override_Unit_Price
            BuildSearchSql(ref where, Extd_Override_Price, def, true); // Extd_Override_Price
            BuildSearchSql(ref where, GP_Override_Price, def, true); // GP_Override_Price
            BuildSearchSql(ref where, Override_Core, def, true); // Override_Core
            BuildSearchSql(ref where, Override_Percent, def, true); // Override_Percent
            BuildSearchSql(ref where, Core_Life_Ind, def, true); // Core_Life_Ind
            BuildSearchSql(ref where, Notes, def, true); // Notes

            // Set up search command
            if (!def && !Empty(where) && (new[] { "", "reset", "resetall" }).Contains(Command))
                Command = "search";
            if (!def && Command == "search") {
                id.AdvancedSearch.Save(); // id
                request_id.AdvancedSearch.Save(); // request_id
                Item_No.AdvancedSearch.Save(); // Item_No
                Part_No.AdvancedSearch.Save(); // Part_No
                Part_Description.AdvancedSearch.Save(); // Part_Description
                Qty.AdvancedSearch.Save(); // Qty
                SAP_Unit_Price.AdvancedSearch.Save(); // SAP_Unit_Price
                Extd_SAP_Price.AdvancedSearch.Save(); // Extd_SAP_Price
                GP_SAP_Price.AdvancedSearch.Save(); // GP_SAP_Price
                Override_Unit_Price.AdvancedSearch.Save(); // Override_Unit_Price
                Extd_Override_Price.AdvancedSearch.Save(); // Extd_Override_Price
                GP_Override_Price.AdvancedSearch.Save(); // GP_Override_Price
                Override_Core.AdvancedSearch.Save(); // Override_Core
                Override_Percent.AdvancedSearch.Save(); // Override_Percent
                Core_Life_Ind.AdvancedSearch.Save(); // Core_Life_Ind
                Notes.AdvancedSearch.Save(); // Notes

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

            // Field Item_No
            filter = QueryBuilderWhere("Item_No");
            if (Empty(filter))
                BuildSearchSql(ref filter, Item_No, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Item_No.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Part_No
            filter = QueryBuilderWhere("Part_No");
            if (Empty(filter))
                BuildSearchSql(ref filter, Part_No, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Part_No.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Part_Description
            filter = QueryBuilderWhere("Part_Description");
            if (Empty(filter))
                BuildSearchSql(ref filter, Part_Description, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Part_Description.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Qty
            filter = QueryBuilderWhere("Qty");
            if (Empty(filter))
                BuildSearchSql(ref filter, Qty, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Qty.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field SAP_Unit_Price
            filter = QueryBuilderWhere("SAP_Unit_Price");
            if (Empty(filter))
                BuildSearchSql(ref filter, SAP_Unit_Price, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + SAP_Unit_Price.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Extd_SAP_Price
            filter = QueryBuilderWhere("Extd_SAP_Price");
            if (Empty(filter))
                BuildSearchSql(ref filter, Extd_SAP_Price, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Extd_SAP_Price.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field GP_SAP_Price
            filter = QueryBuilderWhere("GP_SAP_Price");
            if (Empty(filter))
                BuildSearchSql(ref filter, GP_SAP_Price, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + GP_SAP_Price.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Override_Unit_Price
            filter = QueryBuilderWhere("Override_Unit_Price");
            if (Empty(filter))
                BuildSearchSql(ref filter, Override_Unit_Price, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Override_Unit_Price.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Extd_Override_Price
            filter = QueryBuilderWhere("Extd_Override_Price");
            if (Empty(filter))
                BuildSearchSql(ref filter, Extd_Override_Price, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Extd_Override_Price.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field GP_Override_Price
            filter = QueryBuilderWhere("GP_Override_Price");
            if (Empty(filter))
                BuildSearchSql(ref filter, GP_Override_Price, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + GP_Override_Price.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Override_Core
            filter = QueryBuilderWhere("Override_Core");
            if (Empty(filter))
                BuildSearchSql(ref filter, Override_Core, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Override_Core.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Override_Percent
            filter = QueryBuilderWhere("Override_Percent");
            if (Empty(filter))
                BuildSearchSql(ref filter, Override_Percent, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Override_Percent.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Core_Life_Ind
            filter = QueryBuilderWhere("Core_Life_Ind");
            if (Empty(filter))
                BuildSearchSql(ref filter, Core_Life_Ind, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Core_Life_Ind.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field Notes
            filter = QueryBuilderWhere("Notes");
            if (Empty(filter))
                BuildSearchSql(ref filter, Notes, false, true);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + Notes.Caption + "</span>" + captionSuffix + filter + "</div>";
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
            searchFlds.Add(Item_No);
            searchFlds.Add(Part_No);
            searchFlds.Add(Part_Description);
            searchFlds.Add(Notes);
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
            if (request_id.AdvancedSearch.IssetSession)
                return true;
            if (Item_No.AdvancedSearch.IssetSession)
                return true;
            if (Part_No.AdvancedSearch.IssetSession)
                return true;
            if (Part_Description.AdvancedSearch.IssetSession)
                return true;
            if (Qty.AdvancedSearch.IssetSession)
                return true;
            if (SAP_Unit_Price.AdvancedSearch.IssetSession)
                return true;
            if (Extd_SAP_Price.AdvancedSearch.IssetSession)
                return true;
            if (GP_SAP_Price.AdvancedSearch.IssetSession)
                return true;
            if (Override_Unit_Price.AdvancedSearch.IssetSession)
                return true;
            if (Extd_Override_Price.AdvancedSearch.IssetSession)
                return true;
            if (GP_Override_Price.AdvancedSearch.IssetSession)
                return true;
            if (Override_Core.AdvancedSearch.IssetSession)
                return true;
            if (Override_Percent.AdvancedSearch.IssetSession)
                return true;
            if (Core_Life_Ind.AdvancedSearch.IssetSession)
                return true;
            if (Notes.AdvancedSearch.IssetSession)
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
            request_id.AdvancedSearch.UnsetSession();
            Item_No.AdvancedSearch.UnsetSession();
            Part_No.AdvancedSearch.UnsetSession();
            Part_Description.AdvancedSearch.UnsetSession();
            Qty.AdvancedSearch.UnsetSession();
            SAP_Unit_Price.AdvancedSearch.UnsetSession();
            Extd_SAP_Price.AdvancedSearch.UnsetSession();
            GP_SAP_Price.AdvancedSearch.UnsetSession();
            Override_Unit_Price.AdvancedSearch.UnsetSession();
            Extd_Override_Price.AdvancedSearch.UnsetSession();
            GP_Override_Price.AdvancedSearch.UnsetSession();
            Override_Core.AdvancedSearch.UnsetSession();
            Override_Percent.AdvancedSearch.UnsetSession();
            Core_Life_Ind.AdvancedSearch.UnsetSession();
            Notes.AdvancedSearch.UnsetSession();
        }

        // Restore all search parameters
        protected void RestoreSearchParms() {
            RestoreSearch = true;

            // Restore basic search values
            BasicSearch.Load();

            // Restore advanced search values
            id.AdvancedSearch.Load();
            request_id.AdvancedSearch.Load();
            Item_No.AdvancedSearch.Load();
            Part_No.AdvancedSearch.Load();
            Part_Description.AdvancedSearch.Load();
            Qty.AdvancedSearch.Load();
            SAP_Unit_Price.AdvancedSearch.Load();
            Extd_SAP_Price.AdvancedSearch.Load();
            GP_SAP_Price.AdvancedSearch.Load();
            Override_Unit_Price.AdvancedSearch.Load();
            Extd_Override_Price.AdvancedSearch.Load();
            GP_Override_Price.AdvancedSearch.Load();
            Override_Core.AdvancedSearch.Load();
            Override_Percent.AdvancedSearch.Load();
            Core_Life_Ind.AdvancedSearch.Load();
            Notes.AdvancedSearch.Load();
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
                UpdateSort(Item_No); // Item_No
                UpdateSort(Part_No); // Part_No
                UpdateSort(Part_Description); // Part_Description
                UpdateSort(Qty); // Qty
                UpdateSort(SAP_Unit_Price); // SAP_Unit_Price
                UpdateSort(Extd_SAP_Price); // Extd_SAP_Price
                UpdateSort(GP_SAP_Price); // GP_SAP_Price
                UpdateSort(Override_Unit_Price); // Override_Unit_Price
                UpdateSort(Extd_Override_Price); // Extd_Override_Price
                UpdateSort(GP_Override_Price); // GP_Override_Price
                UpdateSort(Override_Core); // Override_Core
                UpdateSort(Override_Percent); // Override_Percent
                UpdateSort(Core_Life_Ind); // Core_Life_Ind
                UpdateSort(Notes); // Notes
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
                    id.Sort = "";
                    request_id.Sort = "";
                    Item_No.Sort = "";
                    Part_No.Sort = "";
                    Part_Description.Sort = "";
                    Qty.Sort = "";
                    SAP_Unit_Price.Sort = "";
                    Extd_SAP_Price.Sort = "";
                    GP_SAP_Price.Sort = "";
                    Override_Unit_Price.Sort = "";
                    Extd_Override_Price.Sort = "";
                    GP_Override_Price.Sort = "";
                    Override_Core.Sort = "";
                    Override_Percent.Sort = "";
                    Core_Life_Ind.Sort = "";
                    Notes.Sort = "";
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
            item.Visible = (Security.CanDelete || Security.CanEdit);
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
                        string link = "<li><button type=\"button\" class=\"dropdown-item ew-action ew-list-action\" data-caption=\"" + HtmlTitle(caption) + "\" data-ew-action=\"submit\" form=\"ftr_request_detaillist\" data-key=\"" + KeyToJson(true) + "\"" + act.ToDataAttrs() + ">" + icon + " " + caption + "</button></li>";
                        if (!Empty(link)) {
                            links.Add(link);
                            if (Empty(body)) // Setup first button
                                body = "<button type=\"button\" class=\"btn btn-default ew-action ew-list-action\" title=\"" + HtmlTitle(caption) + "\" data-caption=\"" + HtmlTitle(caption) + "\" data-ew-action=\"submit\" form=\"ftr_request_detaillist\" data-key=\"" + KeyToJson(true) + "\"" + act.ToDataAttrs() + ">" + icon + caption + "</button>";
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

            // "checkbox"
            listOption = ListOptions["checkbox"];
            listOption?.SetBody("<div class=\"form-check\"><input type=\"checkbox\" id=\"key_m_" + RowCount + "\" name=\"key_m[]\" class=\"form-check-input ew-multi-select\" value=\"" + HtmlEncode(id.CurrentValue) + "\" data-ew-action=\"select-key\"></div>");
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
            var options = OtherOptions;
            option = options["addedit"];

            // Add
            item = option.Add("add");
            string addTitle = Language.Phrase("AddLink", true);
            if (ModalAdd && !IsMobile())
                item.Body = $@"<a class=""ew-add-edit ew-add"" title=""{addTitle}"" data-table=""tr_request_detail"" data-caption=""{addTitle}"" data-ew-action=""modal"" data-action=""add"" data-ajax=""" + (UseAjaxActions ? "true" : "false") + "\" data-url=\"" + HtmlEncode(AppPath(AddUrl)) + "\" data-btn=\"AddBtn\">" + Language.Phrase("AddLink") + "</a>";
            else
                item.Body = $@"<a class=""ew-add-edit ew-add"" title=""{addTitle}"" data-caption=""{addTitle}"" href=""" + HtmlEncode(AppPath(AddUrl)) + "\">" + Language.Phrase("AddLink") + "</a>";
            item.Visible = AddUrl != "" && Security.CanAdd;
            option = options["action"];

            // Add multi delete
            item = option.Add("multidelete");
            string deleteTitle = Language.Phrase("DeleteSelectedLink", true);
            item.Body = $@"<button type=""button"" class=""ew-action ew-multi-delete"" title=""{deleteTitle}"" data-caption=""{deleteTitle}"" form=""ftr_request_detaillist""" +
                " data-ew-action=\"" + (UseAjaxActions ? "inline" : "submit") + "\"" +
                (UseAjaxActions ? " data-action=\"delete\"" : "") +
                " data-url=\"" + HtmlEncode(AppPath(MultiDeleteUrl)) + "\"" +
                (InlineDelete ? " data-msg=\"" + HtmlEncode(Language.Phrase("DeleteConfirm")) + "\" data-data='{\"action\":\"delete\"}'" : " data-data='{\"action\":\"show\"}'") +
                ">" + Language.Phrase("DeleteSelectedLink") + "</button>";
            item.Visible = Security.CanDelete;

            // Add multi update
            item = option.Add("multiupdate");
            string updateTitle = Language.Phrase("UpdateSelectedLink", true);
            item.Body = $@"<button type=""button"" class=""ew-action ew-multi-update"" title=""{updateTitle}"" data-table=""tr_request_detail"" data-caption=""{updateTitle}"" form=""ftr_request_detaillist"" data-ew-action=""" +
                (ModalUpdate && !IsMobile() ? "modal" : "submit") + "\"" +
                (ModalUpdate && !IsMobile() ? " data-action=\"update\"" : "") +
                (UseAjaxActions ? " data-ajax=\"true\"" : "") +
                " data-url=\"" + HtmlEncode(AppPath(MultiUpdateUrl)) + "\">" + Language.Phrase("UpdateSelectedLink") + "</button>";
            item.Visible = Security.CanEdit;

            // Show column list for column visibility
            if (UseColumnVisibility) {
                option = OtherOptions["column"];
                item = option.AddGroupOption();
                item.Body = "";
                item.Visible = UseColumnVisibility;
                CreateColumnOption(option.Add("Item_No")); // DN
                CreateColumnOption(option.Add("Part_No")); // DN
                CreateColumnOption(option.Add("Part_Description")); // DN
                CreateColumnOption(option.Add("Qty")); // DN
                CreateColumnOption(option.Add("SAP_Unit_Price")); // DN
                CreateColumnOption(option.Add("Extd_SAP_Price")); // DN
                CreateColumnOption(option.Add("GP_SAP_Price")); // DN
                CreateColumnOption(option.Add("Override_Unit_Price")); // DN
                CreateColumnOption(option.Add("Extd_Override_Price")); // DN
                CreateColumnOption(option.Add("GP_Override_Price")); // DN
                CreateColumnOption(option.Add("Override_Core")); // DN
                CreateColumnOption(option.Add("Override_Percent")); // DN
                CreateColumnOption(option.Add("Core_Life_Ind")); // DN
                CreateColumnOption(option.Add("Notes")); // DN
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
            item.Body = "<a class=\"ew-save-filter\" data-form=\"ftr_request_detailsrch\" data-ew-action=\"none\">" + Language.Phrase("SaveCurrentFilter") + "</a>";
            item.Visible = true;
            item = FilterOptions.Add("deletefilter");
            item.Body = "<a class=\"ew-delete-filter\" data-form=\"ftr_request_detailsrch\" data-ew-action=\"none\">" + Language.Phrase("DeleteFilter") + "</a>";
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
                    item.Body = "<<button type=\"button\" class=\"btn btn-default ew-action ew-list-action\" title=\"" + HtmlEncode(caption) + "\" data-caption=\"" + HtmlEncode(caption) + "\" data-ew-action=\"submit\" form=\"ftr_request_detaillist\"" + act.ToDataAttrs() + ">" + icon + "</button>";
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
            RowAttrs.Add("data-rowindex", ConvertToString(trRequestDetailList.RowCount));
            RowAttrs.Add("data-key", GetKey(true));
            RowAttrs.Add("id", "r" + ConvertToString(trRequestDetailList.RowCount) + "_tr_request_detail");
            RowAttrs.Add("data-rowtype", ConvertToString((int)RowType));
            RowAttrs.AppendClass(trRequestDetailList.RowCount % 2 != 1 ? "ew-table-alt-row" : "");
            if (IsAdd && trRequestDetailList.RowType == RowType.Add || IsEdit && trRequestDetailList.RowType == RowType.Edit) // Inline-Add/Edit row
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

            // request_id
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_request_id"))
                    request_id.AdvancedSearch.SearchValue = Get("x_request_id");
                else
                    request_id.AdvancedSearch.SearchValue = Get("request_id"); // Default Value // DN
            if (!Empty(request_id.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_request_id"))
                request_id.AdvancedSearch.SearchOperator = Get("z_request_id");

            // Item_No
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Item_No[]"))
                    Item_No.AdvancedSearch.SearchValue = Get("x_Item_No[]");
                else
                    Item_No.AdvancedSearch.SearchValue = Get("Item_No"); // Default Value // DN
            if (!Empty(Item_No.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Item_No"))
                Item_No.AdvancedSearch.SearchOperator = Get("z_Item_No");

            // Part_No
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Part_No[]"))
                    Part_No.AdvancedSearch.SearchValue = Get("x_Part_No[]");
                else
                    Part_No.AdvancedSearch.SearchValue = Get("Part_No"); // Default Value // DN
            if (!Empty(Part_No.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Part_No"))
                Part_No.AdvancedSearch.SearchOperator = Get("z_Part_No");

            // Part_Description
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Part_Description[]"))
                    Part_Description.AdvancedSearch.SearchValue = Get("x_Part_Description[]");
                else
                    Part_Description.AdvancedSearch.SearchValue = Get("Part_Description"); // Default Value // DN
            if (!Empty(Part_Description.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Part_Description"))
                Part_Description.AdvancedSearch.SearchOperator = Get("z_Part_Description");

            // Qty
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Qty[]"))
                    Qty.AdvancedSearch.SearchValue = Get("x_Qty[]");
                else
                    Qty.AdvancedSearch.SearchValue = Get("Qty"); // Default Value // DN
            if (!Empty(Qty.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Qty"))
                Qty.AdvancedSearch.SearchOperator = Get("z_Qty");

            // SAP_Unit_Price
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_SAP_Unit_Price[]"))
                    SAP_Unit_Price.AdvancedSearch.SearchValue = Get("x_SAP_Unit_Price[]");
                else
                    SAP_Unit_Price.AdvancedSearch.SearchValue = Get("SAP_Unit_Price"); // Default Value // DN
            if (!Empty(SAP_Unit_Price.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_SAP_Unit_Price"))
                SAP_Unit_Price.AdvancedSearch.SearchOperator = Get("z_SAP_Unit_Price");

            // Extd_SAP_Price
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Extd_SAP_Price[]"))
                    Extd_SAP_Price.AdvancedSearch.SearchValue = Get("x_Extd_SAP_Price[]");
                else
                    Extd_SAP_Price.AdvancedSearch.SearchValue = Get("Extd_SAP_Price"); // Default Value // DN
            if (!Empty(Extd_SAP_Price.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Extd_SAP_Price"))
                Extd_SAP_Price.AdvancedSearch.SearchOperator = Get("z_Extd_SAP_Price");

            // GP_SAP_Price
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_GP_SAP_Price[]"))
                    GP_SAP_Price.AdvancedSearch.SearchValue = Get("x_GP_SAP_Price[]");
                else
                    GP_SAP_Price.AdvancedSearch.SearchValue = Get("GP_SAP_Price"); // Default Value // DN
            if (!Empty(GP_SAP_Price.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_GP_SAP_Price"))
                GP_SAP_Price.AdvancedSearch.SearchOperator = Get("z_GP_SAP_Price");

            // Override_Unit_Price
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Override_Unit_Price[]"))
                    Override_Unit_Price.AdvancedSearch.SearchValue = Get("x_Override_Unit_Price[]");
                else
                    Override_Unit_Price.AdvancedSearch.SearchValue = Get("Override_Unit_Price"); // Default Value // DN
            if (!Empty(Override_Unit_Price.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Override_Unit_Price"))
                Override_Unit_Price.AdvancedSearch.SearchOperator = Get("z_Override_Unit_Price");

            // Extd_Override_Price
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Extd_Override_Price[]"))
                    Extd_Override_Price.AdvancedSearch.SearchValue = Get("x_Extd_Override_Price[]");
                else
                    Extd_Override_Price.AdvancedSearch.SearchValue = Get("Extd_Override_Price"); // Default Value // DN
            if (!Empty(Extd_Override_Price.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Extd_Override_Price"))
                Extd_Override_Price.AdvancedSearch.SearchOperator = Get("z_Extd_Override_Price");

            // GP_Override_Price
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_GP_Override_Price[]"))
                    GP_Override_Price.AdvancedSearch.SearchValue = Get("x_GP_Override_Price[]");
                else
                    GP_Override_Price.AdvancedSearch.SearchValue = Get("GP_Override_Price"); // Default Value // DN
            if (!Empty(GP_Override_Price.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_GP_Override_Price"))
                GP_Override_Price.AdvancedSearch.SearchOperator = Get("z_GP_Override_Price");

            // Override_Core
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Override_Core[]"))
                    Override_Core.AdvancedSearch.SearchValue = Get("x_Override_Core[]");
                else
                    Override_Core.AdvancedSearch.SearchValue = Get("Override_Core"); // Default Value // DN
            if (!Empty(Override_Core.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Override_Core"))
                Override_Core.AdvancedSearch.SearchOperator = Get("z_Override_Core");

            // Override_Percent
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Override_Percent[]"))
                    Override_Percent.AdvancedSearch.SearchValue = Get("x_Override_Percent[]");
                else
                    Override_Percent.AdvancedSearch.SearchValue = Get("Override_Percent"); // Default Value // DN
            if (!Empty(Override_Percent.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Override_Percent"))
                Override_Percent.AdvancedSearch.SearchOperator = Get("z_Override_Percent");

            // Core_Life_Ind
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Core_Life_Ind[]"))
                    Core_Life_Ind.AdvancedSearch.SearchValue = Get("x_Core_Life_Ind[]");
                else
                    Core_Life_Ind.AdvancedSearch.SearchValue = Get("Core_Life_Ind"); // Default Value // DN
            if (!Empty(Core_Life_Ind.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Core_Life_Ind"))
                Core_Life_Ind.AdvancedSearch.SearchOperator = Get("z_Core_Life_Ind");

            // Notes
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_Notes[]"))
                    Notes.AdvancedSearch.SearchValue = Get("x_Notes[]");
                else
                    Notes.AdvancedSearch.SearchValue = Get("Notes"); // Default Value // DN
            if (!Empty(Notes.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_Notes"))
                Notes.AdvancedSearch.SearchOperator = Get("z_Notes");
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
            } else if (RowType == RowType.Search) {
                // Item_No
                if (Item_No.UseFilter && !Empty(Item_No.AdvancedSearch.SearchValue)) {
                    Item_No.EditValue = ConvertToString(Item_No.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // Part_No
                if (Part_No.UseFilter && !Empty(Part_No.AdvancedSearch.SearchValue)) {
                    Part_No.EditValue = ConvertToString(Part_No.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // Part_Description
                if (Part_Description.UseFilter && !Empty(Part_Description.AdvancedSearch.SearchValue)) {
                    Part_Description.EditValue = ConvertToString(Part_Description.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // Qty
                if (Qty.UseFilter && !Empty(Qty.AdvancedSearch.SearchValue)) {
                    Qty.EditValue = ConvertToString(Qty.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // SAP_Unit_Price
                if (SAP_Unit_Price.UseFilter && !Empty(SAP_Unit_Price.AdvancedSearch.SearchValue)) {
                    SAP_Unit_Price.EditValue = ConvertToString(SAP_Unit_Price.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // Extd_SAP_Price
                if (Extd_SAP_Price.UseFilter && !Empty(Extd_SAP_Price.AdvancedSearch.SearchValue)) {
                    Extd_SAP_Price.EditValue = ConvertToString(Extd_SAP_Price.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // GP_SAP_Price
                if (GP_SAP_Price.UseFilter && !Empty(GP_SAP_Price.AdvancedSearch.SearchValue)) {
                    GP_SAP_Price.EditValue = ConvertToString(GP_SAP_Price.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // Override_Unit_Price
                if (Override_Unit_Price.UseFilter && !Empty(Override_Unit_Price.AdvancedSearch.SearchValue)) {
                    Override_Unit_Price.EditValue = ConvertToString(Override_Unit_Price.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // Extd_Override_Price
                if (Extd_Override_Price.UseFilter && !Empty(Extd_Override_Price.AdvancedSearch.SearchValue)) {
                    Extd_Override_Price.EditValue = ConvertToString(Extd_Override_Price.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // GP_Override_Price
                if (GP_Override_Price.UseFilter && !Empty(GP_Override_Price.AdvancedSearch.SearchValue)) {
                    GP_Override_Price.EditValue = ConvertToString(GP_Override_Price.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // Override_Core
                if (Override_Core.UseFilter && !Empty(Override_Core.AdvancedSearch.SearchValue)) {
                    Override_Core.EditValue = ConvertToString(Override_Core.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // Override_Percent
                if (Override_Percent.UseFilter && !Empty(Override_Percent.AdvancedSearch.SearchValue)) {
                    Override_Percent.EditValue = ConvertToString(Override_Percent.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // Core_Life_Ind
                if (Core_Life_Ind.UseFilter && !Empty(Core_Life_Ind.AdvancedSearch.SearchValue)) {
                    Core_Life_Ind.EditValue = ConvertToString(Core_Life_Ind.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }

                // Notes
                if (Notes.UseFilter && !Empty(Notes.AdvancedSearch.SearchValue)) {
                    Notes.EditValue = ConvertToString(Notes.AdvancedSearch.SearchValue).Split(Config.MultipleOptionSeparator).ToList();
                }
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

        // Load advanced search
        public void LoadAdvancedSearch()
        {
            id.AdvancedSearch.Load();
            request_id.AdvancedSearch.Load();
            Item_No.AdvancedSearch.Load();
            Part_No.AdvancedSearch.Load();
            Part_Description.AdvancedSearch.Load();
            Qty.AdvancedSearch.Load();
            SAP_Unit_Price.AdvancedSearch.Load();
            Extd_SAP_Price.AdvancedSearch.Load();
            GP_SAP_Price.AdvancedSearch.Load();
            Override_Unit_Price.AdvancedSearch.Load();
            Extd_Override_Price.AdvancedSearch.Load();
            GP_Override_Price.AdvancedSearch.Load();
            Override_Core.AdvancedSearch.Load();
            Override_Percent.AdvancedSearch.Load();
            Core_Life_Ind.AdvancedSearch.Load();
            Notes.AdvancedSearch.Load();
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
                    return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-excel\" title=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\" form=\"ftr_request_detaillist\" data-url=\"" + exportUrl + "\" data-ew-action=\"export\" data-export=\"excel\" data-custom=\"true\" data-export-selected=\"false\">" + Language.Phrase("ExportToExcel") + "</button>";
                else
                    return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-excel\" title=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\">" + Language.Phrase("ExportToExcel") + "</a>";
            } else if (SameText(type, "word")) {
                if (custom)
                    return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-word\" title=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\" form=\"ftr_request_detaillist\" data-url=\"" + exportUrl + "\" data-ew-action=\"export\" data-export=\"word\" data-custom=\"true\" data-export-selected=\"false\">" + Language.Phrase("ExportToWord") + "</button>";
                else
                    return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-word\" title=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\">" + Language.Phrase("ExportToWord") + "</a>";
            } else if (SameText(type, "pdf")) {
                if (custom)
                    return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-pdf\" title=\"" + HtmlEncode(Language.Phrase("ExportToPdf", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToPdf", true)) + "\" form=\"ftr_request_detaillist\" data-url=\"" + exportUrl + "\" data-ew-action=\"export\" data-export=\"pdf\" data-custom=\"true\" data-export-selected=\"false\">" + Language.Phrase("ExportToPDF") + "</button>";
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
                return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-email\" title=\"" + Language.Phrase("ExportToEmail", true) + "\" data-caption=\"" + Language.Phrase("ExportToEmail", true) + "\" form=\"ftr_request_detaillist\" data-ew-action=\"email\" data-custom=\"false\" data-hdr=\"" + Language.Phrase("ExportToEmail", true) + "\" data-export-selected=\"false\"" + url + ">" + Language.Phrase("ExportToEmail") + "</button>";
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
            item.Body = "<a class=\"btn btn-default ew-search-toggle" + searchToggleClass + "\" role=\"button\" title=\"" + Language.Phrase("SearchPanel") + "\" data-caption=\"" + Language.Phrase("SearchPanel") + "\" data-ew-action=\"search-toggle\" data-form=\"ftr_request_detailsrch\" aria-pressed=\"" + (searchToggleClass == " active" ? "true" : "false") + "\">" + Language.Phrase("SearchLink") + "</a>";
            item.Visible = true;

            // Show all button
            item = SearchOptions.Add("showall");
            if (UseCustomTemplate || !UseAjaxActions)
                item.Body = "<a class=\"btn btn-default ew-show-all\" role=\"button\" title=\"" + Language.Phrase("ShowAll") + "\" data-caption=\"" + Language.Phrase("ShowAll") + "\" href=\"" + AppPath(PageUrl) + "cmd=reset\">" + Language.Phrase("ShowAllBtn") + "</a>";
            else
                item.Body = "<a class=\"btn btn-default ew-show-all\" role=\"button\" title=\"" + Language.Phrase("ShowAll") + "\" data-caption=\"" + Language.Phrase("ShowAll") + "\" data-ew-action=\"refresh\" data-url=\"" + AppPath(PageUrl) + "cmd=reset\">" + Language.Phrase("ShowAllBtn") + "</a>";
            item.Visible = (SearchWhere != DefaultSearchWhere && SearchWhere != "0=101");

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
            string exportStyle;

            // Export master record
            if (Config.ExportMasterRecord && !Empty(MasterFilterFromSession) && CurrentMasterTable == "v_tr_request") {
                vTrRequest = new VTrRequestList();
                if (vTrRequest != null) {
                    var c = await GetConnection2Async(vTrRequest.DbId); // Note: Use new connection for master record // DN
                    using var rsmaster = await vTrRequest.LoadReader(DbMasterFilter, "", c); // Load master record
                    if (rsmaster?.HasRows ?? false) { // DN
                        exportStyle = doc.Style;
                        doc.SetStyle("v"); // Change to vertical
                        if (!IsExport("csv") || Config.ExportMasterRecordForCsv) {
                            doc.Table = vTrRequest;
                            await vTrRequest.ExportDocument(doc, rsmaster, 1, 1);
                            doc.ExportEmptyRow();
                            doc.Table = this;
                        }
                        doc.SetStyle(exportStyle); // Restore
                    }
                }
            }

            // Export master record
            if (Config.ExportMasterRecord && !Empty(MasterFilterFromSession) && CurrentMasterTable == "tr_request") {
                trRequest = new TrRequestList();
                if (trRequest != null) {
                    var c = await GetConnection2Async(trRequest.DbId); // Note: Use new connection for master record // DN
                    using var rsmaster = await trRequest.LoadReader(DbMasterFilter, "", c); // Load master record
                    if (rsmaster?.HasRows ?? false) { // DN
                        exportStyle = doc.Style;
                        doc.SetStyle("v"); // Change to vertical
                        if (!IsExport("csv") || Config.ExportMasterRecordForCsv) {
                            doc.Table = trRequest;
                            await trRequest.ExportDocument(doc, rsmaster, 1, 1);
                            doc.ExportEmptyRow();
                            doc.Table = this;
                        }
                        doc.SetStyle(exportStyle); // Restore
                    }
                }
            }

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

                // Update URL
                AddUrl = AddMasterUrl(AddUrl);
                InlineAddUrl = AddMasterUrl(InlineAddUrl);
                GridAddUrl = AddMasterUrl(GridAddUrl);
                GridEditUrl = AddMasterUrl(GridEditUrl);
                MultiEditUrl = AddMasterUrl(MultiEditUrl);

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
