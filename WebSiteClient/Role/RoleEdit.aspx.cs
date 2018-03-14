using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Invengo.RiceManangeServices.Utility;
using Invengo.RiceManangeServices.Model;

public partial class Role_RoleEdit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Type"] != null &&
                Request.QueryString["Type"].ToString() == "0")
            {
                //新增
                lblMsg.Text = "新增网站角色";
            }
            else
            {
                lblMsg.Text = "修改网站角色";
                if (Request.QueryString["RoleID"] != null)
                {
                    int RoleId = int.Parse(Request.QueryString["RoleID"].ToString());
                    DataTable dt = Role.QueryRoleById(RoleId);
                    if (dt.Rows.Count > 0)
                    {
                        txtRoleName.Text = dt.Rows[0]["RoleName"].ToString();
                        txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                    }
                }
            }
        }
    }

    protected void btnConfrim_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            List<TablePrimary> t = new List<TablePrimary>();
            TablePrimary userPrimary = new TablePrimary();
            userPrimary.PrimaryName = "RoleName";
            userPrimary.PrimaryValue = GetRoleInfo().RoleName;
            t.Add(userPrimary);
            if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "0")//新增状态
            {
                if (Common.IsExsit("Role", t))
                {

                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('角色已经存在，请确认！');", true);
                }
                else
                {
                    bool iResult = Role.Add(GetRoleInfo().RoleName, GetRoleInfo().Remark);
                    if (iResult)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(),
                            "Sucess", "alert('操作成功！');location.replace('RoleList.aspx?Refresh=1');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('操作失败！');", true);
                    }
                }
            }
            else
            {
                if (Common.IsExsit("Role", t, GetRoleInfo().RoleID))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('角色已经存在，请确认！');", true);
                }
                else
                {
                    bool bResult = Role.Update(GetRoleInfo().RoleName, GetRoleInfo().Remark, GetRoleInfo().RoleID);
                    if (bResult)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(),
                            "Sucess", "alert('操作成功！');location.replace('RoleList.aspx?Refresh=1');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('操作失败！');", true);
                    }
                }
            }
        }
    }

    private RoleInfo GetRoleInfo()
    {
        RoleInfo roleInfo = new RoleInfo();
        roleInfo.RoleName = txtRoleName.Text;
        roleInfo.Remark = txtRemark.Text;
        if (Request.QueryString["Type"] != null &&
            Request.QueryString["Type"].ToString() == "1")//编辑状态
        {
            if (Request.QueryString["RoleID"] != null)
            {
                roleInfo.RoleID = int.Parse(Request.QueryString["RoleID"].ToString());
            }
        }
        return roleInfo;
    }
}
