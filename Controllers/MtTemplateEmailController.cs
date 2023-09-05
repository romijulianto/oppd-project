namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("MtTemplateEmailList/{id?}", Name = "MtTemplateEmailList-mt_template_email-list")]
    [Route("Home/MtTemplateEmailList/{id?}", Name = "MtTemplateEmailList-mt_template_email-list-2")]
    public async Task<IActionResult> MtTemplateEmailList()
    {
        // Create page object
        mtTemplateEmailList = new GLOBALS.MtTemplateEmailList(this);
        mtTemplateEmailList.Cache = _cache;

        // Run the page
        return await mtTemplateEmailList.Run();
    }

    // add
    [Route("MtTemplateEmailAdd/{id?}", Name = "MtTemplateEmailAdd-mt_template_email-add")]
    [Route("Home/MtTemplateEmailAdd/{id?}", Name = "MtTemplateEmailAdd-mt_template_email-add-2")]
    public async Task<IActionResult> MtTemplateEmailAdd()
    {
        // Create page object
        mtTemplateEmailAdd = new GLOBALS.MtTemplateEmailAdd(this);

        // Run the page
        return await mtTemplateEmailAdd.Run();
    }

    // view
    [Route("MtTemplateEmailView/{id?}", Name = "MtTemplateEmailView-mt_template_email-view")]
    [Route("Home/MtTemplateEmailView/{id?}", Name = "MtTemplateEmailView-mt_template_email-view-2")]
    public async Task<IActionResult> MtTemplateEmailView()
    {
        // Create page object
        mtTemplateEmailView = new GLOBALS.MtTemplateEmailView(this);

        // Run the page
        return await mtTemplateEmailView.Run();
    }

    // edit
    [Route("MtTemplateEmailEdit/{id?}", Name = "MtTemplateEmailEdit-mt_template_email-edit")]
    [Route("Home/MtTemplateEmailEdit/{id?}", Name = "MtTemplateEmailEdit-mt_template_email-edit-2")]
    public async Task<IActionResult> MtTemplateEmailEdit()
    {
        // Create page object
        mtTemplateEmailEdit = new GLOBALS.MtTemplateEmailEdit(this);

        // Run the page
        return await mtTemplateEmailEdit.Run();
    }

    // delete
    [Route("MtTemplateEmailDelete/{id?}", Name = "MtTemplateEmailDelete-mt_template_email-delete")]
    [Route("Home/MtTemplateEmailDelete/{id?}", Name = "MtTemplateEmailDelete-mt_template_email-delete-2")]
    public async Task<IActionResult> MtTemplateEmailDelete()
    {
        // Create page object
        mtTemplateEmailDelete = new GLOBALS.MtTemplateEmailDelete(this);

        // Run the page
        return await mtTemplateEmailDelete.Run();
    }
}
