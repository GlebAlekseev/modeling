using System;
using System.Windows.Controls;
namespace Project_v3._0
{
    public class Data : ICloneable
    {
        public static Data DefaultArbitrary
        {
            get {
                Data DefaultData = new Data();

                DefaultData =(Data)Data.lastData.Clone();
                DefaultData.BallFirst.Mass = 10;
                DefaultData.BallSecond.Mass = 10;

                DefaultData.BallFirst.Radius = 8;
                DefaultData.BallSecond.Radius = 8;

                DefaultData.BallFirst.Speed = new Vector(new Point(20,0));
                DefaultData.BallSecond.Speed = new Vector(new Point(-20, 0));

                DefaultData.BallFirst.Position = new Point(Width/3,Height/2);
                DefaultData.BallSecond.Position = new Point(Width*2/3, Height / 2);

                DefaultData.Dt = StaticResources.Dt;

                return DefaultData;
            }
        }
        private static Point MaxPosition { get; set; } = new Point(Width, Height);

        private static Random rand = new Random();
        public double Dt { get; set; }
        public static int Height { get; set; } = 0;
        public static int Width { get; set; } = 0;
        public Ball BallFirst { get; set; }
        public Ball BallSecond { get; set; }
        public static Grid grid { get; set; }
        public static Grid gridChart_1 { get; set; }
        public static Grid gridChart_2 { get; set; }
        public static MainWindow this_ { get; set; }
        public static Data lastData { get; set; }


        public static Data GetRandom()
        {

            Data randomData = new Data();
            Ball ball_1 = new Ball();
            Ball ball_2 = new Ball();

            bool flag = true;
            while (flag)
            {

                ball_1.Mass = rand.Next(StaticResources.BallMinMass, StaticResources.BallMaxMass);
                ball_2.Mass = rand.Next(StaticResources.BallMinMass, StaticResources.BallMaxMass);

                ball_1.Radius = rand.Next(StaticResources.BallMinRadius, StaticResources.BallMaxRadius);
                ball_2.Radius = rand.Next(StaticResources.BallMinRadius, StaticResources.BallMaxRadius);

 
                switch (MainWindow.location_system)
                {
                    case LOCATION_SYSTEM.STRAIGHT:
                        ball_1.Position = new Point(rand.Next((int)StaticResources.BallMinPosition.x + 10, (int)Width - 50), Height/2);
                        ball_2.Position = new Point(rand.Next((int)StaticResources.BallMinPosition.x + 10, (int)Width - 50),
                            rand.Next((int)StaticResources.BallMinPosition.y + 10, (int)Height - 50));
                        ball_1.Speed = new Vector(new Point(rand.Next((int)StaticResources.BallMinSpeed.x, (int)StaticResources.BallMaxSpeed.x), 0));
                        ball_2.Speed = new Vector(new Point(rand.Next((int)StaticResources.BallMinSpeed.x, (int)StaticResources.BallMaxSpeed.x),
                            rand.Next((int)StaticResources.BallMinSpeed.y, (int)StaticResources.BallMaxSpeed.y)));
                        break;
                    case LOCATION_SYSTEM.ARBITRARY:
                        ball_1.Position = new Point(rand.Next((int)StaticResources.BallMinPosition.x + 10, (int)Width - 50),
                            rand.Next((int)StaticResources.BallMinPosition.y + 10, (int)Height - 50));
                        ball_2.Position = new Point(rand.Next((int)StaticResources.BallMinPosition.x + 10, (int)Width - 50),
                            rand.Next((int)StaticResources.BallMinPosition.y + 10, (int)Height - 50));
                        ball_1.Speed = new Vector(new Point(rand.Next((int)StaticResources.BallMinSpeed.x, (int)StaticResources.BallMaxSpeed.x),
                            rand.Next((int)StaticResources.BallMinSpeed.y, (int)StaticResources.BallMaxSpeed.y)));
                        ball_2.Speed = new Vector(new Point(rand.Next((int)StaticResources.BallMinSpeed.x, (int)StaticResources.BallMaxSpeed.x),
                            rand.Next((int)StaticResources.BallMinSpeed.y, (int)StaticResources.BallMaxSpeed.y)));
                        break;
                }
                randomData.BallFirst = ball_1;
                randomData.BallSecond = ball_2;
                if (new Vector(ball_2.Position,ball_1.Position).Module < (Width*2/3+ball_1.Radius+ball_2.Radius) && new Vector(ball_2.Position, ball_1.Position).Module > (Width * 1 / 3 +ball_1.Radius + ball_2.Radius))
                    if (Physics.IsIntersection(randomData))
                        break;
            }
            return randomData;
        }

        public void Print()
        {
            Console.WriteLine($"Data included: ==>>\n" +
                $"\t  BallFirst  \n{BallFirst.GetTextView()}\n" +
                $"\t  BallSecond \n{BallSecond.GetTextView()}");
        }
        public object Clone()
        {
            return new Data
            {

                BallFirst = this.BallFirst,
                BallSecond = this.BallSecond
            };
        }
    }
}
