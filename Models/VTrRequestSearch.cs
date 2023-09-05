namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// vTrRequestSearch
    /// </summary>
    public static VTrRequestSearch vTrRequestSearch
    {
        get => HttpData.Get<VTrRequestSearch>("vTrRequestSearch")!;
        set => HttpData["vTrRequestSearch"] = value;
    }

    /// <summary>
    /// Page class for v_tr_request
    /// </summary>
    public class VTrRequestSearch : VTrRequestSearchBase
    {
        // Constructor
        public VTrRequestSearch(Controller controller) : base(controller)
        {
        }

        // Constructor
        public VTrRequestSearch() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class VTrRequestSearchBase : VTrRequest
    {
        // Page ID
        public string PageID = "search";

        // Project ID
        public string ProjectID = "{74850334-D87A-4E71-B45A-50C64B48A90E}";

        // Table name
        public string TableName { get; set; } = "v_tr_request";

        // Page object name
        public string PageObjName = "vTrRequestSearch";

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
        public VTrRequestSearchBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-desktop-table ew-search-table";

            // Language object
            Language = ResolveLanguage();

            // Table object (vTrRequest)
            if (vTrRequest == null || vTrRequest is VTrRequest)
                vTrRequest = this;

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
        public string PageName => "VTrRequestSearch";

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
            List_Approver.SetVisibility();
            Last_Update_By.SetVisibility();
            Created_By.SetVisibility();
            Created_Date.SetVisibility();
            ETL_Date.SetVisibility();
            Request_Status.SetVisibility();
            Request_PIC.SetVisibility();
        }

        // Constructor
        public VTrRequestSearchBase(Controller? controller = null): this() { // DN
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

        public string FormClassName = "ew-form ew-search-form";

        public bool IsModal = false;

        public bool IsMobileOrModal = false;

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

            // Set up Breadcrumb
            SetupBreadcrumb();

            // Check modal
            if (IsModal)
                SkipHeaderFooter = true;
            IsMobileOrModal = IsMobile() || IsModal;

            // Get action
            CurrentAction = CurrentForm.GetValue("action");
            if (IsSearch) {
                // Build search string for advanced search, remove blank field
                LoadSearchValues(); // Get search values
                string srchStr = ValidateSearch() ? BuildAdvancedSearch() : "";
                if (!Empty(srchStr)) {
                    srchStr = "VTrRequestList?" + srchStr;
                    // Do not return Json for UseAjaxActions
                    if (IsModal && UseAjaxActions)
                        IsModal = false;
                    return Terminate(srchStr); // Go to List page
                }
            }

            // Restore search settings from Session
            if (!HasInvalidFields())
                LoadAdvancedSearch();

            // Render row for search
            RowType = RowType.Search;
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
                vTrRequestSearch?.PageRender();
            }
            return PageResult();
        }

        // Build advanced search
        protected string BuildAdvancedSearch() {
            string srchUrl = "";
            BuildSearchUrl(ref srchUrl, id); // id
            BuildSearchUrl(ref srchUrl, Request_No); // Request_No
            BuildSearchUrl(ref srchUrl, Reference_Doc); // Reference_Doc
            BuildSearchUrl(ref srchUrl, Reason); // Reason
            BuildSearchUrl(ref srchUrl, Request_By); // Request_By
            BuildSearchUrl(ref srchUrl, Request_By_Name); // Request_By_Name
            BuildSearchUrl(ref srchUrl, Request_By_Position); // Request_By_Position
            BuildSearchUrl(ref srchUrl, Request_By_Branch); // Request_By_Branch
            BuildSearchUrl(ref srchUrl, Request_By_Region); // Request_By_Region
            BuildSearchUrl(ref srchUrl, Request_Industry); // Request_Industry
            BuildSearchUrl(ref srchUrl, Customer_ID); // Customer_ID
            BuildSearchUrl(ref srchUrl, Customer_Name); // Customer_Name
            BuildSearchUrl(ref srchUrl, SAP_Total); // SAP_Total
            BuildSearchUrl(ref srchUrl, Override_Total); // Override_Total
            BuildSearchUrl(ref srchUrl, Variance_Total); // Variance_Total
            BuildSearchUrl(ref srchUrl, Variance_Percent); // Variance_Percent
            BuildSearchUrl(ref srchUrl, Notes); // Notes
            BuildSearchUrl(ref srchUrl, Next_Approver); // Next_Approver
            BuildSearchUrl(ref srchUrl, List_Approver); // List_Approver
            BuildSearchUrl(ref srchUrl, Last_Update_By); // Last_Update_By
            BuildSearchUrl(ref srchUrl, Created_By); // Created_By
            BuildSearchUrl(ref srchUrl, Created_Date); // Created_Date
            BuildSearchUrl(ref srchUrl, ETL_Date); // ETL_Date
            BuildSearchUrl(ref srchUrl, Request_Status); // Request_Status
            BuildSearchUrl(ref srchUrl, Request_PIC); // Request_PIC
            if (!Empty(srchUrl))
                srchUrl += "&";
            srchUrl += "cmd=search";
            return srchUrl;
        }

        // Build search URL
        protected void BuildSearchUrl(ref string url, DbField fld, bool oprOnly = false) {
            bool isValid;
            string wrk = "";
            string fldParm = fld.Param;
            string ctl = "x_" + fldParm;
            string ctl2 = "y_" + fldParm;
            if (fld.IsMultiSelect) { // DN
                ctl += "[]";
                ctl2 += "[]";
            }
            string fldVal = CurrentForm.GetValue(ctl);
            string fldOpr = CurrentForm.GetValue("z_" + fldParm);
            string fldCond = CurrentForm.GetValue("v_" + fldParm);
            string fldVal2 = CurrentForm.GetValue(ctl2);
            string fldOpr2 = CurrentForm.GetValue("w_" + fldParm);
            DataType fldDataType = fld.IsVirtual ? DataType.String : fld.DataType;
            fldVal = ConvertSearchValue(fldVal, fldOpr, fld); // For testing if numeric only
            fldVal2 = ConvertSearchValue(fldVal2, fldOpr2, fld); // For testing if numeric only
            fldOpr = ConvertSearchOperator(fldOpr, fld, fldVal);
            fldOpr2 = ConvertSearchOperator(fldOpr2, fld, fldVal2);
            if ((new [] { "BETWEEN", "NOT BETWEEN" }).Contains(fldOpr)) {
                isValid = fldDataType != DataType.Number || fld.VirtualSearch || IsNumericSearchValue(fldVal, fldOpr, fld) && IsNumericSearchValue(fldVal2, fldOpr2, fld);
                if (!Empty(fldVal) && !Empty(fldVal2) && isValid)
                    wrk = ctl + "=" + UrlEncode(fldVal) + "&" + ctl2 + "=" + UrlEncode(fldVal2) + "&z_" + fldParm + "=" + UrlEncode(fldOpr);
            } else {
                isValid = fldDataType != DataType.Number || fld.VirtualSearch || IsNumericSearchValue(fldVal, fldOpr, fld);
                if (!Empty(fldVal) && isValid && IsValidOperator(fldOpr)) {
                    wrk = ctl + "=" + UrlEncode(fldVal) + "&z_" + fldParm + "=" + UrlEncode(fldOpr);
                } else if ((new [] { "IS NULL", "IS NOT NULL", "IS EMPTY", "IS NOT EMPTY" }).Contains(fldOpr) || !Empty(fldOpr) && oprOnly && IsValidOperator(fldOpr)) {
                    wrk = "z_" + fldParm + "=" + UrlEncode(fldOpr);
                }
                isValid = fldDataType != DataType.Number || fld.VirtualSearch || IsNumericSearchValue(fldVal2, fldOpr2, fld);
                if (!Empty(fldVal2) && isValid && IsValidOperator(fldOpr2)) {
                    if (!Empty(wrk))
                        wrk += "&v_" + fldParm + "=" + fldCond + "&";
                    wrk += ctl2 + "=" + UrlEncode(fldVal2) + "&w_" + fldParm + "=" + UrlEncode(fldOpr2);
                } else if ((new [] { "IS NULL", "IS NOT NULL", "IS EMPTY", "IS NOT EMPTY" }).Contains(fldOpr2) || !Empty(fldOpr2) && oprOnly && IsValidOperator(fldOpr2)) {
                    if (!Empty(wrk))
                        wrk += "&v_" + fldParm + "=" + UrlEncode(fldCond) + "&";
                    wrk += "w_" + fldParm + "=" + UrlEncode(fldOpr2);
                }
            }
            if (!Empty(wrk)) {
                if (!Empty(url))
                    url += "&";
                url += wrk;
            }
        }

        // Load search values for validation // DN
        protected void LoadSearchValues() {
            // id
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_id"))
                    id.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_id");
            if (Form.ContainsKey("z_id"))
                id.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_id");

            // Request_No
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Request_No"))
                    Request_No.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Request_No");
            if (Form.ContainsKey("z_Request_No"))
                Request_No.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Request_No");

            // Reference_Doc
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Reference_Doc"))
                    Reference_Doc.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Reference_Doc");
            if (Form.ContainsKey("z_Reference_Doc"))
                Reference_Doc.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Reference_Doc");

            // Reason
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Reason"))
                    Reason.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Reason");
            if (Form.ContainsKey("z_Reason"))
                Reason.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Reason");

            // Request_By
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Request_By"))
                    Request_By.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Request_By");
            if (Form.ContainsKey("z_Request_By"))
                Request_By.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Request_By");

            // Request_By_Name
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Request_By_Name"))
                    Request_By_Name.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Request_By_Name");
            if (Form.ContainsKey("z_Request_By_Name"))
                Request_By_Name.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Request_By_Name");

            // Request_By_Position
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Request_By_Position"))
                    Request_By_Position.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Request_By_Position");
            if (Form.ContainsKey("z_Request_By_Position"))
                Request_By_Position.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Request_By_Position");

            // Request_By_Branch
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Request_By_Branch"))
                    Request_By_Branch.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Request_By_Branch");
            if (Form.ContainsKey("z_Request_By_Branch"))
                Request_By_Branch.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Request_By_Branch");

            // Request_By_Region
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Request_By_Region"))
                    Request_By_Region.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Request_By_Region");
            if (Form.ContainsKey("z_Request_By_Region"))
                Request_By_Region.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Request_By_Region");

            // Request_Industry
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Request_Industry"))
                    Request_Industry.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Request_Industry");
            if (Form.ContainsKey("z_Request_Industry"))
                Request_Industry.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Request_Industry");

            // Customer_ID
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Customer_ID"))
                    Customer_ID.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Customer_ID");
            if (Form.ContainsKey("z_Customer_ID"))
                Customer_ID.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Customer_ID");

            // Customer_Name
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Customer_Name"))
                    Customer_Name.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Customer_Name");
            if (Form.ContainsKey("z_Customer_Name"))
                Customer_Name.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Customer_Name");

            // SAP_Total
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_SAP_Total"))
                    SAP_Total.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_SAP_Total");
            if (Form.ContainsKey("z_SAP_Total"))
                SAP_Total.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_SAP_Total");

            // Override_Total
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Override_Total"))
                    Override_Total.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Override_Total");
            if (Form.ContainsKey("z_Override_Total"))
                Override_Total.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Override_Total");

            // Variance_Total
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Variance_Total"))
                    Variance_Total.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Variance_Total");
            if (Form.ContainsKey("z_Variance_Total"))
                Variance_Total.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Variance_Total");

            // Variance_Percent
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Variance_Percent"))
                    Variance_Percent.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Variance_Percent");
            if (Form.ContainsKey("z_Variance_Percent"))
                Variance_Percent.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Variance_Percent");

            // Notes
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Notes"))
                    Notes.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Notes");
            if (Form.ContainsKey("z_Notes"))
                Notes.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Notes");

            // Next_Approver
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Next_Approver"))
                    Next_Approver.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Next_Approver");
            if (Form.ContainsKey("z_Next_Approver"))
                Next_Approver.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Next_Approver");

            // List_Approver
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_List_Approver"))
                    List_Approver.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_List_Approver");
            if (Form.ContainsKey("z_List_Approver"))
                List_Approver.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_List_Approver");

            // Last_Update_By
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Last_Update_By"))
                    Last_Update_By.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Last_Update_By");
            if (Form.ContainsKey("z_Last_Update_By"))
                Last_Update_By.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Last_Update_By");

            // Created_By
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Created_By"))
                    Created_By.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Created_By");
            if (Form.ContainsKey("z_Created_By"))
                Created_By.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Created_By");

            // Created_Date
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Created_Date"))
                    Created_Date.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Created_Date");
            if (Form.ContainsKey("z_Created_Date"))
                Created_Date.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Created_Date");

            // ETL_Date
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_ETL_Date"))
                    ETL_Date.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_ETL_Date");
            if (Form.ContainsKey("z_ETL_Date"))
                ETL_Date.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_ETL_Date");

            // Request_Status
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Request_Status"))
                    Request_Status.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Request_Status");
            if (Form.ContainsKey("z_Request_Status"))
                Request_Status.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Request_Status");

            // Request_PIC
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_Request_PIC"))
                    Request_PIC.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_Request_PIC");
            if (Form.ContainsKey("z_Request_PIC"))
                Request_PIC.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_Request_PIC");
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

            // Request_Status
            Request_Status.RowCssClass = "row";

            // Request_PIC
            Request_PIC.RowCssClass = "row";

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

                // ETL_Date
                ETL_Date.HrefValue = "";
                ETL_Date.TooltipValue = "";

                // Request_Status
                Request_Status.HrefValue = "";
                Request_Status.TooltipValue = "";

                // Request_PIC
                Request_PIC.HrefValue = "";
                Request_PIC.TooltipValue = "";
            } else if (RowType == RowType.Search) {
                // id
                id.SetupEditAttributes();
                id.EditValue = id.AdvancedSearch.SearchValue; // DN
                id.PlaceHolder = RemoveHtml(id.Caption);

                // Request_No
                Request_No.SetupEditAttributes();
                if (!Request_No.Raw)
                    Request_No.AdvancedSearch.SearchValue = HtmlDecode(Request_No.AdvancedSearch.SearchValue);
                Request_No.EditValue = HtmlEncode(Request_No.AdvancedSearch.SearchValue);
                Request_No.PlaceHolder = RemoveHtml(Request_No.Caption);

                // Reference_Doc
                Reference_Doc.SetupEditAttributes();
                if (!Reference_Doc.Raw)
                    Reference_Doc.AdvancedSearch.SearchValue = HtmlDecode(Reference_Doc.AdvancedSearch.SearchValue);
                Reference_Doc.EditValue = HtmlEncode(Reference_Doc.AdvancedSearch.SearchValue);
                Reference_Doc.PlaceHolder = RemoveHtml(Reference_Doc.Caption);

                // Reason
                Reason.SetupEditAttributes();
                if (!Reason.Raw)
                    Reason.AdvancedSearch.SearchValue = HtmlDecode(Reason.AdvancedSearch.SearchValue);
                Reason.EditValue = HtmlEncode(Reason.AdvancedSearch.SearchValue);
                Reason.PlaceHolder = RemoveHtml(Reason.Caption);

                // Request_By
                Request_By.SetupEditAttributes();
                if (!Request_By.Raw)
                    Request_By.AdvancedSearch.SearchValue = HtmlDecode(Request_By.AdvancedSearch.SearchValue);
                Request_By.EditValue = HtmlEncode(Request_By.AdvancedSearch.SearchValue);
                Request_By.PlaceHolder = RemoveHtml(Request_By.Caption);

                // Request_By_Name
                Request_By_Name.SetupEditAttributes();
                if (!Request_By_Name.Raw)
                    Request_By_Name.AdvancedSearch.SearchValue = HtmlDecode(Request_By_Name.AdvancedSearch.SearchValue);
                Request_By_Name.EditValue = HtmlEncode(Request_By_Name.AdvancedSearch.SearchValue);
                Request_By_Name.PlaceHolder = RemoveHtml(Request_By_Name.Caption);

                // Request_By_Position
                Request_By_Position.SetupEditAttributes();
                if (!Request_By_Position.Raw)
                    Request_By_Position.AdvancedSearch.SearchValue = HtmlDecode(Request_By_Position.AdvancedSearch.SearchValue);
                Request_By_Position.EditValue = HtmlEncode(Request_By_Position.AdvancedSearch.SearchValue);
                Request_By_Position.PlaceHolder = RemoveHtml(Request_By_Position.Caption);

                // Request_By_Branch
                Request_By_Branch.SetupEditAttributes();
                if (!Request_By_Branch.Raw)
                    Request_By_Branch.AdvancedSearch.SearchValue = HtmlDecode(Request_By_Branch.AdvancedSearch.SearchValue);
                Request_By_Branch.EditValue = HtmlEncode(Request_By_Branch.AdvancedSearch.SearchValue);
                Request_By_Branch.PlaceHolder = RemoveHtml(Request_By_Branch.Caption);

                // Request_By_Region
                Request_By_Region.SetupEditAttributes();
                if (!Request_By_Region.Raw)
                    Request_By_Region.AdvancedSearch.SearchValue = HtmlDecode(Request_By_Region.AdvancedSearch.SearchValue);
                Request_By_Region.EditValue = HtmlEncode(Request_By_Region.AdvancedSearch.SearchValue);
                Request_By_Region.PlaceHolder = RemoveHtml(Request_By_Region.Caption);

                // Request_Industry
                Request_Industry.SetupEditAttributes();
                if (!Request_Industry.Raw)
                    Request_Industry.AdvancedSearch.SearchValue = HtmlDecode(Request_Industry.AdvancedSearch.SearchValue);
                Request_Industry.EditValue = HtmlEncode(Request_Industry.AdvancedSearch.SearchValue);
                Request_Industry.PlaceHolder = RemoveHtml(Request_Industry.Caption);

                // Customer_ID
                Customer_ID.SetupEditAttributes();
                if (!Customer_ID.Raw)
                    Customer_ID.AdvancedSearch.SearchValue = HtmlDecode(Customer_ID.AdvancedSearch.SearchValue);
                Customer_ID.EditValue = HtmlEncode(Customer_ID.AdvancedSearch.SearchValue);
                Customer_ID.PlaceHolder = RemoveHtml(Customer_ID.Caption);

                // Customer_Name
                Customer_Name.SetupEditAttributes();
                if (!Customer_Name.Raw)
                    Customer_Name.AdvancedSearch.SearchValue = HtmlDecode(Customer_Name.AdvancedSearch.SearchValue);
                Customer_Name.EditValue = HtmlEncode(Customer_Name.AdvancedSearch.SearchValue);
                Customer_Name.PlaceHolder = RemoveHtml(Customer_Name.Caption);

                // SAP_Total
                SAP_Total.SetupEditAttributes();
                SAP_Total.EditValue = SAP_Total.AdvancedSearch.SearchValue; // DN
                SAP_Total.PlaceHolder = RemoveHtml(SAP_Total.Caption);

                // Override_Total
                Override_Total.SetupEditAttributes();
                Override_Total.EditValue = Override_Total.AdvancedSearch.SearchValue; // DN
                Override_Total.PlaceHolder = RemoveHtml(Override_Total.Caption);

                // Variance_Total
                Variance_Total.SetupEditAttributes();
                Variance_Total.EditValue = Variance_Total.AdvancedSearch.SearchValue; // DN
                Variance_Total.PlaceHolder = RemoveHtml(Variance_Total.Caption);

                // Variance_Percent
                Variance_Percent.SetupEditAttributes();
                Variance_Percent.EditValue = Variance_Percent.AdvancedSearch.SearchValue; // DN
                Variance_Percent.PlaceHolder = RemoveHtml(Variance_Percent.Caption);

                // Notes
                Notes.SetupEditAttributes();
                if (!Notes.Raw)
                    Notes.AdvancedSearch.SearchValue = HtmlDecode(Notes.AdvancedSearch.SearchValue);
                Notes.EditValue = HtmlEncode(Notes.AdvancedSearch.SearchValue);
                Notes.PlaceHolder = RemoveHtml(Notes.Caption);

                // Next_Approver
                Next_Approver.SetupEditAttributes();
                if (!Next_Approver.Raw)
                    Next_Approver.AdvancedSearch.SearchValue = HtmlDecode(Next_Approver.AdvancedSearch.SearchValue);
                Next_Approver.EditValue = HtmlEncode(Next_Approver.AdvancedSearch.SearchValue);
                Next_Approver.PlaceHolder = RemoveHtml(Next_Approver.Caption);

                // List_Approver
                List_Approver.SetupEditAttributes();
                if (!List_Approver.Raw)
                    List_Approver.AdvancedSearch.SearchValue = HtmlDecode(List_Approver.AdvancedSearch.SearchValue);
                List_Approver.EditValue = HtmlEncode(List_Approver.AdvancedSearch.SearchValue);
                List_Approver.PlaceHolder = RemoveHtml(List_Approver.Caption);

                // Last_Update_By
                Last_Update_By.SetupEditAttributes();
                if (!Last_Update_By.Raw)
                    Last_Update_By.AdvancedSearch.SearchValue = HtmlDecode(Last_Update_By.AdvancedSearch.SearchValue);
                Last_Update_By.EditValue = HtmlEncode(Last_Update_By.AdvancedSearch.SearchValue);
                Last_Update_By.PlaceHolder = RemoveHtml(Last_Update_By.Caption);

                // Created_By
                Created_By.SetupEditAttributes();
                if (!Created_By.Raw)
                    Created_By.AdvancedSearch.SearchValue = HtmlDecode(Created_By.AdvancedSearch.SearchValue);
                Created_By.EditValue = HtmlEncode(Created_By.AdvancedSearch.SearchValue);
                Created_By.PlaceHolder = RemoveHtml(Created_By.Caption);

                // Created_Date
                Created_Date.SetupEditAttributes();
                Created_Date.EditValue = FormatDateTime(UnformatDateTime(Created_Date.AdvancedSearch.SearchValue, Created_Date.FormatPattern), Created_Date.FormatPattern); // DN
                Created_Date.PlaceHolder = RemoveHtml(Created_Date.Caption);

                // ETL_Date
                ETL_Date.SetupEditAttributes();
                ETL_Date.EditValue = FormatDateTime(UnformatDateTime(ETL_Date.AdvancedSearch.SearchValue, ETL_Date.FormatPattern), ETL_Date.FormatPattern); // DN
                ETL_Date.PlaceHolder = RemoveHtml(ETL_Date.Caption);

                // Request_Status
                Request_Status.SetupEditAttributes();
                if (!Request_Status.Raw)
                    Request_Status.AdvancedSearch.SearchValue = HtmlDecode(Request_Status.AdvancedSearch.SearchValue);
                Request_Status.EditValue = HtmlEncode(Request_Status.AdvancedSearch.SearchValue);
                Request_Status.PlaceHolder = RemoveHtml(Request_Status.Caption);

                // Request_PIC
                Request_PIC.SetupEditAttributes();
                if (!Request_PIC.Raw)
                    Request_PIC.AdvancedSearch.SearchValue = HtmlDecode(Request_PIC.AdvancedSearch.SearchValue);
                Request_PIC.EditValue = HtmlEncode(Request_PIC.AdvancedSearch.SearchValue);
                Request_PIC.PlaceHolder = RemoveHtml(Request_PIC.Caption);
            }
            if (RowType == RowType.Add || RowType == RowType.Edit || RowType == RowType.Search) // Add/Edit/Search row
                SetupFieldTitles();

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
            if (!CheckInteger(ConvertToString(id.AdvancedSearch.SearchValue))) {
                id.AddErrorMessage(id.GetErrorMessage(false));
            }
            if (!CheckNumber(ConvertToString(SAP_Total.AdvancedSearch.SearchValue))) {
                SAP_Total.AddErrorMessage(SAP_Total.GetErrorMessage(false));
            }
            if (!CheckNumber(ConvertToString(Override_Total.AdvancedSearch.SearchValue))) {
                Override_Total.AddErrorMessage(Override_Total.GetErrorMessage(false));
            }
            if (!CheckNumber(ConvertToString(Variance_Total.AdvancedSearch.SearchValue))) {
                Variance_Total.AddErrorMessage(Variance_Total.GetErrorMessage(false));
            }
            if (!CheckNumber(ConvertToString(Variance_Percent.AdvancedSearch.SearchValue))) {
                Variance_Percent.AddErrorMessage(Variance_Percent.GetErrorMessage(false));
            }
            if (!CheckDate(ConvertToString(Created_Date.AdvancedSearch.SearchValue), Created_Date.FormatPattern)) {
                Created_Date.AddErrorMessage(Created_Date.GetErrorMessage(false));
            }
            if (!CheckDate(ConvertToString(ETL_Date.AdvancedSearch.SearchValue), ETL_Date.FormatPattern)) {
                ETL_Date.AddErrorMessage(ETL_Date.GetErrorMessage(false));
            }

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

        // Set up Breadcrumb
        protected void SetupBreadcrumb() {
            var breadcrumb = new Breadcrumb();
            string url = CurrentUrl();
            breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("VTrRequestList")), "", TableVar, true);
            string pageId = "search";
            breadcrumb.Add("search", pageId, url);
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
