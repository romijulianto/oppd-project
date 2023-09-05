namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// Advanced Security class
    /// </summary>
    public class AdvancedSecurity : AdvancedSecurityBase
    {
        // Constructor
        public AdvancedSecurity() : base()
        {
            Security = this;
        }
    }
} // End Partial class
