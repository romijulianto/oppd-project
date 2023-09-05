namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("MtApproverList/{id?}", Name = "MtApproverList-mt_approver-list")]
    [Route("Home/MtApproverList/{id?}", Name = "MtApproverList-mt_approver-list-2")]
    public async Task<IActionResult> MtApproverList()
    {
        // Create page object
        mtApproverList = new GLOBALS.MtApproverList(this);
        mtApproverList.Cache = _cache;

        // Run the page
        return await mtApproverList.Run();
    }

    // add
    [Route("MtApproverAdd/{id?}", Name = "MtApproverAdd-mt_approver-add")]
    [Route("Home/MtApproverAdd/{id?}", Name = "MtApproverAdd-mt_approver-add-2")]
    public async Task<IActionResult> MtApproverAdd()
    {
        // Create page object
        mtApproverAdd = new GLOBALS.MtApproverAdd(this);

        // Run the page
        return await mtApproverAdd.Run();
    }

    // view
    [Route("MtApproverView/{id?}", Name = "MtApproverView-mt_approver-view")]
    [Route("Home/MtApproverView/{id?}", Name = "MtApproverView-mt_approver-view-2")]
    public async Task<IActionResult> MtApproverView()
    {
        // Create page object
        mtApproverView = new GLOBALS.MtApproverView(this);

        // Run the page
        return await mtApproverView.Run();
    }

    // edit
    [Route("MtApproverEdit/{id?}", Name = "MtApproverEdit-mt_approver-edit")]
    [Route("Home/MtApproverEdit/{id?}", Name = "MtApproverEdit-mt_approver-edit-2")]
    public async Task<IActionResult> MtApproverEdit()
    {
        // Create page object
        mtApproverEdit = new GLOBALS.MtApproverEdit(this);

        // Run the page
        return await mtApproverEdit.Run();
    }

    // delete
    [Route("MtApproverDelete/{id?}", Name = "MtApproverDelete-mt_approver-delete")]
    [Route("Home/MtApproverDelete/{id?}", Name = "MtApproverDelete-mt_approver-delete-2")]
    public async Task<IActionResult> MtApproverDelete()
    {
        // Create page object
        mtApproverDelete = new GLOBALS.MtApproverDelete(this);

        // Run the page
        return await mtApproverDelete.Run();
    }
}
