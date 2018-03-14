using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Invengo.RiceManangeServices.Utility;

public partial class TruckEir_TaskView : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        DataLoad();
    }
    private void DataLoad()
    {
        lblTime.Text = "监控时间："+DateTime.Now.ToString();
        GridView1.DataSource = Grain.GetTask();
        GridView1.DataBind();
    }
    protected double GetProcess(object totalSalverCount, object processSalverCount)
    {
        double reValue = 0;
        try
        {
            double totalDoubleWeight = Convert.ToDouble(totalSalverCount is DBNull ? 0 : totalSalverCount)*1000;
            double presentDoubleweight = Convert.ToDouble(processSalverCount is DBNull ? 0 : processSalverCount) * 1000;
            double process = presentDoubleweight / totalDoubleWeight;
            if (process >= 1)
            {
                reValue = 100;
            }
            else
            {
                reValue = Math.Round(process*100, 1);
            }
        }
        catch
        {
            reValue = 0;
        }
        return reValue;
    }
}
