//////////////////////////////////////////////////////////////
//filename:		SqlHelper.cs
//
//author:		
//
//date:			2007.10.31
//
//description:	数据持久层
////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

/// <summary>
/// SqlHelper 的摘要说明。
/// </summary>
public class DBHelper
{
    public static readonly string oleconstring = System.Configuration.ConfigurationSettings.AppSettings["oleconstring"];
    public DBHelper()
    {

    }
    /// <summary>
    /// OleDb数据库增、删、改方法
    /// </summary>
    /// <param name="sql">执行数据库操作语句</param>
    /// <param name="param">参数数组</param>
    /// <returns>返回int类型，返回0则操作失败，返回数大于0则操作成功</returns>
    public static int ExecuteNonquery(string sql, params OleDbParameter[] param)
    {
        int bFlag = 0;
        OleDbCommand cmd = new OleDbCommand();
        using (OleDbConnection con = new OleDbConnection(oleconstring))
        {
            cmd.Connection = con;
            cmd.CommandText = sql;

            if (param.Length > 0)
            {
                foreach (OleDbParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
            }

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                bFlag = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        return bFlag;
    }

    /// <summary>
    /// OleDb数据库增、删、改方法
    /// </summary>
    /// <param name="sql">执行数据库操作语句</param>
    /// <param name="param">参数数组</param>
    /// <returns>返回bool类型</returns>
    public static bool ExecuteNonqueryBool(string sql, params OleDbParameter[] param)
    {
        bool bFlag = false;
        OleDbCommand cmd = new OleDbCommand();
        using (OleDbConnection con = new OleDbConnection(oleconstring))
        {
            cmd.Connection = con;
            cmd.CommandText = sql;

            if (param.Length > 0)
            {
                foreach (OleDbParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
            }

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    bFlag = true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        return bFlag;
    }

    /// <summary>
    /// OleDb数据库查询方法
    /// </summary>
    /// <param name="sql">执行数据库操作语句</param>
    /// <param name="param">参数数组</param>
    /// <returns>返回DataTable类型</returns>
    public static DataTable ExecuteDataTable(string sql, params OleDbParameter[] param)
    {
        DataTable dt = new DataTable();
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd = new OleDbCommand();
        da.SelectCommand = cmd;
        using (OleDbConnection con = new OleDbConnection(oleconstring))
        {
            cmd.Connection = con;
            cmd.CommandText = sql;

            if (param.Length > 0)
            {
                foreach (OleDbParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
            }
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            { }
        }
        return dt;
    }
    /// <summary>
    /// OleDb数据库查询方法
    /// </summary>
    /// <param name="sql">执行数据库操作语句</param>
    /// <param name="param">参数数组</param>
    /// <returns>返回DataSet类型</returns>
    public static DataSet ExecuteDataSet(string sql, params OleDbParameter[] param)
    {
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommand cmd = new OleDbCommand();
        da.SelectCommand = cmd;
        using (OleDbConnection con = new OleDbConnection(oleconstring))
        {
            cmd.Connection = con;
            cmd.CommandText = sql;

            if (param.Length > 0)
            {
                foreach (OleDbParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
            }
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            { }
        }
        return ds;
    }
    /// <summary>
    /// OleDb数据库查询方法
    /// </summary>
    /// <param name="sql">执行数据库操作语句</param>
    /// <param name="param">参数数组</param>
    /// <returns>返回ArrayList类型</returns>
    public static ArrayList ExecuteArrayList(string sql, params OleDbParameter[] param)
    {
        OleDbDataReader reader = null;
        ArrayList al = new ArrayList();
        OleDbCommand cmd = new OleDbCommand();
        using (OleDbConnection con = new OleDbConnection(oleconstring))
        {
            cmd.Connection = con;
            cmd.CommandText = sql;

            if (param.Length > 0)
            {
                foreach (OleDbParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
            }
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    al.Add(reader[0]);
                }
                //while (reader.Read())
                //{
                //    //for (int i = 1; i < reader.FieldCount; i++)
                //    //{ 
                //    //    al.Add(reader[i]);
                //    //}
                //    foreach (Object obj in reader)
                //    {
                //        al.Add(obj);
                //    }
                //}
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            { }
        }
        return al;
    }

    /// <summary>
    /// OleDb数据库查询方法
    /// </summary>
    /// <param name="sql">执行数据库操作语句</param>
    /// <param name="param">参数数组</param>
    /// <returns>返回Object类型</returns>
    public static Object ExecuteObject(string sql, params OleDbParameter[] param)
    {
        OleDbDataReader reader = null;
        Object obj = null;
        OleDbCommand cmd = new OleDbCommand();
        using (OleDbConnection con = new OleDbConnection(oleconstring))
        {
            cmd.Connection = con;
            cmd.CommandText = sql;

            if (param.Length > 0)
            {
                foreach (OleDbParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
            }
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    obj = reader[0];
                }
                //while (reader.Read())
                //{
                //    //for (int i = 1; i < reader.FieldCount; i++)
                //    //{ 
                //    //    al.Add(reader[i]);
                //    //}
                //    foreach (Object obj in reader)
                //    {
                //        al.Add(obj);
                //    }
                //}
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            { }
        }
        return obj;
    }

    /// <summary>
    /// OleDb数据库验证方法
    /// </summary>
    /// <param name="sql">执行数据库操作语句</param>
    /// <param name="param">参数数组</param>
    /// <returns>返回bool类型</returns>
    public static bool Exists(string sql, params OleDbParameter[] param)
    {
        OleDbDataReader reader = null;
        bool flag = false;
        OleDbCommand cmd = new OleDbCommand();
        using (OleDbConnection con = new OleDbConnection(oleconstring))
        {
            cmd.Connection = con;
            cmd.CommandText = sql;

            if (param.Length > 0)
            {
                foreach (OleDbParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
            }
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                string msg = ex.Message;
            }
            finally
            { }
        }
        return flag;
    }
}
