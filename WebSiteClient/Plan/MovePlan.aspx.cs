using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Invengo.RiceManangeServices.Utility;

public partial class Plan_MovePlan : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["MBID"] = string.Empty;
            LoadData(1);
        }
    }

    private void LoadData(int page)
    {
        int recordNum = 100;
        int pageSize = Variable.DefaultPageSize;
        string retValue = string.Empty;
        bool isFirstSearch = true;

        BatchPlan.QueryMovePlanList(QueryCondition(),pageSize, page,null,isFirstSearch,out recordNum, out retValue);
        GridView1.DataSource = Helper.ConvertXmlStrToDataTable(retValue);
        GridView1.DataBind();

        lblRecordNum.Text = recordNum.ToString();
        lblPresentPageNum.Text = page.ToString();
        lblTotalPageNum.Text = (Math.Ceiling(Convert.ToDouble(recordNum) / pageSize)).ToString();


        WSDataLayer.EnablePageControl(lbtFirstPage, lbtPreviousPage, lbtNextPage, lbtLastPage, recordNum, pageSize, Convert.ToInt32(lblTotalPageNum.Text), page);
    }

    private Dictionary<string, string> QueryCondition()
    {
        Dictionary<string, string> queryCondition = new Dictionary<string, string>();
        queryCondition.Add("PRType", rdlPlanType.SelectedValue);
        queryCondition.Add("PRNumber", txtPRNumber.Text.Trim());
        queryCondition.Add("PRLot", txtPRLot.Text.Trim());
        queryCondition.Add("StartTime", txtPlanStartTime.Text.Trim());
        queryCondition.Add("EndTime", txtPlanEndTime.Text.Trim());
        queryCondition.Add("MBID", ViewState["MBID"].ToString());

        return queryCondition;
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
        string id = GridView1.DataKeys[gRow.RowIndex].Value.ToString();

        if (e.CommandName == "ViewDetail")
        {
            DetailsView1.DataSource = BatchPlan.QueryMovePlanById(int.Parse(id));
            DetailsView1.DataBind();
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "open", "Open()", true);
        }
        else if (e.CommandName == "ViewRelation")
        {
            Label lblMBID = (Label)GridView1.Rows[gRow.RowIndex].FindControl("lblMBID");
            ViewState["MBID"] = lblMBID.Text;
            LoadData(1);
        }
    }

    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblViewRelation = (Label)e.Row.FindControl("lblViewRelation");
            LinkButton lbtViewRelation = (LinkButton)e.Row.FindControl("lnkViewRelation");
            if (lblViewRelation.Text.Trim() != "父单据")
            {
                lbtViewRelation.Visible = false;
            }

            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor;this.style.backgroundColor='" + OnMouseBackColor + "';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e;");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["MBID"] = string.Empty;
        LoadData(1);
    }
}
