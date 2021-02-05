using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mpGsDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Panel> l = new List<Panel> { panel1, panel2 , panel3, panel4};
            lp = l; 
        }
        List<Panel> lp = new List<Panel>();
        int picex = 0; 
        private void button1_Click(object sender, EventArgs e)
        {
            picex = 1;
            timer1.Stop();
            timer1.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            picex = -1;
            timer1.Stop();
            timer1.Start(); 


        }
        int tick = 0 ; 
        private void timer1_Tick(object sender, EventArgs e)
        {
            tick++;
            foreach (Panel p in lp)
            {
                p.BackColor = Color.Beige;
                label1.Text = p.Name;
                label2.Text = tick.ToString(); 
                p.Location = new Point(p.Location.X + picex, p.Location.Y); 
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
