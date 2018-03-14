using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Invengo.RiceManangeServices.Utility;
using Invengo.RiceManangeServices.Model;

public partial class UserManage_User_UserEdit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 初始化下拉框

            System.Data.DataTable dtUserGroup = Common.GetObjectList("UserGroup");
            ViewState["UserGroup"] = dtUserGroup;
            ddlUserGroup.DataSource = dtUserGroup;
            ddlUserGroup.DataValueField = "UserGroupID";
            ddlUserGroup.DataTextField = "UserGroupName";
            ddlUserGroup.DataBind();
            ddlUserGroup.Items.Insert(0, new ListItem("请选择", ""));

            ddlWebRole.DataSource = Common.GetObjectList("Role");
            ddlWebRole.DataValueField = "RoleID";
            ddlWebRole.DataTextField = "RoleName";
            ddlWebRole.DataBind();
            ddlWebRole.Items.Insert(0, new ListItem("请选择", ""));


            #endregion

            if (Request.QueryString["Type"] != null &&
                Request.QueryString["Type"].ToString() == "0")
            {
                //新增
                lblMsg.Text = "新增用户";
                lblRemark.Visible = true;
            }
            else
            {
                lblMsg.Text = "更新用户";
                txtJobNo.ReadOnly = true;

                if (Request.QueryString["UserId"] != null)
                {
                    int userId = int.Parse(Request.QueryString["UserId"].ToString());
                    System.Data.DataTable dt = Common.GetObjectList("User",userId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.txtJobNo.Text = dt.Rows[0]["JobNo"] as string;
                        this.txtRealName.Text = dt.Rows[0]["RealName"] as string;
                        this.txtJobDuty.Text = dt.Rows[0]["JobDuty"] as string;

                        if (int.Parse(dt.Rows[0]["UserGroupID"].ToString())>0)
                        {
                            this.ddlUserGroup.SelectedValue = dt.Rows[0]["UserGroupID"].ToString();
                        }

                        if (int.Parse(dt.Rows[0]["RoleID"].ToString())>0)
                        {
                            this.ddlWebRole.SelectedValue = dt.Rows[0]["RoleID"].ToString();
                            this.panWebRole.Visible = true;
                        }
                        ViewState["UserID"] = dt.Rows[0]["UserID"];
                        ViewState["Password"] = dt.Rows[0]["Password"];
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
            userPrimary.PrimaryName = "JobNo";
            userPrimary.PrimaryValue = GetUserInfo().JobNo;
            t.Add(userPrimary);
            if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "0")//新增状态
            {
                if (Common.IsExsit("User", t))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('用户已经存在，请确认！');", true);
                }
                else
                {
                    if (UserManage.AddUser(GetUserInfo()))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Sucess",
                            "alert('操作成功！');location.replace('UserList.aspx?Refresh=1');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('操作失败！');", true);
                    }
                }
            }
            else
            {
                if (Common.IsExsit("User", t, GetUserInfo().UserID))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('用户已经存在，请确认！');", true);
                }
                else
                {
                    if (UserManage.UpdateUser(GetUserInfo()))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Sucess",
                            "alert('操作成功！');location.replace('UserList.aspx?Refresh=1');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('操作失败！');", true);
                    }
                }
            }
        }
    }
    private UserInfo GetUserInfo()
    {
        UserInfo userInfo = new UserInfo();
        userInfo.JobNo = txtJobNo.Text.Trim();
        if (Request.QueryString["Type"] != null &&
            Request.QueryString["Type"].ToString() == "1")//编辑状态
        {
            if (ViewState["UserID"] != null)
            {
                userInfo.UserID = int.Parse(ViewState["UserID"].ToString());
            }
            if (ViewState["Password"] != null)
            {
                userInfo.Password = ViewState["Password"].ToString();
            }
        }
        else
        {
            userInfo.Password = SHA1.GetSHA1Password(this.txtJobNo.Text.Trim());
        }
        userInfo.JobDuty = this.txtJobDuty.Text.Trim();
        userInfo.RealName = this.txtRealName.Text.Trim();
        if (this.ddlUserGroup.Text != "请选择")
        {
            userInfo.UserGroupID = int.Parse(this.ddlUserGroup.SelectedValue.ToString());
        }
        else
        {
            userInfo.UserGroupID = 0;
        }
        if (this.ddlWebRole.Text != "请选择" && this.panWebRole.Visible)
        {
            userInfo.RoleID = int.Parse(this.ddlWebRole.SelectedValue.ToString());
        }
        else
        {
            userInfo.RoleID = 0;
        }
        return userInfo;
    }
    protected void ddlUserGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Data.DataTable dt = null;
        if (ViewState["UserGroup"] != null)
        {
            dt = ViewState["UserGroup"] as System.Data.DataTable;
            dt.PrimaryKey = new System.Data.DataColumn[1] { dt.Columns["UserGroupID"] };
        }
        if (dt == null) return;
        string userGroupID = ddlUserGroup.SelectedValue;
        if (!string.IsNullOrEmpty(userGroupID))
        {
            System.Data.DataRow drFind = dt.Rows.Find(userGroupID);
            if (drFind == null) return;
            if (bool.Parse(drFind["HasWebsiteRole"].ToString()))
            {
                this.panWebRole.Visible = true;
            }
            else
            {
                this.panWebRole.Visible = false;
            }
        }
        else
        {
            this.panWebRole.Visible = false;
        }
    }
}
