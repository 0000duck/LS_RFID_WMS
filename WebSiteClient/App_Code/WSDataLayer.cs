using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Web.UI.WebControls;

/// <summary>
///WSDataLayer 的摘要说明
/// </summary>
public class WSDataLayer
{
    public WSDataLayer()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //        
    }   

    public static string GetJSScript(string jsScript)
    {
        return "<script>" + jsScript + "</script>";
    }

    /// <summary>
    /// 查询的结束日期是当天其实是查询明天截止的
    /// </summary>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public static DateTime GetEndDate(DateTime endDate)
    {
        DateTime dateRet = endDate.AddDays(1).AddSeconds(-1);
        return dateRet;
    }

    public static void EnablePageControl(LinkButton lbtFirstPage, LinkButton lbtPreviousPage, LinkButton lbtNextPage, LinkButton lbtLastPage, int recordNum, int pageSize,int totalPageNum,int page)
    {
        if (recordNum <= pageSize)
        {
            lbtFirstPage.Visible = false;
            lbtPreviousPage.Visible = false;
            lbtNextPage.Visible = false;
            lbtLastPage.Visible = false;
        }
        else
        {
            if (page == 1)
            {
                lbtFirstPage.Visible = false;
                lbtPreviousPage.Visible = false;
                lbtNextPage.Visible = true;
                lbtLastPage.Visible = true;
            }
            else if (page == totalPageNum)
            {
                lbtFirstPage.Visible = true;
                lbtPreviousPage.Visible = true;
                lbtNextPage.Visible = false;
                lbtLastPage.Visible = false;
            }
            else
            {
                lbtFirstPage.Visible = true;
                lbtPreviousPage.Visible = true;
                lbtNextPage.Visible = true;
                lbtLastPage.Visible = true;
            }
        }
    }
   
}
