using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace winStudy
{
    public partial class GuideForm : Form
    {
        private MainForm mdiParent
        {
            get { return this.MdiParent as MainForm; }
        }

        public GuideForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 在本窗口获得焦点的时候，将窗口置于所有窗口的最底层，从而避免遮住其它窗口
        /// 但是会闪烁一下，如果你找到更好的方法，希望可以分享给我:914936620@qq.com。
        /// </summary>
        private void GuideForm_Activated(object sender, EventArgs e)
        {
            this.SendToBack();
        }
        //显示表达式窗口
        private void label1_Click(object sender, EventArgs e)
        {
            mdiParent.ShowExpressionForm();
        }
        //调用表达式窗口
        private void label2_Click(object sender, EventArgs e)
        {
            ExpressionForm frmExpre = mdiParent.FindChildForm(typeof(ExpressionForm)) as ExpressionForm;
            if (frmExpre == null)       //没有则创建
            {
                frmExpre = new ExpressionForm();
                frmExpre.MdiParent = MdiParent;
                frmExpre.Show();
            }
            //调公用方法
            frmExpre.Compute("3*7");
            frmExpre.Activate();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.our-code.com/");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.our-code.com/news/2010728/n355876.html");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mdiParent.ShowLoadAlert("在此显示提示信息！");
        }

        private void vistaButton1_Click(object sender, EventArgs e)
        {
            mdiParent.ShowVistaButtonForm();
        }
        //让导航子窗口不会最大化和最小化
        private void GuideForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Normal)
                this.WindowState = FormWindowState.Normal;
        }

        private void vistaButton2_Click(object sender, EventArgs e)
        {
            mdiParent.ShowScreenForm();
        }
       
    }
}