namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("VTrRequestList/{id?}", Name = "VTrRequestList-v_tr_request-list")]
    [Route("Home/VTrRequestList/{id?}", Name = "VTrRequestList-v_tr_request-list-2")]
    public async Task<IActionResult> VTrRequestList()
    {
        // Create page object
        vTrRequestList = new GLOBALS.VTrRequestList(this);
        vTrRequestList.Cache = _cache;

        // Run the page
        return await vTrRequestList.Run();
    }

    // view
    [Route("VTrRequestView/{id?}", Name = "VTrRequestView-v_tr_request-view")]
    [Route("Home/VTrRequestView/{id?}", Name = "VTrRequestView-v_tr_request-view-2")]
    public async Task<IActionResult> VTrRequestView()
    {
        // Create page object
        vTrRequestView = new GLOBALS.VTrRequestView(this);

        // Run the page
        return await vTrRequestView.Run();
    }

    // search
    [Route("VTrRequestSearch", Name = "VTrRequestSearch-v_tr_request-search")]
    [Route("Home/VTrRequestSearch", Name = "VTrRequestSearch-v_tr_request-search-2")]
    public async Task<IActionResult> VTrRequestSearch()
    {
        // Create page object
        vTrRequestSearch = new GLOBALS.VTrRequestSearch(this);

        // Run the page
        return await vTrRequestSearch.Run();
    }

    // query
    [Route("VTrRequestQuery", Name = "VTrRequestQuery-v_tr_request-query")]
    [Route("Home/VTrRequestQuery", Name = "VTrRequestQuery-v_tr_request-query-2")]
    public async Task<IActionResult> VTrRequestQuery()
    {
        // Create page object
        vTrRequestSearch = new GLOBALS.VTrRequestSearch(this);

        // Run the page
        return await vTrRequestSearch.Run();
    }
}
