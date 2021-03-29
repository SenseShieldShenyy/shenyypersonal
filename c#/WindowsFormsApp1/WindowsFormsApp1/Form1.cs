using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form : System.Windows.Forms.Form
    {
        string[,] ninja = new string[2, 3] { { "漩涡鸣人", "Naruto.jpg", "木叶富二代" }, { "春野樱", "sakura.jpg", "追星女人" } };
        public Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //listBox1.Items.Clear();
            for (int i = 0; i < 2; i++)
            {
                listBox1.Items.Add(ninja[i, 0]);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                if (listBox1.Text == ninja[i,0])
                {
                    pictureBox1.Image = Image.FromFile(@"D:\code\c#\WindowsFormsApp1\WindowsFormsApp1\"+ninja[i,1]);//@原来的意思
                    textBox1.Text = ninja[i,2];
                    break;
                    
                }
            }
        }
    }
}
