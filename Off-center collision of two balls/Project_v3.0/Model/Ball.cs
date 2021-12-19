namespace Project_v3._0
{
    public class Ball
    {
        public Ball()
        {
        }
        public Point Position { get; set; }
        public double Mass { get; set; }
        public int Radius { get; set; }
        public Vector Speed { get; set; }
        public System.Drawing.Color Color { get; set; } = StaticResources.DefaultColorBall;
        public System.Drawing.Color TrackColor { get; set; } = StaticResources.DefaultTrackColor;
        public System.Drawing.Color ColorVector { get; set; } = StaticResources.DefaultColorVector;

        public string GetTextView()
        {
            return $"\n\t\tPosition : ({Position.x}, {Position.y})\n" +
                   $"\t\t Mass    : {Mass}\n" +
                   $"\t\t  Radius : {Radius}\n" +
                   $"\t\t   Speed : {{{Speed.PointVector.x}, {Speed.PointVector.y}}}\n";
        }
    }
}
