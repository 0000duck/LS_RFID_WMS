using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///TablePrimary 的摘要说明
/// </summary>
public class TablePrimary
{
    public TablePrimary()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    private string _primaryName;
    private string _primaryValue;

    public string PrimaryName
    {
        set { _primaryName = value; }
        get { return _primaryName; }
    }

    public string PrimaryValue
    {
        set { _primaryValue = value; }
        get { return _primaryValue; }
    }
}
