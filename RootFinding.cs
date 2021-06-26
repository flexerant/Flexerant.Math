using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Flexerant.Math
{
    public class RootFinding
    {
        private static decimal Abs(decimal val)
        {
            return System.Math.Abs(val);
        }

        private static double Abs(double val)
        {
            return System.Math.Abs(val);
        }

        public static IterationResults<decimal> BrentMethodWithRetry(Func<decimal, decimal> f, decimal initialBounds, decimal retryFactor = 2, decimal tolerance = 0.0001m, uint maxIterations = 25)
        {
            Func<double, double> f2 = x =>
            {
                return Convert.ToDouble(f(Convert.ToDecimal(x)));
            };

            IterationResults<double> results = BrentMethodWithRetry(f2, Convert.ToDouble(initialBounds), Convert.ToDouble(retryFactor), Convert.ToDouble(tolerance), maxIterations);

            if (results.HasException)
            {
                return new IterationResults<decimal>(results.Exception);
            }

            return new IterationResults<decimal>(results.Value.ToDecimal(), results.IterationCount, results.FuctionCallCount, results.Converged);
        }

        //* If the orignal bounds don't converge, this method will retry by increasing the bounds by the specified factor.
        public static IterationResults<double> BrentMethodWithRetry(Func<double, double> f, double initialBounds, double retryFactor = 2, double tolerance = 0.0001, uint maxIterations = 25)
        {
            double left = -1 * initialBounds;
            double right = initialBounds;
            int iterationCount = 1;

            IterationResults<double> results = BrentMethod(f, left, right, tolerance, maxIterations);

            while (!results.Converged && (results.Exception is InvalidStartingBracketException))
            {
                //*******************************************************************************
                //* Change the left and right bounds by the specified factor on each iteration. *
                //*******************************************************************************
                left *= retryFactor;
                right *= retryFactor;
                iterationCount += 1;

                if (iterationCount > maxIterations)
                {
                    return new IterationResults<double>(new UnableToConvergeException($"A solution could not be found within {maxIterations} iterations."));
                }

                results = BrentMethod(f, left, right, tolerance, maxIterations);
            }

            return results;
        }

        public static IterationResults<decimal> BrentMethod(Func<decimal, decimal> f, decimal left, decimal right, double tolerance = 0.0001, uint maxIterations = 25)
        {
            Func<double, double> f2 = x =>
            {
                return Convert.ToDouble(f(Convert.ToDecimal(x)));
            };

            IterationResults<double> results = BrentMethod(f2, Convert.ToDouble(left), Convert.ToDouble(right), tolerance, maxIterations);

            if (results.HasException)
            {
                return new IterationResults<decimal>(results.Exception);
            }

            return new IterationResults<decimal>(results.Value.ToDecimal(), results.IterationCount, results.FuctionCallCount, results.Converged);
        }

        public static IterationResults<double> BrentMethod(Func<double, double> f, double left, double right, double tolerance = 0.0001, uint maxIterations = 25)
        {
            // Kudos to https://www.codeproject.com/Articles/79541/Three-Methods-for-Root-finding-in-C

            if (tolerance <= 0.0)
            {
                string msg = string.Format("Tolerance must be positive. Recieved {0}.", tolerance);
                return new IterationResults<double>(new ArgumentException(msg));
            }

            double errorEstimate = double.MaxValue;
            int functionCallCount = 0;

            double c, d, e, fa, fb, fc, tol, m, p, q, r, s;

            // set up aliases to match Brent's notation
            double a = left; double b = right; double t = tolerance;
            int iterationsUsed = 0;

            fa = f(a);
            fb = f(b);

            functionCallCount += 2;

            if (fa * fb > 0.0)
            {
                string str = "Invalid starting bracket. Function must be above target on one end and below target on other end.";
                return new IterationResults<double>(new InvalidStartingBracketException(str));
            }

        label_int:
            c = a; fc = fa; d = e = b - a;
        label_ext:
            if (Abs(fc) < Abs(fb))
            {
                a = b; b = c; c = a;
                fa = fb; fb = fc; fc = fa;
            }

            iterationsUsed++;

            tol = 2.0 * t * Abs(b) + t;
            errorEstimate = m = 0.5 * (c - b);

            if (Abs(m) > tol && fb != 0.0) // exact comparison with 0 is OK here
            {
                // See if bisection is forced
                if (Abs(e) < tol || Abs(fa) <= Abs(fb))
                {
                    d = e = m;
                }
                else
                {
                    s = fb / fa;
                    if (a == c)
                    {
                        // linear interpolation
                        p = 2.0 * m * s; q = 1.0 - s;
                    }
                    else
                    {
                        // Inverse quadratic interpolation
                        q = fa / fc; r = fb / fc;
                        p = s * (2.0 * m * q * (q - r) - (b - a) * (r - 1.0));
                        q = (q - 1.0) * (r - 1.0) * (s - 1.0);
                    }
                    if (p > 0.0)
                        q = -q;
                    else
                        p = -p;
                    s = e; e = d;
                    if (2.0 * p < 3.0 * m * q - Abs(tol * q) && p < Abs(0.5 * s * q))
                        d = p / q;
                    else
                        d = e = m;
                }
                a = b; fa = fb;
                if (Abs(d) > tol)
                    b += d;
                else if (m > 0.0)
                    b += tol;
                else
                    b -= tol;
                if (iterationsUsed == maxIterations)
                {
                    return new IterationResults<double>(b, iterationsUsed, functionCallCount, false);
                }

                fb = f(b);
                functionCallCount++;

                if ((fb > 0.0 && fc > 0.0) || (fb <= 0.0 && fc <= 0.0))
                    goto label_int;
                else
                    goto label_ext;
            }
            else
            {
                return new IterationResults<double>(b, iterationsUsed, functionCallCount, true);
            }
        }

        public static IterationResults<decimal> NewtonRaphsonMethod(Func<decimal, decimal> f, decimal guess = 0, int maxIterations = 25, decimal h = 0.01m, decimal error = 0.001m)
        {
            Func<double, double> dbf = x =>
            {
                return Convert.ToDouble(f(Convert.ToDecimal(x)));
            };

            var result = NewtonRaphsonMethod(dbf, Convert.ToDouble(guess), maxIterations, Convert.ToDouble(h), Convert.ToDouble(error));

            if (result.HasException)
            {
                return new IterationResults<decimal>(result.Exception);
            }

            return new IterationResults<decimal>(result.Value.ToDecimal(), result.IterationCount, result.FuctionCallCount, result.Converged);
        }

        public static IterationResults<double> NewtonRaphsonMethod(Func<double, double> f, double guess = 0, int maxIterations = 25, double h = 0.01, double error = 0.001)
        {
            Func<double, double> df = (x) =>
            {
                return Calculus.FirstDerivative(Calculus.DerivativeApproximationMethods.CenteredFivePointDifference, f, x, h);
            };

            return NewtonRaphsonMethod(f, df, guess, maxIterations, h, error);
        }

        public static IterationResults<decimal> NewtonRaphsonMethod(Func<decimal, decimal> f, Func<decimal, decimal> df, decimal guess = 0, int maxIterations = 25, double h = 0.01, double error = 0.001)
        {
            Func<double, double> f2 = x =>
            {
                return Convert.ToDouble(f(Convert.ToDecimal(x)));
            };

            Func<double, double> df2 = x =>
            {
                return Convert.ToDouble(df(Convert.ToDecimal(x)));
            };

            var result = NewtonRaphsonMethod(f2, df2, Convert.ToDouble(guess), maxIterations, Convert.ToDouble(h), Convert.ToDouble(error));

            if (result.HasException)
            {
                return new IterationResults<decimal>(result.Exception);
            }

            return new IterationResults<decimal>(result.Value.ToDecimal(), result.IterationCount, result.FuctionCallCount, result.Converged);
        }

        public static IterationResults<double> NewtonRaphsonMethod(Func<double, double> f, Func<double, double> df, double guess = 0, int maxIterations = 25, double h = 0.01, double error = 0.001)
        {
            double x = Convert.ToDouble(guess);
            double fx = f(x);
            double dfdx = df(x);
            int iteration = 1;
            int functionCallCount = 0;
            //double errorEstimate = 0;

            while ((System.Math.Abs(fx) > error) && (iteration < maxIterations))
            {
                //double x_minus_1 = x;

                x = x - fx / dfdx;
                //errorEstimate = Abs((x - x_minus_1) / x_minus_1);
                fx = f(x);
                dfdx = df(x);
                functionCallCount += 2;
                iteration++;
            }

            if (System.Math.Abs(fx) <= error)
            {
                return new IterationResults<double>(x, iteration, functionCallCount, true);
            }

            return new IterationResults<double>(x, iteration, functionCallCount, false);
        }


        public static IterationResults<decimal> BisectionMethod(Func<decimal, decimal> f, decimal left, decimal right, double tolerance = 0.0001, uint maxIterations = 25)
        {
            Func<double, double> f2 = x =>
            {
                return Convert.ToDouble(f(Convert.ToDecimal(x)));
            };

            IterationResults<double> results = BisectionMethod(f2, Convert.ToDouble(left), Convert.ToDouble(right), tolerance, maxIterations);

            if (results.HasException)
            {
                return new IterationResults<decimal>(results.Exception);
            }

            return new IterationResults<decimal>(results.Value.ToDecimal(), results.IterationCount, results.FuctionCallCount, results.Converged);
        }

        public static IterationResults<double> BisectionMethod(Func<double, double> f, double left, double right, double tolerance = 0.01, uint maxIterations = 25)
        {
            // Kudos to https://www.codeproject.com/Articles/79541/Three-Methods-for-Root-finding-in-C

            if (tolerance <= 0.0)
            {
                string msg = string.Format("Tolerance must be positive. Recieved {0}.", tolerance);
                return new IterationResults<double>(new ArgumentException(msg));
            }

            int functionCallCount = 2;
            int iterationsUsed = 0;
            //double errorEstimate = double.MaxValue;

            double g_left = f(left);  // evaluation of f at left end of interval
            double g_right = f(right);
            double mid;
            double g_mid;

            if (g_left * g_right >= 0.0)
            {
                string str = "Invalid starting bracket. Function must be above target on one end and below target on other end.";
                return new IterationResults<double>(new InvalidStartingBracketException(str));
            }

            if (g_left > g_right)
            {
                Swap(ref g_left, ref g_right);
                Swap(ref left, ref right);
            }

            double intervalWidth = right - left;

            for (iterationsUsed = 0; (iterationsUsed < maxIterations) && (intervalWidth > tolerance); iterationsUsed++)
            {
                intervalWidth *= 0.5;
                mid = left + intervalWidth;

                if ((g_mid = f(mid)) == 0.0)
                {
                    //errorEstimate = 0.0;
                    return new IterationResults<double>(mid, iterationsUsed, functionCallCount, iterationsUsed < maxIterations);
                }

                if (g_left * g_mid < 0.0)           // g changes sign in (left, mid)    
                    g_right = f(right = mid);
                else                            // g changes sign in (mid, right)
                    g_left = f(left = mid);

                functionCallCount += 2;
                //errorEstimate = intervalWidth / System.Math.Pow(2, iterationsUsed);
            }

            //errorEstimate = intervalWidth / System.Math.Pow(2, iterationsUsed);

            return new IterationResults<double>(left, iterationsUsed, functionCallCount, iterationsUsed < maxIterations);
        }

        public static IterationResults<decimal> BinarySearch(Func<decimal, decimal> f, decimal left, decimal right, uint maxIterations = 25)
        {
            Func<double, double> f2 = x =>
            {
                return Convert.ToDouble(f(Convert.ToDecimal(x)));
            };

            IterationResults<double> results = BinarySearch(f2, Convert.ToDouble(left), Convert.ToDouble(right), maxIterations);

            if (results.HasException)
            {
                return new IterationResults<decimal>(results.Exception);
            }

            return new IterationResults<decimal>(results.Value.ToDecimal(), results.IterationCount, results.FuctionCallCount, results.Converged);
        }

        public static IterationResults<double> BinarySearch(Func<double, double> f, double left, double right, double tolerance = 1, uint maxIterations = 25)
        {
            int functionCallCount = 2;
            int iterationsUsed = 0;
            double a = left;
            double b = right;
            double fa = f(a);
            double fb = f(b);

            if (tolerance <= 0)
            {
                return new IterationResults<double>(new ArgumentException("The tolerance must be greater than zero."));
            }

            if (tolerance >= System.Math.Abs(b - a))
            {
                return new IterationResults<double>(new ArgumentException("The tolerance must be less than the interval."));
            }

            if (fa * fb > 0.0)
            {
                return new IterationResults<double>(new InvalidStartingBracketException("Invalid starting bracket. Function must be above target on one end and below target on other end."));
            }

            if (a > b)
            {
                Swap(ref a, ref b);
                Swap(ref fa, ref fb);
            }

            //*************************************************************************************************
            //* Not sure if this condition will ever be true given the conditions above. Leave it for now but *
            //* a test should be written to verify its need.                                                  *
            //*************************************************************************************************
            if (fa >= fb)
            {
                return new IterationResults<double>(new ArgumentException("The function must be increasing over the entire interval."));
            }

            double mid = (a + b) / 2;
            double fmid = f(mid);
            double flast = fmid;

            iterationsUsed++;

            if (fmid == 0)
            {
                return new IterationResults<double>(mid, iterationsUsed, functionCallCount, true);
            }
            else
            {
                while ((b - a) > 0 && (iterationsUsed < maxIterations))
                {
                    mid = (a + b) / 2;
                    fmid = f(mid);
                    flast = fmid;
                    functionCallCount++;

                    if (fmid == 0)
                    {
                        return new IterationResults<double>(mid, iterationsUsed, functionCallCount, true);
                    }
                    else if (fmid > 0)
                    {
                        b = mid - tolerance;
                    }
                    else
                    {
                        a = mid + tolerance;
                    }

                    iterationsUsed++;
                }
            }

            return new IterationResults<double>((a + b) / 2, iterationsUsed, functionCallCount, (iterationsUsed < maxIterations));
        }

        private static void Swap<T>(ref T value1, ref T value2) where T : struct
        {
            var temp = value1;

            value1 = value2;
            value2 = temp;
        }
    }
}
