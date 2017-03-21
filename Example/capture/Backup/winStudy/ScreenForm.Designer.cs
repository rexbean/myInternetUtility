namespace winStudy
{
    partial class ScreenForm
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
            this.vistaButton1 = new winStudy.Controls.VistaButton();
            this.vistaButton2 = new winStudy.Controls.VistaButton();
            this.vistaButton3 = new winStudy.Controls.VistaButton();
            this.vistaButton4 = new winStudy.Controls.VistaButton();
            this.checkBoxCursor = new System.Windows.Forms.CheckBox();
            this.checkBoxColorTable = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vistaButton1
            // 
            this.vistaButton1.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton1.ButtonColor = System.Drawing.Color.Blue;
            this.vistaButton1.ButtonText = "Api抓全屏";
            this.vistaButton1.Location = new System.Drawing.Point(12, 26);
            this.vistaButton1.Name = "vistaButton1";
            this.vistaButton1.Size = new System.Drawing.Size(100, 32);
            this.vistaButton1.TabIndex = 0;
            this.vistaButton1.Click += new System.EventHandler(this.vistaButton1_Click);
            // 
            // vistaButton2
            // 
            this.vistaButton2.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton2.BaseColor = System.Drawing.Color.Red;
            this.vistaButton2.ButtonText = "CopyFromScreen抓全屏";
            this.vistaButton2.Location = new System.Drawing.Point(13, 89);
            this.vistaButton2.Name = "vistaButton2";
            this.vistaButton2.Size = new System.Drawing.Size(147, 32);
            this.vistaButton2.TabIndex = 1;
            this.vistaButton2.Click += new System.EventHandler(this.vistaButton2_Click);
            // 
            // vistaButton3
            // 
            this.vistaButton3.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton3.ButtonColor = System.Drawing.Color.Lime;
            this.vistaButton3.ButtonText = "动态截屏";
            this.vistaButton3.Location = new System.Drawing.Point(12, 152);
            this.vistaButton3.Name = "vistaButton3";
            this.vistaButton3.Size = new System.Drawing.Size(185, 32);
            this.vistaButton3.TabIndex = 2;
            this.vistaButton3.Click += new System.EventHandler(this.vistaButton3_Click);
            // 
            // vistaButton4
            // 
            this.vistaButton4.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton4.BaseColor = System.Drawing.Color.LightPink;
            this.vistaButton4.ButtonText = "csharpwin截屏";
            this.vistaButton4.Location = new System.Drawing.Point(6, 20);
            this.vistaButton4.Name = "vistaButton4";
            this.vistaButton4.Size = new System.Drawing.Size(142, 32);
            this.vistaButton4.TabIndex = 3;
            this.vistaButton4.Click += new System.EventHandler(this.vistaButton4_Click);
            // 
            // checkBoxCursor
            // 
            this.checkBoxCursor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxCursor.AutoSize = true;
            this.checkBoxCursor.Location = new System.Drawing.Point(180, 75);
            this.checkBoxCursor.Name = "checkBoxCursor";
            this.checkBoxCursor.Size = new System.Drawing.Size(96, 16);
            this.checkBoxCursor.TabIndex = 22;
            this.checkBoxCursor.Text = "更换鼠标样式";
            this.checkBoxCursor.UseVisualStyleBackColor = true;
            // 
            // checkBoxColorTable
            // 
            this.checkBoxColorTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxColorTable.AutoSize = true;
            this.checkBoxColorTable.Location = new System.Drawing.Point(51, 75);
            this.checkBoxColorTable.Name = "checkBoxColorTable";
            this.checkBoxColorTable.Size = new System.Drawing.Size(96, 16);
            this.checkBoxColorTable.TabIndex = 21;
            this.checkBoxColorTable.Text = "更换颜色样式";
            this.checkBoxColorTable.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkLabel2);
            this.groupBox1.Controls.Add(this.checkBoxCursor);
            this.groupBox1.Controls.Add(this.vistaButton4);
            this.groupBox1.Controls.Add(this.checkBoxColorTable);
            this.groupBox1.Location = new System.Drawing.Point(12, 203);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 112);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(203, 163);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(113, 12);
            this.linkLabel1.TabIndex = 24;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "查看实现原理和过程";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(191, 40);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(113, 12);
            this.linkLabel2.TabIndex = 24;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "查看实现原理和过程";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // ScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 325);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.vistaButton3);
            this.Controls.Add(this.vistaButton2);
            this.Controls.Add(this.vistaButton1);
            this.Name = "ScreenForm";
            this.Text = "类似QQ抓屏";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private winStudy.Controls.VistaButton vistaButton1;
        private winStudy.Controls.VistaButton vistaButton2;
        private winStudy.Controls.VistaButton vistaButton3;
        private winStudy.Controls.VistaButton vistaButton4;
        private System.Windows.Forms.CheckBox checkBoxCursor;
        private System.Windows.Forms.CheckBox checkBoxColorTable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}