namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// IHtmlValue interface (Value that can be converted to HTML and string)
    /// </summary>
    public interface IHtmlValue
    {
        string ToHtml();
    }
} // End Partial class
