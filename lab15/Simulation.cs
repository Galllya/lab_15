using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab15
{
    public class Simulation
    {
        const int n = 3;
        public Random rnd = new Random();
        public double time = 0, temp;
        public double[] p = new double[n] { 0.33333, 0.33333, 0.33333 };
        public double[,] m = new double[n, n] { { -0.2, 0.6, 0.2 }, { 0.1, -0.3, 0.6 }, { 0.3, 0.5, -0.2 } };
        public int state, i, k = 0;
        public double a;
        public int numer = 0;
        public string[] wether = new string[3] { "Ясно", "Облачно", "Пасмурно" };
        public double chi = 0;
        public Dictionary<double, double> weatherPose = new Dictionary<double, double>();

        public void Start()
        {

            if (time == 0)
            {
                a = rnd.NextDouble();
                i = 0;
                while (a > 0)
                {
                    a -= p[i];
                    i++;
                }
                weatherPose[state]++;
                state = i;
                k++;
            }
        }

        public void Add()
        {
            weatherPose.Add(0, 1);
            weatherPose.Add(1, 1);
            weatherPose.Add(2, 1);
            weatherPose.Add(3, 1);
        }

        public void Stop()
        {
            int[] maP = new int[3];
            int[] mF = new int[3];
            for (int i = 1; i < weatherPose.Count; i++)
            {
                maP[i - 1] = (int)(p[i - 1] * k);
                mF[i - 1] = (int)(weatherPose[i] * k);
            }

            for (int i = 1; i < weatherPose.Count; i++)
            {
                chi += Math.Pow((mF[i - 1] - maP[i - 1]), 2) / maP[i - 1];
            }
        }

        public void MainM()
        {
            a = rnd.NextDouble();
            temp = Math.Log(a) / m[state - 1, state - 1];
            time += temp;

            double[] prob = new double[n];

            for (i = 0; i < n; i++)
            {
                if (i == (state - 1))
                {
                    prob[i] = 0;
                }
                else
                {
                    prob[i] = Math.Abs(m[state - 1, i] / m[state - 1, state - 1]);
                }
            }

            i = 0;
            while (a > 0)
            {
                a -= prob[i];
                i++;
            }
            state = i;
        }
    }
}
