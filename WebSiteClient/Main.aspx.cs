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
using System.Web.Security;
using System.Text;

public partial class Main : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataLoad();
        }
    }
    private void DataLoad()
    {
        DateTime beginDate = DateTime.Now.AddDays(-6).Date;
        DateTime endDate = DateTime.Now.Date;
        DataTable dt = new DataTable();
        //if (Cache["IOGrainTable"] == null)
        //{
            dt = GroupInfo.GetGroupIOStorageTotal(beginDate, endDate, 0, "", "", 0, "");
        //    Cache.Insert("IOGrainTable", dt, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
        //}
        //else
        //{
        //    dt = (DataTable)Cache["IOGrainTable"];
        //}

        Series series1 = chart1.Series[0];
        Series series2 = chart1.Series[1];        //数据集声明  

        for (int i = 0; i < dt.Rows.Count; i++)      //向数据集绑定数据
        {
            series1.Points.AddXY(Convert.ToDateTime(dt.Rows[i]["opardate"]).ToShortDateString(), dt.Rows[i]["inTotalWeight"].ToString());
            series1.Color = System.Drawing.Color.DodgerBlue;
            series2.Points.AddXY(Convert.ToDateTime(dt.Rows[i]["opardate"]).ToShortDateString(), dt.Rows[i]["outTotalWeight"].ToString());
            series2.Color = System.Drawing.Color.Orange;
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

        DataTable dt1= GroupInfo.GetGroupCurrentInfo();
        
        //数据集""显示属性设置
        Series series3 = chart2.Series[0];        //数据集声明  
        series3.ToolTip = "#LEGENDTEXT: #VAL{C} million";
        //series6.Label = "#PERCENT{P}";
        series3.Points.DataBind(dt1.Select("type=0"), "state", "num", "LegendText=state,YValues=num,ToolTip=state");
        series3.Points[0].Color = System.Drawing.Color.DodgerBlue;
        series3.Points[1].Color = System.Drawing.Color.Orange;

        //数据集""显示属性设置
        Series series4 = chart3.Series[0];        //数据集声明  
        series4.ToolTip = "#LEGENDTEXT: #VAL{C} million";
        //series6.Label = "#PERCENT{P}";
        series4.Points.DataBind(dt1.Select("type=1"), "state", "num", "LegendText=state,YValues=num,ToolTip=state");
        series4.Points[0].Color = System.Drawing.Color.DodgerBlue;
        series4.Points[1].Color = System.Drawing.Color.Orange;

        //数据集""显示属性设置
        Series series5 = chart4.Series[0];        //数据集声明  
        series5.ToolTip = "#LEGENDTEXT: #VAL{C} million";
        //series6.Label = "#PERCENT{P}";
        series5.Points.DataBind(dt1.Select("type=2"), "state", "num", "LegendText=state,YValues=num,ToolTip=state");
        series5.Points[0].Color = System.Drawing.Color.DodgerBlue;
        series5.Points[1].Color = System.Drawing.Color.Orange;
    }
    protected void lbtExit_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        FormsAuthentication.RedirectToLoginPage();
    }
}
