using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CFJK.Windows;

namespace winStudy
{
    //本代码源于：http://www.our-code.com
    //程序员学习，积累编程点滴
    //欢迎加入，共同进步 QQ:914936620 QQ群:158094810
    public partial class MainForm : MainBaseForm
    {
        public PicForm PicFrom
        {
            get
            {
                PicForm picForm = FindChildForm(typeof(PicForm)) as PicForm;
                if (picForm == null)
                {
                    picForm = new PicForm(false);
                    picForm.MdiParent = this;
                    picForm.Show();
                }
                return picForm;
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            //mitemCSExp_Click(null ,null);
            //显示导航子窗体
            GuideForm frm = new GuideForm();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.MdiParent = this;
            frm.Show();

            类似QQ抓屏ToolStripMenuItem_Click(null, null);

            #region 动态加载菜单
            
            DataSet ds = new DataSet();
            ds.ReadXml(Application.StartupPath + "\\xmlfile1.xml");

            foreach (DataRow drow in ds.Tables[0].Rows)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = drow[0].ToString();
                item.Tag = drow[1];
                item.Click += new EventHandler(item_Click);
                mitemMore.DropDownItems.Add(item);
            }
            foreach (DataRow drow in ds.Tables[1].Rows)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = drow[0].ToString();
                item.Tag = drow[1];
                item.Click += new EventHandler(item_Click);
                mitemReg.DropDownItems.Add(item);
            }
            #endregion
        }

        /// <summary>
        /// 表达式菜单
        /// </summary>
        private void mitemCSExp_Click(object sender, EventArgs e)
        {
            ShowExpressionForm();
        }
        //打印+预览
        private void mitemPrint_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm(typeof(BillForm)))
            {
                BillForm frmChild = new BillForm();
                frmChild.MdiParent = this;
                frmChild.Show();
            }
        }
        /// <summary>
        /// 显示表达式窗口
        /// </summary>
        public void ShowExpressionForm()
        {
            if (!ShowChildrenForm(typeof(ExpressionForm)))
            {
                ExpressionForm frmExpre = new ExpressionForm();
                frmExpre.MdiParent = this;
                frmExpre.Show();
            }
        }

        private void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            System.Diagnostics.Process.Start(item.Tag.ToString());
        }

        private void 支持双击事件的日历控件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CldarForm(true).ShowDialog();
        }

        private void 支持Enabled的TabControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm(typeof(TabControlForm)))
            {
                TabControlForm frm = new TabControlForm();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void 右下角提示框ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLoadAlert("在此显示提示信息！");
        }

        private void c串口协议数据解析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm(typeof(SerialportSampleForm)))
            {
                SerialportSampleForm frm = new SerialportSampleForm();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void c监控计算机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm(typeof(WinListenForm)))
            {
                WinListenForm frm = new WinListenForm();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        public void ShowVistaButtonForm()
        {
            if (!ShowChildrenForm(typeof(VistaButtonForm)))
            {
                VistaButtonForm frm = new VistaButtonForm();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void vista风格按钮ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowVistaButtonForm();
        }
        public void ShowScreenForm()
        {
            if (!ShowChildrenForm(typeof(ScreenForm)))
            {
                ScreenForm frm = new ScreenForm();
                frm.MdiParent = this;
                frm.Show();
            }
        }
        private void 类似QQ抓屏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowScreenForm();
        }
    }
}