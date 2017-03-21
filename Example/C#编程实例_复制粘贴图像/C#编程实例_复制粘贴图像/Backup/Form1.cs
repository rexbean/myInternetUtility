using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Pic
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(16, 176);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(72, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "���ͼ��";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(232, 176);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(80, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "�رճ���";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "Image files (JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif; *.tiff; *.png|" +
				" JPeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg |GIF files (*.gif)|*.gif |BMP files (*.b" +
				"mp)|*.bmp|Tiff files (*.tif;*.tiff)|*.tif;*.tiff|Png files (*.png)| *.png |All f" +
				"iles (*.*)|*.*";
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Location = new System.Drawing.Point(8, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(312, 160);
			this.panel1.TabIndex = 3;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, -55);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(600, 600);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(88, 176);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(72, 23);
			this.button3.TabIndex = 4;
			this.button3.Text = "����ͼ��";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(160, 176);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(72, 23);
			this.button4.TabIndex = 5;
			this.button4.Text = "ճ��ͼ��";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(328, 206);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "��ʾͼ����ճ��";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button2_Click(object sender, System.EventArgs e)
		{//�رճ���
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{//���ͼ��
			if(this.openFileDialog1.ShowDialog()==DialogResult.OK)
			{
				this.pictureBox1.Image=System.Drawing.Bitmap.FromFile(this.openFileDialog1.FileName);			
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{//����ͼ��
			if(this.pictureBox1.Image.Size.Height<1)
			{
				MessageBox.Show("��װ��ͼ�����ִ�б�������","��Ϣ��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
			Clipboard.SetDataObject(this.pictureBox1.Image.Clone());
			MessageBox.Show("����ͼ������ɹ���","��Ϣ��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);			
		}
		private void button4_Click(object sender, System.EventArgs e)
		{//ճ��ͼ��
			try
			{
				IDataObject MyData = Clipboard.GetDataObject ( ) ;
				//�жϼ��а��������ǲ���λͼ
				if ( MyData.GetDataPresent ( DataFormats.Bitmap ) ) 
				{
					//���λͼ����
					Bitmap MyBitmap = ( Bitmap ) MyData.GetData ( DataFormats.Bitmap ) ;
				    //��ʾͼƬ 
					this.pictureBox1.Image=MyBitmap;
				}
				else
				{//�����������û��ͼ���ļ����򷢳�����
					MessageBox.Show("û�п���ʾ��ͼ��","��Ϣ��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				}
			}
			catch(Exception Err)
			{//��ȡ�����������		
				MessageBox.Show("������Ϣ�ǣ� "+Err.Message,"��Ϣ��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}		
		}
		
	}
}
