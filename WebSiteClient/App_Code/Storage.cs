using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Invengo.RiceManangeServices.DBCommon;
/// <summary>
///Storage 的摘要说明
/// </summary>
public class Storage
{
    public Storage()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 查询获取库信息
    /// </summary>
    /// <returns></returns>
    public static DataTable GetStorage()
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        cmd.ClearParameters();
        cmd.setCommandText("select * from WareHouse");
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 根据仓库ID查询获取楼层信息
    /// </summary>
    /// <param name="storageId">仓库ID</param>
    /// <returns></returns>
    public static DataTable GetBuildingFloor(string whId)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = "select distinct floor as floor from WarePlace where WHID=@WHID";
        cmd.ClearParameters();
        cmd.setCommandText(sql);
        cmd.AddParameter("@WHID", whId);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 根据楼层，仓库ID查询获取仓位信息
    /// </summary>
    /// <param name="storageId">仓库ID</param>
    /// <param name="楼层">floor</param>
    /// <param name="区域">area</param>
    /// <returns></returns>
    public static DataTable GetStorageDetail(string whId, string floor,string area)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select * from WarePlace wp left join (select wpid,sum(wpgbaseqty)totalWeight
                       from wareplacegrain group by wpid) t on wp.wpid=t.wpid 
                       where wp.WHID=@WHID and wp.[Floor]=@Floor";
        if(!string.IsNullOrEmpty(area))
        {
            sql+=" and wp.Area='"+area+"'";
        }
        sql += " order by wp.WPName";
        cmd.ClearParameters();
        cmd.setCommandText(sql);
        cmd.AddParameter("@WHID", whId);
        cmd.AddParameter("@Floor", floor);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 根据仓位ID查询获取堆位信息
    /// </summary>
    /// <param name="wpId">库ID</param>
    /// <returns></returns>
    public static DataTable GetStorageDetailById(string wpId)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        cmd.ClearParameters();
        cmd.setCommandText("select * from WarePlace where wpId=@wpId");
        cmd.AddParameter("@wpId", wpId);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 根据仓库，楼层，仓位查询目前存粮情况
    /// </summary>
    /// <param name="whId">仓库ID</param>
    /// <param name="floor">楼层数</param>
    /// <param name="wpId">仓位ID</param>
    /// <returns></returns>
    public static DataTable GetStorageGrainByStorageId(string whId,string floor,string wpId)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select sum(WPGBaseQty) as weight
                       from View_WarePlaceGrain where WHID=@WHID";

        cmd.ClearParameters();
        if(!string.IsNullOrEmpty(floor))
        {
            sql += " and Floor=@Floor";
            cmd.AddParameter("@Floor", floor);
        }
        if (!string.IsNullOrEmpty(wpId))
        {
            sql += " and WPID=@WPID";
            cmd.AddParameter("@WPID", wpId);
        }

        cmd.setCommandText(sql);
        cmd.AddParameter("@WHID", whId);
           
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 根据仓库ID查询获取仓库目前存粮情况
    /// </summary>
    /// <param name="whId">仓库ID</param>
    /// <returns></returns>
    public static DataTable GetStorageGrainByStorageId(string whId)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select MaterialNumber,MaterialName,sum(WPGBaseQty) as WPGBaseQty 
                       from View_WarePlaceGrain where WHID=@whId group by MaterialNumber,MaterialName";
        cmd.ClearParameters();
        cmd.setCommandText(sql);
        cmd.AddParameter("@whId", whId);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }

    /// <summary>
    /// 根据仓位ID查询获取堆位目前存粮情况
    /// </summary>
    /// <param name="wpId">仓位ID</param>
    /// <returns></returns>
    public static DataTable GetStorageGrainByStorageDetailId(string wpId)
    {
        DataManager dm = new Invengo.RiceManangeServices.DBCommon.DataManager();
        DBCommand cmd = dm.CreateDBCommand(CommandType.Text);

        string sql = @"select MaterialNumber,MaterialName,sum(WPGBaseQty) as WPGBaseQty 
                       from View_WarePlaceGrain where WPID=@wpId group by MaterialNumber,MaterialName ";
        cmd.ClearParameters();
        cmd.setCommandText(sql);
        cmd.AddParameter("@wpId", wpId);
        DataSet ds = dm.ExecProcCommand(cmd);
        return ds.Tables[0];
    }
}
