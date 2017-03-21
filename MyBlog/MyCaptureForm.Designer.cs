namespace MyBlog
{
    partial class MyCaptureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmenuRect = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.保存为文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出截屏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuRect.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmenuRect
            // 
            this.cmenuRect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存为文件ToolStripMenuItem,
            this.退出截屏ToolStripMenuItem});
            this.cmenuRect.Name = "cmenuRect";
            this.cmenuRect.Size = new System.Drawing.Size(137, 48);
            // 
            // 保存为文件ToolStripMenuItem
            // 
            this.保存为文件ToolStripMenuItem.Name = "保存为文件ToolStripMenuItem";
            this.保存为文件ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.保存为文件ToolStripMenuItem.Text = "保存为文件";
            // 
            // 退出截屏ToolStripMenuItem
            // 
            this.退出截屏ToolStripMenuItem.Name = "退出截屏ToolStripMenuItem";
            this.退出截屏ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.退出截屏ToolStripMenuItem.Text = "退出截屏";
            this.退出截屏ToolStripMenuItem.Click += new System.EventHandler(this.退出截屏ToolStripMenuItem_Click);
            // 
            // MyCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "MyCaptureForm";
            this.Text = "MyCaptureForm";
            this.DoubleClick += new System.EventHandler(this.MyCaptureForm_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MyCaptureForm_MouseDown);
            this.cmenuRect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip cmenuRect;
        private System.Windows.Forms.ToolStripMenuItem 保存为文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出截屏ToolStripMenuItem;
    }
}