namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("LogApiList/{id?}", Name = "LogApiList-log_API-list")]
    [Route("Home/LogApiList/{id?}", Name = "LogApiList-log_API-list-2")]
    public async Task<IActionResult> LogApiList()
    {
        // Create page object
        logApiList = new GLOBALS.LogApiList(this);
        logApiList.Cache = _cache;

        // Run the page
        return await logApiList.Run();
    }

    // add
    [Route("LogApiAdd/{id?}", Name = "LogApiAdd-log_API-add")]
    [Route("Home/LogApiAdd/{id?}", Name = "LogApiAdd-log_API-add-2")]
    public async Task<IActionResult> LogApiAdd()
    {
        // Create page object
        logApiAdd = new GLOBALS.LogApiAdd(this);

        // Run the page
        return await logApiAdd.Run();
    }

    // view
    [Route("LogApiView/{id?}", Name = "LogApiView-log_API-view")]
    [Route("Home/LogApiView/{id?}", Name = "LogApiView-log_API-view-2")]
    public async Task<IActionResult> LogApiView()
    {
        // Create page object
        logApiView = new GLOBALS.LogApiView(this);

        // Run the page
        return await logApiView.Run();
    }

    // edit
    [Route("LogApiEdit/{id?}", Name = "LogApiEdit-log_API-edit")]
    [Route("Home/LogApiEdit/{id?}", Name = "LogApiEdit-log_API-edit-2")]
    public async Task<IActionResult> LogApiEdit()
    {
        // Create page object
        logApiEdit = new GLOBALS.LogApiEdit(this);

        // Run the page
        return await logApiEdit.Run();
    }

    // delete
    [Route("LogApiDelete/{id?}", Name = "LogApiDelete-log_API-delete")]
    [Route("Home/LogApiDelete/{id?}", Name = "LogApiDelete-log_API-delete-2")]
    public async Task<IActionResult> LogApiDelete()
    {
        // Create page object
        logApiDelete = new GLOBALS.LogApiDelete(this);

        // Run the page
        return await logApiDelete.Run();
    }
}
