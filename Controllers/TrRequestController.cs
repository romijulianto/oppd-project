namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("TrRequestList/{id?}", Name = "TrRequestList-tr_request-list")]
    [Route("Home/TrRequestList/{id?}", Name = "TrRequestList-tr_request-list-2")]
    public async Task<IActionResult> TrRequestList()
    {
        // Create page object
        trRequestList = new GLOBALS.TrRequestList(this);
        trRequestList.Cache = _cache;

        // Run the page
        return await trRequestList.Run();
    }

    // add
    [Route("TrRequestAdd/{id?}", Name = "TrRequestAdd-tr_request-add")]
    [Route("Home/TrRequestAdd/{id?}", Name = "TrRequestAdd-tr_request-add-2")]
    public async Task<IActionResult> TrRequestAdd()
    {
        // Create page object
        trRequestAdd = new GLOBALS.TrRequestAdd(this);

        // Run the page
        return await trRequestAdd.Run();
    }

    // view
    [Route("TrRequestView/{id?}", Name = "TrRequestView-tr_request-view")]
    [Route("Home/TrRequestView/{id?}", Name = "TrRequestView-tr_request-view-2")]
    public async Task<IActionResult> TrRequestView()
    {
        // Create page object
        trRequestView = new GLOBALS.TrRequestView(this);

        // Run the page
        return await trRequestView.Run();
    }

    // edit
    [Route("TrRequestEdit/{id?}", Name = "TrRequestEdit-tr_request-edit")]
    [Route("Home/TrRequestEdit/{id?}", Name = "TrRequestEdit-tr_request-edit-2")]
    public async Task<IActionResult> TrRequestEdit()
    {
        // Create page object
        trRequestEdit = new GLOBALS.TrRequestEdit(this);

        // Run the page
        return await trRequestEdit.Run();
    }

    // update
    [Route("TrRequestUpdate", Name = "TrRequestUpdate-tr_request-update")]
    [Route("Home/TrRequestUpdate", Name = "TrRequestUpdate-tr_request-update-2")]
    public async Task<IActionResult> TrRequestUpdate()
    {
        // Create page object
        trRequestUpdate = new GLOBALS.TrRequestUpdate(this);

        // Run the page
        return await trRequestUpdate.Run();
    }

    // delete
    [Route("TrRequestDelete/{id?}", Name = "TrRequestDelete-tr_request-delete")]
    [Route("Home/TrRequestDelete/{id?}", Name = "TrRequestDelete-tr_request-delete-2")]
    public async Task<IActionResult> TrRequestDelete()
    {
        // Create page object
        trRequestDelete = new GLOBALS.TrRequestDelete(this);

        // Run the page
        return await trRequestDelete.Run();
    }

    // search
    [Route("TrRequestSearch", Name = "TrRequestSearch-tr_request-search")]
    [Route("Home/TrRequestSearch", Name = "TrRequestSearch-tr_request-search-2")]
    public async Task<IActionResult> TrRequestSearch()
    {
        // Create page object
        trRequestSearch = new GLOBALS.TrRequestSearch(this);

        // Run the page
        return await trRequestSearch.Run();
    }

    // query
    [Route("TrRequestQuery", Name = "TrRequestQuery-tr_request-query")]
    [Route("Home/TrRequestQuery", Name = "TrRequestQuery-tr_request-query-2")]
    public async Task<IActionResult> TrRequestQuery()
    {
        // Create page object
        trRequestSearch = new GLOBALS.TrRequestSearch(this);

        // Run the page
        return await trRequestSearch.Run();
    }
}
