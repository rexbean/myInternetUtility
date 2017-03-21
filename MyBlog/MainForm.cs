using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Net;
using mshtml;
using System.Collections;
using System.Web;
using System.Threading;

namespace MyBlog
{

   
    public partial class MyBlog : Form
    {
        string newName;
        string newAddress;
        private IHTMLDocument2 doc=null;
        private bool updatingFontName = false;
        private string currentTextName = null;

        public MyBlog()
        {
            InitializeComponent();
            SetTreeView();
            SetWebSiteTree();
            SetFontBox();
            SetFontSizeBox();
            SetWebBrowser();
            SetMenuTabControl();
        }


        private void MyBlog_SizeChanged(object sender, EventArgs e)
        {
            DBTree.Height = this.Height - 107;
            webSiteTree.Height = this.Height - 107;

            TabControl.Width = this.Width - DBTree.Width - 50;
            TabControl.Height = this.Height - 130;

            MenuTabControl.Width = DBTree.Width+15;
            MenuTabControl.Height = this.Height - 75;

            WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
            a.Width = TabControl.Width - 15;
            a.Height = TabControl.Height;

        }//控件大小随窗体大小改变

        private void MyBlog_FormClosing(object sender, FormClosingEventArgs e)
        {
            OutportWebSiteTree();
        }

        private void CreateNewForm()
        {
            Form form1 = new Form();
            form1.Text = "新建对话框";
            form1.Height = 150;

            Label title = new Label();

            title.Text = "新建的名称为：";
            title.Location = new Point(form1.Width / 10, form1.Height / 5);

            TextBox newName = new TextBox();
            newName.Location = new Point(form1.Width / 10 + title.Width, form1.Height / 5 - 5);
            newName.Width = 100;

            Button okButton = new Button();
            okButton.Text = "确定";
            okButton.Location = new Point(title.Location.X, title.Location.Y + 30);
            okButton.Click += new EventHandler(okButton_Click);

            Button cancelButton = new Button();
            cancelButton.Text = "取消";
            cancelButton.Location = new Point(title.Location.X + okButton.Width + 50, title.Location.Y + 30);
            cancelButton.Click += new EventHandler(cancelButton_Click);

            form1.Controls.Add(title);
            form1.Controls.Add(newName);
            form1.Controls.Add(okButton);
            form1.Controls.Add(cancelButton);
            form1.ShowDialog();

        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            Form a = ActiveForm;
            a.Close();
        }

        void okButton_Click(object sender, EventArgs e)
        {
            Form a = ActiveForm;
            newName = a.Controls[1].Text;
            if (a.Controls[1].Text != null)
            {
                a.Close();
            }

        }

        



        public FontFamily FontName
        {
            get
            {
                string name = doc.queryCommandValue("FontName") as string;
                if (name == null) return null;
                return new FontFamily(name);
            }
            set
            {
                if (value != null)
                {
                    WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
                    a.Document.ExecCommand("FontName", false, value.Name);
                }
            }
        }

        private void SetWebBrowser()
        {
           
            webBrowser1.DocumentText = string.Empty;
            webBrowser1.Document.ExecCommand("EditMode", false, null);
            webBrowser1.Document.ExecCommand("LiveResize", false, null);
            webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(mainBrowser_NewWindow);
            webBrowser1.Height = TabControl.Height;
            webBrowser1.Width = TabControl.Width - 15;

            webBrowser2.DocumentText = string.Empty;
            webBrowser2.Document.ExecCommand("EditMode", false, null);
            webBrowser2.Document.ExecCommand("LiveResize", false, null);
            webBrowser2.NewWindow += new System.ComponentModel.CancelEventHandler(mainBrowser_NewWindow);
            webBrowser2.Height = TabControl.Height;
            webBrowser2.Width = TabControl.Width - 15;

            //WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
            //a.Width = TabControl.Width - 15;
            //a.Height = TabControl.Height;

        }

        private void SetFontBox()
        {
            AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
            foreach (FontFamily fam in FontFamily.Families)
            {
                FontComboBox.Items.Add(fam.Name);
                ac.Add(fam.Name);
            }
            FontComboBox.Leave += new EventHandler(FontComboBox_TextChanged);
            FontComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            FontComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            FontComboBox.AutoCompleteCustomSource = ac;

        }

        private void SetFontSizeBox()
        {
            AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
            for (int i = 1; i < 8; i++)
            {
                SizeComboBox.Items.Add(i);
                ac.Add(i.ToString());
            }
            Console.WriteLine(SizeComboBox.SelectedText);
            SizeComboBox.Leave += new EventHandler(SizeComboBox_TextChanged);
            SizeComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            SizeComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            SizeComboBox.AutoCompleteCustomSource = ac;

        }

        private void SetTreeView()
        {
            TabControl.Width = this.Width - DBTree.Width - 50;
            TabControl.Height = this.Height - 130;
            
            DBTree.Height = this.Height - 107;
            String path = Application.StartupPath;

            imageList1.Images.Add(Image.FromFile(path+"\\ico\\Folder.png"));
            imageList1.Images.Add(Image.FromFile(path+"\\ico\\123.png"));
            DBTree.ImageList = imageList1;
            //DBTree.Click += new EventHandler(DBTree_Click);
        }

        private void SetWebSiteTree()
        {
            webSiteTree.Location = DBTree.Location;
            webSiteTree.Width = DBTree.Width;
            webSiteTree.Height = this.Height - 107;
            String path = Application.StartupPath;
            imageList1.Images.Add(Image.FromFile(path+"\\ico\\Folder.png"));
            imageList1.Images.Add(Image.FromFile(path+"\\ico\\123.png"));
            webSiteTree.ImageList = imageList1;
            ImportDefaultWebSiteTree();
 
        }

        

        private void SetMenuTabControl()
        {
            MenuTabControl.Width = DBTree.Width+15;
            MenuTabControl.Height = this.Height - 70;
        }

        private void DBTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (DBTree.SelectedNode != null)
            {
                string SourceCode = null;
                TreeNode tn = DBTree.SelectedNode;
                DataBase tnDB = new DataBase();
                WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
                tnDB = (DataBase)tn.Tag;
                if (tnDB.type == "W")
                {
                    if (File.Exists(tnDB.address) == true)
                    {
                        FileStream aFile = new FileStream(tnDB.address, FileMode.Open);
                        StreamReader sr = new StreamReader(aFile);

                        SourceCode = sr.ReadToEnd();
                        a.DocumentText = SourceCode;
                        //a.Navigate(tnDB.address);
                        currentTextName = tnDB.address;
                        sr.Close();
                    }
                    else
                    {
                        a.DocumentText = "该文件不存在";
                    }
                }
            }
        }

        #region file menu

        private void 新建数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewForm();
            CreateNewDataBase(newName);

        }

        private void 导入数据库ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string strLine;
            string[] readArray;
            
            openFileDialog1.Filter = "文本文件|*.txt|所有文件|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<TreeNode> DBtreeNodes = new List<TreeNode>();

                try
                {
                    FileStream aFile = new FileStream(openFileDialog1.FileName, FileMode.Open);
                    StreamReader sr = new StreamReader(aFile);

                    strLine = sr.ReadLine();
                    readArray = strLine.Split(' ');

                    DataBase rootDB = new DataBase();
                    rootDB.name = readArray[1];
                    rootDB.type = "D";
                    rootDB.address = readArray[4];

                    TreeNode root = new TreeNode();
                    root.Tag = rootDB;
                    root.Text = rootDB.name;

                    DBtreeNodes.Add(root);

                    strLine = sr.ReadLine();

                    while (strLine != null)
                    {
                        int father = 0;
                        readArray = strLine.Split(' ');

                        DataBase childDB = new DataBase();
                        childDB.name = readArray[1];
                        childDB.father = int.Parse(readArray[2]);
                        childDB.type = readArray[3];
                        childDB.address = readArray[4];

                        father = childDB.father;

                        TreeNode tn = new TreeNode();
                        tn.Text = childDB.name;
                        switch (childDB.type)
                        {
                            case "D": tn.ImageIndex = 0; tn.SelectedImageIndex = 0; tn.Tag = childDB; break;
                            case "W": tn.ImageIndex = 1; tn.SelectedImageIndex = 1; tn.Tag = childDB; break;

                        }

                        DBtreeNodes.Add(tn);
                        if (father == -1)
                        {
                            DBTree.Nodes.Add(tn);
                        }
                        else
                        {
                            DBtreeNodes[father].Nodes.Add(tn);
                        }
                        strLine = sr.ReadLine();


                    }
                    sr.Close();
                }
                catch (IOException ex)
                {
                    Console.WriteLine("An IOException has been thrown!");
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine();
                    return;
                }

                DBTree.Nodes.Add(DBtreeNodes[0]);
            }  



            


        }

        private void 保存数据库toolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            List<TreeNode> treeNodesList = new List<TreeNode>();
            for (int i = 0; i < DBTree.Nodes.Count; i++)
            {
                DataBase root = (DataBase)DBTree.Nodes[i].Tag;
                root.father = -1;
                treeNodesList.Add(DBTree.Nodes[i]);
            }
            if (DBTree.Nodes.Count > 1)
            {
                string currentTime = (System.DateTime.Now.Year
                                      + System.DateTime.Now.Month
                                      + System.DateTime.Now.Day
                                      + System.DateTime.Now.Hour
                                      + System.DateTime.Now.Minute
                                      + System.DateTime.Now.Second).ToString();
                name = "复数据库" + currentTime;
            }
            else
            {
                name = DBTree.Nodes[0].Text;
            }

            StreamWriter sw = new StreamWriter(@"D:\inspiration\MyBlog\MyBlog\DB\" + name + ".txt");
            //sw.WriteLine("0 " + name + " -1 D");

            for (int i = 0; i < treeNodesList.Count; i++)
            {
                for (int j = 0; j < treeNodesList[i].Nodes.Count; j++)
                {
                    DataBase tnDB1 = (DataBase)treeNodesList[i].Nodes[j].Tag;
                    tnDB1.father = i;
                    treeNodesList.Add(treeNodesList[i].Nodes[j]);
                }

            }

            for (int i = 0; i < treeNodesList.Count; i++)
            {
                DataBase tnDB = (DataBase)treeNodesList[i].Tag;
                sw.WriteLine(i + " " + treeNodesList[i].Text + " " + tnDB.father + " " + tnDB.type+" "+tnDB.address);

            }
            sw.Close();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region treeView

        private void TreeViewcontextMenu_Opening(object sender, CancelEventArgs e)
        {

            if (DBTree.SelectedNode.Tag.ToString() == "W")
            {
               
                TreeViewcontextMenu.Items[0].Visible = false;
                TreeViewcontextMenu.Items[1].Visible = false;
                TreeViewcontextMenu.Items[2].Visible = false;
            }
            else
            {
                TreeViewcontextMenu.Items[0].Visible = true;
                TreeViewcontextMenu.Items[1].Visible = true;
                TreeViewcontextMenu.Items[2].Visible = true;
            }
        }// 右键菜单

        private void DBTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = DBTree.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    DBTree.SelectedNode = CurrentNode;//选中这个节点
                }
            }
        } //右键选中

        #endregion

        #region right button 

        private void 新建文件夹ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                新建文件夹ToolStripMenuItem_Click();
            }
        }

        private void 新建文件夹ToolStripMenuItem_Click() //新建文件夹
        {
            CreateNewForm();

            DataBase newNodeDB = new DataBase();
            newNodeDB.name = newName;
            newNodeDB.type = "D";
            newNodeDB.time = System.DateTime.Now.ToString() ;

            DataBase fatherNodeDB=new DataBase();
            fatherNodeDB=(DataBase)DBTree.SelectedNode.Tag;
            newNodeDB.address = fatherNodeDB.address+"\\"+ newName;

            TreeNode newNode = new TreeNode();
            newNode.Text = newName;
            newNode.ImageIndex = 0;
            newNode.SelectedImageIndex = 0;
            newNode.Tag = newNodeDB;
            DBTree.SelectedNode.Nodes.Add(newNode);
            DBTree.SelectedNode.Expand();

            Directory.CreateDirectory(newNodeDB.address);
        }

        private void 新建文本ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                新建文本ToolStripMenuItem_Click();
            }
           
        }

        private void 新建文本ToolStripMenuItem_Click()
        {
            CreateNewForm();

            DataBase newNodeDB = new DataBase();
            newNodeDB.name = newName;
            newNodeDB.type = "W";
            newNodeDB.time = System.DateTime.Now.ToString();

            DataBase fatherNodeDB = new DataBase();
            fatherNodeDB = (DataBase)DBTree.SelectedNode.Tag;
            if (fatherNodeDB.address[fatherNodeDB.address.Length - 1] != '\\')
            {
                newNodeDB.address = fatherNodeDB.address + "\\" + newName + ".txt";
            }
            else
            {
                newNodeDB.address = fatherNodeDB.address + newName + ".txt";
 
            }


            TreeNode newNode = new TreeNode();
            newNode.Text = newName;
            newNode.ImageIndex = 1;
            newNode.SelectedImageIndex = 1;
            newNode.Tag = newNodeDB;
            DBTree.SelectedNode.Nodes.Add(newNode);

            DBTree.SelectedNode.Expand();

            FileStream myFs = new FileStream(newNodeDB.address, FileMode.Create);
            StreamWriter mySw = new StreamWriter(myFs);
            currentTextName = newNodeDB.address;
            //mySw.Write();
            mySw.Close();
            myFs.Close();
        }

        private void 删除ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                删除ToolStripMenuItem_Click();
            }
        }

        private void 删除ToolStripMenuItem_Click()
        {
            DBTree.SelectedNode.Remove();
            DBTree.Refresh();
        }//删除节点

        #endregion

        #region webBrowser Edit
        private void BoldButton1_Click(object sender, EventArgs e)
        {
            WebBrowser a=(WebBrowser)TabControl.SelectedTab.Controls[0];
            a.Document.ExecCommand("Bold", false, null);
            a.Focus();
        }

        private void ItalicButton_Click(object sender, EventArgs e)
        {
            WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
            a.Document.ExecCommand("Italic", false, null);
            a.Focus();
        }

        private void UnderLineButton_Click(object sender, EventArgs e)
        {
            WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
            a.Document.ExecCommand("UnderLine", false, null);
            a.Focus();
        }

        private void JustifyCentreButton_Click(object sender, EventArgs e)
        {
            WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
            a.Document.ExecCommand("JustifyCenter", false, null);
            a.Focus();
        }

        private void JustifyLeftButton_Click(object sender, EventArgs e)
        {
            WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
            a.Document.ExecCommand("JustifyLeft", false, null);
            a.Focus();
        }

        private void JustifyRightButton_Click(object sender, EventArgs e)
        {
            WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
            a.Document.ExecCommand("JustifyRight", false, null);
            a.Focus();

        }

        private void WordColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.FullOpen = false;
            colorDialog.ShowHelp = false;
            colorDialog.Color = Color.Black;//初始化当前文本框中的字体颜色，当用户在ColorDialog对话框中点击"取消"按钮
            colorDialog.ShowDialog();

            string colorstr =
                    string.Format("#{0:X2}{1:X2}{2:X2}", colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
            WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
            a.Document.ExecCommand("ForeColor", false, colorstr);
            


        }

        private void InsertPictureButton_Click(object sender, EventArgs e)
        {
            WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
            a.Document.ExecCommand("InsertImage", false, null);
            a.Focus();
        }


        private void FontComboBox_TextChanged(object sender, EventArgs e)
        {
            if (updatingFontName) return;
            FontFamily ff;
            try
            {
                ff = new FontFamily(FontComboBox.Text);
            }
            catch (Exception)
            {
                updatingFontName = true;
                FontComboBox.Text = FontName.GetName(0);
                updatingFontName = false;
                return;
            }
            FontName = ff;

        }

        private void SizeComboBox_TextChanged(object sender, EventArgs e)
        {
            float fontSize = float.Parse(SizeComboBox.SelectedItem.ToString());
            if (SizeComboBox.SelectedItem != null)
            {
                string font = (string)FontComboBox.SelectedItem;
                WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
                a.Document.ExecCommand("FontSize", false, fontSize.ToString());
            }
        }

        private void mainBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            CreateTabPage();
            //string name = TabControl.TabPages[TabControl.TabPages.Count - 2].Controls[0].Name;
            WebBrowser b = (WebBrowser)TabControl.TabPages[TabControl.TabPages.Count - 1].Controls[0];
            WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
            e.Cancel = true;
            try
            {
                string url = a.Document.ActiveElement.GetAttribute("href");

                b.Navigate(url);

                string strHTML = "";
                WebClient myWebClient = new WebClient();
                Stream myStream = myWebClient.OpenRead(url);
                StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
                strHTML = sr.ReadToEnd();
                myStream.Close();

                Int32 i1 = strHTML.IndexOf("<title>") + 7;
                Int32 i2 = strHTML.IndexOf("</title>") - 1;
                string strTitle = strHTML.Substring(i1, i2 - i1);
                if (strTitle.Length > 10)
                {
                    strTitle = strTitle.Substring(0, 10);

                }
                TabControl.TabPages[TabControl.TabPages.Count - 1].Text = strTitle;


            }
            catch
            {
            }
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            Browser();


        }//网页浏览


        #endregion

        #region edit menu

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowser a=(WebBrowser)TabControl.SelectedTab.Controls[0];
            a.Document.ExecCommand("Undo", false, null);
        }

        private void 重复ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
            a.Document.ExecCommand("Redo", false, null);
        }

        #endregion

        #region tabControl

        private void addTabButton_Click(object sender, EventArgs e)
        {
            CreateTabPage();
        }

        private void CreateTabPage()
        {
            TabPage newPage = new TabPage();
            newPage.Text = "新选项卡1";
            WebBrowser newWebBrowser = new WebBrowser();
            newWebBrowser.DocumentText = string.Empty;
            newWebBrowser.Document.ExecCommand("EditMode", false, null);
            newWebBrowser.Document.ExecCommand("LiveResize", false, null);
            newWebBrowser.Location = newPage.Location;
            newWebBrowser.Width = TabControl.Width - 15;
            newWebBrowser.Height = TabControl.Height;
            newPage.Controls.Add(newWebBrowser);
            TabControl.TabPages.Add(newPage);
        }

        private void TabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)//判断你点的是不是中建
            {
                Point ClickPoint = new Point(e.X, e.Y);
                for (int i = 0; i < TabControl.TabPages.Count; i++)
                {
                    Rectangle a = TabControl.GetTabRect(i);
                    if (ClickPoint.X > a.X && ClickPoint.X < a.X + a.Width)
                    {
                        if (ClickPoint.Y > a.Y && ClickPoint.Y < a.Y + a.Height)
                        {
                            TabControl.SelectedIndex = i;
                        }

                    }
                }
                TabControl.TabPages.RemoveAt(TabControl.SelectedIndex);
            }
        }

        private void RemoveTabButton_Click(object sender, EventArgs e)
        {
            TabControl.TabPages.Remove(TabControl.SelectedTab);
        }//中键关闭选项卡

        #endregion


        #region note

        private void CreateNewDataBase(string name)
        {
            String path = Application.StartupPath;
            StreamWriter sw = new StreamWriter(path+@"\\DB\\" + name + ".txt");
            sw.WriteLine("0 " + name + " -1 D");
            sw.Close();
            //TreeView newTree = new TreeView();

            DataBase rootDB = new DataBase();
            rootDB.name = name;
            rootDB.type = "D";
            rootDB.address = @"F:\"+name;
            rootDB.father = -1;
            rootDB.time = System.DateTime.Now.ToString();

            TreeNode root = new TreeNode();
            root.Text = name;
            root.Tag = rootDB;
            root.ImageIndex = 0;

            DBTree.Nodes.Add(root);

            Directory.CreateDirectory(rootDB.address); 
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];

            FileStream myFs = new FileStream(currentTextName, FileMode.Create);
            StreamWriter mySw = new StreamWriter(myFs);
            //currentTextName = newNodeDB.address;
            mySw.Write(a.DocumentText);
            mySw.Close();
            myFs.Close();
        }

        #endregion

        

        

        
        #region webSiteTree

        private void CreateFavoriteMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewForm();
            CreateNewFavorite(newName);
        }

        private void ImportMenuItem_Click(object sender, EventArgs e)
        {
            string fileName;
            openFileDialog1.Filter = "文本文件|*.txt|所有文件|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                ImportWebSiteTree(fileName);
                
            }
            MenuTabControl.SelectedIndex = 1;
            
        }

        private void CreateNewFavorite(string name)
        {
            String path = Application.StartupPath;
            StreamWriter sw = new StreamWriter(path+@"\\Fav\\" + name + ".txt");
            sw.WriteLine("0 " + name + " -1 D");
            sw.Close();
            //TreeView newTree = new TreeView();

            DataBase rootDB = new DataBase();
            rootDB.name = name;
            rootDB.type = "D";
            rootDB.address = path + @"\\Fav\\" + name + ".txt";
            rootDB.father = -1;
            rootDB.time = System.DateTime.Now.ToString();

            TreeNode root = new TreeNode();
            root.Text = name;
            root.Tag = rootDB;
            root.ImageIndex = 0;

            webSiteTree.Nodes.Add(root);

            //Directory.CreateDirectory(rootDB.address);

            MenuTabControl.SelectedIndex = 1;


        }



        private void webSiteContextMenu_Opening(object sender, CancelEventArgs e)
        {
            DataBase tnDB = new DataBase();
            tnDB=(DataBase)webSiteTree.SelectedNode.Tag;
            if (tnDB.type == "website")
            {
                webSiteContextMenu.Items[0].Visible = false;
                webSiteContextMenu.Items[1].Visible = false;
                webSiteContextMenu.Items[2].Visible = false;
            }
            else
            {
                webSiteContextMenu.Items[0].Visible = true;
                webSiteContextMenu.Items[1].Visible = true;
                webSiteContextMenu.Items[2].Visible = true;
 
            }
        }

        private void webSiteTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = webSiteTree.SelectedNode;
            DataBase tnDB = new DataBase();
            tnDB = (DataBase)tn.Tag;
            if (tnDB.type == "website")
            {
                CreateTabPage();
                TabControl.SelectedIndex = TabControl.TabCount - 1;
                WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];

                string url = tnDB.address;
                string strHTML = "";
                WebClient myWebClient = new WebClient();
                Stream myStream = myWebClient.OpenRead(url);
                StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
                strHTML = sr.ReadToEnd();
                myStream.Close();

                Int32 j1 = strHTML.IndexOf("charset=");

                string strFormat = strHTML.Substring(j1, 16);

                Int32 i1 = strHTML.IndexOf("<title>") + 7;
                Int32 i2 = strHTML.IndexOf("</title>") - 1;
                string strTitle = strHTML.Substring(i1, i2 - i1);

                if (strTitle.Length > 10)
                {
                    strTitle = strTitle.Substring(0, 10);

                }
                TabControl.SelectedTab.Text = strTitle;
                a.Navigate(tnDB.address);


            }
        }

        private void OutportWebSiteTree()
        {
            string name;
            List<TreeNode> treeNodesList = new List<TreeNode>();
            for (int i = 0; i < webSiteTree.Nodes.Count; i++)
            {
                DataBase root = (DataBase)webSiteTree.Nodes[i].Tag;
                root.father = -1;
                treeNodesList.Add(webSiteTree.Nodes[i]);
            }
                name = webSiteTree.Nodes[0].Text;

            StreamWriter sw = new StreamWriter(@"D:\inspiration\task4\MyBlog\MyBlog\Fav\default.txt");

            for (int i = 0; i < treeNodesList.Count; i++)
            {
                for (int j = 0; j < treeNodesList[i].Nodes.Count; j++)
                {
                    DataBase tnDB1 = (DataBase)treeNodesList[i].Nodes[j].Tag;
                    tnDB1.father = i;
                    treeNodesList.Add(treeNodesList[i].Nodes[j]);
                }

            }

            for (int i = 0; i < treeNodesList.Count; i++)
            {
                DataBase tnDB = (DataBase)treeNodesList[i].Tag;
                sw.WriteLine(i + " " + treeNodesList[i].Text + " " + tnDB.father + " " + tnDB.type+" "+tnDB.address);

            }
            sw.Close();
 
        
        }

        private void ImportDefaultWebSiteTree()
        {
            
            if (File.Exists(@"D:\inspiration\task4\MyBlog\MyBlog\Fav\default.txt") == true)
            {
                string fileName = null;
                fileName=@"D:\inspiration\task4\MyBlog\MyBlog\Fav\default.txt";
                ImportWebSiteTree(fileName);
 
            }


        }

        private void ImportWebSiteTree(string name)
        {
            string strLine;
            string[] readArray;
            List<TreeNode> webSiteTreeNodes = new List<TreeNode>();

            try
            {
                FileStream aFile = new FileStream(name, FileMode.Open);
                StreamReader sr = new StreamReader(aFile);

                strLine = sr.ReadLine();
                readArray = strLine.Split(' ');

                DataBase rootDB = new DataBase();
                rootDB.name = readArray[1];
                rootDB.type = "D";
                rootDB.address = readArray[4];

                TreeNode root = new TreeNode();
                root.Tag = rootDB;
                root.Text = rootDB.name;

                webSiteTreeNodes.Add(root);

                strLine = sr.ReadLine();

                while (strLine != null&&strLine!="")
                {
                    int father = 0;
                    readArray = strLine.Split(' ');

                    DataBase childDB = new DataBase();
                    childDB.name = readArray[1];
                    childDB.father = int.Parse(readArray[2]);
                    childDB.type = readArray[3];
                    childDB.address = readArray[4];

                    father = childDB.father;

                    TreeNode tn = new TreeNode();
                    tn.Text = childDB.name;
                    switch (childDB.type)
                    {
                        case "D": tn.ImageIndex = 0; tn.SelectedImageIndex = 0; tn.Tag = childDB; break;
                        case "website": tn.ImageIndex = 1; tn.SelectedImageIndex = 1; tn.Tag = childDB; break;

                    }

                    webSiteTreeNodes.Add(tn);
                    if (father == -1)
                    {
                        webSiteTree.Nodes.Add(tn);
                    }
                    else
                    {
                        webSiteTreeNodes[father].Nodes.Add(tn);
                    }
                    strLine = sr.ReadLine();


                }
                sr.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("An IOException has been thrown!");
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
                return;
            }

            webSiteTree.Nodes.Add(webSiteTreeNodes[0]);
 
        }

        private void 添加网址ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                添加网址ToolStripMenuItem_Click();
            }

        }

        private void 添加网址ToolStripMenuItem_Click()
        {
            CreateNewWebSiteForm();
            CreateNewWebSite();
        }

        private void CreateNewWebSiteForm()
        {
            Form form1 = new Form();
            form1.Text = "新建对话框";
            form1.Height = 200;

            Label title = new Label();

            title.Text = "新建的名称为：";
            title.Location = new Point(form1.Width / 10, form1.Height / 5);

            TextBox newName = new TextBox();
            newName.Location = new Point(form1.Width / 10 + title.Width, form1.Height / 5 - 5);
            newName.Width = 100;

            Label title1 = new Label();

            title1.Text = "新建的url为：";
            title1.Location = new Point(title.Location.X, title.Location.Y + 30);

            TextBox newAddress = new TextBox();
            newAddress.Location = new Point(newName.Location.X, newName.Location.Y + 30);
            newAddress.Width = 100;

            Button okButton1 = new Button();
            okButton1.Text = "确定";
            okButton1.Location = new Point(title1.Location.X, title1.Location.Y + 30);
            okButton1.Click += new EventHandler(okButton1_Click);

            Button cancelButton = new Button();
            cancelButton.Text = "取消";
            cancelButton.Location = new Point(title1.Location.X + okButton1.Width + 50, title1.Location.Y + 30);
            cancelButton.Click += new EventHandler(cancelButton_Click);

            form1.Controls.Add(title);
            form1.Controls.Add(title1);
            form1.Controls.Add(newName);
            form1.Controls.Add(newAddress);
            form1.Controls.Add(okButton1);
            form1.Controls.Add(cancelButton);
            form1.ShowDialog();

        }

        void okButton1_Click(object sender, EventArgs e)
        {
            Form a = ActiveForm;
            newName = a.Controls[2].Text;
            newAddress = a.Controls[3].Text;
            if (a.Controls[1].Text != null)
            {
                a.Close();
            }
        }

        private void CreateNewWebSite()
        {
            DataBase newNodeDB = new DataBase();
            newNodeDB.name = newName;
            newNodeDB.type = "website";
            newNodeDB.time = System.DateTime.Now.ToString();

            DataBase fatherNodeDB = new DataBase();
            fatherNodeDB = (DataBase)webSiteTree.SelectedNode.Tag;
            newNodeDB.address = newAddress;


            TreeNode newNode = new TreeNode();
            newNode.Text = newName;
            newNode.ImageIndex = 1;
            newNode.SelectedImageIndex = 1;
            newNode.Tag = newNodeDB;
            webSiteTree.SelectedNode.Nodes.Add(newNode);

            webSiteTree.SelectedNode.Expand();
        
        }

        private void webSiteTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = webSiteTree.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    webSiteTree.SelectedNode = CurrentNode;//选中这个节点
                }
            }
        }

        private void CreateDirectryMenueItem_Click()
        {


            //点鼠标右键,return   

            CreateNewForm();

            DataBase newNodeDB = new DataBase();
            newNodeDB.name = newName;
            newNodeDB.type = "D";
            newNodeDB.time = System.DateTime.Now.ToString();

            DataBase fatherNodeDB = new DataBase();
            fatherNodeDB = (DataBase)webSiteTree.SelectedNode.Tag;
            newNodeDB.address = fatherNodeDB.address + "\\" + newName;

            TreeNode newNode = new TreeNode();
            newNode.Text = newName;
            newNode.ImageIndex = 0;
            newNode.SelectedImageIndex = 0;
            newNode.Tag = newNodeDB;
            webSiteTree.SelectedNode.Nodes.Add(newNode);
            webSiteTree.SelectedNode.Expand();

            Directory.CreateDirectory(newNodeDB.address);

        }

        private void CreateDirectryMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CreateDirectryMenueItem_Click();
            }

        }

        private void 删除网址ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                删除网址ToolStripMenuItem_Click();
            }

        }

        private void 删除网址ToolStripMenuItem_Click()
        {
            webSiteTree.SelectedNode.Remove();
        }

        #endregion





        private void CaptureButton_Click(object sender, EventArgs e)
        {
            if (HideCurrentWindow.Checked == true)
            {
                this.Hide();
                Thread.Sleep(300);
            }
            MyCaptureForm cf = new MyCaptureForm();
            cf.ShowDialog();
            if (cf.Image != null)
            {
                string filepath;
                string filename;
                //显示图片
                WebBrowser a = (WebBrowser)TabControl.TabPages[1].Controls[0];
                filepath = cf.address;
                filename = Path.GetFileName(cf.name);

                HtmlElement el = a.Document.CreateElement("DIV");
                el.InnerHtml = "<img src='" + filepath + "'></img>";

                a.Document.Body.AppendChild(el);
                
            }
            this.Show();
        }

        private void HideCurrentWindow_Click()
        {
            

            if (HideCurrentWindow.Checked)
            {
                HideCurrentWindow.Checked = false;
            }
            else
            {
                HideCurrentWindow.Checked = true;
 
            }
        }

        private void HideCurrentWindow_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                HideCurrentWindow_Click();
            }
        }

        private void MyBlog_KeyPress(object sender, KeyPressEventArgs e)
        {

        }



        private void Browser()
        {
            string url = "http://" + textBox1.Text;
            WebBrowser a = (WebBrowser)TabControl.SelectedTab.Controls[0];
            a.Navigate(url);

            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
            strHTML = sr.ReadToEnd();
            myStream.Close();

            Int32 i1 = strHTML.IndexOf("<title>") + 7;
            Int32 i2 = strHTML.IndexOf("</title>") - 1;
            string strTitle = strHTML.Substring(i1, i2 - i1);

            if (strTitle.Length > 10)
            {
                strTitle = strTitle.Substring(0, 10);

            }
            TabControl.SelectedTab.Text = strTitle;
            int c = strTitle.Length;
 
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Browser();
            }
        }



        



        

       


       

        

        

       





















    }
}
