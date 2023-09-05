namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("TrRequestApproverList/{id?}", Name = "TrRequestApproverList-tr_request_approver-list")]
    [Route("Home/TrRequestApproverList/{id?}", Name = "TrRequestApproverList-tr_request_approver-list-2")]
    public async Task<IActionResult> TrRequestApproverList()
    {
        // Create page object
        trRequestApproverList = new GLOBALS.TrRequestApproverList(this);
        trRequestApproverList.Cache = _cache;

        // Run the page
        return await trRequestApproverList.Run();
    }

    // view
    [Route("TrRequestApproverView/{id?}", Name = "TrRequestApproverView-tr_request_approver-view")]
    [Route("Home/TrRequestApproverView/{id?}", Name = "TrRequestApproverView-tr_request_approver-view-2")]
    public async Task<IActionResult> TrRequestApproverView()
    {
        // Create page object
        trRequestApproverView = new GLOBALS.TrRequestApproverView(this);

        // Run the page
        return await trRequestApproverView.Run();
    }

    // preview
    [Route("TrRequestApproverPreview", Name = "TrRequestApproverPreview-tr_request_approver-preview")]
    [Route("Home/TrRequestApproverPreview", Name = "TrRequestApproverPreview-tr_request_approver-preview-2")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Private, NoStore = true, MaxAge = 0)]
    public async Task<IActionResult> TrRequestApproverPreview()
    {
        // Create page object
        trRequestApproverPreview = new GLOBALS.TrRequestApproverPreview(this);

        // Run the page
        return await trRequestApproverPreview.Run();
    }
}
