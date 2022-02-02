using System;
using System.Collections.Generic;
using System.Text;

namespace Flexerant.Math
{
    public struct Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point(decimal x, decimal y)
        {
            this.X = x.ToDouble();
            this.Y = y.ToDouble();
        }

        public Point(double x, decimal y)
        {
            this.X = x;
            this.Y = y.ToDouble();
        }

        public Point(decimal x, double y)
        {
            this.X = x.ToDouble();
            this.Y = y;
        }
    }
}
