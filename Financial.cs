using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flexerant.Math
{
    public class Financial
    {
        public static decimal NPV(List<decimal> values, decimal r)
        {
            //*********************************************************************
            //* npv(x) = C0 + C1(1+r)^-1 + C2(1+r)^-2 + C3(1+r)^-3... + Cx(1+r)^x *
            //*********************************************************************

            return Convert.ToDecimal(NPV(values.Select(x => Convert.ToDouble(x)).ToList(), Convert.ToDouble(r)));
        }

        public static double NPV(List<double> values, double r)
        {
            //*********************************************************************
            //* npv(x) = C0 + C1(1+r)^-1 + C2(1+r)^-2 + C3(1+r)^-3... + Cx(1+r)^x *
            //*********************************************************************

            return values.Select((c, n) => c * System.Math.Pow((1 + r), -n)).Sum();
        }
               
        //******************************************************************************
        //* dnpv(x)/dx =  0 - C1(1+r) - 2C2(1+r)^-1 - 3C3(1+r)^-2... - xCx(1+r)^-(x+1) *
        //******************************************************************************
        private static double dNPVdr(List<double> values, double r)
        {            
            return values.Select((c, n) => -n * c * System.Math.Pow((1 + r), -(n + 1))).Sum();
        }

        public static decimal? IRR(List<decimal> values, decimal guess = 0)
        {
            return Convert.ToDecimal(IRR(values.Select(x => Convert.ToDouble(x)).ToList(), Convert.ToDouble(guess)));
        }

        public static double? IRR(List<double> values, double guess = 0)
        {
            Func<double, double> f = x =>
            {
                return NPV(values, x);
            };

            Func<double, double> df = x =>
            {
                return dNPVdr(values, x);
            };

            var results = RootFinding.NewtonRaphsonMethod(f, df, guess: 0, h: 0.0001);

            if (results.HasException)
            {
                throw results.Exception;
            }

            if (results.Converged)
            {
                return results.Value;
            }

            return null;
        }
    }
}
