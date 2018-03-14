using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Invengo.RiceManangeServices.Utility;
using Invengo.RiceManangeServices.Model;

public partial class UserManage_UserGroup_UserGroupEdit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Type"] != null &&
                Request.QueryString["Type"].ToString() == "0")
            {
                //新增
                lblMsg.Text = "新增用户组";
            }
            else
            {
                lblMsg.Text = "更新用户组";
                txtUserGroupName.ReadOnly = true;
                int userGroupID = -1;
                if (Request.QueryString["UserGroupID"] != null)
                {
                    userGroupID = int.Parse(Request.QueryString["UserGroupID"].ToString());

                    DataTable dt = Common.GetObjectList("UserGroup", userGroupID);
                    if (dt.Rows.Count > 0)
                    {
                        this.txtUserGroupName.Text = dt.Rows[0]["UserGroupName"].ToString();
                        if (Convert.ToBoolean(dt.Rows[0]["HasGateSysRole"]))
                        {
                            this.rblHasGateSysRole.SelectedIndex = 0;
                        }
                        else
                        {
                            this.rblHasGateSysRole.SelectedIndex = 1;
                        }

                        if (Convert.ToBoolean(dt.Rows[0]["HasHandsetRole"]))
                        {
                            this.rblHasHangsetRole.SelectedIndex = 0;
                        }
                        else
                        {
                            this.rblHasHangsetRole.SelectedIndex = 1;
                        }

                        if (Convert.ToBoolean(dt.Rows[0]["HasLoadCarRole"]))
                        {
                            this.rblHasLoadCarRole.SelectedIndex = 0;
                        }
                        else
                        {
                            this.rblHasLoadCarRole.SelectedIndex = 1;
                        }

                        if (Convert.ToBoolean(dt.Rows[0]["HasWebsiteRole"]))
                        {
                            this.rblHasWebsiteRole.SelectedIndex = 0;
                        }
                        else
                        {
                            this.rblHasWebsiteRole.SelectedIndex = 1;
                        }
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
            userPrimary.PrimaryName = "UserGroupName";
            userPrimary.PrimaryValue = GetUserGroupInfo().UserGroupName;
            t.Add(userPrimary);
            if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "0")//新增状态
            {
                if (Common.IsExsit("UserGroup", t))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('用户组已经存在，请确认！');", true);
                }
                else
                {
                    if (UserManage.AddUserGroup(GetUserGroupInfo()))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Sucess",
                            "alert('操作成功！');location.replace('UserGroupList.aspx?Refresh=1');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('操作失败！');", true);
                    }
                }
            }
            else
            {
                if (Common.IsExsit("UserGroup", t, GetUserGroupInfo().UserGroupID))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('用户组已经存在，请确认！');", true);
                }
                else
                {
                    if (UserManage.UpdateUserGroup(GetUserGroupInfo()))
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Sucess",
                            "alert('操作成功！');location.replace('UserGroupList.aspx?Refresh=1');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Fail", "alert('操作失败！');", true);
                    }
                }
            }
        }
    }

    private UserGroupInfo GetUserGroupInfo()
    {
        UserGroupInfo userGroupInfo = new UserGroupInfo();
        Helper.SetEntityDefaultValue(userGroupInfo);
        userGroupInfo.UserGroupName = this.txtUserGroupName.Text;
        if (Request.QueryString["Type"] != null &&
            Request.QueryString["Type"].ToString() == "1")//编辑状态
        {
            if (Request.QueryString["UserGroupID"] != null)
            {
                userGroupInfo.UserGroupID = int.Parse(Request.QueryString["UserGroupID"].ToString());
            }
        }
        if (rblHasGateSysRole.SelectedIndex == 0)
        {
            userGroupInfo.HasGateSysRole = true;
        }
        else
        {
            userGroupInfo.HasGateSysRole = false;
        }

        if (this.rblHasHangsetRole.SelectedIndex == 0)
        {
            userGroupInfo.HasHandsetRole = true;
        }
        else
        {
            userGroupInfo.HasHandsetRole = false;
        }

        if (this.rblHasLoadCarRole.SelectedIndex == 0)
        {
            userGroupInfo.HasLoadCarRole = true;
        }
        else
        {
            userGroupInfo.HasLoadCarRole = false;
        }

        if (this.rblHasWebsiteRole.SelectedIndex == 0)
        {
            userGroupInfo.HasWebsiteRole = true;
        }
        else
        {
            userGroupInfo.HasWebsiteRole = false;
        }

        return userGroupInfo;
    }
}
