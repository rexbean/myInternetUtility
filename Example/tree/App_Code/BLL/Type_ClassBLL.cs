using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data.OleDb;

/// <summary>
/// Type_ClassBLL 的摘要说明
/// </summary>
public class Type_ClassBLL
{
	public Type_ClassBLL()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public bool getClassPre(string classpre)
    {
        OleDbParameter[] parameters = {
            new OleDbParameter("@ClassPre", classpre)
        };
        return DBHelper.Exists("select * from Type_Class where ClassPre=@ClassPre", parameters);
    }
    /// <summary>
    /// 获取ClassId的包含菜单列表
    /// </summary>
    /// <param name="ClassId"></param>
    /// <returns></returns>
    public DataSet GetClassListByClassId(string ClassId)
    {
        OleDbParameter[] parameters = {
            new OleDbParameter("@ClassId", ClassId)
        };
        return DBHelper.ExecuteDataSet("select ClassList,ClassTj from Type_Class where ClassId=@ClassId", parameters);
    }

    /// <summary>
    /// 添加一个菜单项
    /// </summary>
    /// <param name="ClassId"></param>
    /// <param name="ClassName"></param>
    /// <param name="ClassList"></param>
    /// <param name="ClassPre"></param>
    /// <param name="ClassTj"></param>
    /// <returns></returns>
    public bool ClassAdd(Type_Class model)
    {
        OleDbParameter[] parameters = { 
            new OleDbParameter("@ClassId", model.ClassId),
            new OleDbParameter("@ClassName", model.ClassName),
            new OleDbParameter("@ClassList", model.ClassList),
            new OleDbParameter("@ClassPre", model.ClassPre),
            new OleDbParameter("@ClassTj", model.ClassTj),
            new OleDbParameter("@ClassKind", model.ClassKind),
            new OleDbParameter("@KeyWords", model.KeyWords),
            new OleDbParameter("@Remark", model.Remark)
        };

        return DBHelper.ExecuteNonqueryBool("insert into Type_Class(ClassId,ClassName,ClassList,ClassPre,ClassTj,ClassKind,KeyWords,Remark) values (@ClassId,@ClassName,@ClassList,@ClassPre,@ClassTj,@ClassKind,@KeyWords,@Remark)", parameters);
    }

    /// <summary>
    /// 编辑一个菜单项
    /// </summary>
    /// <param name="ClassId"></param>
    /// <param name="ClassName"></param>
    /// <param name="ClassList"></param>
    /// <param name="ClassPre"></param>
    /// <param name="ClassTj"></param>
    /// <returns></returns>
    public bool ClassSave(Type_Class model)
    {
        OleDbParameter[] parameters = { 
            new OleDbParameter("@ClassName", model.ClassName),
            new OleDbParameter("@ClassList", model.ClassList),
            new OleDbParameter("@ClassPre", model.ClassPre),
            new OleDbParameter("@ClassTj", model.ClassTj),
            new OleDbParameter("@KeyWords", model.KeyWords),
            new OleDbParameter("@Remark", model.Remark),
            new OleDbParameter("@ClassId", model.ClassId)
        };


        StringBuilder strSql = new StringBuilder();
        strSql.Append("update Type_Class set ");
        strSql.Append("ClassName=@ClassName,");
        strSql.Append("ClassList=@ClassList,");
        strSql.Append("ClassPre=@ClassPre,");
        strSql.Append("ClassTj=@ClassTj,");
        strSql.Append("KeyWords=@KeyWords,");
        strSql.Append("Remark=@Remark");
        strSql.Append(" where ClassId=@ClassId");


        //同步更新子菜单项
        DataSet ds = this.GetSubClassList(model.ClassId);
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Type_Class tc = new Type_Class();
                string SubClassList = model.ClassList + dr["ClassId"].ToString().Trim() + ",";

                tc.ClassId = dr["ClassId"].ToString().Trim();
                tc.ClassName = dr["ClassName"].ToString().Trim();
                tc.ClassList = SubClassList.ToString().Trim();
                tc.ClassPre = dr["ClassPre"].ToString().Trim();
                tc.ClassTj = model.ClassTj + 1;
                tc.KeyWords = dr["KeyWords"].ToString();
                tc.Remark = dr["Remark"].ToString();
                ClassSave(tc);
            }
        }

        return DBHelper.ExecuteNonqueryBool(strSql.ToString(), parameters);
        //return DBHelper.ExecuteNonqueryBool("update Type_Class set ClassName=@ClassName,ClassList=@ClassList,ClassPre=@ClassPre,ClassTj=@ClassTj,KeyWords=@KeyWords,Remark=@Remark where ClassId=@ClassId", parameters);
    }

    /// <summary>
    /// 获取该菜单项的所有子菜单项
    /// </summary>
    /// <param name="ClassId"></param>
    /// <returns></returns>
    public DataSet GetSubClassList(string ClassId)
    {
        OleDbParameter[] parameters = { };
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from Type_Class");
        strSql.Append(" where ClassPre='" + ClassId + "' ");
        return DBHelper.ExecuteDataSet(strSql.ToString(), parameters);
    }

    public DataSet GetByClassPre(string ClassId)
    {
        OleDbParameter[] parameters = { };
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from Type_Class");
        strSql.Append(" where ClassPre='" + ClassId + "' order by ClassOrder");
        return DBHelper.ExecuteDataSet(strSql.ToString(), parameters);
    }
    /// <summary>
    /// 获取菜单列表
    /// </summary>
    /// <returns></returns>
    public DataSet GetClassList(int ClassKind)
    {
        OleDbParameter[] parameters = { };
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from Type_Class");
        strSql.Append(" where ClassKind=" + ClassKind + "");
        strSql.Append(" Order By ClassList Asc,ClassOrder Asc");
        return DBHelper.ExecuteDataSet(strSql.ToString(), parameters);
    }

    public string GetPreClassId(string ClassId)
    {
        OleDbParameter[] parameters = { };
        StringBuilder strSql = new StringBuilder();
        strSql.Append("Select top 1 ClassPre From Type_Class");
        strSql.Append(" Where ClassId='" + ClassId + "'");
        return DBHelper.ExecuteObject(strSql.ToString(), parameters).ToString();
    }

    /// <summary>
    /// 删除一个菜单项
    /// </summary>
    /// <param name="ClassId"></param>
    /// <returns></returns>
    public bool DelByClassId(string ClassId)
    {
        OleDbParameter[] parameters = { };
        StringBuilder strSql = new StringBuilder();
        strSql.Append("Delete From Type_Class");
        strSql.Append(" where ClassId='" + ClassId + "'");
        return DBHelper.ExecuteNonqueryBool(strSql.ToString(), parameters);
    }

    /// <summary>
    /// 更新排序
    /// </summary>
    /// <param name="ClassId"></param>
    /// <param name="ClassOrder"></param>
    /// <returns></returns>
    public bool UpdateClassOrder(string ClassId, int ClassOrder)
    {
        OleDbParameter[] parameters = { };
        StringBuilder strSql = new StringBuilder();
        strSql.Append("Update Type_Class Set ");
        strSql.Append("ClassOrder=" + ClassOrder + " ");
        strSql.Append(" where ClassId='" + ClassId + "'");
        return DBHelper.ExecuteNonqueryBool(strSql.ToString(), parameters);
    }

    public DataTable GetModel(int id)
    {
        OleDbParameter param = new OleDbParameter();
        return DBHelper.ExecuteDataTable("select * from Type_Class where [ID]=" + id + "", param);
    }
    public DataTable GetModelByClassId(string classid)
    {
        OleDbParameter param = new OleDbParameter();
        return DBHelper.ExecuteDataTable("select * from Type_Class where [ClassId]='" + classid + "'", param);
    }
    public DataTable GetByYouWhere(string strWhere)
    {
        OleDbParameter param = new OleDbParameter();
        return DBHelper.ExecuteDataTable("select * from Type_Class where " + strWhere + "", param);
    }
    public DataTable GetByMeiWhere(string strWhere)
    {
        OleDbParameter param = new OleDbParameter();
        return DBHelper.ExecuteDataTable("select * from Type_Class " + strWhere + "", param);
    }
    public DataTable GetAll()
    {
        OleDbParameter param = new OleDbParameter();
        return DBHelper.ExecuteDataTable("select * from Type_Class", param);
    }
    public DataTable GetBySql(string sql)
    {
        OleDbParameter param = new OleDbParameter();
        return DBHelper.ExecuteDataTable(sql, param);
    }
}
