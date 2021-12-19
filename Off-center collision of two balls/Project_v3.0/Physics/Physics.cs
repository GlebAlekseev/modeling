using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_v3._0
{
    public class Physics
    {
        public static Vector GetNewSpeedV1(Vector v1, Vector v2, Vector i, Vector n, double m1, double m2)
        {
            return (i * v1) * i + ((m1 - m2) * (n * v1) * n + 2 * m2 * (n * v2) * n) / (m1 + m2);
        }
        public static Vector GetNewSpeedV2(Vector v1, Vector v2, Vector i, Vector n, double m1, double m2)
        {
            return (i * v2) * i + ((m2 - m1) * (n * v2) * n + 2 * m1 * (n * v1) * n) / (m1 + m2);
        }
        public static double Getdt1(Vector v21, Vector r21, double R1, double R2)
        {
            double z = (v21 * r21);
            if (z == 0)
            {
                return StaticResources.Dt;
            }
            return Math.Abs((r21.Module - R1 - R2) * r21.Module / z);
        }

        public static bool isOutside(Data data)
        {
            Point BorderDown = new Point(0,0);
            Point BorderUp = new Point(Data.Width,Data.Height);
            Point ball1 = data.BallFirst.Position;
            Point ball2 = data.BallSecond.Position;
            if ((ball1.x > BorderUp.x || ball1.x < BorderDown.x || ball1.y > BorderUp.y || ball1.y < BorderDown.y) &&
                (ball2.x > BorderUp.x || ball2.x < BorderDown.x || ball2.y > BorderUp.y || ball2.y < BorderDown.y))
                return true;
            return false;
        }

        public static bool isPointInBall(Point p, Point cBall, double radius)
        {
            try
            {
                return (Math.Pow((p.x - cBall.x), 2) + Math.Pow((p.y - cBall.y), 2) <= Math.Pow(radius, 2));
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public static ITERATION_CODE Iteration(List<Data> DataList)
        {
            Data LastData = DataList.Last<Data>();
            if (isOutside(LastData)) return ITERATION_CODE.END;


            int R1 = LastData.BallFirst.Radius;
            int R2 = LastData.BallSecond.Radius;
            double m1 = LastData.BallFirst.Mass;
            double m2 = LastData.BallSecond.Mass;
            Vector v1_ = LastData.BallFirst.Speed;
            Vector v2_ = LastData.BallSecond.Speed;
            Point c1 = LastData.BallFirst.Position;
            Point c2 = LastData.BallSecond.Position;
            Vector v21_ = v2_ - v1_;
            Vector r21 = new Vector(c1, c2);

            double v21_pr = Math.Abs((v21_ * r21) / r21.Module);

            if (r21 * v21_ < 0 && (v21_pr * StaticResources.Dt > (r21.Module - R1 - R2)))
            {
                double dt1 = Getdt1(v21_, r21, R1, R2);
                if (dt1 > StaticResources.Dt)
                    throw new Exception("Проблема алгоритма (dt1 > StaticResources.Dt)");
                
                double dt2 = StaticResources.Dt - dt1;

                // Координаты центров шаров в момент столкновения
                double x1_ = c1.x + v1_.PointVector.x * dt1;
                double y1_ = c1.y + v1_.PointVector.y * dt1;
                double x2_ = c2.x + v2_.PointVector.x * dt1;
                double y2_ = c2.y + v2_.PointVector.y * dt1;

                // Единичный вектор - n     (nx,ny)
                Vector n = Vector.GetUnitVectorN(new Point(x1_, y1_), new Point(x2_, y2_), R1, R2);
                // Единичный вектор - i     (-ny,nx)
                Vector i = Vector.GetUnitVectorI(new Point(x1_, y1_), new Point(x2_, y2_), R1, R2);

                // Скорость после удара
                Vector v1 = Physics.GetNewSpeedV1(v1_, v2_, i, n, m1, m2);
                Vector v2 = Physics.GetNewSpeedV2(v1_, v2_, i, n, m1, m2);
                Vector v21 = v2 - v1;

                // Координаты центров шаров по окончании столкновения спустя dt2
                double x1 = x1_ + v1.PointVector.x * dt2;
                double y1 = y1_ + v1.PointVector.y * dt2;
                double x2 = x2_ + v2.PointVector.x * dt2;
                double y2 = y2_ + v2.PointVector.y * dt2;

                Data newData1 = (Data)LastData.Clone();

                newData1.BallFirst.Position = new Point(x1_, y1_);
                newData1.BallSecond.Position = new Point(x2_, y2_);
                newData1.Dt = dt1 + LastData.Dt;
                DataList.Add(newData1); // до
                newData1.BallFirst.Color = StaticResources.DefaultColorBall1;
                newData1.BallSecond.Color = StaticResources.DefaultColorBall2;

                Data newData2 = (Data)LastData.Clone();
                newData2.BallFirst.Position = new Point(x1, y1);
                newData2.BallSecond.Position = new Point(x2, y2);
                newData2.Dt = dt2 + LastData.Dt + dt1;
                newData2.BallFirst.Speed = v1;
                newData2.BallSecond.Speed = v2;
                newData2.BallFirst.Color = StaticResources.DefaultColorBall1;
                newData2.BallSecond.Color = StaticResources.DefaultColorBall2;

                DataList.Add(newData2); // после
                Data.lastData = newData2;
                return ITERATION_CODE.COLLISION;
            }
            else
            {
                Data newData = new Data();
                newData.BallFirst = new Ball();
                newData.BallSecond = new Ball();
                double xn1 = LastData.BallFirst.Position.x + LastData.BallFirst.Speed.PointVector.x * StaticResources.Dt;
                double yn1 = LastData.BallFirst.Position.y + LastData.BallFirst.Speed.PointVector.y * StaticResources.Dt;
                newData.BallFirst.Position = new Point(xn1, yn1);
                newData.BallFirst.Speed = LastData.BallFirst.Speed;
                newData.BallFirst.Mass = LastData.BallFirst.Mass;
                newData.BallFirst.Radius = LastData.BallFirst.Radius;
                newData.BallFirst.Color = StaticResources.DefaultColorBall1;
                newData.BallSecond.Color = StaticResources.DefaultColorBall2;

                double xn2 = LastData.BallSecond.Position.x + LastData.BallSecond.Speed.PointVector.x * StaticResources.Dt;
                double yn2 = LastData.BallSecond.Position.y + LastData.BallSecond.Speed.PointVector.y * StaticResources.Dt;
                newData.BallSecond.Position = new Point(xn2, yn2);
                newData.BallSecond.Speed = LastData.BallSecond.Speed;
                newData.BallSecond.Mass = LastData.BallSecond.Mass;
                newData.BallSecond.Radius = LastData.BallSecond.Radius;
                newData.BallFirst.Color = StaticResources.DefaultColorBall1;
                newData.BallSecond.Color = StaticResources.DefaultColorBall2;

                newData.Dt = StaticResources.Dt + LastData.Dt;
                DataList.Add(newData);
                Data.lastData = newData;
                return ITERATION_CODE.NEXT;
            }

        }

        public static bool IsIntersection(Data data,Point pPhantom = null)
        {
            int r = data.BallFirst.Radius + data.BallSecond.Radius;
            Vector NV = data.BallSecond.Speed - data.BallFirst.Speed;
            Point Q;

            if (pPhantom == null)
            {
                 Q = data.BallSecond.Position - data.BallFirst.Position;
            }
            else {
                switch (MainWindow.clicked_ball)
                {
                    case CLICKED_BALL.BALL1:
                        Q = data.BallSecond.Position - pPhantom;
                        break;
                    case CLICKED_BALL.BALL2:
                        Q = pPhantom - data.BallFirst.Position;
                        break;
                    case CLICKED_BALL.NONE:
                        switch (MainWindow.clicked_vector_setup_ball)
                        {
                            case CLICKED_FOR_VECTOR_SETUP_BALL.NONE:
                                Q = data.BallSecond.Position - data.BallFirst.Position;
                                break;
                            case CLICKED_FOR_VECTOR_SETUP_BALL.BALL1:
                                Q = data.BallSecond.Position - data.BallFirst.Position;
                                NV = data.BallSecond.Speed - new Vector(pPhantom - data.BallFirst.Position);
                                break;
                            case CLICKED_FOR_VECTOR_SETUP_BALL.BALL2:
                                Q = data.BallSecond.Position - data.BallFirst.Position;
                                NV = new Vector(pPhantom- data.BallSecond.Position) - data.BallFirst.Speed;
                           
                                break;
                            default:
                                Q = data.BallSecond.Position - data.BallFirst.Position;
                                break;
                        }
                        break;
                    default:
                        Q = data.BallSecond.Position - data.BallFirst.Position;
                        break;
                }
            }

            
            if (new Vector(Q) * NV < 0)
            {
                Vector VX = new Vector(Q) + 1000 * NV;
                Point X = VX.PointVector;

                double dx = X.x - Q.x;
                double dy = X.y - Q.y;

                double a = dx * dx + dy * dy;
                double b = 2.0d * (Q.x * dx + Q.y * dy);
                double c = Q.x * Q.x + Q.y * Q.y - r * r;

                if (-b < 0) return (c < 0);
                if (-b < (2.0f * a)) return (4.0f * a * c - b * b < 0);
                return (a + b + c < 0);
            }
            else {

                return false;
            }
        }

    }
}
