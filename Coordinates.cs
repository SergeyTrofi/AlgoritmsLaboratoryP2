using LaboratoryP2.Properties;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryP2
{
    internal class Coordinates
    {
        public class Tools
        {
            public static List<DataPoint> Export(int count)
            {
                RecHanoiTowers towers = new RecHanoiTowers();

                List<double> results = new List<double>();
               
                for (int j = 0; j <= count; j++)
                {
                    results.Add(
                    Timer(j, towers));
                }

                var res = new List<DataPoint> { };
                for (int i = 0; i <= count; i++)
                {
                    res.Add(new DataPoint(i, results[i]));
                }

                return res;
            }


            // Таймер
            public static double Timer(int count, RecHanoiTowers towers)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Reset();
                stopwatch.Start();
                towers.SolveHanoi(count, 0, 2, 1, false);
                stopwatch.Stop();

                return stopwatch.ElapsedTicks;
            }
        }

    }
}
