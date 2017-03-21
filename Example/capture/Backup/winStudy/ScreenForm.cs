using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Imaging;
using CSharpWin;
using System.Diagnostics;

namespace winStudy
{
    public partial class ScreenForm : Form
    {
        private MainForm mdiParent
        {
            get { return this.MdiParent as MainForm; }
        }
        private ProfessionalCaptureImageToolColorTable _colorTable =
            new ProfessionalCaptureImageToolColorTable();

        public ScreenForm()
        {
            InitializeComponent();
            //Cursor = new Cursor(winStudy.Properties.Resources.Arrow_M.Handle);
        }
        //Api抓全屏
        private void vistaButton1_Click(object sender, EventArgs e)
        {
            mdiParent.ShowPicForm();
        }
        //CopyFromScreen抓全屏
        private void vistaButton2_Click(object sender, EventArgs e)
        {
            //获得当前屏幕的分辨率
            Screen scr = Screen.PrimaryScreen;
            Rectangle rc = scr.Bounds;
            int iWidth = rc.Width;
            int iHeight = rc.Height;
            //创建一个和屏幕一样大的Bitmap
            Image myImage = new Bitmap(iWidth, iHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //从一个继承自Image类的对象中创建Graphics对象
            Graphics g = Graphics.FromImage(myImage);
            //抓屏并拷贝到myimage里
            //g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(iWidth, iHeight));
            g.CopyFromScreen(0, 0, 0, 0, new Size(iWidth, iHeight));
            //保存为文件
            //个人比较喜欢用PNG格式，比较清晰，同样图片，文件大小有时比JPG小，有时大，哈！
            string pic = Application.StartupPath + "\\myImage" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".png";
            myImage.Save(pic, ImageFormat.Png);
            myImage.Dispose();
            myImage = null;
            //显示图片
            mdiParent.PicFrom.ImageLocation = pic;
        }
        //动态截屏
        private void vistaButton3_Click(object sender, EventArgs e)
        {
            MyCaptureForm cf = new MyCaptureForm();
            cf.ShowDialog();
            if (cf.Image != null)
            {
                //显示图片
                if (mdiParent.PicFrom.Image != null)
                    mdiParent.PicFrom.Image.Dispose();
                mdiParent.PicFrom.Image = cf.Image;
            }
        }

        private void vistaButton4_Click(object sender, EventArgs e)
        {
            //所有实现代码在项目CaptureImageTool中
            CaptureImageTool capture = new CaptureImageTool();
            if (checkBoxCursor.Checked)
            {
                capture.SelectCursor = CursorManager.ArrowNew;
                capture.DrawCursor = CursorManager.CrossNew;
            }
            else
            {
                capture.SelectCursor = CursorManager.Arrow;
                capture.DrawCursor = CursorManager.Cross;
            }
            if (checkBoxColorTable.Checked)
            {
                capture.ColorTable = _colorTable;
            }

            if (capture.ShowDialog() == DialogResult.OK)
            {
                //显示图片
                if (mdiParent.PicFrom.Image != null)
                    mdiParent.PicFrom.Image.Dispose();
                mdiParent.PicFrom.Image = capture.Image;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //C# 实用截屏 模拟QQ抓屏
            Process.Start("http://www.our-code.com/news/2010927/n1790142.html");
            linkLabel1.LinkVisited = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //C# 实现完整功能的截图控件
            Process.Start("http://www.our-code.com/news/2010926/n0572139.html");
            linkLabel2.LinkVisited = true;
        }
    }
}