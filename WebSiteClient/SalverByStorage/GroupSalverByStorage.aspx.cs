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

public partial class SalverByStorage_GroupSalverByStorage : Page
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
        DataTable dt1 = GroupInfo.GetGroupCurrentGrainByCargoName();
        DataTable dt2 = GroupInfo.GetGroupCurrentGrainByStorage();

        Series series1 = chart1.Series[0];        //数据集声明  
        series1.ToolTip = "#LEGENDTEXT: #VAL{C} million";
        series1.Points.DataBind(dt1.Rows, "MaterialName", "weight", "LegendText=MaterialName,YValues=weight,ToolTip=MaterialName");

        Series series2 = chart2.Series[0];        //数据集声明  
        series2.ToolTip = "#LEGENDTEXT: #VAL{C} million";
        series2.Points.DataBind(dt2.Rows, "WHName", "weight", "LegendText=WHName,YValues=weight,ToolTip=WHName");
    }
}
