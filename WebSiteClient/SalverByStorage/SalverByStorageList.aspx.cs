using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Invengo.RiceManangeServices.Utility;

public partial class SalverByStorage_SalverByStorageList : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlStorage.DataSource = Storage.GetStorage();
            ddlStorage.DataTextField = "WHName";
            ddlStorage.DataValueField = "WHId";
            ddlStorage.DataBind();
            ddlStorage.Items.Insert(0, new ListItem("--全部仓库--", ""));
            LoadData(1);
        }
    }

    private void LoadData(int page)
    {
        int recordNum = 0;
        int pageSize = Variable.DefaultPageSize;
        GridView1.DataSource = Grain.QueryGrainListByStorage(QueryCondition(),pageSize,page,out recordNum);
        GridView1.DataBind();

        lblRecordNum.Text = recordNum.ToString();
        lblPresentPageNum.Text = page.ToString();
        lblTotalPageNum.Text = (Math.Ceiling(Convert.ToDouble(recordNum) / pageSize)).ToString();

        WSDataLayer.EnablePageControl(lbtFirstPage, lbtPreviousPage, lbtNextPage, lbtLastPage, recordNum, pageSize, Convert.ToInt32(lblTotalPageNum.Text), page);
    }

    private Dictionary<string, string> QueryCondition()
    {
        Dictionary<string, string> queryCondition = new Dictionary<string, string>();
        queryCondition.Add("WPNumber", txtWPNumber.Text.Trim());
        queryCondition.Add("MaterailNumber", txtMaterailNumber.Text.Trim());
        queryCondition.Add("WPGLot", txtWPGLot.Text.Trim());
        queryCondition.Add("WHID", ddlStorage.SelectedValue);

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
        System.Threading.Thread.Sleep(2000);
    }
}
