namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("LogEmailList/{id?}", Name = "LogEmailList-log_email-list")]
    [Route("Home/LogEmailList/{id?}", Name = "LogEmailList-log_email-list-2")]
    public async Task<IActionResult> LogEmailList()
    {
        // Create page object
        logEmailList = new GLOBALS.LogEmailList(this);
        logEmailList.Cache = _cache;

        // Run the page
        return await logEmailList.Run();
    }

    // add
    [Route("LogEmailAdd/{id?}", Name = "LogEmailAdd-log_email-add")]
    [Route("Home/LogEmailAdd/{id?}", Name = "LogEmailAdd-log_email-add-2")]
    public async Task<IActionResult> LogEmailAdd()
    {
        // Create page object
        logEmailAdd = new GLOBALS.LogEmailAdd(this);

        // Run the page
        return await logEmailAdd.Run();
    }

    // view
    [Route("LogEmailView/{id?}", Name = "LogEmailView-log_email-view")]
    [Route("Home/LogEmailView/{id?}", Name = "LogEmailView-log_email-view-2")]
    public async Task<IActionResult> LogEmailView()
    {
        // Create page object
        logEmailView = new GLOBALS.LogEmailView(this);

        // Run the page
        return await logEmailView.Run();
    }

    // edit
    [Route("LogEmailEdit/{id?}", Name = "LogEmailEdit-log_email-edit")]
    [Route("Home/LogEmailEdit/{id?}", Name = "LogEmailEdit-log_email-edit-2")]
    public async Task<IActionResult> LogEmailEdit()
    {
        // Create page object
        logEmailEdit = new GLOBALS.LogEmailEdit(this);

        // Run the page
        return await logEmailEdit.Run();
    }

    // delete
    [Route("LogEmailDelete/{id?}", Name = "LogEmailDelete-log_email-delete")]
    [Route("Home/LogEmailDelete/{id?}", Name = "LogEmailDelete-log_email-delete-2")]
    public async Task<IActionResult> LogEmailDelete()
    {
        // Create page object
        logEmailDelete = new GLOBALS.LogEmailDelete(this);

        // Run the page
        return await logEmailDelete.Run();
    }
}
