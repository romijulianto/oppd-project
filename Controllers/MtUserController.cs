namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("MtUserList/{_UserName?}", Name = "MtUserList-mt_user-list")]
    [Route("Home/MtUserList/{_UserName?}", Name = "MtUserList-mt_user-list-2")]
    public async Task<IActionResult> MtUserList()
    {
        // Create page object
        mtUserList = new GLOBALS.MtUserList(this);
        mtUserList.Cache = _cache;

        // Run the page
        return await mtUserList.Run();
    }

    // add
    [Route("MtUserAdd/{_UserName?}", Name = "MtUserAdd-mt_user-add")]
    [Route("Home/MtUserAdd/{_UserName?}", Name = "MtUserAdd-mt_user-add-2")]
    public async Task<IActionResult> MtUserAdd()
    {
        // Create page object
        mtUserAdd = new GLOBALS.MtUserAdd(this);

        // Run the page
        return await mtUserAdd.Run();
    }

    // view
    [Route("MtUserView/{_UserName?}", Name = "MtUserView-mt_user-view")]
    [Route("Home/MtUserView/{_UserName?}", Name = "MtUserView-mt_user-view-2")]
    public async Task<IActionResult> MtUserView()
    {
        // Create page object
        mtUserView = new GLOBALS.MtUserView(this);

        // Run the page
        return await mtUserView.Run();
    }

    // edit
    [Route("MtUserEdit/{_UserName?}", Name = "MtUserEdit-mt_user-edit")]
    [Route("Home/MtUserEdit/{_UserName?}", Name = "MtUserEdit-mt_user-edit-2")]
    public async Task<IActionResult> MtUserEdit()
    {
        // Create page object
        mtUserEdit = new GLOBALS.MtUserEdit(this);

        // Run the page
        return await mtUserEdit.Run();
    }

    // delete
    [Route("MtUserDelete/{_UserName?}", Name = "MtUserDelete-mt_user-delete")]
    [Route("Home/MtUserDelete/{_UserName?}", Name = "MtUserDelete-mt_user-delete-2")]
    public async Task<IActionResult> MtUserDelete()
    {
        // Create page object
        mtUserDelete = new GLOBALS.MtUserDelete(this);

        // Run the page
        return await mtUserDelete.Run();
    }
}
