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

public partial class SalverByStorage_GroupSalverByStorageHis : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DateTime beginDate = Convert.ToDateTime(txtPlanStartTime.Text.Trim());
        DateTime endDate = Convert.ToDateTime(txtPlanEndTime.Text.Trim());
        DataTable dt = GroupInfo.GetGroupGrainTotal(beginDate, endDate);
        if (dt.Rows.Count > 0)
        {
            Series series1 = chart2.Series[0];
            for (int i = 0; i < dt.Rows.Count; i++)      //向数据集绑定数据
            {
                series1.Points.AddXY(Convert.ToDateTime(dt.Rows[i]["opardate"]).ToShortDateString(), dt.Rows[i]["total"].ToString());
            }
            while (this.chart2.Series[0].Points.Count > dt.Rows.Count)
            {
                // Remove series points
                foreach (Series s in this.chart2.Series)
                {
                    s.Points.RemoveAt(0);
                }
            }
            chart2.ChartAreas["ChartArea1"].AxisY.Title = "重量（单位：吨）";
            chart2.ChartAreas["ChartArea1"].AxisX.Title = "截至日期";
        }
    }
}
