namespace Project_v3._0
{
    public class Point
    {
        public double x;
        public double y;
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static implicit operator System.Drawing.Point(Point point)
        {
            return new System.Drawing.Point((int)point.x, (int)point.y);
        }
        public static implicit operator Point(System.Drawing.Point point)
        {
            return new Point((double)point.X, (double)point.Y);
        }

        public static bool operator ==(Point a, Point b)
        {
            return (a?.x == b?.x && a?.y == b?.y);

        }
        public static bool operator !=(Point a, Point b)
        {
            return !(a==b);
        }
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y);

        }
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);

        }
        public static Point operator *(double a, Point b)
        {
            return new Point(a * b.x, a * b.y);
        }
        public static Point operator /(Point a, double b)
        {
            return new Point(a.x / b, a.y / b);
        }
        public static Point operator *(Point a, Point b)
        {
            return new Point(a.x * b.x, a.y * b.y);
        }
    }
}
