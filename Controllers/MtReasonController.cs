namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("MtReasonList/{id?}", Name = "MtReasonList-mt_reason-list")]
    [Route("Home/MtReasonList/{id?}", Name = "MtReasonList-mt_reason-list-2")]
    public async Task<IActionResult> MtReasonList()
    {
        // Create page object
        mtReasonList = new GLOBALS.MtReasonList(this);
        mtReasonList.Cache = _cache;

        // Run the page
        return await mtReasonList.Run();
    }

    // add
    [Route("MtReasonAdd/{id?}", Name = "MtReasonAdd-mt_reason-add")]
    [Route("Home/MtReasonAdd/{id?}", Name = "MtReasonAdd-mt_reason-add-2")]
    public async Task<IActionResult> MtReasonAdd()
    {
        // Create page object
        mtReasonAdd = new GLOBALS.MtReasonAdd(this);

        // Run the page
        return await mtReasonAdd.Run();
    }

    // view
    [Route("MtReasonView/{id?}", Name = "MtReasonView-mt_reason-view")]
    [Route("Home/MtReasonView/{id?}", Name = "MtReasonView-mt_reason-view-2")]
    public async Task<IActionResult> MtReasonView()
    {
        // Create page object
        mtReasonView = new GLOBALS.MtReasonView(this);

        // Run the page
        return await mtReasonView.Run();
    }

    // edit
    [Route("MtReasonEdit/{id?}", Name = "MtReasonEdit-mt_reason-edit")]
    [Route("Home/MtReasonEdit/{id?}", Name = "MtReasonEdit-mt_reason-edit-2")]
    public async Task<IActionResult> MtReasonEdit()
    {
        // Create page object
        mtReasonEdit = new GLOBALS.MtReasonEdit(this);

        // Run the page
        return await mtReasonEdit.Run();
    }

    // delete
    [Route("MtReasonDelete/{id?}", Name = "MtReasonDelete-mt_reason-delete")]
    [Route("Home/MtReasonDelete/{id?}", Name = "MtReasonDelete-mt_reason-delete-2")]
    public async Task<IActionResult> MtReasonDelete()
    {
        // Create page object
        mtReasonDelete = new GLOBALS.MtReasonDelete(this);

        // Run the page
        return await mtReasonDelete.Run();
    }
}
