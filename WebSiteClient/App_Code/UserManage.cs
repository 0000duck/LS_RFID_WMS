using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Invengo.RiceManangeServices.Model;

/// <summary>
///UserManage 的摘要说明
/// </summary>
public class UserManage
{
	public UserManage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    ///<summary>
    /// 添加用户
    ///</summary>
    public static bool AddUser(UserInfo userInfo)
    {
        string sql = @"insert into [User] values(@JobNo,@Password,@RealName,@JobDuty,@UserGroupId,@RoleId)";

        #region 处理参数
        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();
        if (userInfo.JobNo != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "JobNo";
            sqlPara.Value = userInfo.JobNo;
            listSqlPara.Add(sqlPara);
        }
        if (userInfo.Password != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "Password";
            sqlPara.Value = userInfo.Password;
            listSqlPara.Add(sqlPara);
        }
        if (userInfo.RealName != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "RealName";
            sqlPara.Value = userInfo.RealName;
            listSqlPara.Add(sqlPara);
        }
        if (userInfo.JobDuty != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "JobDuty";
            sqlPara.Value = userInfo.JobDuty;
            listSqlPara.Add(sqlPara);
        }
        if (userInfo.UserGroupID != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "UserGroupId";
            sqlPara.Value = userInfo.UserGroupID;
            listSqlPara.Add(sqlPara);
        }
        if (userInfo.RoleID != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "RoleId";
            sqlPara.Value = userInfo.RoleID;
            listSqlPara.Add(sqlPara);
        }
        return Common.ExecNonQuerySql(sql, listSqlPara);
        #endregion
    }

    ///<summary>
    /// 修改用户
    ///</summary>
    public static bool UpdateUser(UserInfo userInfo)
    {
        string sql = @"update [User] set Password=@Password,RealName=@RealName,JobDuty=@JobDuty,UserGroupId=@UserGroupId
                     , RoleId=@RoleId where JobNo=@JobNo";

        #region 处理参数
        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();
        if (userInfo.JobNo != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "JobNo";
            sqlPara.Value = userInfo.JobNo;
            listSqlPara.Add(sqlPara);
        }
        if (userInfo.Password != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "Password";
            sqlPara.Value = userInfo.Password;
            listSqlPara.Add(sqlPara);
        }
        if (userInfo.RealName != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "RealName";
            sqlPara.Value = userInfo.RealName;
            listSqlPara.Add(sqlPara);
        }
        if (userInfo.JobDuty != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "JobDuty";
            sqlPara.Value = userInfo.JobDuty;
            listSqlPara.Add(sqlPara);
        }
        if (userInfo.UserGroupID != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "UserGroupId";
            sqlPara.Value = userInfo.UserGroupID;
            listSqlPara.Add(sqlPara);
        }
        if (userInfo.RoleID != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "RoleId";
            sqlPara.Value = userInfo.RoleID;
            listSqlPara.Add(sqlPara);
        }
        return Common.ExecNonQuerySql(sql, listSqlPara);
        #endregion
    }

    ///<summary>
    /// 修改密码
    ///</summary>
    public static bool ChangePassword(UserInfo userInfo)
    {
        string sql = @"update [User] set Password=@Password where JobNo=@JobNo";

        #region 处理参数
        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();
        if (userInfo.JobNo != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "JobNo";
            sqlPara.Value = userInfo.JobNo;
            listSqlPara.Add(sqlPara);
        }
        if (userInfo.Password != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "Password";
            sqlPara.Value = userInfo.Password;
            listSqlPara.Add(sqlPara);
        }
        return Common.ExecNonQuerySql(sql, listSqlPara);
        #endregion
    }

    ///<summary>
    /// 删除用户
    ///</summary>
    public static bool DeleteUser(int id)
    {
        string sql = @"delete [User] where UserId=" + id;

        #region 处理参数
        return Common.ExecNonQuerySql(sql, null);
        #endregion
    }

    ///<summary>
    /// 添加用户组
    ///</summary>
    public static bool AddUserGroup(UserGroupInfo ugInfo)
    {
        string sql = @"insert into [UserGroup] values(@UserGroupName,@HasHandsetRole,@HasLoadCarRole,@HasGateSysRole,@HasWebsiteRole)";

        #region 处理参数
        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();
        if (ugInfo.UserGroupName != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "UserGroupName";
            sqlPara.Value = ugInfo.UserGroupName;
            listSqlPara.Add(sqlPara);
        }
        if (ugInfo.HasHandsetRole != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.Bit;
            sqlPara.ParameterName = "HasHandsetRole";
            sqlPara.Value = ugInfo.HasHandsetRole;
            listSqlPara.Add(sqlPara);
        }
        if (ugInfo.HasLoadCarRole != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.Bit;
            sqlPara.ParameterName = "HasLoadCarRole";
            sqlPara.Value = ugInfo.HasLoadCarRole;
            listSqlPara.Add(sqlPara);
        }
        if (ugInfo.HasGateSysRole != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.Bit;
            sqlPara.ParameterName = "HasGateSysRole";
            sqlPara.Value = ugInfo.HasGateSysRole;
            listSqlPara.Add(sqlPara);
        }
        if (ugInfo.HasWebsiteRole != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.Bit;
            sqlPara.ParameterName = "HasWebsiteRole";
            sqlPara.Value = ugInfo.HasWebsiteRole;
            listSqlPara.Add(sqlPara);
        }
        return Common.ExecNonQuerySql(sql, listSqlPara);
        #endregion
    }

    ///<summary>
    /// 修改用户组
    ///</summary>
    public static bool UpdateUserGroup(UserGroupInfo ugInfo)
    {
        string sql = @"update [UserGroup] set UserGroupName=@UserGroupName,HasHandsetRole=@HasHandsetRole,HasLoadCarRole=@HasLoadCarRole,                                   HasGateSysRole=@HasGateSysRole,HasWebsiteRole=@HasWebsiteRole where UserGroupId="+ugInfo.UserGroupID;

        #region 处理参数
        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();
        if (ugInfo.UserGroupName != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.NVarChar;
            sqlPara.ParameterName = "UserGroupName";
            sqlPara.Value = ugInfo.UserGroupName;
            listSqlPara.Add(sqlPara);
        }
        if (ugInfo.HasHandsetRole != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.Bit;
            sqlPara.ParameterName = "HasHandsetRole";
            sqlPara.Value = ugInfo.HasHandsetRole;
            listSqlPara.Add(sqlPara);
        }
        if (ugInfo.HasLoadCarRole != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.Bit;
            sqlPara.ParameterName = "HasLoadCarRole";
            sqlPara.Value = ugInfo.HasLoadCarRole;
            listSqlPara.Add(sqlPara);
        }
        if (ugInfo.HasGateSysRole != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.Bit;
            sqlPara.ParameterName = "HasGateSysRole";
            sqlPara.Value = ugInfo.HasGateSysRole;
            listSqlPara.Add(sqlPara);
        }
        if (ugInfo.HasWebsiteRole != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = new System.Data.SqlClient.SqlParameter();
            sqlPara.SqlDbType = SqlDbType.Bit;
            sqlPara.ParameterName = "HasWebsiteRole";
            sqlPara.Value = ugInfo.HasWebsiteRole;
            listSqlPara.Add(sqlPara);
        }
        return Common.ExecNonQuerySql(sql, listSqlPara);
        #endregion
    }

    ///<summary>
    /// 删除用户组
    ///</summary>
    public static bool DeleteUserGroup(int id)
    {
        string sql = @"delete [UserGroup] where UserGroupId=" + id;

        #region 处理参数
        return Common.ExecNonQuerySql(sql, null);
        #endregion
    }

}
