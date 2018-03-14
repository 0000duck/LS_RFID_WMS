using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

using Invengo.RiceManangeServices.DBCommon;

/// <summary>
///Common 的摘要说明
/// </summary>
public class Common
{
    public Common()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 判断某表中记录是否存在
    /// </summary>
    /// <returns></returns>
    public static bool IsExsit(string tableName, List<TablePrimary> paramList)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select count(*) from [" + tableName + "] where 1=1";
        cmd.ClearParameters();
        foreach (TablePrimary param in paramList)
        {
            sql += " and " + param.PrimaryName + "=@" + param.PrimaryName;
            cmd.AddParameter("@" + param.PrimaryName, param.PrimaryValue);
        }
        cmd.setCommandText(sql);
        if (Convert.ToInt32(dm.ExecScalarCommand(cmd)) > 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 判断某表中记录是否存在除了id对应的值
    /// </summary>
    /// <returns></returns>
    public static bool IsExsit(string tableName, List<TablePrimary> paramList,int id)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select count(*) from [" + tableName + "] where 1=1 and "+tableName +"id <>"+id;
        cmd.ClearParameters();
        foreach (TablePrimary param in paramList)
        {
            sql += " and " + param.PrimaryName + "=@" + param.PrimaryName;
            cmd.AddParameter("@" + param.PrimaryName, param.PrimaryValue);
        }
        cmd.setCommandText(sql);
        if (Convert.ToInt32(dm.ExecScalarCommand(cmd)) > 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 判断角色是否有菜单访问权限
    /// </summary>
    /// <returns></returns>
    public static bool IsRoleAuthenticated(string roleId, string path)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select count(*) from menu m, usermenurole umr where m.path=@path and umr.roleid=@roleId and m.menuid=umr.menuid";
        cmd.ClearParameters();
        cmd.setCommandText(sql);
        cmd.AddParameter("@path", path);
        cmd.AddParameter("@roleId", roleId);
        if (Convert.ToInt32(dm.ExecScalarCommand(cmd)) > 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 查询对象列表
    /// </summary>
    /// <returns></returns>
    public static DataTable GetObjectList(string sqlCount, string sqlQuery, List<SqlParameter> listSqlPara, out int recordNum)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        if (listSqlPara != null && listSqlPara.Count > 0)
        {
            cmd.ClearParameters();
            for (int i = 0; i < listSqlPara.Count; i++)
            {
                cmd.AddParameter(listSqlPara[i]);
            }
        }
        if (!string.IsNullOrEmpty(sqlCount))
        {
            cmd.setCommandText(sqlCount);
            object o = dm.ExecScalarCommand(cmd);
            if (o == null)
                recordNum = 0;
            else
                recordNum = int.Parse(o.ToString());
        }
        else
        {
            recordNum = 0;
        }

        if (!string.IsNullOrEmpty(sqlQuery))
        {
            cmd.setCommandText(sqlQuery);
            return dm.ExecDataSetCommand(cmd).Tables[0];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 查询对象列表
    /// </summary>
    /// <returns></returns>
    public static DataTable GetObjectList(string tableName)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);
        string sqlQuery = "select * from [" + tableName+"]";

        cmd.setCommandText(sqlQuery);
        return dm.ExecDataSetCommand(cmd).Tables[0];
    }

    /// <summary>
    /// 查询对象列表
    /// </summary>
    /// <returns></returns>
    public static DataTable GetObjectList(string tableName,int id)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);
        string sqlQuery = "select * from [" + tableName + "] where " + tableName+"Id="+id;

        cmd.setCommandText(sqlQuery);
        return dm.ExecDataSetCommand(cmd).Tables[0];
    }

    /// <summary>
    /// 查询对象列表
    /// </summary>
    /// <returns></returns>
    public static DataTable GetObjectList(string sql, List<SqlParameter> listSqlPara)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        cmd.setCommandText(sql);
        if (listSqlPara != null && listSqlPara.Count > 0)
        {
            for (int i = 0; i < listSqlPara.Count; i++)
            {
                cmd.AddParameter(listSqlPara[i]);
            }
        }
        return dm.ExecDataSetCommand(cmd).Tables[0];
    }

    /// <summary>
    /// 执行某条语句
    /// </summary>
    /// <returns></returns>
    public static bool ExecNonQuerySql(string sql, List<SqlParameter> listSqlPara)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        cmd.setCommandText(sql);
        if (listSqlPara != null && listSqlPara.Count > 0)
        {
            for (int i = 0; i < listSqlPara.Count; i++)
            {
                cmd.AddParameter(listSqlPara[i]);
            }
        }
        return dm.ExecNonQueryCommand(cmd);
    }

    /// <summary>
    /// 获取出入场单据状态
    /// </summary>
    /// <returns></returns>
    public static string GetPRMBState(string state)
    {
        if (state == "1")
            return "初始状态";
        else if (state == "2")
            return "拒绝状态";
        else if (state == "3")
            return "挂起状态";
        else if (state == "4")
            return "开始执行";
        else if (state == "5")
            return "执行完成";
        else if (state == "6")
            return "已出厂";
        else if (state == "7")
            return "已完成";
        else
            return string.Empty;
    }


    /// <summary>
    /// 获得通知单类型名称
    /// </summary>
    /// <param name="prTypeCode">通知单类型码</param>
    /// <returns>通知单类型名称</returns>
    public static string GetPRTypeName(string prTypeCode)
    {
        if (prTypeCode == "1")
            return "发货通知单";
        else if (prTypeCode == "2")
            return "收货通知单";
        else if (prTypeCode == "3")
            return "领料出库单";
        else if (prTypeCode == "4")
            return "生产入库单";
        else if (prTypeCode == "20")
            return "其他出库单";
        else if (prTypeCode == "21")
            return "其他入库单";
        else
            return string.Empty;
    }

    /// <summary>
    /// 获得通知单状态
    /// </summary>
    /// <param name="prTypeCode">通知单类型码</param>
    /// <returns>通知单类型名称</returns>
    public static string GetPRStatus(string status)
    {
        if (status == "0")
            return "未分配";
        else if (status == "1")
            return "已分配";
        else if (status == "2")
            return "已撤销";
        else
            return string.Empty;
    }

    /// <summary>
    /// 获得移库通知单类型名称
    /// </summary>
    /// <param name="mbTypeCode">通知单类型码</param>
    /// <returns>通知单类型名称</returns>
    public static string GetMBTypeName(string mbTypeCode)
    {
        if (mbTypeCode == "1")
            return "同仓移库";
        else if (mbTypeCode == "2")
            return "异仓移库";
        else if (mbTypeCode == "3")
            return "异库移库";
        else
            return string.Empty;
    }


    /// <summary>
    /// 获取出入厂单据关系
    /// </summary>
    /// <returns></returns>
    public static string GetPRMBRelation(string relation)
    {
        if (relation == "1")
            return "原始单据";
        else if (relation == "2")
            return "父单据";
        else if (relation == "3")
            return "子单据";
        else
            return string.Empty;
    }

    /// <summary>
    /// 获取出入厂单据任务类型
    /// </summary>
    /// <returns></returns>
    public static string GetTETaskType(string tastType)
    {
        if (tastType == "1")
            return "出库任务";
        else if (tastType == "2")
            return "入库任务";
        else if (tastType == "3")
            return "库内移库";
        else
            return string.Empty;
    }
}
