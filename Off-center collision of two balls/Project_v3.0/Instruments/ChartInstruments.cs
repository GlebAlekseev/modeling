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

                Series s1 = getSeries(MainWindow.Chart_1, 1);
                Series s2 = getSeries(MainWindow.Chart_1, 2);
                Series s3 = getSeries(MainWindow.Chart_2, 1);


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

                    Vector Impulse1 = (data.BallFirst.Speed * data.BallFirst.Mass);
                    Vector Impulse2 = (data.BallSecond.Speed * data.BallSecond.Mass);

                    s1.ChartType = SeriesChartType.Line;
                    s1.Color = data.BallFirst.Color;
                    s1.Points.AddXY(data.Dt, Impulse1.Module);

                    s2.ChartType = SeriesChartType.Line;
                    s2.Color = data.BallSecond.Color;
                    s2.Points.AddXY(data.Dt, Impulse2.Module);


                    s3.ChartType = SeriesChartType.Line;
                    s3.Color = data.BallFirst.Color;
                    s3.Points.AddXY(data.Dt, (Impulse1 + Impulse2).Module);


                }

            }));
        }

    }
}
