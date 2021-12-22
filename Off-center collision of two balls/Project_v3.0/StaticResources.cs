namespace Project_v3._0
{
    public class StaticResources
    {
        public static int ThreadDelay { get; set; } = 30;
        public static System.Drawing.Color DefaultColorBall { get; set; } = System.Drawing.Color.Red;
        public static System.Drawing.Color DefaultColorBall1 { get; set; } = System.Drawing.Color.Tomato;
        public static System.Drawing.Color DefaultColorBall2 { get; set; } = System.Drawing.Color.Cyan;
        public static System.Drawing.Color DefaultTrackColor { get; set; } = System.Drawing.Color.Purple;
        public static System.Drawing.Color DefaultColorVector { get; set; } = System.Drawing.Color.Black;

        public static int ChartPointsMax { get; set; } = 20;


        public static int BallMinMass { get; } = 10;
        public static int BallMaxMass { get; } = 100;
        public static int BallMinRadius { get; } = 10;
        public static int BallMaxRadius { get; } = 25;
        public static Point BallMinPosition { get; set; } = new Point(50, 50);
        public static Point BallMinSpeed { get; set; } = new Point(-150, -150);
        public static Point BallMaxSpeed { get; set; } = new Point(150, 150);

        public static double Dt { get; set; } = DefaultDt;
        public static double DefaultDt { get; } = 0.01;
/*        public static List<System.Drawing.Color> Colors = new List<System.Drawing.Color>() 
        { 
            System.Drawing.Color.Yellow, 
            System.Drawing.Color.Cyan, 
            System.Drawing.Color.Chocolate, 
            System.Drawing.Color.Aqua, 
            System.Drawing.Color.DeepPink, 
            System.Drawing.Color.Brown, 
            System.Drawing.Color.Indigo, 
            System.Drawing.Color.Orange, 
            System.Drawing.Color.Teal 
        };*/

    }
}
