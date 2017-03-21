using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// 实体类Type_Class 。(属性说明自动提取数据库字段的描述信息)
/// </summary>
public class Type_Class
{
    public Type_Class()
    { }
    #region Model
    private int _id;
    private string _classid;
    private string _classname;
    private string _classlist;
    private string _classpre;
    private int _classtj;
    private int _classorder;
    private int _classkind;
    private string _keywords;
    private string _remark;
    /// <summary>
    /// 
    /// </summary>
    public int ID
    {
        set { _id = value; }
        get { return _id; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ClassId
    {
        set { _classid = value; }
        get { return _classid; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ClassName
    {
        set { _classname = value; }
        get { return _classname; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ClassList
    {
        set { _classlist = value; }
        get { return _classlist; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ClassPre
    {
        set { _classpre = value; }
        get { return _classpre; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int ClassTj
    {
        set { _classtj = value; }
        get { return _classtj; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int ClassOrder
    {
        set { _classorder = value; }
        get { return _classorder; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int ClassKind
    {
        set { _classkind = value; }
        get { return _classkind; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string KeyWords
    {
        set { _keywords = value; }
        get { return _keywords; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string Remark
    {
        set { _remark = value; }
        get { return _remark; }
    }
    #endregion Model
}
