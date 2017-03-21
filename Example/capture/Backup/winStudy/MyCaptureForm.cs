using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace winStudy
{
    //本代码源于：http://www.our-code.com
    //程序员学习，积累编程点滴
    //欢迎加入，共同进步 QQ:914936620 QQ群:158094810
    public partial class MyCaptureForm : Form
    {
        private Rectangle currRect= Rectangle.Empty; //所画矩形
        private int ControlBlockIndex = -1;         //控制点索引
        private Point point;
        private Point distance;                      //移动时鼠标与矩形的距离
        private Cursor curRGB;      //彩色光标
        private Image image = null; //所截屏幕图
        private Image infoPanelPic;//提示面板
        private string currPixel;   //当前像素的RGB信息
        private bool isMouseDown=false;//鼠标左键是否按下

        public Image Image
        {
            get { return this.image; }
        }
        /// <summary>
        /// 遮罩层颜色
        /// </summary>
        private SolidBrush mask = new SolidBrush(Color.FromArgb(100,0,0,0));
        /// <summary>
        /// 原始屏幕图
        /// </summary>
        private Image ScreenImage;

        //抓屏Api
        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, CopyPixelOperation rop);
        [DllImport("user32.dll")]
        static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);
        [DllImport("gdi32.dll")]
        static extern IntPtr DeleteDC(IntPtr hDc);
        [DllImport("gdi32.dll")]
        static extern IntPtr DeleteObject(IntPtr hDc);
        [DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr ptr);

        public MyCaptureForm()
        {
            InitializeComponent();

            //双缓冲绘制，避免闪烁
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            //无边框
            this.FormBorderStyle = FormBorderStyle.None;
            //不在任务栏中显示，用户按开始键或Ctrl+Tab时
            this.ShowInTaskbar = false;
            //置于顶层
            this.TopMost = true;
            //窗口布满整个屏幕
            this.StartPosition = FormStartPosition.Manual;
            this.Bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            //查看任务管理器时用
            //this.Height -= 30;
            //接受键盘输入，用于快捷键，如Esc
            this.KeyPreview = true;

            //保留原来
            ScreenImage=GetScreenImage();
            //将当前屏做为背景
            Image BackScreen = new Bitmap(ScreenImage);
            Graphics g= Graphics.FromImage(BackScreen);
            //画遮罩
            g.FillRectangle(this.mask, 0, 0, BackScreen.Width, BackScreen.Height);
            Font font=new Font("宋体",18f,FontStyle.Bold);
            g.DrawString("OUR-CODE整理出品，QQ技术交流群：158094810", font, Brushes.Gold, 10, 10);
            g.DrawString("http://www.our-code.com", font, Brushes.Gold, 10, 35);
            g.DrawImage(winStudy.Properties.Resources.face1, 550, 5);
            g.Dispose();
            this.BackgroundImage = BackScreen;
            //彩色光标
            curRGB = new Cursor(winStudy.Properties.Resources.Arrow_M.Handle);
            Cursor = curRGB;
            toolTip1.SetToolTip(this, "按住左键不放选择截图区域,可重复选择");
        }
        private Image GetScreenImage()
        {
            //可以抓悬浮窗等LayeredWindow
            //出自http://social.msdn.microsoft.com/forums/en-US/winforms/thread/474450b9-e260-4369-9efb-0d57a5b2e06d/
            Size sz = Screen.PrimaryScreen.Bounds.Size;
            IntPtr hDesk = GetDesktopWindow();
            IntPtr hSrce = GetWindowDC(hDesk);
            IntPtr hDest = CreateCompatibleDC(hSrce);
            IntPtr hBmp = CreateCompatibleBitmap(hSrce, sz.Width, sz.Height);
            IntPtr hOldBmp = SelectObject(hDest, hBmp);
            bool b = BitBlt(hDest, 0, 0, sz.Width, sz.Height, hSrce, 0, 0, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
            Bitmap bmp = Bitmap.FromHbitmap(hBmp);
            SelectObject(hDest, hOldBmp);
            DeleteObject(hBmp);
            DeleteDC(hDest);
            ReleaseDC(hDesk, hSrce);
            return bmp;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                point.X = e.X;
                point.Y = e.Y;
                if (currRect == Rectangle.Empty)//第一次保持原光标不变
                {
                    currRect.X = e.X;
                    currRect.Y = e.Y;
                    ControlBlockIndex = GetSelectedHandle(point);
                }
                else
                {
                    ControlBlockIndex = GetSelectedHandle(point);
                    if (ControlBlockIndex < 0)      //重新画
                    {
                        currRect = Rectangle.Empty;
                        currRect.X = e.X;
                        currRect.Y = e.Y;
                        ControlBlockIndex = GetSelectedHandle(point);
                    }
                    else                            //重新画保持原光标不变
                        this.SetCursor();
                    this.distance.X = e.Location.X - currRect.Location.X;
                    this.distance.Y = e.Location.Y - currRect.Location.Y;
                }
            }
            base.OnMouseDown(e);
        }

        private int GetSelectedHandle(Point p)
        {
            int index = -1;
            for (int i = 1; i < 9; i++)
            {
                if (GetHandleRect(i).Contains(p))
                {
                    index = i;
                    break;
                }
            }
            if (this.currRect.Contains(p))
                index = 0;
            return index;
        }
        /// <summary>
        /// 得到控制点的矩形范围
        /// </summary>
        private Rectangle GetHandleRect(int index)
        {
            Point point = GetHandle(index);
            return new Rectangle(point.X - 3, point.Y - 3, 7, 7);
        }
        /// <summary>
        /// 得到控制点 共8个
        /// </summary>
        private Point GetHandle(int index)
        {
            int x, y, xCenter, yCenter;

            x = currRect.X;
            y = currRect.Y;
            xCenter = currRect.X + currRect.Width / 2;
            yCenter = currRect.Y + currRect.Height / 2;

            switch (index)
            {
                case 1:
                    x = currRect.X;
                    y = currRect.Y;
                    break;
                case 2:
                    x = xCenter;
                    y = currRect.Y;
                    break;
                case 3:
                    x = currRect.Right;
                    y = currRect.Y;
                    break;
                case 4:
                    x = currRect.Right;
                    y = yCenter;
                    break;
                case 5:
                    x = currRect.Right;
                    y = currRect.Bottom;
                    break;
                case 6:
                    x = xCenter;
                    y = currRect.Bottom;
                    break;
                case 7:
                    x = currRect.X;
                    y = currRect.Bottom;
                    break;
                case 8:
                    x = currRect.X;
                    y = yCenter;
                    break;
            }

            return new Point(x, y);
        }

        /// <summary>
        /// 设置鼠标样式
        /// </summary>
        private void SetCursor()
        {
            Cursor cr = curRGB;// Cursors.Default;
            if (ControlBlockIndex == 1 || ControlBlockIndex == 5)
            {
                cr = Cursors.SizeNWSE;
            }
            else if (ControlBlockIndex == 2 || ControlBlockIndex == 6)
            {
                cr = Cursors.SizeNS;
            }
            else if (ControlBlockIndex == 3 || ControlBlockIndex == 7)
            {
                cr = Cursors.SizeNESW;
            }
            else if (ControlBlockIndex == 4 || ControlBlockIndex == 8)
            {
                cr = Cursors.SizeWE;
            }
            else if (ControlBlockIndex == 0)
            {
                cr = Cursors.SizeAll;
            }
            Cursor.Current = cr;
        }

        //鼠标移动
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Color pixel = ((Bitmap) ScreenImage).GetPixel(e.X, e.Y);
            this.currPixel = string.Format("({0},{1},{2})", pixel.R, pixel.G, pixel.B);
            if (isMouseDown)
            {
                this.MoveHandleTo(e.Location);
                this.Invalidate();
            }
            else
            {
                if (!this.currRect.IsEmpty)
                {
                    this.ControlBlockIndex = this.GetSelectedHandle(e.Location);
                    this.SetCursor();
                    //鼠标移动时刷新，显示当前点的RGB值，比较耗资源
                    this.Invalidate();
                }
            }
            base.OnMouseMove(e);
        }
        /// <summary>
        /// 根据控制，计算所画矩形位置和大小
        /// </summary>
        private void MoveHandleTo(Point pt)
        {
            int left = currRect.Left;
            int top = currRect.Top;
            int right = currRect.Right;
            int bottom = currRect.Bottom;

            switch (ControlBlockIndex)
            {
                case 0:
                    Point location = pt;
                    location.X -= this.distance.X;
                    location.Y -= this.distance.Y;

                    if (location.X < 0)
                    {
                        location.X = 0;
                    }
                    if (location.Y < 0)
                    {
                        location.Y = 0;
                    }
                    if (location.X > (this.Width - this.currRect.Width))
                    {
                        location.X = this.Width - this.currRect.Width;
                    }
                    if (location.Y > (this.Height - this.currRect.Height))
                    {
                        location.Y = this.Height - this.currRect.Height;
                    }
                    this.currRect.Location = location;
                    return;
                case 1:
                    left = pt.X;
                    top = pt.Y;
                    break;
                case 2:
                    top = pt.Y;
                    break;
                case 3:
                    right = pt.X;
                    top = pt.Y;
                    break;
                case 4:
                    right = pt.X;
                    break;
                case 5:
                    right = pt.X;
                    bottom = pt.Y;
                    break;
                case 6:
                    bottom = pt.Y;
                    break;
                case 7:
                    left = pt.X;
                    bottom = pt.Y;
                    break;
                case 8:
                    left = pt.X;
                    break;
            }
            this.point = pt;
            currRect.X = left;
            currRect.Y = top;
            currRect.Width = right - left;
            currRect.Height = bottom - top;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            //右键释放
            if (e.Button == MouseButtons.Right)
            {
                ExitForm(true);
            }
            else if(e.Button== MouseButtons.Left)
            {
                isMouseDown = false;
                //整理矩形，使其定位点为左上
                int left = currRect.Left;
                int top = currRect.Top;
                int right = currRect.Right;
                int bottom = currRect.Bottom;
                currRect.X = Math.Min(left, right);
                currRect.Y = Math.Min(top, bottom);
                currRect.Width = Math.Abs(left - right);
                currRect.Height = Math.Abs(top - bottom);
                this.ControlBlockIndex = this.GetSelectedHandle(e.Location);
                this.SetCursor();
            }

            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (currRect.IsEmpty)
            {
                base.OnPaint(e);
                return;
            }
            
            Graphics g = e.Graphics;
            //画突出显示的部分
            g.DrawImage(this.ScreenImage, this.currRect, this.currRect, GraphicsUnit.Pixel);
            //画矩形
            Pen pen = new Pen(SystemColors.GradientActiveCaption);
            //g.DrawRectangle(pen, currRect);//this.ForeColor
            g.DrawLine(pen, currRect.Left, currRect.Top, currRect.Right, currRect.Top);
            g.DrawLine(pen, currRect.Left, currRect.Top, currRect.Left, currRect.Bottom);
            g.DrawLine(pen, currRect.Left, currRect.Bottom, currRect.Right, currRect.Bottom);
            g.DrawLine(pen, currRect.Right, currRect.Top, currRect.Right, currRect.Bottom);
            //画控制点
            for (int i = 1; i < 9; i++)
            {
                g.FillRectangle(new SolidBrush(SystemColors.GradientActiveCaption), this.GetHandleRect(i));//Red
            }
            //画信息面板
            this.DrawInfoPanel(g);

            base.OnPaint(e);
        }
        //双击截图
        private void MyCaptureForm_DoubleClick(object sender, EventArgs e)
        {
            if (this.currRect.Width <= 0 || this.currRect.Height <= 0)
            {
                this.DialogResult = DialogResult.Cancel;
                return;
            }
            Bitmap bm = new Bitmap(this.currRect.Width, this.currRect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bm);
            g.DrawImage(this.ScreenImage, 0, 0, this.currRect, GraphicsUnit.Pixel);
            this.image = bm;

            this.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 画信息面板
        /// </summary>
        protected void DrawInfoPanel(Graphics g)
        {
            int x=((this.currRect.X + 160) > this.Width) ? (this.currRect.X - 160) : this.currRect.X;
            int y=((this.currRect.Y - 76) > 0) ? (this.currRect.Y - 76) : (this.currRect.Y + 6);
            //CFJK.Windows.AppLog.AddLog(currRect.Location.ToString() + " " + x.ToString() + "," + y.ToString());
            string zone=Math.Abs(currRect.Width).ToString() + "*" + Math.Abs(currRect.Height).ToString();

            if (this.infoPanelPic == null)
            {
                this.infoPanelPic = new Bitmap(160, 70);
            }
            Graphics graphics = Graphics.FromImage(this.infoPanelPic);
            graphics.Clear(this.mask.Color);
            //graphics.FillRectangle(this.mask, 0, 0, this.infoPanelPic.Width, this.infoPanelPic.Height);
            Font font = new Font("宋体", 9f);
            Brush brush = Brushes.LemonChiffon;
            graphics.DrawString("区域大小:", font, brush, new PointF(3f, 8f));
            graphics.DrawString("当前RGB:", font, brush, new PointF(3f, 26f));
            graphics.DrawString("双击确认,右击取消。", font, brush, new PointF(3f, 52f));
            graphics.DrawString(zone, font, brush, new PointF(60f, 8f));
            graphics.DrawString(this.currPixel, font, brush, new PointF(55f, 26f));
            g.DrawImageUnscaled(infoPanelPic, x, y);
            font.Dispose();
            graphics.Dispose();
        }

        private void ExitForm(bool isMouse)
        {
            if (this.currRect == Rectangle.Empty)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                //右键在矩形区中，显示快捷菜单
                if (isMouse && currRect.Contains(Cursor.Position))
                {
                    cmenuRect.Show(Cursor.Position);
                }
                else
                {
                    this.currRect = Rectangle.Empty;
                    this.Invalidate();
                }
            }
        }
        #region 快捷键

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyData==Keys.Escape)
            {
                ExitForm(false);
            }
            base.OnKeyUp(e);
        }
        #endregion
        #region 右键菜单
        //保存为文件
        private void mitemSaveFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("自已写吧，哈哈！", "QQ群：158094810");
        }
        //退出截屏
        private void mitemCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }       
        #endregion
    }
}