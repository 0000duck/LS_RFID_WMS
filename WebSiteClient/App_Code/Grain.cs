using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Invengo.RiceManangeServices.DBCommon;
using Invengo.RiceManangeServices.Utility;
using Invengo.RiceManangeServices.Model;

/// <summary>
///Grain 的摘要说明
/// </summary>
public class Grain
{
    public Grain()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 查询获取粮食物料信息
    /// </summary>
    /// <returns></returns>
    public static DataTable GetMaterial()
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        cmd.ClearParameters();
        cmd.setCommandText("select *  from Material");
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 查询获取已经存在粮库的物料名称
    /// </summary>
    /// <returns></returns>
    public static DataTable GetMaterialInStorage()
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        cmd.ClearParameters();
        cmd.setCommandText("select distinct MaterialName from View_WarePlaceGrainList");
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 查询获取粮食存放在仓库的位置和重量
    /// </summary>
    /// <param name="TEId">出入厂单据ID</param>
    /// <returns></returns>
    public static DataTable GetGrainStorage(string TEId)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select te.*,t.PRMBNumber,m.MaterialNumber,m.MaterialName,wh.WHNumber,wh.WHName,wp.WPNumber,wp.WPName,
                              wpOut.WPNumber as OutWPNumber,wpOut.WPName as OutWPName
                         from TruckEir te
                              left join Material m on te.MaterialID=m.MaterialID
                              left join WareHouse wh on te.WHID=wh.WHID
                              left join WarePlace wp on te.WPID=wp.WPID
                              left join WarePlace wpOut on te.OutWPID=wpOut.WPID 
                              left join (
                              select prid,prnumber as prmbnumber from postrequisition
                              union 
                              select mbid as prid,mbnumber as prmbnumber from movelocationbill) t on te.prmbid=t.prid
                              where te.TEID=@TEId";
        cmd.ClearParameters();
        cmd.setCommandText(sql);
        cmd.AddParameter("@TEId", TEId);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 查询获取正在处理的任务单
    /// </summary>
    /// <returns></returns>
    public static DataTable GetTask()
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select r.PRMBNumber,t.PRMBLot,t.TETaskType,m.MaterialNumber,m.MaterialName,wh.WHName,wp.Floor,wp.WPName,
                       t.TESalverQty,t.TECurSalverQty 
                       from truckeir t 
                       left join Material m on t.MaterialId=m.MaterialId 
                       left join WareHouse wh on t.WHID=wh.WHID
                       left join WarePlace wp on t.WPID=wp.WPID
                       left join (
                              select prid,prnumber as prmbnumber from postrequisition
                              union 
                              select mbid as prid,mbnumber as prmbnumber from movelocationbill) r on t.prmbid=r.prid
                       where t.TEStatus='4'";
        cmd.ClearParameters();
        cmd.setCommandText(sql);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 查询仓位粮食情况
    /// </summary>
    /// <param name="QueryCondition">查询条件</param>
    /// <param name="pageSize">网页列表一次最多显示多少行记录</param>
    /// <param name="currentPage">用户查询的第几页信息</param>
    /// <param name="recordNume">根据查询条件查询的总记录数</param>
    /// <returns></returns>
    public static DataTable QueryGrainListByStorage(Dictionary<string, string> QueryCondition, int pageSize, int currentPage, out int recordNum)
    {
        recordNum = 0;

        #region 处理查询语句和排序语句
        string sqlSelect = @"select *,Row_Number() Over(Order By WPNumber) AS serialNum";
        string sqlFrom = @"from View_WarePlaceGrain where 1=1";
        #endregion

        #region 处理参数

        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();

        if (QueryCondition != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = null;
            if (!string.IsNullOrEmpty(QueryCondition["WPNumber"]))
            {
                sqlFrom += " and WPNumber like '%'+@WPNumber+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "WPNumber";
                sqlPara.Value = QueryCondition["WPNumber"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(QueryCondition["MaterailNumber"]))
            {
                sqlFrom += " and MaterialNumber like '%'+@MaterailNumber+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "MaterailNumber";
                sqlPara.Value = QueryCondition["MaterailNumber"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(QueryCondition["WPGLot"]))
            {
                sqlFrom += " and WPGLot like '%'+@WPGLot+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "WPGLot";
                sqlPara.Value = QueryCondition["WPGLot"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(QueryCondition["WHID"]))
            {
                sqlFrom += " and WHID=@WHID";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "WHID";
                sqlPara.Value = QueryCondition["WHID"];
                listSqlPara.Add(sqlPara);
            }
        }

        #endregion

       #region 调用Ado.net查询数据库
       DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
       DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

       string sqlCount = @"select count(1) " + sqlFrom;
       cmd.setCommandText(sqlCount);
       if (listSqlPara != null && listSqlPara.Count > 0)
       {
           for (int i = 0; i < listSqlPara.Count; i++)
           {
               cmd.AddParameter(listSqlPara[i]);
           }
       }
       object o = dm.ExecScalarCommand(cmd);
       if (o == null)
           recordNum = 0;
       else
           recordNum = int.Parse(o.ToString());

       string sqlQuery = @"select * from (" + sqlSelect + " " + sqlFrom + ") t " +
                   @"where t.serialNum>@pageSize*(@pageCount-1) and t.serialNum<=@pageSize*@pageCount";
       // cmd.ClearParameters();
       cmd.setCommandText(sqlQuery);
       cmd.AddParameter("pageSize", pageSize);
       cmd.AddParameter("pageCount", currentPage);

       return dm.ExecDataSetCommand(cmd).Tables[0];
       #endregion
    }

    /// <summary>
    /// 作业任务单据查询
    /// </summary>
    /// <param name="info"> TruckEir实体类</param>
    /// <param name="pageSize">网页列表一次最多显示多少行记录</param>
    /// <param name="currentPage">用户查询的第几页信息</param>
    /// <param name="sortInfo">排列的列名实体类，可以指定几个列名，并且可以指定排列是升序还是降序</param>
    /// <param name="isFirstSearch">代表是否第一点击查询(如果是则需要对totalRecNum赋值，否则不需要，这样可以避免每次都去计算记录总数)</param>
    /// <param name="totalRecNum">根据查询条件查询的总记录数</param>
    /// <param name="retValue">返回string(DataSet格式)类型，</param>
    /// <returns>返回bool类型，错误返回false，正确返回true</returns>        
    public static bool QueryTruckEirList(Dictionary<string, string> queryCondition, int pageSize, int currentPage, SortInfo sortInfo, 
        bool isFirstSearch,
        out int totalRecNum, out string retValue)
    {
        totalRecNum = 0;
        retValue = null;
        #region 处理查询语句和排序语句
        string sqlSelect = @"select te.TEID,t.PRMBNumber, te.PRMBLot,te.PRMBRelation,te.PRMBSerial,te.TEAttribute,te.TETaskType,te.TEInWeight,
                             te.TEInTime,te.TEInMan,te.TEOutWeight,te.TEOutTime,te.TEOutMan,te.TEStatus,m.MaterialNumber,m.MaterialName";
        string[] sortColumns = null;
        if (sortInfo != null && !string.IsNullOrEmpty(sortInfo.SortSql))
        {
            sortColumns = sortInfo.SortSql.Split(',');
            string sqlSortIn = null;
            if (sortColumns != null && sortColumns.Length > 0)
            {
                foreach (string sortItem in sortColumns)
                {
                    sqlSortIn += "te." + sortItem + ",";
                }
                sqlSortIn = sqlSortIn.Substring(0, sqlSortIn.Length - 1);
            }
            sqlSelect += ",Row_Number() Over(Order By " + sqlSortIn + ") AS serialNum";
        }
        else
        {
            sqlSelect += ",Row_Number() Over(Order By te.TEID desc) AS serialNum";
        }
        string sqlFrom = @"from truckeir te inner join Material m on te.MaterialId=m.MaterialId 
                           inner join (
                           select prid,prnumber as prmbnumber from postrequisition
                           union 
                           select mbid as prid,mbnumber as prmbnumber from movelocationbill) t on te.prmbid=t.prid Where 1=1";
        #endregion

        #region 处理参数

        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();

        if (queryCondition != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = null;

            if (!string.IsNullOrEmpty(queryCondition["TETaskType"]))
            {
                sqlFrom += " AND te.TETaskType=@TETaskType";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "TETaskType";
                sqlPara.Value = queryCondition["TETaskType"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["PRMBLot"]))
            {
                sqlFrom += " AND te.PRMBLot like '%'+@PRMBLot+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "PRMBLot";
                sqlPara.Value = queryCondition["PRMBLot"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["MaterialNumber"]))
            {
                sqlFrom += " AND m.MaterialNumber like '%'+@MaterialNumber+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "MaterialNumber";
                sqlPara.Value = queryCondition["MaterialNumber"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["PlanStartTime"]))
            {
                sqlFrom += " AND te.TEInTime>=@PlanStartTime";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.DateTime;
                sqlPara.ParameterName = "PlanStartTime";
                sqlPara.Value = Convert.ToDateTime(queryCondition["PlanStartTime"]+" 00:00:00");
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["PlanEndTime"]))
            {
                sqlFrom += " AND te.TEInTime<=@PlanEndTime";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.DateTime;
                sqlPara.ParameterName = "PlanEndTime";
                sqlPara.Value = Convert.ToDateTime(queryCondition["PlanEndTime"] + " 23:59:59");
                listSqlPara.Add(sqlPara);
            }
        }

        #endregion

        return ExcutePage.ExcutePageCommand(sqlSelect, sqlFrom, sortColumns, pageSize, currentPage, listSqlPara, isFirstSearch, out totalRecNum, out retValue);
    }

}
