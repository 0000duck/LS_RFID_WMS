using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Web.Caching;
using AjaxControlToolkit;

using Invengo.RiceManangeServices.Utility;

public partial class Left : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateMenu();
        }
    }
    private void CreateMenu()
    {
        if (Session["RoleID"] != null)
        {
            int roleID = int.Parse(Session["RoleID"].ToString());
            DataTable dt = UserRoleMenu.QueryUserRoleMenu(roleID);
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow[] drs = dt.Select("level=1 and isshow=1", "orderno");
            if (string.IsNullOrEmpty(Request.QueryString["Index"]))
            {
                MyAccordion.SelectedIndex = -1;//默认不展开
            }
            else
            {
                MyAccordion.SelectedIndex = Convert.ToInt32(Request.QueryString["Index"]);
            }
            int index = 0;
            foreach (DataRow dr in drs)
            {
                AccordionPane pane = new AccordionPane();

                Label lalMenu1 = new Label();
                string id = dr["MenuID"].ToString();
                lalMenu1.Text = dr["name"].ToString();
                pane.CssClass = "MyPane";
                pane.HeaderContainer.Controls.Add(lalMenu1);
                DataRow[] drsChild = dt.Select("isshow=1 and parentmenuid=" + id, "orderno");
                foreach (DataRow drC in drsChild)
                {
                    Literal ltl = new Literal();
                    ltl.Text = @"<table style='padding-left:0px;margin-left:0px;'>
                             <tr><td><img src='images/point.png' ></td><td><a href='./" + drC["path"].ToString() + "?Index=" + index +
                                 "' target=\"contentFrame\">" + drC["name"].ToString() + "</a></td></tr></table>";
                    pane.ContentContainer.Controls.Add(ltl);
                }
                MyAccordion.Panes.Add(pane);
                index++;
            }
        }
        else
        {
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}
