using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FibonacciGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List <Label> labels = new List <Label>();
        int a = 30, b = 30;
        int cnt = 0;
        int plus = 0 , minus = 0;
        int pr = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Visible = false;
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            int x = 30, y = 30;
            Random rnd = new Random();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Label celula = new Label();
                    celula.Width = 40;
                    celula.Height = 40;
                    celula.Text = rnd.Next(1,16).ToString();
                    celula.BorderStyle = BorderStyle.FixedSingle;
                    celula.TextAlign = ContentAlignment.MiddleCenter;
                    celula.Font = new Font("Arial" , 12, FontStyle.Bold);
                    celula.Location = new Point(x, y);
                    this.Controls.Add(celula);
                    labels.Add(celula);
                    celula.BackColor = Color.Transparent;
                    celula.Parent = this;
                    celula.Tag = "" + pr;
                    pr++; 
                    x += 40;
                }
                x = 30;
                y+= 40;
            }
            labels[0].Text = "";
            this.Invalidate();
            pr = 0;
        }
        int fibonacci(int x)
        {
            if(x == 1 || x == 2 || x == 3 || x == 5 || x == 8 || x == 13)
                 return 1;
            return 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            foreach (var control in labels)
            {
                control.Text = rnd.Next(1,16).ToString() ;
                control.BackColor = Color.Transparent;
                
            }
            labels[0].Text = "";
            a = 30;
            b = 30;
            cnt = 0;
            minus = 0;
            plus = 0;
            label1.Text = "Scor: " + cnt;
            pr = 0;
            this.KeyPreview = true;
            this.Invalidate();
            chart1.Series[0].Points.Clear();
            chart1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red);
            Brush brush = new SolidBrush(Color.Red);
            g.DrawEllipse(pen, 33, 33, 30, 30);
            g.FillEllipse(brush, 33, 33, 30, 30);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.W)
                {
                if (b - 40 >= 30)
                    b -= 40;
                else
                    return;
            }
            else if (e.KeyCode == Keys.S)
            {
                if (b + 40 <= 390)
                        b += 40;
                else
                    return;
            }
            else if (e.KeyCode == Keys.D)
            {
                if (a + 40 <= 390)
                        a += 40;
                else
                    return;
            }
            else if (e.KeyCode == Keys.A)
            {
                if (a - 40 >= 30)
                        a -= 40;
                else
                    return;
            }
            labels[pr].BackColor = Color.Gray;
            labels[pr].Text = "";
            foreach (var c in labels)
            {
                if(c.Location == new Point(a,b))
                {
                    int nr;
                    if (c.Text != "")
                    {
                        nr = Convert.ToInt32(c.Text);
                    }
                    else
                        nr = 0;
                    if (fibonacci(nr) == 1)
                    {
                        cnt += nr;
                        plus++;
                    }
                    else
                    {
                        if (nr != 0) { 
                        cnt -= nr;
                        minus++;
                        }
                    }
                    label1.Text = "Scor: " + cnt;
                    c.BackColor = Color.Red;
                    pr = Convert.ToInt32(c.Tag);
                    c.Text = "";
                }
                    
            }
            if (labels[99].Location == new Point(a, b))
            {
                MessageBox.Show("Felicitari ati ajuns la final!");
                this.KeyPreview = false;
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = Math.Max(minus, plus);
                chart1.Series[0].Points.AddXY("Fibonacci", plus);
                chart1.Series[0].Points.AddXY("Non-Fibonacci", minus);
                chart1.Series[0].BorderWidth = 10;
                chart1.Visible = true;
            }
        }
    }
}
