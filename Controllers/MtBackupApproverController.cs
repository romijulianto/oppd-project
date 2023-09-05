namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("MtBackupApproverList/{id?}", Name = "MtBackupApproverList-mt_backup_approver-list")]
    [Route("Home/MtBackupApproverList/{id?}", Name = "MtBackupApproverList-mt_backup_approver-list-2")]
    public async Task<IActionResult> MtBackupApproverList()
    {
        // Create page object
        mtBackupApproverList = new GLOBALS.MtBackupApproverList(this);
        mtBackupApproverList.Cache = _cache;

        // Run the page
        return await mtBackupApproverList.Run();
    }

    // add
    [Route("MtBackupApproverAdd/{id?}", Name = "MtBackupApproverAdd-mt_backup_approver-add")]
    [Route("Home/MtBackupApproverAdd/{id?}", Name = "MtBackupApproverAdd-mt_backup_approver-add-2")]
    public async Task<IActionResult> MtBackupApproverAdd()
    {
        // Create page object
        mtBackupApproverAdd = new GLOBALS.MtBackupApproverAdd(this);

        // Run the page
        return await mtBackupApproverAdd.Run();
    }

    // view
    [Route("MtBackupApproverView/{id?}", Name = "MtBackupApproverView-mt_backup_approver-view")]
    [Route("Home/MtBackupApproverView/{id?}", Name = "MtBackupApproverView-mt_backup_approver-view-2")]
    public async Task<IActionResult> MtBackupApproverView()
    {
        // Create page object
        mtBackupApproverView = new GLOBALS.MtBackupApproverView(this);

        // Run the page
        return await mtBackupApproverView.Run();
    }

    // edit
    [Route("MtBackupApproverEdit/{id?}", Name = "MtBackupApproverEdit-mt_backup_approver-edit")]
    [Route("Home/MtBackupApproverEdit/{id?}", Name = "MtBackupApproverEdit-mt_backup_approver-edit-2")]
    public async Task<IActionResult> MtBackupApproverEdit()
    {
        // Create page object
        mtBackupApproverEdit = new GLOBALS.MtBackupApproverEdit(this);

        // Run the page
        return await mtBackupApproverEdit.Run();
    }

    // delete
    [Route("MtBackupApproverDelete/{id?}", Name = "MtBackupApproverDelete-mt_backup_approver-delete")]
    [Route("Home/MtBackupApproverDelete/{id?}", Name = "MtBackupApproverDelete-mt_backup_approver-delete-2")]
    public async Task<IActionResult> MtBackupApproverDelete()
    {
        // Create page object
        mtBackupApproverDelete = new GLOBALS.MtBackupApproverDelete(this);

        // Run the page
        return await mtBackupApproverDelete.Run();
    }
}
