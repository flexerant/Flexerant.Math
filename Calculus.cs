using System;
using System.Collections.Generic;
using System.Text;

namespace Flexerant.Math
{
    public class Calculus
    {
        public enum DerivativeApproximationMethods
        {
            CenteredDifference,
            ForwardDifference,
            BackwardDifference,
            CenteredFivePointDifference,
        }

        public static double Derivative(Func<double, double> f, double x, double h)
        {
            return Derivative(DerivativeApproximationMethods.CenteredFivePointDifference, f, x, h);
        }

        public static double Derivative(DerivativeApproximationMethods method, Func<double, double> f, double x, double h)
        {
            switch (method)
            {
                case DerivativeApproximationMethods.CenteredDifference:
                    return (f(x + h) - f(x - h)) / (2 * h);

                case DerivativeApproximationMethods.BackwardDifference:
                    return (3 * f(x) - 4 * f(x - h) + f(x - 2 * h)) / (2 * h);

                case DerivativeApproximationMethods.ForwardDifference:
                    return (-3 * f(x) + 4 * f(x + h) - f(x + 2 * h)) / (2 * h);

                default:
                    return (f(x - 2 * h) - 8 * f(x - h) + 8 * f(x + h) - f(x + 2 * h)) / (12 * h);
            }
        }

        public static decimal Derivative(DerivativeApproximationMethods method, Func<decimal, decimal> f, decimal x, decimal h)
        {
            Func<double, double> f2 = x =>
            {
                return Convert.ToDouble(f(Convert.ToDecimal(x)));
            };

            return Convert.ToDecimal(Derivative(method, f2, Convert.ToDouble(x), Convert.ToDouble(h)));
        }

        public static double Derivative(List<Point> list, int i)
        {
            double h = list[1].X - list[0].X;
            double? derivative;
            DerivativeApproximationMethods method = DerivativeApproximationMethods.CenteredDifference;
            int count = list.Count;

            if (i < 2)
            {
                method = DerivativeApproximationMethods.ForwardDifference;
            }
            else if (i > count - 3)
            {
                method = DerivativeApproximationMethods.BackwardDifference;
            }

            Func<int, double> f = x =>
            {
                return list[i].Y;
            };

            switch (method)
            {
                case DerivativeApproximationMethods.BackwardDifference:
                    derivative = GetBackwardDifference(list, i);
                    break;

                case DerivativeApproximationMethods.ForwardDifference:
                    derivative = GetForwardDifference(list, i);
                    break;

                default:
                    derivative = GetCenterDifference(list, i);
                    break;
            }

            if (!derivative.HasValue) throw new Exception();

            return derivative.Value;
        }

        private static double GetCenterDifference(List<Point> list, int i)
        {
            var h = (list[i + 1].X - list[i - 1].X) / 2;
            var f_i_plus_1 = list[i + 1].Y;
            var f_i_minus_1 = list[i - 1].Y;

            return (f_i_plus_1 - f_i_minus_1) / (2 * h);
        }

        private static double GetBackwardDifference(List<Point> list, int i)
        {
            var h = (list[i].X - list[i - 2].X) / 2;
            var f_i = list[i].Y;
            var f_i_minus_1 = list[i - 1].Y;
            var f_i_minus_2 = list[i - 2].Y;

            return (3 * f_i - 4 * f_i_minus_1 + f_i_minus_2) / (2 * h);
        }

        private static double GetForwardDifference(List<Point> list, int i)
        {
            var h = (list[i + 2].X - list[i].X) / 2;
            var f_i = list[i].Y;
            var f_i_plus_1 = list[i + 1].Y;
            var f_i_plus_2 = list[i + 2].Y;

            return (-3 * f_i + 4 * f_i_plus_1 - f_i_plus_2) / (2 * h);
        }
    }
}
