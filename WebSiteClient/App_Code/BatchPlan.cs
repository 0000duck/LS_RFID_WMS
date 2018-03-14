using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Invengo.RiceManangeServices.DBCommon;
using Invengo.RiceManangeServices.Utility;
using Invengo.RiceManangeServices.Model;

/// <summary>
///BatchPlan 的摘要说明
/// </summary>
public class BatchPlan
{
    public BatchPlan()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    ///<summary>
    /// 网站查询出入库通知单列表
    ///</summary>
    public static DataTable QueryBatchPlanList(Dictionary<string, string> queryCondition, int pageSize, int currentPage, out int recordNum)
    {
        recordNum = 0;

        #region 处理查询语句和排序语句
        string sqlSelect = @"select pr.Id,pr.PRNumber,pr.PRType,pr.PRBizDate,pr.PRFinishDate,pr.PRConsignmentUnitName,
                             pr.PRConsigneeUnitName,pr.PRBaseStatus,pr.PRLot,pr.PRCards,pr.PRSealNum,pr.PRBaseQty,m.MaterialNumber,m.MaterialName,
                             pr.PRRelation,pr.PRID,pr.PRIsFinished,Row_Number() Over(Order By pr.Id desc) AS serialNum ";

        string sqlFrom = @"from PostRequisition pr,Material m where pr.MaterialId=m.MaterialId ";
        #endregion

        #region 处理参数

        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();
        if (queryCondition != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = null;
            if (!string.IsNullOrEmpty(queryCondition["PRType"]))
            {
                sqlFrom += " and pr.PRType=@PRType";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.Int;
                sqlPara.ParameterName = "PRType";
                sqlPara.Value = Int32.Parse(queryCondition["PRType"]);
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["PRNumber"]))
            {
                sqlFrom += " and pr.PRNumber like '%'+@PRNumber+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "PRNumber";
                sqlPara.Value = queryCondition["PRNumber"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["PRLot"]))
            {
                sqlFrom += " and pr.PRLot like '%'+@PRLot+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "PRLot";
                sqlPara.Value = queryCondition["PRLot"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["StartTime"]))
            {
                sqlFrom += " and pr.PRBizDate >= @StartTime";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.DateTime;
                sqlPara.ParameterName = "StartTime";
                sqlPara.Value = Convert.ToDateTime(queryCondition["StartTime"]);
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["EndTime"]))
            {
                sqlFrom += " and pr.PRBizDate <= @EndTime";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.DateTime;
                sqlPara.ParameterName = "EndTime";
                sqlPara.Value = Convert.ToDateTime(queryCondition["EndTime"]);
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["PRID"]))
            {
                sqlFrom += " and pr.PRID = @PRID and pr.PRRelation=3";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "PRID";
                sqlPara.Value = queryCondition["PRID"];
                listSqlPara.Add(sqlPara);
            }
            else
            {
                sqlFrom += " and pr.PRRelation<>3";
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
    /// 通过出入库通知单ID查询出入库通知单信息
    /// </summary>
    /// <param name="id">出入库通知单ID</param>
    public static DataTable QueryBatchPlanById(int id)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select pr.*,m.MaterialNumber,m.MaterialName,m.MaterialModel,wh.WHNumber,wh.WHName,wp.WPNumber,wp.WPName
                       from PostRequisition pr
                            left join Material m on pr.MaterialId=m.MaterialId 
                            left join WareHouse wh on pr.whId=wh.whId 
                            left join WarePlace wp on pr.wpId=wp.wpId
                       where pr.Id=@Id";

        cmd.ClearParameters();
        cmd.AddParameter("@Id", id);
        cmd.setCommandText(sql);
        return dm.ExecDataSetCommand(cmd).Tables[0];
    }

    /// <summary>
    /// 查询EAS移库通知列表
    /// </summary>
    /// <param name="queryCondition">查询条件</param>
    /// <param name="pageSize">网页列表一次最多显示多少行记录</param>
    /// <param name="currentPage">用户查询的第几页信息</param>
    /// <param name="sortInfo">排列的列名实体类，可以指定几个列名，并且可以指定排列是升序还是降序</param>
    /// <param name="isFirstSearch">代表是否第一点击查询(如果是则需要对totalRecNum赋值，否则不需要，这样可以避免每次都去计算记录总数)</param>
    /// <param name="totalRecNum">根据查询条件查询的总记录数</param>
    /// <param name="retValue">返回string(DataSet格式)类型</param>
    /// <returns>返回bool类型，错误返回false，正确返回true</returns>
    public static bool QueryMovePlanList(Dictionary<string, string> queryCondition, int pageSize, int currentPage, SortInfo sortInfo, 
        bool isFirstSearch, out int totalRecNum, out string retValue)
    {
        totalRecNum = 0;
        retValue = null;
        #region 处理查询语句和排序语句
        string sqlSelect = @"select mlb.*,m.MaterialNumber,m.MaterialName";
        string[] sortColumns = null;
        if (sortInfo != null && !string.IsNullOrEmpty(sortInfo.SortSql))
        {
            sortColumns = sortInfo.SortSql.Split(',');
            string sqlSortIn = null;
            if (sortColumns != null && sortColumns.Length > 0)
            {
                foreach (string sortItem in sortColumns)
                {
                    sqlSortIn += "mlb." + sortItem + ",";
                }
                sqlSortIn = sqlSortIn.Substring(0, sqlSortIn.Length - 1);
            }
            sqlSelect += ",Row_Number() Over(Order By " + sqlSortIn + ") AS serialNum";
        }
        else
        {
            sqlSelect += ",Row_Number() Over(Order By mlb.ID desc) AS serialNum";
        }
        string sqlFrom = @" from MoveLocationBill mlb,Material m Where mlb.MaterialId=m.MaterialId ";
        #endregion

        #region 处理参数

        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();

        if (queryCondition != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = null;

            if (!string.IsNullOrEmpty(queryCondition["PRType"]))
            {
                sqlFrom += " AND mlb.MBType=@PRType";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "PRType";
                sqlPara.Value = queryCondition["PRType"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["PRNumber"]))
            {
                sqlFrom += " and mlb.MBNumber like '%'+@PRNumber+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "PRNumber";
                sqlPara.Value = queryCondition["PRNumber"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["PRLot"]))
            {
                sqlFrom += " and mlb.MBLot like '%'+@PRLot+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "PRLot";
                sqlPara.Value = queryCondition["PRLot"];
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["StartTime"]))
            {
                sqlFrom += " and mlb.MBBizDate >= @StartTime";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.DateTime;
                sqlPara.ParameterName = "StartTime";
                sqlPara.Value = Convert.ToDateTime(queryCondition["StartTime"]);
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["EndTime"]))
            {
                sqlFrom += " and mlb.MBBizDate <= @EndTime";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.DateTime;
                sqlPara.ParameterName = "EndTime";
                sqlPara.Value = Convert.ToDateTime(queryCondition["EndTime"]);
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(queryCondition["MBID"]))
            {
                sqlFrom += " and mlb.MBID = @MBID and mlb.MBRelation=3";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "MBID";
                sqlPara.Value = queryCondition["MBID"];
                listSqlPara.Add(sqlPara);
            }
            else
            {
                sqlFrom += " and mlb.MBRelation<>3";
            }
        }

        #endregion

        return ExcutePage.ExcutePageCommand(sqlSelect, sqlFrom, sortColumns, pageSize, currentPage, listSqlPara, isFirstSearch, out totalRecNum, out retValue);
    }

    /// <summary>
    /// 通过通知单ID查询移库通知单信息
    /// </summary>
    /// <param name="id">移库通知单ID</param>
    public static DataTable QueryMovePlanById(int id)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select mlb.*,m.MaterialNumber,m.MaterialName,m.MaterialModel,
                              suOut.SUNumber as outSUNumber,suOut.SUName as outSUName,
                              whOut.WHNumber as outWHNumber,whOut.WHName as outWHName,
                              wpOut.WPNumber as outWPNumber,wpOut.WPName as outWPName,
                              suIn.SUNumber as inSUNumber,suIn.SUName as inSUName,
                              whIn.WHNumber as inWHNumber,whIn.WHName as inWHName,
                              wpIn.WPNumber as inWPNumber,wpIn.WPName as inWPName
                         from MoveLocationBill mlb 
                              left join Material m on mlb.MaterialId=m.MaterialId
                              left join StorageUnit suOut on mlb.outSUID=suOut.SUID
                              left join WareHouse whOut on mlb.OutWHID=whOut.WHID
                              left join WarePlace wpOut on mlb.OutWPID=wpOut.WPID
                              left join StorageUnit suIn on mlb.inSUID=suIn.SUID
                              left join WareHouse whIn on mlb.InWHID=whIn.whId
                              left join WarePlace wpIn on mlb.InWPID=wpIn.wpId
                        where mlb.Id=@Id";

        cmd.ClearParameters();
        cmd.AddParameter("@Id", id);
        cmd.setCommandText(sql);
        return dm.ExecDataSetCommand(cmd).Tables[0];
    }

    /// <summary>
    /// 通过计划ID查询计划详细信息
    /// </summary>
    /// <returns></returns>
    public static DataTable GetBatchPlanById(string batchPlanId)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select b.*,s.storagecode,s.storagename,sd.storagedetailid,sd.storagecode as storagedetailcode,t.totalweight
                     from (select * from batchplan where batchplanid=@batchPlanId) b,storage s,batchplanstorage bs,storagedetail sd
                     ,(select abs(sum(inweight)-sum(outweight)) as totalweight from truckeir where batchplanid=@batchPlanId) t
                      where b.storageid=s.storageid and b.batchplanid=bs.batchplanid
                      and bs.storagedetailid=sd.storagedetailid";
        cmd.ClearParameters();
        cmd.setCommandText(sql);
        cmd.AddParameter("@batchPlanId", batchPlanId);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 更新出入库订单返回金蝶状态
    /// </summary>
    public static bool UpdateBatchPlanReturnState(string batchPlanId)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        cmd.ClearParameters();
        cmd.setCommandText("update batchplan set upload=0 where batchplanid=@batchPlanId");
        cmd.AddParameter("@batchPlanId", batchPlanId);
        return dm.ExecNonQueryCommand(cmd);
    }
}
