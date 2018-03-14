using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Invengo.RiceManangeServices.Model;
using Invengo.RiceManangeServices.DBCommon;

/// <summary>
///Role 的摘要说明
/// </summary>
public class Role
{
	public Role()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    ///<summary>
    /// 查询角色列表
    ///</summary>
    public static DataTable QueryRoleList(string roleName, int pageSize, int currentPage, out int recordNum)
    {
        recordNum = 0;

        #region 处理查询语句
        string sqlSelect = @"select r.* , Row_Number() Over(Order By r.RoleId) AS serialNum ";

        string sqlFrom = @"from Role r where 1=1";
        #endregion

        #region 处理参数
        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();
        if (roleName != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = null;

            sqlFrom += " and r.RoleName like '%'+@RoleName+'%'";
            sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "RoleName";
            sqlPara.Value = roleName;
            listSqlPara.Add(sqlPara);
        }
        #endregion

        string sqlCount = @"select count(1) " + sqlFrom;
        string sqlQuery = @"select * from (" + sqlSelect + " " + sqlFrom + ") t " +
                   @"where t.serialNum>"+pageSize*(currentPage-1)+" and t.serialNum<="+pageSize*currentPage;
        return Common.GetObjectList(sqlCount, sqlQuery, listSqlPara, out recordNum);
    }

    ///<summary>
    /// 通过ID查询角色
    ///</summary>
    public static DataTable QueryRoleById(int id)
    {
        int recordNum = 0;
        string sqlQuery = "select * from Role where RoleId="+id ;
        return Common.GetObjectList(string.Empty, sqlQuery, null, out recordNum);
    }

    ///<summary>
    /// 添加角色
    ///</summary>
    public static bool Add(string roleName,string remark)
    {
        string sql = @"insert into role values(@RoleName,@Remark)";

        #region 处理参数
        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();
        if (roleName != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "RoleName";
            sqlPara.Value = roleName;
            listSqlPara.Add(sqlPara);
        }
        if (remark != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "Remark";
            sqlPara.Value = remark;
            listSqlPara.Add(sqlPara);
        }
        return Common.ExecNonQuerySql(sql, listSqlPara);
        #endregion
    }

    ///<summary>
    /// 修改角色
    ///</summary>
    public static bool Update(string roleName, string remark,int id)
    {
        string sql = @"update role set RoleName=@RoleName,Remark=@Remark where RoleId="+id;

        #region 处理参数
        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();
        if (roleName != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "RoleName";
            sqlPara.Value = roleName;
            listSqlPara.Add(sqlPara);
        }
        if (remark != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "Remark";
            sqlPara.Value = remark;
            listSqlPara.Add(sqlPara);
        }
        return Common.ExecNonQuerySql(sql, listSqlPara);
        #endregion
    }

    ///<summary>
    /// 删除角色
    ///</summary>
    public static bool Delete(int id)
    {
        string sql = @"delete role where RoleId="+id;

        #region 处理参数
        return Common.ExecNonQuerySql(sql, null);
        #endregion
    }

    /// <summary>
    /// 修改网站角色的菜单权限
    /// </summary>
    /// <param name="listMenuRole"></param>
    /// <returns></returns>
    public static bool SetUserRoleMenu(int roleID, List<UserMenuRoleInfo> listMenuRole)
    {
        if (listMenuRole == null && listMenuRole.Count == 0) return false;
        try
        {
            List<string> cmdTextList = new List<string>();
            List<CommandType> cmdTypeList = new List<CommandType>();

            //根据网站角色的ID删除所有对应的权限
            string sqlDelUserRole = "Delete From UserMenuRole Where RoleID=" + roleID;
            cmdTextList.Add(sqlDelUserRole);
            cmdTypeList.Add(CommandType.Text);

            //新增网站角色的菜单权限
            for (int i = 0; i < listMenuRole.Count; i++)
            {
                string sqlInsertUserRole = "insert into UserMenuRole(RoleID,MenuID)"
                    + "Values(" + listMenuRole[i].RoleID + "," + listMenuRole[i].MenuID + ")";
                cmdTextList.Add(sqlInsertUserRole);
                cmdTypeList.Add(CommandType.Text);
            }

            //用事务执行
            DataManager.ExecuteNoQueryTran(cmdTextList, cmdTypeList);//抛出错误代表执行错误，其它代表执行成功

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
