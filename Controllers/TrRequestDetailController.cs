namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("TrRequestDetailList/{id?}", Name = "TrRequestDetailList-tr_request_detail-list")]
    [Route("Home/TrRequestDetailList/{id?}", Name = "TrRequestDetailList-tr_request_detail-list-2")]
    public async Task<IActionResult> TrRequestDetailList()
    {
        // Create page object
        trRequestDetailList = new GLOBALS.TrRequestDetailList(this);
        trRequestDetailList.Cache = _cache;

        // Run the page
        return await trRequestDetailList.Run();
    }

    // add
    [Route("TrRequestDetailAdd/{id?}", Name = "TrRequestDetailAdd-tr_request_detail-add")]
    [Route("Home/TrRequestDetailAdd/{id?}", Name = "TrRequestDetailAdd-tr_request_detail-add-2")]
    public async Task<IActionResult> TrRequestDetailAdd()
    {
        // Create page object
        trRequestDetailAdd = new GLOBALS.TrRequestDetailAdd(this);

        // Run the page
        return await trRequestDetailAdd.Run();
    }

    // view
    [Route("TrRequestDetailView/{id?}", Name = "TrRequestDetailView-tr_request_detail-view")]
    [Route("Home/TrRequestDetailView/{id?}", Name = "TrRequestDetailView-tr_request_detail-view-2")]
    public async Task<IActionResult> TrRequestDetailView()
    {
        // Create page object
        trRequestDetailView = new GLOBALS.TrRequestDetailView(this);

        // Run the page
        return await trRequestDetailView.Run();
    }

    // edit
    [Route("TrRequestDetailEdit/{id?}", Name = "TrRequestDetailEdit-tr_request_detail-edit")]
    [Route("Home/TrRequestDetailEdit/{id?}", Name = "TrRequestDetailEdit-tr_request_detail-edit-2")]
    public async Task<IActionResult> TrRequestDetailEdit()
    {
        // Create page object
        trRequestDetailEdit = new GLOBALS.TrRequestDetailEdit(this);

        // Run the page
        return await trRequestDetailEdit.Run();
    }

    // update
    [Route("TrRequestDetailUpdate", Name = "TrRequestDetailUpdate-tr_request_detail-update")]
    [Route("Home/TrRequestDetailUpdate", Name = "TrRequestDetailUpdate-tr_request_detail-update-2")]
    public async Task<IActionResult> TrRequestDetailUpdate()
    {
        // Create page object
        trRequestDetailUpdate = new GLOBALS.TrRequestDetailUpdate(this);

        // Run the page
        return await trRequestDetailUpdate.Run();
    }

    // delete
    [Route("TrRequestDetailDelete/{id?}", Name = "TrRequestDetailDelete-tr_request_detail-delete")]
    [Route("Home/TrRequestDetailDelete/{id?}", Name = "TrRequestDetailDelete-tr_request_detail-delete-2")]
    public async Task<IActionResult> TrRequestDetailDelete()
    {
        // Create page object
        trRequestDetailDelete = new GLOBALS.TrRequestDetailDelete(this);

        // Run the page
        return await trRequestDetailDelete.Run();
    }

    // preview
    [Route("TrRequestDetailPreview", Name = "TrRequestDetailPreview-tr_request_detail-preview")]
    [Route("Home/TrRequestDetailPreview", Name = "TrRequestDetailPreview-tr_request_detail-preview-2")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Private, NoStore = true, MaxAge = 0)]
    public async Task<IActionResult> TrRequestDetailPreview()
    {
        // Create page object
        trRequestDetailPreview = new GLOBALS.TrRequestDetailPreview(this);

        // Run the page
        return await trRequestDetailPreview.Run();
    }
}
