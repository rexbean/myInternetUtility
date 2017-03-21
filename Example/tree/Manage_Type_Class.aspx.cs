using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Manage_Type_Class : System.Web.UI.Page
{
    Type_Class model = new Type_Class();
    Type_ClassBLL tcbll = new Type_ClassBLL();

    #region ClassKind属性
    protected int ClassKind
    {
        get
        {
            return (int)ViewState["ClassKind"];
        }
        set
        {
            ViewState["ClassKind"] = value;
        }
    }
    #endregion

    DataTable data = new DataTable();

    private void Page_Init(object sender, System.EventArgs e)
    {
        DataColumn dc1 = new DataColumn("ID");
        DataColumn dc2 = new DataColumn("ClassID");
        DataColumn dc3 = new DataColumn("ClassName");
        DataColumn dc4 = new DataColumn("ClassList");
        DataColumn dc5 = new DataColumn("ClassPre");
        DataColumn dc6 = new DataColumn("ClassTj");
        DataColumn dc7 = new DataColumn("ClassOrder");
        DataColumn dc8 = new DataColumn("ClassKind");

        data.Columns.Add(dc1);
        data.Columns.Add(dc2);
        data.Columns.Add(dc3);
        data.Columns.Add(dc4);
        data.Columns.Add(dc5);
        data.Columns.Add(dc6);
        data.Columns.Add(dc7);
        data.Columns.Add(dc8);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.LabError.Text = "";

        if (!IsPostBack)
        {
            this.ClassKind = Convert.ToInt32(Request.QueryString["ClassKind"]);
            bind_tree("0");
            BindData();
        }
    }

    private void bind_tree(string ChildNodes)
    {
        //绑定根节点
        //OleDbCommand cmd = new OleDbCommand("select * from Type_Class where ClassPre='" + ChildNodes + "'", conn);
        //OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
        DataTable dt = tcbll.GetByClassPre(ChildNodes).Tables[0];
        //oda.Fill(dt);




        foreach (DataRow dr in dt.Rows)
        {
            DataRow dar = data.NewRow();
            dar[0] = dr[0];
            dar[1] = dr[1];
            dar[2] = dr[2];
            dar[3] = dr[3];
            dar[4] = dr[4];
            dar[5] = dr[5];
            dar[6] = dr[6];
            dar[7] = dr[7];
            data.Rows.Add(dar);
            //TreeNode tn = new TreeNode(dr["ClassName"].ToString(), dr["ClassId"].ToString());
            //tn.NavigateUrl = "Product.aspx?id=" + dr["id"].ToString();
            //tn.Expanded = false;
            //this.TreeView1.Nodes.Add(tn);

            bind_tree(dr["ClassId"].ToString());
        }

    }

    #region 数据绑定
    private void BindData()
    {

        this.btnAdd.Attributes.Add("onclick", "return ChkInput()");
        this.btnEdit.Attributes.Add("onclick", "return ChkInput()");

        //DataSet ds = tcbll.GetClassList(this.ClassKind);

        this.rptMenuList.DataSource = data;
        this.rptMenuList.DataBind();

        this.DdlMenu.Items.Clear();
        this.DdlMenu.Items.Add(new ListItem("添加为根栏目", "0"));
        foreach (DataRow dr in data.Rows)
        {
            int ClassTj = Convert.ToInt32(dr["ClassTj"]);
            string ClassId = dr["ClassId"].ToString().Trim();
            string ClassName = dr["ClassName"].ToString().Trim();

            if (ClassTj == 1)
            {
                this.DdlMenu.Items.Add(new ListItem(ClassName, ClassId));

            }
            else
            {
                ClassName = "├ " + ClassName;
                ClassName = StringHelper.StringOfChar(ClassTj - 1, "　") + ClassName;

                this.DdlMenu.Items.Add(new ListItem(ClassName, ClassId));
            }
        }

    }
    #endregion

    #region 添加按钮操作
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string ClassId = StringHelper.GetRamCode();                   //栏目ID
        string ClassName = this.txtClassName.Text.Trim();               //栏目名称
        string ClassList = "";                                          //栏目包含列表
        string ClassPre = this.DdlMenu.SelectedValue.Trim();            //上一级目录
        int ClassTj = 1;                                                //栏目深度
        string classtitle = this.txtClassTitle.Text;
        string remark = this.txtRemark.Text;

        if (ClassPre == "0")
        {
            ClassList = ClassId + ",";
            ClassTj = 1;
        }
        else
        {
            DataSet ds = tcbll.GetClassListByClassId(ClassPre);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                ClassList = dr["ClassList"].ToString().Trim() + ClassId + ",";
                ClassTj = Convert.ToInt32(dr["ClassTj"]) + 1;
            }
        }
        model.ClassId = ClassId;
        model.ClassName = ClassName;
        model.ClassList = ClassList;
        model.ClassPre = ClassPre;
        model.ClassTj = ClassTj;
        model.ClassOrder = 0;
        model.ClassKind = this.ClassKind;
        model.KeyWords = classtitle;
        model.Remark = remark;
        if (tcbll.ClassAdd(model))
        {
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ajaxjs", "alert('栏目添加成功！')", true);
            this.LabError.Text = "信息提示：栏目添加成功！";
        }
        else
        {
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ajaxjs", "alert('栏目添加操作失败！')", true);
            this.LabError.Text = "信息提示：栏目添加操作失败！";
        }
        this.txtClassName.Text = "";
        bind_tree("0");
        BindData();
    }
    #endregion

    #region 保存栏目信息
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        this.btnEdit.Visible = false;
        this.btnAdd.Visible = true;
        string ClassId = this.HidClassId.Value.Trim();
        string ClassName = this.txtClassName.Text.Trim();
        string ClassList = "";                                            //栏目包含列表
        string ClassPre = this.DdlMenu.SelectedValue.Trim();              //上一级目录
        int ClassTj = 1;                                                  //栏目深度
        string classtitle = this.txtClassTitle.Text;
        string remark = this.txtRemark.Text;

        if (ClassPre == "0")
        {
            ClassList = ClassId + ",";
            ClassTj = 1;
        }
        else
        {
            DataSet ds = tcbll.GetClassListByClassId(ClassPre);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                ClassList = dr["ClassList"].ToString().Trim() + ClassId + ",";
                ClassTj = Convert.ToInt32(dr["ClassTj"]) + 1;
            }
        }
        model.ClassId = ClassId;
        model.ClassName = ClassName;
        model.ClassList = ClassList;
        model.ClassPre = ClassPre;
        model.ClassTj = ClassTj;
        model.KeyWords = classtitle;
        model.Remark = remark;
        if (tcbll.ClassSave(model))
        {
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ajaxjs", "alert('栏目保存成功！')", true);
            this.LabError.Text = "信息提示：栏目保存成功！";
        }
        else
        {
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ajaxjs", "alert('栏目保存操作失败！')", true);
            this.LabError.Text = "信息提示：栏目保存操作失败！！";
        }
        this.txtClassName.Text = "";
        this.txtClassTitle.Text = "";
        this.txtClassTitle.Text = "";
        this.txtRemark.Text = "";
        bind_tree("0");
        BindData();
    }
    #endregion

    #region 显示数据处理
    protected void rptMenuList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Literal LitFirst = (Literal)e.Item.FindControl("LitFirst");
            //HyperLink LabClassNm = (HyperLink)e.Item.FindControl("LabClassNm");
            Label LabClassNm = (Label)e.Item.FindControl("LabClassNm");
            LinkButton linkbutton = (LinkButton)e.Item.FindControl("BtnEdit");

            //linkbutton.Attributes["onclick"] = "ToM();";//"javascript:window.location.href='Manage_Type_Class.aspx#M'";

            string LitStyle = "<span style=width:{0}px;text-align:right;display:inline-block;>{1}{2}</span>";

            string LitImg1 = "<img src=images/openfolder.gif align=absmiddle />";
            string LitImg2 = "<img src=images/file.gif align=absmiddle />";
            string LitImg3 = "<img src=images/t.gif align=absmiddle />";

            DataRowView drv = (DataRowView)e.Item.DataItem;
            int ClassTj = Convert.ToInt32(drv["ClassTj"]);

            if (ClassTj == 1)
            {
                LabClassNm.Font.Bold = true;
                LitFirst.Text = LitImg1;
            }
            else
            {
                LitFirst.Text = string.Format(LitStyle, ClassTj * 20, LitImg3, LitImg2);
            }
        }
    }
    #endregion

    #region 编辑删除处理
    protected void rptMenuList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        this.txtClassName.Text = "";
        this.txtClassTitle.Text = "";
        this.txtRemark.Text = "";

        HiddenField txtClassId = (HiddenField)e.Item.FindControl("txtClassId");
        //HyperLink LabClassNm = (HyperLink)e.Item.FindControl("LabClassNm");
        Label LabClassNm = (Label)e.Item.FindControl("LabClassNm");

        DataTable dt = tcbll.GetModelByClassId(txtClassId.Value);

        switch (e.CommandName.ToLower())
        {
            case "btnedit":
                if (dt.Rows.Count > 0)
                {
                    this.txtClassName.Text = dt.Rows[0]["ClassName"].ToString();
                    this.HidClassId.Value = dt.Rows[0]["ClassId"].ToString();
                    this.DdlMenu.SelectedValue = tcbll.GetPreClassId(txtClassId.Value.Trim()).Trim();
                    this.txtClassTitle.Text = dt.Rows[0]["ClassTitle"].ToString();
                    this.txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                    this.btnAdd.Visible = false;
                    this.btnEdit.Visible = true;
                }
                break;

            case "btndelete":
                //DataTable data = oimgbll.GetByYouWhere("ClassId='" + txtClassId.Value + "'");
                //if (data.Rows.Count > 0)
                //{
                //    this.LabError.Text = "信息提示：该栏目下还有产品存在，你无法删除！";
                //}
                //else
                //{
                    if (!tcbll.getClassPre(txtClassId.Value))
                    {
                        if (tcbll.DelByClassId(txtClassId.Value))
                        {
                            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ajaxjs", "alert('所选栏目删除成功！')", true);
                            this.LabError.Text = "信息提示：所选栏目删除成功！";
                        }
                        else
                        {
                            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ajaxjs", "alert('删除操作失败，请与管理员联系！')", true);
                            this.LabError.Text = "信息提示：删除操作失败，请与管理员联系！";
                        }
                        bind_tree("0");
                        BindData();
                    }
                    else
                    {
                        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ajaxjs", "alert('该目录下还有子目录存在，请先删除所有子目录！')", true);
                        this.LabError.Text = "信息提示：该目录下还有子目录存在，请先删除所有子目录！";
                    }
                //}
                break;
        }
    }
    #endregion

    #region 列表顺序保存操作
    /// <summary>
    /// 列表顺序保存操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int UpSum = 0;
        bool ReVal = true;
        foreach (RepeaterItem RptItem in this.rptMenuList.Items)
        {
            HiddenField txtClassId = (HiddenField)RptItem.FindControl("txtClassId");
            TextBox txtOrder = (TextBox)RptItem.FindControl("txtOrder");
            try
            {
                ReVal = tcbll.UpdateClassOrder(txtClassId.Value.Trim(), Convert.ToInt32(txtOrder.Text));
            }
            catch
            {
                ReVal = false;
            }

            if (ReVal == false)
            {
                ++UpSum;
            }
        }
        if (UpSum == 0)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ajaxjs", "alert('所有设定的栏目顺序都已保存成功！')", true);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ajaxjs", string.Format("alert('栏目顺序部份保存成功,共有{0}条记录保存失败!')", UpSum), true);
        }
        bind_tree("0");
        BindData();
    }
    #endregion
}
