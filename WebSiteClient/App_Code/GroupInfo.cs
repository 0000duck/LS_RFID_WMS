using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Invengo.RiceManangeServices.DBCommon;

/// <summary>
///GroupInfo 的摘要说明
/// </summary>
public class GroupInfo
{
    public GroupInfo()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 查询统计时间段内出入库粮食信息
    /// </summary>
    /// <param name="beginTime">开始日期</param>
    /// <param name="beginTime">结束日期</param>
    /// <param name="type">查询类型，0为全部，1为按品名，2为按仓库，3为楼层，4为按仓位，5为品名仓库，6为品名楼层，7为品名仓位</param>
    /// <param name="materialName">物料名</param>
    /// <param name="whId">仓库ID</param>
    /// <param name="floorDigit">楼层数</param>
    /// <param name="wpId">仓位ID</param>
    /// <returns></returns>
    public static DataTable GetGroupIOStorageTotal(DateTime beginTime, DateTime endTime, int type, string materialName, string whId, 
        int floorDigit, string wpId)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.StoredProcedure);

        cmd.ClearParameters();
        cmd.setCommandText("Pro_GroupIOStorageTotal");
        cmd.AddParameter("@beginDate", beginTime);
        cmd.AddParameter("@endDate", endTime);
        cmd.AddParameter("@type", type);
        cmd.AddParameter("@materialName", materialName);
        cmd.AddParameter("@whId", whId);
        cmd.AddParameter("@floorDigit", floorDigit);
        cmd.AddParameter("@wpId", wpId);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 按月查询统计时间段内出入库粮食信息
    /// </summary>
    /// <param name="year">年份</param>
    /// <param name="type">查询类型，0为全部，1为按品名，2为按仓库，3为楼层，4为按仓位，5为品名仓库，6为品名楼层，7为品名仓位</param>
    /// <param name="materialName">物料名</param>
    /// <param name="whId">仓库ID</param>
    /// <param name="floorDigit">楼层数</param>
    /// <param name="wpId">仓位ID</param>
    /// <returns></returns>
    public static DataTable GetGroupIOStorageTotalByMonth(int year, int type, string materialName, string whId,int floorDigit, string wpId)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.StoredProcedure);

        cmd.ClearParameters();
        cmd.setCommandText("Pro_GroupIOStorageTotalByMouth");
        cmd.AddParameter("@year", year);
        cmd.AddParameter("@type", type);
        cmd.AddParameter("@materialName", materialName);
        cmd.AddParameter("@whId", whId);
        cmd.AddParameter("@floorDigit", floorDigit);
        cmd.AddParameter("@wpId", wpId);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 按年查询统计时间段内出入库粮食信息
    /// </summary>
    /// <param name="beginYear">开始年份</param>
    /// <param name="endYear">结束年份</param>
    /// <param name="type">查询类型，0为全部，1为按品名，2为按仓库，3为楼层，4为按仓位，5为品名仓库，6为品名楼层，7为品名仓位</param>
    /// <param name="materialName">物料名</param>
    /// <param name="whId">仓库ID</param>
    /// <param name="floorDigit">楼层数</param>
    /// <param name="wpId">仓位ID</param>
    /// <returns></returns>
    public static DataTable GetGroupIOStorageTotalByYear(int beginYear, int endYear, int type, string materialName, string whId, int floorDigit,
        string wpId)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.StoredProcedure);

        cmd.ClearParameters();
        cmd.setCommandText("Pro_GroupIOStorageTotalByYear");
        cmd.AddParameter("@beginYear", beginYear);
        cmd.AddParameter("@endYear", endYear);
        cmd.AddParameter("@type", type);
        cmd.AddParameter("@materialName", materialName);
        cmd.AddParameter("@whId", whId);
        cmd.AddParameter("@floorDigit", floorDigit);
        cmd.AddParameter("@wpId", wpId);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 查询统计时间段内库粮食信息
    /// </summary>
    /// <param name="beginTime">开始日期</param>
    /// <param name="beginTime">结束日期</param>
    /// <returns></returns>
    public static DataTable GetGroupGrainTotal(DateTime beginTime, DateTime endTime)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.StoredProcedure);

        cmd.ClearParameters();
        cmd.setCommandText("Pro_GroupGrainTotal");
        cmd.AddParameter("@beginDate", beginTime);
        cmd.AddParameter("@endDate", endTime);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 统计员工工作量
    /// </summary>
    /// <param name="beginTime">开始日期</param>
    /// <param name="beginTime">结束日期</param>
    /// <returns></returns>
    public static DataTable GetGroupWorkItem(DateTime beginTime, DateTime endTime)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select JobNo,RealName,sum(opernum)as OperNum 
                       from view_workitemhis where optime>=@begintime and optime<=@endtime 
                       group by JobNo,RealName";
        cmd.ClearParameters();
        cmd.setCommandText(sql);
        cmd.AddParameter("@begintime", beginTime);
        cmd.AddParameter("@endtime", endTime);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 查询统计托盘使用情况
    /// </summary>
    /// <param name="beginTime">开始日期</param>
    /// <param name="beginTime">结束日期</param>
    /// <returns></returns>
    public static DataTable GetGroupSalver(DateTime beginTime, DateTime endTime)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.StoredProcedure);

        cmd.ClearParameters();
        cmd.setCommandText("PRO_GROUPSALVER");
        cmd.AddParameter("@beginDate", beginTime);
        cmd.AddParameter("@endDate", endTime);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 统计粮库目前的情况，包括托盘,装卸点，叉车情况
    /// </summary>
    /// <returns></returns>
    public static DataTable GetGroupCurrentInfo()
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.StoredProcedure);

        cmd.ClearParameters();
        cmd.setCommandText("PRO_GROUPCURRENTINFO");
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 按品种统计粮库目前存量的情况
    /// </summary>
    public static DataTable GetGroupCurrentGrainByCargoName()
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select MaterialName, sum(isnull(WPGBaseQty,0))/1000 as weight from View_WarePlaceGrain group by MaterialName";
        cmd.setCommandText(sql);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 按粮库统计粮库目前存量的情况
    /// </summary>
    public static DataTable GetGroupCurrentGrainByStorage()
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select WHName, sum(isnull(WPGBaseQty,0))/1000 as weight from View_WarePlaceGrain group by WHName";
        cmd.setCommandText(sql);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }
}
