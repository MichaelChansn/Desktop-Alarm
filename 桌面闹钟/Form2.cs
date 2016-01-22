using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace 桌面闹钟
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(Alarm.mypoint.X+330,Alarm.mypoint.Y);
            this.timer1.Enabled = true;
            this.comboBox1.SelectedIndex = DateTime.Now.Hour;
            this.comboBox2.SelectedIndex = DateTime.Now.Minute;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "")
            {
                Alarm.isp = true;
                Alarm.isokay = true;
                Alarm.mydate = this.dateTimePicker1.Text;//timetmp;
                Alarm.myring = this.textBox1.Text;// ring;
                Alarm.myhour = this.comboBox1.Text;//sethour;
                Alarm.mymin = this.comboBox2.Text;// setminute;
                Alarm.isrd = true;
                Alarm.isud = true;
                this.Close();
                this.timer1.Enabled = false;
            }
            else
            {
                MessageBox.Show("请选择好Mp3铃声！！！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label2.Text = DateTime.Now.TimeOfDay.ToString().Substring(0,8);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
        }
        public static string  timetmp;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            timetmp = this.dateTimePicker1.Text;
            this.label5.Text = timetmp;
        }

        string ring;
        bool isok = false;
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ring=this.openFileDialog1.FileName;
            this.textBox1.Text = ring;
            isok = true;

        }

         public static uint SND_ASYNC = 0x0001; // play asynchronously 
          public static uint SND_FILENAME = 0x00020000; // name is file name
          [DllImport("winmm.dll")]
          public static extern int mciSendString(string m_strCmd, string m_strReceive, int m_v1, int m_v2);

          [DllImport("Kernel32", CharSet = CharSet.Auto)]
          static extern Int32 GetShortPathName(String path,StringBuilder shortPath, Int32 shortPathLength); 
              


        private void button4_Click(object sender, EventArgs e)
        {
            if (isok)
            {
                string name = ring;
                StringBuilder shortpath = new StringBuilder(80);
                int result = GetShortPathName(name, shortpath, shortpath.Capacity);
                name = shortpath.ToString();
                mciSendString(@"close all", null, 0, 0);

                if (this.button4.Text == "试听")
                {
                    this.button4.Text = "停止";
                    
                    mciSendString(@"open " + name + " alias song", null, 0, 0); //打开
                    mciSendString("play song", null, 0, 0); //播放
                }
                else
                {
                    this.button4.Text = "试听";
                    mciSendString(@"close all", null, 0, 0);
                }
            }
          
        }
        string sethour;
        string setminute;

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            sethour = this.comboBox1.Text;
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setminute = this.comboBox2.Text;
        }
    }
}
