namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    /// <summary>
    /// User level permission class
    /// </summary>
    public class UserLevelPermission
    {
        // Table name
        [SqlKata.Column("TableName")]
        public string Table { set; get; } = "";

        // User level ID
        [SqlKata.Column("UserLevelID")]
        public int Id { set; get; }

        // Permission
        [SqlKata.Column("Permission")]
        public int Permission { set; get; } = 0;
    }
} // End Partial class
