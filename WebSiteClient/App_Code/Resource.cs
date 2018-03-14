using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Invengo.RiceManangeServices.DBCommon;
using Invengo.RiceManangeServices.Utility;
using Invengo.RiceManangeServices.Model;

/// <summary>
///Resource 的摘要说明
/// </summary>
public class Resource
{
    public Resource()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    ///  根据仓位编号查询仓位信息
    /// </summary>
    /// <param name="info">WarePlaceInfo实体类</param>
    /// <param name="pageSize">网页列表一次最多显示多少行记录</param>
    /// <param name="currentPage">用户查询的第几页信息</param>
    /// <param name="sortInfo">排列的列名实体类，可以指定几个列名，并且可以指定排列是升序还是降序</param>
    /// <param name="isFirstSearch">代表是否第一点击查询(如果是则需要对totalRecNum赋值，否则不需要，这样可以避免每次都去计算记录总数)</param>
    /// <param name="totalRecNum">根据查询条件查询的总记录数</param>
    /// <param name="retValue">返回Xml格式字符串实体类，字段包括标签号、人工仓位编号、长、宽、高，最大容量等</param>
    /// <returns>返回bool类型，错误返回false，正确返回true</returns>
    public static bool QueryStorageDetailList(WarePlaceInfo info, int pageSize, int currentPage, SortInfo sortInfo, bool isFirstSearch, 
        out int totalRecNum, out string retValue)
    {
        totalRecNum = 0;
        retValue = null;
        #region 处理查询语句和排序语句
        string sqlSelect = @"select wp.*";
        string[] sortColumns = null;
        if (sortInfo != null && !string.IsNullOrEmpty(sortInfo.SortSql))
        {
            sortColumns = sortInfo.SortSql.Split(',');
            string sqlSortIn = null;
            if (sortColumns != null && sortColumns.Length > 0)
            {
                foreach (string sortItem in sortColumns)
                {
                    sqlSortIn += "wp." + sortItem + ",";
                }
                sqlSortIn = sqlSortIn.Substring(0, sqlSortIn.Length - 1);
            }
            sqlSelect += ",Row_Number() Over(Order By " + sqlSortIn + ") AS serialNum";
        }
        else
        {
            sqlSelect += ",Row_Number() Over(Order By wp.id) AS serialNum";
        }
        string sqlFrom = @" from View_WarePlace wp where 1=1";
        #endregion

        #region 处理参数

        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();

        if (info != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = null;

            if (!string.IsNullOrEmpty(info.WPNumber))
            {
                sqlFrom += " AND wp.WPNumber like '%'+@WPNumber+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "WPNumber";
                sqlPara.Value = info.WPNumber;
                listSqlPara.Add(sqlPara);
            }
            if (!string.IsNullOrEmpty(info.WHID))
            {
                sqlFrom += " AND wp.WHID=@WHID";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "WHID";
                sqlPara.Value = info.WHID;
                listSqlPara.Add(sqlPara);
            }
        }

        #endregion

        return ExcutePage.ExcutePageCommand(sqlSelect, sqlFrom, sortColumns, pageSize, currentPage, listSqlPara, isFirstSearch, out totalRecNum, out retValue);
    }

    /// <summary>
    ///查询装卸点列表信息
    /// </summary>
    /// <param name="info">ZXPlaceInfo实体类（可以是根据装卸点编号，如果为null则查询所有装卸点信息）</param>
    /// <param name="pageSize">网页列表一次最多显示多少行记录</param>
    /// <param name="currentPage">用户查询的第几页信息</param>
    /// <param name="sortInfo">排列的列名实体类，可以指定几个列名，并且可以指定排列是升序还是降序</param>
    /// <param name="isFirstSearch">代表是否第一点击查询(如果是则需要对totalRecNum赋值，否则不需要，这样可以避免每次都去计算记录总数)</param>
    /// <param name="totalRecNum">根据查询条件查询的总记录数</param>
    /// <param name="retValue"返回string(DataSet格式)类型，字段包括仓库名称、装卸点编号、装卸点标签号等信息</param>
    /// <returns>返回bool类型，错误返回false，正确返回true</returns>
    public static bool QueryZXPlaceList(ZXPlaceInfo info, int pageSize, int currentPage, SortInfo sortInfo, bool isFirstSearch, out int totalRecNum, out string retValue)
    {
        totalRecNum = 0;
        retValue = null;
        #region 处理查询语句和排序语句
        string sqlSelect = @"select zx.*,wh.whnumber,wh.whname";
        string[] sortColumns = null;
        if (sortInfo != null && !string.IsNullOrEmpty(sortInfo.SortSql))
        {
            sortColumns = sortInfo.SortSql.Split(',');
            string sqlSortIn = null;
            if (sortColumns != null && sortColumns.Length > 0)
            {
                foreach (string sortItem in sortColumns)
                {
                    sqlSortIn += "zx." + sortItem + ",";
                }
                sqlSortIn = sqlSortIn.Substring(0, sqlSortIn.Length - 1);
            }
            sqlSelect += ",Row_Number() Over(Order By " + sqlSortIn + ") AS serialNum";
        }
        else
        {
            sqlSelect += ",Row_Number() Over(Order By zx.ZxPlaceID) AS serialNum";
        }
        string sqlFrom = @" from View_ZXPlace zx,WareHouse wh Where zx.whid=wh.whid";
        #endregion

        #region 处理参数

        List<System.Data.SqlClient.SqlParameter> listSqlPara = new List<System.Data.SqlClient.SqlParameter>();

        if (info != null)
        {
            System.Data.SqlClient.SqlParameter sqlPara = null;

            if (!string.IsNullOrEmpty(info.ZXPlaceCode))
            {
                sqlFrom += " AND zx.ZXPlaceCode like '%'+@ZXPlaceCode+'%'";
                sqlPara = new System.Data.SqlClient.SqlParameter();
                sqlPara.SqlDbType = SqlDbType.NVarChar;
                sqlPara.ParameterName = "ZXPlaceCode";
                sqlPara.Value = info.ZXPlaceCode;
                listSqlPara.Add(sqlPara);
            }
        }

        #endregion

        return ExcutePage.ExcutePageCommand(sqlSelect, sqlFrom, sortColumns, pageSize, currentPage, listSqlPara, isFirstSearch, out totalRecNum, out retValue);
    }

}
