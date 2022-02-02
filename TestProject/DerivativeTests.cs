using Flexerant.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class DerivativeTests
    {
        [Fact]
        public void DerivativeTest_Default_Method()
        {
            double start = 0;
            double end = 2 * Math.PI;
            double step = Math.PI / 10;
            var maxError = 0.0003m;

            double f(double x)
            {
                return Math.Sin(x);
            }

            double df(double x)
            {
                return Calculus.Derivative(f, x, step);
            }

            List<decimal> errors = new();

            for (double x = start; x < end; x += step)
            {
                var fx = f(x);
                var dfxExpected = (Math.Cos(x)).ToDecimal();
                var dfxActual = df(x).ToDecimal();
                var error = Math.Abs(dfxExpected - dfxActual);

                errors.Add(error);
            }

            Assert.True(maxError.WithDecimalPlaces(4) >= errors.Max().WithDecimalPlaces(4), $"expected: {maxError.WithDecimalPlaces(4)}, actual: {errors.Max().WithDecimalPlaces(4)}");
        }

        [Theory]
        [InlineData(Calculus.DerivativeApproximationMethods.BackwardDifference, 0.0326)]
        [InlineData(Calculus.DerivativeApproximationMethods.CenteredDifference, 0.0164)]
        [InlineData(Calculus.DerivativeApproximationMethods.CenteredFivePointDifference, 0.0003)]
        [InlineData(Calculus.DerivativeApproximationMethods.ForwardDifference, 0.0326)]
        public void FirstDerivativeTests(Calculus.DerivativeApproximationMethods method, decimal maxError)
        {
            // f = sin(x)
            // df = cos(x)

            double start = 0;
            double end = 2 * Math.PI;
            double step = Math.PI / 10;

            double f(double x)
            {
                return Math.Sin(x);
            }

            double df(double x)
            {
                return Calculus.Derivative(method, f, x, step);
            }

            List<decimal> errors = new();

            for (double x = start; x < end; x += step)
            {
                var fx = f(x);
                var dfxExpected = (Math.Cos(x)).ToDecimal();
                var dfxActual = df(x).ToDecimal();
                var error = Math.Abs(dfxExpected - dfxActual);

                errors.Add(error);
            }

            Assert.True(maxError.WithDecimalPlaces(4) >= errors.Max().WithDecimalPlaces(4), $"expected: {maxError.WithDecimalPlaces(4)}, actual: {errors.Max().WithDecimalPlaces(4)}");
        }

        [Theory]
        [InlineData(Calculus.DerivativeApproximationMethods.BackwardDifference, 0.0662)]
        [InlineData(Calculus.DerivativeApproximationMethods.CenteredDifference, 0.0325)]
        [InlineData(Calculus.DerivativeApproximationMethods.CenteredFivePointDifference, 0.0006)]
        [InlineData(Calculus.DerivativeApproximationMethods.ForwardDifference, 0.0662)]
        public void SecondDerivativeTests_Double(Calculus.DerivativeApproximationMethods method, decimal maxError)
        {
            // f = sin(x)
            // df = cos(x)
            // df2 = -sin(x)

            double start = 0;
            double end = 2 * Math.PI;
            double step = Math.PI / 10;

            double f(double x)
            {
                return Math.Sin(x);
            }

            double df(double x)
            {
                return Calculus.Derivative(method, f, x, step);
            }

            double df2(double x)
            {
                return Calculus.Derivative(method, df, x, step);
            }

            List<decimal> errors = new();

            for (double x = start; x < end; x += step)
            {
                var df2xExpected = (-1 * Math.Sin(x)).ToDecimal();
                var df2xActual = df2(x).ToDecimal();
                var error = Math.Abs(df2xExpected - df2xActual);

                errors.Add(error);
            }

            var expectedMaxError = maxError.WithDecimalPlaces(4);
            var acutualMaxError = errors.Max().WithDecimalPlaces(4);

            Assert.True(expectedMaxError >= acutualMaxError, $"expected: {expectedMaxError}, actual: {acutualMaxError}");
        }

        [Theory]
        [InlineData(Calculus.DerivativeApproximationMethods.BackwardDifference, 0.0662)]
        [InlineData(Calculus.DerivativeApproximationMethods.CenteredDifference, 0.0325)]
        [InlineData(Calculus.DerivativeApproximationMethods.CenteredFivePointDifference, 0.0006)]
        [InlineData(Calculus.DerivativeApproximationMethods.ForwardDifference, 0.0662)]
        public void SecondDerivativeTests_Decimal(Calculus.DerivativeApproximationMethods method, decimal maxError)
        {
            // f = sin(x)
            // df = cos(x)
            // df2 = -sin(x)

            decimal start = 0;
            decimal end = (2 * Math.PI).ToDecimal();
            decimal step = (Math.PI / 10).ToDecimal();

            decimal f(decimal x)
            {
                return (Math.Sin(Convert.ToDouble(x))).ToDecimal();
            }

            decimal df(decimal x)
            {
                return Calculus.Derivative(method, f, x, step);
            }

            decimal df2(decimal x)
            {
                return Calculus.Derivative(method, df, x, step);
            }

            List<decimal> errors = new();

            for (decimal x = start; x < end; x += step)
            {
                var df2xExpected = (-1 * Math.Sin(Convert.ToDouble(x))).ToDecimal();
                var df2xActual = df2(x);
                var error = Math.Abs(df2xExpected - df2xActual);

                errors.Add(error);
            }

            var expectedMaxError = maxError.WithDecimalPlaces(4);
            var acutualMaxError = errors.Max().WithDecimalPlaces(4);

            Assert.True(expectedMaxError >= acutualMaxError, $"expected: {expectedMaxError}, actual: {acutualMaxError}");
        }

        [Fact]
        public void FirstDerivativeTestsList_Double()
        {
            decimal maxError = 0.01m;
            double start = 0;
            double end = 2 * Math.PI;
            double step = Math.PI / 20;
            List<Point> f = new();
            List<Point> df = new();
            List<decimal> errors = new();

            for (double x = start; x < end; x += step)
            {
                f.Add(new Point(x, Math.Sin(x)));
                df.Add(new Point(x, Math.Cos(x)));
            }

            for (int i = 0; i < f.Count; i++)
            {
                var expected = df[i].Y;
                var actual = Calculus.Derivative(f, i);
                var error = Math.Abs(expected - actual);

                errors.Add(Convert.ToDecimal(error));
            }

            Assert.True(maxError.WithDecimalPlaces(4) >= errors.Max().WithDecimalPlaces(4), $"expected: {maxError.WithDecimalPlaces(4)}, actual: {errors.Max().WithDecimalPlaces(4)}");
        }

        [Fact]
        public void FirstDerivativeTestsList_Decimal()
        {
            decimal maxError = 0.01m;
            decimal start = 0;
            decimal end = (2 * Math.PI).ToDecimal();
            decimal step = (Math.PI / 20).ToDecimal();
            List<Point> f = new();
            List<Point> df = new();
            List<decimal> errors = new();

            for (decimal x = start; x < end; x += step)
            {
                var y1 = Math.Sin(x.ToDouble());
                var y2 = Math.Cos(x.ToDouble());

                f.Add(new Point(x, y1));
                df.Add(new Point(x, y2));
            }

            for (int i = 0; i < f.Count; i++)
            {
                var expected = df[i].Y;
                var actual = Calculus.Derivative(f, i);
                var error = Math.Abs(expected - actual);

                errors.Add(Convert.ToDecimal(error));
            }

            Assert.True(maxError.WithDecimalPlaces(4) >= errors.Max().WithDecimalPlaces(4), $"expected: {maxError.WithDecimalPlaces(4)}, actual: {errors.Max().WithDecimalPlaces(4)}");
        }
    }
}
