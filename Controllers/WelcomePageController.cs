namespace OverridePSDashboard.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // WelcomePage (custom)
    [Route("WelcomePage/{**key}", Name = "WelcomePage-WelcomePage-custom")]
    [Route("Home/WelcomePage/{**key}", Name = "WelcomePage-WelcomePage-custom-2")]
    public async Task<IActionResult> WelcomePage()
    {
        // Create page object
        welcomePage = new GLOBALS.WelcomePage(this);

        // Run the page
        return await welcomePage.Run();
    }
}
