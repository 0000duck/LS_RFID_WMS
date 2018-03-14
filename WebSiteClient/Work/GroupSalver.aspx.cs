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

public partial class Work_GroupSalver : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DateTime beginDate = Convert.ToDateTime(txtPlanStartTime.Text.Trim()+" 00:00:00");
        DateTime endDate = Convert.ToDateTime(txtPlanEndTime.Text.Trim()+" 23:59:59");

        DataTable dt=GroupInfo.GetGroupSalver(beginDate, endDate);
        //GridView1.DataSource = dt;
        //GridView1.DataBind();

        Series series1 = chart2.Series[0];
        Series series2 = chart2.Series[1];        //数据集声明  

        // DateTime date1 = DateTime.Now.Date;
        for (int i = 0; i < dt.Rows.Count; i++)      //向数据集绑定数据
        {
            series1.Points.AddXY(Convert.ToDateTime(dt.Rows[i]["opardate"]).ToShortDateString(), dt.Rows[i]["useTotal"].ToString());
            series2.Points.AddXY(Convert.ToDateTime(dt.Rows[i]["opardate"]).ToShortDateString(), dt.Rows[i]["freeTotal"].ToString());
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
        chart2.ChartAreas["ChartArea1"].AxisY.Title = "数量（单位：个）";
        chart2.ChartAreas["ChartArea1"].AxisX.Title = "日期";
        System.Threading.Thread.Sleep(2000);
    }
}
