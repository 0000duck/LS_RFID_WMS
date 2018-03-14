using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Invengo.RiceManangeServices.DBCommon;
using Invengo.RiceManangeServices.Utility;
using Invengo.RiceManangeServices.Model;

/// <summary>
///WorkItem 的摘要说明
/// </summary>
public class WorkItem
{
    public WorkItem()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 查询叉车工作详细工作记录
    /// </summary>
    /// <param name="queryCondition">查询条件</param>
    /// <param name="pageSize">网页列表一次最多显示多少行记录</param>
    /// <param name="currentPage">用户查询的第几页信息</param>
    /// <param name="sortInfo">排列的列名实体类，可以指定几个列名，并且可以指定排列是升序还是降序</param>
    /// <param name="isFirstSearch">代表是否第一点击查询(如果是则需要对totalRecNum赋值，否则不需要，这样可以避免每次都去计算记录总数)</param>
    /// <param name="totalRecNum">根据查询条件查询的总记录数</param>
    /// <param name="retValue">返回string(DataSet格式)类型，字段包括司机、日期、合同、批次、计划时间和计划仓位等信息</param>
    /// <returns>返回bool类型，错误返回false，正确返回true</returns>
    public static bool QueryWorkItemList(Dictionary<string, string> queryCondition, int pageSize, int currentPage, SortInfo sortInfo,
        bool isFirstSearch, out int totalRecNum, out string retValue)
    {
        totalRecNum = 0;
        retValue = null;
        #region 处理查询语句和排序语句
        string sqlSelect = @"select w.*";

        string[] sortColumns = null;
        string sqlSortIn = string.Empty;
        if (sortInfo != null && !string.IsNullOrEmpty(sortInfo.SortSql))
        {
            sortColumns = sortInfo.SortSql.Split(',');
            if (sortColumns != null && sortColumns.Length > 0)
            {
                foreach (string sortItem in sortColumns)
                {
                    sqlSortIn += "w." + sortItem + ",";
                }
                sqlSortIn = sqlSortIn.Substring(0, sqlSortIn.Length - 1);
            }
            sqlSelect += ",Row_Number() Over(Order By " + sqlSortIn + ") AS serialNum";
        }
        else
        {
            sqlSelect += ",Row_Number() Over(Order By w.WorkItemhisID) AS serialNum";
        }
        string sqlFrom = @" from View_WorkItemHis w where 1=1";

        #endregion

        #region 处理参数

        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();

        if (queryCondition != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = null;

            if (!string.IsNullOrEmpty(queryCondition["WorkType"]))
            {
                sqlFrom += " AND w.TETaskType = @WorkType";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "WorkType";
                sqlPara.Value = queryCondition["WorkType"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["PRMBlot"]))
            {
                sqlFrom += " AND w.PRMBlot like '%'+@PRMBlot+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "PRMBlot";
                sqlPara.Value = queryCondition["PRMBlot"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["JobNo"]))
            {
                sqlFrom += " AND w.JobNo like '%'+@JobNo+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "JobNo";
                sqlPara.Value = queryCondition["JobNo"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["StartTime"]))
            {
                sqlFrom += " AND w.OpTime >= @OpBeginTime";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.DateTime;
                sqlPara.ParameterName = "OpBeginTime";
                sqlPara.Value = Convert.ToDateTime(queryCondition["StartTime"] + " 00:00:00");
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["EndTime"]))
            {
                sqlFrom += " AND w.OpTime <= @OpEndTime";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.DateTime;
                sqlPara.ParameterName = "OpEndTime";
                sqlPara.Value = Convert.ToDateTime(queryCondition["EndTime"] + " 23:59:59");
                listSqlPara.Add(sqlPara);
            }
        }

        #endregion

        return ExcutePage.ExcutePageCommand(sqlSelect, sqlFrom, sortColumns, pageSize, currentPage, listSqlPara, isFirstSearch, out totalRecNum, out retValue);
    }

    /// <summary>
    ///查询叉车工作统计信息
    /// </summary>
    /// <param name="queryCondition">查询条件</param>
    /// <param name="pageSize">网页列表一次最多显示多少行记录</param>
    /// <param name="currentPage">用户查询的第几页信息</param>
    /// <param name="sortInfo">排列的列名实体类，可以指定几个列名，并且可以指定排列是升序还是降序</param>
    /// <param name="isFirstSearch">代表是否第一点击查询(如果是则需要对totalRecNum赋值，否则不需要，这样可以避免每次都去计算记录总数)</param>
    /// <param name="totalRecNum">根据查询条件查询的总记录数</param>
    /// <param name="retValue">返回string(DataSet格式)类型，字段包括叉车、司机、工作日期、合同、批次、仓位、出/入托盘数量等信息</retValue>
    /// <returns>返回bool类型，错误返回false，正确返回true</returns>
    public static bool QueryWorkItemStatList(Dictionary<string, string> queryCondition, int pageSize, int currentPage, SortInfo sortInfo,
        bool isFirstSearch, out int totalRecNum, out string retValue)
    {
        totalRecNum = 0;
        retValue = null;
        #region 处理查询语句和排序语句
        string sqlSelect = @"select w.PRMBlot,w.TETaskType,w.JobNo,w.RealName,count(w.opernum) as TotalOperNum ";
        string sqlFrom = @"from View_WorkItemHis w where 1=1";
        #endregion

        string[] sortColumns = null;
        string sqlSerialNum = null, sqlSortIn = null;
        if (sortInfo != null && !string.IsNullOrEmpty(sortInfo.SortSql))
        {
            sortColumns = sortInfo.SortSql.Split(',');
            if (sortColumns != null && sortColumns.Length > 0)
            {
                foreach (string sortItem in sortColumns)
                {
                    sqlSerialNum += "w." + sortItem + ",";
                }
                sqlSortIn = sqlSortIn.Substring(0, sqlSortIn.Length - 1);
            }
            sqlSelect += ",Row_Number() Over(Order By " + sqlSortIn + ") AS serialNum ";
        }
        else
        {
            sqlSelect += ",Row_Number() Over(Order By w.PRMBlot) AS serialNum";
        }

        #region 处理参数

        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();
        if (queryCondition != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = null;

            if (!string.IsNullOrEmpty(queryCondition["WorkType"]))
            {
                sqlFrom += " AND w.TETaskType = @WorkType";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "WorkType";
                sqlPara.Value = queryCondition["WorkType"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["PRMBlot"]))
            {
                sqlFrom += " AND w.PRMBlot like '%'+@PRMBlot+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "PRMBlot";
                sqlPara.Value = queryCondition["PRMBlot"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["JobNo"]))
            {
                sqlFrom += " AND w.JobNo like '%'+@JobNo+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "JobNo";
                sqlPara.Value = queryCondition["JobNo"];
                listSqlPara.Add(sqlPara);
            }
        }

        sqlFrom += " group by w.PRMBlot,w.TETaskType,w.JobNo,w.RealName";
        #endregion

        return ExcutePage.ExcutePageCommand(sqlSelect, sqlFrom, sortColumns, pageSize, currentPage, listSqlPara, isFirstSearch, out totalRecNum, out retValue);
    }

    /// <summary>
    ///查询叉车列表信息
    /// </summary>
    /// <param name="info">DeviceInfo实体类(主要是叉车编号，如果什么实体类为null，则代表查询所有叉车信息)</param>
    /// <param name="pageSize">网页列表一次最多显示多少行记录</param>
    /// <param name="currentPage">用户查询的第几页信息</param>
    /// <param name="sortInfo">排列的列名实体类，可以指定几个列名，并且可以指定排列是升序还是降序</param>
    /// <param name="isFirstSearch">代表是否第一点击查询(如果是则需要对totalRecNum赋值，否则不需要，这样可以避免每次都去计算记录总数)</param>
    /// <param name="totalRecNum">根据查询条件查询的总记录数</param>
    /// <param name="retValue">返回string(DataSet格式)类型，字段包括仓库名称、叉车编号、楼层、备注等信息</param>
    /// <returns>返回bool类型，错误返回false，正确返回true</returns>
    public static bool QueryDeviceList(DeviceInfo info, int pageSize, int currentPage, SortInfo sortInfo, bool isFirstSearch, out int totalRecNum, out string retValue)
    {
        totalRecNum = 0;
        retValue = null;
        #region 处理查询语句和排序语句
        string sqlSelect = @"select d.*";
        string[] sortColumns = null;
        if (sortInfo != null && !string.IsNullOrEmpty(sortInfo.SortSql))
        {
            sortColumns = sortInfo.SortSql.Split(',');
            string sqlSortIn = null;
            if (sortColumns != null && sortColumns.Length > 0)
            {
                foreach (string sortItem in sortColumns)
                {
                    sqlSortIn += "d."+sortItem + ",";
                }
                sqlSortIn = sqlSortIn.Substring(0, sqlSortIn.Length - 1);
            }
            sqlSelect += ",Row_Number() Over(Order By " + sqlSortIn + ") AS serialNum";
        }
        else
        {
            sqlSelect += ",Row_Number() Over(Order By d.DeviceID) AS serialNum";
        }
        string sqlFrom = @" from View_Device d Where d.DeviceType='C'";
        #endregion

        #region 处理参数

        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();

        if (info != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = null;

            if (!string.IsNullOrEmpty(info.DeviceCode))
            {
                sqlFrom += " AND d.DeviceCode like '%'+@DeviceCode+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NChar;
                sqlPara.ParameterName = "DeviceCode";
                sqlPara.Value = info.DeviceCode;
                listSqlPara.Add(sqlPara);
            }
        }

        #endregion

        return ExcutePage.ExcutePageCommand(sqlSelect, sqlFrom, sortColumns, pageSize, currentPage, listSqlPara, isFirstSearch, out totalRecNum, out retValue);
    }

    /// <summary>
    /// 获取叉车司机用户
    /// </summary>
    /// <returns></returns>
    public static DataTable GetWorkItemUser()
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        cmd.ClearParameters();
        cmd.setCommandText("select * from [user] u,usergroup up where up.hasloadcarrole=1 and up.usergroupid=u.usergroupid");
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }


}
