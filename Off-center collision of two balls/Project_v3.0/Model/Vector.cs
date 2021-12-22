using System;

namespace Project_v3._0
{
    public class Vector
    {
        public Point PointVector;

        public double Module
        {
            get { return Math.Sqrt(this * this); }
        }

        public Vector(Point PointBegin, Point PointEnd)
        {
            this.PointVector = PointEnd - PointBegin;
        }
        public Vector(Point PointVector)
        {
            this.PointVector = PointVector;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.PointVector + b.PointVector);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.PointVector - b.PointVector);
        }
        public static Vector operator *(double a, Vector b)
        {
            return new Vector(a * b.PointVector);
        }
        public static Vector operator *(Vector a, double b)
        {
            return b * a;
        }
        public static Vector operator /(Vector a, double b)
        {
            return new Vector(a.PointVector / b);
        }
        public static double operator *(Vector a, Vector b)
        {
            Point tmp = a.PointVector * b.PointVector;
            return tmp.x + tmp.y;
        }

        public static Vector GetUnitVectorN(Point p1, Point p2, double R1, double R2)
        {
            double sumR = (R1 + R2);
            double x = (p2.x - p1.x) / sumR;
            double y = (p2.y - p1.y) / sumR;
            return new Vector(new Point(x, y));
        }
        public static Vector GetUnitVectorI(Point p1, Point p2, double R1, double R2)
        {
            double sumR = (R1 + R2);
            double x = (p2.x - p1.x) / sumR;
            double y = (p2.y - p1.y) / sumR;
            return new Vector(new Point(-y, x));
        }

        public string GetTextView() { return $"{{{PointVector.x}; {PointVector.y}}}"; }
    }
}
