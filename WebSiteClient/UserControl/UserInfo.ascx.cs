using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class UserControl_UserInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] != null)
            {
                lblUser.Text = Session["UserName"].ToString();
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }
    }
    protected void ibtnExit_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }
    protected void ibtnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Main.aspx");
    }
    protected void ibtnPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UserManage/Password.aspx");
    }
}
