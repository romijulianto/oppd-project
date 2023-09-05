namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// trRequestView
    /// </summary>
    public static TrRequestView trRequestView
    {
        get => HttpData.Get<TrRequestView>("trRequestView")!;
        set => HttpData["trRequestView"] = value;
    }

    /// <summary>
    /// Page class for tr_request
    /// </summary>
    public class TrRequestView : TrRequestViewBase
    {
        // Constructor
        public TrRequestView(Controller controller) : base(controller)
        {
        }

        // Constructor
        public TrRequestView() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TrRequestViewBase : TrRequest
    {
        // Page ID
        public string PageID = "view";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "tr_request";

        // Page object name
        public string PageObjName = "trRequestView";

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
        public TrRequestViewBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-view-table";

            // Language object
            Language = ResolveLanguage();

            // Table object (trRequest)
            if (trRequest == null || trRequest is TrRequest)
                trRequest = this;

            // DN
            string[] keys = new string[0];
            StringValues str = "";
            object? obj = null;
            string currentPageName = CurrentPageName();
            string currentUrl = AppPath(currentPageName); // DN
            if (IsApi()) {
                if (RouteValues.TryGetValue("key", out object? k) && !Empty(k))
                    keys = ConvertToString(k).Split('/');
                if (keys.Length > 0)
                    RecordKeys["id"] = keys[0];
            } else {
                RecordKeys["id"] = RouteValues.TryGetValue("id", out obj) && obj != null ? UrlDecode(obj) : Get("id"); // DN
            }

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
            OtherOptions["detail"] = new () { TagClassName = "ew-detail-option" };
            OtherOptions["action"] = new () { TagClassName = "ew-action-option" };
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
        public string PageName => "TrRequestView";

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
            ETL_Date.SetVisibility();
            Request_Currency.SetVisibility();
        }

        // Constructor
        public TrRequestViewBase(Controller? controller = null): this() { // DN
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

        public int DisplayRecords = 1; // Number of display records

        public int StartRecord;

        public int StopRecord;

        public int TotalRecords = -1;

        public int RecordRange = 10;

        public int RecordCount;

        public Dictionary<string, string> RecordKeys = new ();

        public bool IsModal = false;

        public string SearchWhere = "";

        public string DbMasterFilter = "";

        public string DbDetailFilter = "";

        public bool MasterRecordExists;

        public DbDataReader? Recordset = null;

        public SubPages? MultiPages; // Multi pages object

        public SubPages? DetailPages; // Detail pages object

        #pragma warning disable 168, 219
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

            // Check modal
            if (IsModal)
                SkipHeaderFooter = true;

            // Load current record
            bool loadCurrentRecord = false;
            string returnUrl = "";
            bool matchRecord = false;
            string[] keyValues = {};
            object? v;
            StringValues sv;
            if (IsApi()) {
                if (RouteValues.TryGetValue(Config.ApiKeyName, out object? k)) {
                    if (!Empty(k))
                        keyValues = ConvertToString(k).Split('/');
                } else { // Get key from query string
                    string key = Get(Config.ApiKeyName);
                    if (!Empty(key))
                        keyValues = key.Split(',');
                }
                if (keyValues.Length == 0)
                    return new JsonBoolResult(new { success = false, error = Language.Phrase("NoRecord"), version = Config.ProductVersion }, false);
            }
            if (RouteValues.TryGetValue("id", out v) && !Empty(v)) { // DN
                id.QueryValue = UrlDecode(v); // DN
                RecordKeys["id"] = id.QueryValue;
            } else if (Get("id", out sv)) {
                id.QueryValue = sv.ToString();
                RecordKeys["id"] = id.QueryValue;
            } else if (IsApi() && !Empty(keyValues)) {
                id.QueryValue = ConvertToString(keyValues[0]);
                RecordKeys["id"] = id.QueryValue;
            } else if (!loadCurrentRecord) {
                returnUrl = "TrRequestList"; // Return to list
            }

            // Get action
            CurrentAction = "show"; // Display form
            switch (CurrentAction) {
                case "show": // Get a record to display
                        bool res;
                        if (IsApi()) {
                            string filter = GetRecordFilter();
                            CurrentFilter = filter;
                            string sql = CurrentSql;
                            var conn = await GetConnectionAsync();
                            Recordset = await conn.GetDataReaderAsync(sql);
                            res = !Empty(Recordset) && await Recordset.ReadAsync();
                        } else {
                            res = await LoadRow();
                        }
                        if (!res) { // Load record based on key
                            if (Empty(SuccessMessage) && Empty(FailureMessage))
                                FailureMessage = Language.Phrase("NoRecord"); // Set no record message
                            if (IsApi()) {
                                if (!Empty(SuccessMessage))
                                    return new JsonBoolResult(new { success = true, message = SuccessMessage, version = Config.ProductVersion }, true);
                                else
                                    return new JsonBoolResult(new { success = false, error = FailureMessage, version = Config.ProductVersion }, false);
                            } else {
                                return Terminate("TrRequestList"); // Return to list page
                            }
                        }
                    break;
            }
            if (!Empty(returnUrl))
                return Terminate(returnUrl);

            // Render row
            RowType = RowType.View;
            ResetAttributes();
            await RenderRow();

            // Setup export options
            SetupExportOptions();

            // Set up Breadcrumb
            if (!IsExport())
                SetupBreadcrumb();

            // Set up detail parameters
            SetupDetailParms();

            // Normal return
            if (IsApi()) // Get current record only
                if (!IsExport())
                    return Controller.Json(new { success = true, TableVar = await GetRecordFromRecordset(Recordset), version = Config.ProductVersion });

            // Set LoginStatus, Page Rendering and Page Render
            if (!IsApi() && !IsTerminated) {
                SetupLoginStatus(); // Setup login status

                // Pass login status to client side
                SetClientVar("login", LoginStatus);

                // Global Page Rendering event
                PageRendering();

                // Page Render event
                trRequestView?.PageRender();
            }
            return PageResult();
        }
        #pragma warning restore 168, 219

        // Set up other options
        #pragma warning disable 168, 219

        public void SetupOtherOptions()
        {
            var options = OtherOptions;
            var option = options["action"];
            ListOption item;
            string links = "";

            // Add
            item = option.Add("add");
            string addTitle = Language.Phrase("ViewPageAddLink", true);
            if (IsModal) // Modal
                item.Body = "<a class=\"ew-action ew-add\" title=\"" + addTitle + "\" data-caption=\"" + addTitle + "\" data-ew-action=\"modal\" data-url=\"" + HtmlEncode(AppPath(AddUrl)) + "\">" + Language.Phrase("ViewPageAddLink") + "</a>";
            else
                item.Body = "<a class=\"ew-action ew-add\" title=\"" + addTitle + "\" data-caption=\"" + addTitle + "\" href=\"" + HtmlEncode(AppPath(AddUrl)) + "\">" + Language.Phrase("ViewPageAddLink") + "</a>";
                item.Visible = AddUrl != "" && Security.CanAdd;

            // Edit
            item = option.Add("edit");
            var editTitle = Language.Phrase("ViewPageEditLink", true);
            if (IsModal) // Modal
                item.Body = "<a class=\"ew-action ew-edit\" title=\"" + editTitle + "\" data-caption=\"" + editTitle + "\" data-ew-action=\"modal\" data-url=\"" + HtmlEncode(AppPath(EditUrl)) + "\">" + Language.Phrase("ViewPageEditLink") + "</a>";
            else
                item.Body = "<a class=\"ew-action ew-edit\" title=\"" + editTitle + "\" data-caption=\"" + editTitle + "\" href=\"" + HtmlEncode(AppPath(EditUrl)) + "\">" + Language.Phrase("ViewPageEditLink") + "</a>";
                item.Visible = EditUrl != "" && Security.CanEdit;

            // Copy
            item = option.Add("copy");
            var copyTitle = Language.Phrase("ViewPageCopyLink", true);
            if (IsModal) // Modal
                item.Body = "<a class=\"ew-action ew-copy\" title=\"" + copyTitle + "\" data-caption=\"" + copyTitle + "\" data-ew-action=\"modal\" data-url=\"" + HtmlEncode(AppPath(CopyUrl)) + "\" data-btn=\"AddBtn\">" + Language.Phrase("ViewPageCopyLink") + "</a>";
            else
                item.Body = "<a class=\"ew-action ew-copy\" title=\"" + copyTitle + "\" data-caption=\"" + copyTitle + "\" href=\"" + HtmlEncode(AppPath(CopyUrl)) + "\">" + Language.Phrase("ViewPageCopyLink") + "</a>";
                item.Visible = CopyUrl != "" && Security.CanAdd;

            // Delete
            item = option.Add("delete");
            string url = AppPath(DeleteUrl);
            item.Body = "<a class=\"ew-action ew-delete\"" +
                (InlineDelete || IsModal ? " data-ew-action=\"inline-delete\"" : "") +
                " title=\"" + Language.Phrase("ViewPageDeleteLink", true) + "\" data-caption=\"" + Language.Phrase("ViewPageDeleteLink", true) +
                "\" href=\"" + HtmlEncode(url) + "\">" + Language.Phrase("ViewPageDeleteLink") + "</a>";
            item.Visible = DeleteUrl != "" && Security.CanDelete;
            string body = "";
            option = options["detail"];
            string detailTableLink = "";
            string detailViewTblVar = "";
            string detailCopyTblVar = "";
            string detailEditTblVar = "";
            dynamic? detailPage = null;
            string detailFilter = "";

            // "detail_tr_request_detail"
            item = option.Add("detail_tr_request_detail");
            body = Language.Phrase("ViewPageDetailLink") + Language.TablePhrase("tr_request_detail", "TblCaption");
            body = "<a class=\"btn btn-default btn-sm ew-row-link ew-detail\" data-action=\"list\" href=\"" + HtmlEncode(AppPath("TrRequestDetailList?" + Config.TableShowMaster + "=tr_request&" + ForeignKeyUrl("fk_id", id.CurrentValue) + "")) + "\">" + body + "</a>";
            links = "";
            detailPage = Resolve("TrRequestDetailGrid");
            if (detailPage?.DetailView && Security.CanView && Security.AllowView(CurrentProjectID + "tr_request") ?? false) {
                links += "<li><a class=\"dropdown-item ew-row-link ew-detail-view\" data-action=\"view\" data-caption=\"" + Language.Phrase("MasterDetailViewLink", true) + "\" href=\"" + HtmlEncode(AppPath(GetViewUrl(Config.TableShowDetail + "=tr_request_detail"))) + "\">" + Language.Phrase("MasterDetailViewLink", null) + "</a></li>";
                if (!Empty(detailViewTblVar))
                    detailViewTblVar += ",";
                detailViewTblVar += "tr_request_detail";
            }
            if (detailPage?.DetailEdit && Security.CanEdit && Security.AllowEdit(CurrentProjectID + "tr_request") ?? false) {
                links += "<li><a class=\"dropdown-item ew-row-link ew-detail-edit\" data-action=\"edit\" data-caption=\"" + Language.Phrase("MasterDetailEditLink", true) + "\" href=\"" + HtmlEncode(AppPath(GetEditUrl(Config.TableShowDetail + "=tr_request_detail"))) + "\">" + Language.Phrase("MasterDetailEditLink", null) + "</a></li>";
                if (!Empty(detailEditTblVar))
                    detailEditTblVar += ",";
                detailEditTblVar += "tr_request_detail";
            }
            if (detailPage?.DetailAdd && Security.CanAdd && Security.AllowAdd(CurrentProjectID + "tr_request") ?? false) {
                links += "<li><a class=\"dropdown-item ew-row-link ew-detail-copy\" data-action=\"add\" data-caption=\"" + Language.Phrase("MasterDetailCopyLink", true) + "\" href=\"" + HtmlEncode(AppPath(GetCopyUrl(Config.TableShowDetail + "=tr_request_detail"))) + "\">" + Language.Phrase("MasterDetailCopyLink", null) + "</a></li>";
                if (!Empty(detailCopyTblVar))
                    detailCopyTblVar += ",";
                detailCopyTblVar += "tr_request_detail";
            }
            if (!Empty(links)) {
                body += "<button type=\"button\" class=\"dropdown-toggle btn btn-default ew-detail\" data-bs-toggle=\"dropdown\"></button>";
                body += "<ul class=\"dropdown-menu\">" + links + "</ul>";
            } else {
                body = Regex.Replace(body, @"\b\s+dropdown-toggle\b", "");
            }
            body = "<div class=\"btn-group btn-group-sm ew-btn-group\">" + body + "</div>";
            item.Body = body;
            item.Visible = Security.AllowList(CurrentProjectID + "tr_request_detail");
            if (item.Visible) {
                if (!Empty(detailTableLink))
                    detailTableLink += ",";
                detailTableLink += "tr_request_detail";
            }
            if (ShowMultipleDetails)
                item.Visible = false;

            // "detail_tr_request_approver"
            item = option.Add("detail_tr_request_approver");
            body = Language.Phrase("ViewPageDetailLink") + Language.TablePhrase("tr_request_approver", "TblCaption");
            body = "<a class=\"btn btn-default btn-sm ew-row-link ew-detail\" data-action=\"list\" href=\"" + HtmlEncode(AppPath("TrRequestApproverList?" + Config.TableShowMaster + "=tr_request&" + ForeignKeyUrl("fk_id", id.CurrentValue) + "")) + "\">" + body + "</a>";
            links = "";
            detailPage = Resolve("TrRequestApproverGrid");
            if (detailPage?.DetailView && Security.CanView && Security.AllowView(CurrentProjectID + "tr_request") ?? false) {
                links += "<li><a class=\"dropdown-item ew-row-link ew-detail-view\" data-action=\"view\" data-caption=\"" + Language.Phrase("MasterDetailViewLink", true) + "\" href=\"" + HtmlEncode(AppPath(GetViewUrl(Config.TableShowDetail + "=tr_request_approver"))) + "\">" + Language.Phrase("MasterDetailViewLink", null) + "</a></li>";
                if (!Empty(detailViewTblVar))
                    detailViewTblVar += ",";
                detailViewTblVar += "tr_request_approver";
            }
            if (detailPage?.DetailEdit && Security.CanEdit && Security.AllowEdit(CurrentProjectID + "tr_request") ?? false) {
                links += "<li><a class=\"dropdown-item ew-row-link ew-detail-edit\" data-action=\"edit\" data-caption=\"" + Language.Phrase("MasterDetailEditLink", true) + "\" href=\"" + HtmlEncode(AppPath(GetEditUrl(Config.TableShowDetail + "=tr_request_approver"))) + "\">" + Language.Phrase("MasterDetailEditLink", null) + "</a></li>";
                if (!Empty(detailEditTblVar))
                    detailEditTblVar += ",";
                detailEditTblVar += "tr_request_approver";
            }
            if (detailPage?.DetailAdd && Security.CanAdd && Security.AllowAdd(CurrentProjectID + "tr_request") ?? false) {
                links += "<li><a class=\"dropdown-item ew-row-link ew-detail-copy\" data-action=\"add\" data-caption=\"" + Language.Phrase("MasterDetailCopyLink", true) + "\" href=\"" + HtmlEncode(AppPath(GetCopyUrl(Config.TableShowDetail + "=tr_request_approver"))) + "\">" + Language.Phrase("MasterDetailCopyLink", null) + "</a></li>";
                if (!Empty(detailCopyTblVar))
                    detailCopyTblVar += ",";
                detailCopyTblVar += "tr_request_approver";
            }
            if (!Empty(links)) {
                body += "<button type=\"button\" class=\"dropdown-toggle btn btn-default ew-detail\" data-bs-toggle=\"dropdown\"></button>";
                body += "<ul class=\"dropdown-menu\">" + links + "</ul>";
            } else {
                body = Regex.Replace(body, @"\b\s+dropdown-toggle\b", "");
            }
            body = "<div class=\"btn-group btn-group-sm ew-btn-group\">" + body + "</div>";
            item.Body = body;
            item.Visible = Security.AllowList(CurrentProjectID + "tr_request_approver");
            if (item.Visible) {
                if (!Empty(detailTableLink))
                    detailTableLink += ",";
                detailTableLink += "tr_request_approver";
            }
            if (ShowMultipleDetails)
                item.Visible = false;

            // "detail_tr_request_approval_history"
            item = option.Add("detail_tr_request_approval_history");
            body = Language.Phrase("ViewPageDetailLink") + Language.TablePhrase("tr_request_approval_history", "TblCaption");
            body = "<a class=\"btn btn-default btn-sm ew-row-link ew-detail\" data-action=\"list\" href=\"" + HtmlEncode(AppPath("TrRequestApprovalHistoryList?" + Config.TableShowMaster + "=tr_request&" + ForeignKeyUrl("fk_id", id.CurrentValue) + "")) + "\">" + body + "</a>";
            links = "";
            detailPage = Resolve("TrRequestApprovalHistoryGrid");
            if (detailPage?.DetailView && Security.CanView && Security.AllowView(CurrentProjectID + "tr_request") ?? false) {
                links += "<li><a class=\"dropdown-item ew-row-link ew-detail-view\" data-action=\"view\" data-caption=\"" + Language.Phrase("MasterDetailViewLink", true) + "\" href=\"" + HtmlEncode(AppPath(GetViewUrl(Config.TableShowDetail + "=tr_request_approval_history"))) + "\">" + Language.Phrase("MasterDetailViewLink", null) + "</a></li>";
                if (!Empty(detailViewTblVar))
                    detailViewTblVar += ",";
                detailViewTblVar += "tr_request_approval_history";
            }
            if (detailPage?.DetailEdit && Security.CanEdit && Security.AllowEdit(CurrentProjectID + "tr_request") ?? false) {
                links += "<li><a class=\"dropdown-item ew-row-link ew-detail-edit\" data-action=\"edit\" data-caption=\"" + Language.Phrase("MasterDetailEditLink", true) + "\" href=\"" + HtmlEncode(AppPath(GetEditUrl(Config.TableShowDetail + "=tr_request_approval_history"))) + "\">" + Language.Phrase("MasterDetailEditLink", null) + "</a></li>";
                if (!Empty(detailEditTblVar))
                    detailEditTblVar += ",";
                detailEditTblVar += "tr_request_approval_history";
            }
            if (detailPage?.DetailAdd && Security.CanAdd && Security.AllowAdd(CurrentProjectID + "tr_request") ?? false) {
                links += "<li><a class=\"dropdown-item ew-row-link ew-detail-copy\" data-action=\"add\" data-caption=\"" + Language.Phrase("MasterDetailCopyLink", true) + "\" href=\"" + HtmlEncode(AppPath(GetCopyUrl(Config.TableShowDetail + "=tr_request_approval_history"))) + "\">" + Language.Phrase("MasterDetailCopyLink", null) + "</a></li>";
                if (!Empty(detailCopyTblVar))
                    detailCopyTblVar += ",";
                detailCopyTblVar += "tr_request_approval_history";
            }
            if (!Empty(links)) {
                body += "<button type=\"button\" class=\"dropdown-toggle btn btn-default ew-detail\" data-bs-toggle=\"dropdown\"></button>";
                body += "<ul class=\"dropdown-menu\">" + links + "</ul>";
            } else {
                body = Regex.Replace(body, @"\b\s+dropdown-toggle\b", "");
            }
            body = "<div class=\"btn-group btn-group-sm ew-btn-group\">" + body + "</div>";
            item.Body = body;
            item.Visible = Security.AllowList(CurrentProjectID + "tr_request_approval_history");
            if (item.Visible) {
                if (!Empty(detailTableLink))
                    detailTableLink += ",";
                detailTableLink += "tr_request_approval_history";
            }
            if (ShowMultipleDetails)
                item.Visible = false;

            // Multiple details
            if (ShowMultipleDetails) {
                body = "<div class=\"btn-group btn-group-sm ew-btn-group\">";
                links = "";
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
                    body += "<button type=\"button\" class=\"dropdown-toggle btn btn-default btn-sm ew-master-detail\" title=\"" + HtmlEncode(Language.Phrase("MultipleMasterDetails", true)) + "\" data-bs-toggle=\"dropdown\">" + Language.Phrase("MultipleMasterDetails") + "</button>";
                    body += "<ul class=\"dropdown-menu ew-dropdown-menu\">" + links + "</ul>";
                }
                body += "</div>";
                // Multiple details
                item = option.Add("details");
                item.Body = body;
            }

            // Set up detail default
            option = options["detail"];
            options["detail"].DropDownButtonPhrase = "ButtonDetails";
            var ar = detailTableLink.Split(',');
            option.UseDropDownButton = (ar.Length > 1);
            option.UseButtonGroup = true;
            item = option.AddGroupOption();
            item.Body = "";
            item.Visible = false;

            // Set up action default
            option = options["action"];
            option.DropDownButtonPhrase = "ButtonActions";
            option.UseDropDownButton = !IsJsonResponse() && true;
            option.UseButtonGroup = true;
            item = option.AddGroupOption();
            item.Body = "";
            item.Visible = false;
        }
        #pragma warning restore 168, 219

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
            SetupOtherOptions();

            // Call Row Rendering event
            RowRendering();

            // Common render codes for all row types

            // id

            // Request_No

            // Reference_Doc

            // Reason

            // Request_By

            // Request_By_Name

            // Request_By_Position

            // Request_By_Branch

            // Request_By_Region

            // Request_Industry

            // Customer_ID

            // Customer_Name

            // SAP_Total

            // Override_Total

            // Variance_Total

            // Variance_Percent

            // Notes

            // Next_Approver

            // Request_PIC

            // Request_Status

            // List_Approver

            // Last_Update_By

            // Created_By

            // Created_Date

            // ETL_Date

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

        // Get export HTML tag
        protected string GetExportTag(string type, bool custom = false) {
            string exportUrl = AppPath(CurrentPageName()); // DN
            if (type == "print" || custom) { // Printer friendly / custom export
                List<string> keys = GetKey(true).Split(Config.CompositeKeySeparator).ToList();
                foreach (string key in keys)
                    exportUrl += "/" + UrlEncode(key);
                exportUrl += "?export=" + type + (custom ? "&amp;custom=1" : "");
            } else {
                exportUrl = AppPath(Config.ApiUrl + Config.ApiExportAction + "/" + type + "/" + TableVar);
                exportUrl += "?" + Config.ApiKeyName + "=" + GetKey(true);
            }
            if (SameText(type, "excel")) {
                if (custom)
                    return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-excel\" title=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\" form=\"ftr_requestview\" data-url=\"" + exportUrl + "\" data-ew-action=\"export\" data-export=\"excel\" data-custom=\"true\" data-export-selected=\"false\">" + Language.Phrase("ExportToExcel") + "</button>";
                else
                    return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-excel\" title=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\">" + Language.Phrase("ExportToExcel") + "</a>";
            } else if (SameText(type, "word")) {
                if (custom)
                    return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-word\" title=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\" form=\"ftr_requestview\" data-url=\"" + exportUrl + "\" data-ew-action=\"export\" data-export=\"word\" data-custom=\"true\" data-export-selected=\"false\">" + Language.Phrase("ExportToWord") + "</button>";
                else
                    return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-word\" title=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\">" + Language.Phrase("ExportToWord") + "</a>";
            } else if (SameText(type, "pdf")) {
                if (custom)
                    return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-pdf\" title=\"" + HtmlEncode(Language.Phrase("ExportToPdf", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToPdf", true)) + "\" form=\"ftr_requestview\" data-url=\"" + exportUrl + "\" data-ew-action=\"export\" data-export=\"pdf\" data-custom=\"true\" data-export-selected=\"false\">" + Language.Phrase("ExportToPDF") + "</button>";
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
                return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-email\" title=\"" + Language.Phrase("ExportToEmail", true) + "\" data-caption=\"" + Language.Phrase("ExportToEmail", true) + "\" form=\"ftr_requestview\" data-ew-action=\"email\" data-hdr=\"" + Language.Phrase("ExportToEmail", true) + "\" data-key='" + HtmlEncode(ConvertToJsonAttribute(RecordKeys)) + "' data-export-selected=\"false\"" + url + ">" + Language.Phrase("ExportToEmail") + "</button>";
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

            // Hide options for export
            if (IsExport())
                ExportOptions.HideAllOptions();

            // Hide options if json response
            if (IsJsonResponse())
                ExportOptions.HideAllOptions();
            if (!Security.CanExport) // Export not allowed
                ExportOptions.HideAllOptions();
        }

        #pragma warning disable 168

        /// <summary>
        /// Export data
        /// </summary>
        public async Task ExportData(dynamic? doc, string[] keys)
        {
            // Load recordset // DN
            DbDataReader? dr = null;
            if (TotalRecords < 0)
                TotalRecords = await ListRecordCountAsync();
            StartRecord = 1;
            if (keys.Length >= 1) {
                id.OldValue = keys[0];
                var c = await GetConnection2Async(DbId); // Note: Use new connection for view page export // DN
                dr = await LoadReader(GetRecordFilter(), "", c);
            }
            if (doc == null) { // DN
                RemoveHeader("Content-Type"); // Remove header
                RemoveHeader("Content-Disposition");
                FailureMessage = Language.Phrase("ExportClassNotFound"); // Export class not found
                return;
            }

            // Call Page Exporting server event
            doc.ExportCustom = !PageExporting(ref doc);
            string exportStyle;

            // Page header
            string header = PageHeader;
            PageDataRendering(ref header);
            doc.Text.Append(header);

            // Export
            if (dr != null)
                await ExportDocument(doc, dr, StartRecord, StopRecord, "view");

            // Export detail records (tr_request_detail)
            if (Config.ExportDetailRecords && CurrentDetailTables.Contains("tr_request_detail")) {
                trRequestDetail = new TrRequestDetailList();
                if (trRequestDetail != null) {
                    var c = await GetConnection2Async(trRequestDetail.DbId); // Note: Use new connection for detail records // DN
                    using var rsdetail = await trRequestDetail.LoadReader(trRequestDetail.DetailFilterFromSession, trRequestDetail.SessionOrderBy, c); // Load detail records
                    if (rsdetail?.HasRows ?? false) { // DN
                        exportStyle = doc.Style;
                        doc.SetStyle("h"); // Change to horizontal
                        if (Export != "csv" || Config.ExportDetailRecordsForCsv) {
                            doc.ExportEmptyRow();
                            int detailcnt = await trRequestDetail.LoadRecordCountAsync(trRequestDetail.DetailFilterFromSession); // DN
                            var oldtbl = doc.Table;
                            doc.Table = trRequestDetail;
                            await trRequestDetail.ExportDocument(doc, rsdetail, 1, detailcnt);
                            doc.Table = oldtbl;
                        }
                        doc.SetStyle(exportStyle); // Restore
                    }
                }
            }

            // Export detail records (tr_request_approver)
            if (Config.ExportDetailRecords && CurrentDetailTables.Contains("tr_request_approver")) {
                trRequestApprover = new TrRequestApproverList();
                if (trRequestApprover != null) {
                    var c = await GetConnection2Async(trRequestApprover.DbId); // Note: Use new connection for detail records // DN
                    using var rsdetail = await trRequestApprover.LoadReader(trRequestApprover.DetailFilterFromSession, trRequestApprover.SessionOrderBy, c); // Load detail records
                    if (rsdetail?.HasRows ?? false) { // DN
                        exportStyle = doc.Style;
                        doc.SetStyle("h"); // Change to horizontal
                        if (Export != "csv" || Config.ExportDetailRecordsForCsv) {
                            doc.ExportEmptyRow();
                            int detailcnt = await trRequestApprover.LoadRecordCountAsync(trRequestApprover.DetailFilterFromSession); // DN
                            var oldtbl = doc.Table;
                            doc.Table = trRequestApprover;
                            await trRequestApprover.ExportDocument(doc, rsdetail, 1, detailcnt);
                            doc.Table = oldtbl;
                        }
                        doc.SetStyle(exportStyle); // Restore
                    }
                }
            }

            // Export detail records (tr_request_approval_history)
            if (Config.ExportDetailRecords && CurrentDetailTables.Contains("tr_request_approval_history")) {
                trRequestApprovalHistory = new TrRequestApprovalHistoryList();
                if (trRequestApprovalHistory != null) {
                    var c = await GetConnection2Async(trRequestApprovalHistory.DbId); // Note: Use new connection for detail records // DN
                    using var rsdetail = await trRequestApprovalHistory.LoadReader(trRequestApprovalHistory.DetailFilterFromSession, trRequestApprovalHistory.SessionOrderBy, c); // Load detail records
                    if (rsdetail?.HasRows ?? false) { // DN
                        exportStyle = doc.Style;
                        doc.SetStyle("h"); // Change to horizontal
                        if (Export != "csv" || Config.ExportDetailRecordsForCsv) {
                            doc.ExportEmptyRow();
                            int detailcnt = await trRequestApprovalHistory.LoadRecordCountAsync(trRequestApprovalHistory.DetailFilterFromSession); // DN
                            var oldtbl = doc.Table;
                            doc.Table = trRequestApprovalHistory;
                            await trRequestApprovalHistory.ExportDocument(doc, rsdetail, 1, detailcnt);
                            doc.Table = oldtbl;
                        }
                        doc.SetStyle(exportStyle); // Restore
                    }
                }
            }

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
                    if (trRequestDetailGrid?.DetailView ?? false) {
                        trRequestDetailGrid.CurrentMode = "view";

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
                    if (trRequestApproverGrid?.DetailView ?? false) {
                        trRequestApproverGrid.CurrentMode = "view";

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
                    if (trRequestApprovalHistoryGrid?.DetailView ?? false) {
                        trRequestApprovalHistoryGrid.CurrentMode = "view";

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
            string pageId = "view";
            breadcrumb.Add("view", pageId, url);
            CurrentBreadcrumb = breadcrumb;
        }

        // Set up multi pages
        protected void SetupMultiPages() {
            var pages = new SubPages();
            pages.Style = "tabs";
            pages.Add(0);
            pages.Add(1);
            pages.Add(2);
            pages.Add(3);
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
    } // End page class
} // End Partial class
