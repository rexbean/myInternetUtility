namespace winStudy
{
    partial class MyCaptureForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmenuRect = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mitemSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmenuRect.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmenuRect
            // 
            this.cmenuRect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitemSaveFile,
            this.mitemCancel});
            this.cmenuRect.Name = "cmenuRect";
            this.cmenuRect.Size = new System.Drawing.Size(131, 48);
            // 
            // mitemSaveFile
            // 
            this.mitemSaveFile.Name = "mitemSaveFile";
            this.mitemSaveFile.Size = new System.Drawing.Size(130, 22);
            this.mitemSaveFile.Text = "保存为文件";
            this.mitemSaveFile.Click += new System.EventHandler(this.mitemSaveFile_Click);
            // 
            // mitemCancel
            // 
            this.mitemCancel.Name = "mitemCancel";
            this.mitemCancel.Size = new System.Drawing.Size(130, 22);
            this.mitemCancel.Text = "退出截屏";
            this.mitemCancel.Click += new System.EventHandler(this.mitemCancel_Click);
            // 
            // MyCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "MyCaptureForm";
            this.Text = "MyCaptureForm";
            this.DoubleClick += new System.EventHandler(this.MyCaptureForm_DoubleClick);
            this.cmenuRect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmenuRect;
        private System.Windows.Forms.ToolStripMenuItem mitemSaveFile;
        private System.Windows.Forms.ToolStripMenuItem mitemCancel;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}