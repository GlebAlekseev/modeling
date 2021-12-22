using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Threading;

namespace Project_v3._0
{
    public class ChartInstruments
    {
        public static void setChart(string AxisX, string AxisY, Chart chart, string chartName)
        {
            chart.ChartAreas.Add(new ChartArea(chartName));
            chart.ChartAreas[0].AxisY.Title = AxisX;
            chart.ChartAreas[0].AxisX.Title = AxisY;
        }
        private static Series getSeries(Chart chart, int number)
        {
            return chart.Series.Add(number.ToString());
        }


        public static void UpdateDataChart(List<Data> dataList)
        {

            Data.grid.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                MainWindow.Chart_1.Series.Clear();
                MainWindow.Chart_2.Series.Clear();
                MainWindow.Chart_3.Series.Clear();
                MainWindow.Chart_4.Series.Clear();
                Series s1_1 = getSeries(MainWindow.Chart_1, 1);
                Series s1_2 = getSeries(MainWindow.Chart_1, 2);

                Series s2_1 = getSeries(MainWindow.Chart_2, 3);
                Series s2_2 = getSeries(MainWindow.Chart_2, 4);

                Series s3_1 = getSeries(MainWindow.Chart_3, 5);
                Series s3_2 = getSeries(MainWindow.Chart_3, 6);

                Series s4_1 = getSeries(MainWindow.Chart_4, 7);
                Series s4_2 = getSeries(MainWindow.Chart_4, 8);


                Data[] tmpDataList = new Data[dataList.Count];
                dataList.CopyTo(tmpDataList);

                List<Data> newDataList = new List<Data>();
                foreach (var data in dataList)
                {
                    newDataList.Add(data);
                }
                switch (MainWindow.chartPointsBarier)
                {
                    case CHART_POINT_BARIER.TRUE:
                        for (int i = 0; i < newDataList.Count; i++)
                        {
                            if (newDataList.Count > StaticResources.ChartPointsMax)
                            {
                                newDataList.RemoveAt(0);
                            }
                            else { break;}
                        }
                        break;
                    case CHART_POINT_BARIER.FALSE:
                        break;
                }

                foreach (Data data in newDataList)
                {
                    double Dt = Math.Round(data.Dt, 3);
                    Vector Speed1 = data.BallFirst.Speed;
                    Vector Speed2 = data.BallSecond.Speed;

                    Vector Impulse1 = (data.BallFirst.Speed * data.BallFirst.Mass);
                    Vector Impulse2 = (data.BallSecond.Speed * data.BallSecond.Mass);

                    s1_1.ChartType = SeriesChartType.Line;
                    s1_1.Color = data.BallFirst.Color;
                    s1_1.Points.AddXY(Dt, Impulse1.Module);

                    s1_2.ChartType = SeriesChartType.Line;
                    s1_2.Color = data.BallSecond.Color;
                    s1_2.Points.AddXY(Dt, Impulse2.Module);

                             
                    s2_2.ChartType = SeriesChartType.Line;
                    s2_2.Color = data.BallSecond.Color;
                    s2_2.Points.AddXY(Dt, ((Impulse1 + Impulse2).Module));


                    s3_1.ChartType = SeriesChartType.Line;
                    s3_1.Color = data.BallFirst.Color;
                    s3_1.Points.AddXY(Dt, Speed1.Module);

                    s3_2.ChartType = SeriesChartType.Line;
                    s3_2.Color = data.BallSecond.Color;
                    s3_2.Points.AddXY(Dt, Speed2.Module);


                    double E = data.BallFirst.Mass * Speed1.Module * Speed1.Module + data.BallSecond.Mass * Speed2.Module * Speed2.Module;


                    s4_1.ChartType = SeriesChartType.Line;
                    s4_1.Color = data.BallFirst.Color;
                    s4_1.Points.AddXY(Dt, E);


                }

            }));
        }

    }
}
