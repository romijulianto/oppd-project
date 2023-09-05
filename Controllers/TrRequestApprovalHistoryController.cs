namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("TrRequestApprovalHistoryList/{id?}", Name = "TrRequestApprovalHistoryList-tr_request_approval_history-list")]
    [Route("Home/TrRequestApprovalHistoryList/{id?}", Name = "TrRequestApprovalHistoryList-tr_request_approval_history-list-2")]
    public async Task<IActionResult> TrRequestApprovalHistoryList()
    {
        // Create page object
        trRequestApprovalHistoryList = new GLOBALS.TrRequestApprovalHistoryList(this);
        trRequestApprovalHistoryList.Cache = _cache;

        // Run the page
        return await trRequestApprovalHistoryList.Run();
    }

    // view
    [Route("TrRequestApprovalHistoryView/{id?}", Name = "TrRequestApprovalHistoryView-tr_request_approval_history-view")]
    [Route("Home/TrRequestApprovalHistoryView/{id?}", Name = "TrRequestApprovalHistoryView-tr_request_approval_history-view-2")]
    public async Task<IActionResult> TrRequestApprovalHistoryView()
    {
        // Create page object
        trRequestApprovalHistoryView = new GLOBALS.TrRequestApprovalHistoryView(this);

        // Run the page
        return await trRequestApprovalHistoryView.Run();
    }

    // preview
    [Route("TrRequestApprovalHistoryPreview", Name = "TrRequestApprovalHistoryPreview-tr_request_approval_history-preview")]
    [Route("Home/TrRequestApprovalHistoryPreview", Name = "TrRequestApprovalHistoryPreview-tr_request_approval_history-preview-2")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Private, NoStore = true, MaxAge = 0)]
    public async Task<IActionResult> TrRequestApprovalHistoryPreview()
    {
        // Create page object
        trRequestApprovalHistoryPreview = new GLOBALS.TrRequestApprovalHistoryPreview(this);

        // Run the page
        return await trRequestApprovalHistoryPreview.Run();
    }
}
