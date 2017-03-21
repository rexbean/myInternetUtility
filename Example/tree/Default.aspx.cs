using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    Type_ClassBLL tcbll = new Type_ClassBLL();

    private void bind_tree(string ChildNodes, TreeNode tn)
    {       
        DataTable dt = tcbll.GetByClassPre(ChildNodes).Tables[0];

        foreach (DataRow dr in dt.Rows)
        {
            TreeNode Node = new TreeNode();
            if (tn == null)
            {    //添加根节点
                Node.Text = dr["ClassName"].ToString();                
                this.TreeView1.Nodes.Add(Node);
                bind_tree(dr["ClassID"].ToString(), Node);    //再次递归
            }
            else
            {   //添加当前节点的子节点
                Node.Text = dr["ClassName"].ToString();
                tn.ChildNodes.Add(Node);
                bind_tree(dr["ClassID"].ToString(), Node);     //再次递归
            }
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.bind_tree("0", null);
        }
    }
}
