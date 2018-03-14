using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Invengo.RiceManangeServices.Utility;

public partial class SalverByStorage_SalverByStorageListCheck : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlStorage.DataSource = Storage.GetStorage();
            ddlStorage.DataTextField = "WHName";
            ddlStorage.DataValueField = "WHID";
            ddlStorage.DataBind();
            ddlStorage.Items.Insert(0, new ListItem("--选择--", ""));
        }
    }

    private void LoadData(int page)
    {
        int recordNum = 0;
        int pageSize = int.MaxValue;
        ViewState["STOREAEGRAIN"] = Grain.QueryGrainListByStorage(QueryCondition(), pageSize, page, out recordNum);
        var query = from WareHouseByWHID in Common.GetObjectList("View_WarePlace").AsEnumerable()
                               where WareHouseByWHID.Field<string>("WHID") == ddlStorage.SelectedValue
                    orderby WareHouseByWHID.Field<string>("WPNumber")
                               select WareHouseByWHID;
        GridView1.DataSource = query.AsDataView();
        GridView1.DataBind();
    }

    private Dictionary<string, string> QueryCondition()
    {
        Dictionary<string, string> queryCondition = new Dictionary<string, string>();
        queryCondition.Add("WPNumber", "");
        queryCondition.Add("MaterailNumber", "");
        queryCondition.Add("WPGLot", "");
        queryCondition.Add("WHID", ddlStorage.SelectedValue);

        return queryCondition;
    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor;this.style.backgroundColor='" + OnMouseBackColor + "';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e;");

            string wpNumber = e.Row.Cells[3].Text;
            string storageGrainInfo = string.Empty;
            string storageGrainInfoEAS = string.Empty;

            DataTable dtStorageGrain = (DataTable)ViewState["STOREAEGRAIN"];
            DataRow[] drs = dtStorageGrain.Select("WPNumber='" + wpNumber + "'", "");
            foreach (DataRow dr in drs)
            {
                storageGrainInfo += dr["MaterialNumber"].ToString() + "&nbsp;" +
                                    dr["MaterialName"].ToString() + "&nbsp;" +
                                    Math.Round(Convert.ToDouble(dr["WPGBaseQty"]), 3).ToString() + "(KG)<br/>";
            }

            string[] storageGrainEAS = (string[])ViewState["STOREAGEGRAIEAS"];
            foreach (string info in storageGrainEAS)
            {
                string materialNumberEAS = info.Split('~')[3];
                string materialNameEAS = info.Split('~')[4];
                string wpNumberEAS = info.Split('~')[12];
                string baseQtyEAS = info.Split('~')[18];

                if (wpNumber == wpNumberEAS)
                {
                    storageGrainInfoEAS += materialNumberEAS + "&nbsp;" +
                                           materialNameEAS + "&nbsp;" +
                                           Math.Round(Convert.ToDouble(baseQtyEAS), 3).ToString() + "(KG)<br/>";
                }
            }

            ((Label)e.Row.FindControl("lblWPGBaseQty")).Text = storageGrainInfo;
            ((Label)e.Row.FindControl("lblWPGBaseQtyEAS")).Text = storageGrainInfoEAS;
            if (storageGrainInfo != storageGrainInfoEAS)
            {
                ((Label)e.Row.FindControl("lblWPGBaseQtyEAS")).ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        EASLoginService.EASLoginProxyService service = new EASLoginService.EASLoginProxyService();
        EASLoginService.WSContext context = service.login("user", "", "eas", "shenliang", "L2", 2);
        if (string.IsNullOrEmpty(context.sessionId) == false)
        {
            string WHNumber = Storage.GetStorage().Select("WHID='" + ddlStorage.SelectedValue + "'", "")[0]["WHNumber"].ToString();
            string[] queryParams = new string[] { "", "", "", "", WHNumber, "" };
            WSBillManageFacadeService.WSBillManageFacadeSrvProxyService client = new WSBillManageFacadeService.WSBillManageFacadeSrvProxyService();
            ViewState["STOREAGEGRAIEAS"] = client.getInventory(queryParams);
        }
        LoadData(1);
    }
}
