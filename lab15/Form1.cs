using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab15
{
    public partial class Form1 : Form
    {
        int k = 0;
        int numer = 0;
        Simulation simulation = new Simulation();

        public Form1()
        {
            InitializeComponent();
            simulation.Add();
            chart2.Series[0].IsValueShownAsLabel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            simulation.Start();
            if (simulation.time == 0) chart1.Series[0].Points.AddXY(0, simulation.state);

            timer1.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int i = 1; i < simulation.weatherPose.Count; i++)
            {
                chart2.Series[0].Points.AddXY(i, simulation.weatherPose[i] / k);
            }

            simulation.Stop();

            if (simulation.chi < 5.991)
                label3.Text = " Распределения НЕ различаются";
            else
                label3.Text = " Распределения различаются";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            simulation.time = 0;
            label3.Text = "";
            numer = 0;
            textBox1.Text = "";
            label2.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Generate();
            double big_time;
            double t;
            if (numer == 0)
            {
                t = simulation.time;
            }
            else { t = simulation.time - 144 * numer; }
            big_time = t * 600;
            int H = (int)(big_time / (60 * 60));
            double Sec = big_time % (60 * 60);
            double M = (int)(Sec / 60);
            Sec = (int)(Sec % 60);

            textBox1.Text = textBox1.Text + numer + " день:  " + H + "ч   " + M + "м    " + Sec + "с.   " + " Погода: " + simulation.wether[simulation.state - 1] + Environment.NewLine;
            chart1.Series[0].Points.AddXY(simulation.time, simulation.state);
            label2.Text = simulation.wether[simulation.state - 1];
            simulation.weatherPose[simulation.state]++;
            k++;

            if (H >= 24)
            {
                numer++;
            }

        }

        public void Generate()
        {
            simulation.MainM();
        }
    }
}
