using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

using Invengo.RiceManangeServices.Utility;

public partial class Work_GroupWorkItem : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            for (int i = 2010; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
                ddlYearBegin.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
            }
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            ddlYearBegin.SelectedValue = DateTime.Now.Year.ToString();
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Add(new ListItem(i.ToString() + "月份", i.ToString()));
            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
    }

    protected void btnSearch_Click(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "date")
        {
            DateTime beginTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim()+" 00:00:00");
            DateTime endTime = Convert.ToDateTime(txtPlanStartTime.Text.Trim() + " 23:59:59");

            GridView1.DataSource = GroupInfo.GetGroupWorkItem(beginTime, endTime);
            GridView1.DataBind();
        }
        else if (e.CommandName == "month")
        {
            int month = Int32.Parse(ddlMonth.SelectedValue);
            DateTime beginTime = Convert.ToDateTime(ddlYear.SelectedValue + "-" + month.ToString() + "-1 00:00:00");
            DateTime endTime;
            if (month == 12)
                endTime = Convert.ToDateTime((Int32.Parse(ddlYear.SelectedValue) + 1).ToString() + "-" + month.ToString() + "-1  00:00:00");
            else
                endTime = Convert.ToDateTime(ddlYear.SelectedValue + "-" + (month + 1).ToString() + "-1 00:00:00");
            GridView1.DataSource = GroupInfo.GetGroupWorkItem(beginTime, endTime);
            GridView1.DataBind();
        }
        else if (e.CommandName == "year")
        {
            DateTime beginTime = Convert.ToDateTime(ddlYearBegin.SelectedValue + "-1-1 00:00:00");
            DateTime endTime = Convert.ToDateTime((Int32.Parse( ddlYearBegin.SelectedValue)+1).ToString() + "-1-1 00:00:00");

            GridView1.DataSource = GroupInfo.GetGroupWorkItem(beginTime, endTime);
            GridView1.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Error", "alert('异常操作！')", true);
        }
        System.Threading.Thread.Sleep(2000);
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Show", "document.getElementById('divtoexcel').style.display='block';", true);
    }
    protected void ltnToExcle_OnClick(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=Excel.xls");
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        GridView1.Page.EnableViewState = false;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        GridView1.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
}
