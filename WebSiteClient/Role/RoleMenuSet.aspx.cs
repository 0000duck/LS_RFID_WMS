using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Invengo.RiceManangeServices.Utility;
using Invengo.RiceManangeServices.Model;

public partial class Role_RoleMenuSet : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //初始化菜单树形列表
            DataTable dt = Common.GetObjectList("Menu");
            if (dt != null && dt.Rows.Count > 0)
            {
                InitialTreeView(dt);
            }

            //初始化角色名称
            if (Request.QueryString["RoleID"] != null)
            {
                int RoleID = int.Parse(Request.QueryString["RoleID"].ToString());
                lblMsg.Text = Common.GetObjectList("Role").Rows[0]["RoleName"].ToString() + ">>权限设定";
            }                
        }
    }
    public void InitialTreeView(DataTable dt)
    {
        DataTable dtUserRoleMenu = null;
        int roleID = -1;
        if (Request.QueryString["RoleID"] != null)
        {
            roleID = int.Parse(Request.QueryString["RoleID"].ToString());
        }
        dtUserRoleMenu = UserRoleMenu.QueryUserRoleMenu(roleID);

        DataRow[] drs = dt.Select("level=1", "orderno");//查询第一级的菜单
        if (drs == null || drs.Length == 0)
            return;
        foreach (DataRow dr in drs)
        {
            TreeNode node = new TreeNode();
            node.Text = dr["Name"].ToString();
            node.Value = dr["MenuID"].ToString();
            string menuID = dr["MenuID"].ToString();
            DataRow[] drsChild = dt.Select("ParentMenuID=" + menuID, "orderno");
            foreach (DataRow drC in drsChild)
            {
                TreeNode nodeChild = new TreeNode();
                nodeChild.Text = drC["Name"].ToString();
                nodeChild.Value = drC["MenuID"].ToString();
                if (dtUserRoleMenu != null)
                {
                    DataRow[] drsFind = dtUserRoleMenu.Select("MenuID=" + drC["MenuID"].ToString(), "MenuID");
                    if (drsFind != null && drsFind.Length > 0) nodeChild.Checked = true;
                }
                node.ChildNodes.Add(nodeChild);
            }
            trvMenu.Nodes.Add(node);
        }
    }
    protected void btnConfrim_Click(object sender, EventArgs e)
    {
        List<UserMenuRoleInfo> list = new List<UserMenuRoleInfo>();
        int roleID = -1;
        if (Request.QueryString["RoleID"] != null)
        {
            roleID = int.Parse(Request.QueryString["RoleID"].ToString());
        }
        foreach (TreeNode node in trvMenu.Nodes)
        {
            bool hasChildnodeChecked = false;
            foreach (TreeNode nodeChild in node.ChildNodes)
            {
                if (nodeChild.Checked)
                {
                    UserMenuRoleInfo menuRoleInfo = new UserMenuRoleInfo();
                    menuRoleInfo.MenuID = int.Parse(nodeChild.Value);
                    menuRoleInfo.RoleID = roleID;
                    hasChildnodeChecked = true;
                    list.Add(menuRoleInfo);
                }
            }
            if (hasChildnodeChecked)//if the childnodes is choose,the parentnode is choose too
            {
                UserMenuRoleInfo pMenuRoleInfo = new UserMenuRoleInfo();
                pMenuRoleInfo.RoleID = roleID;
                pMenuRoleInfo.MenuID = int.Parse(node.Value);
                list.Add(pMenuRoleInfo);
            }
        }
        
        if (Role.SetUserRoleMenu(roleID, list))
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "Success",
                "alert('授权成功！');location.replace('RoleList.aspx');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "Fail", "alert('授权失败！');location.replace('RoleList.aspx');", true);
        }
    }
}
