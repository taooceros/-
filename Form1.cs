using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 挂机点名器
{
    public partial class Form1 : Form
    {
        Thread thread = new Thread(Extra.Start);

        public Form1()
        {
            InitializeComponent();
            thread.Start();
            thread.IsBackground = true;
            label1.Text = DateTime.Now.Minute.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Extra.run = !Extra.run;
            if (Extra.run)
                status.Text = "已开始";
            else 
                status.Text = "未开始";
            
        }
    }
}
