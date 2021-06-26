using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flexerant.Math
{
    public class Financial
    {
        public static double NPV(List<double> values, double endingValue, double r)
        {
            values.Add(-1 * endingValue);

            //*********************************************************************
            //* npv(x) = C0 + C1(1+r)^-1 + C2(1+r)^-2 + C3(1+r)^-3... + Cx(1+r)^x *
            //*********************************************************************

            return values.Select((c, n) => c * System.Math.Pow((1 + r), -n)).Sum();
        }

        public static decimal NPV(List<decimal> values, decimal endingValue, decimal r)
        {
            return Convert.ToDecimal(NPV(values.Select(x => Convert.ToDouble(x)).ToList(), Convert.ToDouble(endingValue), Convert.ToDouble(r)));
        }

        //******************************************************************************
        //* dnpv(x)/dx =  0 - C1(1+r) - 2C2(1+r)^-1 - 3C3(1+r)^-2... - xCx(1+r)^-(x+1) *
        //******************************************************************************
        public static double dNPVdr(List<double> values, double endingValue, double r)
        {
            values.Add(-1 * endingValue);

            return values.Select((c, n) => -n * c * System.Math.Pow((1 + r), -(n + 1))).Sum();
        }

        public static decimal dNPVdr(List<decimal> values, decimal endingValue, decimal r)
        {
            return Convert.ToDecimal(dNPVdr(values.Select(x => Convert.ToDouble(x)).ToList(), Convert.ToDouble(endingValue), Convert.ToDouble(r)));
        }
    }
}
