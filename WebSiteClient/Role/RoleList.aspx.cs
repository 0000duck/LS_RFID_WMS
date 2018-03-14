using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Invengo.RiceManangeServices.Utility;

public partial class Role_RoleList : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData(1);
        }
    }
    private void LoadData(int page)
    {
        int recordNum = 100;
        int pageSize = Variable.DefaultPageSize;
        string roleName =txtRoleName.Text.Trim();

        GridView1.DataSource = Role.QueryRoleList(roleName, pageSize, page, out recordNum);
        GridView1.DataBind();

        lblRecordNum.Text = recordNum.ToString();
        lblPresentPageNum.Text = page.ToString();
        lblTotalPageNum.Text = (Math.Ceiling(Convert.ToDouble(recordNum) / pageSize)).ToString();
        WSDataLayer.EnablePageControl(lbtFirstPage, lbtPreviousPage, lbtNextPage, lbtLastPage, recordNum, pageSize, Convert.ToInt32(lblTotalPageNum.Text), page);
    }
    protected void ChangePage_Click(object sender, CommandEventArgs e)
    {
        string arg = e.CommandArgument.ToString();
        int page = (Convert.ToInt32(lblPresentPageNum.Text));
        switch (arg)
        {
            case "first":
                page = 1;
                break;
            case "previous":
                page -= 1;
                break;
            case "next":
                page += 1;
                break;
            case "last":
                page = (Convert.ToInt32(lblTotalPageNum.Text));
                break;
            default:
                page = 1;
                break;
        }
        LoadData(page);
    }
    protected void btJump_Click(object sender, EventArgs e)
    {
        string page = txtPageNum.Text.Trim();
        if (!System.Text.RegularExpressions.Regex.IsMatch(page, "^\\d+$"))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "DataError", "alert('输入页数必须为正整数！')", true);
            return;
        }
        if (Convert.ToInt32(page) < 1 || Convert.ToInt32(page) > Convert.ToInt32(lblTotalPageNum.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "DataError", "alert('输入页数不在范围之内！')", true);
            return;
        }
        LoadData(Convert.ToInt32(page));
        txtPageNum.Text = string.Empty;
    }
    protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
        string roleID = GridView1.DataKeys[gRow.RowIndex].Value.ToString();

        if (e.CommandName == "Edit")
        {
            Response.Redirect("RoleEdit.aspx?RoleID=" + roleID + " &Type=1");
        }
        else if (e.CommandName == "Enter")
        {
            Response.Redirect("RoleMenuSet.aspx?RoleID=" + roleID + " &Type=1");
        }
        else if (e.CommandName == "Del")
        {
            if (UserRoleMenu.IsCanDelRole(roleID))
            {
                bool result = Role.Delete(Int32.Parse(roleID));
                if (result)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "DataSucess", "alert('删除成功！');", true);
                    LoadData(1);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "DataError", "alert('删除失败！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "DataError", "alert('角色已经分配用户，请先删除该角色下的所有用户！');", true);
            }
        }
    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor;this.style.backgroundColor='" + OnMouseBackColor + "';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e;");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadData(1);
    }
}
