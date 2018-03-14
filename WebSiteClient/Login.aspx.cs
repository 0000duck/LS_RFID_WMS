using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Security;
using Invengo.RiceManangeServices.Model;
using WSUserManage;

public partial class _Login : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            Server.Transfer("~/Index.htm");
        }
        SetFocus("txtUserName");
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            System.Threading.Thread.Sleep(2000);
            string userName = txtUserName.Text.Trim();
            string passWord = this.txtPassWord.Text.Trim();
           
            UserInfo userInfo = new UserInfo();
            int iLogResult = 5;
            UserManageSystemServiceClient wsClient = new WSUserManage.UserManageSystemServiceClient();
            try
            {
                iLogResult = wsClient.UserLogin(out userInfo, userName, passWord, 1);
            }
            catch (Exception ex)
            {
                wsClient.Abort();
                throw ex;
            }
            finally
            {
                wsClient.Close();
            }
            if (iLogResult == 5)
            {
                lblErrMsg.Text = "用户不存在!";
                return;
            }
            else if (iLogResult == 6)
            {
                lblErrMsg.Text = "密码错误!";
                return;
            }
            else if (iLogResult == 7)
            {
                lblErrMsg.Text = "用户没有访问网站的权限!";
                return;
            }
            else
            {
                SetUserDataAndRedirect(userInfo);
            }
        }
    }


    private void SetUserDataAndRedirect(UserInfo userInfo)
    {
        Session["JobNo"] = userInfo.JobNo;
        Session["UserName"] = userInfo.RealName;
        Session["UserID"] = userInfo.UserID;
        Session["RoleID"] = userInfo.RoleID;
        string userID = userInfo.JobNo;

        // 获得 来到登录页之前的页面，即url中return参数的值
        string url = FormsAuthentication.GetRedirectUrl(userID.ToString(), true);
        Response.Redirect("~/Index.htm");
    }
}
