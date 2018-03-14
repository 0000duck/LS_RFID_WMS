using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Invengo.RiceManangeServices.DBCommon;
using Invengo.RiceManangeServices.Utility;
using Invengo.RiceManangeServices.Model;
/// <summary>
///UserRoleMenu 的摘要说明
/// </summary>
public class UserRoleMenu
{
    public UserRoleMenu()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 根据RoleID（网站角色）返回能返回的网站菜单
    /// </summary>
    /// <param name="roleID"></param>
    /// <param name="retValue"></param>
    /// <returns></returns>
    public static DataTable QueryUserRoleMenu(int roleID)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);
        string sql = null;
        sql = @"select M.*
            from [Role] AS R
            Inner Join UserMenuRole MR
            On MR.RoleID=R.RoleID
            Inner Join Menu M
            On M.MenuID=MR.MenuID
            Where R.RoleID=@RoleID";
        cmd.ClearParameters();
        cmd.setCommandText(sql);
        cmd.AddParameter("RoleID", roleID);
        DataSet ds = dm.ExecDataSetCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 判断是否能删用户组
    /// </summary>
    /// <returns></returns>
    public static bool IsCanDelUserGroup(string userGroupID)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select count(*) from [user] where usergroupid=@userGroupID";
        cmd.ClearParameters();
        cmd.AddParameter("@userGroupID", userGroupID);
        cmd.setCommandText(sql);
        if (Convert.ToInt32(dm.ExecScalarCommand(cmd)) > 0)
            return false;
        else
            return true;
    }

    /// <summary>
    /// 判断是否能删网站角色
    /// </summary>
    /// <returns></returns>
    public static bool IsCanDelRole(string roleID)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select count(*) from [user] where roleid=@roleID";
        cmd.ClearParameters();
        cmd.AddParameter("@roleID", roleID);
        cmd.setCommandText(sql);
        if (Convert.ToInt32(dm.ExecScalarCommand(cmd)) > 0)
            return false;
        else
            return true;
    }

    /// <summary>
    ///查询用户列表信息
    /// </summary>
    /// <param name="info">UserInfo实体类</param>
    /// <param name="pageSize">网页列表一次最多显示多少行记录</param>
    /// <param name="currentPage">用户查询的第几页信息</param>
    /// <param name="sortInfo">排列的列名实体类，可以指定几个列名，并且可以指定排列是升序还是降序</param>
    /// <param name="isFirstSearch">代表是否第一点击查询(如果是则需要对totalRecNum赋值，否则不需要，这样可以避免每次都去计算记录总数)</param>
    /// <param name="totalRecNum">根据查询条件查询的总记录数</param>
    /// <param name="retValue"返回string(DataSet格式)类型，字段包括用户名称、用户工号、所在用户组等信息</param>
    /// <returns>返回bool类型，错误返回false，正确返回true</returns>
    public static bool QueryUserList(UserInfo info, int pageSize, int currentPage, SortInfo sortInfo, bool isFirstSearch, out int totalRecNum, out string retValue)
    {
        totalRecNum = 0;
        retValue = null;
        #region 处理查询语句和排序语句
        string sqlSelect = @"select U.UserID,U.JobNo,U.PassWord,U.RealName,U.JobDuty,
            U.UserGroupID,U.RoleID,R.RoleName,UG.UserGroupName ";
        string[] sortColumns = null;
        if (sortInfo != null && !string.IsNullOrEmpty(sortInfo.SortSql))
        {
            sortColumns = sortInfo.SortSql.Split(',');
            string sqlSortIn = null;
            if (sortColumns != null && sortColumns.Length > 0)
            {
                foreach (string sortItem in sortColumns)
                {
                    sqlSortIn += sortItem + ",";
                }
                sqlSortIn = sqlSortIn.Substring(0, sqlSortIn.Length - 1);
            }
            sqlSelect += ",Row_Number() Over(Order By " + sqlSortIn + ") AS serialNum";
        }
        else
        {
            sqlSelect += ",Row_Number() Over(Order By UserID) AS serialNum";
        }
        string sqlFrom = @" from [User] U
            Left Join UserGroup UG
            On U.UserGroupID=UG.UserGroupID
            Left Join [Role] R
            On U.RoleID=R.RoleID 
            Where 1=1 ";
        #endregion

        #region 处理参数

        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();

        if (info != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = null;

            if (!string.IsNullOrEmpty(info.JobNo))
            {
                sqlFrom += " AND JobNo like '%'+@JobNo+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "JobNo";
                sqlPara.Value = info.JobNo;
                listSqlPara.Add(sqlPara);
            }
        }

        #endregion

        return ExcutePage.ExcutePageCommand(sqlSelect, sqlFrom, sortColumns, pageSize, currentPage, listSqlPara, isFirstSearch, out totalRecNum, out retValue);
    }

    /// <summary>
    ///查询用户组列表信息
    /// </summary>
    /// <param name="info">UserGroupInfo实体类</param>
    /// <param name="pageSize">网页列表一次最多显示多少行记录</param>
    /// <param name="currentPage">用户查询的第几页信息</param>
    /// <param name="sortInfo">排列的列名实体类，可以指定几个列名，并且可以指定排列是升序还是降序</param>
    /// <param name="isFirstSearch">代表是否第一点击查询(如果是则需要对totalRecNum赋值，否则不需要，这样可以避免每次都去计算记录总数)</param>
    /// <param name="totalRecNum">根据查询条件查询的总记录数</param>
    /// <param name="retValue"返回string(DataSet格式)类型，字段包括用户组名称、是否有手持机权限等等信息</param>
    /// <returns>返回bool类型，错误返回false，正确返回true</returns>
    public static bool QueryUserGroupList(UserGroupInfo info, int pageSize, int currentPage, SortInfo sortInfo, bool isFirstSearch, out int totalRecNum, out string retValue)
    {
        totalRecNum = 0;
        retValue = null;
        #region 处理查询语句和排序语句
        string sqlSelect = @"select UserGroupID,UserGroupName,HasHandSetRole,HasLoadCarRole,
            HasGateSysRole,HasWebSiteRole";
        string[] sortColumns = null;
        if (sortInfo != null && !string.IsNullOrEmpty(sortInfo.SortSql))
        {
            sortColumns = sortInfo.SortSql.Split(',');
            string sqlSortIn = null;
            if (sortColumns != null && sortColumns.Length > 0)
            {
                foreach (string sortItem in sortColumns)
                {
                    sqlSortIn += sortItem + ",";
                }
                sqlSortIn = sqlSortIn.Substring(0, sqlSortIn.Length - 1);
            }
            sqlSelect += ",Row_Number() Over(Order By " + sqlSortIn + ") AS serialNum";
        }
        else
        {
            sqlSelect += ",Row_Number() Over(Order By UserGroupID) AS serialNum";
        }
        string sqlFrom = @"from UserGroup
                Where 1=1 ";
        #endregion

        #region 处理参数

        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();

        if (info != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = null;

            if (!string.IsNullOrEmpty(info.UserGroupName))
            {
                sqlFrom += " AND UserGroupName=@UserGroupName";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "UserGroupName";
                sqlPara.Value = info.UserGroupName;
                listSqlPara.Add(sqlPara);
            }
        }

        #endregion

        return ExcutePage.ExcutePageCommand(sqlSelect, sqlFrom, sortColumns, pageSize, currentPage, listSqlPara, isFirstSearch, out totalRecNum, out retValue);
    }
}
