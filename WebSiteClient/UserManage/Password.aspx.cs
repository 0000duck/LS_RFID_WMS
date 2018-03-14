using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserManage_Password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnConfrim_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string jobNo = Session["JobNo"].ToString();
            string oldPassword = txtOldPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            using (WSUserManage.UserManageSystemServiceClient wsClient = new WSUserManage.UserManageSystemServiceClient())
            {
                int iResult = wsClient.UserChangePassword(jobNo, oldPassword, newPassword);
                if (iResult == 0)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Sucess", "alert('修改成功！');", true);
                }
                else if (iResult == 8)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Sucess", "alert('原密码错误！');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('操作失败！');", true);
                }
            }
        }
    }
}
