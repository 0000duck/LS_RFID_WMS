using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
using System.Data;

/// <summary>
///Page 的摘要说明
/// </summary>
public class Page : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {
        if (IsAuthenticated())
        {
            base.OnLoad(e);
        }
        else
        {
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
    protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
    {
        if (IsAuthenticated())
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);
        }
        else
        {
            FormsAuthentication.RedirectToLoginPage();
        }
    }
    protected string OnMouseBackColor
    {
        get
        {
            return ConfigurationManager.AppSettings["OnMouseBackColor"].ToString();
        }
    }
    protected string JobNo
    {
        get
        {
            if (Session["JobNo"] != null)
                return Session["JobNo"].ToString();
            else
                return string.Empty;
        }
    }
    protected string UserName
    {
        get
        {
            if (Session["UserName"] != null)
                return Session["UserName"].ToString();
            else
                return string.Empty;
        }
    }
    protected string UserID
    {
        get
        {
            if (Session["UserID"] != null)
                return Session["UserID"].ToString();
            else
                return string.Empty;
        }
    }
    protected string RoleID
    {
        get
        {
            if (Session["RoleID"] != null)
                return Session["RoleID"].ToString();
            else
                return string.Empty;
        }
    }
    public string PageTitle
    {
        get
        {
            string url = Request.Url.ToString();
            string[] urls = url.Split('/');
            string page = urls[urls.Length - 2].ToString() + "/" + urls[urls.Length - 1].ToString();
            if (page.IndexOf("?") > 0)
                page = page.Substring(0, page.IndexOf("?"));
            string sql = @"select m2.[name]+'>>'+m1.[name]  from menu m1,menu m2 where m1.path='" + page
                + "' and m1.parentmenuid=m2.menuid";

            DataTable dt = Common.GetObjectList(sql,null);
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return string.Empty;
        }
    }
    private bool IsAuthenticated()
    {
        string url = Request.Url.ToString();
        string[] urls=url.Split('/');
        string page = urls[urls.Length - 2].ToString() + "/" + urls[urls.Length-1].ToString();
        if(page.IndexOf("?")>0)
            page=page.Substring(0, page.IndexOf("?"));

        return Common.IsRoleAuthenticated(RoleID, page);
    }
}
