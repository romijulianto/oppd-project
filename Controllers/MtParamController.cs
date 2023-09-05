namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("MtParamList/{id?}", Name = "MtParamList-mt_param-list")]
    [Route("Home/MtParamList/{id?}", Name = "MtParamList-mt_param-list-2")]
    public async Task<IActionResult> MtParamList()
    {
        // Create page object
        mtParamList = new GLOBALS.MtParamList(this);
        mtParamList.Cache = _cache;

        // Run the page
        return await mtParamList.Run();
    }

    // add
    [Route("MtParamAdd/{id?}", Name = "MtParamAdd-mt_param-add")]
    [Route("Home/MtParamAdd/{id?}", Name = "MtParamAdd-mt_param-add-2")]
    public async Task<IActionResult> MtParamAdd()
    {
        // Create page object
        mtParamAdd = new GLOBALS.MtParamAdd(this);

        // Run the page
        return await mtParamAdd.Run();
    }

    // view
    [Route("MtParamView/{id?}", Name = "MtParamView-mt_param-view")]
    [Route("Home/MtParamView/{id?}", Name = "MtParamView-mt_param-view-2")]
    public async Task<IActionResult> MtParamView()
    {
        // Create page object
        mtParamView = new GLOBALS.MtParamView(this);

        // Run the page
        return await mtParamView.Run();
    }

    // edit
    [Route("MtParamEdit/{id?}", Name = "MtParamEdit-mt_param-edit")]
    [Route("Home/MtParamEdit/{id?}", Name = "MtParamEdit-mt_param-edit-2")]
    public async Task<IActionResult> MtParamEdit()
    {
        // Create page object
        mtParamEdit = new GLOBALS.MtParamEdit(this);

        // Run the page
        return await mtParamEdit.Run();
    }

    // delete
    [Route("MtParamDelete/{id?}", Name = "MtParamDelete-mt_param-delete")]
    [Route("Home/MtParamDelete/{id?}", Name = "MtParamDelete-mt_param-delete-2")]
    public async Task<IActionResult> MtParamDelete()
    {
        // Create page object
        mtParamDelete = new GLOBALS.MtParamDelete(this);

        // Run the page
        return await mtParamDelete.Run();
    }
}
