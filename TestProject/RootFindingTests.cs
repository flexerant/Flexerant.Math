using Flexerant.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class RootFindingTests
    {
        [Fact]
        public void BrentMethod_Invalid_Starting_Bracket()
        {
            static double f(double x)
            {
                return 1;
            }

            var x = RootFinding.BrentMethod(f, 0, 0);
            var ex = x.Exception;

            Assert.True(x.HasException);
            Assert.IsType<InvalidStartingBracketException>(ex);
        }

        [Fact]
        public void BrentMethod_Unable_To_Converge()
        {
            static double f(double x)
            {
                if (x < 0) return -1;

                return 1;
            }

            var x = RootFinding.BrentMethod(f, -1000, 1000, maxIterations: 5);
            var ex = x.Exception;

            Assert.True(x.HasException);
            Assert.IsType<UnableToConvergeException>(ex);
        }

        [Fact]
        public void BrentMethod_Bad_Tolerance_Double()
        {
            var left = -1 * Math.PI / 2;
            var right = Math.PI / 2;

            var result = RootFinding.BrentMethod((x) => 1.0, left, right, tolerance: 0);

            Assert.True(result.HasException);
            Assert.IsType<ToleranceException>(result.Exception);
        }

        [Fact]
        public void BrentMethod_Bad_Tolerance_Decimal()
        {
            var left = (-1 * Math.PI / 2).ToDecimal();
            var right = (Math.PI / 2).ToDecimal();

            var result = RootFinding.BrentMethod((x) => 1.0m, left, right, tolerance: 0);

            Assert.True(result.HasException);
            Assert.IsType<ToleranceException>(result.Exception);
        }

        [Fact]
        public void BrentMethod_Double()
        {
            var left = -1 * Math.PI / 2;
            var right = Math.PI / 2;

            static double f(double x)
            {
                return Math.Sin(x - 1);
            }

            var x = RootFinding.BrentMethod(f, left, right);

            Assert.Equal(1d, x.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void BrentMethod_Decimal()
        {
            var left = Convert.ToDecimal(-1 * Math.PI / 2);
            var right = Convert.ToDecimal(Math.PI / 2);

            static decimal f(decimal x)
            {
                return (Math.Sin(x.ToDouble() - 1)).ToDecimal();
            }

            var x = RootFinding.BrentMethod(f, left, right);

            Assert.Equal(1m, x.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void NewtonRaphsonMethod_Double()
        {
            static double f(double x)
            {
                return Math.Sin(x - 1);
            }

            var x = RootFinding.NewtonRaphsonMethod(f);

            Assert.Equal(1d, x.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void NewtonRaphsonMethod_Decimal()
        {
            static decimal f(decimal x)
            {
                return (Math.Sin(x.ToDouble() - 1)).ToDecimal();
            }

            var x = RootFinding.NewtonRaphsonMethod(f);

            Assert.Equal(1m, x.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void NewtonRaphsonMethod_With_Derivative_Double()
        {
            var step = Math.PI / 10;

            double f(double x)
            {
                return Math.Sin(x - 1);
            }

            double df(double x)
            {
                return Calculus.Derivative(f, x, step);
            }

            var x = RootFinding.NewtonRaphsonMethod(f, df);

            Assert.Equal(1d, x.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void NewtonRaphsonMethod_With_Derivative_Decimal()
        {
            var step = (Math.PI / 10).ToDecimal();

            decimal f(decimal x)
            {
                return (Math.Sin(x.ToDouble() - 1)).ToDecimal();
            }

            decimal df(decimal x)
            {
                return Calculus.Derivative(f, x, step);
            }

            var x = RootFinding.NewtonRaphsonMethod(f, df);

            Assert.Equal(1m, x.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void BisectionMethod_Double()
        {
            var left = -1 * Math.PI / 2;
            var right = Math.PI / 2;

            static double f(double x)
            {
                return Math.Sin(x - 1);
            }

            var x = RootFinding.BisectionMethod(f, left, right);

            Assert.Equal(0.99, x.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void BisectionMethod_Decimal()
        {
            var right = Convert.ToDecimal(-1 * Math.PI / 2);
            var left = Convert.ToDecimal(Math.PI / 2);

            static decimal f(decimal x)
            {
                return (Math.Sin(x.ToDouble() - 1)).ToDecimal();
            }

            var x = RootFinding.BisectionMethod(f, left, right);

            Assert.Equal(1m, x.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void BisectionMethod_Bad_Tolerance_Double()
        {
            var left = -1 * Math.PI / 2;
            var right = Math.PI / 2;
            var result = RootFinding.BisectionMethod(x => 1d, left, right, tolerance: 0);

            Assert.True(result.HasException);
            Assert.IsType<ToleranceException>(result.Exception);
        }

        [Fact]
        public void BisectionMethod_Bad_Tolerance_Decimal()
        {
            var left = (-1 * Math.PI / 2).ToDecimal();
            var right = (Math.PI / 2).ToDecimal();
            var result = RootFinding.BisectionMethod(x => 1m, left, right, tolerance: 0);

            Assert.True(result.HasException);
            Assert.IsType<ToleranceException>(result.Exception);
        }

        [Fact]
        public void BisectionMethod_Bad_Starting_Bracket_Double()
        {
            var left = Math.PI / 2;
            var right = Math.PI / 2;
            var result = RootFinding.BisectionMethod(x => 1d, left, right);

            Assert.True(result.HasException);
            Assert.IsType<InvalidStartingBracketException>(result.Exception);
        }

        [Fact]
        public void BisectionMethod_Bad_Starting_Bracket_Decimal()
        {
            var left = (Math.PI / 2).ToDecimal();
            var right = (Math.PI / 2).ToDecimal();
            var result = RootFinding.BisectionMethod(x => 1m, left, right);

            Assert.True(result.HasException);
            Assert.IsType<InvalidStartingBracketException>(result.Exception);
        }

        [Fact]
        public void BinarySearch_Double()
        {
            var left = -1 * Math.PI / 2;
            var right = Math.PI / 2;

            static double f(double x)
            {
                return Math.Sin(x - 1);
            }

            var x = RootFinding.BinarySearch(f, left, right);

            Assert.Equal(0.64, x.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void BinarySearch_Decimal()
        {
            var left = Convert.ToDecimal(-1 * Math.PI / 2);
            var right = Convert.ToDecimal(Math.PI / 2);

            static decimal f(decimal x)
            {
                return (Math.Sin(x.ToDouble() - 1)).ToDecimal();
            }

            var x = RootFinding.BinarySearch(f, left, right);

            Assert.Equal(0.64m, x.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void BrentMethodWithRetry_No_Converge_Double()
        {
            var left = -100 * Math.PI / 2;
            var right = 100 * Math.PI / 2;

            static double f(double x)
            {
                return 2 * Math.Pow(x, 2) - Math.Pow(x, 3) - 2;
            }

            var (success, iterationCount) = RootFinding.BrentMethodWithRetry(f, left, right, out IterationResults<double> result, retryFactor: 1, maxIterations: 10);

            Assert.False(success);
            Assert.True(iterationCount > 1);
            Assert.True(result.HasException);
            Assert.IsType<RetryUnableToConvergeException>(result.Exception);
        }

        [Fact]
        public void BrentMethodWithRetry_No_Converge_Decimal()
        {
            var left = (-100 * Math.PI / 2).ToDecimal();
            var right = (100 * Math.PI / 2).ToDecimal();

            static decimal f(decimal x)
            {
                return (2 * Math.Pow(x.ToDouble(), 2) - Math.Pow(x.ToDouble(), 3) - 2).ToDecimal();
            }

            var (success, iterationCount) = RootFinding.BrentMethodWithRetry(f, left, right, out IterationResults<decimal> result, retryFactor: 1, maxIterations: 10);

            Assert.False(success);
            Assert.True(iterationCount > 1);
            Assert.True(result.HasException);
            Assert.IsType<RetryUnableToConvergeException>(result.Exception);
        }

        [Fact]
        public void BrentMethodWithRetry_Double()
        {
            var left = -100 * Math.PI / 2;
            var right = 100 * Math.PI / 2;

            static double f(double x)
            {
                return 2 * Math.Pow(x, 2) - Math.Pow(x, 3) - 2;
            }

            var (success, iterationCount) = RootFinding.BrentMethodWithRetry(f, left, right, out IterationResults<double> result, retryFactor: 0.1, maxIterations: 10);

            Assert.True(success);
            Assert.True(iterationCount > 1);
        }

        [Fact]
        public void BrentMethodWithRetry_Decimal()
        {
            var left = (-100 * Math.PI / 2).ToDecimal();
            var right = (100 * Math.PI / 2).ToDecimal();

            static decimal f(decimal x)
            {
                return (2 * Math.Pow(x.ToDouble(), 2) - Math.Pow(x.ToDouble(), 3) - 2).ToDecimal();
            }

            var (success, iterationCount) = RootFinding.BrentMethodWithRetry(f, left, right, out IterationResults<decimal> result, retryFactor: 0.1m, maxIterations: 10);

            Assert.True(success);
            Assert.True(iterationCount > 1);
        }
    }
}
