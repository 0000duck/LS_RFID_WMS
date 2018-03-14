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

public partial class TruckEir_TruckEirGroup : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 初始化数据
            txtPlanStartTime.Text = DateTime.Today.AddDays(-7).ToShortDateString();
            txtPlanEndTime.Text = DateTime.Today.ToShortDateString();
            for (int i = 2010; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
                ddlYearBegin.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
                ddlYearEnd.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
            }
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            ddlYearBegin.SelectedValue = "2010";
            ddlYearEnd.SelectedValue = DateTime.Now.Year.ToString();

            DataTable dtStorage = Storage.GetStorage();
            DataTable dtMaterial = Grain.GetMaterialInStorage();

            ddlStorage.DataSource = dtStorage;
            ddlStorage.DataTextField = "WHName";
            ddlStorage.DataValueField = "WHId";
            ddlStorage.DataBind();
            ddlStorage.Items.Insert(0, new ListItem("--全部仓库--", ""));
            ddlFloor.Items.Insert(0, new ListItem("--全部楼层--", ""));
            ddlStorageDetail.Items.Insert(0, new ListItem("--全部仓位--", ""));

            ddlMaterialName.DataSource = dtMaterial;
            ddlMaterialName.DataTextField = "MaterialName";
            ddlMaterialName.DataValueField = "MaterialName";
            ddlMaterialName.DataBind();
            ddlMaterialName.Items.Insert(0, new ListItem("--不限--", ""));

            ddlStorageM.DataSource = dtStorage;
            ddlStorageM.DataTextField = "WHName";
            ddlStorageM.DataValueField = "WHId";
            ddlStorageM.DataBind();
            ddlStorageM.Items.Insert(0, new ListItem("--全部仓库--", ""));
            ddlFloorM.Items.Insert(0, new ListItem("--全部楼层--", ""));
            ddlStorageDetailM.Items.Insert(0, new ListItem("--全部仓位--", ""));

            ddlMaterialNameM.DataSource = dtMaterial;
            ddlMaterialNameM.DataTextField = "MaterialName";
            ddlMaterialNameM.DataValueField = "MaterialName";
            ddlMaterialNameM.DataBind();
            ddlMaterialNameM.Items.Insert(0, new ListItem("--不限--", ""));

            ddlStorageY.DataSource = dtStorage;
            ddlStorageY.DataTextField = "WHName";
            ddlStorageY.DataValueField = "WHId";
            ddlStorageY.DataBind();
            ddlStorageY.Items.Insert(0, new ListItem("--全部仓库--", ""));
            ddlFloorY.Items.Insert(0, new ListItem("--全部楼层--", ""));
            ddlStorageDetailY.Items.Insert(0, new ListItem("--全部仓位--", ""));

            ddlMaterialNameY.DataSource = dtMaterial;
            ddlMaterialNameY.DataTextField = "MaterialName";
            ddlMaterialNameY.DataValueField = "MaterialName";
            ddlMaterialNameY.DataBind();
            ddlMaterialNameY.Items.Insert(0, new ListItem("--不限--", ""));
            #endregion
        }
    }

    #region 回传事件
    protected void ddlStorage_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFloor.Items.Clear();
        ddlStorageDetail.Items.Clear();
        string WHId = ddlStorage.SelectedValue;
        if (!string.IsNullOrEmpty(WHId))
        {
            ddlFloor.DataSource = Storage.GetBuildingFloor(WHId);
            ddlFloor.DataTextField = "floor";
            ddlFloor.DataValueField = "floor";
            ddlFloor.DataBind();
        }
        ddlFloor.Items.Insert(0, new ListItem("--全部楼层--", ""));
        ddlStorageDetail.Items.Insert(0, new ListItem("--全部仓位--", ""));
    }
    protected void ddlFloor_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStorageDetail.Items.Clear();
        string WHId = ddlStorage.SelectedValue;
        string floor = ddlFloor.SelectedValue;
        if (!string.IsNullOrEmpty(floor))
        {
            ddlStorageDetail.DataSource = Storage.GetStorageDetail(WHId, floor, string.Empty);
            ddlStorageDetail.DataTextField = "WPName";
            ddlStorageDetail.DataValueField = "WPId";
            ddlStorageDetail.DataBind();
        }
        ddlStorageDetail.Items.Insert(0, new ListItem("--全部仓位--", ""));
    }
    protected void ddlStorageM_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFloorM.Items.Clear();
        ddlStorageDetailM.Items.Clear();
        string WHId = ddlStorageM.SelectedValue;
        if (!string.IsNullOrEmpty(WHId))
        {
            ddlFloorM.DataSource = Storage.GetBuildingFloor(WHId);
            ddlFloorM.DataTextField = "floor";
            ddlFloorM.DataValueField = "floor";
            ddlFloorM.DataBind();
        }
        ddlFloorM.Items.Insert(0, new ListItem("--全部楼层--", ""));
        ddlStorageDetailM.Items.Insert(0, new ListItem("--全部仓位--", ""));
    }
    protected void ddlFloorM_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStorageDetailM.Items.Clear();
        string WHId = ddlStorageM.SelectedValue;
        string floor = ddlFloorM.SelectedValue;
        if (!string.IsNullOrEmpty(floor))
        {
            ddlStorageDetailM.DataSource = Storage.GetStorageDetail(WHId, floor, string.Empty);
            ddlStorageDetailM.DataTextField = "WPName";
            ddlStorageDetailM.DataValueField = "WPId";
            ddlStorageDetailM.DataBind();
        }
        ddlStorageDetailM.Items.Insert(0, new ListItem("--全部仓位--", ""));
    }
    protected void ddlStorageY_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFloorY.Items.Clear();
        ddlStorageDetailY.Items.Clear();
        string WHId = ddlStorageY.SelectedValue;
        if (!string.IsNullOrEmpty(WHId))
        {
            ddlFloorY.DataSource = Storage.GetBuildingFloor(WHId);
            ddlFloorY.DataTextField = "floor";
            ddlFloorY.DataValueField = "floor";
            ddlFloorY.DataBind();
        }
        ddlFloorY.Items.Insert(0, new ListItem("--全部楼层--", ""));
        ddlStorageDetailY.Items.Insert(0, new ListItem("--全部仓位--", ""));
    }
    protected void ddlFloorY_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStorageDetailY.Items.Clear();
        string WHId = ddlStorageY.SelectedValue;
        string floor = ddlFloorY.SelectedValue;
        if (!string.IsNullOrEmpty(floor))
        {
            ddlStorageDetailY.DataSource = Storage.GetStorageDetail(WHId, floor, string.Empty);
            ddlStorageDetailY.DataTextField = "WPName";
            ddlStorageDetailY.DataValueField = "WPId";
            ddlStorageDetailY.DataBind();
        }
        ddlStorageDetailY.Items.Insert(0, new ListItem("--全部仓位--", ""));
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
            DateTime beginDate = Convert.ToDateTime(txtPlanStartTime.Text.Trim());
            DateTime endDate = Convert.ToDateTime(txtPlanEndTime.Text.Trim());

            DataTable dt = new DataTable();
            string cargoNmae = ddlMaterialName.SelectedValue;
            string storageId = ddlStorage.SelectedValue;
            string buildFloor = ddlFloor.SelectedItem.Text.Replace("--全部楼层--", "");
            string storageDeatilId = ddlStorageDetail.SelectedValue;

            #region 取得数据源

            if (string.IsNullOrEmpty(cargoNmae))
            {
                if (string.IsNullOrEmpty(storageId))
                {
                    dt = GroupInfo.GetGroupIOStorageTotal(beginDate, endDate, 0, "", "", 0, "");//无条件，全部统计
                }
                else
                {
                    if (string.IsNullOrEmpty(buildFloor))
                    {
                        dt = GroupInfo.GetGroupIOStorageTotal(beginDate, endDate, 2, "", storageId, 0, "");//按仓库
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(storageDeatilId))
                        {
                            dt = GroupInfo.GetGroupIOStorageTotal(beginDate, endDate, 3, "", storageId, int.Parse(buildFloor), "");//按楼层
                        }
                        else
                        {
                            dt = GroupInfo.GetGroupIOStorageTotal(beginDate, endDate, 4, "", "", 0, storageDeatilId);//按仓位
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(storageId))
                {
                    dt = GroupInfo.GetGroupIOStorageTotal(beginDate, endDate, 1, cargoNmae, "", 0, "");//按品名
                }
                else
                {
                    if (string.IsNullOrEmpty(storageId))
                    {
                        dt = GroupInfo.GetGroupIOStorageTotal(beginDate, endDate, 5, cargoNmae, storageId, 0, "");//按品名仓库
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(storageDeatilId))
                        {
                            dt = GroupInfo.GetGroupIOStorageTotal(beginDate, endDate, 6, cargoNmae, storageId, int.Parse(buildFloor), "");
                            //按品名楼层
                        }
                        else
                        {
                            dt = GroupInfo.GetGroupIOStorageTotal(beginDate, endDate, 7, cargoNmae, "", 0, storageDeatilId);//按品名仓位
                        }
                    }
                }
            }

            #endregion

            GridView1.DataSource = dt;
            GridView1.DataBind();
            ltnToExcle.Visible = true;
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Show", "document.getElementById('divtoexcel').style.display='block';", true);
        }
    }
    protected void ibtnSearch_Click(object sender,  CommandEventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
        if (e.CommandName == "month")
        {
            int year = Int32.Parse(ddlYear.SelectedValue);
            DataTable dt = new DataTable();
            string cargoNmae = ddlMaterialNameM.SelectedValue;
            string storageId = ddlStorageM.SelectedValue;
            string buildFloor = ddlFloorM.SelectedItem.Text.Replace("--全部楼层--", "");
            string storageDeatilId = ddlStorageDetailM.SelectedValue;

            #region 取得数据源

            if (string.IsNullOrEmpty(cargoNmae))
            {
                if (string.IsNullOrEmpty(storageId))
                {
                    dt = GroupInfo.GetGroupIOStorageTotalByMonth(year, 0, "", "", 0, "");//无条件，全部统计
                }
                else
                {
                    if (string.IsNullOrEmpty(buildFloor))
                    {
                        dt = GroupInfo.GetGroupIOStorageTotalByMonth(year, 2, "", storageId, 0, "");//按仓库
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(storageDeatilId))
                        {
                            dt = GroupInfo.GetGroupIOStorageTotalByMonth(year, 3, "",storageId,int.Parse(buildFloor), "");//按楼层
                        }
                        else
                        {
                            dt = GroupInfo.GetGroupIOStorageTotalByMonth(year, 4, "", "", 0, storageDeatilId);//按仓位
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(storageId))
                {
                    dt = GroupInfo.GetGroupIOStorageTotalByMonth(year, 1, cargoNmae, "", 0, "");//按品名
                }
                else
                {
                    if (string.IsNullOrEmpty(buildFloor))
                    {
                        dt = GroupInfo.GetGroupIOStorageTotalByMonth(year, 5, cargoNmae, storageId, 0, "");//按品名仓库
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(storageDeatilId))
                        {
                            dt = GroupInfo.GetGroupIOStorageTotalByMonth(year, 6, cargoNmae,storageId, int.Parse(buildFloor), "");//按品名楼层
                        }
                        else
                        {
                            dt = GroupInfo.GetGroupIOStorageTotalByMonth(year, 7, cargoNmae, "", 0, storageDeatilId);//按品名仓位
                        }
                    }
                }
            }

            #endregion

            Series series1 = chart2.Series[0];
            Series series2 = chart2.Series[1];        //数据集声明  

            // DateTime date1 = DateTime.Now.Date;
            for (int i = 0; i < dt.Rows.Count; i++)      //向数据集绑定数据
            {
                series1.Points.AddXY(dt.Rows[i]["mouth"].ToString() + "月份", dt.Rows[i]["inTotalWeight"].ToString());
                series2.Points.AddXY(dt.Rows[i]["mouth"].ToString() + "月份", dt.Rows[i]["outTotalWeight"].ToString());
                //  date1 = date1.AddDays(1);
            }
            while (this.chart2.Series[0].Points.Count > dt.Rows.Count)
            {
                // Remove series points
                foreach (Series s in this.chart2.Series)
                {
                    s.Points.RemoveAt(0);
                }
            }
            chart2.Titles.Clear();
            Title t1 = new Title(year.ToString() + "年每月粮食出入库情况", Docking.Top, new System.Drawing.Font("微软雅黑", 16), System.Drawing.Color.FromArgb(26, 59, 105));//图片标题
            chart2.Titles.Add(t1);
            chart2.ChartAreas["ChartArea1"].AxisY.Title = "重量（单位：吨）";
            chart2.ChartAreas["ChartArea1"].AxisX.Title = "月份";
            //chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        }
        else if (e.CommandName == "year")
        {
            int beginYear = Int32.Parse(ddlYearBegin.SelectedValue);
            int endYear = Int32.Parse(ddlYearEnd.SelectedValue);
            DataTable dt = new DataTable();
            string cargoNmae = ddlMaterialNameY.SelectedValue;
            string storageId = ddlStorageY.SelectedValue;
            string buildFloor = ddlFloorY.SelectedItem.Text.Replace("--全部楼层--", "");
            string storageDeatilId = ddlStorageDetailY.SelectedValue;

            #region 取得数据源

            if (string.IsNullOrEmpty(cargoNmae))
            {
                if (string.IsNullOrEmpty(storageId))
                {
                    dt = GroupInfo.GetGroupIOStorageTotalByYear(beginYear, endYear, 0, "", "", 0, "");//无条件，全部统计
                }
                else
                {
                    if (string.IsNullOrEmpty(buildFloor))
                    {
                        dt = GroupInfo.GetGroupIOStorageTotalByYear(beginYear, endYear, 2, "", storageId, 0, "");//按仓库
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(storageDeatilId))
                        {
                            dt = GroupInfo.GetGroupIOStorageTotalByYear(beginYear, endYear, 3, "", storageId, int.Parse(buildFloor), "");//按楼层
                        }
                        else
                        {
                            dt = GroupInfo.GetGroupIOStorageTotalByYear(beginYear, endYear, 4, "", "", 0, storageDeatilId);//按仓位
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(storageId))
                {
                    dt = GroupInfo.GetGroupIOStorageTotalByYear(beginYear, endYear, 1, cargoNmae, "", 0, "");//按品名
                }
                else
                {
                    if (string.IsNullOrEmpty(buildFloor))
                    {
                        dt = GroupInfo.GetGroupIOStorageTotalByYear(beginYear, endYear, 5, cargoNmae, storageId, 0, "");//按品名仓库
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(storageDeatilId))
                        {
                            dt = GroupInfo.GetGroupIOStorageTotalByYear(beginYear, endYear, 6, cargoNmae, storageId, int.Parse(buildFloor), "");//按品名楼层
                        }
                        else
                        {
                            dt = GroupInfo.GetGroupIOStorageTotalByYear(beginYear, endYear, 7, cargoNmae, "", 0, storageDeatilId);//按品名仓位
                        }
                    }
                }
            }

            #endregion

            Series series1 = chart2.Series[0];
            Series series2 = chart2.Series[1];        //数据集声明  

            // DateTime date1 = DateTime.Now.Date;
            for (int i = 0; i < dt.Rows.Count; i++)      //向数据集绑定数据
            {
                series1.Points.AddXY(dt.Rows[i]["year"].ToString() + "年", dt.Rows[i]["inTotalWeight"].ToString());
                series2.Points.AddXY(dt.Rows[i]["year"].ToString() + "年", dt.Rows[i]["outTotalWeight"].ToString());
                //  date1 = date1.AddDays(1);
            }
            while (this.chart2.Series[0].Points.Count > dt.Rows.Count)
            {
                // Remove series points
                foreach (Series s in this.chart2.Series)
                {
                    s.Points.RemoveAt(0);
                }
            }
            string title = string.Empty;
            if (beginYear == endYear)
                title = beginYear.ToString();
            else
                title = beginYear.ToString() + "至" + endYear.ToString();
            chart2.Titles.Clear();
            Title t1 = new Title(title + "粮食出入库情况", Docking.Top, new System.Drawing.Font("微软雅黑", 16), System.Drawing.Color.FromArgb(26, 59, 105));//图片标题
            chart2.Titles.Add(t1);
            chart2.ChartAreas["ChartArea1"].AxisY.Title = "重量（单位：吨）";
            chart2.ChartAreas["ChartArea1"].AxisX.Title = "年度";
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Error", "alert('异常操作！')", true);
        }
        ltnToExcle.Visible = false;
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Show", "document.getElementById('divtoexcel').style.display='none';", true);
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
