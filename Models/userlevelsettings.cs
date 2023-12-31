namespace OverridePSDashboard.Models;

// Partial class
public partial class OverridePS {
    // Configuration
    public static partial class Config
    {
        //
        // ASP.NET Maker 2023 user level settings
        //

        // User level info
        public static List<UserLevel> UserLevels = new ()
        {
            new () { Id = -2, Name = "Anonymous" },
            new () { Id = 0, Name = "Default" },
            new () { Id = 1, Name = "Branch Manager" },
            new () { Id = 2, Name = "Region Manager" },
            new () { Id = 3, Name = "General Manager" },
            new () { Id = 4, Name = "COO Industry" },
            new () { Id = 5, Name = "Comodity Manager" },
            new () { Id = 6, Name = "Pricing Team" },
            new () { Id = 7, Name = "PSSR Requestor" }
        };

        // User level priv info
        public static List<UserLevelPermission> UserLevelPermissions = new ()
        {
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_API", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_API", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_API", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_API", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_API", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_API", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_API", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_API", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_API", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_email", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_email", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_email", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_email", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_email", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_email", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_email", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_email", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}log_email", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_param", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_param", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_param", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_param", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_param", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_param", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_param", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_param", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_param", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_reason", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_reason", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_reason", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_reason", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_reason", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_reason", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_reason", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_reason", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_reason", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_template_email", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_template_email", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_template_email", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_template_email", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_template_email", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_template_email", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_template_email", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_template_email", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_template_email", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_user", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_user", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_user", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_user", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_user", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_user", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_user", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_user", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_user", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approval_history", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approval_history", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approval_history", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approval_history", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approval_history", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approval_history", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approval_history", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approval_history", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approval_history", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approver", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approver", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approver", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approver", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approver", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approver", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approver", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approver", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_approver", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_detail", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_detail", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_detail", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_detail", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_detail", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_detail", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_detail", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_detail", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}tr_request_detail", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevelPermissions", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevelPermissions", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevelPermissions", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevelPermissions", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevelPermissions", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevelPermissions", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevelPermissions", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevelPermissions", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevelPermissions", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevels", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevels", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevels", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevels", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevels", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevels", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevels", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevels", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}UserLevels", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}WelcomePage", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}WelcomePage", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}WelcomePage", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}WelcomePage", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}WelcomePage", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}WelcomePage", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}WelcomePage", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}WelcomePage", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}WelcomePage", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}v_tr_request", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}v_tr_request", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}v_tr_request", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}v_tr_request", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}v_tr_request", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}v_tr_request", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}v_tr_request", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}v_tr_request", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}v_tr_request", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_approver", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_approver", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_approver", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_approver", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_approver", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_approver", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_approver", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_approver", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_approver", Id = 7, Permission = 367 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_backup_approver", Id = -2, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_backup_approver", Id = 0, Permission = 0 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_backup_approver", Id = 1, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_backup_approver", Id = 2, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_backup_approver", Id = 3, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_backup_approver", Id = 4, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_backup_approver", Id = 5, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_backup_approver", Id = 6, Permission = 360 },
            new () { Table = "{74850334-D87A-4E71-B45A-50C64B48A90E}mt_backup_approver", Id = 7, Permission = 367 }
        };

        // User level table info // DN
        public static List<UserLevelTablePermission> UserLevelTablePermissions = new ()
        {
            new () { TableName = "log_API", TableVar = "log_API", Caption = "API History", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "LogApiList" },
            new () { TableName = "log_email", TableVar = "log_email", Caption = "Email History", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "LogEmailList" },
            new () { TableName = "mt_param", TableVar = "mt_param", Caption = "Master Parameter", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "MtParamList" },
            new () { TableName = "mt_reason", TableVar = "mt_reason", Caption = "Master Reason", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "MtReasonList" },
            new () { TableName = "mt_template_email", TableVar = "mt_template_email", Caption = "Email Template", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "MtTemplateEmailList" },
            new () { TableName = "mt_user", TableVar = "mt_user", Caption = "Master User", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "MtUserList" },
            new () { TableName = "tr_request", TableVar = "tr_request", Caption = "Override Request", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "TrRequestList" },
            new () { TableName = "tr_request_approval_history", TableVar = "tr_request_approval_history", Caption = "Approver Log History", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "TrRequestApprovalHistoryList" },
            new () { TableName = "tr_request_approver", TableVar = "tr_request_approver", Caption = "Approver List", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "TrRequestApproverList" },
            new () { TableName = "tr_request_detail", TableVar = "tr_request_detail", Caption = "Request Detail", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "TrRequestDetailList" },
            new () { TableName = "UserLevelPermissions", TableVar = "UserLevelPermissions", Caption = "User Level Permissions", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "UserLevelPermissionsList" },
            new () { TableName = "UserLevels", TableVar = "UserLevels", Caption = "User Levels", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "UserLevelsList" },
            new () { TableName = "WelcomePage", TableVar = "WelcomePage", Caption = "Welcome Page", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "WelcomePage" },
            new () { TableName = "v_tr_request", TableVar = "v_tr_request", Caption = "Completed Request", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "VTrRequestList" },
            new () { TableName = "mt_approver", TableVar = "mt_approver", Caption = "mt approver", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "MtApproverList" },
            new () { TableName = "mt_backup_approver", TableVar = "mt_backup_approver", Caption = "mt backup approver", Allowed = true, ProjectId = "{74850334-D87A-4E71-B45A-50C64B48A90E}", Url = "MtBackupApproverList" }
        };
    }
} // End Partial class
