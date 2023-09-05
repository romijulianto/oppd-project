namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    // IAspNetMakerPage interface // DN
    public interface IAspNetMakerPage
    {
        Task<IActionResult> Run();
        IActionResult Terminate(string url = "");
    }
} // End Partial class
