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

        /// <summary>
        /// Calculates the derivative for a function. y = f'(x).
        /// </summary>
        /// <param name="f">The source function.</param>
        /// <param name="x">The value to calculate the derivative.</param>
        /// <param name="h">The interval between x values.</param>
        /// <returns></returns>
        public static double Derivative(Func<double, double> f, double x, double h)
        {
            return Derivative(DerivativeApproximationMethods.CenteredFivePointDifference, f, x, h);
        }

        /// <summary>
        /// Calculates the derivative for a function. y = f'(x).
        /// </summary>
        /// <param name="f">The source function.</param>
        /// <param name="x">The value to calculate the derivative.</param>
        /// <param name="h">The interval between x values.</param>
        /// <returns></returns>
        public static decimal Derivative(Func<decimal, decimal> f, decimal x, decimal h)
        {
            double f2(double x)
            {
                return Convert.ToDouble(f(Convert.ToDecimal(x)));
            }

            return Derivative(DerivativeApproximationMethods.CenteredFivePointDifference, f2, x.ToDouble(), h.ToDouble()).ToDecimal();
        }

        /// <summary>
        /// Calculates the derivative for a function. y = f'(x).
        /// </summary>
        /// <param name="method">The approximation method to use.</param>
        /// <param name="f">The source function.</param>
        /// <param name="x">The value to calculate the derivative.</param>
        /// <param name="h">The interval between x values.</param>
        /// <returns></returns>
        public static double Derivative(DerivativeApproximationMethods method, Func<double, double> f, double x, double h)
        {
            return method switch
            {
                DerivativeApproximationMethods.CenteredDifference => (f(x + h) - f(x - h)) / (2 * h),
                DerivativeApproximationMethods.BackwardDifference => (3 * f(x) - 4 * f(x - h) + f(x - 2 * h)) / (2 * h),
                DerivativeApproximationMethods.ForwardDifference => (-3 * f(x) + 4 * f(x + h) - f(x + 2 * h)) / (2 * h),
                _ => (f(x - 2 * h) - 8 * f(x - h) + 8 * f(x + h) - f(x + 2 * h)) / (12 * h),
            };
        }

        /// <summary>
        /// Calculates the derivative for a function. y = f'(x).
        /// </summary>
        /// <param name="method">The approximation method to use.</param>
        /// <param name="f">The source function.</param>
        /// <param name="x">The value to calculate the derivative.</param>
        /// <param name="h">The interval between x values.</param>
        /// <returns></returns>
        public static decimal Derivative(DerivativeApproximationMethods method, Func<decimal, decimal> f, decimal x, decimal h)
        {
            double f2(double x)
            {
                return Convert.ToDouble(f(Convert.ToDecimal(x)));
            }

            return Convert.ToDecimal(Derivative(method, f2, Convert.ToDouble(x), Convert.ToDouble(h)));
        }

        /// <summary>
        /// Calculates the derivative for a function. y = f'(x).
        /// </summary>
        /// <param name="list">The list of x,y values representing the original function.</param>
        /// <param name="i">The index of the list for which the derivative is to be calculated.</param>
        /// <returns></returns>
        public static double Derivative(List<Point> list, int i)
        {
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

            double? derivative = method switch
            {
                DerivativeApproximationMethods.BackwardDifference => GetBackwardDifference(list, i),
                DerivativeApproximationMethods.ForwardDifference => GetForwardDifference(list, i),
                _ => GetCenterDifference(list, i),
            };
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
