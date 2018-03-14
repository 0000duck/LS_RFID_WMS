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
using System.Text;

using Invengo.RiceManangeServices.Utility;

public partial class SalverByStorage_GroupStorageInfo : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = Storage.GetStorage();

            TreeNode node = new TreeNode();
            node.Text = "";
            node.Value = "";
            node.Expanded = false;
            TreeView1.Nodes.Add(node);
            foreach (DataRow dr in dt.Select("", "WHName"))
            {
                //#region 计算库目前总粮食存量
                //DataTable dt1 = Storage.GetStorageGrainByStorageId(dr["WHID"].ToString(),string.Empty,string.Empty);
                //double totalWeight = 0;
                //foreach (DataRow dr1 in dt1.Rows)
                //{
                //    totalWeight += Convert.ToDouble(dr1["weight"] is DBNull ? 0 : dr1["weight"]);
                //}
                //#endregion
                node = new TreeNode();
                node.Text = "<img src='../images/storage.png' border=0>" + dr["WHName"].ToString();// +"&nbsp;(" + totalWeight / 1000 + "吨)";
                node.Value = "0," + dr["WHID"].ToString();
                node.Expanded = false;
                TreeView1.Nodes.Add(node);
                CreateChildNodeBuildingFloor(dr["WHID"].ToString(), node);
            }
        }
    }
    private void CreateChildNodeBuildingFloor(string whId, TreeNode parentNode)
    {
        DataTable dt = Storage.GetBuildingFloor(whId);
        foreach (DataRow dr in dt.Rows)
        {
            #region 计算库目前总粮食存量
            DataTable dt1 = Storage.GetStorageGrainByStorageId(whId, dr["floor"].ToString(),string.Empty);
            double totalWeight = 0;
            foreach (DataRow dr1 in dt1.Rows)
            {
                totalWeight += Convert.ToDouble(dr1["weight"] is DBNull ? 0 : dr1["weight"]);
            }
            #endregion
            TreeNode node = new TreeNode();
            node.Text = dr["floor"].ToString() + "楼&nbsp;(" + totalWeight/1000 + "吨)";
            node.Value = "1," + whId + "," + dr["floor"].ToString();
            node.Expanded = true;
            parentNode.ChildNodes.Add(node);
            //CreateChildNodeStorageDetail(whId, dr["floor"].ToString(), node);
        }
    }
    private void CreateChildNodeStorageDetail(string whId, string floor, TreeNode parentNode)
    {
        DataTable dt = Storage.GetStorageDetail(whId, floor, string.Empty);
        foreach (DataRow dr in dt.Rows)
        {
            TreeNode node = new TreeNode();
            node.Text = dr["WPName"].ToString();
            node.Value = "2," + dr["WPID"].ToString();
            node.Expanded = false;
            parentNode.ChildNodes.Add(node);
        }
    }
    protected void TreeView1_OnSelectedNodeChanged(object sender, EventArgs e)
    {
        DateTime beginDate = DateTime.Now.AddDays(-6).Date;
        DateTime endDate = DateTime.Now.Date;

        string selectValue=TreeView1.SelectedValue;
        string level = selectValue.Split(',')[0];

        #region 点击仓库信息

        if (level == "0")
        {
            string whId = selectValue.Split(',')[1];
            DataRow[] drs = Storage.GetStorage().Select("WHID='" + whId+"'");

            lilInfo.Text = "仓库名称：" + drs[0]["WHName"].ToString() +
               "<br/>电话：" + drs[0]["WHTel"].ToString() +
               "<br/>地址：" + drs[0]["WHAddress"].ToString();
            Repeater3.DataSource = Storage.GetStorageGrainByStorageId(whId);
            Repeater3.DataBind();

            DataTable dt = GroupInfo.GetGroupIOStorageTotal(beginDate, endDate, 2, "", whId, 0, string.Empty);

            Series series1 = chart1.Series[0];
            Series series2 = chart1.Series[1];        //数据集声明  

            // DateTime date1 = DateTime.Now.Date;
            for (int i = 0; i < dt.Rows.Count; i++)      //向数据集绑定数据
            {
                series1.Points.AddXY(Convert.ToDateTime(dt.Rows[i]["opardate"]).ToShortDateString(), dt.Rows[i]["inTotalWeight"].ToString());
                series2.Points.AddXY(Convert.ToDateTime(dt.Rows[i]["opardate"]).ToShortDateString(), dt.Rows[i]["outTotalWeight"].ToString());
                //  date1 = date1.AddDays(1);
            }
            while (this.chart1.Series[0].Points.Count > dt.Rows.Count)
            {
                // Remove series points
                foreach (Series s in this.chart1.Series)
                {
                    s.Points.RemoveAt(0);
                }
            }
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "重量（单位：吨）";
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "日期";

        }

        #endregion

        #region 点击楼层信息

        if (level == "1")
        {
            string whId = selectValue.Split(',')[1];
            string floorDigit = selectValue.Split(',')[2];

            lilInfo.Text = "楼层：" + TreeView1.SelectedNode.Text;
            Repeater1.DataSource = Storage.GetStorageDetail(whId, floorDigit, "A");
            Repeater1.DataBind();
            Repeater2.DataSource = Storage.GetStorageDetail(whId, floorDigit, "B");
            Repeater2.DataBind();

            TreeView1.Nodes[0].Selected = true;
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "open", "Open('comment2',250,80)", true);
            return;
        }

        #endregion

        TreeView1.Nodes[0].Selected = true;
        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.GetType(), "open", "Open('comment1',250,80)", true);

    }
    protected void Repeater_OnItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Show")
        {
            string wpId = e.CommandArgument.ToString();

            DataTable dt = Storage.GetStorageDetailById(wpId);
            if (dt != null)
            {
                lilInfo.Text = "仓位编码：" + dt.Rows[0]["WPNumber"].ToString() +
                        "<br/>仓位名称：" + dt.Rows[0]["WPName"].ToString()+
                        "<br/>堆号：" + dt.Rows[0]["Number"].ToString();
            }
            Repeater3.DataSource = Storage.GetStorageGrainByStorageDetailId(wpId);
            Repeater3.DataBind();

            DateTime beginDate = DateTime.Now.AddDays(-6).Date;
            DateTime endDate = DateTime.Now.Date;
            dt = GroupInfo.GetGroupIOStorageTotal(beginDate, endDate, 4, "", "", 0, wpId);

            Series series1 = chart1.Series[0];
            Series series2 = chart1.Series[1];        //数据集声明  

            // DateTime date1 = DateTime.Now.Date;
            for (int i = 0; i < dt.Rows.Count; i++)      //向数据集绑定数据
            {
                series1.Points.AddXY(Convert.ToDateTime(dt.Rows[i]["opardate"]).ToShortDateString(), dt.Rows[i]["inTotalWeight"].ToString());
                series2.Points.AddXY(Convert.ToDateTime(dt.Rows[i]["opardate"]).ToShortDateString(), dt.Rows[i]["outTotalWeight"].ToString());
                //  date1 = date1.AddDays(1);
            }
            while (this.chart1.Series[0].Points.Count > dt.Rows.Count)
            {
                // Remove series points
                foreach (Series s in this.chart1.Series)
                {
                    s.Points.RemoveAt(0);
                }
            }
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "重量（单位：吨）";
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "日期";
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "open", "Open('comment1',260,120);$('comment2').style.display='block';", true);

        }
    }
}
