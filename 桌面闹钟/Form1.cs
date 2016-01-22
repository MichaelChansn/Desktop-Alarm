using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace 桌面闹钟
{
    public partial class Alarm : Form
    {
        public Alarm()
        {
            InitializeComponent();
            this.ContextMenuStrip = contextMenuStrip1;
        }
        public static string mydate;
        public static string myring;
        public static string myhour;
        public static string mymin;
        public static bool isokay = false;
      
        private void Alarm_Load(object sender, EventArgs e)
        {
         /*   int h = DateTime.Now.Hour;
            int s = DateTime.Now.Second;
            int m = DateTime.Now.Minute;
            int y = DateTime.Now.Year;
            int mo = DateTime.Now.Month;
            int d = DateTime.Now.Day;

            MyDrawClock(h, m, s);
            toolStripStatusLabel1.Text = string.Format("{0}-{1}-{2}  {3}:{4}:{5}", y, mo, d, h, m, s);
            this.label1.Text = GetTime();
          * */
            mypoint = this.Location;
            
            timer1_Tick(null,null);
            
        }
        private void MyDrawClock(int h, int m, int s)
        {
            Graphics g = this.panel1.CreateGraphics();
            Rectangle rect = this.panel1.ClientRectangle;

            Bitmap bmp = new Bitmap(桌面闹钟.Properties.Resources._6411889_151918009397_2);
            ImageAttributes imageAttrib = new ImageAttributes();
            imageAttrib.SetWrapMode(WrapMode.TileFlipXY);
            g.DrawImage(bmp,this.panel1.ClientRectangle, 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, imageAttrib);
            imageAttrib.Dispose();


            //g.DrawImage(桌面闹钟.Properties.Resources._6411889_151918009397_2, 0, 0);
            Pen myPen = new Pen(Color.Blue, 1);
           // g.DrawEllipse(myPen, this.panel1.Location.X+10, this.panel1.Location.Y-45,this.panel1.Width-20,this.panel1.Height-20);//画表盘

            Point centerPoint = new Point(((this.panel1.ClientRectangle.Width-20) / 2)+10, ((this.panel1.ClientRectangle.Height-20) / 2)+10);//表的中心点
            Pen pen1 = new Pen(Color.Black, 1);
            Pen pen2 = new Pen(Color.Black,2);
            Font f = new Font("Arial", 8);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
/*
            for (int i = 1; i <= 60;i++ )
            {
                //Point sht1 = new Point((int)(centerPoint.X + (Math.Sin(i * Math.PI / 30) * ((this.panel1.Width - 20) / 2 - 10))), (int)(centerPoint.Y - (Math.Cos(i * Math.PI / 30) * ((this.panel1.Width - 20) / 2 - 10))));
                Point sht2 = new Point((int)(centerPoint.X + (Math.Sin(i * Math.PI / 30) * (this.panel1.Width - 20) / 2)), (int)(centerPoint.Y - (Math.Cos(i * Math.PI / 30) * (this.panel1.Width - 20) / 2)));
                if (i % 5 != 0)
                {
                    Point sht1 = new Point((int)(centerPoint.X + (Math.Sin(i * Math.PI / 30) * ((this.panel1.Width - 20) / 2 - 10))), (int)(centerPoint.Y - (Math.Cos(i * Math.PI / 30) * ((this.panel1.Width - 20) / 2 - 10))));
                   // Point sht2 = new Point((int)(centerPoint.X + (Math.Sin(i * Math.PI / 30) * (this.panel1.Width - 20) / 2)), (int)(centerPoint.Y - (Math.Cos(i * Math.PI / 30) * (this.panel1.Width - 20) / 2)));
                    g.DrawLine(pen1, sht1, sht2);
                }
                else
                {
                    Point sht1 = new Point((int)(centerPoint.X + (Math.Sin(i * Math.PI / 30) * ((this.panel1.Width - 20) / 2 - 15))), (int)(centerPoint.Y - (Math.Cos(i * Math.PI / 30) * ((this.panel1.Width - 20) / 2 - 15))));
                    //Point sht2 = new Point((int)(centerPoint.X + (Math.Sin(i * Math.PI / 30) * (this.panel1.Width - 20) / 2)), (int)(centerPoint.Y - (Math.Cos(i * Math.PI / 30) * (this.panel1.Width - 20) / 2)));
                    g.DrawLine(pen2, sht1, sht2);
                    sht1.X-=10;

                    g.DrawString((i / 5).ToString(),f,drawBrush,sht1);

                }

            }
 */
            //计算出秒针，时针，分针的另外一个商点
            Point secPoint = new Point((int)(centerPoint.X + (Math.Sin(s * Math.PI / 30) * 120)), (int)(centerPoint.Y - (Math.Cos(s * Math.PI / 30) * 120)));
            Point minPoint = new Point((int)(centerPoint.X + (Math.Sin(m * Math.PI / 30) * 100)), (int)(centerPoint.Y - (Math.Cos(m * Math.PI / 30) * 100)));
            Point hourPoint = new Point((int)(centerPoint.X + (Math.Sin(h * Math.PI / 6) * 80) - m * Math.PI / 360), (int)(centerPoint.Y - (Math.Cos(h * Math.PI / 6) * 80) - m * Math.PI / 360));

            //以不同颜色和宽度绘制表针
            myPen = new Pen(Color.Red, 1);
            g.DrawLine(myPen, centerPoint, secPoint);
            myPen = new Pen(Color.MidnightBlue, 3);
            g.DrawLine(myPen, centerPoint, minPoint);
            myPen = new Pen(Color.Black, 5);
            g.DrawLine(myPen, centerPoint, hourPoint);
        }
        public static Point mypoint = new Point();
        private void Alarm_Paint(object sender, PaintEventArgs e)
        {
            mypoint = this.Location;
        }



        public static uint SND_ASYNC = 0x0001; // play asynchronously 
        public static uint SND_FILENAME = 0x00020000; // name is file name
        [DllImport("winmm.dll")]
        public static extern int mciSendString(string m_strCmd, string m_strReceive, int m_v1, int m_v2);

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);

        public static bool isrd = false;
        public static bool isp=false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int h = DateTime.Now.Hour;
            int s = DateTime.Now.Second;
            int m = DateTime.Now.Minute;
            
            
            MyDrawClock(h, m, s);
            toolStripStatusLabel1.Text = "DATE:  " + DateTime.Now.ToString() + "  "+DateTime.Now.DayOfWeek.ToString();//DateTime.Now.ToString();//string.Format("现在是：{0}-{1}-{2}      {3}:{4}:{5}",y,mo,d, h, m, s);

            if (isokay)
            {
                this.label1.Text =mydate.Substring(0,4)+"/"+mydate.Substring(5,2)+"/"+mydate.Substring(8,2)+" "+ myhour + ":" + mymin + " ";
                if (isp)
                {
                    isp = false;
                    isal = true;
                    this.pictureBox1.BackgroundImage = 桌面闹钟.Properties.Resources._012;
                    this.toolTip1.SetToolTip(this.pictureBox1, "按下关闭闹铃");
                }
                if ((Convert.ToInt32(myhour) == h) && (Convert.ToInt32(mymin) == m) && (Convert.ToInt32(mydate.Substring(0, 4)) == DateTime.Now.Year) && (Convert.ToInt32(mydate.Substring(5, 2)) == DateTime.Now.Month) && (Convert.ToInt32(mydate.Substring(8, 2)) == DateTime.Now.Day))
                {
                    if(isrd)
                    {
                    isrd = false;
                    string name = myring;
                    StringBuilder shortpath = new StringBuilder(80);
                    int result = GetShortPathName(name, shortpath, shortpath.Capacity);
                    name = shortpath.ToString();
                    mciSendString(@"close all", null, 0, 0);
                    mciSendString(@"open " + name + " alias song", null, 0, 0); //打开
                    mciSendString("play song", null, 0, 0); //播放
                    this.timer2.Enabled = true;
                    this.timer1.Interval = 100;
                    }
                    
                }
                else
                {
                    
                    mciSendString(@"close all", null, 0, 0);
                    this.timer2.Enabled = false;
                    this.timer1.Interval = 1000;
                    if (this.Opacity < 0.3)
                        this.Opacity = 1;

                }
                
            }
            
            
            
            
            
            
            // this.label1.Text = GetTime();
        }
/*
        public string GetTime()
        {
            String TimeInString = "";
            int hour = DateTime.Now.Hour;
            int min = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;
            //将时，分，秒连成一个字符串
            TimeInString = (hour < 10) ? "0" + hour.ToString() : hour.ToString();
            TimeInString += ":" + ((min < 10) ? ("0" + min.ToString()) : min.ToString());
            TimeInString += ":" + ((sec < 10) ? ("0" + sec.ToString()) : sec.ToString());

            return TimeInString;
        }
        */
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 透明停靠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (this.透明停靠ToolStripMenuItem.Text == "透明停靠")
            {
                this.透明停靠ToolStripMenuItem.Text = "取消停靠";
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
               // this.BackgroundImage = null;
                //this.TransparencyKey = this.BackColor;
              //  this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
                
               // this.TransparencyKey = Color.White;
                
                this.Opacity = 0.3;
            }
            else
            {
                this.透明停靠ToolStripMenuItem.Text = "透明停靠";
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
               // this.BackgroundImage = 桌面闹钟.Properties.Resources.backg;
                //this.TransparencyKey = this.BackColor;
               // this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
               // this.TransparencyKey = System.Drawing.SystemColors.Control;// Color.White;
                this.Opacity = 1;
            }
        }

        private void 一直置顶ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.一直置顶ToolStripMenuItem.Text == "一直置顶")
            {
                this.一直置顶ToolStripMenuItem.Text = "取消置顶";
                this.TopMost = true;
            }
            else
            {
                this.一直置顶ToolStripMenuItem.Text = "一直置顶";
                this.TopMost = false;
            }
        }

        private void 设定闹钟ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (this.设定闹钟ToolStripMenuItem.Text == "设定闹钟")
            {
                this.设定闹钟ToolStripMenuItem.Text = "取消闹钟";
                Form2 setform = new Form2();

                setform.ShowDialog();
            }
            else
            {
                this.设定闹钟ToolStripMenuItem.Text="设定闹钟";
                isal = false;
                isud = false;
                isokay = false;
                this.pictureBox1.BackgroundImage = 桌面闹钟.Properties.Resources.alarm_dark;
                // this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.toolTip1.SetToolTip(this.pictureBox1, "按下打开闹铃");
               
                mciSendString(@"close all", null, 0, 0);
                this.timer2.Enabled = false;
                this.timer1.Interval = 1000;
                if (this.Opacity < 0.3)
                    this.Opacity = 1;
            }
         
       
            //setform.Location = new Point(this.Location.X + this.Width+10, this.Location.Y);
           
            //this.label1.Text = this.Location.X.ToString();
            
        }

        private void Alarm_LocationChanged(object sender, EventArgs e)
        {
            mypoint = this.Location;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (this.pictureBox1.Visible)
            {
                this.pictureBox1.BackColor = Color.YellowGreen;
              // this.pictureBox1.
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (this.pictureBox1.Visible)
            {
                this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            }
        }
        bool isal = false;
        public static bool isud = false;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
           if (isud)
            {
                if (isal)
                {
                    isal = false;
                    if(this.设定闹钟ToolStripMenuItem.Text=="取消闹钟")
                    {
                    this.设定闹钟ToolStripMenuItem.Text = "设定闹钟";
                    isud = false;
                    }
                    this.pictureBox1.BackgroundImage = 桌面闹钟.Properties.Resources.alarm_dark;
                   // this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    this.toolTip1.SetToolTip(this.pictureBox1, "按下打开闹铃");
                    isokay = false;
                    mciSendString(@"close all", null, 0, 0);
                    this.timer2.Enabled = false;
                    this.timer1.Interval = 1000;
                    if (this.Opacity < 0.3)
                        this.Opacity = 1;
                }
                else
                {
                    isal = true;
                    this.pictureBox1.BackgroundImage = 桌面闹钟.Properties.Resources._012;
                    // this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    this.toolTip1.SetToolTip(this.pictureBox1, "按下关闭闹铃");
                    isokay= true;
 
                }
            }
        }
        private int i = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Opacity = i;
            i++;
            if (i > 1)
            {
                i = 0;
            }
        }

        

       
    }
}
