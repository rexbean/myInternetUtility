using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace winStudy
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //启动MDI主窗口，查看更多实例
            Application.Run(new MainForm());
            //隐藏运行使用下面代码
            //WinListenForm app = new WinListenForm(true);
            //if (app.mutex != null)
            //{
            //    Application.Run(app);
            //}
            //else
            //{
            //    MessageBox.Show(app, "程序已经有一个实例在运行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
    }
}